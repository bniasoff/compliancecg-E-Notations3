Imports Syncfusion.DocIO.DLS

Namespace RemoveParagraphs

    Class Program

        Private Shared Sub Main(args As String())

            'Opens an existing document from file system through constructor of WordDocument class

            Dim document As New WordDocument("TestDocument.docx")

            'Processes the body contents for each section in the Word document

            For Each section As WSection In document.Sections

                'Accesses the Body of section where all the contents in document are apart

                Dim sectionBody As WTextBody = section.Body

                IterateTextBody(sectionBody)

                Dim headersFooters As WHeadersFooters = section.HeadersFooters

                'Consider that OddHeader and OddFooter are applied to this document

                'Iterates through the text body of OddHeader and OddFooter

                IterateTextBody(headersFooters.OddHeader)

                IterateTextBody(headersFooters.OddFooter)

            Next

            'Saves and closes the document instance

            document.Save("Result.docx")

            document.Close()

            System.Diagnostics.Process.Start("Result.docx")

        End Sub

        Private Shared Sub IterateTextBody(textBody As WTextBody)

            'Iterates through the each of the child items of WTextBody

            For i As Integer = 0 To textBody.ChildEntities.Count - 1

                'IEntity is the basic unit in DocIO DOM. 

                'Accesses the body items (should be either paragraph or table) as IEntity

                Dim bodyItemEntity As IEntity = textBody.ChildEntities(i)

                'A Text body has 2 types of elements - Paragraph and Table

                'decide the element type using EntityType

                Select Case bodyItemEntity.EntityType

                    Case EntityType.Paragraph

                        Dim paragraph As WParagraph = TryCast(bodyItemEntity, WParagraph)

                        'Checks for a particular style name and removes the paragraph from DOM

                        If paragraph.StyleName = "MyStyle" Then

                            Dim index As Integer = textBody.ChildEntities.IndexOf(paragraph)

                            textBody.ChildEntities.RemoveAt(index)

                        End If

                        Exit Select

                    Case EntityType.Table

                        'Table is a collection of rows and cells

                        'Iterates through table's DOM

                        IterateTable(TryCast(bodyItemEntity, WTable))

                        Exit Select

                End Select

            Next

        End Sub

        Private Shared Sub IterateTable(table As WTable)

            'Iterates the row collection in a table

            For Each row As WTableRow In table.Rows

                'Iterates the cell collection in a table row

                For Each cell As WTableCell In row.Cells

                    'Table cell is derived from (also a) TextBody

                    'Reusing the code meant for iterating TextBody

                    IterateTextBody(cell)

                Next

            Next

        End Sub

    End Class

End Namespace
