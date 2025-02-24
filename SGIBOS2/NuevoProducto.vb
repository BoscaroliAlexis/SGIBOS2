Imports MySql.Data.MySqlClient

Public Class NuevoProducto

    Public idProducto As Integer = 0

    Dim comando As MySqlCommand


    ' Definir la conexión global
    Dim conexion As New MySqlConnection("Server=localhost;Database=tiendadb;User=root;Password=mysql;")


    Sub LlenarComboBox(ByVal consulta As String, ByVal combo As ComboBox, ByVal idCampo As String, ByVal nombreCampo As String)
        Try
            Dim adaptador As New MySqlDataAdapter(consulta, conexion)
            Dim tabla As New DataTable()
            adaptador.Fill(tabla)

            ' Configurar el ComboBox
            combo.DataSource = tabla
            combo.DisplayMember = nombreCampo  ' Se muestra el nombre
            combo.ValueMember = idCampo  ' Se guarda el ID
        Catch ex As Exception
            MessageBox.Show("Error al llenar el ComboBox: " & ex.Message)
        End Try
    End Sub

    Sub CargarCombos()
        ' Llenar cmbCategoria con datos de la tabla Categorias
        LlenarComboBox("SELECT id_categoria, nombre FROM Categorias", cmbCategoria, "id_categoria", "nombre")

        ' Llenar cmbProveedor con datos de la tabla Proveedores
        LlenarComboBox("SELECT id_proveedor, nombre FROM Proveedores", cmbProveedor, "id_proveedor", "nombre")
    End Sub

    Private Sub NuevoProducto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarCombos()
    End Sub

    Private Sub btnAñadirPro_Click(sender As Object, e As EventArgs) Handles btnAñadirPro.Click
        ' Establecer conexión
        Dim cadenaConexion As String = "Server=localhost;Database=tiendadb;Uid=root;Pwd=mysql;"
        Dim conexion As New MySqlConnection(cadenaConexion)

        Try
            conexion.Open()
            Dim consulta As String

            If idProducto = 0 Then
                ' INSERT si no hay un producto seleccionado
                consulta = "INSERT INTO Productos (nombre, descripcion, precio, cantidad_stock, id_categoria, id_proveedor, fecha_creacion, activo) " &
               "VALUES (@nombre, @descripcion, @precio, @cantidad_stock, @id_categoria, @id_proveedor, NOW(), @activo)"
            Else
                ' UPDATE si se está editando un producto
                consulta = "UPDATE Productos SET nombre=@nombre, descripcion=@descripcion, precio=@precio, cantidad_stock=@cantidad_stock, " &
               "id_categoria=@id_categoria, id_proveedor=@id_proveedor, activo=@activo WHERE id_producto=@id_producto"
            End If

            ' Crear y ejecutar comando
            Dim comando As New MySqlCommand(consulta, conexion)
            comando.Parameters.AddWithValue("@nombre", txtNombre.Text)
            comando.Parameters.AddWithValue("@descripcion", txtDescripcion.Text)
            comando.Parameters.AddWithValue("@precio", txtPrecio.Text)
            comando.Parameters.AddWithValue("@cantidad_stock", txtCantidadStock.Text)
            comando.Parameters.AddWithValue("@id_categoria", cmbCategoria.SelectedValue)
            comando.Parameters.AddWithValue("@id_proveedor", cmbProveedor.SelectedValue)
            comando.Parameters.AddWithValue("@activo", cmbActivo.SelectedValue)



            If idProducto > 0 Then
                comando.Parameters.AddWithValue("@id_cliente", idProducto)
            End If

            comando.ExecuteNonQuery()
            MessageBox.Show("Producto guardado correctamente.")

            ' Cerrar el formulario después de guardar
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error al guardar: " & ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub


End Class