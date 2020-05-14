'Imports System.IO
'Imports System.Text.RegularExpressions
'Imports CCGData
'Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Interop.Word

'Public Class WordFunctions
'    Private DataRepository As New DataRepository
'    Public Function Document(WordFilePath As String) As String

'        Dim doc As Word.Document = OpenDocument(WordFilePath)
'        FindAndReplace(doc)
'        FillFieldValues(doc)
'        Dim FileName As String = SaveDocument(WordFilePath, doc)
'        Return FileName
'    End Function



'    Public Sub ConvertWordFile(WordFilePath As String)
'        Try
'            Dim Missing As Object = System.Reflection.Missing.Value
'            Dim File As FileInfo = New FileInfo(WordFilePath)

'            Dim SaveAsDir = "C:\CCG\temp"
'            Dim SaveasFile As String = File.Name.Replace(File.Extension.ToString, "")
'            SaveasFile = $"{SaveasFile}.xml"
'            Dim FullSaveAS As String = $"{SaveAsDir}\{SaveasFile}"
'            If (Not System.IO.Directory.Exists(SaveAsDir)) Then System.IO.Directory.CreateDirectory(SaveAsDir)

'            Dim word As Word.Application = New Word.Application()

'            Dim [readOnly] As Object = True
'            Dim docs As Word.Document = word.Documents.Open(WordFilePath, Missing, [readOnly], Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing)



'            docs.SaveAs(FullSaveAS, word.WdSaveFormat.wdFormatFlatXML)
'            , Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing)
'            docs.Close()


'        Catch ex As Exception

'        End Try
'    End Sub

'    Private Function OpenDocument(WordFilePath As String) As Word.Document
'        Try
'            Dim Missing As Object = System.Reflection.Missing.Value
'            Dim File As FileInfo = New FileInfo(WordFilePath)
'            Dim word As Word.Application = New Word.Application()

'            Dim [readOnly] As Object = True
'            If File IsNot Nothing Then
'                Dim doc As Word.Document = word.Documents.Open(WordFilePath, Missing, [readOnly], Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing)
'                Return doc
'            End If
'        Catch ex As Exception

'        End Try
'    End Function

'    Private Function SaveDocument(WordFilePath As String, currentdoc As Word.Document) As String
'        Try
'            Dim File As FileInfo = New FileInfo(WordFilePath)
'            Dim SaveDir = "C:\CCG\temp"
'            Dim SaveFile As String = File.Name.Replace(File.Extension.ToString, "")
'            SaveFile = $"{SaveFile}.docx"
'            Dim SaveAsFile = $"{SaveDir}\{SaveFile}"
'            If (Not System.IO.Directory.Exists(SaveDir)) Then System.IO.Directory.CreateDirectory(SaveDir)

'            currentdoc.SaveAs(SaveAsFile, WdSaveFormat.wdFormatDocumentDefault)
'            currentdoc.Close()
'            Return SaveAsFile
'        Catch ex As Exception

'        End Try
'    End Function


'    Private Sub FindAndReplace(ByRef currentdoc As Word.Document)
'        Try
'            Dim Replacements As New Dictionary(Of String, String)
'            Replacements.Add("Client", "Client2")
'            Replacements.Add("LastName", "LastName2")

'            For Each Replacement In Replacements

'                Dim Missing As Object = System.Reflection.Missing.Value

'                Dim matchCase As Object = False
'                Dim matchWholeWord As Object = True
'                Dim matchWildCards As Object = False
'                Dim matchSoundsLike As Object = False
'                Dim matchAllWordForms As Object = False
'                Dim forward As Object = True
'                Dim format As Object = False
'                Dim matchKashida As Object = False
'                Dim matchDiacritics As Object = False
'                Dim matchAlefHamza As Object = False
'                Dim matchControl As Object = False
'                Dim read_only As Object = False
'                Dim visible As Object = True
'                Dim replace As Object = 2
'                Dim wrap As Object = 1

'                Dim intFound As Integer = 0
'                Dim rng As Word.Range = currentdoc.Range
'                rng.Find.ClearFormatting()

