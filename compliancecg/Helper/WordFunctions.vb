Imports System.IO
Imports CCGData
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Word
Imports DevExpress.Web.Office
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native
Imports Section = Microsoft.Office.Interop.Word.Section
Imports Field = Microsoft.Office.Interop.Word.Field
Imports SubDocument = DevExpress.XtraRichEdit.API.Native.SubDocument
Imports Syncfusion.DocIO
Imports Syncfusion.DocIO.DLS
Imports Syncfusion.DocToPDFConverter
Imports Syncfusion.Pdf
Imports Style = Microsoft.Office.Interop.Word.Style
Imports HeaderFooterType = DevExpress.XtraRichEdit.API.Native.HeaderFooterType
Imports NLog
Imports Syncfusion.Pdf.Security
Imports Syncfusion.Pdf.Parsing
Imports Syncfusion.Pdf.Graphics
Imports System.Drawing
Imports Syncfusion.Pdf.Interactive
Imports Bookmark = Microsoft.Office.Interop.Word.Bookmark

Public Class WordFunctions
    Private DataRepository As New DataRepository
    Private Shared DocVariablesFields As New List(Of WField)
    Private Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Public Function SyncfusionBookmarks(FileName As String)

        'Dim document As New WordDocument()
        ''Adds a new section into the Word Document
        'Dim section As IWSection = document.AddSection()
        ''Adds a new paragraph into Word document and appends text into paragraph
        'Dim paragraph As IWParagraph = section.AddParagraph()
        'paragraph.AppendText("Northwind Database")
        'paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center
        ''Adds a paragraph into section
        'paragraph = section.AddParagraph()
        ''Adds a new bookmark start into paragraph with name "Northwind"
        'paragraph.AppendBookmarkStart("Northwind")
        ''Adds a text between the bookmark start and end into paragraph
        'paragraph.AppendText("The Northwind sample database (Northwind.mdb) is included with all versions of Access. It provides data you can experiment with and database objects that demonstrate features you might want to implement in your own databases.")
        ''Adds a new bookmark end into paragraph with name " Northwind "
        'paragraph.AppendBookmarkEnd("Northwind")
        ''Adds a text after the bookmark end
        'paragraph.AppendText(" Using Northwind, you can become familiar with how a relational database is structured and how the database objects work together to help you enter, store, manipulate, and print your data.")
        ''Saves the document in the given name and format
        'document.Save("Bookmarks.docx", FormatType.Docx)
        ''Releases the resources occupied by WordDocument instance
        'document.Close()


        'Dim document As New WordDocument("Bookmarks.docx", FormatType.Docx)
        ''Gets the bookmark instance by using FindByName method of BookmarkCollection with bookmark name
        'Dim bookmark As Syncfusion.DocIO.DLS.Bookmark = document.Bookmarks.FindByName("Northwind")
        ''Accesses the bookmark start’s owner paragraph by using bookmark and changes its back color
        'bookmark.BookmarkStart.OwnerParagraph.ParagraphFormat.BackColor = Color.AliceBlue
        'document.Save("Result.docx", FormatType.Docx)
        'document.Close()

        'Dim document As New WordDocument("Bookmarks.docx", FormatType.Docx)
        ''Gets the bookmark instance by using FindByName method of BookmarkCollection with bookmark name
        'Dim bookmarkc = document.Bookmarks.FindByName("Northwind")
        ''Removes the bookmark named "Northwind" from Word document.
        'document.Bookmarks.Remove(bookmark)
        'document.Save("Result.docx", FormatType.Docx)
        'document.Close()

    End Function

    Public Function SetUpDocument2(File As FileInfo)
        Try
            AddVariablesToFiles(File)
            SetBookMarks2(File)
            '  WordFunctions.SetHyperLinks(WordFile)
        Catch ex As Exception
            logger.Error(ex)
        End Try
    End Function


    Private Function AddVariablesToFiles(File As FileInfo)
        Try

            If File.Length > 500 Then
                'Dim Document As New Syncfusion.DocIO.DLS.WordDocument
                'Document.OpenReadOnly(File.FullName, FormatType.Docx)
                Dim Document As New Syncfusion.DocIO.DLS.WordDocument(File.FullName, FormatType.Docx)

                Dim FindText = "[FACILITY NAME]"
                Dim ReplaceText = "{Facility}"
                Dim Replaced As Boolean = AddDocVariables(Document, FindText, ReplaceText)
                Document.Save(File.FullName, FormatType.Docx)
                Document.Close()
            End If


        Catch ex As Exception
            logger.Error(ex)

        End Try

    End Function

    Private Function SetBookMarks2(File As FileInfo)
        Try
            Dim BookMarkNames As New List(Of String)

            If File.Exists And File.Length > 500 Then
                Dim Document As New Syncfusion.DocIO.DLS.WordDocument(File.FullName, FormatType.Docx)
                Document.Bookmarks.Clear()
                Dim SectionCounter As Integer = 0
                For Each Section As Syncfusion.DocIO.DLS.WSection In Document.Sections
                    Dim BookMarkParagraph As WParagraph = Document.CreateParagraph
                    Section.Paragraphs.Insert(0, BookMarkParagraph)

                    Dim HeadersFooters As WHeadersFooters = Section.HeadersFooters
                    Dim Footers As DLS.HeaderFooter = HeadersFooters.Footer
                    Dim ChildEntities As EntityCollection = Footers.ChildEntities

                    For Each Child As Entity In ChildEntities
                        If Child.EntityType = EntityType.Paragraph Then
                            Dim Paragraph As WParagraph = Child.Clone
                            Dim FooterText As String = Paragraph.Text
                            If FooterText.Contains("CCG") Then
                                Dim Title = GetCCGIndex(FooterText)
                                If Title IsNot Nothing Then
                                    Dim BookMarkName = Title.Replace(" ", "")
                                    If BookMarkNames.IndexOf(BookMarkName) = -1 Then
                                        BookMarkNames.Add(BookMarkName)
                                        BookMarkParagraph.AppendBookmarkStart(Title)
                                        BookMarkParagraph.AppendText(Title)
                                        BookMarkParagraph.AppendBookmarkEnd(Title)
                                        Exit For
                                    End If
                                End If
                            End If
                        End If

                        If Child.EntityType = EntityType.BlockContentControl Then
                            Dim BlockContentControl As BlockContentControl = Child.Clone()
                            Dim ChildEntities2 As EntityCollection = BlockContentControl.ChildEntities

                            For Each Child2 As Entity In ChildEntities2
                                If Child2.EntityType = EntityType.Paragraph Then
                                    Dim Paragraph2 As WParagraph = Child2.Clone
                                    Dim FooterText2 As String = Paragraph2.Text
                                    Dim Title = GetCCGIndex(FooterText2)

                                    If Title IsNot Nothing Then
                                        Dim BookMarkName = Title.Replace(" ", "")
                                        If BookMarkNames.IndexOf(BookMarkName) = -1 Then
                                            BookMarkNames.Add(BookMarkName)
                                            BookMarkParagraph.AppendBookmarkStart(Title)
                                            BookMarkParagraph.AppendText(Title)
                                            BookMarkParagraph.AppendBookmarkEnd(Title)
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                        End If

                    Next
                    SectionCounter += 1
                Next

                'Document.Save("c: \CCG\Sample.docx", FormatType.Docx)
                Document.Save(File.FullName, FormatType.Docx)
                Document.Close()
            End If
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function SetBookMarks3(FileName As String)
        Try
            ' Dim WordFunctions As New WordFunctions

            Dim ReadFile As FileInfo = New FileInfo(FileName)
            Dim word As Word.Application = New Word.Application()

            Dim MasterDocument = OpenDocument(ReadFile.FullName, word)
            If MasterDocument IsNot Nothing Then
                For Each BookMark As Word.Bookmark In MasterDocument.Bookmarks
                    BookMark.Delete()
                Next

                Dim BookMarkNames As New List(Of String)
                Dim BookMarkCounter As Integer = 1
                For Each section As Word.Section In MasterDocument.Sections
                    Dim Range As Range = section.Range
                    Dim FooterRange As Range = section.Footers(WdHeaderFooterIndex.wdHeaderFooterFirstPage).Range
                    ' Dim bookmarkName As String = $"BookMark{BookMarkCounter}"

                    Dim Footers = section.Footers
                    For Each Footer In Footers
                        Dim FooterText = Footer.range.text

                        Dim Title = GetCCGIndex(FooterText)
                        If Title IsNot Nothing Then
                            Dim BookMarkName = Title.Replace(" ", "")
                            If BookMarkNames.IndexOf(BookMarkName) = -1 Then
                                BookMarkNames.Add(BookMarkName)
                                section.Range.Bookmarks.Add(BookMarkName)
                                BookMarkCounter += 1
                                Exit For
                            End If
                        End If
                    Next
                Next
                MasterDocument.Save()
                MasterDocument.Close()
            End If


        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function




    Public Function Document(WordFilePath As String) As String
        Dim word As Word.Application = New Word.Application()
        Dim doc As Word.Document = OpenDocument(WordFilePath, word)
        ' FindAndReplace(doc)
        FillFieldValues(doc)
        Dim FileName As String = SaveDocument(WordFilePath, doc)
        Return FileName
    End Function

    'Shared Sub BookMarkReplace(FileName As String, ByRef bookmark As Word.Bookmark, ByVal newText As String)
    '    Dim WordFunctions As New WordFunctions

    '    Dim ReadFile As FileInfo = New FileInfo(FileName)
    '    Dim MasterDocument = WordFunctions.OpenDocument(ReadFile.FullName)



    '    If MasterDocument.Bookmarks.Exists("bookmark1") Then
    '        bookmark = MasterDocument.Bookmarks("bookmark1")

    '        Dim rng As Word.Range = bookmark.Range
    '        Dim bookmarkName As String = bookmark.Name
    '    End If
    'End Sub

    'Private Sub SurroundingSub()
    '    Dim section As Word.Section
    '    Dim header As Word.HeaderFooter
    '    Dim footer As Word.HeaderFooter
    '    Dim headerRange As Word.Range
    '    Dim footerRange As Word.Range
    '    Dim table As Word.Table
    '    Dim title As String
    '    Dim version As String
    '    Dim date As String
    '    Dim page As String

    '    For i As Integer = 1 To docs.Sections.Count

    '        Try
    '            section = docs.Sections(i)

    '            If section IsNot Nothing Then
    '                header = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary)
    '                headerRange = header.Range

    '                If headerRange.Tables.Count > 0 Then
    '                    table = headerRange.Tables(1)
    '                    title = table.Cell(1, 2).Range.Text
    '                    version = table.Cell(2, 2).Range.Text
    '                    Date = table.Cell(2, 2).Range.Text
    '                End If

    '                footer = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary)
    '                footerRange = footer.Range

    '                If footerRange.Tables.Count > 0 Then
    '                    table = footerRange.Tables(1)
    '                    page = table.Cell(1, 2).Range.Text
    '                End If
    '            End If

    '        Catch
    '        End Try
    '    Next
    'End Sub

    Public Function SetBookMarks(FileName As String)
        Try
            ' Dim WordFunctions As New WordFunctions

            Dim ReadFile As FileInfo = New FileInfo(FileName)
            Dim word As Word.Application = New Word.Application()

            Dim MasterDocument = OpenDocument(ReadFile.FullName, word)
            If MasterDocument IsNot Nothing Then
                For Each BookMark As Word.Bookmark In MasterDocument.Bookmarks
                    BookMark.Delete()
                Next

                Dim BookMarkNames As New List(Of String)
                Dim BookMarkCounter As Integer = 1
                For Each section As Word.Section In MasterDocument.Sections
                    Dim Range As Range = section.Range
                    Dim FooterRange As Range = section.Footers(WdHeaderFooterIndex.wdHeaderFooterFirstPage).Range
                    ' Dim bookmarkName As String = $"BookMark{BookMarkCounter}"

                    Dim Footers = section.Footers
                    For Each Footer In Footers
                        Dim FooterText = Footer.range.text

                        Dim Title = GetCCGIndex(FooterText)
                        If Title IsNot Nothing Then
                            Dim BookMarkName = Title.Replace(" ", "")
                            If BookMarkNames.IndexOf(BookMarkName) = -1 Then
                                BookMarkNames.Add(BookMarkName)
                                section.Range.Bookmarks.Add(BookMarkName)
                                BookMarkCounter += 1
                                Exit For
                            End If
                        End If
                    Next
                Next
                MasterDocument.Save()
                MasterDocument.Close()
            End If


        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function SetHyperLinks(FileName As String)
        Try
            Dim BookMarkNames As New List(Of String)
            Dim word As Word.Application = New Word.Application()

            Dim ReadFile As FileInfo = New FileInfo(FileName)



            If ReadFile.Exists And ReadFile.Length > 500 Then
                'Dim MasterDocument = OpenDocument(ReadFile.FullName, word)
                Dim Document As New Syncfusion.DocIO.DLS.WordDocument(ReadFile.FullName)

                'For Each Section As Word.Section In Document.Sections
                '    For Each para As WParagraph In Section.Range.Paragraphs
                '        para.Find()
                '    Next
                'Next



                Dim HyperLinkCounter As Integer = 0
                For Each BookMark As Syncfusion.DocIO.DLS.Bookmark In Document.Bookmarks
                    If HyperLinkCounter < 1000 Then
                        If BookMark.Name.Substring(0, 3) = "CCG" Then
                            Dim BookmarkLabel = BookMark.Name.Replace("CCG", "CCG ")
                            Dim BookmarkLabel2 = BookMark.Name.Replace("CCG", "CCG ") + " "

                            Dim paragraph2 As WParagraph = New WParagraph(Document)
                            paragraph2.AppendHyperlink(BookMark.Name, BookmarkLabel2, HyperlinkType.Bookmark)

                            Dim textBodyPart As TextBodyPart = New TextBodyPart(Document)
                            textBodyPart.BodyItems.Add(paragraph2)

                            'Document.ReplaceFirst = True
                            'Dim Replaced = Document.Replace(BookmarkLabel, textBodyPart, False, True)

                            'Dim Regex As New Regex("^[C][C][G]\s[0][0][\d]")
                            Dim Regex As New Regex("^" & BookmarkLabel + "\s")
                            Dim Replaced = Document.Replace(Regex, textBodyPart)

                            HyperLinkCounter += 1
                        End If
                    End If
                Next
                'Document.Save("c: \CCG\Sample.docx", FormatType.Docx)
                Document.Save(FileName)
                Document.Close()
            End If
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function



    Public Function SetHyperLinks2(FileName As String)
        Try
            Dim BookMarkNames As New List(Of String)
            Dim word As Word.Application = New Word.Application()

            Dim ReadFile As FileInfo = New FileInfo(FileName)

            If ReadFile.Exists And ReadFile.Length > 500 Then
                'Dim MasterDocument = OpenDocument(ReadFile.FullName, word)
                Dim Document As New Syncfusion.DocIO.DLS.WordDocument(ReadFile.FullName)

                Dim HyperLinkCounter As Integer = 0
                For Each BookMark As Syncfusion.DocIO.DLS.Bookmark In Document.Bookmarks
                    If HyperLinkCounter < 2 Then
                        If BookMark.Name.Substring(0, 3) = "CCG" Then
                            Dim BookmarkLabel = BookMark.Name.Replace("CCG", "CCG ")

                            Dim paragraph2 As WParagraph = New WParagraph(Document)
                            paragraph2.AppendHyperlink(BookMark.Name, BookmarkLabel, HyperlinkType.Bookmark)

                            ' Document.ReplaceFirst = True

                            Dim textBodyPart As TextBodyPart = New TextBodyPart(Document)
                            textBodyPart.BodyItems.Add(paragraph2)
                            Document.Replace(BookmarkLabel, textBodyPart, False, True)
                            HyperLinkCounter += 1
                        End If
                    End If
                Next
                Document.Save("c:\CCG\Sample.docx", FormatType.Docx)
                Document.Close()
            End If
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function


    Public Shared Function AddDocVariables(Document As WordDocument, FindText As String, ReplaceText As String) As Boolean
        Try

            'Dim ReplaceText = "{FACILITY NAME}"
            Dim paragraph As WParagraph = New WParagraph(Document)
            paragraph.AppendField(ReplaceText, FieldType.FieldDocVariable)
            Dim bodyPart As TextBodyPart = New TextBodyPart(Document)
            bodyPart.BodyItems.Add(paragraph)
            Dim Replaced = Document.Replace(FindText, bodyPart, False, False, True)
            Document.UpdateDocumentFields()
            Return True
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function
    'Public Function AddDocVariables(Document As WordDocument, FindText As String, ReplaceText As String) As Boolean
    '    Try

    '        'Dim ReplaceText = "{FACILITY NAME}"
    '        Dim paragraph As WParagraph = New WParagraph(Document)
    '        paragraph.AppendField(ReplaceText, FieldType.FieldDocVariable)
    '        Dim bodyPart As TextBodyPart = New TextBodyPart(Document)
    '        bodyPart.BodyItems.Add(paragraph)
    '        Document.Replace(FindText, bodyPart, False, False, True)
    '        Document.UpdateDocumentFields()
    '        Return True
    '  Catch ex As Exception
    'logger.Error(ex)

    '    End Try
    'End Function


    'Public Function SetHyperLinks2(FileName As String)
    '    Try
    '        Dim BookMarkNames As New List(Of String)
    '        Dim word As Word.Application = New Word.Application()

    '        Dim ReadFile As FileInfo = New FileInfo(FileName)

    '        If ReadFile.Exists And ReadFile.Length > 500 Then
    '            Dim Document As New Syncfusion.DocIO.DLS.WordDocument(ReadFile.FullName)

    '            Dim HyperLinkCounter As Integer = 0
    '            For Each BookMark As Syncfusion.DocIO.DLS.Bookmark In Document.Bookmarks
    '                If HyperLinkCounter < 2 Then
    '                    If BookMark.Name.Substring(0, 3) = "CCG" Then
    '                        Dim BookmarkLabel = BookMark.Name.Replace("CCG", "CCG ")

    '                        Dim paragraph2 As WParagraph = New WParagraph(Document)
    '                        paragraph2.AppendHyperlink(BookMark.Name, BookmarkLabel, HyperlinkType.Bookmark)

    '                        'Dim Hyper As Syncfusion.DocIO.DLS.Hyperlink
    '                        'Hyper.BookmarkName = BookmarkLabel
    '                        'Hyper.TextToDisplay = BookmarkLabel
    '                        'Hyper.Type = HyperlinkType.Bookmark
    '                        'Hyper.Uri = BookMark.Name





    '                        Dim Selections As TextSelection() = Document.FindAll(BookmarkLabel, True, False)
    '                        Dim TextSelections As List(Of TextSelection) = Selections.ToList

    '                        For Each Selection As TextSelection In TextSelections

    '                            Dim bodyPart As New TextBodyPart(Selection)

    '                            Dim textBodyPart As TextBodyPart = New TextBodyPart(Document)
    '                            textBodyPart.BodyItems.Add(paragraph2)
    '                            Document.ReplaceFirs = True
    '                            'Replaces a particular text with the text body part
    '                            Document.Replace(BookmarkLabel, TextBodyPart, False, True)



    '                            'Dim SelectionRanges As WTextRange() = Selection.GetRanges
    '                            'Dim SelectionRange = SelectionRanges.FirstOrDefault
    '                            '' Dim SelectionRangeClone = SelectionRange.Owner.Clone


    '                            'Dim ChildEntities As ParagraphItemCollection = DirectCast(SelectionRange.Owner, WParagraph).ChildEntities

    '                            'If ChildEntities.Count > 0 Then
    '                            '    If ChildEntities.FirstItem.EntityType = EntityType.TextRange Then
    '                            '        Dim Ent As WTextRange = ChildEntities.FirstItem
    '                            '        ChildEntities.Remove(Ent)
    '                            '        ChildEntities.Add(Hyper)

    '                            '    End If
    '                            'End If




    '                            'Dim Section As IWSection = Document.AddSection()
    '                            'Dim Paragraph As IWParagraph = Section.AddParagraph()

    '                            'Paragraph.AppendText("Northwind Database")
    '                            'Paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center
    '                            'Paragraph = Section.AddParagraph()
    '                            'Paragraph.AppendBookmarkStart("Northwind")
    '                            'Paragraph.AppendText("The Northwind sample database (Northwind.mdb) is included with all versions of Access.")
    '                            'Paragraph.AppendBookmarkEnd("Northwind")

    '                            'Dim Bookmark As Syncfusion.DocIO.DLS.Bookmark = Document.Bookmarks.FindByName("Northwind")





    '                            'Dim paragraph2 As WParagraph = New WParagraph(Document)
    '                            'paragraph2.AppendHyperlink(Bookmark.Name, "Syncfusion", HyperlinkType.Bookmark)

    '                            'Dim textBodyPart As TextBodyPart = New TextBodyPart(Document)
    '                            'textBodyPart.BodyItems.Add(paragraph2)
    '                            'Document.Replace("INTRODUCTION", textBodyPart, False, True)


    '                            ' SelectionRange.OwnerParagraph.AppendHyperlink(BookMark.Name, FindWord, HyperlinkType.Bookmark)
    '                            HyperLinkCounter += 1
    '                            Exit For
    '                        Next




    '                        HyperLinkCounter += 1
    '                    End If
    '                End If
    '            Next
    '            Document.Save("c:\CCG\Sample.docx", FormatType.Docx)
    '            Document.Close()
    '        End If





    '        '        End If
    '        '    End If
    '        'Next







    '        'Document.Save("C:\CCG\Hyperlink.docx", FormatType.Docx)

    '        'Document.Close()




    '        'Dim Replaced As Boolean = WordFunctions.AddDocVariables(Document, FindText, ReplaceText)
    '        '    For Each BookMark As Word.Bookmark In MasterDocument.Bookmarks
    '        '    Dim FindWord = BookMark.Name.Replace("CCG", "CCG ")
    '        '    BookMarkNames.Add(BookMark.Name)
    '        '    ' Section.Range.Hyperlinks.Add(BookMarkName)
    '        'Next





    '        'Dim document As New WordDocument()
    '        ''Adds new section to the document
    '        'Dim section As IWSection = document.AddSection()
    '        ''Adds new paragraph to the section
    '        'Dim paragraph As IWParagraph = section.AddParagraph()
    '        'paragraph.AppendText("Web Hyperlink:  ")
    '        'paragraph = section.AddParagraph()
    '        ''Appends web hyperlink to the paragraph
    '        'Dim field As IWField = paragraph.AppendHyperlink("http://www.syncfusion.com", "Syncfusion", HyperlinkType.WebLink)
    '        ''Saves the Word document
    '        'document.Save("Sample.docx", FormatType.Docx)
    '        ''Closes the document
    '        'document.Close()c




    '        'Dim document As New WordDocument()
    '        ''Adds new section to the document
    '        'Dim section As IWSection = document.AddSection()
    '        ''Adds new paragraph to the section
    '        'Dim paragraph As IWParagraph = section.AddParagraph()
    '        'paragraph.AppendText("Web Hyperlink:  ")
    '        'paragraph = section.AddParagraph()
    '        ''Appends web hyperlink to the paragraph
    '        'Dim field As IWField = paragraph.AppendHyperlink("http://www.syncfusion.com", "Syncfusion", HyperlinkType.WebLink)
    '        ''Saves the Word document
    '        'document.Save("Sample.docx", FormatType.Docx)
    '        ''Closes the document
    '        'document.Close()


    '  Catch ex As Exception
    'logger.Error(ex)

    '    End Try
    'End Function
    Public Function GetBookMarks(FileName As String) As List(Of String)
        Try
            Dim WordFunctions As New WordFunctions
            Dim BookMarkNames As New List(Of String)
            Dim word As Word.Application = New Word.Application()

            Dim ReadFile As FileInfo = New FileInfo(FileName)
            Dim MasterDocument = OpenDocument(ReadFile.FullName, word)
            For Each BookMark As Word.Bookmark In MasterDocument.Bookmarks
                BookMarkNames.Add(BookMark.Name)
            Next

            Return BookMarkNames
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function


    'Public Shared Function Bookmarks(FileName As String)
    '    Dim WordFunctions As New WordFunctions
    '    'Dim firstParagraph As Microsoft.Office.Tools.Word.Bookmark
    '    'firstParagraph = Me.Controls.AddBookmark(Me.Paragraphs(1).Range, "FirstParagraph")

    '    '                Section Section = Document.Sections[0]
    '    'Section.Paragraphs[2].AppendBookmarkStart("bookmark")  




    '    '//Load document  
    '    'Document Document = New Document();  
    '    'Document.LoadFromFile(@"Bookmark.docx");  

    '    '//Add a section to the document And two paragraphs to the section  
    '    'Section sec = Document.AddSection();  
    '    'sec.AddParagraph().AppendText("Welcome Back, ");  
    '    'sec.AddParagraph().AppendText("Friend! ");  

    '    '//Get the text body part of the two paragraphs  
    '    'ParagraphBase firstReplacementParagraph = sec.Paragraphs[0].Items.FirstItem as ParagraphBase;  
    '    'ParagraphBase lastReplacementParagraph = sec.Paragraphs[sec.Paragraphs.Count - 1].Items.LastItem as ParagraphBase;  
    '    'TextBodySelection Selection = New TextBodySelection(firstReplacementParagraph, lastReplacementParagraph);  
    '    'TextBodyPart Part = New TextBodyPart(Selection);  

    '    '//Go to “bookmark”, remove its content but save the formatting  
    '    'BookmarksNavigator bookmarkNavigator = New BookmarksNavigator(Document);  
    '    'bookmarkNavigator.MoveToBookmark("bookmark", True, True);  
    '    'bookmarkNavigator.DeleteBookmarkContent(True);  

    '    '//Replace the bookmark content with the text body part of the two paragraphs And keep formatting  
    '    'bookmarkNavigator.ReplaceBookmarkContent(Part, True, True);  

    '    '//Remove the section And save the document  
    '    'Document.Sections.Remove(sec);  
    '    'Document.SaveToFile("ReplaceBookmark.docx");   



    '    ' Dim EFile As EFile = New EFile()
    '    ' Dim MasterDocument As New WordDocument()
    '    Dim ReadFile As FileInfo = New FileInfo(FileName)
    '    Dim MasterDocument = WordFunctions.OpenDocument(ReadFile.FullName)

    '    Dim bookmarksProcessed As List(Of String) = New List(Of String)()

    '    'For Each b As Word.Bookmark In MasterDocument.Bookmarks
    '    '    If Not bookmarksProcessed.Contains(b.Name) Then

    '    '    End If
    '    'Next




    '    If MasterDocument.Bookmarks.Exists("bookmark1") Then
    '        Try
    '            Dim rng As Word.Range = Bookmark.Range
    '            'Dim bookmarkName As String = Bookmark.Name
    '            'Bookmark.Range.Text = newText


    '            'Dim oBookMark As Object = "bookmark1"
    '            'Dim bk As Word.Bookmark = MasterDocument.Bookmarks("bookmark1")
    '            'Dim sec As Section = MasterDocument.AddSection()
    '            ''  Dim bookmark_1 As Replace
    '      Catch ex As Exception
    'logger.Error(ex)

    '        End Try
    '    End If
    'End Function

    Public Sub ConvertWordFile(WordFilePath As String)
        Try
            Dim Missing As Object = System.Reflection.Missing.Value
            Dim File As FileInfo = New FileInfo(WordFilePath)

            Dim SaveAsDir = "C:\CCG\temp"
            Dim SaveasFile As String = File.Name.Replace(File.Extension.ToString, "")
            SaveasFile = $"{SaveasFile}.xml"
            Dim FullSaveAS As String = $"{SaveAsDir}\{SaveasFile}"
            If (Not System.IO.Directory.Exists(SaveAsDir)) Then System.IO.Directory.CreateDirectory(SaveAsDir)

            Dim word As Word.Application = New Word.Application()

            Dim [readOnly] As Object = True
            Dim docs As Word.Document = word.Documents.Open(WordFilePath, Missing, [readOnly], Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing)



            docs.SaveAs(FullSaveAS, word.WdSaveFormat.wdFormatFlatXML)
            ', Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing)
            docs.Close()


        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Sub

    'Public Function OpenDocument(WordFilePath As String) As Word.Document
    '    Try
    '        Dim Missing As Object = System.Reflection.Missing.Value
    '        Dim File As FileInfo = New FileInfo(WordFilePath)
    '        Dim word As Word.Application = New Word.Application()

    '        Dim [readOnly] As Object = True
    '        If File IsNot Nothing Then
    '            Dim doc As Word.Document = word.Documents.Open(WordFilePath, Missing, [readOnly], Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing)
    '            Return doc
    '        End If
    '  Catch ex As Exception
    'logger.Error(ex)

    '    End Try
    'End Function


    Public Shared Function GetMasterDocument(Path As String, State As String) As Syncfusion.DocIO.DLS.WordDocument
        Try
            ' Dim WordFunctions As New WordFunctions
            'Dim Path As String = "C:\Projects\Policies\Masters for State\"
            ' Dim Path As String = Server.MapPath("../App_Data/" & "GA Masters.docx")"

            'Dim searchPattern As String = "*Masters.docx"
            'Dim DirectoryInfo As DirectoryInfo = New DirectoryInfo(Path)
            'Dim directories As DirectoryInfo() = DirectoryInfo.GetDirectories(searchPattern, SearchOption.TopDirectoryOnly)
            'Dim Files As FileInfo() = DirectoryInfo.GetFiles(searchPattern, SearchOption.AllDirectories)

            'If State.Length > 2 Then
            '    Select Case State
            '        Case "New Jersey"
            '            State = "NJ"
            '    End Select
            'End If

            Dim FileName As String = $"{State} Masters.docx"

            Dim Blob As New AzureFiles
            Dim WordDocument As New Syncfusion.DocIO.DLS.WordDocument
            WordDocument = Blob.GetBlobWordFile("policies", FileName)

            If WordDocument IsNot Nothing Then Return WordDocument

            'Dim Extension = FileName.Split(".")

            'If Extension.Length > 1 Then
            '    Select Case Extension.Last
            '        Case "docx"
            '            WordDocument = Blob.GetBlobWordFile("policies", FileName)
            '        Case = "Pdf"

            '    End Select
            'End If




            'Dim File As FileInfo = Files.Where(Function(f) f.Name = FileName).SingleOrDefault
            'If File IsNot Nothing Then
            '    Return File
            'End If

        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Shared Function GetMasterDocument(State As String) As Syncfusion.DocIO.DLS.WordDocument
        Try

            Dim FileName As String = $"{State} Masters.docx"

            Dim Blob As New AzureFiles
            Dim WordDocument As New Syncfusion.DocIO.DLS.WordDocument
            WordDocument = Blob.GetBlobWordFile("policies", FileName)

            If WordDocument IsNot Nothing Then Return WordDocument
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function



    Public Shared Function GetMasterDocuments() As FileInfo
        Try
            Dim WordFunctions As New WordFunctions
            Dim Path As String = "C:\Projects\Policies\Masters for State\"

            Dim searchPattern As String = "*Masters.docx"
            Dim DirectoryInfo As DirectoryInfo = New DirectoryInfo(Path)
            Dim directories As DirectoryInfo() = DirectoryInfo.GetDirectories(searchPattern, SearchOption.TopDirectoryOnly)
            Dim Files As FileInfo() = DirectoryInfo.GetFiles(searchPattern, SearchOption.AllDirectories)


            For Each File As FileInfo In Files
                'Dim Document As New Syncfusion.DocIO.DLS.WordDocument
                'Document.OpenReadOnly(File.FullName, FormatType.Docx)
                Dim Document As New Syncfusion.DocIO.DLS.WordDocument(File.FullName)
                Dim FindText = "[FACILITY NAME]"
                Dim ReplaceText = "{Facility}"
                Dim Replaced As Boolean = WordFunctions.AddDocVariables(Document, FindText, ReplaceText)
                Document.Save(File.FullName, FormatType.Docx)
                Document.Close()
            Next

        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function
    Public Function OpenDocument(WordFilePath As String, word As Word.Application) As Word.Document
        Try
            Dim Missing As Object = System.Reflection.Missing.Value
            Dim File As FileInfo = New FileInfo(WordFilePath)



            Dim [readOnly] As Object = False
            If File IsNot Nothing Then
                Dim doc As Word.Document = word.Documents.Open(WordFilePath, Missing, [readOnly], Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing)
                '

                'Dim doc2 As Word.Document = word2.Documents.Open(WordFilePath, Missing, [readOnly], Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing)
                ' Dim doc2 As New Word.Document = word.ActiveDocument
                ' doc.Close()

                Return doc

            End If
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Private Function SaveDocument(WordFilePath As String, currentdoc As Word.Document) As String
        Try
            Dim File As FileInfo = New FileInfo(WordFilePath)
            Dim SaveDir = "C:\CCG\temp"
            Dim SaveFile As String = File.Name.Replace(File.Extension.ToString, "")
            SaveFile = $"{SaveFile}.docx"
            Dim SaveAsFile = $"{SaveDir}\{SaveFile}"
            If (Not System.IO.Directory.Exists(SaveDir)) Then System.IO.Directory.CreateDirectory(SaveDir)

            currentdoc.SaveAs(SaveAsFile, WdSaveFormat.wdFormatDocumentDefault)
            currentdoc.Close()
            Return SaveAsFile
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function


    Public Sub FindAndReplace(ByRef currentdoc As Word.Document)
        Try
            Dim Replacements As New Dictionary(Of String, String)
            'Replacements.Add("Client", "Client2")
            'Replacements.Add("LastName", "LastName2")
            Replacements.Add("[FACILITY NAME]", "Facility")


            For Each Replacement In Replacements

                Dim Missing As Object = System.Reflection.Missing.Value

                Dim matchCase As Object = False
                Dim matchWholeWord As Object = True
                Dim matchWildCards As Object = False
                Dim matchSoundsLike As Object = False
                Dim matchAllWordForms As Object = False
                Dim forward As Object = True
                Dim format As Object = False
                Dim matchKashida As Object = False
                Dim matchDiacritics As Object = False
                Dim matchAlefHamza As Object = False
                Dim matchControl As Object = False
                Dim read_only As Object = False
                Dim visible As Object = True
                Dim replace As Object = 2
                Dim wrap As Object = 1

                Dim intFound As Integer = 0
                Dim rng As Word.Range = currentdoc.Range
                rng.Find.ClearFormatting()

                'rng.Find.Replacement.ClearFormatting()
                'rng.Find.Replacement.Font.ColorIndex = Word.WdColorIndex.wdBlue


                rng.Find.Forward = True
                rng.Find.Text = Replacement.Key
                rng.Find.Execute(Missing, matchCase, matchWholeWord, matchWildCards, matchSoundsLike, matchAllWordForms, forward, wrap, format, Missing, Missing, matchKashida, matchDiacritics, matchAlefHamza, matchControl)
                '  rng.Find.Replacement.Font.ColorIndex = Word.WdColorIndex.wdDarkRed
                While rng.Find.Found
                    intFound += 1
                    currentdoc.Fields.Add(rng, WdFieldType.wdFieldDocVariable, Replacement.Value)
                    rng.Find.Execute(Missing, matchCase, matchWholeWord, matchWildCards, matchSoundsLike, matchAllWordForms, forward, wrap, format, Missing, Missing, matchKashida, matchDiacritics, matchAlefHamza, matchControl)
                End While

            Next
        Catch ex As Exception
            logger.Error(ex)

        End Try

    End Sub

    Private Sub FindAndReplace(ByRef currentRange As Word.Range, URLfound As List(Of String))
        Try
            'Dim Replacements As New Dictionary(Of String, String)
            'Replacements.Add("Client", "Client2")
            'Replacements.Add("LastName", "LastName2")

            For Each URL As String In URLfound

                Dim Missing As Object = System.Reflection.Missing.Value

                Dim matchCase As Object = False
                Dim matchWholeWord As Object = True
                Dim matchWildCards As Object = False
                Dim matchSoundsLike As Object = False
                Dim matchAllWordForms As Object = False
                Dim forward As Object = True
                Dim format As Object = False
                Dim matchKashida As Object = False
                Dim matchDiacritics As Object = False
                Dim matchAlefHamza As Object = False
                Dim matchControl As Object = False
                Dim read_only As Object = False
                Dim visible As Object = True
                Dim replace As Object = 2
                Dim wrap As Object = 1

                Dim intFound As Integer = 0
                Dim rng As Word.Range = currentRange
                rng.Find.ClearFormatting()

                'rng.Find.Replacement.ClearFormatting()
                'rng.Find.Replacement.Font.ColorIndex = Word.WdColorIndex.wdBlue


                rng.Find.Forward = True
                rng.Find.Text = URL
                rng.Find.Execute(Missing, matchCase, matchWholeWord, matchWildCards, matchSoundsLike, matchAllWordForms, forward, wrap, format, Missing, Missing, matchKashida, matchDiacritics, matchAlefHamza, matchControl)

                While rng.Find.Found
                    intFound += 1
                    currentRange.Fields.Add(rng, WdFieldType.wdFieldHyperlink, URL)
                    Exit While
                    ' rng.Find.Execute(Missing, matchCase, matchWholeWord, matchWildCards, matchSoundsLike, matchAllWordForms, forward, wrap, format, Missing, Missing, matchKashida, matchDiacritics, matchAlefHamza, matchControl)
                End While

            Next
        Catch ex As Exception
            logger.Error(ex)

        End Try

    End Sub

    Private Sub FindByColor(ByRef currentdoc As Word.Document, Color As Word.WdColorIndex)
        Try
            Dim Missing As Object = System.Reflection.Missing.Value

            Dim matchCase As Object = False
            Dim matchWholeWord As Object = False
            Dim matchWildCards As Object = False
            Dim matchSoundsLike As Object = False
            Dim matchAllWordForms As Object = False
            Dim forward As Object = True
            Dim format As Object = True
            Dim matchKashida As Object = False
            Dim matchDiacritics As Object = False
            Dim matchAlefHamza As Object = False
            Dim matchControl As Object = False
            Dim read_only As Object = False
            Dim visible As Object = True
            Dim replace As Object = 2
            Dim wrap As Object = 1



            ' currentdoc.Selection.Find.Font.Color = Color
            'For Each aPar As Word.Paragraph In currentdoc.Paragraphs
            '    Dim rng As Word.Range = aPar.Range
            '    Dim sText As String = rng.Text

            '    Dim d = rng.Sentences.Count
            '    For Each sentance As Word.Sentences In rng.Sentences
            '        Dim rng2 As Word.Range = sentance.Pa
            '        FindSentenceByColor()
            '    Next




            'Next



            'Dim intFound As Integer = 0
            'Dim rng As Word.Range = currentdoc.Range
            'rng.Find.ClearFormatting()

            ''rng.Find.Replacement.ClearFormatting()
            ''rng.Find.Replacement.Font.ColorIndex = Word.WdColorIndex.wdBlue


            'rng.Find.Forward = True
            'rng.Find.Text = String.Empty
            'rng.Find.Font.ColorIndex = Color
            'rng.Find.Execute(Missing, matchCase, matchWholeWord, matchWildCards, matchSoundsLike, matchAllWordForms, forward, wrap, Format, Missing, Missing, matchKashida, matchDiacritics, matchAlefHamza, matchControl)

            ''While (Word.Selection.Find.Execute(ref findStr))
            '    hasFound = rng.Find.Execute(replace: Word.WdReplace.wdReplaceAll);
            'While rng.Find.Found()
            '    intFound += 1
            '    If rng.Text = Nothing Then
            '        Exit While
            '    End If
            '    rng.Delete()
            '    'Exit While
            '    ' Dim rng2 = rng





            '    'rng.Find.ClearFormatting()
            '    'rng.Find.Forward = True
            '    'rng.Find.Text = String.Empty
            '    'rng.Find.Font.ColorIndex = Color

            '    '    currentdoc.Fields.Add(rng, WdFieldType.wdFieldDocVariable, Replacement.Value)
            '    rng.Find.Execute(Missing, matchCase, matchWholeWord, matchWildCards, matchSoundsLike, matchAllWordForms, forward, wrap, Format, Missing, Missing, matchKashida, matchDiacritics, matchAlefHamza, matchControl)
            'End While





            '    ' Dim rngDoc As Word.Range = currentdoc.Content
            '    '  Dim rngFind As Word.Range = rngDoc.Duplicate
            '    Dim bFound As Boolean = True
            '    ' Dim oCollapseEnd = Word.WdCollapseDirection.wdCollapseEnd

            '    While bFound
            '    bFound = rngFind.Find.Execute()

            '    If bFound Then
            '        rngFind.Collapse(oCollapseEnd)
            '        rngFind.[End] = rngDoc.[End]
            '    End If
            'End While



            ' Next
        Catch ex As Exception
            logger.Error(ex)

        End Try

    End Sub

    Private Sub FindRangeByColor(ByRef currentRange As Word.Range, Color As Word.WdColorIndex)
        Try
            Dim Missing As Object = System.Reflection.Missing.Value

            Dim matchCase As Object = False
            Dim matchWholeWord As Object = False
            Dim matchWildCards As Object = False
            Dim matchSoundsLike As Object = False
            Dim matchAllWordForms As Object = False
            Dim forward As Object = True
            Dim format As Object = True
            Dim matchKashida As Object = False
            Dim matchDiacritics As Object = False
            Dim matchAlefHamza As Object = False
            Dim matchControl As Object = False
            Dim read_only As Object = False
            Dim visible As Object = True
            Dim replace As Object = 2
            Dim wrap As Object = 1


            Dim intFound As Integer = 0
            Dim rng As Word.Range = currentRange
            rng.Find.ClearFormatting()

            rng.Find.Forward = True
            rng.Find.Text = String.Empty
            rng.Find.Font.ColorIndex = Color
            rng.Find.Execute(Missing, matchCase, matchWholeWord, matchWildCards, matchSoundsLike, matchAllWordForms, forward, wrap, format, Missing, Missing, matchKashida, matchDiacritics, matchAlefHamza, matchControl)

            If rng.Find.Found() = True Then
                rng.Delete()
            End If



        Catch ex As Exception
            logger.Error(ex)

        End Try

    End Sub


    Private Sub FindRangeByFont(ByRef currentRange As Word.Range, currentdoc As Word.Document, FontName As String)
        Try
            Dim HyperLink2 As Style = Nothing
            For Each objStyle As Style In currentdoc.Styles
                If objStyle.NameLocal = "Hyperlink2" Then
                    HyperLink2 = objStyle
                    Exit For
                End If
            Next objStyle

            With currentRange.Find
                .ClearFormatting()
                '  .Font.Name = FontName
                .Replacement.Font.Color = WdColor.wdColorGreen
                .Style = HyperLink2
                .Execute(FindText:="", ReplaceWith:="", Format:=True, Forward:=True, Replace:=Word.WdReplace.wdReplaceAll)
            End With

        Catch ex As Exception
            logger.Error(ex)

        End Try

    End Sub

    Private Sub FindRangeByFont2(ByRef currentRange As Word.Range, currentdoc As Word.Document, FontName As String)
        Try
            'Dim HyperLink2 As Style = Nothing
            'For Each objStyle As Style In currentdoc.Styles
            '    If objStyle.NameLocal = "Hyperlink2" Then
            '        HyperLink2 = objStyle
            '        Exit For
            '    End If
            'Next objStyle


            For Each Field As Field In currentdoc.Fields
                'Dim CurrentStyle As Style = Field.Result.Style
                If Field.Result.Font.Name = "Aharoni" Then
                    Field.Result.Delete()
                End If
            Next


            Dim WordRanges As New List(Of WordRange)
            Dim WordCounter As Integer = 0
            For Each CurrentWord As Range In currentRange.Words
                WordCounter += 1
                Dim CurrentText As String = CurrentWord.Text
                '  Dim CurrentStyle As Style = CurrentWord.Style

                If CurrentWord.Font.Name = "Aharoni" Then
                    WordRanges.Add(New WordRange With {.Range = CurrentWord, .Deleted = False})
                End If
            Next CurrentWord

            Dim RangeCounter As Integer = 0
            For Each CurrentRange2 As WordRange In WordRanges
                RangeCounter += 1
                Dim CurrentText As String = CurrentRange2.Range.Text

                If CurrentText = ChrW(19) & " " Or CurrentText = ChrW(21) & "." Or CurrentText = ChrW(21) & ". " Or CurrentText = ChrW(21) & " " Or CurrentText = ChrW(21) Or CurrentText = ChrW(21) & ", " Or CurrentText = ChrW(21) & "  " Then
                    '  CurrentRange2.Range.Font.Color = Word.WdColor.wdColorGreen
                Else
                    CurrentRange2.Range.Delete()
                    ' CurrentRange2.Deleted = True
                End If
            Next


            'Dim WordCounter As Integer = 0
            'For Each CurrentWord As Range In currentRange.Words
            '    WordCounter += 1
            '    Dim CurrentText As String = CurrentWord.Text
            '    Dim CurrentStyle As Style = CurrentWord.Style

            '    If CurrentWord.Font.Name = "Aharoni" Then
            '        If CurrentText = ChrW(19) & " " Or CurrentText = ChrW(21) & "." Or CurrentText = ChrW(21) & ". " Or CurrentText = ChrW(21) & " " Or CurrentText = ChrW(21) Or CurrentText = ChrW(21) & ", " Or CurrentText = ChrW(21) & "  " Or CurrentText = ChrW(19) & "://" Or CurrentText = ChrW(19) & "://." Then
            '            '  CurrentRange2.Range.Font.Color = Word.WdColor.wdColorGreen
            '        Else
            '            CurrentWord.Delete()
            '        End If
            '    End If
            'Next CurrentWord


            Dim intFound As Integer = 0
        Catch ex As Exception
            logger.Error(ex)
        End Try

    End Sub


    Public Sub GetSections(ByRef currentdoc As Word.Document)
        Try

            Dim Sections As New List(Of Word.Section)

            For Each sec As Word.Section In currentdoc.Sections
                Sections.Add(sec)
                Dim secOrientation As Word.WdOrientation = sec.PageSetup.Orientation
                Dim secType As Word.WdSectionStart = sec.PageSetup.SectionStart
            Next
            Dim SecCount = Sections.Count


        Catch ex As Exception
            logger.Error(ex)

        End Try

    End Sub

    Public Function GetSectionChapters(ByRef Document As Word.Document, State As String) As List(Of SectionIndex)
        Try

            Dim Fields2 As New List(Of Word.Field)
            Dim FieldsText As New List(Of String)
            Dim FieldsTextValue As New List(Of String)
            Dim SectionCounter As Integer = 0
            Dim FooterCounter As Integer = 0
            Dim SectionIndexes As New List(Of SectionIndex)
            Dim SectionToExtract As Integer = 5

            Dim SectionIndexs As List(Of CCGData.CCGData.SectionIndex) = DataRepository.GetSectionIndexForState(State)


            For Each section As Word.Section In Document.Sections
                Document.TrackRevisions = False
                SectionCounter += 1

                Dim SectionIndex As New SectionIndex
                'SectionIndex.FromIndex = section.Index

                'Dim headers As Word.HeadersFooters = section.Headers
                '    For Each header As Word.HeaderFooter In headers
                '        Dim fields As Fields = header.Range.Fields

                '        For Each field As Field In fields
                '            If field.Type = WdFieldType.wdFieldDate Then
                '                'field.[Select]()
                '                'field.Delete()
                '                ' wordApplication.Selection.TypeText("[DATE]")
                '            ElseIf field.Type = WdFieldType.wdFieldFileName Then
                '                'field.[Select]()
                '                'field.Delete()
                '                'wordApplication.Selection.TypeText("[FILE NAME]")
                '            End If
                '        Next
                '    Next

                Dim footers As Word.HeadersFooters = section.Footers
                For Each footer As Word.HeaderFooter In footers
                    If footer.Range.Text.Contains("CCG") Then
                        Dim FooterText As String = footer.Range.Text
                        Dim StartPos = FooterText.IndexOf("CCG")
                        Dim EndPos = FooterText.IndexOf("  ", StartPos)
                        Dim Chapter As String = FooterText.Substring(StartPos, EndPos - StartPos)
                        SectionIndex.Title = Chapter
                        SectionIndexes.Add(SectionIndex)
                        Exit For
                    End If
                Next
            Next


            Dim SecCount = Fields2.Count
            Return SectionIndexes
        Catch ex As Exception
            logger.Error(ex)

        End Try

    End Function

    Public Function GetSectionChapters(ByRef Document As WordDocument) As List(Of SectionIndex)
        Try

            ''Dim TextFound As New List(Of String)
            ''Dim SectionFound As New List(Of WSection)
            ''Dim SectionFoundIndex As New List(Of Integer)


            'Dim SectionIndexes As New List(Of SectionIndex)
            'Dim Selections As TextSelection() = Document.FindAll("CCG", True, False)
            'Dim TextSelections As List(Of TextSelection) = Selections.ToList



            'For Each Selection As TextSelection In TextSelections
            '    Dim SelectionRange As WTextRange() = Selection.GetRanges

            '    If SelectionRange.FirstOrDefault.Owner.Owner.Owner.Owner IsNot Nothing Then
            '        If SelectionRange.FirstOrDefault.Owner.Owner.Owner.Owner.EntityType = EntityType.HeaderFooter Then
            '            If SelectionRange.FirstOrDefault.Owner.Owner.Owner.Owner.Owner.EntityType = EntityType.Section Then
            '                Dim SelectionSection As WSection = SelectionRange.FirstOrDefault.Owner.Owner.Owner.Owner.Owner.Clone
            '                Dim SectionIndex = PropertyHelper.GetPrivateFieldValue(Of Integer)(SelectionSection, "Index")
            '                'SectionFound.Add(SelectionSection)
            '                'SectionFoundIndex.Add(SectionIndex)
            '                If SelectionRange.FirstOrDefault.Owner.EntityType = EntityType.Paragraph Then
            '                    Dim SelectionRangeOwner As WParagraph = SelectionRange.FirstOrDefault.Owner.Clone

            '                    Dim FooterText = SelectionRangeOwner.Text
            '                    Dim Title = GetCCGIndex(FooterText)
            '                    If Title IsNot Nothing Then
            '                        Dim Root = Title.Replace("CCG ", "").Substring(0, 3)
            '                        SectionIndexes.Add(New SectionIndex With {.FromIndex = SectionIndex, .Title = Title, .Root = Root})
            '                    End If



            '                    'Dim StartPos = FooterText.IndexOf("CCG")
            '                    'If StartPos > 0 Then
            '                    '    Dim EndPos As Integer = 0
            '                    '    EndPos = FooterText.IndexOf("  ", StartPos)
            '                    '    If EndPos = -1 Then EndPos = FooterText.IndexOf(" ", StartPos)
            '                    '    Dim Chapter As String = FooterText.Substring(StartPos, EndPos - StartPos)
            '                    '    Dim Root = Chapter.Replace("CCG ", "").Substring(0, 3)
            '                    '    SectionIndexes.Add(New SectionIndex With {.Index = SectionIndex, .Title = Chapter, .Root = Root})
            '                    'End If
            '                End If
            '            End If
            '        End If
            '    End If

            '    If SelectionRange.FirstOrDefault.Owner.Owner.Owner.Owner.Owner IsNot Nothing Then
            '        If SelectionRange.FirstOrDefault.Owner.Owner.Owner.Owner.Owner.EntityType = EntityType.HeaderFooter Then
            '            If SelectionRange.FirstOrDefault.Owner.Owner.Owner.Owner.Owner.Owner.EntityType = EntityType.Section Then
            '                Dim SelectionSection As WSection = SelectionRange.FirstOrDefault.Owner.Owner.Owner.Owner.Owner.Owner.Clone
            '                Dim SectionIndex = PropertyHelper.GetPrivateFieldValue(Of Integer)(SelectionSection, "Index")
            '                'SectionFound.Add(SelectionSection)
            '                'SectionFoundIndex.Add(SectionIndex)
            '                If SelectionRange.FirstOrDefault.Owner.EntityType = EntityType.Paragraph Then
            '                    Dim SelectionRangeOwner As WParagraph = SelectionRange.FirstOrDefault.Owner.Clone

            '                    Dim FooterText = SelectionRangeOwner.Text
            '                    Dim Title = GetCCGIndex(FooterText)
            '                    If Title IsNot Nothing Then
            '                        Dim Root = Title.Replace("CCG ", "").Substring(0, 3)
            '                        SectionIndexes.Add(New SectionIndex With {.FromIndex = SectionIndex, .Title = Title, .Root = Root})
            '                    End If
            '                    'Dim StartPos = FooterText.IndexOf("CCG")
            '                    'If StartPos > -1 Then
            '                    '    Dim EndPos As Integer = 0
            '                    '    EndPos = FooterText.IndexOf("  ", StartPos)
            '                    '    If EndPos = -1 Then EndPos = FooterText.IndexOf(" ", StartPos)
            '                    '    Dim Chapter As String = FooterText.Substring(StartPos, EndPos - StartPos)
            '                    '    Dim Root = Chapter.Replace("CCG ", "").Substring(0, 3)
            '                    '    SectionIndexes.Add(New SectionIndex With {.Index = SectionIndex, .Title = Chapter, .Root = Root})
            '                    'End If
            '                End If
            '            End If
            '        End If
            '    End If
            'Next

            'Return SectionIndexes
        Catch ex As Exception
            logger.Error(ex)

        End Try

    End Function
    Public Function GetSectionHeader(SelectionSection As WSection) As String
        Try


            Dim SelectionParagraphs As WParagraphCollection = SelectionSection.Paragraphs
            Dim SectionHeader As String = String.Empty

            If SelectionParagraphs.Count > 0 Then

                For Each Para As WParagraph In SelectionParagraphs
                    Dim ContainsField As Boolean = False
                    Dim ChildEntities As EntityCollection = Para.ChildEntities
                    If ChildEntities.Count > 0 Then
                        For Each Entitity As Entity In ChildEntities
                            If Entitity.EntityType = EntityType.Field Then
                                ContainsField = True
                                Exit For
                            End If
                        Next

                        If ContainsField = False Then
                            For Each Entitity As Entity In ChildEntities
                                If Entitity.EntityType = EntityType.TextRange Then
                                    SectionHeader = Para.Text
                                    Return SectionHeader
                                    Exit Function
                                End If
                            Next

                            'If Para.ChildEntities.FirstItem.EntityType = EntityType.TextRange Then
                            '    SectionHeader = Para.Text
                            '    Exit For
                            'End If
                        End If
                    End If
                Next


                'For Each Para As WParagraph In SelectionParagraphs
                '        'If SelectionParagraphs.Item(0).ChildEntities.FirstItem.EntityType = EntityType.Field Then
                '        'End If

                '        If Para.ChildEntities.FirstItem.EntityType = EntityType.TextRange Then
                '            SectionHeader = Para.Text
                '            Exit For
                '        End If


                '    Next
                'End If
            End If
            Return SectionHeader

        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function
    Public Function GetSectionHeader4(Document As WordDocument, SectionIndexes As List(Of SectionIndex)) As List(Of SectionIndex)

        Try
            Dim Tocs As List(Of SectionIndex)
            Tocs = SectionIndexes.Where(Function(s) s.Title.Length = 7).ToList
            ' Dim Selections As TextSelection()



            For Each Toc As SectionIndex In Tocs
                Dim SectionTOC As SectionIndex = SectionIndexes.Where(Function(s) s.Title = Toc.Title).SingleOrDefault
                If SectionTOC IsNot Nothing Then SectionTOC.Header = $"{Toc.Title} Table Of Contents"

                For Each Section As compliancecg.Section In Toc.Sections
                    For Index = Section.FromIndex To Section.ToIndex
                        Dim SectionDocument As WordDocument = CopyDocSection4(Document, Index)
                        Dim Selections As TextSelection() = SectionDocument.FindAll("CCG 00", True, False)

                        For Each Selection As TextSelection In Selections
                            Dim SelectionRange As WTextRange() = Selection.GetRanges


                            If SelectionRange.FirstOrDefault.Owner IsNot Nothing Then

                                If SelectionRange.FirstOrDefault.Owner.EntityType = EntityType.Paragraph Then
                                    Dim SelectionSection As WParagraph = SelectionRange.FirstOrDefault.Owner.Clone
                                    Dim ChildEntities = SelectionSection.ChildEntities

                                    'If ChildEntities.LastItem.EntityType = EntityType.TextRange Then
                                    '    Dim LastItem As WTextRange = ChildEntities.LastItem.Clone
                                    'Dim Title As String = LastItem.Text
                                    Dim Line As String = SelectionSection.Text

                                    If Line.Contains("CCG 00") = True Then
                                        Dim StartPos = Line.IndexOf("CCG 00")
                                        Dim LineLength = Line.Length

                                        Dim charArr = Line.ToCharArray()
                                        Dim EndPos = StartPos + 6
                                        For Each ch As Char In charArr.Skip(StartPos + 6)
                                            Dim Ascii = Asc(ch)
                                            If Ascii = 32 Then Exit For
                                            EndPos += 1
                                        Next

                                        Dim Header = Line.Substring(StartPos, Line.Length - StartPos)
                                        Dim SectionTitle = Line.Substring(StartPos, EndPos - StartPos)

                                        If Header.Length > 7 Then
                                            Dim Sections As List(Of SectionIndex) = SectionIndexes.Where(Function(s) s.Title = SectionTitle And s.Header Is Nothing).ToList
                                            If Sections.Count > 0 Then
                                                For Each Section2 As SectionIndex In Sections
                                                    Section2.Header = Header
                                                Next
                                            End If
                                            If Sections.Count = 0 Then
                                                Header = Header
                                            End If
                                        End If

                                    End If

                                End If


                            End If
                            'Dim ChildEntities = SelectionSection.ChildEntities
                            'Dim SelectionText As String = DirectCast(SelectionRange.FirstOrDefault.Owner, Syncfusion.DocIO.DLS.WParagraph).[Text]


                            'If SelectionRange.FirstOrDefault.Owner.EntityType = WField Then

                            'End If


                            'If SelectionRange.FirstOrDefault.Owner.Owner.Owner.Owner.EntityType = EntityType.HeaderFooter Then
                            '    Dim SelectionSection As WSection = SelectionRange.FirstOrDefault.Owner.Clone
                            'End If
                            'End If



                            ' Dim TextOwner = Selection
                        Next
                        Dim SelCount = Selections.Count

                    Next
                Next



            Next

            Dim SectionIndexesNull = SectionIndexes.Where(Function(s) s.Header = Nothing).ToList
            Return SectionIndexes
            'Dim TocsCount As Integer = Tocs.Count
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetSectionHeader2(SelectionSection As WSection) As String
        Try


            Dim SelectionParagraphs As WParagraphCollection = SelectionSection.Paragraphs
            Dim SectionHeader As String = String.Empty

            If SelectionParagraphs.Count > 0 Then

                For Each Para As WParagraph In SelectionParagraphs
                    Dim ContainsField As Boolean = False
                    Dim ChildEntities As EntityCollection = Para.ChildEntities
                    If ChildEntities.Count > 0 Then
                        For Each Entitity As Entity In ChildEntities
                            If Entitity.EntityType = EntityType.Field Then
                                ContainsField = True
                                Exit For
                            End If
                        Next

                        If ContainsField = False Then
                            For Each Entitity As Entity In ChildEntities
                                If Entitity.EntityType = EntityType.TextRange Then
                                    SectionHeader = Para.Text
                                    Return SectionHeader
                                    Exit Function
                                End If
                            Next

                            'If Para.ChildEntities.FirstItem.EntityType = EntityType.TextRange Then
                            '    SectionHeader = Para.Text
                            '    Exit For
                            'End If
                        End If
                    End If
                Next


                'For Each Para As WParagraph In SelectionParagraphs
                '        'If SelectionParagraphs.Item(0).ChildEntities.FirstItem.EntityType = EntityType.Field Then
                '        'End If

                '        If Para.ChildEntities.FirstItem.EntityType = EntityType.TextRange Then
                '            SectionHeader = Para.Text
                '            Exit For
                '        End If


                '    Next
                'End If
            End If
            Return SectionHeader

        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function
    Public Function GetSectionTitle(ByRef Document As WordDocument, State As String) As List(Of SectionIndex)
        Try

            Dim SectionIndexes As New List(Of compliancecg.SectionIndex)
            Dim Selections As TextSelection() = Document.FindAll("CCG", True, False)
            Dim TextSelections As List(Of TextSelection) = Selections.ToList

            Dim SectionIndexsforState As List(Of CCGData.CCGData.SectionIndex) = DataRepository.GetSectionIndexForState(State)

            Dim SelectionCounter As Integer = 0
            For Each Selection As TextSelection In TextSelections
                SelectionCounter += 1
                Dim SelectionRange As WTextRange() = Selection.GetRanges
                Dim SectionIndexes2 As New List(Of compliancecg.SectionIndex)

                If SelectionRange.FirstOrDefault.Owner.Owner IsNot Nothing Then
                    If SelectionRange.FirstOrDefault.Owner.Owner.EntityType = EntityType.HeaderFooter Then
                        SectionIndexes2 = GetDocOwnerInfo(SelectionRange, SelectionRange.FirstOrDefault.Owner.Owner.Owner, SectionIndexsforState)
                    End If
                End If

                If SelectionRange.FirstOrDefault.Owner.Owner.Owner.Owner IsNot Nothing Then
                    If SelectionRange.FirstOrDefault.Owner.Owner.Owner.Owner.EntityType = EntityType.HeaderFooter Then
                        SectionIndexes2 = GetDocOwnerInfo(SelectionRange, SelectionRange.FirstOrDefault.Owner.Owner.Owner.Owner.Owner, SectionIndexsforState)
                    End If
                End If

                If SelectionRange.FirstOrDefault.Owner.Owner.Owner.Owner.Owner IsNot Nothing Then
                    If SelectionRange.FirstOrDefault.Owner.Owner.Owner.Owner.Owner.EntityType = EntityType.HeaderFooter Then
                        SectionIndexes2 = GetDocOwnerInfo(SelectionRange, SelectionRange.FirstOrDefault.Owner.Owner.Owner.Owner.Owner.Owner, SectionIndexsforState)
                    End If
                End If

                If SectionIndexes2.Count > 0 Then
                    For Each Index As compliancecg.SectionIndex In SectionIndexes2
                        If Index.Root > 0 Then
                            If SectionIndexes.Where(Function(i) i.Title = Index.Title).Any = False Then
                                SectionIndexes.Add(Index)
                            End If

                            'If SectionIndexes.Where(function(i) i.Root=SectionIndex.Root and i.Index=SectionIndex.Index).Any=false Then
                            '    SectionIndexes.Add(SectionIndex)
                            'End If
                            'For Each Section As Sections In Index.Sections
                            '    For Index2 = Section.FromIndex To Section.ToIndex
                            '        If SectionIndexes.Where(Function(i) i.Title = Index.Title And i.FromIndex = Index2).Any = False Then
                            '            SectionIndexes.Add(Index)
                            '        End If
                            '    Next
                            'Next

                        End If
                    Next

                End If
            Next

            SectionIndexes = GetSectionHeader4(Document, SectionIndexes)
            'For x = SectionIndexes.Count - 1 To 0 Step -1
            '    Dim Needed = SectionIndexes(x)
            '    Dim IndexDelete = SectionIndexes.Where(Function(s) s.FromIndex = s.ToIndex).SingleOrDefault

            '    If IndexDelete IsNot Nothing Then
            '        SectionIndexes.RemoveAt(x)
            '    End If
            'Next


            Dim RemoveItems As New List(Of compliancecg.SectionIndex)
            For Each Index As compliancecg.SectionIndex In SectionIndexes
                Dim Sections = Index.Sections

                For Each Section As Section In Index.Sections
                    If Section.FromIndex < Section.ToIndex Then
                        Dim IndexDelete = SectionIndexes.Where(Function(i) (i.Sections.Where(Function(s) s.FromIndex >= Section.FromIndex).Any) And (i.Sections.Where(Function(s) s.FromIndex <= Section.ToIndex).Any) And Index.Title <> i.Title).ToList
                        RemoveItems.AddRange(IndexDelete)
                    End If
                Next

                'If Index.Sections.Then Then
                '    'Dim IndexDelete = SectionIndexes.Where(Function(s) s.FromIndex > Index.FromIndex And s.FromIndex <= Index.ToIndex).ToList
                '    'RemoveItems.AddRange(IndexDelete)
                'End If
            Next
            For Each Index In RemoveItems
                SectionIndexes.Remove(Index)
            Next


            Return SectionIndexes
        Catch ex As Exception
            logger.Error(ex)

        End Try

    End Function

    Public Function GetSectionTitle2(ByRef Document As WordDocument) As List(Of SectionIndex)
        Try

            'Dim SectionIndexes As New List(Of SectionIndex)

            'For Each Bookmark As Syncfusion.DocIO.DLS.Bookmark In Document.Bookmarks
            '    If Bookmark.Name.Substring(0, 3) = "CCG" Then
            '        If Bookmark.BookmarkStart.Owner.Owner.EntityType = EntityType.TextBody Then
            '            Dim ChildEntities As BodyItemCollection = DirectCast(Bookmark.BookmarkStart.Owner.Owner, Syncfusion.DocIO.DLS.WTextBody).ChildEntities
            '            If ChildEntities.Count > 0 Then
            '                For Each Child In ChildEntities
            '                    If Child.EntityType = EntityType.Paragraph Then
            '                        Dim Paragraph As Syncfusion.DocIO.DLS.WParagraph = Child
            '                        If Paragraph.BreakCharacterFormat.Bold = True And Paragraph.BreakCharacterFormat.FontSize >= 12 Then
            '                            If Paragraph.Text.Length > 0 Then
            '                                If Paragraph.Text.StartsWith("DOCVARIABLE") = False Then
            '                                    'If Paragraph.Text.Substring(0, 11) <> "DOCVARIABLE" Then
            '                                    Dim Title = Bookmark.Name.Replace("CCG", "CCG ")
            '                                    Dim Root = Title.Replace("CCG ", "").Substring(0, 3)
            '                                    Dim SectionHeader As String = $"{Title} {Paragraph.Text}"
            '                                    Dim SectionIndex As Integer = Title.Substring(4, 3)
            '                                    Dim SectionIndex2 As SectionIndex = New SectionIndex With {.FromIndex = SectionIndex, .Title = Title, .Root = Root, .Header = SectionHeader, .Bookmark = Bookmark.Name}
            '                                    SectionIndexes.Add(SectionIndex2)
            '                                    Exit For
            '                                End If
            '                            End If
            '                        End If
            '                    End If
            '                Next
            '            End If
            '        End If
            '    End If
            'Next


            'Return SectionIndexes
        Catch ex As Exception
            logger.Error(ex)

        End Try

    End Function

    Private Function GetDocOwnerInfo(SelectionRange As WTextRange(), SelectionEntity As Syncfusion.DocIO.DLS.Entity, SectionIndexesforState As List(Of CCGData.CCGData.SectionIndex)) As List(Of compliancecg.SectionIndex)
        Try

            If SelectionEntity.EntityType = EntityType.Section Then
                Dim SelectionSection As WSection = SelectionEntity.Clone
                Dim SectionIndex = PropertyHelper.GetPrivateFieldValue(Of Integer)(SelectionSection, "Index")
                Dim SectionHeader = GetSectionHeader(SelectionSection)
                Dim SectionIndexes As New List(Of compliancecg.SectionIndex)

                SectionHeader = SectionHeader.ToTitleCase
                If SectionIndex > -1 Then
                    If SelectionRange.FirstOrDefault.Owner.EntityType = EntityType.Paragraph Then
                        Dim SelectionRangeOwner As WParagraph = SelectionRange.FirstOrDefault.Owner.Clone

                        Dim FooterText = SelectionRangeOwner.Text
                        Dim Title = GetCCGIndex(FooterText)


                        Dim SectionIndexforState As New List(Of CCGData.CCGData.SectionIndex)

                        If Title IsNot Nothing Then
                            Dim Title2 = Title.Remove(0, 6)
                            SectionHeader = $"{Title} {SectionHeader}"
                            Dim Root = Title.Replace("CCG ", "").Substring(0, 3)
                            Dim SectionIndex2 As compliancecg.SectionIndex = New SectionIndex With {.Title = Title, .Root = Root}

                            Dim Sections As New List(Of compliancecg.Section)
                            SectionIndexforState = SectionIndexesforState.Where(Function(s) s.PolicyNo = Title2).ToList
                            If SectionIndexforState.Count > 0 Then
                                For Each Index As CCGData.CCGData.SectionIndex In SectionIndexforState
                                    Dim Section As New compliancecg.Section
                                    Section.FromIndex = Index.SectionFrom - 1
                                    Section.ToIndex = Index.SectionTo - 1
                                    Sections.Add(Section)
                                Next
                            End If
                            If SectionIndexforState.Count = 0 Then
                                Dim Section As New compliancecg.Section
                                Section.FromIndex = SectionIndex
                                Section.ToIndex = SectionIndex
                                Sections.Add(Section)
                            End If

                            If Sections.Count > 0 Then SectionIndex2.Sections.AddRange(Sections)
                            SectionIndexes.Add(SectionIndex2)


                            'If SectionIndexforState.Count > 0 Then
                            '    For Each Index As CCGData.CCGData.SectionIndex In SectionIndexforState
                            '        SectionIndexFrom = Index.SectionFrom
                            '        SectionIndexTo = Index.SectionTo

                            '    Next
                            'End If
                            'If SectionIndexforState.Count = 0 Then
                            '    SectionIndexFrom = SectionIndex
                            '    SectionIndexTo = SectionIndex
                            '    Dim SectionIndex2 As compliancecg.SectionIndex = New SectionIndex With {.FromIndex = SectionIndexFrom, .ToIndex = SectionIndexTo, .Title = Title, .Root = Root}
                            '    SectionIndexes.Add(SectionIndex2)
                            'End If


                            'Dim SectionIndex2 As SectionIndex = New SectionIndex With {.Index = SectionIndex, .Title = Title, .Root = Root, .Header = SectionHeader}

                            ' SectionIndexes.Add(New SectionIndex With {.Index = SectionIndex, .Title = Title, .Root = Root, .Header = SectionHeader})
                        End If
                    End If
                End If
                Return SectionIndexes
            End If



            'For Each BookMark As Word.Bookmark In MasterDocument.Bookmarks
            '    BookMarkNames.Add(BookMark.Name)
            'Next

        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    'Public Shared Function GetCCGIndex(FileName As String) As String
    '    Try
    '        Dim StartPos = FileName.IndexOf("CCG")
    '        If StartPos > -1 Then
    '            Dim EndPos As Integer = 0
    '            EndPos = FileName.IndexOf("  ", StartPos + 4)
    '            If EndPos = -1 Then EndPos = FileName.IndexOf(" ", StartPos + 4)

    '            Dim Index As String = FileName.Substring(StartPos, EndPos - StartPos)
    '            Return Index
    '        End If
    '  Catch ex As Exception
    'logger.Error(ex)

    '    End Try
    'End Function

    Public Shared Function GetCCGIndex(FileName As String) As String
        Try
            Dim Ascii As Integer
            Dim StartPos = FileName.IndexOf("CCG 00")
            Dim EndPos As Integer = 0 ' = 9
            If StartPos > -1 Then
                Dim TestDigit As Integer = StartPos + 7
                Dim charArr = FileName.ToCharArray()
                Ascii = Asc(charArr(TestDigit - 1))


                If Ascii >= 40 And Ascii <= 57 Then

                    EndPos = StartPos + 6
                    For Each ch As Char In charArr.Skip(StartPos + 6)
                        Ascii = Asc(ch)
                        If Ascii = 32 Then Exit For
                        EndPos += 1
                    Next

                    'Dim WordLength = FileName.Length
                    'Dim StartPostoLastPos = WordLength - StartPos

                    'If 9 > StartPostoLastPos Then
                    '    Ascii = Asc(FileName.Substring(StartPos + StartPostoLastPos - 1, 1))
                    'End If

                    'If 9 <= StartPostoLastPos Then
                    '    Ascii = Asc(FileName.Substring(StartPos + 9 - 1, 1))
                    'End If

                    ''Dim EndPos As Integer = 0
                    ''EndPos = FileName.IndexOf("  ", StartPos + 4)
                    ''If EndPos = -1 Then EndPos = FileName.IndexOf(" ", StartPos + 4)
                    '' Dim asciis As Byte() = System.Text.Encoding.ASCII.GetBytes(FileName.Substring(StartPos + 9, 1))
                    ''Ascii = Asc(FileName.Substring(StartPos + 9, 1))

                    'Select Case Ascii
                    '    Case 40 To 57, 65 To 90, 97 To 122
                    '        If 9 > StartPostoLastPos Then
                    '            EndPos = StartPostoLastPos
                    '        End If
                    '        If 9 <= StartPostoLastPos Then
                    '            'EndPos += 1
                    '        End If

                    '    Case Else
                    '        EndPos = EndPos
                    'End Select

                    Dim Index As String = FileName.Substring(StartPos, EndPos - StartPos)
                    Return Index
                End If
            End If
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function
    Public Function GetDocIndexFromPath(Path As String) As String
        Try
            Dim Index As String = String.Empty

            Dim StartPos = Path.IndexOf("CCG ")

            If StartPos > -1 Then
                Dim EndPos = Path.IndexOf(" ", StartPos)
                Dim EndPos2 = Path.IndexOf(" ", EndPos + 1)
                Index = Path.Substring(StartPos, EndPos2 - StartPos)
            End If
            Return Index
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function
    Public Shared Function ReadPages(ByRef Document As Word.Document) As IEnumerable(Of String)
        Try
            Dim pageStrings As ICollection(Of String) = New List(Of String)()
            Dim app As Word.Application = New Word.Application()
            ' Dim doc As Document = app.Documents.Open(filePath)
            Dim pageCount As Long = Document.ComputeStatistics(Word.WdStatistic.wdStatisticPages)
            Dim lastPageEnd As Integer = 0

            Dim pageBreakRange As Range = app.Selection.GoTo(Word.WdGoToItem.wdGoToSection, 5)
            Dim currentPageText As String = Document.Range(lastPageEnd, pageBreakRange.[End]).Text
            'For i As Long = 0 To pageCount - 1
            '    Dim pageBreakRange As Range = Document.Selection.GoTo(Word.WdGoToItem.wdGoToSection, 5)
            '    Dim currentPageText As String = Document.Range(lastPageEnd, pageBreakRange.[End]).Text
            '    lastPageEnd = pageBreakRange.[End]
            '    pageStrings.Add(currentPageText)
            'Next

            Return pageStrings
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function


    Public Shared Function ReadPages2(ByRef Document As Word.Document) As IEnumerable(Of String)
        Try
            Dim word As Word.Application = New Word.Application()
            'Dim wordFiles As FileInfo() = dirInfo.GetFiles("*.doc")

            Dim oMissing As Object = System.Reflection.Missing.Value
            Dim what As Object = WdGoToItem.wdGoToPage
            Dim which As Object = WdGoToDirection.wdGoToFirst
            Dim count As Object = 1
            Dim startRange As Range = word.Selection.[GoTo](what, which, count, oMissing)
            Dim count2 As Object = CInt(count) + 1
            Dim endRange As Range = word.Selection.[GoTo](what, which, count2, oMissing)
            endRange.SetRange(startRange.Start, endRange.[End] - 1)
            endRange.[Select]()

            word.Selection.Copy()
            word.Documents.Add()
            word.Selection.Paste()
            '  Dim outputFileName As Object = wordFile.FullName.Replace(".doc", ".pdf")
            ' Dim fileFormat As Object = WdSaveFormat.wdFormatPDF
            '   Word.ActiveDocument.SaveAs(outputFileName, fileFormat, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing)
            ' Dim saveChanges As Object = WdSaveOptions.wdDoNotSaveChanges
            '  word.Documents.Close(saveChanges, oMissing, oMissing)
            '  Document = Nothing



            'For Each wordFile As FileInfo In wordFiles

            'Next


        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function


    Private Sub FillFieldValues(ByRef currentdoc As Word.Document)
        ' Try
        '    Dim PolicyReplacements = DataRepository.GetPolicyReplacements
        '    Dim Fields As New List(Of String)
        '    Dim Variables As New List(Of String)

        '    'FindRangeByFont(currentdoc.Range, currentdoc, "Aharoni")

        '    'FindByLine(currentdoc)
        '    'IterateRanges(currentdoc)
        '    'IterateListRanges(currentdoc)
        '    'FindByColor(currentdoc, WdColorIndex.wdRed)
        '    'For Each V In currentdoc.Variables
        '    '    Variables.Add(V.Name)
        '    'Next V
        '    'For Each Field As Field In currentdoc.Fields
        '    '    Fields.Add(Field.Code.Text)
        '    'Next
        '    ' addBullets()
        '    'Fields = currentdoc.MainDocumentPart.Document.Descendants(Of Word.Field).ToList()
        '    ' Dim listMergeFields = currentdoc.MainDocumentPart.Document.Descendants(Of FieldCode)().ToList()

        '    'For Each para As Word.Paragraph In doc.Paragraphs
        '    '    Dim paraRange As Word.Range = para.Range
        '    '    Dim text As String = paraRange.Text
        '    '    Dim regex As Regex = New Regex(findText)
        '    '    Dim final As String = regex.Replace(text, replaceText)
        '    '    paraRange.Text = final
        '    'Next

        '    'Regex.Replace(currentdoc.ActiveDocument.Range, InputBox("ThenReplace With:"))

        '    'For Each ContentRange As PlaceHolder In currentdoc.Content.Find(placeholderRegex)
        '    '    Dim name As String = PlaceHolder.ToString().Trim("[', ']")
        '    '    Dim link As String
        '    '    If linkData.TryGetValue(name, link As out) Then
        '    '        PlaceHolder.Set(New Hyperlink(Document, link, name).Content);
        '    '    End If
        '    'Next


        '    Dim rngField As Word.Range = Nothing
        '    Dim oCollapseEnd As Object = Word.WdCollapseDirection.wdCollapseEnd
        '    Dim oMoveCharacter As Object = Word.WdUnits.wdCharacter
        '    Dim oOne As Object = 1

        '    For Each Field As Field In currentdoc.Fields
        '        If Field.Type = WdFieldType.wdFieldDocVariable Then
        '            Select Case Field.Code.Text
        '                Case " DOCVARIABLE  208.1.IV  \* MERGEFORMAT "
        '                    rngField = Field.Code

        '                    Dim State As String = "MI"
        '                    Dim PolicyNo As Integer = 208
        '                    Dim Section As Integer = 1
        '                    Dim Part As String = "IV"

        '                    Dim DocVarText As String = Nothing
        '                    DocVarText = PolicyReplacements.Where(Function(p) p.State = State And p.PolicyNo = PolicyNo And p.Section = Section And p.Part = Part).Select(Function(p) p.Text).SingleOrDefault

        '                    If DocVarText IsNot Nothing Then
        '                        Field.Result.Text = DocVarText
        '                        Dim URLFound = FindURL(DocVarText)
        '                        FindAndReplace(rngField, URLFound)
        '                    Else

        '                        Dim listTemplate As ListTemplate = rngField.ListFormat.ListTemplate
        '                        rngField.ListFormat.RemoveNumbers()
        '                        rngField.Delete()
        '                    End If
        '                Case " DOCVARIABLE  208.1.VI  \* MERGEFORMAT "
        '                    Dim State As String = "MI"
        '                    Dim PolicyNo As Integer = 208
        '                    Dim Section As Integer = 1
        '                    Dim Part As String = "VI"

        '                    Dim DocVarText As String = Nothing
        '                    DocVarText = PolicyReplacements.Where(Function(p) p.State = State And p.PolicyNo = PolicyNo And p.Section = Section And p.Part = Part).Select(Function(p) p.Text).SingleOrDefault

        '                    If DocVarText IsNot Nothing Then
        '                        Field.Result.Text = DocVarText
        '                    Else
        '                        rngField = Field.Code
        '                        Dim listTemplate As ListTemplate = rngField.ListFormat.ListTemplate
        '                        rngField.Delete()
        '                        rngField.ListFormat.RemoveNumbers()
        '                    End If

        '                Case " DOCVARIABLE Facility \* MERGEFORMAT "
        '                    Dim Facility = "Cypress Skilled Nursing"
        '
        '                    If Facility IsNot Nothing Then
        '                        Field.Result.Text = Facility
        '                    End If
        '            End Select
        '        End If
        '    Next

        '    currentdoc.Fields.Update()
        'Catch ex As Exception

        'End Try

    End Sub

    Private Function FindReplaceText(ByRef currentdoc As Word.Document, FindText As String, ReplaceText As String)
        Try

            Dim totalParagraphs As Integer = currentdoc.Paragraphs.Count
            Dim final As String

            For i As Integer = 1 To totalParagraphs
                Dim temp As String = currentdoc.Paragraphs(i).Range.Text
                Dim x1 As Single = currentdoc.Paragraphs(i).Format.LeftIndent
                Dim x2 As Single = currentdoc.Paragraphs(i).Format.RightIndent
                Dim x3 As Single = currentdoc.Paragraphs(i).Format.SpaceBefore
                Dim x4 As Single = currentdoc.Paragraphs(i).Format.SpaceAfter

                If temp.Length > 1 Then
                    Dim regex As Regex = New Regex(FindText)
                    final = regex.Replace(temp, ReplaceText)

                    If final <> temp Then
                        currentdoc.Paragraphs(i).Range.Text = final
                        currentdoc.Paragraphs(i).Format.LeftIndent = x1
                        currentdoc.Paragraphs(i).Format.RightIndent = x2
                        currentdoc.Paragraphs(i).Format.SpaceBefore = x3
                        currentdoc.Paragraphs(i).Format.SpaceAfter = x4
                    End If
                End If
            Next

        Catch ex As Exception
            logger.Error(ex)
        Finally
        End Try
    End Function

    Private Function FindReplaceText(Text As String, FindText As String, ReplaceText As String) As String
        Try
            Dim Final As String = ""

            Dim linkData As New Dictionary(Of String, String) From {{"FacebookPage1", "https://www.facebook.com"}}
            Dim placeholderRegex As Regex = New Regex("\[(.*?)\]", RegexOptions.Compiled)

            If Text.Length > 1 Then
                Dim regex As Regex = New Regex(FindText)
                Final = regex.Replace(Text, ReplaceText)

                If Final <> Text Then
                    Return Final
                End If
            End If


        Catch ex As Exception
            logger.Error(ex)
        Finally
        End Try

    End Function
    Private Sub IterateRanges(ByRef currentdoc As Word.Document)
        Try

            Dim FontNames As New List(Of String)

            For Each ParaGraph In currentdoc.Paragraphs
                Dim currentRange = ParaGraph.Range
                Dim text As String = currentRange.text
                Dim FontName As String = ParaGraph.Range.Font.Name

                FontNames.Add(FontName)

                'If currentRange.Listformat.List IsNot Nothing Then
                '    Dim text2 = currentRange.text

                'End If
                'If text.Contains("Georgia") Then
                '    Dim text2 = currentRange.text
                'End If

                'If text.StartsWith("Reviewing") Then
                '    Dim text2 = currentRange.text
                'End If

                'If text.Substring(0, 7) = "Georgia" Then
                '    Dim text2 = currentRange.text
                'End If
                FindRangeByFont(currentRange, currentdoc, "Aharoni")
            Next

            Debug.WriteLine(FontNames.Count)

        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Sub

    Private Sub IterateListRanges(ByRef currentdoc As Word.Document)
        Try


            For Each ParaGraph In currentdoc.ListTemplate
                Dim currentRange = ParaGraph.Range
                Dim text = currentRange.text
                'If currentRange.Listformat.List IsNot Nothing Then
                '    Dim text2 = currentRange.text

                'End If
                FindRangeByColor(currentRange, WdColorIndex.wdRed)
            Next
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Sub

    Private Function FindURL(Text As String) As List(Of String)
        '  Dim txt As String = "this is my url http://www.google.com and visit this website and this is my url http://www.yahoo.com"
        Dim URLFound As New List(Of String)
        For Each item As Match In Regex.Matches(Text, "(http|ftp|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?")
            URLFound.Add(item.Value)
        Next
        Return URLFound
    End Function
    'Private Shared Sub AddBullets()
    '    Try
    '        Dim app As Application = New Application()
    '        Dim doc As Document = app.Documents.Add()
    '        Dim range As Range = doc.Range(0, 0)

    '        range.ListFormat.ApplyNumberDefault()
    '        range.Text = "Birinci"
    '        range.InsertParagraphAfter()

    '        Dim listTemplate As ListTemplate = range.ListFormat.ListTemplate
    '        Dim subRange As Range = doc.Range(range.StoryLength - 1)
    '        subRange.ListFormat.ApplyBulletDefault()
    '        subRange.ListFormat.ListIndent()
    '        subRange.Text = "Alt Birinci"
    '        subRange.InsertParagraphAfter()

    '        Dim sublistTemplate As ListTemplate = subRange.ListFormat.ListTemplate

    '        Dim subRange2 As Range = doc.Range(subRange.StoryLength - 1)
    '        subRange2.ListFormat.ApplyListTemplate(sublistTemplate)
    '        subRange2.ListFormat.ListIndent()
    '        subRange2.Text = "Alt İkinci"
    '        subRange2.InsertParagraphAfter()

    '        Dim range2 As Range = doc.Range(range.StoryLength - 1)
    '        range2.ListFormat.ApplyListTemplateWithLevel(listTemplate, True)
    '        Dim isContinue As WdContinue = range2.ListFormat.CanContinuePreviousList(listTemplate)
    '        range2.Text = "İkinci"
    '        range2.InsertParagraphAfter()

    '        Dim range3 As Range = doc.Range(range2.StoryLength - 1)
    '        range3.ListFormat.ApplyListTemplate(listTemplate)
    '        range3.Text = "Üçüncü"
    '        range3.InsertParagraphAfter()

    '        Dim path As String = Environment.CurrentDirectory
    '        Dim totalExistDocx As Integer = Directory.GetFiles(path, "test*.docx").Count()

    '        '  path = path.Combine(path, String.Format("test{0}.docx", totalExistDocx + 1))
    '        app.ActiveDocument.SaveAs2("c:\CCG\Temp\Test4.docx", WdSaveFormat.wdFormatXMLDocument)
    '        doc.Close()
    '        Process.Start(path)
    '    Catch exception As Exception
    '        Throw
    '    End Try
    'End Sub


    'Public Function DocumentGenerator(ByVal templatePath As String) As Byte()
    '    Dim buffer As Byte()

    '    Using stream As MemoryStream = New MemoryStream()
    '        buffer = System.IO.File.ReadAllBytes(templatePath)
    '        stream.Write(buffer, 0, buffer.Length)

    '        Using wordDocument As WordprocessingDocument = WordprocessingDocument.Open(stream, True)
    '            Dim listBookMarks = wordDocument.MainDocumentPart.Document.Descendants(Of BookmarkStart)().ToList()
    '            Dim listMergeFields = wordDocument.MainDocumentPart.Document.Descendants(Of FieldCode)().ToList()
    '            wordDocument.Close()
    '        End Using

    '        buffer = stream.ToArray()
    '    End Using

    '    Return buffer
    'End Function


    Public Shared Function CopyDocSection(Document As WordDocument, SectionIndex As SectionIndex, FacilityName As String) As Byte()
        Try
            Dim DestinationDocument As New WordDocument()

            For Each Section As compliancecg.Section In SectionIndex.Sections
                For Index = Section.FromIndex To Section.ToIndex
                    DestinationDocument.Sections.Add(Document.Sections(Index).Clone())
                Next
            Next

            GetFieldsInDocument(DestinationDocument, FieldType.FieldDocVariable, FacilityName)

            Dim stream As New MemoryStream()
            DestinationDocument.Save(stream, FormatType.Docx)
            DestinationDocument.UpdateDocumentFields()
            Return stream.ToArray
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function



    'Public Shared Function CopyDocSection2(Document As WordDocument, SectionNumber As String, FacilityName As String) As Byte()
    '    Try
    '        Dim DestinationDocument As New WordDocument()
    '        DestinationDocument.Sections.Add(Document.Sections(SectionNumber).Clone())


    '        'GetFieldsInDocument(DestinationDocument, FieldType.FieldDocVariable, FacilityName)


    '        Dim converter As DocToPDFConverter = New DocToPDFConverter()
    '        Dim document2 As PdfDocument = converter.ConvertToPDF(DestinationDocument)
    '        Dim security As PdfSecurity = document2.Security

    '        security.KeySize = PdfEncryptionKeySize.Key256Bit
    '        security.Algorithm = PdfEncryptionAlgorithm.AES
    '        security.OwnerPassword = "syncfusion"
    '        ' security.Permissions = Not (PdfPermissionsFlags.CopyContent Or PdfPermissionsFlags.Print)
    '        security.Permissions = PdfPermissionsFlags.Default
    '        'graphics.DrawString("This document is protected with owner password", font, brush, New PointF(0, 40))
    '        document2.Save("c:\ccg\sample.pdf")







    '        'Dim converter As DocToPDFConverter = New DocToPDFConverter()

    '        ''Dim document As WordDocument = New WordDocument(File.InputStream)
    '        ''  Dim pdfDoc As PdfDocument = converter.ConvertToPDF(DestinationDocument)

    '        'Dim pdfDoc As PdfDocument = converter.ConvertToPDF(DestinationDocument)

    '        'Dim security As PdfSecurity = pdfDoc.Security
    '        ''Specifies key size and encryption algorithm using 128 bit key in RC4 mode.
    '        'security.KeySize = PdfEncryptionKeySize.Key128Bit
    '        'security.Algorithm = PdfEncryptionAlgorithm.RC4
    '        'security.OwnerPassword = "syncfusion"

    '        ''It allows printing and accessibility copy content

    '        ''  security.Permissions = PdfPermissionsFlags.Print Or PdfPermissionsFlags.AccessibilityCopyContent
    '        ''security.Permissions = (PdfPermissionsFlags.CopyContent Or PdfPermissionsFlags.Print)
    '        ''security.Permissions = ~PdfPermissionsFlags.Default
    '        '' security.Permissions = +(PdfPermissionsFlags.CopyContent Or PdfPermissionsFlags.Print)
    '        'security.Permissions = PdfPermissionsFlags.Print
    '        'security.UserPassword = "password"
    '        'pdfDoc.Save("c:\ccg\sample.pdf")

    '        ''Dim stream As MemoryStream = New MemoryStream()
    '        ''pdfDoc.Save(stream)
    '        ''pdfDoc.Close(True)

    '        ''Dim docBytes As Byte() = stream.ToArray()
    '        ''Dim ldoc As PdfLoadedDocument = New PdfLoadedDocument(docBytes, "password")
    '        ''ldoc.Save("c:\ccg\sample.pdf")
    '        ''ldoc.Close()






    '        ''pdfDoc.Save("c:\CCG\FacilityName.pdf")
    '        ''Dim stream As New MemoryStream()
    '        ''DestinationDocument.Save(stream, FormatType.Docx)
    '        ''DestinationDocument.UpdateDocumentFields()
    '        ''Return stream.ToArray



    '    Catch ex As Exception
    '        logger.Error(ex)

    '    End Try
    'End Function




    'Dim document2 As PdfDocument = New PdfDocument()
    'Dim page As PdfPage = document2.Pages.Add()
    'Dim graphics As PdfGraphics = page.Graphics
    'Dim font As PdfStandardFont = New PdfStandardFont(PdfFontFamily.TimesRoman, 20.0F, PdfFontStyle.Bold)
    'Dim brush As PdfBrush = PdfBrushes.Black
    'Dim security As PdfSecurity = document2.Security
    Private Function FromArgbExample() As Color
        Dim myArgbColor As Color = New Color()
        myArgbColor = Color.FromArgb(166, 166, 166, 166)
        Return myArgbColor
    End Function
    Public Function CopyDocSection2(Document As WordDocument, SectionNumber As String, FacilityName As String) As MemoryStream
        Try
            Dim DestinationDocument As New WordDocument()
            DestinationDocument.Sections.Add(Document.Sections(SectionNumber).Clone())
            GetFieldsInDocument(DestinationDocument, FieldType.FieldDocVariable, FacilityName)

            Dim TextColor = FromArgbExample()

            Dim TextWatermark As New TextWatermark()
            DestinationDocument.Watermark = TextWatermark
            TextWatermark.Size = 90
            TextWatermark.Layout = WatermarkLayout.Diagonal
            'TextWatermark.ApplyStyle("Heading 2")
            ' TextWatermark.Color = TextColor
            TextWatermark.Color = Color.Black
            TextWatermark.Text = FacilityName
            'TextWatermark.Semitransparent = True

            'DestinationDocument.EnsureMinimal()
            'Dim paragraph2 As IWParagraph = DestinationDocument.LastParagraph
            'paragraph2.AppendText("AdventureWorks Cycles, the fictitious company on which the AdventureWorks sample databases are based, is a large, multinational manufacturing company.")
            'Dim picWatermark As New PictureWatermark()
            'picWatermark.Scaling = 120.0F
            'picWatermark.Washout = True
            'DestinationDocument.Watermark = picWatermark
            'picWatermark.Picture = Image.FromFile("C:\Users\bnias\source\repos\compliancecg\compliancecg\Content\Images\Logos\CCG_LOGO_BW.jpg")



            Dim converter As DocToPDFConverter = New DocToPDFConverter()
            Dim ConvertedDocument As PdfDocument = converter.ConvertToPDF(DestinationDocument)


            Dim security As PdfSecurity = ConvertedDocument.Security
            security.KeySize = PdfEncryptionKeySize.Key128Bit
            security.Algorithm = PdfEncryptionAlgorithm.RC4
            security.OwnerPassword = "syncfusion"
            'It allows printing and accessibility copy content
            security.Permissions = PdfPermissionsFlags.Print Or PdfPermissionsFlags.AccessibilityCopyContent


            security.KeySize = PdfEncryptionKeySize.Key256Bit
            security.Algorithm = PdfEncryptionAlgorithm.AES
            security.OwnerPassword = "syncfusion"

            security.Permissions = PdfPermissionsFlags.Print Or PdfPermissionsFlags.FullQualityPrint
            'security.Permissions = PdfPermissionsFlags.Default
            ConvertedDocument.Save("c:\ccg\Sample Secured.pdf")

            Dim stream As MemoryStream = New MemoryStream()
            ConvertedDocument.Save(stream)
            ConvertedDocument.Close(True)
            Dim docBytes As Byte() = stream.ToArray()

            Return stream
            'document2.Save("c:\ccg\sample.pdf")
        Catch ex As Exception

        End Try
    End Function

    Public Sub Encryption(document As PdfDocument, ByVal encryptionType As String, ByVal encryptOptionType As String)

        Dim page As PdfPage = document.Pages.Add()
        Dim graphics As PdfGraphics = page.Graphics
        Dim font As PdfStandardFont = New PdfStandardFont(PdfFontFamily.TimesRoman, 14.0F, PdfFontStyle.Bold)
        Dim brush As PdfBrush = PdfBrushes.Black

        Dim security As PdfSecurity = document.Security

        If encryptionType = "40_RC4" Then
            security.KeySize = PdfEncryptionKeySize.Key40Bit
        ElseIf encryptionType = "128_RC4" Then
            security.KeySize = PdfEncryptionKeySize.Key128Bit
            security.Algorithm = PdfEncryptionAlgorithm.RC4
        ElseIf encryptionType = "128_AES" Then
            security.KeySize = PdfEncryptionKeySize.Key128Bit
            security.Algorithm = PdfEncryptionAlgorithm.AES
        ElseIf encryptionType = "256_AES" Then
            security.KeySize = PdfEncryptionKeySize.Key256Bit
            security.Algorithm = PdfEncryptionAlgorithm.AES
        ElseIf encryptionType = "256_AES_Revision_6" Then
            security.KeySize = PdfEncryptionKeySize.Key256BitRevision6
            security.Algorithm = PdfEncryptionAlgorithm.AES
        End If

        If encryptOptionType = "Encrypt only attachments [For AES only]" Then
            'Dim attachment As PdfAttachment = New PdfAttachment(ResolveApplicationDataPath("Products.xml"))
            'attachment.ModificationDate = DateTime.Now
            'attachment.Description = "About Syncfusion"
            'attachment.MimeType = "application/txt"
            'document.Attachments.Add(attachment)
            'security.EncryptionOptions = PdfEncryptionOptions.EncryptOnlyAttachments
        ElseIf encryptOptionType = "Encrypt all contents except metadata" Then
            security.EncryptionOptions = PdfEncryptionOptions.EncryptAllContentsExceptMetadata
        Else
            security.EncryptionOptions = PdfEncryptionOptions.EncryptAllContents
        End If

        security.OwnerPassword = "syncfusion"
        security.Permissions = PdfPermissionsFlags.Print Or PdfPermissionsFlags.FullQualityPrint
        'security.Permissions = PdfPermissionsFlags

        'security.Permissions = PdfPermissionsFlags.Default

        ' security.UserPassword = "password"
        Dim text As String = "Security options:" & vbLf & vbLf & String.Format("KeySize: {0}" & vbLf & vbLf & "Encryption Algorithm: {4}" & vbLf & vbLf & "Owner Password: {1}" & vbLf & vbLf & "Permissions: {2}" & vbLf & vbLf & "UserPassword: {3}", security.KeySize, security.OwnerPassword, security.Permissions, security.UserPassword, security.Algorithm)

        If encryptionType = "256_AES_Revision_6" Then
            text += String.Format(vbLf & vbLf & "Revision: {0}", "Revision 6")
        ElseIf encryptionType = "256_AES" Then
            text += String.Format(vbLf & vbLf & "Revision: {0}", "Revision 5")
        End If

        graphics.DrawString("Document is Encrypted with following settings", font, brush, PointF.Empty)
        font = New PdfStandardFont(PdfFontFamily.TimesRoman, 11.0F, PdfFontStyle.Bold)
        graphics.DrawString(text, font, brush, New PointF(0, 40))

    End Sub
    Public Shared Function CopyDocSection2() As Byte()
        Try

            'Dim Document As New WordDocument
            'Dim File As New FileInfo("c:\ccg\NJ Masters.docx")
            'Document.OpenReadOnly(File.FullName, FormatType.Docx)

            'Dim converter As DocToPDFConverter = New DocToPDFConverter()
            'Dim document2 As PdfDocument = converter.ConvertToPDF(Document)
            'Dim security As PdfSecurity = document2.Security

            ''security.KeySize = PdfEncryptionKeySize.Key256Bit
            ''security.Algorithm = PdfEncryptionAlgorithm.AES
            ''security.OwnerPassword = "syncfusion"
            ''' security.Permissions = Not (PdfPermissionsFlags.CopyContent Or PdfPermissionsFlags.Print)
            ''security.Permissions = PdfPermissionsFlags.Default
            ''graphics.DrawString("This document is protected with owner password", font, brush, New PointF(0, 40))
            'document2.Save("c:\ccg\sample.pdf")
        Catch ex As Exception

        End Try
    End Function


    Public Function CopyDocSection4(Document As WordDocument, SectionNumber As String) As WordDocument
        Try
            Dim DestinationDocument As New WordDocument()
            DestinationDocument.Sections.Add(Document.Sections(SectionNumber).Clone())

            Return DestinationDocument
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Shared Function CopyDocSection(Document As WordDocument, FacilityName As String) As Byte()
        Try
            'GetFieldsInDocument(Document, FieldType.FieldDocVariable, FacilityName)
            GetFieldsInDocument2(Document, FieldType.FieldDocVariable, FacilityName)

            Dim stream As New MemoryStream()
            Document.Save(stream, FormatType.Docx)
            'Document.UpdateDocumentFields()
            Return stream.ToArray
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Shared Function CopyDocSection3(Document As WordDocument, FacilityName As String) As Byte()
        Try
            Dim DestinationDocument As New WordDocument()

            DestinationDocument = Document

            GetFieldsInDocument(DestinationDocument, FieldType.FieldDocVariable, FacilityName)

            Dim stream As New MemoryStream()
            DestinationDocument.Save(stream, FormatType.Docx)
            DestinationDocument.UpdateDocumentFields()
            Return stream.ToArray
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Shared Function CopyDocSection2(Document As WordDocument, SectionNumber As String) As Byte()

        Try

            'Dim DeleteCounter As Integer = 0
            'For Each sec As Section In document.Sections
            '    DeleteCounter += 1
            '    If DeleteCounter <> SectionNumber Then
            '        sec.Range.Delete()
            '    End If
            'Next sec

            'DeleteCounter = 0
            'For Each sec As Section In document.Sections
            '    DeleteCounter += 1
            '    If DeleteCounter > 1 Then
            '        sec.Headers(WdHeaderFooterIndex.wdHeaderFooterPrimary).Range.Delete()
            '        sec.Footers(WdHeaderFooterIndex.wdHeaderFooterPrimary).Range.Delete()
            '        sec.Range.Delete()
            '    End If
            'Next sec

            'Dim document.Open(fileName)



            'For Each section As WSection In document2.Sections
            '    'Accesses the Body of section where all the contents in document are apart
            '    Dim sectionBody As WTextBody = section.Body
            '    IterateTextBody(sectionBody)
            '    Dim headersFooters As WHeadersFooters = section.HeadersFooters
            '    'Consider that OddHeader and OddFooter are applied to this document
            '    'Iterates through the text body of OddHeader and OddFooter
            '    IterateTextBody(headersFooters.OddHeader)
            '    IterateTextBody(headersFooters.OddFooter)
            'Next


            ''Accesses the Body of section where all the contents in document are apart
            'Dim sectionBody As WTextBody = section.Body
            'IterateTextBody(sectionBody)
            'Dim headersFooters As WHeadersFooters = section.HeadersFooters
            ''Consider that OddHeader and OddFooter are applied to this document
            ''Iterates through the text body of OddHeader and OddFooter
            'IterateTextBody(headersFooters.OddHeader)
            'IterateTextBody(headersFooters.OddFooter)



            Dim destinationDocument As New WordDocument()
            destinationDocument.Sections.Add(Document.Sections(SectionNumber).Clone())
            'destinationDocument.Save("C:\CCG\temp\Sections" + ".docx")
            'destinationDocument.Close()


            Dim stream As New MemoryStream()
            destinationDocument.Save(stream, FormatType.Docx)

            ' Dim Bytes = ReadFully(stream)
            'Saves and closes the document instance
            'document2.Save("C:\CCG\temp\Result.docx")
            'document2.Close()
            'System.Diagnostics.Process.Start("C:\CCG\temp\Result.docx")



            ' Dim document As New Syncfusion.DocIO.DLS.WordDocument(wordDocumentStream, FormatType.Automatic)


            'Dim WordFunctions As New WordFunctions
            'WordFunctions.FillFieldValues(Document)

            '  Dim paragraphText As String = RichEditDocumentServer.Document.GetText(RichEditDocumentServer.Document.Paragraphs(0).Range)


            'Dim server = New RichEditDocumentServer()
            'server.LoadDocument(Document.FullName)

            ''Dim SectionCount = server.Document.Sections.Count
            ''Dim retrievedRange As DocumentRange = server.Document.Sections(SectionNumber).Range
            '''Dim retrievedRange As DocumentRange = server.Document.GetSection(SectionNumber).Range

            'Dim sourceSection As API.Native.Section = server.Document.Sections(SectionNumber)
            'Dim SubDocument As SubDocument = sourceSection.BeginUpdateFooter(HeaderFooterType.Odd)
            'sourceSection.EndUpdateFooter(SubDocument)
            'Dim bytes = SubDocument.GetOpenXmlBytes(sourceSection.Range)


            'Dim retrievedRange As DocumentRange = server.Document.Sections(SectionNumber).Range
            'Dim Subdocument2 As SubDocument = retrievedRange.BeginUpdateDocument()
            'Dim bytes2 = Subdocument2.GetOpenXmlBytes(retrievedRange)

            'Dim r As DocumentRange = Document.CreateRange(Document.Paragraphs(0).Range.Start, Document.Paragraphs(0).Range.Length + Document.Paragraphs(1).Range.Length + Document.Paragraphs(2).Range.Length)

            ' retrievedRange.EndUpdateDocument(Subdocument)


            '  Dim firstSection As API.Native.Section = server.Document.Sections(0)
            '' Create an empty header.
            'Dim newFooter As SubDocument = firstSection.BeginUpdateFooter()

            ''firstSection.EndUpdateFooter(newFooter)
            '' Check whether the document already has a header (the same header for all pages).
            'If firstSection.HasFooter(HeaderFooterType.Primary) Then
            '    Dim footerDocument As SubDocument = firstSection.BeginUpdateFooter()
            '    Document.ChangeActiveDocument(footerDocument)
            '    Document.CaretPosition = footerDocument.CreatePosition(0)
            '    firstSection.EndUpdateHeader(footerDocument)
            'End If



            'If Me.richEditControl.Document.Selection.Length > 0 Then

            '    Dim selection As DevExpress.XtraRichEdit.API.Native.DocumentRange = richEditControl.Document.Selection
            '    Dim doc As DevExpress.XtraRichEdit.API.Native.SubDocument = selection.BeginUpdateDocument()
            '    bytes = doc.GetOpenXmlBytes(selection)
            '    selection.EndUpdateDocument(doc)
            'Else
            '    bytes = richEditControl.Document.GetOpenXmlBytes(richEditControl.Document.Range)
            'End If


            ''Dim server1 = New RichEditDocumentServer()
            ''server1.Document.InsertDocumentContent(server1.Document.Range.Start, retrieveRange)
            ''server1.SaveDocument("secondParagraph.docx", DocumentFormat.OpenXml)

            ''Dim Paragraphs = sourceDoc.Paragraphs
            ''For Each paragraph As Word.Paragraph In Paragraphs
            ''    If paragraph.Range.Text.Trim() = String.Empty Then
            ''        paragraph.Range.[Select]()
            ''        word.Selection.Delete()
            ''    End If
            ''Next


            ''Dim docText As String = Document.WordOpenXML
            ''Dim bytes As Byte() = Encoding.Unicode.GetBytes(docText)

            'Dim tmpFile = Path.GetTempFileName()

            'Dim app As Word.Application = New Word.Application()
            ''app.Visible = False
            ''app.WindowState = Word.WdWindowState.wdWindowStateMaximize
            'Dim Document2 = app.Documents.Add(Document.FullName)


            ''For Each sec As Section In Document.Sections
            ''    sec.Range.Copy()
            ''    Document2.Content.PasteSpecial(DataType:=WdPasteOptions.wdKeepSourceFormatting)
            ''Next sec



            'Document2.SaveAs(tmpFile)
            ''Dim doc2 = app.Document = app.Documents.Add

            ''app.Documents.Add()








            '' Document.SaveAs(tmpFile)
            'Document2.Close()
            'Dim newFileBytes As Byte() '= File.ReadAllBytes(tmpFile)

            ''
            ''Document = Nothing

            '' File.WriteAllBytes(tmpFile, bytes)

            '' File.Delete(tmpFile)





            'Return newFileBytes
            'Return bytes

            Return stream.ToArray
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function




    Private Shared Sub GetFieldsInDocument2(ByVal document As WordDocument, ByVal fieldType As FieldType, FacilityName As String)
        Try
            Dim ReplaceText = "{Facility}"

            document.Variables.Add(ReplaceText, FacilityName)
            document.UpdateDocumentFields()


            ''Dim FacilityName As String = "Cypress Skilled Nursing"
            'For Each section As WSection In document.Sections
            'IterateTextBody(Section.Body, fieldType)
            'Next
            'For Each Field As WField In DocVariablesFields
            '    Field.Text = FacilityName
            'Next
            'Dim DocVariablesFieldsCount = DocVariablesFields.Count
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Sub

    Private Shared Sub GetFieldsInDocument(ByVal document As WordDocument, ByVal fieldType As FieldType, FacilityName As String)
        Try


            'Dim FacilityName As String = "Cypress Skilled Nursing"
            For Each section As WSection In document.Sections
                IterateTextBody(section.Body, fieldType)
            Next
            For Each Field As WField In DocVariablesFields
                Field.Text = FacilityName
            Next
            Dim DocVariablesFieldsCount = DocVariablesFields.Count
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Sub

    Private Shared Sub IterateTextBody(ByVal textBody As WTextBody, ByVal fieldType As FieldType)
        For i As Integer = 0 To textBody.ChildEntities.Count - 1
            Dim bodyItemEntity As IEntity = textBody.ChildEntities(i)

            Select Case bodyItemEntity.EntityType
                Case EntityType.Paragraph
                    Dim paragraph As WParagraph = TryCast(bodyItemEntity, WParagraph)
                    IterateParagraph(paragraph.Items, fieldType)
                Case EntityType.Table
                    IterateTable(TryCast(bodyItemEntity, WTable), fieldType)
                Case EntityType.BlockContentControl
                    Dim blockContentControl As BlockContentControl = TryCast(bodyItemEntity, BlockContentControl)
                    IterateTextBody(blockContentControl.TextBody, fieldType)
            End Select
        Next
    End Sub

    Private Shared Sub IterateParagraph(ByVal paraItems As ParagraphItemCollection, ByVal fieldType As FieldType)
        Try
            For i As Integer = 0 To paraItems.Count - 1

                If TypeOf paraItems(i) Is WField AndAlso (TryCast(paraItems(i), WField)).FieldType = fieldType Then
                    'fields.Add(TryCast(paraItems(i), WMergeField))
                    DocVariablesFields.Add(paraItems(i))
                    ' DocVariablesFields.Add(TryCast(paraItems(i), DocVariables))
                ElseIf TypeOf paraItems(i) Is WTextBox Then
                    Dim textBox As WTextBox = TryCast(paraItems(i), WTextBox)
                    IterateTextBody(textBox.TextBoxBody, fieldType)
                    'ElseIf TypeOf paraItems(i) Is API.Native.Shape Then
                    '    'Dim shape As API.Native.Shape = TryCast(paraItems(i), API.Native.Shape)
                    '    'IterateTextBody(shape.TextBody, fieldType)
                    'ElseIf TypeOf paraItems(i) Is InlineContentControl Then
                    Dim inlineContentControl As InlineContentControl = TryCast(paraItems(i), InlineContentControl)
                    IterateParagraph(inlineContentControl.ParagraphItems, fieldType)
                End If
            Next
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Sub

    Private Shared Sub IterateTable(ByVal table As WTable, ByVal fieldType As FieldType)
        For Each row As WTableRow In table.Rows

            For Each cell As WTableCell In row.Cells
                IterateTextBody(cell, fieldType)
            Next
        Next
    End Sub







    'Private Shared Function IterateTextBody2(SectionNo As Integer, textBody As WTextBody) As List(Of SectionChapter)
    '    Dim SectionChapters As New List(Of SectionChapter)
    '    'Iterates through the each of the child items of WTextBody

    '    For i As Integer = 0 To textBody.ChildEntities.Count - 1

    '        'IEntity is the basic unit in DocIO DOM. 
    '        'Accesses the body items (should be either paragraph or table) as IEntity

    '        Dim bodyItemEntity As IEntity = textBody.ChildEntities(i)

    '        'A Text body has 2 types of elements - Paragraph and Table
    '        'decide the element type using EntityType

    '        Select Case bodyItemEntity.EntityType
    '            Case EntityType.Paragraph

    '                Dim paragraph As WParagraph = TryCast(bodyItemEntity, WParagraph)

    '                '  SectionChapters.Add(New SectionChapter With {.SectionIndex = SectionNo, .Chapter = paragraph.Text})

    '                ' Checks for a particular style name And removes the paragraph from DOM
    '                If paragraph.Text.Contains("CCG") Then
    '                    Dim StartPos = paragraph.Text.IndexOf("CCG")
    '                    Dim EndPos = paragraph.Text.IndexOf("  ", StartPos)
    '                    Dim Chapter As String = paragraph.Text.Substring(StartPos, EndPos - StartPos)
    '                    SectionChapters.Add(New SectionChapter With {.SectionIndex = SectionNo, .Chapter = Chapter})

    '                End If

    '                Exit Select

    '            Case EntityType.Table


    '        End Select

    '    Next
    '    Return SectionChapters
    'End Function
    'Private Shared Sub IterateTextBody(textBody As WTextBody)

    '    'Iterates through the each of the child items of WTextBody

    '    For i As Integer = 0 To textBody.ChildEntities.Count - 1

    '        'IEntity is the basic unit in DocIO DOM. 

    '        'Accesses the body items (should be either paragraph or table) as IEntity

    '        Dim bodyItemEntity As IEntity = textBody.ChildEntities(i)

    '        'A Text body has 2 types of elements - Paragraph and Table

    '        'decide the element type using EntityType

    '        Select Case bodyItemEntity.EntityType

    '            Case EntityType.Paragraph

    '                Dim paragraph As WParagraph = TryCast(bodyItemEntity, WParagraph)

    '                'Checks for a particular style name and removes the paragraph from DOM

    '                If paragraph.StyleName = "MyStyle" Then

    '                    Dim index As Integer = textBody.ChildEntities.IndexOf(paragraph)

    '                    textBody.ChildEntities.RemoveAt(index)

    '                End If

    '                Exit Select

    '            Case EntityType.Table

    '                'Table is a collection of rows and cells

    '                'Iterates through table's DOM

    '                IterateTable(TryCast(bodyItemEntity, WTable))

    '                Exit Select

    '        End Select

    '    Next

    'End Sub

    'Private Shared Sub IterateTable(table As WTable)

    '    'Iterates the row collection in a table

    '    For Each row As WTableRow In table.Rows

    '        'Iterates the cell collection in a table row

    '        For Each cell As WTableCell In row.Cells

    '            'Table cell is derived from (also a) TextBody

    '            'Reusing the code meant for iterating TextBody

    '            IterateTextBody(cell)

    '        Next

    '    Next

    'End Sub

    Public Shared Function ReadFully(ByVal input As Stream) As Byte()
        Try

            Dim Bytes As Byte() = Nothing
            Using ms As MemoryStream = New MemoryStream()
                input.CopyTo(ms)
                Bytes = ms.ToArray()
            End Using
            Return Bytes
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    'Public Shared Function ReadFully(ByVal input As Stream) As Byte()
    '        Dim buffer As Byte() = New Byte(16383) {}

    '        Using ms As MemoryStream = New MemoryStream()
    '            Dim read As Integer

    '            While (CSharpImpl.__Assign(read, input.Read(buffer, 0, buffer.Length))) > 0
    '                ms.Write(buffer, 0, read)
    '            End While

    '            Return ms.ToArray()
    '        End Using
    '    End Function

    '    Private Class CSharpImpl
    '        <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
    '        Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
    '            target = value
    '            Return value
    '        End Function
    '    End Class


    'Private Sub LoadHelpContent(ByVal utilityHelpID As Integer, ByVal userID As Integer)
    '    DocumentManager.CloseDocument(Session.SessionID)
    '    Dim HDB As HelpDB = New HelpDB()
    '    Dim Content As Byte() = HDB.GetHelpContent(utilityHelpID, userID)

    '    If Content.Length > 0 Then
    '        rteHelp.Open(Session.SessionID, DocumentFormat.Rtf, Function() New MemoryStream(Content))
    '    Else
    '        rteHelp.Open(Session.SessionID, DocumentFormat.PlainText, Function() Encoding.UTF8.GetBytes("Help is not available for this topic. Please check back later."))
    '    End If
    'End Sub

    'Private Sub SurroundingSub()
    '    Dim templateData As Byte() = MyResources.DocumentTemplate
    '    Dim stream As MemoryStream = New MemoryStream()
    '    stream.Write(teamplateData, 0, templateData.Length)
    '    Dim document = WordprocessingDocument.Open(stream, True)
    '    Dim body As Body = document.MainDocumentPart.Document.Body
    '    document.ChangeDocumentType(WordprocessingDocumentType.MacroEnabledDocument)
    '    Dim result As Byte() = stream.ToArray()

    '    Using filestream As FileStream = File.Create("C:Document.docx")
    '        filestream.Write(result, 0, result.Length)
    '    End Using
    'End Sub

    'Private Sub SurroundingSub()
    '    Dim tmpFile = Path.GetTempFileName()
    '    File.WriteAllBytes(tmpFile, fileBytes)
    '    Dim app As Application = New Word.Application()
    '    Dim doc As Document = app.Documents.Open(filePath)
    '    doc.Close()
    '    app.Quit()
    '    Dim newFileBytes As Byte() = File.ReadAllBytes(tmpFile)
    '    File.Delete(tmpFile)
    'End Sub


End Class


Public Class WordRange
    Public Range As Range
    Public Deleted As Boolean = False
End Class

Public Class SectionIndex
    Public Root As Integer
    Public Sections As New List(Of Section)
    Public Title As String
    Public Header As String
    Public Bookmark As String
End Class

Public Class Section
    Public FromIndex As Integer
    Public ToIndex As Integer
End Class
Public Class WordDocuments
    Public Documents As New List(Of Word.Document)
End Class
