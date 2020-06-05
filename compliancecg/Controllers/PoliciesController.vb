Imports System.IO
Imports System.Web.Mvc
Imports Newtonsoft.Json
Imports Syncfusion.EJ2.Navigations
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Word
Imports CCGData
Imports DevExpress.Web.Mvc
Imports compliancecg.Models
Imports DevExpress.Web.Office
Imports DevExpress.Web
Imports System.Web
Imports System.Drawing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Drawing
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.DocumentFormat
Imports DocumentFormat.OpenXml.Wordprocessing
Imports DevExpress.XtraRichEdit.API.Native


Imports DocumentFormat.OpenXml.Packaging
Imports Syncfusion.DocIO.DLS

Imports Syncfusion.DocIO

Imports System.Reflection
Imports FormFieldType = Syncfusion.DocIO.DLS.FormFieldType
Imports System.Security.Claims
Imports CCGData.CCGData

Imports System.ComponentModel.DataAnnotations
Imports System.Drawing.Printing
Imports DevExpress.Web.ASPxRichEdit
Imports NLog
Imports Syncfusion.EJ2.PdfViewer

Namespace Controllers
    Public Class PoliciesController
        Inherits Controller
        Private DataRepository As New DataRepository
        Private Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
        ' GET: Policies
        Function Index() As ActionResult
            Return PartialView()
        End Function

        'Function RichEditPartial() As ActionResult
        '    Return PartialView()
        'End Function

        Public Function Details(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        Public Function Create() As ActionResult
            Return View()
        End Function

        <HttpPost>
        Public Function Create(ByVal collection As FormCollection) As ActionResult
            Try
                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        Public Function Edit(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        <HttpPost>
        Public Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        Public Function Delete(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        <HttpPost>
        Public Function Delete(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function


        Public Function [Default]() As ActionResult
            Return View()
        End Function
        Public Function PDFViewer() As ActionResult

            Dim Document As New Syncfusion.DocIO.DLS.WordDocument
            Dim MasterFile = WordFunctions.GetMasterDocument(Server.MapPath("/App_Data/Policies/"), "NJ")
            'Document.OpenReadOnly(MasterFile.FullName, FormatType.Docx)

            Session("MasterDocument") = Document
            ' Return PartialView("PDFViewer")
            Return View("PDFViewer")
        End Function


        '<System.Web.Mvc.HttpPost>
        'Public Function Load(ByVal jsonObject As jsonObjects) As ActionResult
        '    Try

        '        Dim pdfviewer As PdfRenderer = New PdfRenderer()
        '        Dim stream As MemoryStream = New MemoryStream()
        '        Dim jsonData = JsonConverter(jsonObject)
        '        Dim jsonResult As Object = New Object()

        '        If Session("MasterDocument") IsNot Nothing Then
        '            Dim MasterDocument = Session("MasterDocument")
        '            ' Dim bytes As Byte() = Convert.FromBase64String(jsonData("document"))
        '            stream = WordFunctions.CopyDocSection2(MasterDocument, "4", "Abigail House")
        '        End If




        '        '        Dim bytes As Byte() = Convert.FromBase64String(jsonData("document"))
        '        '        stream = New MemoryStream(bytes)

        '        'If jsonObject IsNot Nothing AndAlso jsonData.ContainsKey("document") Then

        '        '    If Boolean.Parse(jsonData("isFileName")) Then
        '        '        Dim documentPath As String = GetDocumentPath(jsonData("document"))

        '        '        If Not String.IsNullOrEmpty(documentPath) Then
        '        '            Dim bytes As Byte() = System.IO.File.ReadAllBytes(documentPath)
        '        '            stream = New MemoryStream(bytes)
        '        '        Else
        '        '            Return Me.Content(jsonData("document") & " is not found")
        '        '        End If
        '        '    Else
        '        '        Dim bytes As Byte() = Convert.FromBase64String(jsonData("document"))
        '        '        stream = New MemoryStream(bytes)
        '        '    End If
        '        'End If
        '        If stream.Length > 0 Then
        '            jsonResult = pdfviewer.Load(stream, jsonData)
        '            Return Content(JsonConvert.SerializeObject(jsonResult))
        '        End If


        '    Catch ex As Exception

        '    End Try
        'End Function

        <System.Web.Mvc.HttpPost>
        Public Function Load(ByVal jsonObject As jsonObjects) As ActionResult
            Dim pdfviewer As PdfRenderer = New PdfRenderer()
            Dim stream As MemoryStream = New MemoryStream()
            Dim jsonData = JsonConverter(jsonObject)
            Dim jsonResult As Object = New Object()

            If jsonObject IsNot Nothing AndAlso jsonData.ContainsKey("document") Then

                If Boolean.Parse(jsonData("isFileName")) Then
                    Dim documentPath As String = GetDocumentPath(jsonData("document"))

                    If Not String.IsNullOrEmpty(documentPath) Then
                        Dim bytes As Byte() = System.IO.File.ReadAllBytes(documentPath)
                        stream = New MemoryStream(bytes)
                    Else
                        Return Me.Content(jsonData("document") & " is not found")
                    End If
                Else
                    Dim bytes As Byte() = Convert.FromBase64String(jsonData("document"))
                    stream = New MemoryStream(bytes)
                End If
            End If

            jsonResult = pdfviewer.Load(stream, jsonData)
            Return Content(JsonConvert.SerializeObject(jsonResult))
        End Function

        Public Function JsonConverter(ByVal results As jsonObjects) As Dictionary(Of String, String)
            Dim resultObjects As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
            resultObjects = results.[GetType]().GetProperties(BindingFlags.Instance Or BindingFlags.[Public]).ToDictionary(Function(prop) prop.Name, Function(prop) prop.GetValue(results, Nothing))
            Dim emptyObjects = (From kv In resultObjects Where kv.Value IsNot Nothing Select kv).ToDictionary(Function(kv) kv.Key, Function(kv) kv.Value)
            Dim jsonResult As Dictionary(Of String, String) = emptyObjects.ToDictionary(Function(k) k.Key, Function(k) k.Value.ToString())
            Return jsonResult
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function RenderPdfPages(ByVal jsonObject As jsonObjects) As ActionResult
            Dim pdfviewer As PdfRenderer = New PdfRenderer()
            Dim jsonData = JsonConverter(jsonObject)
            Dim jsonResult As Object = pdfviewer.GetPage(jsonData)
            Return Content(JsonConvert.SerializeObject(jsonResult))
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function Unload(ByVal jsonObject As jsonObjects) As ActionResult
            Dim pdfviewer As PdfRenderer = New PdfRenderer()
            Dim jsonData = JsonConverter(jsonObject)
            pdfviewer.ClearCache(jsonData)
            Return Me.Content("Document cache is cleared")
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function RenderThumbnailImages(ByVal jsonObject As jsonObjects) As ActionResult
            Dim pdfviewer As PdfRenderer = New PdfRenderer()
            Dim jsonData = JsonConverter(jsonObject)
            Dim result As Object = pdfviewer.GetThumbnailImages(jsonData)
            Return Content(JsonConvert.SerializeObject(result))
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function Bookmarks(ByVal jsonObject As jsonObjects) As ActionResult
            Dim pdfviewer As PdfRenderer = New PdfRenderer()
            Dim jsonData = JsonConverter(jsonObject)
            Dim jsonResult As Object = pdfviewer.GetBookmarks(jsonData)
            Return Content(JsonConvert.SerializeObject(jsonResult))
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function Download(ByVal jsonObject As jsonObjects) As ActionResult
            Dim pdfviewer As PdfRenderer = New PdfRenderer()
            Dim jsonData = JsonConverter(jsonObject)
            Dim documentBase As String = pdfviewer.GetDocumentAsBase64(jsonData)
            Return Content(documentBase)
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function PrintImages(ByVal jsonObject As jsonObjects) As ActionResult
            Dim pdfviewer As PdfRenderer = New PdfRenderer()
            Dim jsonData = JsonConverter(jsonObject)
            Dim pageImage As Object = pdfviewer.GetPrintImage(jsonData)
            Return Content(JsonConvert.SerializeObject(pageImage))
        End Function

        Private Function GetDocumentPath(ByVal document As String) As String
            Dim documentPath As String = String.Empty

            If Not System.IO.File.Exists(document) Then
                Dim path = HttpContext.Request.PhysicalApplicationPath
                If System.IO.File.Exists(path & "App_Data\data\" & document) Then documentPath = path & "App_Data\data\" & document
            Else
                documentPath = document
            End If

            Return documentPath
        End Function

        Public Function [Get]() As IEnumerable(Of String)
            Return New String() {"value1", "value2"}
        End Function





        Public Function DocFileViewerCallback() As ActionResult
            'Dim AllRichEditSettings = Session("AllRichEditSettings")
            'Return PartialView("RichEditPartial", AllRichEditSettings)
            Return PartialView("RichEditPartial")
        End Function
        Public Shared Function GetCustomRibbonTab(ByVal isExtenernalRibbon As Boolean, FacilityGroup As FacilityGroup) As RibbonTab
            Try

                Dim ribbonTab As RibbonTab = New RibbonTab("Home")
                ribbonTab.Groups.AddRange(New RibbonGroup() {GetCommonGroup()})
                Return ribbonTab


                'If Session("FacilityGroup") IsNot Nothing Then
                '    FacilityGroup = Session("FacilityGroup")
                'End If



                'If FacilityGroup IsNot Nothing Then
                '    If FacilityGroup.AllowPolicyPrint Then
                '        Dim ribbonTab As RibbonTab = New RibbonTab("Home")
                '        ribbonTab.Groups.AddRange(New RibbonGroup() {GetCommonGroup()})
                '        Return ribbonTab
                '    End If
                'End If

                'Dim ribbonTab2 As RibbonTab
                'Return ribbonTab2
                'If isExtenernalRibbon Then
                '    ribbonTab.Groups.AddRange(New RibbonGroup() {GetCommonGroup(), GetFontGroup(isExtenernalRibbon), GetViewGroup()})
                'Else
                '    ribbonTab.Groups.AddRange(New RibbonGroup() {GetCommonGroup(), GetUndoGroup(), GetFontGroup(isExtenernalRibbon), GetPagesGroup(), GetViewGroup()})
                'End If



            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Private Shared Function GetCommonGroup() As RERFileCommonGroup
            Try
                'Dim FacilityGroup As New FacilityGroup
                'If Session("FacilityGroup") IsNot Nothing Then
                '    FacilityGroup = Session("FacilityGroup")
                'End If
                Dim commonGroup = New RERFileCommonGroup()
                'commonGroup.Items.AddRange(New RibbonItemBase() {New RERNewCommand(RibbonItemSize.Small) With {.Text = "New Document", .ToolTip = "Ctrl + N"}, New RERPrintCommand(RibbonItemSize.Small) With {.Text = "Print Document", .ToolTip = "Ctrl + P"}})
                commonGroup.Items.AddRange(New RibbonItemBase() {New RERPrintCommand(RibbonItemSize.Small) With {.Text = "Print Document", .ToolTip = "Ctrl + P"}})
                Return commonGroup

            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Public Function SetFacilityGroupSessionValues() As Boolean
            Try
                Dim jsonString As [String] = New StreamReader(Me.Request.InputStream).ReadToEnd()
                Dim FacilityGroup As FacilityGroup = JsonConvert.DeserializeObject(Of FacilityGroup)(jsonString)

                Session("FacilityGroup") = FacilityGroup
                Return True
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Public Function LoadDocument(FileName As String, State As String, SelectedFacilityName As String) As ActionResult
            Try
                'Dim FacilityGroup As New FacilityGroup
                'If Session("FacilityGroup") IsNot Nothing Then
                '    FacilityGroup = Session("FacilityGroup")
                'End If

                DocumentManager.CloseAllDocuments()

                Dim AllRichEditSettings As New Models.AllRichEditSettings
                AllRichEditSettings.RichEditSettingOptions.BehaviorSettings.Copy = DocumentCapability.Disabled
                AllRichEditSettings.RichEditSettings.RichEditRibbonMode = RichEditRibbonMode.None

                Dim FacilityGroup As FacilityGroup = Nothing
                If Session("FacilityGroup") IsNot Nothing Then
                    Dim FacilityGroupTmp As FacilityGroup = Session("FacilityGroup")
                    FacilityGroup = DataRepository.GetGroupFromID(FacilityGroupTmp.FacilityGroupID)
                    'FacilityGroup = Session("FacilityGroup")
                End If

                'If FacilityGroup.AllowPolicyPrint = True Then
                '    AllRichEditSettings.RibbonTabCollection.Add(GetCustomRibbonTab(True))
                '    AllRichEditSettings.RichEditSettingOptions.BehaviorSettings.Printing = DocumentCapability.Enabled
                'End If


                'AllRichEditSettings.RichEditSettingOptions.BehaviorSettings.Printing = DocumentCapability.Enabled
                'AllRichEditSettings.RibbonTabs.Assign(GetCustomRibbonTab(True))

                'Settings.RibbonTabs.Add(compliancecg.Controllers.PoliciesController.GetCustomRibbonTab(True))

                'Settings.RibbonTabs.Add(compliancecg.Controllers.PoliciesController.GetCustomRibbonTab(True))



                'Dim ribbonTab As RibbonTab = New RibbonTab("Home")
                'ribbonTab.Groups.AddRange(New RibbonGroup() {GetCommonGroup()})

                'Dim PrinterSettings As PrinterSettings = New PrinterSettings
                'PrinterSettings.FromPage = 2
                'PrinterSettings.ToPage = 3
                'PrinterSettings.Copies = 2
                'RichEditControl.Print(PrinterSettings)

                'Dim printOptions As PrintingOptions = richEditControl1.Options.Printing
                'printOptions.EnableCommentBackgroundOnPrint = True
                'printOptions.EnablePageBackgroundOnPrint = True
                'printOptions.PrintPreviewFormKind = PrintPreviewFormKind.Bars

                '

                ' AllRichEditSettings.RibbonTab.Groups.AddRange(New RibbonGroup() {GetCommonGroup()})
                AllRichEditSettings.RichEditSettings.EditMode = True
                AllRichEditSettings.RichEditSettings.ReadOnly = True

                Dim MasterFile As Boolean = True

                ' Dim ReadFile As FileInfo = New FileInfo(Server.MapPath("../App_Data/" & "CCG 00208 Screening New and Current Emp, Cont, Vendors, Phys, and other Healthcare Practitioners Pol.docx"))
                'Dim ReadFile As FileInfo = New FileInfo(Server.MapPath("../App_Data/" & "Test.docx"))
                'Dim ReadFile As FileInfo = New FileInfo(MasterFileName)

                Dim ReadFile As FileInfo = Nothing
                Dim EFile As EFile = New EFile()
                Dim MasterDocument As New WordDocument()







                If MasterFile = True Then
                    Dim WordFunctions As New WordFunctions
                    Dim SectionIndexes As New List(Of SectionIndex)


                    If Session("MasterDocument") IsNot Nothing Then
                        MasterDocument = Session("MasterDocument")
                    End If

                    'If Session("MasterDocument") IsNot Nothing Then
                    '    '  Dim MasterDocument2 As Word.Document = DirectCast(Session("MasterDocument"), Word.Document)
                    '    MasterDocument = Session("MasterDocument")

                    '    '   Dim Doc2 As Word.Document = Session("MasterDocument")
                    '    '  Session("MasterDocument2") = Doc2

                    '    '   Session("MasterDocument2") = MasterDocument
                    '    '   Dim WordDocuments As WordDocuments = Session("WordDocuments")
                    'End If
                    '
                    'If Session("MasterDocument") Is Nothing Then
                    '    Dim MasterFileName As String = Server.MapPath("../App_Data/" & State & "Masters.docx")
                    '    ReadFile = New FileInfo(MasterFileName)
                    '    MasterDocument = WordFunctions.OpenDocument(MasterFileName)
                    '    Session("MasterDocument") = MasterDocument
                    'End If



                    If MasterDocument IsNot Nothing Then
                        Dim FacilityName As String = String.Empty
                        Dim FacilityUser As New User

                        If Session("SectionIndexes") IsNot Nothing Then SectionIndexes = Session("SectionIndexes")
                        If Session("SectionIndexes") Is Nothing Then SectionIndexes = WordFunctions.GetSectionChapters(MasterDocument, State)
                        If Session("FacilityUser") Is Nothing Then FacilityUser = HomeController.GetCurrentUser()
                        If Session("FacilityUser") IsNot Nothing Then FacilityUser = Session("FacilityUser")



                        FacilityName = SelectedFacilityName

                        'If Session("FacilityName") IsNot Nothing Then FacilityName = Session("FacilityName")

                        'If Session("FacilityName") Is Nothing Then
                        '    'Dim Facilites As List(Of Facility) = DataRepository.GetFacilities(FacilityUser.EmailAddress)
                        '    'If Facilites IsNot Nothing Then Session("FacilityName") = Facilites.FirstOrDefault.Name

                        '    'Dim Facility As Facility = DataRepository.GetFacilities3(FacilityUser.EmailAddress)
                        '    'If Facility IsNot Nothing Then Session("FacilityName") = Facility.Name
                        '    'FacilityName = Session("FacilityName")

                        '    FacilityName = SelectedFacilityName


                        'End If

                        If SectionIndexes IsNot Nothing Then
                            Dim DocIndex As String = WordFunctions.GetDocIndexFromPath(FileName)
                            Dim SectionIndex = SectionIndexes.Where(Function(s) s.Title = DocIndex).FirstOrDefault
                            If SectionIndex IsNot Nothing Then
                                EFile.Data = WordFunctions.CopyDocSection(MasterDocument, SectionIndex, FacilityName)
                                EFile.FriendlyName = SectionIndex.Title
                            End If
                        End If
                    End If
                End If


                If MasterFile = False Then
                    ReadFile = New FileInfo(FileName)
                    If ReadFile IsNot Nothing Then
                        EFile.Data = System.IO.File.ReadAllBytes(ReadFile.FullName)
                        'EFile.Data = System.IO.File.ReadAllBytes(Server.MapPath("../App_Data/" & "Test2.docx"))
                        EFile.FriendlyName = ReadFile.Name
                    End If
                End If

                If EFile.Data IsNot Nothing Then
                    EFile.Name = New Guid().ToString() & ".docx"
                    EFile.Extension = "docx"
                    EFile.FilePath = FileName
                    Session("EFile") = EFile
                    Session("AllRichEditSettings") = AllRichEditSettings
                    AllRichEditSettings.EFile = EFile
                End If

                If EFile.Data Is Nothing Then
                    'EFile = Session("EFile")
                    'If EFile IsNot Nothing Then
                    'End If
                    'Dim Docs As List(Of IDocumentInfo) = DocumentManager.GetAllDocuments().ToList
                    'DocumentManager.CloseDocument(Docs.LastOrDefault.DocumentId)
                    DocumentManager.CloseAllDocuments()
                    AllRichEditSettings.EFile = Nothing
                End If

                Dim Docs As List(Of IDocumentInfo) = DocumentManager.GetAllDocuments().ToList
                If FacilityGroup IsNot Nothing Then
                    If FacilityGroup?.AllowPolicyPrint IsNot Nothing Then AllRichEditSettings.AllowPrint = FacilityGroup?.AllowPolicyPrint
                End If
                Return PartialView("RichEditPartial", AllRichEditSettings)
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function


        Public Function DocumentSettings() As ActionResult

            '   Dim richEditControl1 As DevExpress.XtraRichEdit.RichEditControl


            '        settings.Items.AddGroupItem(m >= m.DocumentCapabilitiesSettings, g >= {
            '    g.Caption = "Document Capabilities";
            '    g.Items.Add(m >= m.DocumentCapabilitiesSettings.CharacterFormatting);
            '    g.Items.Add(m >= m.DocumentCapabilitiesSettings.CharacterStyle);
            '    g.Items.Add(m >= m.DocumentCapabilitiesSettings.Fields);
            '    g.Items.Add(m >= m.DocumentCapabilitiesSettings.Hyperlinks);
            '    g.Items.Add(m >= m.DocumentCapabilitiesSettings.InlinePictures);
            '    g.Items.Add(m >= m.DocumentCapabilitiesSettings.Numbering.Bulleted);
            '    g.Items.Add(m >= m.DocumentCapabilitiesSettings.Numbering.MultiLevel);
            '    g.Items.Add(m >= m.DocumentCapabilitiesSettings.Numbering.Simple);
            '    g.Items.Add(m >= m.DocumentCapabilitiesSettings.ParagraphFormatting);
            '    g.Items.Add(m >= m.DocumentCapabilitiesSettings.Paragraphs);
            '    g.Items.Add(m >= m.DocumentCapabilitiesSettings.ParagraphStyle);
            '    g.Items.Add(m >= m.DocumentCapabilitiesSettings.ParagraphTabs);
            '    g.Items.Add(m >= m.DocumentCapabilitiesSettings.Sections);
            '    g.Items.Add(m >= m.DocumentCapabilitiesSettings.TabSymbol);
            '    g.Items.Add(m >= m.DocumentCapabilitiesSettings.Undo);
            '});
            'settings.Items.AddGroupItem(m >= m.BehaviorSettings, g >= {
            '    g.Caption = "Behavior";
            '    g.Items.Add(m >= m.BehaviorSettings.Copy);
            '    g.Items.Add(m >= m.BehaviorSettings.CreateNew);
            '    g.Items.Add(m >= m.BehaviorSettings.Cut);
            '    g.Items.Add(m >= m.BehaviorSettings.Drag);
            '    g.Items.Add(m >= m.BehaviorSettings.Drop);
            '    g.Items.Add(m >= m.BehaviorSettings.FullScreen);
            '    g.Items.Add(m >= m.BehaviorSettings.Open);
            '    g.Items.Add(m >= m.BehaviorSettings.PageBreakInsertMode);
            '    g.Items.Add(m >= m.BehaviorSettings.Paste);
            '    g.Items.Add(m >= m.BehaviorSettings.Printing);
            '    g.Items.Add(m >= m.BehaviorSettings.Save);
            '    g.Items.Add(m >= m.BehaviorSettings.SaveAs);
            '    g.Items.Add(m >= m.BehaviorSettings.TabMarker);
            '});

            '            DemoRichEdit.commands.insertText.execute("Hello! This is new RichEdit API")
            '            DemoRichEdit.selection.selectAll()
            '            DemoRichEdit.commands.delete.execute()
            '            DemoRichEdit.commands.insertText.execute("Now you can do more.")
            '            DemoRichEdit.commands.insertParagraph.execute()
            '            DemoRichEdit.commands.insertText.execute("You can format text")
            '            DemoRichEdit.selection.goToPrevWord()
            '            DemoRichEdit.selection.goToPrevWord(True)
            '            DemoRichEdit.commands.changeFontFormatting.execute({bold:  true, foreColor: "#789" })
            'DemoRichEdit.selection.goToLineEnd()
            '            DemoRichEdit.commands.insertParagraph.execute()
            '            DemoRichEdit.commands.insertText.execute("and whole paragraphs")
            '            DemoRichEdit.commands.toggleParagraphAlignmentCenter.execute(True)
            '            DemoRichEdit.commands.insertParagraph.execute()
            '            DemoRichEdit.commands.toggleParagraphAlignmentCenter.execute(False)
            '            DemoRichEdit.commands.insertHeader.execute()
            '            DemoRichEdit.commands.insertText.execute("Insert headers...")
            '            DemoRichEdit.commands.closeHeaderFooter.execute()
            '            DemoRichEdit.selection.goToDocumentEnd()
            '            DemoRichEdit.commands.insertParagraph.execute()
            '            DemoRichEdit.commands.toggleBulletedList.execute()
            '            DemoRichEdit.commands.insertText.execute("and numbering lists ")
            '            DemoRichEdit.commands.insertParagraph.execute()
            '            DemoRichEdit.commands.toggleBulletedList.execute()
            '            DemoRichEdit.commands.insertTable.execute(2, 3)
            '            DemoRichEdit.commands.insertText.execute("and tables ")
            '            DemoRichEdit.selection.goToDocumentEnd()
            '            DemoRichEdit.commands.insertText.execute("and fields: ")
            '            DemoRichEdit.commands.createDateField.execute()
            '            DemoRichEdit.selection.selectAll()
            '            DemoRichEdit.commands.delete.execute()
            '            DemoRichEdit.commands.insertText.execute("Become a UI Superhero")
            '            DemoRichEdit.commands.insertParagraph.execute()
            ' DemoRichEdit.commands.insertPicture.execute(getImagePath())



            'Sub SetTextWatermark(ps As PrintingSystem)
            '    ' Create the text watermark. 
            '    Dim textWatermark As New Watermark()

            '    ' Set watermark options. 
            '    textWatermark.Text = "CUSTOM WATERMARK TEXT"
            '    textWatermark.TextDirection = DirectionMode.ForwardDiagonal
            '    textWatermark.Font = New Font(textWatermark.Font.FontFamily, 40)
            '    textWatermark.ForeColor = Color.DodgerBlue
            '    textWatermark.TextTransparency = 150
            '    textWatermark.ShowBehind = False
            '    textWatermark.PageRange = "1,3-5"

            '    ' Add the watermark to a document. 
            '    ps.Watermark.CopyFrom(textWatermark)
            'End Sub

            'Public Sub SetPictureWatermark(ps As PrintingSystem)
            '    ' Create the picture watermark. 
            '    Dim pictureWatermark As New Watermark()

            '    ' Set watermark options. 
            '    pictureWatermark.Image = Bitmap.FromFile("watermark.gif")
            '    pictureWatermark.ImageAlign = ContentAlignment.TopCenter
            '    pictureWatermark.ImageTiling = False
            '    pictureWatermark.ImageViewMode = ImageViewMode.Stretch
            '    pictureWatermark.ImageTransparency = 150
            '    pictureWatermark.ShowBehind = True
            '    pictureWatermark.PageRange = "2,4"

            '    ' Add the watermark to a document. 
            '    ps.Watermark.CopyFrom(pictureWatermark)
            'End Sub

        End Function


        Public Function RichEditPartial() As ActionResult
            'Dim model = New RichEditData() With {.DocumentId = Guid.NewGuid().ToString(), .DocumentFormat = DocumentFormat.Doc, .Document = DataHelper.GetDocument()}
            ' Return PartialView(Model)
            '  ViewBag.File = "CCG00105ResolutionDesignatingaComplianceOfficer.docx"
            '  Return RichEditExtension.Open("RichEdit", "C:\CCG\temp\" + ViewBag.File)
            'Return RichEditExtension.Open("RichEdit", Path.Combine("C:\CCG\temp\", "CCG00101CorporateCompliancePlan.docx"))
            Return PartialView("RichEditPartial")
        End Function
        'Public Function CustomDocumentManagementPartial() As ActionResult
        '    Return PartialView("CustomDocumentManagementPartial")
        'End Function

        Public Function OpenFile(ByVal filePath As String) As ActionResult
            Try
                'Dim fields = RichEdit.document.activeSubDocument.fieldsInfo
                ' Dim RichEdit As RichEdit.Forms.

                Dim EFile As EFile = Session("file")
                If EFile IsNot Nothing Then
                    DocumentManager.CloseDocument(EFile.FriendlyName)
                End If
                'If RichEditExtension.GetDocumentId("RichEdit") <> "" Then
                '    DocumentManager.CloseDocument(RichEditExtension.GetDocumentId("RichEdit"))
                'End If


                'Return RichEditExtension.Open("RichEdit", Path.Combine("C:\CCG\temp\", "CCG00101CorporateCompliancePlan.docx"))
                ' Return RichEditExtension.Open("RichEdit", Path.Combine("C:\CCG\temp\", "CCG00101CorporateCompliancePlan.docx"))

                '    Return RichEditExtension.Open("RichEdit", Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, filePath))

            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Function TreeView5() As ActionResult
            Return PartialView()
        End Function

        Function TreeView6() As ActionResult
            Try

                'Dim SectionIndexes As New List(Of SectionIndex)
                'Dim WordFunctions As New WordFunctions
                'Dim Document As New Syncfusion.DocIO.DLS.WordDocument

                'If Session("MasterDocument") Is Nothing Then
                '    Dim MasterFile = WordFunctions.GetMasterDocument(Server.MapPath("/App_Data/Policies/"), "GA")
                '    Document.OpenReadOnly(MasterFile.FullName, FormatType.Docx)
                '    Session("MasterDocument") = Document
                'End If

                'If Session("SectionIndexes") Is Nothing Then
                '    SectionIndexes = WordFunctions.GetSectionTitle(Document)
                '    Session("SectionIndexes") = SectionIndexes
                'End If


                'Dim iconFields As TreeViewFieldsSettings = New TreeViewFieldsSettings()
                'Dim treeviewIcon As TreeviewImageIcons = New TreeviewImageIcons()


                'If Session("SectionIndexes") IsNot Nothing Then
                '    SectionIndexes = Session("SectionIndexes")
                '    iconFields.DataSource = treeviewIcon.getTreeviewImageIconsModel2(SectionIndexes)
                'End If

                'iconFields.Id = "NodeId"
                'iconFields.Text = "NodeText"
                'iconFields.IconCss = "Icon"
                'iconFields.Child = "NodeChild"
                'iconFields.Expanded = "Expanded"
                'ViewBag.iconFields = iconFields

                'Dim ImageIcons As New List(Of ImageIcons)

                'For Each Dir As TreeviewImageIcons In iconFields.DataSource
                '    ImageIcons.AddRange(Dir.NodeChild)
                'Next

                'ViewBag.iconFieldsjson = JsonConvert.SerializeObject(ImageIcons)

                'Return PartialView()

            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Function TreeView7() As ActionResult
            Try

                Return PartialView()
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function
        Function TreeView() As ActionResult
            Try

                Dim User As User = HomeController.GetCurrentUser()
                Dim StaffMember As StaffMember = HomeController.GetCurrentStaffMember()

                Dim UserInfo As New List(Of GetUserInfo_Result)
                Dim Facilites As New List(Of Facility)
                Dim FacilityStates As New List(Of FacilityState)

                If User IsNot Nothing Then
                    UserInfo = DataRepository.GetUserInfo(User.EmailAddress)

                    Dim IsGroupAdmin As Boolean = False
                    Dim IsFacilityAdmin As Boolean = False
                    If UserInfo.Count > 0 Then
                        Dim UserInfo2 As GetUserInfo_Result = UserInfo.FirstOrDefault
                        IsGroupAdmin = DataRepository.IsUserGroupOwner(UserInfo2.EmailAddress, UserInfo2.FacilityGroupID)
                        IsFacilityAdmin = DataRepository.IsUserFacilityAdmin(UserInfo2.EmailAddress, UserInfo2.FacilityID)

                        If IsGroupAdmin Then
                            Facilites = DataRepository.GetFacilitesByGroup(UserInfo2.FacilityGroupID)
                        Else
                            Facilites = DataRepository.GetFacilityFromUser(User.EmailAddress, True)
                        End If
                    End If





                    '  Dim FacilityGroups As New List(Of FacilityGroup)
                    '  Dim FacilityGroupIDs As New List(Of Integer)
                    '  If Facilites IsNot Nothing Then
                    '      For Each Facility As Facility In Facilites
                    '          Dim FoundIndex As Integer = FacilityGroupIDs.IndexOf(Facility.FacilityGroupID)
                    '          If FoundIndex = -1 Then FacilityGroupIDs.Add(Facility.FacilityGroupID)
                    '
                    '          If Facility.State IsNot Nothing Then
                    '              If FacilityStates.Where(Function(s) s.State = Facility.State).Any = False Then
                    '                  FacilityStates.Add(New FacilityState With {.State = Facility.State, .Icon = ""})
                    '              End If
                    '          End If
                    '      Next
                    '
                    '      For Each FacilityState As FacilityState In FacilityStates
                    '          Dim FacilitiesInState = Facilites.Where(Function(f) f.State = FacilityState.State).ToList
                    '          For Each Facility As Facility In FacilitiesInState
                    '              If Facility.Name IsNot Nothing Then
                    '                  FacilityState.FacilityNameNodes.Add(New FacilityNameNode With {.Name = Facility.Name, .Icon = ""})
                    '              End If
                    '          Next
                    '      Next
                    '  End If

                    'For Each FacilityGroupID As Integer In FacilityGroupIDs
                    'FacilityGroups.Add(DataRepository.GetFacilityGroup(FacilityGroupID))
                    'Next
                    'If FacilityGroups.Count = 1 Then Session("FacilityGroup") = FacilityGroups.FirstOrDefault

                    'ViewBag.DataSource = FacilityGroups
                    ViewBag.data = Facilites
                    ViewBag.dataJson = JsonConvert.SerializeObject(Facilites)
                End If


                'Dim treeviewIcon As TreeViewImageIcons = New TreeViewImageIcons()
                'Dim iconFields As TreeViewFieldsSettings = New TreeViewFieldsSettings()
                'iconFields.DataSource = treeviewIcon.GetTreeViewNodesForStates(FacilityStates)


                '   iconFields.Id = "NodeId"
                '   iconFields.Text = "NodeText"
                '   iconFields.IconCss = "Icon"
                '   iconFields.Child = "NodeChild"
                '   iconFields.Expanded = "Expanded"
                '   ViewBag.iconFields2 = iconFields

                'Dim ImageIcons As New List(Of ImageIcons)
                'For Each Dir As TreeViewImageIcons In iconFields.DataSource
                'ImageIcons.AddRange(Dir.NodeChild)
                'Next
                'ViewBag.iconFieldsjson2 = JsonConvert.SerializeObject(ImageIcons)


                If Session("FacilityGroup") IsNot Nothing Then
                    Dim FacilityGroup As FacilityGroup = Nothing
                    FacilityGroup = Session("FacilityGroup")
                    ViewBag.FacilityGroup = FacilityGroup
                End If




                Return PartialView()
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Function TreeViewPolicies(State As String, SelectedFacilityName As String) As String
            Try
                Dim SectionIndexes As New List(Of SectionIndex)
                Dim WordFunctions As New WordFunctions
                Dim Document As New Syncfusion.DocIO.DLS.WordDocument

                Dim iconFields As TreeViewFieldsSettings = New TreeViewFieldsSettings()
                Dim treeviewIcon As TreeViewImageIcons = New TreeViewImageIcons()

                Session("SectionIndexes") = Nothing

                If Session("State") <> State Then
                    Dim MasterFile = WordFunctions.GetMasterDocument(Server.MapPath("/App_Data/Policies/"), State)
                    'Document.OpenReadOnly(MasterFile.FullName, FormatType.Docx)
                    Document = MasterFile
                    SectionIndexes = WordFunctions.GetSectionTitle(Document, State)
                    If Document IsNot Nothing Then Session("MasterDocument") = Document
                    If SectionIndexes IsNot Nothing Then Session("SectionIndexes") = SectionIndexes
                End If

                If Session("State") = State Then
                    If Session("MasterDocument") IsNot Nothing Then Document = Session("MasterDocument")
                    If Session("SectionIndexes") IsNot Nothing Then SectionIndexes = Session("SectionIndexes")
                End If
                Session("State") = State

                If Session("MasterDocument") Is Nothing Then
                    Dim MasterFile = WordFunctions.GetMasterDocument(Server.MapPath("/App_Data/Policies/"), State)
                    Document = MasterFile
                    ' Document.OpenReadOnly(MasterFile.FullName, FormatType.Docx)
                    Session("MasterDocument") = Document
                End If

                If Session("SectionIndexes") Is Nothing Then
                    SectionIndexes = WordFunctions.GetSectionTitle(Document, State)
                    Session("SectionIndexes") = SectionIndexes
                End If

                If SectionIndexes IsNot Nothing Then
                    iconFields.DataSource = treeviewIcon.GetTreeViewNodesForPolicies(SectionIndexes)
                End If


                iconFields.Id = "NodeId"
                iconFields.Text = "NodeText"
                iconFields.IconCss = "Icon"
                iconFields.Child = "NodeChild"
                iconFields.Expanded = "Expanded"
                'ViewBag.iconFields = iconFields

                Dim ImageIcons As New List(Of ImageIcons)
                For Each Dir As TreeViewImageIcons In iconFields.DataSource
                    ImageIcons.AddRange(Dir.NodeChild)
                Next
                ViewBag.iconFieldsjson = JsonConvert.SerializeObject(iconFields.DataSource)

                Return ViewBag.iconFieldsjson
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function
        'Function TreeView1b(State As String, SelectedFacilityName As String) As String
        '    Try
        '        Dim SectionIndexes As New List(Of SectionIndex)
        '        Dim WordFunctions As New WordFunctions
        '        Dim Document As New Syncfusion.DocIO.DLS.WordDocument

        '        Dim iconFields As TreeViewFieldsSettings = New TreeViewFieldsSettings()
        '        Dim treeviewIcon As TreeviewImageIcons = New TreeviewImageIcons()
        '        Dim treeviewIconHead As TreeviewImageIconsHead = New TreeviewImageIconsHead()


        '        If Session("MasterDocument") Is Nothing Then
        '            Dim MasterFile = WordFunctions.GetMasterDocument(Server.MapPath("/App_Data/Policies/"), State)
        '            Document.OpenReadOnly(MasterFile.FullName, FormatType.Docx)
        '            Session("MasterDocument") = Document
        '        End If

        '        If Session("SectionIndexes") Is Nothing Then
        '            SectionIndexes = WordFunctions.GetSectionTitle(Document)
        '            Session("SectionIndexes") = SectionIndexes
        '        End If

        '        If Session("SectionIndexes") IsNot Nothing Then
        '            SectionIndexes = Session("SectionIndexes")
        '            iconFields.DataSource = treeviewIcon.GetTreeViewNodesForPolicies(SectionIndexes)
        '        End If

        '        iconFields.Id = "NodeId"
        '        iconFields.Text = "NodeText"
        '        iconFields.IconCss = "Icon"
        '        iconFields.Child = "NodeChild"
        '        iconFields.Expanded = "Expanded"
        '        ViewBag.iconFields = iconFields

        '        Dim ImageIcons As New List(Of ImageIcons)
        '        For Each Dir As TreeviewImageIcons In iconFields.DataSource
        '            ImageIcons.AddRange(Dir.NodeChild)
        '        Next
        '        ViewBag.iconFieldsjson = JsonConvert.SerializeObject(ImageIcons)

        '        'Return iconFields
        '        Return JsonConvert.SerializeObject(iconFields)

        '        ''Dim TreeviewImageIcons As New List(Of TreeviewImageIcons)
        '        ''For Each State As TreeviewImageIconsHead In iconFields.DataSource

        '        ''    Dim ImageIcons As New List(Of ImageIcons)
        '        ''    For Each Dir As TreeviewImageIcons In TreeviewImageIcons
        '        ''        ImageIcons.AddRange(Dir.NodeChild)
        '        ''    Next

        '        ''    TreeviewImageIcons.AddRange(State.TreeviewNode)
        '        ''Next





        '        ' ViewBag.iconFieldsjson = JsonConvert.SerializeObject(TreeviewImageIcons)

        '        'Return PartialView("TreeView")

        '  Catch ex As Exception
        'logger.Error(ex)

        '    End Try
        'End Function

        Public Function OpenFile() As Boolean
            Try
                Dim jsonString As [String] = New StreamReader(Me.Request.InputStream).ReadToEnd()
                'Dim Facility As Facility = JsonConvert.DeserializeObject(Of Facility)(jsonString)

                Dim WordFunctions As New WordFunctions
                WordFunctions.ConvertWordFile(jsonString)
                ' WordFunctions.ConvertWordFile("C:\CCG\Policies\1 Compliance Program General Policies\CCG 00103 Adoption, Amendment, and Modification of the Compliance Program.docx")



                Return True
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function


    End Class
    Public Class TreeviewImageIconsHead
        Public Property NodeText As String
        Public Property Icon As String
        Public Property NodeId As String
        Public Property Expanded As Boolean
        Public TreeviewNode As List(Of TreeViewImageIcons)
    End Class

    Public Class TreeViewImageIcons
        Public Property NodeText As String
        Public Property Icon As String
        Public Property NodeId As String
        Public Property Expanded As Boolean
        Public NodeChild As List(Of ImageIcons)
        Private Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        'Public Function getTreeviewImageIconsModel2(SectionIndexes As List(Of SectionIndex)) As List(Of TreeviewImageIcons)
        '    Try
        '        Dim path As String
        '        Select Case My.Computer.Name
        '            Case "SC-BENYOMIN"
        '                path = "C:\Projects\Policies"
        '            Case "NIASOFF-DESKTOP"
        '                path = "C:\CCG\Policies"

        '            Case ""
        '                path = "C:\CCG\Policies"
        '            Case Else
        '                path = "C:\Projects\Policies"
        '        End Select



        '        Dim TreeviewImageIcons As List(Of TreeviewImageIcons) = New List(Of TreeviewImageIcons)()


        '        Dim DirectorySearchPattern As String = "*.*"
        '        Dim DirectoryInfo As DirectoryInfo = New DirectoryInfo(path)
        '        Dim Directories As DirectoryInfo() = DirectoryInfo.GetDirectories(DirectorySearchPattern, SearchOption.TopDirectoryOnly)

        '        Dim ImageIcons2 As New List(Of ImageIcons)

        '        Dim FolderCounter As Integer = 0
        '        For Each Directory As DirectoryInfo In Directories
        '            Dim Root As Integer = -1
        '            Dim Parsed = Integer.TryParse(Directory.Name.Substring(0, 1), Root)

        '            ' Dim SectionIndexs2 = SectionIndexes.Where(Function(s) s.Root = Root)
        '            If Parsed = True And SectionIndexes.Where(Function(s) s.Root = Root).Any Then

        '                FolderCounter += 1
        '                Dim ImageIcons As New List(Of ImageIcons)
        '                Dim FileSearchPattern As String = "*.*"

        '                TreeviewImageIcons.Add(New TreeviewImageIcons With {.NodeId = FolderCounter.ToString, .NodeText = Directory.Name, .Icon = "folder", .NodeChild = ImageIcons})

        '                Dim FileCounter As Integer = 0
        '                For Each Index As SectionIndex In SectionIndexes.Where(Function(s) s.Root = Root)
        '                    FileCounter += 1
        '                    Dim ID As String = $"{FolderCounter.ToString}-{FileCounter.ToString}"
        '                    ImageIcons.Add(New ImageIcons With {.NodeId = ID, .NodeText = Index.Header, .FullPath = Index.Header, .Icon = "docx"})
        '                Next

        '                'Dim FileCounter As Integer = 0
        '                'Dim Files As FileInfo() = Directory.GetFiles(FileSearchPattern, SearchOption.TopDirectoryOnly)
        '                'For Each File As FileInfo In Files
        '                '    Dim Title = WordFunctions.GetCCGIndex(File.Name)

        '                '    If Title IsNot Nothing Then
        '                '        If SectionIndexes.Where(Function(s) s.Title = Title).Any Then
        '                '            FileCounter += 1
        '                '            Dim ID As String = $"{FolderCounter.ToString}-{FileCounter.ToString}"
        '                '            ImageIcons.Add(New ImageIcons With {.NodeId = ID, .NodeText = File.Name, .FullPath = File.FullName, .Icon = File.Extension.ToString.TrimStart(".")})
        '                '        End If
        '                '    End If
        '                'Next
        '            End If
        '        Next

        '        Return TreeviewImageIcons
        '  Catch ex As Exception
        'logger.Error(ex)

        '    End Try

        'End Function

        Public Function GetTreeViewNodesForStates(FacilityStates As List(Of FacilityState)) As List(Of TreeViewImageIcons)
            Try
                Dim TreeviewImageIcons As List(Of TreeViewImageIcons) = New List(Of TreeViewImageIcons)()


                Dim SteateCounter As Integer = 0
                For Each State As FacilityState In FacilityStates
                    SteateCounter += 1
                    Dim ImageIcons As New List(Of ImageIcons)
                    Dim FileSearchPattern As String = "*.*"
                    Dim Expanded As Boolean = True
                    'If State.State = "IL" Then
                    '    Expanded = False
                    'End If



                    TreeviewImageIcons.Add(New TreeViewImageIcons With {.NodeId = SteateCounter.ToString, .NodeText = State.State, .Icon = "folder", .NodeChild = ImageIcons, .Expanded = Expanded})

                    Dim FacilityNameCounter As Integer = 0
                    For Each FacilityName As FacilityNameNode In State.FacilityNameNodes
                        FacilityNameCounter += 1

                        ImageIcons.Add(New ImageIcons With {.NodeId = FacilityNameCounter.ToString, .NodeText = FacilityName.Name, .FullPath = State.State, .Icon = "", .Expanded = Expanded})
                    Next
                Next

                Return TreeviewImageIcons
            Catch ex As Exception
                logger.Error(ex)

            End Try

        End Function

        Public Function GetTreeViewNodesForPolicies(SectionIndexes As List(Of SectionIndex)) As List(Of TreeViewImageIcons)
            Try

                Dim DirectoryHeaders As New List(Of String)
                DirectoryHeaders.Add("1 Compliance Program General Policies")
                DirectoryHeaders.Add("2 Fraud Waste and Abuse")
                DirectoryHeaders.Add("3 Employment")
                DirectoryHeaders.Add("4 Privacy & Data Security")
                DirectoryHeaders.Add("5 Quality of Care")


                Dim TreeviewImageIcons As List(Of TreeViewImageIcons) = New List(Of TreeViewImageIcons)()
                Dim ImageIcons2 As New List(Of ImageIcons)
                Dim isFirstExpanded As Boolean
                Dim FolderCounter As Integer = 0
                For Each Header As String In DirectoryHeaders

                    Dim Root As Integer = -1
                    Dim Parsed = Integer.TryParse(Header.Substring(0, 1), Root)

                    ' Dim SectionIndexs2 = SectionIndexes.Where(Function(s) s.Root = Root)
                    If Parsed = True And SectionIndexes.Where(Function(s) s.Root = Root).Any Then

                        FolderCounter += 1
                        Dim ImageIcons As New List(Of ImageIcons)
                        Dim FileSearchPattern As String = "*.*"
                        isFirstExpanded = IIf(FolderCounter = 1, True, False)

                        TreeviewImageIcons.Add(New TreeViewImageIcons With {.NodeId = FolderCounter.ToString, .NodeText = Header, .Icon = "folder", .NodeChild = ImageIcons, .Expanded = isFirstExpanded})


                        Dim FileCounter As Integer = 0
                        For Each Index As SectionIndex In SectionIndexes.Where(Function(s) s.Root = Root)
                            FileCounter += 1
                            Dim ID As String = $"{FolderCounter.ToString}-{FileCounter.ToString}"
                            ImageIcons.Add(New ImageIcons With {.NodeId = ID, .NodeText = Index.Header, .FullPath = Index.Header, .Icon = "docx"})
                        Next

                        'Dim FileCounter As Integer = 0
                        'Dim Files As FileInfo() = Directory.GetFiles(FileSearchPattern, SearchOption.TopDirectoryOnly)
                        'For Each File As FileInfo In Files
                        '    Dim Title = WordFunctions.GetCCGIndex(File.Name)

                        '    If Title IsNot Nothing Then
                        '        If SectionIndexes.Where(Function(s) s.Title = Title).Any Then
                        '            FileCounter += 1
                        '            Dim ID As String = $"{FolderCounter.ToString}-{FileCounter.ToString}"
                        '            ImageIcons.Add(New ImageIcons With {.NodeId = ID, .NodeText = File.Name, .FullPath = File.FullName, .Icon = File.Extension.ToString.TrimStart(".")})
                        '        End If
                        '    End If
                        'Next
                    End If
                Next

                Return TreeviewImageIcons
            Catch ex As Exception
                logger.Error(ex)

            End Try

        End Function




        'Public Function getTreeviewImageIconsModel2(SectionIndexes As List(Of SectionIndex), StateHeaders As List(Of String)) As List(Of TreeviewImageIconsHead)
        '    Try

        '        'Dim StateHeaders As New List(Of String)
        '        'StateBag.Add("1 Compliance Program General Policies")

        '        Dim DirectoryHeaders As New List(Of String)
        '        DirectoryHeaders.Add("1 Compliance Program General Policies")
        '        DirectoryHeaders.Add("2 Fraud Waste and Abuse")
        '        DirectoryHeaders.Add("3 Employment")
        '        DirectoryHeaders.Add("4 Privacy & Data Security")
        '        DirectoryHeaders.Add("5 Quality of Care")



        '        Dim TreeviewImageIconsHeads As New List(Of TreeviewImageIconsHead)


        '        Dim StateCounter As Integer = 0
        '        For Each State As String In StateHeaders
        '            StateCounter += 1
        '            Dim TreeviewImageIcons As List(Of TreeviewImageIcons) = New List(Of TreeviewImageIcons)()


        '            Dim FolderCounter As Integer = 0
        '            For Each Header As String In DirectoryHeaders

        '                Dim Root As Integer = -1
        '                Dim Parsed = Integer.TryParse(Header.Substring(0, 1), Root)

        '                ' Dim SectionIndexs2 = SectionIndexes.Where(Function(s) s.Root = Root)
        '                If Parsed = True And SectionIndexes.Where(Function(s) s.Root = Root).Any Then
        '                    Dim ImageIcons2 As New List(Of ImageIcons)
        '                    FolderCounter += 1

        '                    Dim FileSearchPattern As String = "*.*"


        '                    Dim ImageIcons As New List(Of ImageIcons)



        '                    TreeviewImageIcons.Add(New TreeviewImageIcons With {.NodeId = FolderCounter.ToString, .NodeText = Header, .Icon = "folder", .NodeChild = ImageIcons2})

        '                    Dim FileCounter As Integer = 0
        '                    For Each Index As SectionIndex In SectionIndexes.Where(Function(s) s.Root = Root)
        '                        FileCounter += 1
        '                        Dim ID As String = $"{FolderCounter.ToString}-{FileCounter.ToString}"
        '                        ImageIcons2.Add(New ImageIcons With {.NodeId = ID, .NodeText = Index.Header, .FullPath = Index.Header, .Icon = "docx"})
        '                    Next



        '                    'Dim FileCounter As Integer = 0
        '                    'Dim Files As FileInfo() = Directory.GetFiles(FileSearchPattern, SearchOption.TopDirectoryOnly)
        '                    'For Each File As FileInfo In Files
        '                    '    Dim Title = WordFunctions.GetCCGIndex(File.Name)

        '                    '    If Title IsNot Nothing Then
        '                    '        If SectionIndexes.Where(Function(s) s.Title = Title).Any Then
        '                    '            FileCounter += 1
        '                    '            Dim ID As String = $"{FolderCounter.ToString}-{FileCounter.ToString}"
        '                    '            ImageIcons.Add(New ImageIcons With {.NodeId = ID, .NodeText = File.Name, .FullPath = File.FullName, .Icon = File.Extension.ToString.TrimStart(".")})
        '                    '        End If
        '                    '    End If
        '                    'Next
        '                End If
        '            Next

        '            TreeviewImageIconsHeads.Add(New TreeviewImageIconsHead With {.NodeId = StateCounter.ToString, .NodeText = State, .TreeviewNode = TreeviewImageIcons})

        '        Next
        '        Return TreeviewImageIconsHeads
        '  Catch ex As Exception
        'logger.Error(ex)

        '    End Try

        'End Function


    End Class


    Public Class ImageIcons
        Public Property NodeText As String
        Public Property Icon As String
        Public Property NodeId As String
        Public Property FullPath As String
        Public Property Expanded As Boolean
    End Class

    Public Class FacilityState
        Public Property State As String
        Public Property Icon As String
        Public FacilityNameNodes As New List(Of FacilityNameNode)
    End Class

    Public Class FacilityNameNode
        Public Property Name As String
        Public Property Icon As String
        Public Property Expanded As Boolean


    End Class
End Namespace

Public Class DexExpDocumentPermission

    Public Function OpenFile(FileName As String)
        ' Dim fileName As String = "Test.docx"
        Using srv As New RichEditDocumentServer()
            srv.LoadDocument(FileName, OpenXml)
            'Dim doc As API.Native.Document = srv.Document



            'doc.AppendText("This document is generated by Word Processing Document API")
            'Dim cp As API.Native.CharacterProperties = doc.BeginUpdateCharacters(doc.Paragraphs(0).Range)
            'cp.ForeColor = System.Drawing.Color.FromArgb(&H83, &H92, &H96)
            'cp.Italic = True
            'doc.EndUpdateCharacters(cp)
            'Dim pp As API.Native.ParagraphProperties = doc.BeginUpdateParagraphs(doc.Paragraphs(0).Range)
            'pp.Alignment = API.Native.ParagraphAlignment.Right
            'doc.EndUpdateParagraphs(pp)


            ' Dim rangePermissions = doc.BeginUpdateRangePermissions()
            'ProtectDoc(doc)

            'SetPermissions(Doc)

            srv.SaveDocument(FileName, OpenXml)
        End Using

    End Function


    'Private Function ProtectDoc(Document As API.Native.Document)
    '    'DevExpress.XtraRichEdit

    '    ' Protect document range
    '    Dim rangePermissions As RangePermissionCollection = Document.BeginUpdateRangePermissions()
    '    Dim rp As RangePermission = rangePermissions.CreateRangePermission(Document.Paragraphs(5).Range)
    '    rp.Group = "Administrators"
    '    rp.UserName = "admin@somecompany.com"
    '    rangePermissions.Add(rp)

    '    Document.EndUpdateRangePermissions(rangePermissions)
    '    ' Enforce protection and set password.
    '    Document.Protect("123")

    'End Function
    'Private Sub SetPermissions(Document As API.Native.Document)

    '    Dim rangePermissions = Document.BeginUpdateRangePermissions()
    '    rangePermissions.Clear()
    '    Dim lastNonProtectedPosition = Document.Range.Start
    '    Dim containsProtectedRanges As Boolean = False

    '    For Each bookmark In Document.Bookmarks
    '        containsProtectedRanges = True

    '        If bookmark.Range.Start.ToInt() > lastNonProtectedPosition.ToInt() Then
    '            Dim rangeAfterProtection = Document.CreateRange(lastNonProtectedPosition, bookmark.Range.Start.ToInt() - lastNonProtectedPosition.ToInt() - 1)
    '            rangePermissions.AddRange(CreateRangePermissions(rangeAfterProtection, "UserDOF", "UserDOF"))
    '        End If

    '        lastNonProtectedPosition = bookmark.Range.[End]
    '    Next

    '    If Document.Range.[End].ToInt() > lastNonProtectedPosition.ToInt() Then
    '        Dim rangeAfterProtection = Document.CreateRange(lastNonProtectedPosition, Document.Range.[End].ToInt() - lastNonProtectedPosition.ToInt() - 1)
    '        rangePermissions.AddRange(CreateRangePermissions(rangeAfterProtection, "UserDOF", "UserDOF"))
    '    End If

    '    Document.EndUpdateRangePermissions(rangePermissions)

    '    If containsProtectedRanges = True Then
    '        Document.Protect("123")
    '    End If
    'End Sub

    Private Function CreateRangePermissions(ByVal range As DocumentRange, ByVal userGroup As String, ParamArray usernames As String()) As List(Of RangePermission)
        Dim ranges = New List(Of RangePermission)()

        For Each username As String In usernames
            Dim rangePermission = New RangePermission(range)
            rangePermission.Group = userGroup
            rangePermission.UserName = username
            ranges.Add(rangePermission)
        Next

        Return ranges
    End Function
End Class

Public Class DocumentProtectionDemoOptions
    Public Sub New()
        User = "lawyer@somecompany.com"
        Color = Color.FromArgb(255, 254, 213)
        BracketsColor = Color.FromArgb(164, 160, 0)
        Visibility = True
    End Sub

    Const DocumentProtectionDemoOptionsKey As String = "DocumentProtectionDemoOptions"

    Public Shared Property Current As DocumentProtectionDemoOptions
        Get
            If HttpContext.Current.Session(DocumentProtectionDemoOptionsKey) Is Nothing Then HttpContext.Current.Session(DocumentProtectionDemoOptionsKey) = New DocumentProtectionDemoOptions()
            Return CType(HttpContext.Current.Session(DocumentProtectionDemoOptionsKey), DocumentProtectionDemoOptions)
        End Get
        Set(ByVal value As DocumentProtectionDemoOptions)
            HttpContext.Current.Session(DocumentProtectionDemoOptionsKey) = value
        End Set
    End Property

    <Display(Name:="User")>
    Public Property User As String
    <Display(Name:="Color")>
    Public Property Color As System.Drawing.Color
    <Display(Name:="Brackets Color")>
    Public Property BracketsColor As System.Drawing.Color
    <Display(Name:="Visibility")>
    Public Property Visibility As Boolean
End Class
Public Class jsonObjects
    Public Property document As String
    Public Property password As String
    Public Property zoomFactor As String
    Public Property isFileName As String
    Public Property xCoordinate As String
    Public Property yCoordinate As String
    Public Property pageNumber As String
    Public Property documentId As String
    Public Property hashId As String
    Public Property sizeX As String
    Public Property sizeY As String
    Public Property startPage As String
    Public Property endPage As String
End Class