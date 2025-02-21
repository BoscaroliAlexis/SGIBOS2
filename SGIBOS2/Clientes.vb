Imports System.Net.Http
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json
Imports SGIBOS.MySql.Data.MySqlClient

Public Class Clientes
    ' Declarar las variables a nivel de clase
    Dim conexion As MySqlConnection
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As DataTable

    ' Método para cargar los datos desde MySQL
    Private Sub CargarDatos(Optional ByVal busqueda As String = "")
        Try
            ' Cadena de conexión (ajusta los valores según tu servidor)
            Dim cadenaConexion As String = "Server=localhost;Database=tiendadb;Uid=root;Pwd=mysql;"
            conexion = New MySqlConnection(cadenaConexion)
            conexion.Open()

            ' Si hay texto en el campo de búsqueda, se ajusta la consulta
            Dim consulta As String
            If busqueda = "" Then
                consulta = "SELECT * FROM clientes" ' Muestra todos los registros
            Else
                consulta = "SELECT * FROM clientes WHERE nombre LIKE '%" & busqueda & "%'" ' Filtra por nombre
            End If

            ' Ejecutar la consulta y llenar el DataGridView
            adaptador = New MySqlDataAdapter(consulta, conexion)
            tabla = New DataTable()
            adaptador.Fill(tabla)
            dgvClientes.DataSource = tabla

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If conexion IsNot Nothing Then conexion.Close()
        End Try
    End Sub


    ' Evento Load para cargar los datos cuando se abre el formulario

    Private Sub btnGenerarReporteCli_Click(sender As Object, e As EventArgs) Handles btnGenerarReporteCli.Click
        For Each frm As Form In Application.OpenForms
            If TypeOf frm Is Clientes Then
                Dim frmClientes As Clientes = CType(frm, Clientes)
                Dim frmReportes As New Reportes(frmClientes.ObtenerDatosClientes())
                frmReportes.ShowDialog()
                frmReportes.StartPosition = FormStartPosition.CenterScreen

                Exit For
            End If
        Next

    End Sub

    Private Sub btnAñadirCli_Click(sender As Object, e As EventArgs) Handles btnAñadirCli.Click
        NuevoCliente.StartPosition = FormStartPosition.CenterScreen
        NuevoCliente.Show()
    End Sub

    Private Sub Clientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarDatos()
        ' Asigna el ContextMenuStrip al DataGridView
        dgvClientes.ContextMenuStrip = cmsActElim

    End Sub

    Private Async Sub Cllientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await LoadData()
    End Sub

    Private Async Function LoadData() As Task
        Try
            Dim client As New HttpClient()
            Dim response As HttpResponseMessage = Await client.GetAsync("http://localhost:5000/get_data")
            response.EnsureSuccessStatusCode()

            Dim responseBody As String = Await response.Content.ReadAsStringAsync()
            Dim dt As DataTable = JsonConvert.DeserializeObject(Of DataTable)(responseBody)

            dgvClientes.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Function

    Private Sub btnBuscarCliente_Click(sender As Object, e As EventArgs) Handles btnBuscarCliente.Click
        CargarDatos(txtBuscarCliente.Text)
    End Sub

    Private Sub dgvClientes_MouseClick(sender As Object, e As MouseEventArgs) Handles dgvClientes.MouseClick
        ' Verificar si se hizo clic derecho
        If e.Button = MouseButtons.Right Then
            ' Obtener la fila seleccionada
            Dim hit As DataGridView.HitTestInfo = dgvClientes.HitTest(e.X, e.Y)

            ' Verificar si se hizo clic en una fila válida
            If hit.RowIndex >= 0 Then
                ' Seleccionar la fila
                dgvClientes.ClearSelection()
                dgvClientes.Rows(hit.RowIndex).Selected = True

                ' Mostrar el menú contextual
                cmsActElim.Show(dgvClientes, e.Location)
            End If
        End If
    End Sub

    Private Sub ActualizarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualizarToolStripMenuItem.Click

        dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        ' Verificar si hay una fila seleccionada
        If dgvClientes.SelectedRows.Count > 0 Then
            ' Obtener los datos del cliente seleccionado
            Dim idCliente As Integer = dgvClientes.SelectedRows(0).Cells("id_cliente").Value
            Dim nombre As String = dgvClientes.SelectedRows(0).Cells("nombre").Value.ToString()
            Dim telefono As String = dgvClientes.SelectedRows(0).Cells("telefono").Value.ToString()
            Dim correo As String = dgvClientes.SelectedRows(0).Cells("correo").Value.ToString()
            Dim direccion As String = dgvClientes.SelectedRows(0).Cells("direccion").Value.ToString()

            ' Crear instancia de NuevoCliente y pasar los datos
            Dim formulario As New NuevoCliente()
            formulario.idCliente = idCliente
            formulario.txtNombre.Text = nombre
            formulario.txtTelefono.Text = telefono
            formulario.txtCorreo.Text = correo
            formulario.txtDireccion.Text = direccion

            ' Mostrar formulario
            formulario.ShowDialog()

            ' Actualizar DataGridView después de cerrar el formulario
            CargarDatos()
        Else
            MessageBox.Show("Seleccione un cliente para actualizar.")
        End If
    End Sub

    Private Sub EliminarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem.Click
        ' Verificar si hay una fila seleccionada
        If dgvClientes.SelectedRows.Count > 0 Then
            ' Obtener el ID del cliente seleccionado
            Dim idCliente As Integer = dgvClientes.SelectedRows(0).Cells("id_cliente").Value

            ' Confirmar eliminación
            Dim confirmacion As DialogResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este cliente?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If confirmacion = DialogResult.Yes Then
                ' Cadena de conexión
                Dim cadenaConexion As String = "Server=localhost;Database=tiendadb;Uid=root;Pwd=mysql;"
                Dim conexion As New MySqlConnection(cadenaConexion)

                Try
                    conexion.Open()
                    Dim consulta As String = "DELETE FROM Clientes WHERE id_cliente = @id"
                    Dim comando As New MySqlCommand(consulta, conexion)
                    comando.Parameters.AddWithValue("@id", idCliente)
                    comando.ExecuteNonQuery()

                    MessageBox.Show("Cliente eliminado correctamente.")
                    CargarDatos() ' Refrescar el DataGridView
                Catch ex As Exception
                    MessageBox.Show("Error al eliminar: " & ex.Message)
                Finally
                    conexion.Close()
                End Try
            End If
        Else
            MessageBox.Show("Por favor, seleccione un cliente para eliminar.")
        End If
    End Sub

    Public Function ObtenerDatosClientes() As DataTable
        Dim dt As New DataTable()

        ' Verificar que el DataGridView tenga datos
        If dgvClientes.Rows.Count > 0 Then
            ' Crear columnas en el DataTable basadas en las columnas del DataGridView
            For Each col As DataGridViewColumn In dgvClientes.Columns
                dt.Columns.Add(col.HeaderText)
            Next

            ' Agregar las filas al DataTable
            For Each row As DataGridViewRow In dgvClientes.Rows
                If Not row.IsNewRow Then
                    Dim newRow As DataRow = dt.NewRow()
                    For i As Integer = 0 To dgvClientes.Columns.Count - 1
                        newRow(i) = row.Cells(i).Value?.ToString()
                    Next
                    dt.Rows.Add(newRow)
                End If
            Next
        End If

        Return dt
    End Function



End Class