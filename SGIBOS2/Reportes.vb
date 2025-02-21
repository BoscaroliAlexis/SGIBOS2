Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class Reportes

    Private dtClientes As DataTable

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

    Public Sub New(ByVal datos As DataTable)
        InitializeComponent()
        dtClientes = datos
    End Sub

    Private Sub btnExportarPDF_Click(sender As Object, e As EventArgs) Handles btnExportarPDF.Click
        ' Verificar si se ha seleccionado "Clientes" en el ComboBox
        If cmbTipo.SelectedItem Is Nothing OrElse cmbTipo.SelectedItem.ToString() <> "Clientes" Then
            MessageBox.Show("Por favor, selecciona 'Clientes' antes de exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Verificar si hay datos en el DataTable
        If dtClientes.Rows.Count = 0 Then
            MessageBox.Show("No hay datos para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Crear el cuadro de diálogo para guardar el PDF
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "Archivo PDF|*.pdf"
        saveFileDialog.Title = "Guardar Reporte PDF"
        saveFileDialog.FileName = "Reporte_Clientes.pdf"

        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                Dim doc As New Document(PageSize.A4)
                Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(saveFileDialog.FileName, FileMode.Create))
                doc.Open()

                ' Agregar título
                Dim titulo As New Paragraph("Reporte de Clientes" & vbCrLf & vbCrLf, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK))
                titulo.Alignment = Element.ALIGN_CENTER
                doc.Add(titulo)

                ' Crear tabla con el número de columnas
                Dim pdfTable As New PdfPTable(dtClientes.Columns.Count)
                pdfTable.WidthPercentage = 100

                ' Agregar encabezados
                For Each column As DataColumn In dtClientes.Columns
                    Dim cell As New PdfPCell(New Phrase(column.ColumnName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE)))
                    cell.BackgroundColor = BaseColor.DARK_GRAY
                    pdfTable.AddCell(cell)
                Next

                ' Agregar filas del DataTable
                For Each row As DataRow In dtClientes.Rows
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