Imports System.IO
Imports System.Web.Mvc
Imports compliancecg.Models
Imports DevExpress.Web
Imports DevExpress.Web.ASPxRichEdit.Internal
Imports DevExpress.Web.Mvc
Imports NLog


Namespace Controllers
    Public Class DevExpressController
        Inherits Controller
        Private Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        '' GET: DevExpress
        'Function Index() As ActionResult
        '    Return View()
        'End Function
        'Function LoadAndSave() As ActionResult
        '    ' Return DemoView("LoadAndSave")
        '    Return View("LoadAndSave")
        'End Function

        'Function LoadAndSavePartial() As ActionResult
        '    Return PartialView("LoadAndSavePartial")
        'End Function
        Public Function Index() As ActionResult
            Return View()
        End Function

        Public Function DocFileViewerCallback() As ActionResult
            Dim EFile As EFile = CType(Session("EFile"), EFile)
            Return PartialView("_DocumentViewPartial", EFile)
        End Function

        Public Function LoadDocument(FileName As String) As ActionResult
            Try

                ' Dim Control As RichEditControl

                Dim AllRichEditSettings As New Models.AllRichEditSettings
                Dim RichEditSettingOptions As New Models.RichEditSettingOptions
                Dim RichEditSettings As New Models.RichEditSettings

                'RichEditSettingOptions.BehaviorSettings.Copy = DevExpress.XtraRichEdit.DocumentCapability.Disabled
                'RichEditSettingOptions.BehaviorSettings.Printing = DevExpress.XtraRichEdit.DocumentCapability.Disabled
                RichEditSettings.RichEditRibbonMode = ASPxRichEdit.RichEditRibbonMode.None
                RichEditSettings.EditMode = False

                'Dim section As Section = RichEditControl.Document.Sections(0)

                Dim EFile As EFile = New EFile()
                EFile.Name = New Guid().ToString() & ".docx"
                EFile.FriendlyName = "Test.docx"
                EFile.Extension = "docx"
                '  file.Data = System.IO.File.ReadAllBytes(Server.MapPath("../" & file.FriendlyName))
                EFile.Data = System.IO.File.ReadAllBytes("c:\CCG\Temp\CCG00101CorporateCompliancePlan.docx")
                Session("EFile") = EFile
                AllRichEditSettings.EFile = EFile
                AllRichEditSettings.RichEditSettingOptions = RichEditSettingOptions.Current
                AllRichEditSettings.RichEditSettings = RichEditSettings


                Return PartialView("_DocumentViewPartial", AllRichEditSettings)
            Catch ex As Exception
                logger.Error(ex)

            End Try
            ' Dim RichEdit
        End Function


        Public Function Index2() As ActionResult
            Return View()
        End Function

        Public Function RichEditPartial() As ActionResult

            ViewBag.File = "CCG00101CorporateCompliancePlan.docx"

            Return PartialView("RichEditPartial")
        End Function
        'Public Function CustomDocumentManagementPartial() As ActionResult
        '    Return PartialView("CustomDocumentManagementPartial")
        'End Function

        Public Function OpenFile(ByVal filePath As String) As ActionResult
            'Return RichEditExtension.Open("RichEdit", Path.Combine("C:\CCG\temp\", "CCG00101CorporateCompliancePlan.docx"))
            Return RichEditExtension.Open("RichEdit", Path.Combine("C:\CCG\temp\", "CCG00101CorporateCompliancePlan.docx"))

            '    Return RichEditExtension.Open("RichEdit", Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, filePath))
        End Function


        Public Function UploadControlUpload() As ActionResult
            Dim errors As String()
            Dim files As UploadedFile() = UploadControlExtension.GetUploadedFiles("UploadControl", CustomUploadControlValidationSettings.Settings, errors, AddressOf UploadControl_FileUploadComplete, AddressOf UploadControl_FilesUploadComplete)

            Return Nothing
        End Function

        Public Sub UploadControl_FileUploadComplete(sender As Object, e As FileUploadCompleteEventArgs)

        End Sub

        Public Sub UploadControl_FilesUploadComplete(sender As Object, e As FilesUploadCompleteEventArgs)
            Dim UploadDirectory As String = "~/Content/UploadControl/UploadFolder/"

            Dim files As UploadedFile() = DirectCast(sender, MVCxUploadControl).UploadedFiles
            For i As Integer = 0 To files.Length - 1
                If files(i).IsValid AndAlso Not String.IsNullOrWhiteSpace(files(i).FileName) Then
                    Dim resultFilePath As String = "~/Content/" + files(i).FileName
                    files(i).SaveAs(System.Web.HttpContext.Current.Request.MapPath(resultFilePath))

                    Dim urlResolver As IUrlResolutionService = TryCast(sender, IUrlResolutionService)
                    If urlResolver IsNot Nothing Then
                        e.CallbackData += urlResolver.ResolveClientUrl(resultFilePath)
                    End If
                End If
            Next
        End Sub

        'Public Function _RichEditPartial(path As String) As ActionResult
        '    'ViewData("path") = path
        '    Return PartialView("_RichEditPartial")
        'End Function

        Public Function CallbackPanelPartial(path As String) As ActionResult
            'ViewData("path") = path
            Return PartialView("_CallbackPanelPartial")
        End Function

        Public Function UploadControlCallbackAction() As ActionResult
            UploadControlExtension.GetUploadedFiles("uc", UploadControlDemosHelper.ValidationSettings, AddressOf UploadControlDemosHelper.uc_FileUploadComplete)
            Return Nothing
        End Function

        Public Shared Sub HideFileTab(ByVal richedit As MVCxRichEdit)
            richedit.CreateDefaultRibbonTabs(True)
            Dim fileTab As RibbonTab = richedit.RibbonTabs(0)
            richedit.RibbonTabs.Remove(fileTab)
        End Sub

        'Public Shared Sub SetTextWaterMark(ByVal doc As SubDocument, ByVal sec As Section, ByVal text As String, ByVal richEditControl As RichEditControl)
        '    Dim cp As CharacterProperties
        '    Dim shape As Shape
        '    shape = doc.Shapes.InsertTextBox(doc.Range.Start)
        '    shape.TextBox.Document.AppendText(text)
        '    cp = shape.TextBox.Document.BeginUpdateCharacters(shape.TextBox.Document.Range)
        '    cp.FontName = "Times New Roman"
        '    cp.FontSize = 90
        '    cp.ForeColor = Color.Gray
        '    Dim measureFont As New Font(cp.FontName, cp.FontSize.Value)
        '    shape.TextBox.Document.EndUpdateCharacters(cp)
        '    shape.TextBox.Document.Paragraphs(0).Alignment = ParagraphAlignment.Center
        '    ' further code...
        'End Sub
    End Class



    Public Class UploadControlDemosHelper
        Public Const UploadDirectory As String = "~/Content/UploadControl/UploadFolder/"

        Public Shared ReadOnly ValidationSettings As New UploadControlValidationSettings() With {.AllowedFileExtensions = New String() {".jpg", ".jpeg", ".jpe", ".gif", ".bmp", ".docx"}, .MaxFileSize = 4194304}

        Public Shared Sub uc_FileUploadComplete(ByVal sender As Object, ByVal e As FileUploadCompleteEventArgs)
            If e.UploadedFile.IsValid Then
                Dim resultFilePath As String = HttpContext.Current.Request.MapPath(UploadDirectory & e.UploadedFile.FileName)
                'e.UploadedFile.SaveAs(resultFilePath, True)'Code Central Mode - Uncomment This Line
                Dim urlResolver As IUrlResolutionService = TryCast(sender, IUrlResolutionService)
                If urlResolver IsNot Nothing Then
                    e.CallbackData = urlResolver.ResolveClientUrl(resultFilePath)
                End If
            End If
        End Sub
    End Class


End Namespace


