Imports MySql.Data.MySqlClient


Public Class Inventario

    Private conexion As MySqlConnection
    Private adaptador As MySqlDataAdapter
    Private tabla As DataTable

    'Private Sub btnGenerarReporteInv_Click(sender As Object, e As EventArgs) Handles btnGenerarReporteInv.Click
    '  Reportes.StartPosition = FormStartPosition.CenterScreen
    '   Reportes.Show()
    ' End Sub

    Private Sub btnAñadirInv_Click(sender As Object, e As EventArgs) Handles btnAñadirInv.Click
        NuevoProducto.StartPosition = FormStartPosition.CenterScreen
        NuevoProducto.Show()
    End Sub

    Private Sub Inventario_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    ' Método para cargar datos en el DataGridView
    Private Sub CargarDatos(Optional ByVal busqueda As String = "")
        Try
            ' Cadena de conexión (ajusta según tu configuración)
            Dim cadenaConexion As String = "Server=localhost;Database=tiendadb;Uid=root;Pwd=mysql;"
            conexion = New MySqlConnection(cadenaConexion)
            conexion.Open()

            ' Si hay texto en el campo de búsqueda, se ajusta la consulta
            Dim consulta As String
            If busqueda = "" Then
                consulta = "SELECT * FROM productos" ' Muestra todos los registros
            Else
                consulta = "SELECT * FROM productos WHERE nombre LIKE @busqueda"
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

    ' Evento Load para cargar datos al iniciar el formulario
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarDatos()
    End Sub

    ' Evento para buscar cuando se presiona el botón
    Private Sub btnBuscarInventario_Click(sender As Object, e As EventArgs) Handles btnBuscarInventario.Click
        CargarDatos(txtBuscarInventario.Text)
    End Sub
End Class




