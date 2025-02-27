Imports MySql.Data.MySqlClient

Public Class NuevoVenta
    Private connectionString As String = "server=localhost;user id=root;password=mysql;database=tiendadb;"
    Private ventaID As Integer = -1
    Public Property VentaSeleccionada As Integer

    Private Sub NuevoVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ventaID <> -1 Then
            CargarDetalleVenta()
        End If
        CargarProductos()
    End Sub

    Public Sub SetVentaID(id As Integer)
        ventaID = id
    End Sub

    Private Sub CargarDetalleVenta()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT * FROM detalle_ventas WHERE id_venta = @idVenta"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@idVenta", ventaID)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    dgvDetalleVenta.DataSource = dt
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar detalles: " & ex.Message)
        End Try
    End Sub

    Private Sub btnGuardarSalir_Click(sender As Object, e As EventArgs) Handles btnGuardarSalir.Click
        Me.Close()
    End Sub

    Private Sub CargarProductos()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT id_producto, nombre FROM Productos"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    cmbProducto.DataSource = dt
                    cmbProducto.DisplayMember = "nombre"
                    cmbProducto.ValueMember = "id_producto"
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar productos: " & ex.Message)
        End Try
    End Sub

    Private Sub btnAñadirVen_Click(sender As Object, e As EventArgs) Handles btnAñadirVen.Click
        Try
            Dim idVenta As Integer = Integer.Parse(txtNumVenta.Text)
            Dim idProducto As Integer = Integer.Parse(cmbProducto.SelectedValue.ToString())
            Dim cantidad As Integer = Integer.Parse(txtCantidad.Text)
            Dim precioUnitario As Decimal = Decimal.Parse(txtPrecioUni.Text)
            Dim subtotal As Decimal = cantidad * precioUnitario

            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "INSERT INTO detalle_ventas (id_venta, id_producto, cantidad, precio_unitario, subtotal) VALUES (@idVenta, @idProducto, @cantidad, @precioUnitario, @subtotal)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@idVenta", idVenta)
                    cmd.Parameters.AddWithValue("@idProducto", idProducto)
                    cmd.Parameters.AddWithValue("@cantidad", cantidad)
                    cmd.Parameters.AddWithValue("@precioUnitario", precioUnitario)
                    cmd.Parameters.AddWithValue("@subtotal", subtotal)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MessageBox.Show("Detalle de venta añadido correctamente.")
            CargarDetalleVenta()
        Catch ex As Exception
            MessageBox.Show("Error al añadir detalle de venta: " & ex.Message)
        End Try
    End Sub
End Class