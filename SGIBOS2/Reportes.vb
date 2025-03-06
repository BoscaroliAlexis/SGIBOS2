Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports MySql.Data.MySqlClient

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


    Private Sub btnExportarCSV_Click(sender As Object, e As EventArgs) Handles btnExportarCSV.Click
        ' Configurar la conexión a MySQL
        Dim connectionString As String = "server=localhost;user=root;password=mysql;database=tiendadb;"
        Dim query As String = "SELECT * FROM Productos"

        ' Crear y configurar el SaveFileDialog
        Dim saveDialog As New SaveFileDialog()
    saveDialog.Filter = "Archivos CSV (*.csv)|*.csv"
    saveDialog.Title = "Guardar archivo CSV"
        saveDialog.FileName = "reporte_inventario.csv"

        ' Mostrar el cuadro de diálogo y verificar si el usuario seleccionó una ubicación
        If saveDialog.ShowDialog() = DialogResult.OK Then
        Dim savePath As String = saveDialog.FileName
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            Using writer As New StreamWriter(savePath, False, System.Text.Encoding.UTF8)
                                ' Escribir encabezados
                                Dim columnCount As Integer = reader.FieldCount
                                Dim header As String = ""
                                For i As Integer = 0 To columnCount - 1
                                    header &= reader.GetName(i) & If(i < columnCount - 1, ",", "")
                                Next
                                writer.WriteLine(header)

                                ' Escribir filas de datos
                                While reader.Read()
                                    Dim row As String = ""
                                    For i As Integer = 0 To columnCount - 1
                                        row &= reader(i).ToString().Replace(",", ";") & If(i < columnCount - 1, ",", "")
                                    Next
                                    writer.WriteLine(row)
                                End While
                            End Using
                            MessageBox.Show("Datos exportados correctamente a: " & savePath, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("No hay datos en la tabla Inventario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End If
End Sub
End Class

