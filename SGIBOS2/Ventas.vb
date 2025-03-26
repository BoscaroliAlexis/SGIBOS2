Imports MySql.Data.MySqlClient
Imports Mysqlx.Crud.Order.Types
Imports Newtonsoft.Json
Imports System.Net.Http

Public Class Ventas

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
                consulta = "SELECT * FROM Ventas"
            Else
                consulta = "SELECT * FROM Ventas WHERE metodo_pago LIKE '%" & busqueda & "%'" ' Filtra por metodo de pago
            End If

            adaptador = New MySqlDataAdapter(consulta, conexion)
            tabla = New DataTable()
            adaptador.Fill(tabla)
            dgvVentas.DataSource = tabla

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If conexion IsNot Nothing Then conexion.Close()
        End Try
    End Sub


    Private Sub btnGenerarReporteVen_Click(sender As Object, e As EventArgs) Handles btnGenerarReporteVen.Click
        For Each frm As Form In Application.OpenForms
            If TypeOf frm Is Ventas Then
                Dim frmVentas As Ventas = CType(frm, Ventas)
                Dim frmReportes As New Reportes(frmVentas.ObtenerDatosVentas())
                frmReportes.ShowDialog()
                frmReportes.StartPosition = FormStartPosition.CenterScreen

                Exit For
            End If
        Next

    End Sub

    Private Sub btnAñadirVen_Click(sender As Object, e As EventArgs) Handles btnAñadirVen.Click
        NuevoVenta.StartPosition = FormStartPosition.CenterScreen
        NuevoVenta.Show()
    End Sub

    Private Sub Ventas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarDatos()
        dgvVentas.ContextMenuStrip = cmsActEli5

    End Sub

    Private Async Sub VVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await LoadData()
    End Sub

    Private Async Function LoadData() As Task
        Try
            Dim client As New HttpClient()
            Dim response As HttpResponseMessage = Await client.GetAsync("http://localhost:5000/get_data")
            response.EnsureSuccessStatusCode()

            Dim responseBody As String = Await response.Content.ReadAsStringAsync()
            Dim dt As DataTable = JsonConvert.DeserializeObject(Of DataTable)(responseBody)

            dgvVentas.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Function

    Private Sub btnBuscarVentas_Click(sender As Object, e As EventArgs) Handles btnBuscarVentas.Click
        CargarDatos(txtBuscarVentas.Text)
    End Sub

    Private Sub dgvVentas_MouseClick(sender As Object, e As MouseEventArgs) Handles dgvVentas.MouseClick

        If e.Button = MouseButtons.Right Then
            Dim hit As DataGridView.HitTestInfo = dgvVentas.HitTest(e.X, e.Y)

            If hit.RowIndex >= 0 Then
                dgvVentas.ClearSelection()
                dgvVentas.Rows(hit.RowIndex).Selected = True

                cmsActEli5.Show(dgvVentas, e.Location)
            End If
        End If
    End Sub

    Private Sub ActualizarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualizarToolStripMenuItem.Click
        dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        If dgvVentas.SelectedRows.Count > 0 Then
            Dim idVenta As Integer = dgvVentas.SelectedRows(0).Cells("id_venta").Value
            Dim fechaVenta As String = dgvVentas.SelectedRows(0).Cells("fecha_venta").Value.ToString()
            Dim idCliente As Integer = dgvVentas.SelectedRows(0).Cells("id_cliente").Value
            Dim totalVenta As Decimal = dgvVentas.SelectedRows(0).Cells("total_venta").Value
            Dim metodoPago As String = dgvVentas.SelectedRows(0).Cells("metodo_pago").Value.ToString()
            Dim idUsuario As Integer = dgvVentas.SelectedRows(0).Cells("id_usuario").Value

            Dim formulario As New NuevoVenta()

            formulario.txtNumVenta.Text = idVenta
            formulario.txtNumVenta2.Text = idVenta
            formulario.lblFechaActual.Text = fechaVenta
            formulario.txtCliente.Text = idCliente
            formulario.lblTotalVenta.Text = totalVenta
            formulario.cmbMetodoPago.Text = metodoPago
            formulario.SetVentaID(idVenta)

            formulario.ShowDialog()

            CargarDatos()
        Else
            MessageBox.Show("Seleccione una venta para actualizar.")
        End If
    End Sub

    Private Sub EliminarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem.Click
        If dgvVentas.SelectedRows.Count > 0 Then
            Dim idVenta As Integer = dgvVentas.SelectedRows(0).Cells("id_venta").Value

            Dim confirmacion As DialogResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este cliente?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If confirmacion = DialogResult.Yes Then
                Dim cadenaConexion As String = "Server=localhost;Database=tiendadb;Uid=root;Pwd=mysql;"
                Dim conexion As New MySqlConnection(cadenaConexion)

                Try
                    conexion.Open()
                    Dim consulta As String = "DELETE FROM Ventas WHERE id_venta = @id"
                    Dim comando As New MySqlCommand(consulta, conexion)
                    comando.Parameters.AddWithValue("@id", idVenta)
                    comando.ExecuteNonQuery()

                    MessageBox.Show("Venta eliminada correctamente.")
                    CargarDatos()
                Catch ex As Exception
                    MessageBox.Show("Error al eliminar: " & ex.Message)
                Finally
                    conexion.Close()
                End Try
            End If
        Else
            MessageBox.Show("Por favor, seleccione una venta para eliminar.")
        End If
    End Sub

    Public Function ObtenerDatosVentas() As DataTable
        Dim dt As New DataTable()

        If dgvVentas.Rows.Count > 0 Then
            For Each col As DataGridViewColumn In dgvVentas.Columns
                dt.Columns.Add(col.HeaderText)
            Next

            For Each row As DataGridViewRow In dgvVentas.Rows
                If Not row.IsNewRow Then
                    Dim newRow As DataRow = dt.NewRow()
                    For i As Integer = 0 To dgvVentas.Columns.Count - 1
                        newRow(i) = row.Cells(i).Value?.ToString()
                    Next
                    dt.Rows.Add(newRow)
                End If
            Next
        End If

        Return dt
    End Function


End Class