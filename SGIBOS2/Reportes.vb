Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class Reportes

    Private dtClientes As DataTable = New DataTable()
    Private dtProveedores As DataTable = New DataTable()


    Private Sub Reportes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbTipo.Items.AddRange(New String() {"Inventario", "Ventas", "Clientes", "Proveedores"})
        cmbOrden.Items.AddRange(New String() {"Mayor a menor", "Menor a mayor"})
        cmbPeriodo.Items.AddRange(New String() {"Ultimo mes", "Ultimos 3 meses", "Ultimos 6 meses", "Ultimo año"})

        cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList
        cmbOrden.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPeriodo.DropDownStyle = ComboBoxStyle.DropDownList

        cmbTipo.SelectedIndex = 0
        cmbOrden.SelectedIndex = 0
        cmbPeriodo.SelectedIndex = 0
    End Sub

    Public Sub New(ByVal datosClientes As DataTable, Optional ByVal datosProveedores As DataTable = Nothing)
        InitializeComponent()
        dtClientes = datosClientes
        dtProveedores = datosProveedores
    End Sub

    Private Sub btnExportarPDF_Click(sender As Object, e As EventArgs) Handles btnExportarPDF.Click
        Dim dataTable As DataTable = Nothing
        Dim tituloReporte As String = ""
        Dim nombreArchivo As String = ""

        ' Determinar qué tipo de reporte se debe generar
        If cmbTipo.SelectedItem.ToString() = "Clientes" Then
            dataTable = dtClientes
            tituloReporte = "Reporte de Clientes"
            nombreArchivo = "Reporte_Clientes.pdf"
        ElseIf cmbTipo.SelectedItem.ToString() = "Proveedores" Then
            dataTable = dtProveedores
            tituloReporte = "Reporte de Proveedores"
            nombreArchivo = "Reporte_Proveedores.pdf"
        Else
            MessageBox.Show("Seleccione un tipo de reporte válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Verificar si hay datos en el DataTable
        If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then
            MessageBox.Show("No hay datos para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Crear el cuadro de diálogo para guardar el PDF
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "Archivo PDF|*.pdf"
        saveFileDialog.Title = "Guardar Reporte PDF"
        saveFileDialog.FileName = nombreArchivo

        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                Dim doc As New Document(PageSize.A4)
                Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(saveFileDialog.FileName, FileMode.Create))
                doc.Open()

                ' Agregar título
                Dim titulo As New Paragraph(tituloReporte & vbCrLf & vbCrLf, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK))
                titulo.Alignment = Element.ALIGN_CENTER
                doc.Add(titulo)

                ' Crear tabla con el número de columnas
                Dim pdfTable As New PdfPTable(dataTable.Columns.Count)
                pdfTable.WidthPercentage = 100

                ' Agregar encabezados
                For Each column As DataColumn In dataTable.Columns
                    Dim cell As New PdfPCell(New Phrase(column.ColumnName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE)))
                    cell.BackgroundColor = BaseColor.DARK_GRAY
                    pdfTable.AddCell(cell)
                Next

                ' Agregar filas del DataTable
                For Each row As DataRow In dataTable.Rows
                    For Each item In row.ItemArray
                        pdfTable.AddCell(New Phrase(item.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK)))
                    Next
                Next

                ' Agregar la tabla al documento
                doc.Add(pdfTable)
                doc.Close()

                MessageBox.Show("PDF generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show("Error al generar el PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

End Class
