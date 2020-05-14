Imports System.Web.Mvc
Imports gridmvclocalization.Models
Imports Syncfusion.EJ2.Base
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web



Namespace Controllers
    Public Class GridController
        Inherits Controller

        Public Shared dropdata As List(Of drodpowndatadetails) = New List(Of drodpowndatadetails)()

        Public Function Index() As ActionResult
            BinddataSource()
            ViewBag.dropdowndata = dropdata
            Return View()
        End Function

        Public Function BinddataSource() As List(Of drodpowndatadetails)
            If dropdata.Count() = 0 Then
                dropdata.Add(New drodpowndatadetails() With {
                    .text = "Simons bistro",
                    .value = 1
                })
                dropdata.Add(New drodpowndatadetails() With {
                    .text = "Queen Cozinha",
                    .value = 2
                })
                dropdata.Add(New drodpowndatadetails() With {
                    .text = "Frankenversand",
                    .value = 3
                })
                dropdata.Add(New drodpowndatadetails() With {
                    .text = "Ernst Handel",
                    .value = 4
                })
                dropdata.Add(New drodpowndatadetails() With {
                    .text = "Hanari Carnes",
                    .value = 5
                })
            End If

            Return dropdata
        End Function

        Public Function UrlDatasource(ByVal dm As DataManagerRequest) As ActionResult
            Dim DataSource As IEnumerable = OrdersDetails.GetAllRecords()
            Dim operation As DataOperations = New DataOperations()
            Dim str As List(Of String) = New List(Of String)()

            If dm.Search IsNot Nothing AndAlso dm.Search.Count > 0 Then
                DataSource = operation.PerformSearching(DataSource, dm.Search)
            End If

            If dm.Sorted IsNot Nothing AndAlso dm.Sorted.Count > 0 Then
                DataSource = operation.PerformSorting(DataSource, dm.Sorted)
            End If

            If dm.Where IsNot Nothing AndAlso dm.Where.Count > 0 Then
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where(0).[Operator])
            End If

            Dim count As Integer = DataSource.Cast(Of OrdersDetails)().Count()

            If dm.Skip <> 0 Then
                DataSource = operation.PerformSkip(DataSource, dm.Skip)
            End If

            If dm.Take <> 0 Then
                DataSource = operation.PerformTake(DataSource, dm.Take)
            End If

            Return If(dm.RequiresCounts, Json(New With {Key .result = DataSource, Key .count = count
            }), Json(DataSource))
        End Function

        Public Function Update(ByVal value As OrdersDetails) As ActionResult
            Dim ord = value
            Dim val As OrdersDetails = OrdersDetails.GetAllRecords().Where(Function([or]) [or].OrderID = ord.OrderID).FirstOrDefault()
            val.OrderID = ord.OrderID
            val.EmployeeID = ord.EmployeeID
            val.CustomerID = ord.CustomerID
            val.ShipName = ord.ShipName
            Return Json(value)
        End Function

        Public Function Insert(ByVal value As OrdersDetails) As ActionResult
            OrdersDetails.GetAllRecords().Insert(0, value)
            Return Json(New With {Key .data = value
            })
        End Function

        Public Function Delete(ByVal key As Integer) As ActionResult
            OrdersDetails.GetAllRecords().Remove(OrdersDetails.GetAllRecords().Where(Function([or]) [or].OrderID = key).FirstOrDefault())
            Dim data = OrdersDetails.GetAllRecords()
            Return Json(data)
        End Function

        Public Function About() As ActionResult
            ViewBag.Message = "Your application description page."
            Return View()
        End Function

        Public Function Contact() As ActionResult
            ViewBag.Message = "Your contact page."
            Return View()
        End Function

        Public Class Data
            Public Property requiresCounts As Boolean
            Public Property skip As Integer
            Public Property take As Integer
        End Class
    End Class

    Public Class drodpowndatadetails
        Public Sub New()
        End Sub

        Public Sub New(ByVal text As String, ByVal value As Integer)
            Me.text = text
            Me.value = value
        End Sub

        Public Property text As String
        Public Property value As Integer
    End Class

    Public Class OrdersDetails
        Public Shared order As List(Of OrdersDetails) = New List(Of OrdersDetails)()

        Public Sub New()
        End Sub

        Public Sub New(ByVal OrderID As Integer, ByVal CustomerId As String, ByVal EmployeeId As Integer, ByVal Freight As Double, ByVal Verified As Boolean, ByVal OrderDate As DateTime, ByVal ShipCity As String, ByVal ShipName As String, ByVal ShipCountry As String, ByVal ShippedDate As DateTime, ByVal ShipAddress As String)
            Me.OrderID = OrderID
            Me.CustomerID = CustomerId
            Me.EmployeeID = EmployeeId
            Me.Freight = Freight
            Me.ShipCity = ShipCity
            Me.Verified = Verified
            Me.OrderDate = OrderDate
            Me.ShipName = ShipName
            Me.ShipCountry = ShipCountry
            Me.ShippedDate = ShippedDate
            Me.ShipAddress = ShipAddress
        End Sub

        Public Shared Function GetAllRecords() As List(Of OrdersDetails)
            If order.Count() = 0 Then
                Dim code As Integer = 10000

                For i As Integer = 1 To 5 - 1
                    order.Add(New OrdersDetails(code + 1, "ALFKI", 1, 2.3 * i, False, New DateTime(1991, 5, 15), "Berlin", "Simons bistro", "Denmark", New DateTime(1996, 7, 16), "Kirchgasse 6"))
                    order.Add(New OrdersDetails(code + 2, "ANATR", 2, 3.3 * i, True, New DateTime(1990, 4, 4), "Madrid", "Queen Cozinha", "Brazil", New DateTime(1996, 9, 11), "Avda. Azteca 123"))
                    order.Add(New OrdersDetails(code + 3, "ANTON", 3, 4.3 * i, True, New DateTime(1957, 11, 30), "Cholchester", "Frankenversand", "Germany", New DateTime(1996, 10, 7), "Carrera 52 con Ave. Bolívar #65-98 Llano Largo"))
                    order.Add(New OrdersDetails(code + 4, "BLONP", 4, 5.3 * i, False, New DateTime(1930, 10, 22), "Marseille", "Ernst Handel", "Austria", New DateTime(1996, 12, 30), "Magazinweg 7"))
                    order.Add(New OrdersDetails(code + 5, "BOLID", 5, 6.3 * i, True, New DateTime(1953, 2, 18), "Tsawassen", "Hanari Carnes", "Switzerland", New DateTime(1997, 12, 3), "1029 - 12th Ave. S."))
                    code += 5
                Next
            End If

            Return order
        End Function

        Public Property OrderID As Integer?
        Public Property CustomerID As String
        Public Property EmployeeID As Integer?
        Public Property Freight As Double?
        Public Property ShipCity As String
        Public Property Verified As Boolean
        Public Property OrderDate As DateTime
        Public Property ShipName As String
        Public Property ShipCountry As String
        Public Property ShippedDate As DateTime
        Public Property ShipAddress As String
    End Class
End Namespace
