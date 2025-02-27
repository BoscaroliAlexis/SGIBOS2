Imports MySql.Data.MySqlClient

Public Class Inventario

    Private conexion As MySqlConnection
    Private adaptador As MySqlDataAdapter
    Private tabla As DataTable
    Private idProducto As Integer

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

    Private Sub ActualizarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualizarToolStripMenuItem.Click

        dgvInventario.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        ' Verificar si hay una fila seleccionada
        If dgvInventario.SelectedRows.Count > 0 Then
            ' Obtener los datos del cliente seleccionado
            Dim idCliente As Integer = dgvInventario.SelectedRows(0).Cells("id_producto").Value
            Dim nombre As String = dgvInventario.SelectedRows(0).Cells("Producto").Value.ToString()
            Dim descripcion As String = dgvInventario.SelectedRows(0).Cells("descripcion").Value.ToString()
            Dim precio As String = dgvInventario.SelectedRows(0).Cells("precio").Value.ToString()
            Dim cantidadStock As String = dgvInventario.SelectedRows(0).Cells("cantidad_stock").Value.ToString()
            Dim categoria As String = dgvInventario.SelectedRows(0).Cells("Categoria").Value.ToString()
            Dim proveedor As String = dgvInventario.SelectedRows(0).Cells("Proveedor").Value.ToString()
            Dim activo As String = dgvInventario.SelectedRows(0).Cells("activo").Value.ToString()


            ' Crear instancia de NuevoCliente y pasar los datos
            Dim formulario As New NuevoProducto()
            formulario.idProducto = idProducto
            formulario.txtNombre.Text = nombre
            formulario.txtDescripcion.Text = descripcion
            formulario.txtPrecio.Text = precio.ToString()
            formulario.txtCantidadStock.Text = cantidadStock.ToString()
            formulario.cmbCategoria.SelectedValue = categoria
            formulario.cmbProveedor.SelectedValue = proveedor
            'formulario.cmbActivo.SelectedValue = If(activo, "Sí", "No")

            '
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
        If dgvInventario.SelectedRows.Count > 0 Then
            ' Obtener el ID del cliente seleccionado
            Dim idProducto As Integer = dgvInventario.SelectedRows(0).Cells("id_producto").Value

            ' Confirmar eliminación
            Dim confirmacion As DialogResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If confirmacion = DialogResult.Yes Then
                ' Cadena de conexión
                Dim cadenaConexion As String = "Server=localhost;Database=tiendadb;Uid=root;Pwd=mysql;"
                Dim conexion As New MySqlConnection(cadenaConexion)

                Try
                    conexion.Open()
                    Dim consulta As String = "DELETE FROM Productos WHERE id_producto = @id"
                    Dim comando As New MySqlCommand(consulta, conexion)
                    comando.Parameters.AddWithValue("@id", idProducto)
                    comando.ExecuteNonQuery()

                    MessageBox.Show("Producto eliminado correctamente.")
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

End Class


