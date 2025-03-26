Imports MySql.Data.MySqlClient



Public Class NuevoCliente

    Public idCliente As Integer = 0

    Dim conexion As MySqlConnection
    Dim comando As MySqlCommand
    Private Sub btnAñadirCli_Click(sender As Object, e As EventArgs) Handles btnAñadirCli.Click
        Dim conexion As New MySqlConnection(cadenaConexion)

        Try
            conexion.Open()
            Dim consulta As String

            If idCliente = 0 Then
                consulta = "INSERT INTO Clientes (nombre, telefono, correo, direccion) VALUES (@nombre, @telefono, @correo, @direccion)"
            Else
                consulta = "UPDATE Clientes SET nombre=@nombre, telefono=@telefono, correo=@correo, direccion=@direccion WHERE id_cliente=@id_cliente"
            End If

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

            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error al guardar: " & ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub NuevoCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class