'                rng.Find.Replacement.ClearFormatting()
'                rng.Find.Replacement.Font.ColorIndex = Word.WdColorIndex.wdBlue


'                rng.Find.Forward = True
'                rng.Find.Text = Replacement.Key
'                rng.Find.Execute(Missing, matchCase, matchWholeWord, matchWildCards, matchSoundsLike, matchAllWordForms, forward, wrap, format, Missing, Missing, matchKashida, matchDiacritics, matchAlefHamza, matchControl)
'                rng.Find.Replacement.Font.ColorIndex = Word.WdColorIndex.wdDarkRed
'                While rng.Find.Found
'                    intFound += 1
'                    currentdoc.Fields.Add(rng, WdFieldType.wdFieldDocVariable, Replacement.Value)
'                    rng.Find.Execute(Missing, matchCase, matchWholeWord, matchWildCards, matchSoundsLike, matchAllWordForms, forward, wrap, format, Missing, Missing, matchKashida, matchDiacritics, matchAlefHamza, matchControl)
'                End While

'            Next
'        Catch ex As Exception

'        End Try

'    End Sub

'    Private Sub FindAndReplace(ByRef currentRange As Word.Range, URLfound As List(Of String))
'        Try
'            Dim Replacements As New Dictionary(Of String, String)
'            Replacements.Add("Client", "Client2")
'            Replacements.Add("LastName", "LastName2")

'            For Each URL As String In URLfound

'                Dim Missing As Object = System.Reflection.Missing.Value

'                Dim matchCase As Object = False
'                Dim matchWholeWord As Object = True
'                Dim matchWildCards As Object = False
'                Dim matchSoundsLike As Object = False
'                Dim matchAllWordForms As Object = False
'                Dim forward As Object = True
'                Dim format As Object = False
'                Dim matchKashida As Object = False
'                Dim matchDiacritics As Object = False
'                Dim matchAlefHamza As Object = False
'                Dim matchControl As Object = False
'                Dim read_only As Object = False
'                Dim visible As Object = True
'                Dim replace As Object = 2
'                Dim wrap As Object = 1

'                Dim intFound As Integer = 0
'                Dim rng As Word.Range = currentRange
'                rng.Find.ClearFormatting()

'                rng.Find.Replacement.ClearFormatting()
'                rng.Find.Replacement.Font.ColorIndex = Word.WdColorIndex.wdBlue


'                rng.Find.Forward = True
'                rng.Find.Text = URL
'                rng.Find.Execute(Missing, matchCase, matchWholeWord, matchWildCards, matchSoundsLike, matchAllWordForms, forward, wrap, format, Missing, Missing, matchKashida, matchDiacritics, matchAlefHamza, matchControl)

'                While rng.Find.Found
'                    intFound += 1
'                    currentRange.Fields.Add(rng, WdFieldType.wdFieldHyperlink, URL)
'                    Exit While
'                    rng.Find.Execute(Missing, matchCase, matchWholeWord, matchWildCards, matchSoundsLike, matchAllWordForms, forward, wrap, format, Missing, Missing, matchKashida, matchDiacritics, matchAlefHamza, matchControl)
'                End While

'            Next
'        Catch ex As Exception

'        End Try

'    End Sub

'    Private Sub FindByColor(ByRef currentdoc As Word.Document, Color As Word.WdColorIndex)
'        Try
'            Dim Missing As Object = System.Reflection.Missing.Value

'            Dim matchCase As Object = False
'            Dim matchWholeWord As Object = False
'            Dim matchWildCards As Object = False
'            Dim matchSoundsLike As Object = False
'            Dim matchAllWordForms As Object = False
'            Dim forward As Object = True
'            Dim format As Object = True
'            Dim matchKashida As Object = False
'            Dim matchDiacritics As Object = False
'            Dim matchAlefHamza As Object = False
'            Dim matchControl As Object = False
'            Dim read_only As Object = False
'            Dim visible As Object = True
'            Dim replace As Object = 2
'            Dim wrap As Object = 1



