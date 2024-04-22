Imports System.Drawing.Printing

Public Class frmReports

    Private printDocument As New PrintDocument()
    Private printPreviewDialog As New PrintPreviewDialog()

    Private Sub frmReports_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dtReports As DataTable = DisplayReport()
        dgReports.DataSource = dtReports
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PrintReport()
    End Sub
    Private Sub PrintDocument_PrintPage(ByVal sender As Object, ByVal e As Printing.PrintPageEventArgs)
        Dim printArea As RectangleF = e.MarginBounds

        Dim font As New Font("Arial", 10)
        Dim brush As New SolidBrush(Color.Black)

        Dim xPos As Single = printArea.Left
        Dim yPos As Single = printArea.Top

        Dim rowHeight As Single = font.GetHeight() + 5

        Dim dt As DataTable = DisplayReport()

        For Each column As DataColumn In dt.Columns
            e.Graphics.DrawString(column.ColumnName, font, brush, xPos, yPos)
            xPos += e.Graphics.MeasureString(column.ColumnName, font).Width + 10
        Next

        yPos += rowHeight

        For Each row As DataRow In dt.Rows
            xPos = printArea.Left
            For Each column As DataColumn In dt.Columns
                Dim cellValue As String = row(column).ToString()
                e.Graphics.DrawString(cellValue, font, brush, xPos, yPos)
                xPos += e.Graphics.MeasureString(cellValue, font).Width + 10
            Next
            yPos += rowHeight
        Next
        e.HasMorePages = False
    End Sub


    Private Sub PrintReport()
        AddHandler printDocument.PrintPage, AddressOf PrintDocument_PrintPage

        printPreviewDialog.Document = printDocument
        printPreviewDialog.ShowDialog()
    End Sub

End Class