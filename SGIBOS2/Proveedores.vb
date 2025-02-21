Public Class Proveedores
    Private Sub btnGenerarReportePro_Click(sender As Object, e As EventArgs) Handles btnGenerarReportePro.Click
        For Each frm As Form In Application.OpenForms
            If TypeOf frm Is Proveedores Then
                Dim frmClientes As Proveedores = CType(frm, Proveedores)
                '  Dim frmReportes As New Reportes(frmClientes.ObtenerDatosClientes())
                '  frmReportes.ShowDialog()
                ' frmReportes.StartPosition = FormStartPosition.CenterScreen

                Exit For
            End If
        Next
    End Sub

    Private Sub btnAñadirPro_Click(sender As Object, e As EventArgs) Handles btnAñadirPro.Click
        NuevoProveedor.StartPosition = FormStartPosition.CenterScreen
        NuevoProveedor.Show()
    End Sub
End Class