'            currentdoc.Selection.Find.Font.Color = Color
'            For Each aPar As Word.Paragraph In currentdoc.Paragraphs
'                Dim rng As Word.Range = aPar.Range
'                Dim sText As String = rng.Text

'                Dim d = rng.Sentences.Count
'                For Each sentance As Word.Sentences In rng.Sentences
'                    Dim rng2 As Word.Range = sentance.Pa
'                    FindSentenceByColor()
'                Next




'            Next



'            Dim intFound As Integer = 0
'            Dim rng As Word.Range = currentdoc.Range
'            rng.Find.ClearFormatting()

'            'rng.Find.Replacement.ClearFormatting()
'            'rng.Find.Replacement.Font.ColorIndex = Word.WdColorIndex.wdBlue


'            rng.Find.Forward = True
'            rng.Find.Text = String.Empty
'            rng.Find.Font.ColorIndex = Color
'            rng.Find.Execute(Missing, matchCase, matchWholeWord, matchWildCards, matchSoundsLike, matchAllWordForms, forward, wrap, Format, Missing, Missing, matchKashida, matchDiacritics, matchAlefHamza, matchControl)

'            'While (Word.Selection.Find.Execute(ref findStr))
'            hasFound = rng.Find.Execute(replace: Word.WdReplace.wdReplaceAll);
'            While rng.Find.Found()
'                intFound += 1
'                If rng.Text = Nothing Then
'                    Exit While
'                End If
'                rng.Delete()
'                'Exit While
'                ' Dim rng2 = rng





'                'rng.Find.ClearFormatting()
'                'rng.Find.Forward = True
'                'rng.Find.Text = String.Empty
'                'rng.Find.Font.ColorIndex = Color

'                '    currentdoc.Fields.Add(rng, WdFieldType.wdFieldDocVariable, Replacement.Value)
'                rng.Find.Execute(Missing, matchCase, matchWholeWord, matchWildCards, matchSoundsLike, matchAllWordForms, forward, wrap, Format, Missing, Missing, matchKashida, matchDiacritics, matchAlefHamza, matchControl)
'            End While





'            ' Dim rngDoc As Word.Range = currentdoc.Content
'            '  Dim rngFind As Word.Range = rngDoc.Duplicate
'            Dim bFound As Boolean = True
'            ' Dim oCollapseEnd = Word.WdCollapseDirection.wdCollapseEnd

'            While bFound
'                bFound = rngFind.Find.Execute()

'                If bFound Then
'                    rngFind.Collapse(oCollapseEnd)
'                    rngFind.[End] = rngDoc.[End]
'                End If
'            End While



'            Next
'        Catch ex As Exception

'        End Try

'    End Sub

'    Private Sub FindRangeByColor(ByRef currentRange As Word.Range, Color As Word.WdColorIndex)
'        Try
'            Dim Missing As Object = System.Reflection.Missing.Value

'            Dim matchCase As Object = False
'            Dim matchWholeWord As Object = False
'            Dim matchWildCards As Object = False
'            Dim matchSoundsLike As Object = False
'            Dim matchAllWordForms As Object = False
'            Dim forward As Object = True
'            Dim format As Object = True
'            Dim matchKashida As Object = False
'            Dim matchDiacritics As Object = False
'            Dim matchAlefHamza As Object = False
'            Dim matchControl As Object = False
'            Dim read_only As Object = False
'            Dim visible As Object = True
'            Dim replace As Object = 2
'            Dim wrap As Object = 1


'            Dim intFound As Integer = 0
'            Dim rng As Word.Range = currentRange
'            rng.Find.ClearFormatting()

'            rng.Find.Forward = True
'            rng.Find.Text = String.Empty
'            rng.Find.Font.ColorIndex = Color
'            rng.Find.Execute(Missing, matchCase, matchWholeWord, matchWildCards, matchSoundsLike, matchAllWordForms, forward, wrap, format, Missing, Missing, matchKashida, matchDiacritics, matchAlefHamza, matchControl)

'            If rng.Find.Found() = True Then
'                rng.Delete()
'            End If



