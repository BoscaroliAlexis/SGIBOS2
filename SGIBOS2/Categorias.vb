Imports System.Net.Http
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json
Imports SGIBOS.MySql.Data.MySqlClient
Public Class Categorias

    Dim conexion As MySqlConnection
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As DataTable


    Private Sub CargarDatos(Optional ByVal busqueda As String = "")
        Try
            ' Cadena de conexión (ajusta los valores según tu servidor)
            conexion = New MySqlConnection(cadenaConexion)
            conexion.Open()

            ' Si hay texto en el campo de búsqueda, se ajusta la consulta
            Dim consulta As String
            If busqueda = "" Then
                consulta = "SELECT * FROM categorias" ' Muestra todos los registros
            Else
                consulta = "SELECT * FROM categorias WHERE nombre LIKE '%" & busqueda & "%'" ' Filtra por nombre
            End If

            ' Ejecutar la consulta y llenar el DataGridView
            adaptador = New MySqlDataAdapter(consulta, conexion)
            tabla = New DataTable()
            adaptador.Fill(tabla)
            dgvCategorias.DataSource = tabla

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If conexion IsNot Nothing Then conexion.Close()
        End Try
    End Sub
    Private Sub btnAñadirCat_Click(sender As Object, e As EventArgs) Handles btnAñadirCat.Click
        NuevaCategoria.StartPosition = FormStartPosition.CenterScreen
        NuevaCategoria.Show()
    End Sub


    Private Sub Categorias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarDatos()
        ' Asigna el ContextMenuStrip al DataGridView
        dgvCategorias.ContextMenuStrip = cmsActEli3

    End Sub

    Private Sub btnBuscarCat_Click(sender As Object, e As EventArgs) Handles btnBuscarCat.Click
        CargarDatos(txtBuscarCat.Text)
    End Sub

    Private Async Sub CCategorias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await LoadData()
    End Sub

    Private Async Function LoadData() As Task
        Try
            Dim client As New HttpClient()
            Dim response As HttpResponseMessage = Await client.GetAsync("http://localhost:5000/get_data")
            response.EnsureSuccessStatusCode()

            Dim responseBody As String = Await response.Content.ReadAsStringAsync()
            Dim dt As DataTable = JsonConvert.DeserializeObject(Of DataTable)(responseBody)

            dgvCategorias.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Function

    Private Sub dgvCategorias_MouseClick(sender As Object, e As MouseEventArgs) Handles dgvCategorias.MouseClick
        ' Verificar si se hizo clic derecho
        If e.Button = MouseButtons.Right Then
            ' Obtener la fila seleccionada
            Dim hit As DataGridView.HitTestInfo = dgvCategorias.HitTest(e.X, e.Y)

            ' Verificar si se hizo clic en una fila válida
            If hit.RowIndex >= 0 Then
                ' Seleccionar la fila
                dgvCategorias.ClearSelection()
                dgvCategorias.Rows(hit.RowIndex).Selected = True

                ' Mostrar el menú contextual
                cmsActEli3.Show(dgvCategorias, e.Location)
            End If
        End If
    End Sub

    Private Sub ActualizarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualizarToolStripMenuItem.Click

        dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        ' Verificar si hay una fila seleccionada
        If dgvCategorias.SelectedRows.Count > 0 Then
            ' Obtener los datos del cliente seleccionado
            Dim idCategoria As Integer = dgvCategorias.SelectedRows(0).Cells("id_categoria").Value
            Dim nombre As String = dgvCategorias.SelectedRows(0).Cells("nombre").Value.ToString()

            ' Crear instancia de NuevoCliente y pasar los datos
            Dim formulario As New NuevaCategoria()
            formulario.idCategoria = idCategoria
            formulario.txtNombreCat.Text = nombre


            ' Mostrar formulario
            formulario.ShowDialog()

            ' Actualizar DataGridView después de cerrar el formulario
            CargarDatos()
        Else
            MessageBox.Show("Seleccione una categoria para actualizar.")
        End If
    End Sub

    Private Sub EliminarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem.Click
        ' Verificar si hay una fila seleccionada
        If dgvCategorias.SelectedRows.Count > 0 Then
            ' Obtener el ID del cliente seleccionado
            Dim idCategoria As Integer = dgvCategorias.SelectedRows(0).Cells("id_categoria").Value

            ' Confirmar eliminación
            Dim confirmacion As DialogResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este cliente?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If confirmacion = DialogResult.Yes Then
                ' Cadena de conexión
                Dim cadenaConexion As String = "Server=localhost;Database=tiendadb;Uid=root;Pwd=mysql;"
                Dim conexion As New MySqlConnection(cadenaConexion)

                Try
                    conexion.Open()
                    Dim consulta As String = "DELETE FROM Categorias WHERE id_categoria = @id"
                    Dim comando As New MySqlCommand(consulta, conexion)
                    comando.Parameters.AddWithValue("@id", idCategoria)
                    comando.ExecuteNonQuery()

                    MessageBox.Show("Categoria eliminado correctamente.")
                    CargarDatos() ' Refrescar el DataGridView
                Catch ex As Exception
                    MessageBox.Show("Error al eliminar: " & ex.Message)
                Finally
                    conexion.Close()
                End Try
            End If
        Else
            MessageBox.Show("Por favor, seleccione una categoria para eliminar.")
        End If
    End Sub

    Public Function ObtenerDatosCategorias() As DataTable
        Dim dt As New DataTable()

        ' Verificar que el DataGridView tenga datos
        If dgvCategorias.Rows.Count > 0 Then
            ' Crear columnas en el DataTable basadas en las columnas del DataGridView
            For Each col As DataGridViewColumn In dgvCategorias.Columns
                dt.Columns.Add(col.HeaderText)
            Next

            ' Agregar las filas al DataTable
            For Each row As DataGridViewRow In dgvCategorias.Rows
                If Not row.IsNewRow Then
                    Dim newRow As DataRow = dt.NewRow()
                    For i As Integer = 0 To dgvCategorias.Columns.Count - 1
                        newRow(i) = row.Cells(i).Value?.ToString()
                    Next
                    dt.Rows.Add(newRow)
                End If
            Next
        End If

        Return dt
    End Function
End Class