Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Mvc.Ajax
Imports Syncfusion.DocIO.DLS
Imports Syncfusion.DocToPDFConverter
Imports Syncfusion.Pdf
Imports Syncfusion.MVC.pdf
Imports Syncfusion.OfficeChartToImageConverter

Namespace EJ2MVCSampleBrowser.Controllers.DocIO
    Partial Public Class DocIOController
        Inherits Controller

        Public Function DOCtoPDF(ByVal button As String, ByVal renderingMode As String, ByVal renderingMode1 As String, ByVal renderingMode2 As String, ByVal renderingMode3 As String, ByVal renderingMode4 As String, ByVal renderingMode5 As String, ByVal renderingMode6 As String, ByVal autoTag As String, ByVal preserveFormFields As String, ByVal exportBookmarks As String, ByVal embeddingFont As String, ByVal file As HttpPostedFileBase) As ActionResult
            'If button Is Nothing Then Return View()

            'If file IsNot Nothing Then
            '    Dim extension = Path.GetExtension(file.FileName).ToLower()

            '    If extension = ".doc" OrElse extension = ".docx" OrElse extension = ".dot" OrElse extension = ".dotx" OrElse extension = ".dotm" OrElse extension = ".docm" OrElse extension = ".xml" OrElse extension = ".rtf" Then
            '        Dim document As WordDocument = New WordDocument(file.InputStream)
            '        'document.ChartToImageConverter = New ChartToImageConverter()
            '        'document.ChartToImageConverter.ScalingMode = Syncfusion.OfficeChart.ScalingMode.Normal
            '        Dim converter As DocToPDFConverter = New DocToPDFConverter()
            '        If renderingMode = "DirectPDF" Then converter.Settings.EnableFastRendering = True
            '        If renderingMode1 = "PreserveStructureTags" Then converter.Settings.AutoTag = True
            '        If renderingMode2 = "PreserveFormFields" Then converter.Settings.PreserveFormFields = True
            '        converter.Settings.ExportBookmarks = If(renderingMode3 = "PreserveWordHeadingsToPDFBookmarks", Syncfusion.DocIO.ExportBookmarkType.Headings, Syncfusion.DocIO.ExportBookmarkType.Bookmarks)
            '        If renderingMode4 = "EnablesCompleteFont" Then converter.Settings.EmbedCompleteFonts = True
            '        If renderingMode5 = "EnablesSubsetFont" Then converter.Settings.EmbedFonts = True
            '        If renderingMode6 = "ShowRevisions" Then document.RevisionOptions.ShowMarkup = RevisionType.Deletions Or RevisionType.Formatting Or RevisionType.Insertions
            '        Dim pdfDoc As PdfDocument = converter.ConvertToPDF(document)

            '        Try
            '            ' Return pdfDoc.ExportAsActionResult("sample.pdf", HttpContext.ApplicationInstance.Response, HttpReadType.Save)
            '        Catch __unusedException1__ As Exception
            '        Finally
            '        End Try
            '    Else
            '        ViewBag.Message = String.Format("Please choose Word format document to convert to PDF")
            '    End If
            'Else
            '    ViewBag.Message = String.Format("Browse a Word document and then click the button to convert as a PDF document")
            'End If

            Return View()
        End Function



        'Private Sub protectPDF()
        '    Dim document As New PdfDocument()

        '    Dim page As PdfPage = document.Pages.Add()

        '    Dim graphics As PdfGraphics = page.Graphics

        '    Dim font As New PdfStandardFont(PdfFontFamily.TimesRoman, 20.0F, PdfFontStyle.Bold)

        '    Dim brush As PdfBrush = PdfBrushes.Black

        '    'Document security.

        '    Dim security As PdfSecurity = document.Security

        '    'Specifies key size and encryption algorithm using 128 bit key in RC4 mode.

        '    security.KeySize = PdfEncryptionKeySize.Key128Bit

        '    security.Algorithm = PdfEncryptionAlgorithm.RC4

        '    security.OwnerPassword = "syncfusion"

        '    'It allows printing and accessibility copy content

        '    security.Permissions = PdfPermissionsFlags.Print Or PdfPermissionsFlags.AccessibilityCopyContent

        '    security.UserPassword = "password"

        '    graphics.DrawString("This document is protected with owner password", font, brush, New PointF(0, 40))

        '    'Save and close the document.

        '    document.Save("Output.pdf")

        '    document.Close(True)
        'End Sub

    End Class

End Namespace