'        Catch ex As Exception

'        End Try

'    End Sub

'    Private Sub FillFieldValues(ByRef currentdoc As Word.Document)
'        Try
'            Dim PolicyReplacements = DataRepository.GetPolicyReplacements
'            Dim Fields As New List(Of String)
'            Dim Variables As New List(Of String)

'            FindByLine(currentdoc)
'            IterateRanges(currentdoc)
'            IterateListRanges(currentdoc)
'            FindByColor(currentdoc, WdColorIndex.wdRed)
'            For Each V In currentdoc.Variables
'                Variables.Add(V.Name)
'            Next V
'            For Each Field As Field In currentdoc.Fields
'                Fields.Add(Field.Code.Text)
'            Next
'            addBullets()
'            Fields = currentdoc.MainDocumentPart.Document.Descendants(Of Word.Field).ToList()
'            Dim listMergeFields = currentdoc.MainDocumentPart.Document.Descendants(Of FieldCode)().ToList()












'            For Each para As Word.Paragraph In doc.Paragraphs
'                Dim paraRange As Word.Range = para.Range
'                Dim text As String = paraRange.Text
'                Dim regex As Regex = New Regex(findText)
'                Dim final As String = regex.Replace(text, replaceText)
'                paraRange.Text = final
'            Next

'            Regex.Replace(currentdoc.ActiveDocument.Range, InputBox("Replace with:"))

'            For Each ContentRange As PlaceHolder In currentdoc.Content.Find(placeholderRegex)
'                Dim name As String = PlaceHolder.ToString().Trim("[', ']")
'                Dim link As String
'                If linkData.TryGetValue(name, link As out) Then
'                    PlaceHolder.Set(New Hyperlink(Document, link, name).Content);
'                End If
'            Next




'            Dim rngField As Word.Range = Nothing
'            Dim oCollapseEnd As Object = Word.WdCollapseDirection.wdCollapseEnd
'            Dim oMoveCharacter As Object = Word.WdUnits.wdCharacter
'            Dim oOne As Object = 1

'            For Each Field As Field In currentdoc.Fields
'                If Field.Type = WdFieldType.wdFieldDocVariable Then
'                    Select Case Field.Code.Text
'                        Case " DOCVARIABLE  208.1.IV  \* MERGEFORMAT "
'                            rngField = Field.Code

'                            Dim State As String = "MI"
'                            Dim PolicyNo As Integer = 208
'                            Dim Section As Integer = 1
'                            Dim Part As String = "IV"

'                            Dim DocVarText As String = Nothing
'                            DocVarText = PolicyReplacements.Where(Function(p) p.State = State And p.PolicyNo = PolicyNo And p.Section = Section And p.Part = Part).Select(Function(p) p.Text).SingleOrDefault


'                            Dim DocVarText2 = FindReplaceText(DocVarText, "", "")



'                            If DocVarText IsNot Nothing Then
'                                Field.Result.Text = DocVarText
'                                Dim URLFound = FindURL(DocVarText)
'                                FindAndReplace(rngField, URLFound)
'                            Else

'                                Dim listTemplate As ListTemplate = rngField.ListFormat.ListTemplate
'                                rngField.ListFormat.RemoveNumbers()
'                                rngField.Delete()
'                            End If
'                        Case " DOCVARIABLE  208.1.VI  \* MERGEFORMAT "
'                            Dim State As String = "MI"
'                            Dim PolicyNo As Integer = 208
'                            Dim Section As Integer = 1
'                            Dim Part As String = "VI"

'                            Dim DocVarText As String = Nothing
'                            DocVarText = PolicyReplacements.Where(Function(p) p.State = State And p.PolicyNo = PolicyNo And p.Section = Section And p.Part = Part).Select(Function(p) p.Text).SingleOrDefault

'                            If DocVarText IsNot Nothing Then
'                                Field.Result.Text = DocVarText
'                            Else
'                                rngField = Field.Code
'                                Dim listTemplate As ListTemplate = rngField.ListFormat.ListTemplate
'                                rngField.Delete()
'                                rngField.ListFormat.RemoveNumbers()
'                            End If

