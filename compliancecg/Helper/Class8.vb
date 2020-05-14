'Imports System
'Imports System.Web.Mvc
'Imports Syncfusion.Mvc.Pdf
'Imports Syncfusion.Pdf
'Imports Syncfusion.Pdf.Graphics
'Imports Syncfusion.Pdf.Interactive
'Imports Syncfusion.Pdf.Security
'Imports System.Drawing

'Namespace EJ2MVCSampleBrowser.Controllers.PDF
'    Partial Public Class PdfController
'        Inherits Controller

'        Public Function Encryption() As ActionResult
'            ViewBag.data = New String() {"Encrypt all contents", "Encrypt all contents except metadata", "Encrypt only attachments [For AES only]"}
'            Return View()
'        End Function

'        <HttpPost>
'        Public Function Encryption(ByVal InsideBrowser As String, ByVal encryptionType As String, ByVal encryptOptionType As String) As ActionResult
'            Dim document As PdfDocument = New PdfDocument()
'            Dim page As PdfPage = document.Pages.Add()
'            Dim graphics As PdfGraphics = page.Graphics
'            Dim font As PdfStandardFont = New PdfStandardFont(PdfFontFamily.TimesRoman, 14.0F, PdfFontStyle.Bold)
'            Dim brush As PdfBrush = PdfBrushes.Black
'            Dim form As PdfForm = document.Form
'            Dim security As PdfSecurity = document.Security

'            If encryptionType = "40_RC4" Then
'                security.KeySize = PdfEncryptionKeySize.Key40Bit
'            ElseIf encryptionType = "128_RC4" Then
'                security.KeySize = PdfEncryptionKeySize.Key128Bit
'                security.Algorithm = PdfEncryptionAlgorithm.RC4
'            ElseIf encryptionType = "128_AES" Then
'                security.KeySize = PdfEncryptionKeySize.Key128Bit
'                security.Algorithm = PdfEncryptionAlgorithm.AES
'            ElseIf encryptionType = "256_AES" Then
'                security.KeySize = PdfEncryptionKeySize.Key256Bit
'                security.Algorithm = PdfEncryptionAlgorithm.AES
'            ElseIf encryptionType = "256_AES_Revision_6" Then
'                security.KeySize = PdfEncryptionKeySize.Key256BitRevision6
'                security.Algorithm = PdfEncryptionAlgorithm.AES
'            End If

'            If encryptOptionType = "Encrypt only attachments [For AES only]" Then
'                Dim attachment As PdfAttachment = New PdfAttachment(ResolveApplicationDataPath("Products.xml"))
'                attachment.ModificationDate = DateTime.Now
'                attachment.Description = "About Syncfusion"
'                attachment.MimeType = "application/txt"
'                document.Attachments.Add(attachment)
'                security.EncryptionOptions = PdfEncryptionOptions.EncryptOnlyAttachments
'            ElseIf encryptOptionType = "Encrypt all contents except metadata" Then
'                security.EncryptionOptions = PdfEncryptionOptions.EncryptAllContentsExceptMetadata
'            Else
'                security.EncryptionOptions = PdfEncryptionOptions.EncryptAllContents
'            End If

'            security.OwnerPassword = "syncfusion"
'            security.Permissions = PdfPermissionsFlags.Print Or PdfPermissionsFlags.FullQualityPrint
'            security.UserPassword = "password"
'            Dim text As String = "Security options:" & vbLf & vbLf & String.Format("KeySize: {0}" & vbLf & vbLf & "Encryption Algorithm: {4}" & vbLf & vbLf & "Owner Password: {1}" & vbLf & vbLf & "Permissions: {2}" & vbLf & vbLf & "UserPassword: {3}", security.KeySize, security.OwnerPassword, security.Permissions, security.UserPassword, security.Algorithm)

'            If encryptionType = "256_AES_Revision_6" Then
'                text += String.Format(vbLf & vbLf & "Revision: {0}", "Revision 6")
'            ElseIf encryptionType = "256_AES" Then
'                text += String.Format(vbLf & vbLf & "Revision: {0}", "Revision 5")
'            End If

'            graphics.DrawString("Document is Encrypted with following settings", font, brush, PointF.Empty)
'            font = New PdfStandardFont(PdfFontFamily.TimesRoman, 11.0F, PdfFontStyle.Bold)
'            graphics.DrawString(text, font, brush, New PointF(0, 40))

'            If InsideBrowser = "Browser" Then
'                Return document.ExportAsActionResult("sample.pdf", HttpContext.ApplicationInstance.Response, HttpReadType.Open)
'            Else
'                Return document.ExportAsActionResult("sample.pdf", HttpContext.ApplicationInstance.Response, HttpReadType.Save)
'            End If
'        End Function
'    End Class
'End Namespace
