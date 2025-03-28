﻿Imports MySql.Data.MySqlClient

Public Class NuevaCategoria

    Public idCategoria As Integer = 0

    Dim conexion As MySqlConnection
    Dim comando As MySqlCommand
    Private Sub btnGuardarCat_Click(sender As Object, e As EventArgs) Handles btnGuardarCat.Click

        Dim conexion As New MySqlConnection(cadenaConexion)

        Try
            conexion.Open()
            Dim consulta As String

            If idCategoria = 0 Then
                consulta = "INSERT INTO Categorias (nombre) VALUES (@nombre)"
            Else
                consulta = "UPDATE Categorias SET nombre=@nombre WHERE id_categoria=@id_categoria"
            End If

            ' Crear y ejecutar comando
            Dim comando As New MySqlCommand(consulta, conexion)
            comando.Parameters.AddWithValue("@nombre", txtNombreCat.Text)


            If idCategoria > 0 Then
                comando.Parameters.AddWithValue("@id_categoria", idCategoria)
            End If

            comando.ExecuteNonQuery()
            MessageBox.Show("Categoria guardada correctamente.")

            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error al guardar: " & ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub



    Private Sub NuevaCategoria_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class