'                    End Select
'                End If
'            Next



'            currentdoc.Fields.Update()
'        Catch ex As Exception

'        End Try

'    End Sub

'    Private Function FindReplaceText(ByRef currentdoc As Word.Document, FindText As String, ReplaceText As String)
'        Try

'            Dim totalParagraphs As Integer = currentdoc.Paragraphs.Count
'            Dim final As String

'            For i As Integer = 1 To totalParagraphs
'                Dim temp As String = currentdoc.Paragraphs(i).Range.Text
'                Dim x1 As Single = currentdoc.Paragraphs(i).Format.LeftIndent
'                Dim x2 As Single = currentdoc.Paragraphs(i).Format.RightIndent
'                Dim x3 As Single = currentdoc.Paragraphs(i).Format.SpaceBefore
'                Dim x4 As Single = currentdoc.Paragraphs(i).Format.SpaceAfter

'                If temp.Length > 1 Then
'                    Dim regex As Regex = New Regex(FindText)
'                    final = regex.Replace(temp, ReplaceText)

'                    If final <> temp Then
'                        currentdoc.Paragraphs(i).Range.Text = final
'                        currentdoc.Paragraphs(i).Format.LeftIndent = x1
'                        currentdoc.Paragraphs(i).Format.RightIndent = x2
'                        currentdoc.Paragraphs(i).Format.SpaceBefore = x3
'                        currentdoc.Paragraphs(i).Format.SpaceAfter = x4
'                    End If
'                End If
'            Next

'        Catch ex As Exception
'        Finally
'        End Try
'    End Function

'    Private Function FindReplaceText(Text As String, FindText As String, ReplaceText As String) As String
'        Try
'            Dim Final As String = ""

'            Dim linkData As New Dictionary(Of String, String) From {{"FacebookPage1", "https://www.facebook.com"}}
'            Dim placeholderRegex As Regex = New Regex("\[(.*?)\]", RegexOptions.Compiled)

'            If Text.Length > 1 Then
'                Dim regex As Regex = New Regex(FindText)
'                Final = regex.Replace(Text, ReplaceText)

'                If Final <> Text Then
'                    Return Final
'                End If
'            End If


'        Catch ex As Exception
'        Finally
'        End Try

'    End Function
'    Private Sub IterateRanges(ByRef currentdoc As Word.Document)
'        For Each ParaGraph In currentdoc.Paragraphs
'            Dim currentRange = ParaGraph.Range
'            Dim text = currentRange.text
'            If currentRange.Listformat.List IsNot Nothing Then
'                Dim text2 = currentRange.text

'            End If

'            If text.StartsWith("Reviewing the State of Georgia Medicaid Exclusions List available at") Then
'                Dim text2 = currentRange.text
'            End If
'            FindRangeByColor(currentRange, WdColorIndex.wdRed)
'        Next
'    End Sub

'    Private Sub IterateListRanges(ByRef currentdoc As Word.Document)
'        Try


'            For Each ParaGraph In currentdoc.ListTemplate
'                Dim currentRange = ParaGraph.Range
'                Dim text As String = currentRange.text

'                If text.StartsWith("Reviewing the State of Georgia Medicaid Exclusions List available at") Then
'                    Dim text2 = currentRange.text
'                End If
'                If currentRange.Listformat.List IsNot Nothing Then
'                    Dim text2 = currentRange.text

'                End If
'                FindRangeByColor(currentRange, WdColorIndex.wdRed)
'            Next
'        Catch ex As Exception

'        End Try
'    End Sub

'    Private Function FindURL(Text As String) As List(Of String)
'        Dim txt As String = "this is my url http://www.google.com and visit this website and this is my url http://www.yahoo.com"
'        Dim URLFound As New List(Of String)
'        For Each item As Match In Regex.Matches(Text, "(http|ftp|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?")
'            URLFound.Add(item.Value)
'        Next
'        Return URLFound
'    End Function
'    Private Shared Sub AddBullets()
'        Try
'            Dim app As Application = New Application()
'            Dim doc As Document = app.Documents.Add()
'            Dim range As Range = doc.Range(0, 0)

