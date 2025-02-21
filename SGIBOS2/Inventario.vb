Imports MySql.Data.MySqlClient

Public Class Inventario

    Private conexion As MySqlConnection
    Private adaptador As MySqlDataAdapter
    Private tabla As DataTable

    Private Sub btnAñadirInv_Click(sender As Object, e As EventArgs) Handles btnAñadirInv.Click
        NuevoProducto.StartPosition = FormStartPosition.CenterScreen
        NuevoProducto.Show()
    End Sub

    Private Sub Inventario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarDatos()
    End Sub

    ' Método para cargar datos en el DataGridView
    Private Sub CargarDatos(Optional ByVal busqueda As String = "")
        Try
            ' Cadena de conexión (ajusta según tu configuración)
            Dim cadenaConexion As String = "Server=localhost;Database=tiendadb;Uid=root;Pwd=mysql;"
            conexion = New MySqlConnection(cadenaConexion)
            conexion.Open()

            ' Consulta con JOIN para mostrar los nombres de categoría y proveedor
            Dim consulta As String = "SELECT p.id_producto, p.nombre AS Producto, p.descripcion, " &
                                     "p.precio, p.cantidad_stock, c.nombre AS Categoria, pr.nombre AS Proveedor, " &
                                     "p.fecha_creacion, p.activo " &
                                     "FROM productos p " &
                                     "LEFT JOIN categorias c ON p.id_categoria = c.id_categoria " &
                                     "LEFT JOIN proveedores pr ON p.id_proveedor = pr.id_proveedor"

            ' Si hay un término de búsqueda, agregar condición WHERE
            If busqueda <> "" Then
                consulta &= " WHERE p.nombre LIKE @busqueda"
            End If

            ' Crear y ejecutar comando
            adaptador = New MySqlDataAdapter(consulta, conexion)
            adaptador.SelectCommand.Parameters.AddWithValue("@busqueda", "%" & busqueda & "%")

            ' Llenar el DataGridView
            tabla = New DataTable()
            adaptador.Fill(tabla)
            dgvInventario.DataSource = tabla

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            ' Cerrar conexión
            If conexion IsNot Nothing Then conexion.Close()
        End Try
    End Sub

    ' Evento para buscar cuando se presiona el botón
    Private Sub btnBuscarInventario_Click(sender As Object, e As EventArgs) Handles btnBuscarInventario.Click
        CargarDatos(txtBuscarInventario.Text)
    End Sub

    Private Sub btnGesCat_Click(sender As Object, e As EventArgs) Handles btnGesCat.Click
        Categorias.StartPosition = FormStartPosition.CenterScreen
        Categorias.Show()
    End Sub

    Private Sub dgvInventario_MouseClick(sender As Object, e As MouseEventArgs) Handles dgvInventario.MouseClick
        ' Verificar si se hizo clic derecho
        If e.Button = MouseButtons.Right Then
            ' Obtener la fila seleccionada
            Dim hit As DataGridView.HitTestInfo = dgvInventario.HitTest(e.X, e.Y)

            ' Verificar si se hizo clic en una fila válida
            If hit.RowIndex >= 0 Then
                ' Seleccionar la fila
                dgvInventario.ClearSelection()
                dgvInventario.Rows(hit.RowIndex).Selected = True

                ' Mostrar el menú contextual
                cmsActEli2.Show(dgvInventario, e.Location)
            End If
        End If
    End Sub

End Class


