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
Imports DocumentFormat.OpenXml.Wordprocessing
Imports DocumentFormat.OpenXml.Packaging
Imports Syncfusion.DocIO.DLS

Imports Syncfusion.DocIO

Imports System.Reflection
Imports FormFieldType = Syncfusion.DocIO.DLS.FormFieldType
Imports System.Security.Claims
Imports CCGData.CCGData

Namespace Controllers
    Public Class PoliciesController2
        Inherits Controller
        Private DataRepository As New DataRepository
        ' GET: Policies
        Function Index() As ActionResult


            Return PartialView()
        End Function

        Private Sub SurroundingSub()
            Dim document As RichEditDocumentServer = New RichEditDocumentServer()
            'document.LoadDocument(Stream, DocumentFormat.OpenXml)
            ' Dim Range As API.Native.DocumentRange
            'document.Document.Selection = document.Document.CreateBookmark(, "")



            'Dim bytesOfDocument = RichEditExtension.SaveCopy("editorName", DocumentFormat.OpenXml)
            'Dim stream = New MemoryStream(bytes)
            'Dim document As RichEditDocumentServer = New RichEditDocumentServer()
            'document.LoadDocument(stream, DocumentFormat.OpenXml)
            'document.BeginUpdate()
            'Dim name As String = "nameOfBookmark"
            'document.Document.Selection = document.Document.Bookmarks.First(Function(x) x.Name.Equals(name)).Range
            'document.EndUpdate()
            'bytes = document.OpenXmlBytes
        End Sub

        Public Function DocFileViewerCallback() As ActionResult
            Dim AllRichEditSettings = Session("AllRichEditSettings")
            Return PartialView("RichEditPartial", AllRichEditSettings)
        End Function

        Public Function LoadDocument(FileName As String, State As String, SelectedFacilityName As String) As ActionResult
            Try
                DocumentManager.CloseAllDocuments()

                Dim AllRichEditSettings As New Models.AllRichEditSettings
                AllRichEditSettings.RichEditSettingOptions.BehaviorSettings.Copy = DocumentCapability.Disabled
                AllRichEditSettings.RichEditSettingOptions.BehaviorSettings.Printing = DocumentCapability.Disabled
                AllRichEditSettings.RichEditSettings.RichEditRibbonMode = ASPxRichEdit.RichEditRibbonMode.Ribbon

                'AllRichEditSettings.RichEditSettings.EditMode = True
                'AllRichEditSettings.RichEditSettings.ReadOnly = True

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
                    ''End If
                    ''
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
                        If Session("SectionIndexes") Is Nothing Then SectionIndexes = WordFunctions.GetSectionChapters(MasterDocument)
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
                                EFile.Data = WordFunctions.CopyDocSection(MasterDocument, SectionIndex.Index, FacilityName)
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
                    'AllRichEditSettings.EFile = Nothing
                End If

                Dim Docs As List(Of IDocumentInfo) = DocumentManager.GetAllDocuments().ToList

                'Return PartialView("RichEditPartial")
                Return PartialView("RichEditPartial", AllRichEditSettings)
            Catch ex As Exception

            End Try
        End Function

        Public Function LoadDocument3(FileName As String, State As String, SelectedFacilityName As String) As ActionResult
            Try
                DocumentManager.CloseAllDocuments()

                Dim AllRichEditSettings As New Models.AllRichEditSettings
                AllRichEditSettings.RichEditSettingOptions.BehaviorSettings.Copy = DocumentCapability.Disabled
                AllRichEditSettings.RichEditSettingOptions.BehaviorSettings.Printing = DocumentCapability.Disabled
                AllRichEditSettings.RichEditSettings.RichEditRibbonMode = ASPxRichEdit.RichEditRibbonMode.None

                'Settings.ClientSideEvents.DocumentLoaded = "function(s, e) { DXEventMonitor.Trace(s, e, 'DocumentLoaded') }"
                'AllRichEditSettings.RichEditSettings.EditMode = True
                'AllRichEditSettings.RichEditSettings.ReadOnly = True

                Dim MasterFile As Boolean = True

                ' Dim ReadFile As FileInfo = New FileInfo(Server.MapPath("../App_Data/" & "CCG 00208 Screening New and Current Emp, Cont, Vendors, Phys, and other Healthcare Practitioners Pol.docx"))
                'Dim ReadFile As FileInfo = New FileInfo(Server.MapPath("../App_Data/" & "Test.docx"))
                'Dim ReadFile As FileInfo = New FileInfo(MasterFileName)

                Dim ReadFile As FileInfo = Nothing
                Dim EFile As EFile = New EFile()
                Dim MasterDocument As New WordDocument()
                Dim BookMarks As List(Of String)
                Dim word As Word.Application = New Word.Application()


                If MasterFile = True Then
                    Dim WordFunctions As New WordFunctions

                    If Session("MasterDocument") Is Nothing Then
                        Dim MasterFileName As String = Server.MapPath("../App_Data/Policies/" & State & "Masters.docx")

                        ReadFile = New FileInfo(MasterFileName)
                        If ReadFile IsNot Nothing Then MasterDocument = WordFunctions.OpenDocument(MasterFileName, word)
                    End If

                    If Session("MasterDocument") IsNot Nothing Then
                        MasterDocument = Session("MasterDocument")
                    End If


                    If MasterDocument IsNot Nothing Then
                        Session("MasterDocument") = MasterDocument
                        BookMarks = WordFunctions.GetBookMarks(FileName)
                    End If


                    '    If Session("MasterDocument") IsNot Nothing Then
                    '        Dim FacilityName As String = String.Empty

                    '        MasterDocument = Session("MasterDocument")
                    '        FacilityName = SelectedFacilityName
                    '        EFile.Data = WordFunctions.CopyDocSection3(MasterDocument, FacilityName)
                    '        EFile.FriendlyName = FacilityName

                    '    End If





                    If MasterDocument IsNot Nothing Then
                        'Dim FacilityName As String = String.Empty
                        'Dim FacilityUser As New User
                        'If Session("FacilityUser") Is Nothing Then FacilityUser = HomeController.GetCurrentUser()
                        'If Session("FacilityUser") IsNot Nothing Then FacilityUser = Session("FacilityUser")

                        'FacilityName = SelectedFacilityName

                        'If Session("FacilityName") IsNot Nothing Then FacilityName = Session("FacilityName")

                        'If Session("FacilityName") Is Nothing Then
                        '    'Dim Facilites As List(Of Facility) = DataRepository.GetFacilities(FacilityUser.EmailAddress)
                        '    'If Facilites IsNot Nothing Then Session("FacilityName") = Facilites.FirstOrDefault.Name

                        '    'Dim Facility As Facility = DataRepository.GetFacilities3(FacilityUser.EmailAddress)
                        '    'If Facility IsNot Nothing Then Session("FacilityName") = Facility.Name
                        '    'FacilityName = Session("FacilityName")

                        '    FacilityName = SelectedFacilityName


                        'End If

                        'If SectionIndexes IsNot Nothing Then
                        '    Dim DocIndex As String = WordFunctions.GetDocIndexFromPath(FileName)
                        '    Dim SectionIndex = SectionIndexes.Where(Function(s) s.Title = DocIndex).FirstOrDefault
                        '    If SectionIndex IsNot Nothing Then
                        '        EFile.Data = WordFunctions.CopyDocSection(MasterDocument, SectionIndex.Index, FacilityName)
                        '        EFile.FriendlyName = SectionIndex.Title
                        '    End If
                        'End If
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
                        'AllRichEditSettings.EFile = Nothing
                    End If
                End If
                'Dim Docs As List(Of IDocumentInfo) = DocumentManager.GetAllDocuments().ToList

                'Return PartialView("RichEditPartial")
                Return PartialView("RichEditPartial", AllRichEditSettings)
            Catch ex As Exception

            End Try
        End Function

        Public Function LoadDocument4(FileName As String, State As String, SelectedFacilityName As String) As ActionResult
            Try
                DocumentManager.CloseAllDocuments()

                Dim AllRichEditSettings As New Models.AllRichEditSettings
                'AllRichEditSettings.RichEditSettingOptions.BehaviorSettings.Copy = DocumentCapability.Disabled
                AllRichEditSettings.RichEditSettingOptions.BehaviorSettings.Printing = DocumentCapability.Disabled
                AllRichEditSettings.RichEditSettings.RichEditRibbonMode = ASPxRichEdit.RichEditRibbonMode.None
                AllRichEditSettings.RichEditClientSideEvents.DocumentLoaded = "function(s, e) { DXEventMonitor.Trace(s, e, 'DocumentLoaded') }"

                AllRichEditSettings.RichEditSettings.EditMode = True
                AllRichEditSettings.RichEditSettings.ReadOnly = True

                Dim ReadFile As FileInfo = Nothing
                Dim EFile As EFile = New EFile()


                Dim MasterFileName As String = Server.MapPath("../App_Data/Policies/" & State & " Masters.docx")

                ReadFile = New FileInfo(MasterFileName)
                If ReadFile.Exists Then
                    Dim Document As New Syncfusion.DocIO.DLS.WordDocument
                    Document.OpenReadOnly(ReadFile.FullName, FormatType.Docx)

                    EFile.Data = WordFunctions.CopyDocSection(Document, SelectedFacilityName)

                    ' EFile.Data = System.IO.File.ReadAllBytes(MasterFileName)
                    EFile.FriendlyName = ReadFile.Name

                    If EFile.Data IsNot Nothing Then
                        EFile.Name = New Guid().ToString() & ".docx"
                        EFile.Extension = "docx"
                        EFile.FilePath = FileName
                        Session("EFile") = EFile
                        Session("AllRichEditSettings") = AllRichEditSettings
                        AllRichEditSettings.EFile = EFile
                    End If
                End If

                If EFile.Data Is Nothing Then
                    DocumentManager.CloseAllDocuments()
                End If
                Return PartialView("RichEditPartial", AllRichEditSettings)
            Catch ex As Exception

            End Try
        End Function
        'Private Sub SurroundingSub()
        '    If doc.Bookmarks.Exists("bookmark_1") Then
        '        Dim oBookMark As Object = "bookmark_1"
        '        doc.Bookmarks.get_Item(oBookMark).Range.Text = My
        '        Dim [To] As Text
        '        Dim bookmark_1 As Replace
        '    End If

        '    If doc.Bookmarks.Exists("bookmark_2") Then
        '        Dim oBookMark As Object = "bookmark_2"
        '        doc.Bookmarks.get_Item(oBookMark).Range.Text = My
        '        Dim [To] As Text
        '        Dim bookmark_2 As Replace
        '    End If

        '    doc.ExportAsFixedFormat("myNewPdf.pdf", WdExportFormat.wdExportFormatPDF)
        'End Sub

        'Private Sub SurroundingSub()
        '    Dim bookmarksProcessed As List(Of String) = New List(Of String)()

        '    For Each b As Bookmark In Document.Bookmarks

        '        If Not bookmarksProcessed.Contains(b.Name) Then
        '            Dim text As String = getTextFromBookmarkName(b.Name)
        '            Dim newend = b.Range.Start + text.Length
        '            Dim name = b.Name
        '            Dim rng As Range = b.Range
        '            b.Range.Text = text
        '            rng.[End] = newend
        '            Document.Bookmarks.Add(name, rng)
        '            bookmarksProcessed.Add(name)
        '        End If
        '    Next
        'End Sub

        'Private Sub SurroundingSub()
        '    Dim document As WordDocument = New WordDocument()
        '    Dim section As Section = document.Sections.AddSection()
        '    Dim para As Paragraph = section.Blocks.AddParagraph()
        '    para.Inlines.AddText("Sentence start ")
        '    Dim bookmark As Bookmark = New Bookmark(document, "bookmark2")
        '    para.Inlines.Add(bookmark.BookmarkRangeStart)
        '    para.Inlines.AddText("text")
        '    para.Inlines.Add(bookmark.BookmarkRangeEnd)
        '    para.Inlines.AddText(" Sentence end.")
        '    Dim wordFile As WordFile = New WordFile()

        '    Using stream = File.OpenWrite("AddBookmark2.docx")
        '        wordFile.Export(document, stream)
        '    End Using
        'End Sub


        Public Function LoadDocument2(FileName As String) As ActionResult
            Try


                Dim AllRichEditSettings As New Models.AllRichEditSettings
                AllRichEditSettings.RichEditSettingOptions.BehaviorSettings.Copy = DocumentCapability.Disabled
                AllRichEditSettings.RichEditSettingOptions.BehaviorSettings.Printing = DocumentCapability.Disabled
                AllRichEditSettings.RichEditSettings.RichEditRibbonMode = ASPxRichEdit.RichEditRibbonMode.None
                AllRichEditSettings.RichEditSettings.EditMode = True
                AllRichEditSettings.RichEditSettings.ReadOnly = True

                Dim WordFunctions As New WordFunctions

                Dim Bookmarks = WordFunctions.GetBookMarks(FileName)





                'Dim Bookmarks = MasterDocument.Bookmarks

                'EFile.Data = System.IO.File.ReadAllBytes(ReadFile.FullName)
                'EFile.FriendlyName = "test"

                ''Dim stream As New MemoryStream()
                ''MasterDocument.Save(stream, FormatType.Docx)

                '''EFile.Data = stream.ToArray()
                ''EFile.FriendlyName = "test"

                'If EFile.Data IsNot Nothing Then
                '    EFile.Name = New Guid().ToString() & ".docx"
                '    EFile.Extension = "docx"
                '    EFile.FilePath = FileName
                '    Session("EFile") = EFile
                '    Session("AllRichEditSettings") = AllRichEditSettings
                '    AllRichEditSettings.EFile = EFile
                'End If

                'If EFile.Data Is Nothing Then
                '    DocumentManager.CloseAllDocuments()
                '    AllRichEditSettings.EFile = Nothing
                'End If

                Dim Docs As List(Of IDocumentInfo) = DocumentManager.GetAllDocuments().ToList

                Return PartialView("RichEditPartial", AllRichEditSettings)
            Catch ex As Exception

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

            End Try
        End Function

        Function TreeView7() As ActionResult
            Try

                Return PartialView()
            Catch ex As Exception

            End Try
        End Function
        Function TreeView() As ActionResult
            Try


                'Dim FileName As String = Server.MapPath("../App_Data/Temp\GA Masters.docx")
                'LoadDocument2(FileName)



                Dim User As User = HomeController.GetCurrentUser()
                Dim UserInfo As New List(Of GetUserInfo_Result)
                Dim Facilites As List(Of Facility)
                Dim FacilityStates As New List(Of FacilityState)

                If User IsNot Nothing Then
                    Facilites = DataRepository.GetFacilityFromUser(User.EmailAddress)
                    UserInfo = DataRepository.GetUserInfo(User.EmailAddress)

                    If Facilites IsNot Nothing Then
                        For Each Facility As Facility In Facilites
                            If Facility.State IsNot Nothing Then
                                If FacilityStates.Where(Function(s) s.State = Facility.State).Any = False Then
                                    FacilityStates.Add(New FacilityState With {.State = Facility.State, .Icon = ""})
                                End If
                            End If
                        Next

                        For Each FacilityState As FacilityState In FacilityStates
                            Dim FacilitiesInState = Facilites.Where(Function(f) f.State = FacilityState.State).ToList
                            For Each Facility As Facility In FacilitiesInState
                                If Facility.Name IsNot Nothing Then
                                    FacilityState.FacilityNameNodes.Add(New FacilityNameNode With {.Name = Facility.Name, .Icon = ""})
                                End If
                            Next
                        Next
                    End If
                End If

                Dim treeviewIcon As TreeViewImageIcons = New TreeViewImageIcons()
                Dim iconFields As TreeViewFieldsSettings = New TreeViewFieldsSettings()
                iconFields.DataSource = treeviewIcon.GetTreeViewNodesForStates(FacilityStates)


                iconFields.Id = "NodeId"
                iconFields.Text = "NodeText"
                iconFields.IconCss = "Icon"
                iconFields.Child = "NodeChild"
                iconFields.Expanded = "Expanded"
                ViewBag.iconFields2 = iconFields

                Dim ImageIcons As New List(Of ImageIcons)
                For Each Dir As TreeViewImageIcons In iconFields.DataSource
                    ImageIcons.AddRange(Dir.NodeChild)
                Next
                ViewBag.iconFieldsjson2 = JsonConvert.SerializeObject(ImageIcons)

                Dim AllRichEditSettings As New Models.AllRichEditSettings
                AllRichEditSettings.RichEditSettingOptions.BehaviorSettings.Copy = DocumentCapability.Disabled
                AllRichEditSettings.RichEditSettingOptions.BehaviorSettings.Printing = DocumentCapability.Disabled
                AllRichEditSettings.RichEditSettings.RichEditRibbonMode = ASPxRichEdit.RichEditRibbonMode.None
                AllRichEditSettings.RichEditClientSideEvents.DocumentLoaded = "function(s, e) { DXEventMonitor.Trace(s, e, 'DocumentLoaded') }"
                AllRichEditSettings.RichEditSettings.EditMode = True
                AllRichEditSettings.RichEditSettings.ReadOnly = True



                Dim ReadFile As FileInfo = Nothing
                Dim EFile As EFile = New EFile()


                Dim MasterFileName As String = Server.MapPath("../App_Data/Policies/Blank.docx")

                ReadFile = New FileInfo(MasterFileName)
                If ReadFile.Exists Then
                    EFile.Data = System.IO.File.ReadAllBytes(MasterFileName)
                    EFile.FriendlyName = ReadFile.Name

                    If EFile.Data IsNot Nothing Then
                        EFile.Name = New Guid().ToString() & ".docx"
                        EFile.Extension = "docx"
                        ' EFile.FilePath = FileName
                        Session("EFile") = EFile
                        Session("AllRichEditSettings") = AllRichEditSettings
                        AllRichEditSettings.EFile = EFile
                    End If
                End If
                AllRichEditSettings.EFile = EFile


                Return PartialView("TreeView", AllRichEditSettings)
            Catch ex As Exception

            End Try
        End Function

        Function TreeViewPolicies(State As String, SelectedFacilityName As String) As String
            Try
                Dim SectionIndexes As New List(Of SectionIndex)
                Dim WordFunctions As New WordFunctions
                Dim Document As New Syncfusion.DocIO.DLS.WordDocument
                Dim iconFields As TreeViewFieldsSettings = New TreeViewFieldsSettings()
                Dim treeviewIcon As TreeViewImageIcons = New TreeViewImageIcons()




                'If Session("MasterDocument") Is Nothing Then
                '    Dim MasterFileName As String = Server.MapPath("../App_Data/" & State & "Masters.docx")
                '    ReadFile = New FileInfo(MasterFileName)
                '    If ReadFile IsNot Nothing Then MasterDocument = WordFunctions.OpenDocument(MasterFileName)
                '    If MasterDocument IsNot Nothing Then
                '        Session("MasterDocument") = MasterDocument
                '        Bookmarks = WordFunctions.GetBookMarks(FileName)
                '    End If
                'End If



                If Session("State") <> State Then
                    Dim MasterFile = WordFunctions.GetMasterDocument(Server.MapPath("/App_Data/Policies/"), State)
                    Document.OpenReadOnly(MasterFile.FullName, FormatType.Docx)
                    If Document IsNot Nothing Then
                        'SectionIndexes = WordFunctions.GetSectionTitle(Document)
                        SectionIndexes = WordFunctions.GetSectionTitle2(Document)
                        'Bookmarks = WordFunctions.GetBookMarks(FileName)
                        Session("MasterDocument") = Document
                        If SectionIndexes IsNot Nothing Then Session("SectionIndexes") = SectionIndexes
                    End If



                End If

                If Session("State") = State Then
                    If Session("MasterDocument") IsNot Nothing Then Document = Session("MasterDocument")
                    If Session("SectionIndexes") IsNot Nothing Then SectionIndexes = Session("SectionIndexes")
                End If
                Session("State") = State

                If Session("MasterDocument") Is Nothing Then
                    Dim MasterFile = WordFunctions.GetMasterDocument(Server.MapPath("/App_Data/Policies/"), State)
                    Document.OpenReadOnly(MasterFile.FullName, FormatType.Docx)
                    Session("MasterDocument") = Document
                End If

                If Session("SectionIndexes") Is Nothing Then
                    SectionIndexes = WordFunctions.GetSectionTitle(Document)
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

        '    Catch ex As Exception

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
        '    Catch ex As Exception

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

                    TreeviewImageIcons.Add(New TreeViewImageIcons With {.NodeId = SteateCounter.ToString, .NodeText = State.State, .Icon = "folder", .NodeChild = ImageIcons})

                    Dim FacilityNameCounter As Integer = 0
                    For Each FacilityName As FacilityNameNode In State.FacilityNameNodes
                        FacilityNameCounter += 1

                        ImageIcons.Add(New ImageIcons With {.NodeId = FacilityNameCounter.ToString, .NodeText = FacilityName.Name, .FullPath = State.State, .Icon = ""})
                    Next
                Next

                Return TreeviewImageIcons
            Catch ex As Exception

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

                Dim FolderCounter As Integer = 0
                For Each Header As String In DirectoryHeaders

                    Dim Root As Integer = -1
                    Dim Parsed = Integer.TryParse(Header.Substring(0, 1), Root)

                    ' Dim SectionIndexs2 = SectionIndexes.Where(Function(s) s.Root = Root)
                    If Parsed = True And SectionIndexes.Where(Function(s) s.Root = Root).Any Then

                        FolderCounter += 1
                        Dim ImageIcons As New List(Of ImageIcons)
                        Dim FileSearchPattern As String = "*.*"

                        TreeviewImageIcons.Add(New TreeViewImageIcons With {.NodeId = FolderCounter.ToString, .NodeText = Header, .Icon = "folder", .NodeChild = ImageIcons})

                        Dim FileCounter As Integer = 0
                        For Each Index As SectionIndex In SectionIndexes.Where(Function(s) s.Root = Root)
                            FileCounter += 1
                            Dim ID As String = $"{FolderCounter.ToString}-{FileCounter.ToString}"
                            ImageIcons.Add(New ImageIcons With {.NodeId = ID, .NodeText = Index.Header, .FullPath = Index.Bookmark, .Icon = "docx"})
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
        '    Catch ex As Exception

        '    End Try

        'End Function


    End Class


    Public Class ImageIcons
        Public Property NodeText As String
        Public Property Icon As String
        Public Property NodeId As String
        Public Property FullPath As String
    End Class

    Public Class FacilityState
        Public Property State As String
        Public Property Icon As String
        Public FacilityNameNodes As New List(Of FacilityNameNode)
    End Class

    Public Class FacilityNameNode
        Public Property Name As String
        Public Property Icon As String
    End Class
End Namespace