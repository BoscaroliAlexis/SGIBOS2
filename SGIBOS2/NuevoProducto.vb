Imports MySql.Data.MySqlClient

Public Class NuevoProducto

    Public EsActualizar As Boolean = False

    Public idProducto As Integer = 0

    Dim comando As MySqlCommand



    Sub LlenarComboBox(ByVal consulta As String, ByVal combo As ComboBox, ByVal idCampo As String, ByVal nombreCampo As String)
        Try
            Dim adaptador As New MySqlDataAdapter(consulta, cadenaConexion)
            Dim tabla As New DataTable()
            adaptador.Fill(tabla)

            combo.DataSource = tabla
            combo.DisplayMember = nombreCampo
            combo.ValueMember = idCampo
        Catch ex As Exception
            MessageBox.Show("Error al llenar el ComboBox: " & ex.Message)
        End Try
    End Sub

    Sub CargarComboCategoria()
        LlenarComboBox("SELECT id_categoria, nombre FROM Categorias", cmbCategoria, "id_categoria", "nombre")
        cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    Sub CargarComboProveedor()
        LlenarComboBox("SELECT id_proveedor, nombre FROM Proveedores", cmbProveedor, "id_proveedor", "nombre")
        cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub
    Private Sub NuevoProducto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not EsActualizar Then
            CargarComboProveedor()
            CargarComboCategoria()
        End If
    End Sub


    Private Sub cmbCategoria_DropDown(sender As Object, e As EventArgs) Handles cmbCategoria.DropDown
        CargarComboCategoria()
    End Sub
    Private Sub cmbProveedor_DropDown(sender As Object, e As EventArgs) Handles cmbProveedor.DropDown
        CargarComboProveedor()
    End Sub


    Private Sub btnAñadirPro_Click(sender As Object, e As EventArgs) Handles btnAñadirPro.Click
        ' Establecer conexión
        Dim conexion As New MySqlConnection(cadenaConexion)

        Try
            conexion.Open()
            Dim consulta As String

            If idProducto = 0 Then
                consulta = "INSERT INTO Productos (nombre, descripcion, precio, cantidad_stock, id_categoria, id_proveedor, fecha_creacion, activo) " &
               "VALUES (@nombre, @descripcion, @precio, @cantidad_stock, @id_categoria, @id_proveedor, NOW(), @activo)"
            Else
                consulta = "UPDATE Productos SET nombre=@nombre, descripcion=@descripcion, precio=@precio, cantidad_stock=@cantidad_stock, " &
               "id_categoria=@id_categoria, id_proveedor=@id_proveedor, activo=@activo WHERE id_producto=@id_producto"
            End If

            Dim comando As New MySqlCommand(consulta, conexion)
            comando.Parameters.AddWithValue("@nombre", txtNombre.Text)
            comando.Parameters.AddWithValue("@descripcion", txtDescripcion.Text)
            comando.Parameters.AddWithValue("@precio", txtPrecio.Text)
            comando.Parameters.AddWithValue("@cantidad_stock", txtCantidadStock.Text)
            comando.Parameters.AddWithValue("@id_categoria", cmbCategoria.SelectedValue)
            comando.Parameters.AddWithValue("@id_proveedor", cmbProveedor.SelectedValue)
            comando.Parameters.AddWithValue("@activo", cmbActivo.SelectedValue)



            If idProducto > 0 Then
                comando.Parameters.AddWithValue("@id_producto", idProducto)
            End If

            comando.ExecuteNonQuery()
            MessageBox.Show("Producto guardado correctamente.")

            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error al guardar: " & ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub


End Class