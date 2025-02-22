Imports MySql.Data.MySqlClient

Public Class NuevoProveedor

    Public idProveedor As Integer = 0

    Dim conexion As MySqlConnection
    Dim comando As MySqlCommand
    Private Sub btnAñadirProv_Click(sender As Object, e As EventArgs) Handles btnAñadirProv.Click
        ' Establecer conexión
        Dim cadenaConexion As String = "Server=localhost;Database=tiendadb;Uid=root;Pwd=mysql;"
        Dim conexion As New MySqlConnection(cadenaConexion)

        Try
            conexion.Open()
            Dim consulta As String

            If idProveedor = 0 Then
                ' INSERT si no hay un cliente seleccionado
                consulta = "INSERT INTO Proveedores (nombre,contacto, telefono, correo, direccion) VALUES (@nombre, @contacto ,  @telefono, @correo, @direccion)"
            Else
                ' UPDATE si se está editando un cliente
                consulta = "UPDATE Proveedores SET nombre=@nombre, contacto=@contacto ,  telefono=@telefono, correo=@correo, direccion=@direccion WHERE id_proveedor=@id_proveedor"
            End If

            ' Crear y ejecutar comando
            Dim comando As New MySqlCommand(consulta, conexion)
            comando.Parameters.AddWithValue("@nombre", txtNombre.Text)
            comando.Parameters.AddWithValue("@contacto", txtContacto.Text)
            comando.Parameters.AddWithValue("@telefono", txtTelefono.Text)
            comando.Parameters.AddWithValue("@correo", txtCorreo.Text)
            comando.Parameters.AddWithValue("@direccion", txtDireccion.Text)

            If idProveedor > 0 Then
                comando.Parameters.AddWithValue("@id_proveedor", idProveedor)
            End If

            comando.ExecuteNonQuery()
            MessageBox.Show("Proveedor guardado correctamente.")

            ' Cerrar el formulario después de guardar
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error al guardar: " & ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub
End Class