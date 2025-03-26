Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json
Imports System.Net.Http

Public Class Proveedores

    Dim conexion As MySqlConnection
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As DataTable

    Private Sub CargarDatos(Optional ByVal busqueda As String = "")
        Try
            conexion = New MySqlConnection(cadenaConexion)
            conexion.Open()

            Dim consulta As String
            If busqueda = "" Then
                consulta = "SELECT * FROM proveedores"
            Else
                consulta = "SELECT * FROM proveedores WHERE nombre LIKE '%" & busqueda & "%'" ' Filtra por nombre
            End If

            adaptador = New MySqlDataAdapter(consulta, conexion)
            tabla = New DataTable()
            adaptador.Fill(tabla)
            dgvProveedores.DataSource = tabla

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If conexion IsNot Nothing Then conexion.Close()
        End Try
    End Sub



    Private Sub btnGenerarReportePro_Click(sender As Object, e As EventArgs) Handles btnGenerarReportePro.Click
        For Each frm As Form In Application.OpenForms
            If TypeOf frm Is Proveedores Then
                Dim frmProveedores As Proveedores = CType(frm, Proveedores)
                Dim dtProveedores As DataTable = frmProveedores.ObtenerDatosProveedores()

                Dim frmReportes As New Reportes(Nothing, dtProveedores)
                frmReportes.ShowDialog()
                frmReportes.StartPosition = FormStartPosition.CenterScreen

                Exit For
            End If
        Next
    End Sub


    Private Sub btnAñadirPro_Click(sender As Object, e As EventArgs) Handles btnAñadirPro.Click
        NuevoProveedor.StartPosition = FormStartPosition.CenterScreen
        NuevoProveedor.Show()
    End Sub

    Private Sub Proveedores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarDatos()
        dgvProveedores.ContextMenuStrip = cmsActEli4

    End Sub

    Private Async Sub Pproveedores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await LoadData()
    End Sub

    Private Async Function LoadData() As Task
        Try
            Dim client As New HttpClient()
            Dim response As HttpResponseMessage = Await client.GetAsync("http://localhost:5000/get_data")
            response.EnsureSuccessStatusCode()

            Dim responseBody As String = Await response.Content.ReadAsStringAsync()
            Dim dt As DataTable = JsonConvert.DeserializeObject(Of DataTable)(responseBody)

            dgvProveedores.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Function

    Private Sub btnBuscarProveedores_Click(sender As Object, e As EventArgs) Handles btnBuscarProveedores.Click
        CargarDatos(txtBuscarProveedores.Text)
    End Sub

    Private Sub dgvProveeedores_MouseClick(sender As Object, e As MouseEventArgs) Handles dgvProveedores.MouseClick
        If e.Button = MouseButtons.Right Then
            Dim hit As DataGridView.HitTestInfo = dgvProveedores.HitTest(e.X, e.Y)


            If hit.RowIndex >= 0 Then
                dgvProveedores.ClearSelection()
                dgvProveedores.Rows(hit.RowIndex).Selected = True


                cmsActEli4.Show(dgvProveedores, e.Location)
            End If
        End If
    End Sub

    Private Sub ActualizarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualizarToolStripMenuItem.Click

        dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        If dgvProveedores.SelectedRows.Count > 0 Then
            Dim idProveedor As Integer = dgvProveedores.SelectedRows(0).Cells("id_proveedor").Value
            Dim nombre As String = dgvProveedores.SelectedRows(0).Cells("nombre").Value.ToString()
            Dim contacto As String = dgvProveedores.SelectedRows(0).Cells("contacto").Value.ToString()
            Dim telefono As String = dgvProveedores.SelectedRows(0).Cells("telefono").Value.ToString()
            Dim correo As String = dgvProveedores.SelectedRows(0).Cells("correo").Value.ToString()
            Dim direccion As String = dgvProveedores.SelectedRows(0).Cells("direccion").Value.ToString()

            Dim formulario As New NuevoProveedor()
            formulario.idProveedor = idProveedor
            formulario.txtNombre.Text = nombre
            formulario.txtContacto.Text = contacto
            formulario.txtTelefono.Text = telefono
            formulario.txtCorreo.Text = correo
            formulario.txtDireccion.Text = direccion

            formulario.ShowDialog()

            CargarDatos()
        Else
            MessageBox.Show("Seleccione un cliente para actualizar.")
        End If
    End Sub

    Private Sub EliminarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem.Click
        If dgvProveedores.SelectedRows.Count > 0 Then
            Dim idProveedor As Integer = dgvProveedores.SelectedRows(0).Cells("id_proveedor").Value

            Dim confirmacion As DialogResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este cliente?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If confirmacion = DialogResult.Yes Then
                Dim cadenaConexion As String = "Server=localhost;Database=tiendadb;Uid=root;Pwd=mysql;"
                Dim conexion As New MySqlConnection(cadenaConexion)

                Try
                    conexion.Open()
                    Dim consulta As String = "DELETE FROM Proveedores WHERE id_proveedor = @id"
                    Dim comando As New MySqlCommand(consulta, conexion)
                    comando.Parameters.AddWithValue("@id", idProveedor)
                    comando.ExecuteNonQuery()

                    MessageBox.Show("Cliente eliminado correctamente.")
                    CargarDatos()
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

    Public Function ObtenerDatosProveedores() As DataTable
        Dim dt As New DataTable()

        If dgvProveedores.Rows.Count > 0 Then
            For Each col As DataGridViewColumn In dgvProveedores.Columns
                dt.Columns.Add(col.HeaderText)
            Next

            For Each row As DataGridViewRow In dgvProveedores.Rows
                If Not row.IsNewRow Then
                    Dim newRow As DataRow = dt.NewRow()
                    For i As Integer = 0 To dgvProveedores.Columns.Count - 1
                        newRow(i) = row.Cells(i).Value?.ToString()
                    Next
                    dt.Rows.Add(newRow)
                End If
            Next
        End If

        Return dt
    End Function

End Class