﻿Public Class Proveedores
    Private Sub btnGenerarReportePro_Click(sender As Object, e As EventArgs) Handles btnGenerarReportePro.Click
        'Reportes.StartPosition = FormStartPosition.CenterScreen
        'Reportes.Show()
    End Sub

    Private Sub btnAñadirPro_Click(sender As Object, e As EventArgs) Handles btnAñadirPro.Click
        NuevoProveedor.StartPosition = FormStartPosition.CenterScreen
        NuevoProveedor.Show()
    End Sub
End Class