Imports MySql.Data.MySqlClient

Public Class NuevoVenta
    Private ventaID As Integer = -1
    Public Property VentaSeleccionada As Integer

    Private Sub NuevoVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ventaID <> -1 Then
            CargarDetalleVenta()
        End If
        CargarProductos()

        ObtenerSiguienteNumeroVenta()
        ObtenerSiguienteNumeroDetalleVenta()
        CargarMetodosPago()
    End Sub

    Public Sub SetVentaID(id As Integer)
        ventaID = id
    End Sub

    Private Sub CargarDetalleVenta()
        Try
            Using conn As New MySqlConnection(cadenaConexion)
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
            Using conn As New MySqlConnection(cadenaConexion)
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

            Using conn As New MySqlConnection(cadenaConexion)
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

    Private Sub ObtenerSiguienteNumeroVenta()
        Try
            ' Cadena de conexión a la base de datos
            Dim conexion As New MySqlConnection(cadenaConexion)
            Dim comando As New MySqlCommand("SELECT IFNULL(MAX(id_venta), 0) + 1 FROM Ventas", conexion)

            conexion.Open()
            Dim siguienteID As Integer = Convert.ToInt32(comando.ExecuteScalar()) ' Obtiene el valor de la consulta
            conexion.Close()

            txtNumVenta.Text = siguienteID.ToString()
            txtNumVenta2.Text = siguienteID.ToString() ' Muestra el valor en el TextBox

        Catch ex As Exception
            MessageBox.Show("Error al obtener el número de venta: " & ex.Message)
        End Try
    End Sub

    Private Sub ObtenerSiguienteNumeroDetalleVenta()
        Try
            ' Cadena de conexión a la base de datos
            Dim conexion As New MySqlConnection(cadenaConexion)
            Dim comando As New MySqlCommand("SELECT IFNULL(MAX(id_detalle_ventas), 0) + 1 FROM Detalle_ventas", conexion)

            conexion.Open()
            Dim siguienteID As Integer = Convert.ToInt32(comando.ExecuteScalar()) ' Obtiene el valor de la consulta
            conexion.Close()

            txtIDdetalle.Text = siguienteID.ToString()

        Catch ex As Exception
            MessageBox.Show("Error al obtener el número de detalle venta: " & ex.Message)
        End Try
    End Sub

    Private Function ObtenerPrecioProducto(idProducto As Integer) As Decimal
        Dim precio As Decimal = 0

        Try

            Dim conexion As New MySqlConnection(cadenaConexion)
            Dim comando As New MySqlCommand("SELECT precio FROM Productos WHERE id_producto = @idProducto", conexion)
            comando.Parameters.AddWithValue("@idProducto", idProducto)

            conexion.Open()
            Dim resultado As Object = comando.ExecuteScalar()
            conexion.Close()


            If resultado IsNot Nothing Then
                precio = Convert.ToDecimal(resultado)
            End If

        Catch ex As Exception
            MessageBox.Show("Error al obtener el precio: " & ex.Message)
        End Try

        Return precio
    End Function


    Private Sub cmbProducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProducto.SelectedIndexChanged
        If TypeOf cmbProducto.SelectedValue Is DataRowView Then
            Dim row As DataRowView = DirectCast(cmbProducto.SelectedItem, DataRowView)
            Dim idProducto As Integer = Convert.ToInt32(row("id_producto"))
            txtPrecioUni.Text = ObtenerPrecioProducto(idProducto).ToString("0.00")
        ElseIf cmbProducto.SelectedValue IsNot Nothing Then
            Dim idProducto As Integer = Convert.ToInt32(cmbProducto.SelectedValue)
            txtPrecioUni.Text = ObtenerPrecioProducto(idProducto).ToString("0.00")
        End If
    End Sub

    Private Sub txtCantidad_TextChanged(sender As Object, e As EventArgs) Handles txtCantidad.TextChanged
        CalcularSubtotal()
    End Sub
    Private Sub CalcularSubtotal()
        Dim cantidad As Integer
        Dim precio As Decimal

        If Integer.TryParse(txtCantidad.Text, cantidad) AndAlso cantidad >= 0 Then
            If Decimal.TryParse(txtPrecioUni.Text, precio) Then
                Dim subtotal As Decimal = cantidad * precio
                lblSubtotal.Text = subtotal.ToString("0.00")
            Else
                lblSubtotal.Text = "0.00"
            End If
        Else
            lblSubtotal.Text = "0.00"
        End If
    End Sub

    Private Sub CargarMetodosPago()
        cmbMetodoPago.Items.Add("Efectivo")
        cmbMetodoPago.Items.Add("Tarjeta")
        cmbMetodoPago.Items.Add("Transferencia")

        cmbMetodoPago.SelectedIndex = 0
        cmbMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

End Class