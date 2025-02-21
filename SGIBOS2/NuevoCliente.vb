Imports MySql.Data.MySqlClient



Public Class NuevoCliente

    Public idCliente As Integer = 0

    Dim conexion As MySqlConnection
    Dim comando As MySqlCommand
    Private Sub btnAñadirCli_Click(sender As Object, e As EventArgs) Handles btnAñadirCli.Click
        ' Establecer conexión
        Dim cadenaConexion As String = "Server=localhost;Database=tiendadb;Uid=root;Pwd=mysql;"
        Dim conexion As New MySqlConnection(cadenaConexion)

        Try
            conexion.Open()
            Dim consulta As String

            If idCliente = 0 Then
                ' INSERT si no hay un cliente seleccionado
                consulta = "INSERT INTO Clientes (nombre, telefono, correo, direccion) VALUES (@nombre, @telefono, @correo, @direccion)"
            Else
                ' UPDATE si se está editando un cliente
                consulta = "UPDATE Clientes SET nombre=@nombre, telefono=@telefono, correo=@correo, direccion=@direccion WHERE id_cliente=@id_cliente"
            End If

            ' Crear y ejecutar comando
            Dim comando As New MySqlCommand(consulta, conexion)
            comando.Parameters.AddWithValue("@nombre", txtNombre.Text)
            comando.Parameters.AddWithValue("@telefono", txtTelefono.Text)
            comando.Parameters.AddWithValue("@correo", txtCorreo.Text)
            comando.Parameters.AddWithValue("@direccion", txtDireccion.Text)

            If idCliente > 0 Then
                comando.Parameters.AddWithValue("@id_cliente", idCliente)
            End If

            comando.ExecuteNonQuery()
            MessageBox.Show("Cliente guardado correctamente.")

            ' Cerrar el formulario después de guardar
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error al guardar: " & ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub


End Class