Imports DevExpress.Web.Mvc
Imports DevExpress.Web.Office
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports T534498.Models

Namespace Controllers
    Public Class HomeController
        Inherits Controller

        Public Function Index() As ActionResult
            Session("InvoiceID") = 0
            Return View()
        End Function

        <HttpPost>
        Public Function OpenMergedFile(ByVal Optional InvoiceID As Integer = 0) As ActionResult
            Dim documentId As String = "mailMergeDocId"
            Session("InvoiceID") = InvoiceID
            Dim documentServer As RichEditDocumentServer = New RichEditDocumentServer()
            documentServer.LoadDocument(Server.MapPath("~/App_Data/MailMergeTemplate.docx"))
            documentServer.Options.MailMerge.DataSource = Invoice.GetInvoiceData().Where(Function(i) i.Id = InvoiceID).ToList(Of Invoice)()
            documentServer.Document.CalculateDocumentVariable += AddressOf Document_CalculateDocumentVariable

            Using stream As MemoryStream = New MemoryStream()
                documentServer.MailMerge(stream, DocumentFormat.OpenXml)
                stream.Position = 0
                DocumentManager.CloseDocument(documentId)
                Return RichEditExtension.Open("RichEdit", documentId, DocumentFormat.OpenXml, Function() stream)
            End Using
        End Function

        Public Function RichEditPartial() As ActionResult
            Return PartialView("_RichEditPartial")
        End Function

        Private Sub Document_CalculateDocumentVariable(ByVal sender As Object, ByVal e As CalculateDocumentVariableEventArgs)
            If e.VariableName = "CustomerDetails" Then
                Dim InvoiceId As Integer = Convert.ToInt32(e.Arguments(0).Value)
                Dim customersData As List(Of Customer) = Customer.GetCustomersByInvoice(InvoiceId)
                Dim detailDocServer As RichEditDocumentServer = New RichEditDocumentServer()
                Dim detailDocServerResult As RichEditDocumentServer = New RichEditDocumentServer()
                detailDocServer.LoadDocument(Server.MapPath("~/App_Data/MedicationBody.rtf"))
                detailDocServer.Options.MailMerge.DataSource = CookedData()
                detailDocServer.MailMerge(detailDocServerResult.Document)
                Dim headerDoc As RichEditDocumentServer = New RichEditDocumentServer()
                headerDoc.LoadDocument(Server.MapPath("~/App_Data/MedicationHeader.rtf"))
                headerDoc.Document.AppendDocumentContent(detailDocServerResult.Document.Range)
                e.Value = headerDoc.Document
                e.Handled = True
            End If
        End Sub

        Private Function CookedData() As List(Of Student)
            Dim list = New List(Of Student)()
            Dim obj1 = New Student() With {
                .PatientAge = "AAAAA",
                .PatientState = "AAAAAHTMl"
            }
            Dim obj2 = New Student() With {
                .PatientAge = "BBBBB",
                .PatientState = "BBBBBHTMl"
            }
            list.Add(obj1)
            list.Add(obj2)
            Return list
        End Function

        Public Class Student
            Public Property PatientAge As String
            Public Property PatientState As String
        End Class
    End Class
End Namespace