'            range.ListFormat.ApplyNumberDefault()
'            range.Text = "Birinci"
'            range.InsertParagraphAfter()

'            Dim listTemplate As ListTemplate = range.ListFormat.ListTemplate
'            Dim subRange As Range = doc.Range(range.StoryLength - 1)
'            subRange.ListFormat.ApplyBulletDefault()
'            subRange.ListFormat.ListIndent()
'            subRange.Text = "Alt Birinci"
'            subRange.InsertParagraphAfter()

'            Dim sublistTemplate As ListTemplate = subRange.ListFormat.ListTemplate

'            Dim subRange2 As Range = doc.Range(subRange.StoryLength - 1)
'            subRange2.ListFormat.ApplyListTemplate(sublistTemplate)
'            subRange2.ListFormat.ListIndent()
'            subRange2.Text = "Alt İkinci"
'            subRange2.InsertParagraphAfter()

'            Dim range2 As Range = doc.Range(range.StoryLength - 1)
'            range2.ListFormat.ApplyListTemplateWithLevel(listTemplate, True)
'            Dim isContinue As WdContinue = range2.ListFormat.CanContinuePreviousList(listTemplate)
'            range2.Text = "İkinci"
'            range2.InsertParagraphAfter()

'            Dim range3 As Range = doc.Range(range2.StoryLength - 1)
'            range3.ListFormat.ApplyListTemplate(listTemplate)
'            range3.Text = "Üçüncü"
'            range3.InsertParagraphAfter()

'            Dim path As String = Environment.CurrentDirectory
'            Dim totalExistDocx As Integer = Directory.GetFiles(path, "test*.docx").Count()

'            '  path = path.Combine(path, String.Format("test{0}.docx", totalExistDocx + 1))
'            app.ActiveDocument.SaveAs2("c:\CCG\Temp\Test4.docx", WdSaveFormat.wdFormatXMLDocument)
'            doc.Close()
'            Process.Start(path)
'        Catch exception As Exception
'            Throw
'        End Try
'    End Sub


'    Public Function DocumentGenerator(ByVal templatePath As String) As Byte()
'        Dim buffer As Byte()

'        Using stream As MemoryStream = New MemoryStream()
'            buffer = System.IO.File.ReadAllBytes(templatePath)
'            stream.Write(buffer, 0, buffer.Length)

'            Using wordDocument As WordprocessingDocument = WordprocessingDocument.Open(stream, True)
'                Dim listBookMarks = wordDocument.MainDocumentPart.Document.Descendants(Of BookmarkStart)().ToList()
'                Dim listMergeFields = wordDocument.MainDocumentPart.Document.Descendants(Of FieldCode)().ToList()
'                wordDocument.Close()
'            End Using

'            buffer = stream.ToArray()
'        End Using

'        Return buffer
'    End Function


'    Private Sub SurroundingSub()
'        Dim rngField As Word.Range = Nothing
'        Dim oCollapseEnd As Object = Word.WdCollapseDirection.wdCollapseEnd
'        Dim oMoveCharacter As Object = Word.WdUnits.wdCharacter
'        Dim oOne As Object = 1

'        For Each f As Field In Document.Fields

'            If f.Type = WdFieldType.wdFieldIndexEntry Then
'                rngField = f.Code
'                rngField.Collapse(oCollapseEnd)
'                rngField.MoveStart(oMoveCharacter, oOne)
'                rngField.InsertAfter("{{Some After text}}")
'            End If
'        Next
'    End Sub

'    Class SurroundingClass
'        Private Shared regExHttpLinks As Regex = New Regex("(?<=\()\b(https?://|www\.)[-A-Za-z0-9+&@#/%?=~_()|!:,.;]*[-A-Za-z0-9+&@#/%=~_()|](?=\))|(?<=(?<wrap>[=~|_#]))\b(https?://|www\.)[-A-Za-z0-9+&@#/%?=~_()|!:,.;]*[-A-Za-z0-9+&@#/%=~_()|](?=\k<wrap>)|\b(https?://|www\.)[-A-Za-z0-9+&@#/%?=~_()|!:,.;]*[-A-Za-z0-9+&@#/%=~_()|]", RegexOptions.Compiled Or RegexOptions.IgnoreCase)

