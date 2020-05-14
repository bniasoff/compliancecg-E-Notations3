Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

Namespace Models
    Public Class Customer
        Public Property CustomerID As Integer
        Public Property CustomerName As String

        Public Shared Function GetCustomersByInvoice(ByVal InvoiceID As Integer) As List(Of Customer)
            Return Enumerable.Range(0, InvoiceID + 1).[Select](Function(j) New Customer With {
                .CustomerID = InvoiceID * 10 + j,
                .CustomerName = "Name_" & InvoiceID * 10 & "_" & j
            }).ToList
        End Function
    End Class

    Public Class Invoice
            Public Property Id As Integer?
            Public Property Description As String
            Public Property Price As Decimal?
            Public Property RegisterDate As DateTime?
            Public Property Customers As List(Of Customer)

            Public Shared Function GetInvoiceData() As List(Of Invoice)
                Dim invoices As List(Of Invoice) = New List(Of Invoice)()
                Const count As Integer = 3

                For i As Integer = 0 To count - 1
                    Dim customers As List(Of Customer) = Customer.GetCustomersByInvoice(i)
                    invoices.Add(New Invoice() With {
                    .Id = i,
                    .Description = "Invoice" & i.ToString(),
                    .Price = i * 10,
                    .RegisterDate = DateTime.Today.AddDays(i - count),
                    .Customers = customers
                })
                Next

                Return invoices
            End Function
        End Class




    End Namespace
