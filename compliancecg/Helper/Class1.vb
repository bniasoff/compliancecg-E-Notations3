'Imports System
'Imports System.Linq
'Imports DocumentFormat.OpenXml.Packaging
'Imports DocumentFormat.OpenXml.Wordprocessing

'Module Module1
'    Sub Main()
'        ' Apply the Heading 3 style to a paragraph.   
'        Dim fileName As String = "C:\Users\Public\Documents\WordProcessingEx.docx"
'        Using myDocument As WordprocessingDocument = WordprocessingDocument.Open(fileName, True)
'            ' Get the first paragraph.  
'            Dim p As Paragraph = myDocument.MainDocumentPart.Document.Body.Elements(Of Paragraph)().First()

'            ' If the paragraph has no ParagraphProperties object, create a new one.  
'            If p.Elements(Of ParagraphProperties)().Count() = 0 Then
'                p.PrependChild(Of ParagraphProperties)(New ParagraphProperties())
'            End If

'            ' Get the ParagraphProperties element of the paragraph.  
'            Dim pPr As ParagraphProperties = p.Elements(Of ParagraphProperties)().First()

'            ' Set the value of ParagraphStyleId to "Heading3".  
'            pPr.ParagraphStyleId = New ParagraphStyleId() With {.Val = "Heading3"}
'        End Using
'        Console.WriteLine("All done. Press a key.")
'        Console.ReadKey()
'    End Sub
'End Module



Imports DevExpress.XtraRichEdit.API.Native

Public Class SectionsMerger
    Public Shared Sub Append(ByVal source As Document, ByVal target As Document)
        Dim lastSectionIndexBeforeAppending As Integer = target.Sections.Count - 1
        Dim sourceSectionCount As Integer = source.Sections.Count

        ' Append document body
        target.AppendRtfText(source.RtfText)

        For i As Integer = 0 To sourceSectionCount - 1
            Dim sourceSection As Section = source.Sections(i)
            Dim SubDocument As SubDocument = sourceSection.BeginUpdateFooter(HeaderFooterType.Odd)
            sourceSection.EndUpdateFooter(sourceSection)

            Dim targetSection As Section = target.Sections(lastSectionIndexBeforeAppending + i)

            '' Copy standard header/footer
            'AppendHeader(sourceSection, targetSection, HeaderFooterType.Odd)
            'AppendFooter(sourceSection, targetSection, HeaderFooterType.Odd)
        Next i
    End Sub

    Private Shared Sub AppendHeader(ByVal sourceSection As Section, ByVal targetSection As Section, ByVal headerFooterType As HeaderFooterType)
        Dim source As SubDocument = sourceSection.BeginUpdateHeader(headerFooterType)
        Dim target As SubDocument = targetSection.BeginUpdateHeader(headerFooterType)
        target.Delete(target.Range)
        target.InsertDocumentContent(target.Range.Start, source.Range, InsertOptions.KeepSourceFormatting)

        ' Delete empty paragraphs
        Dim emptyParagraph As DocumentRange = target.CreateRange(target.Range.End.ToInt() - 2, 2)
        target.Delete(emptyParagraph)

        sourceSection.EndUpdateHeader(source)
        targetSection.EndUpdateFooter(target)
    End Sub

    Private Shared Sub AppendFooter(ByVal sourceSection As Section, ByVal targetSection As Section, ByVal headerFooterType As HeaderFooterType)
        Dim source As SubDocument = sourceSection.BeginUpdateFooter(headerFooterType)
        Dim target As SubDocument = targetSection.BeginUpdateFooter(headerFooterType)
        target.Delete(target.Range)
        target.InsertDocumentContent(target.Range.Start, source.Range, InsertOptions.KeepSourceFormatting)

        ' Delete empty paragraphs
        Dim emptyParagraph As DocumentRange = target.CreateRange(target.Range.End.ToInt() - 2, 2)
        target.Delete(emptyParagraph)

        sourceSection.EndUpdateFooter(source)
        targetSection.EndUpdateFooter(target)
    End Sub
End Class