'        <Extension()>
'        Public Shared Function Format(ByVal htmlHelper As HtmlHelper, ByVal html As String) As String
'            If String.IsNullOrEmpty(html) Then
'                Return html
'            End If

'            html = htmlHelper.Encode(html)
'            html = html.Replace(Environment.NewLine, "<br />")
'            Dim periodReplacement = "[[[replace:period]]]"
'            html = Regex.Replace(html, "(?<=\d)\.(?=\d)", periodReplacement)
'            Dim linkMatches = regExHttpLinks.Matches(html)

'            For i As Integer = 0 To linkMatches.Count - 1
'                Dim temp = linkMatches(i).ToString()

'                If Not temp.Contains("://") Then
'                    temp = "http://" & temp
'                End If

'                html = html.Replace(linkMatches(i).ToString(), String.Format("<a href=""{0}"" title=""{0}"">{1}</a>", temp.Replace(".", periodReplacement).ToLower(), linkMatches(i).ToString().Replace(".", periodReplacement)))
'            Next

'            html = html.Replace(periodReplacement, ".")
'            Return html
'        End Function
'    End Class

'    Private Sub SurroundingSub()
'        Dim misValue As Object = System.Reflection.Missing.Value
'        Dim wordApp As Microsoft.Office.Interop.Word.Application = New Microsoft.Office.Interop.Word.Application()
'        Dim docPth As Object = "c:\tmp\aDoc.doc"
'        Dim aDoc As Microsoft.Office.Interop.Word.Document = wordApp.Documents.Open(docPth, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue)
'        wordApp.Visible = True

'        For Each aPar As Microsoft.Office.Interop.Word.Paragraph In aDoc.Paragraphs
'            Dim parRng As Microsoft.Office.Interop.Word.Range = aPar.Range
'            Dim sText As String = parRng.Text
'            Dim sList As String = parRng.ListFormat.ListString
'            Dim nLevel As Integer = parRng.ListFormat.ListLevelNumber
'            MessageBox.Show("Text = " & sText & " - List = " & sList & " - Level " & nLevel.ToString())
'        Next
'    End Sub





'    Private Function RangeStoryTypeIsHeaderOrFooter(ByVal range As Range) As Boolean
'        Return (range.StoryType = WdStoryType.wdEvenPagesHeaderStory OrElse range.StoryType = WdStoryType.wdPrimaryHeaderStory OrElse range.StoryType = WdStoryType.wdEvenPagesFooterStory OrElse range.StoryType = WdStoryType.wdPrimaryFooterStory OrElse range.StoryType = WdStoryType.wdFirstPageHeaderStory OrElse range.StoryType = WdStoryType.wdFirstPageFooterStory)
'    End Function

'    Private Function CurrentRangeHaveShapeRanges(ByVal range As Range) As Boolean
'        Return range.ShapeRange.Count > 0
'    End Function
'    Private Sub FindByLine(word As Word.Application, ByRef currentdoc As Word.Document)
'        Dim word As Word.Application = New Word.Application()
'        Dim miss As Object = System.Reflection.Missing.Value
'        Dim path As Object = "D:\viewstate.docx"
'        Dim [readOnly] As Object = True
'        Dim docs As Microsoft.Office.Interop.Word.Document = word.Documents.Open(path, miss, [readOnly], miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss)
'        Dim totaltext As String = ""
'        Dim unit As Object = WdUnits.wdLine
'        Dim count As Object = 1
'        word.Selection.MoveEnd(unit, count)
'        totaltext = word.Selection.Text
'        TextBox1.Text = totaltext
'        currentdoc.Close(miss, miss, miss)
'        Word.Quit(miss, miss, miss)
'        docs = Nothing
'        word = Nothing
'    End Sub

'End Class



