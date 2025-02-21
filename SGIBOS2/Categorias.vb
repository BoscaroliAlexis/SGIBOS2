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
            Dim cadenaConexion As String = "Server=localhost;Database=tiendadb;Uid=root;Pwd=mysql;"
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


End Class