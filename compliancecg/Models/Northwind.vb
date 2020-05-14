''INSTANT VB TODO TASK: There is no equivalent to #line in VB:
'Imports System
'Imports System.Collections
'Imports System.Collections.Generic
'Imports System.ComponentModel.DataAnnotations
'Imports System.Data
'Imports System.Data.Entity
'Imports System.Data.Linq
'Imports System.Linq
'Imports System.Web
'Imports CCGData.compliancecg
'Imports compliancecg.compliancecg

'Namespace DevExpress.Web.Demos.Mvc
'    Public NotInheritable Class NorthwindDataProvider

'        Private Sub New()
'        End Sub

'        Private Const NorthwindDataContextKey As String = "DXNorthwindDataContext"
'        Public Shared ReadOnly Property DB() As NorthwindContext
'            Get
'                If HttpContext.Current.Items(NorthwindDataContextKey) Is Nothing Then
'                    HttpContext.Current.Items(NorthwindDataContextKey) = New NorthwindContext()
'                End If
'                Return DirectCast(HttpContext.Current.Items(NorthwindDataContextKey), NorthwindContext)
'            End Get
'        End Property
'        Private Shared Function CalculateAveragePrice(ByVal categoryID As Integer) As Double
'            Return CDbl((
'                From product In DB.Products
'                Where product.CategoryID = categoryID
'                Select product).Average(Function(s) s.UnitPrice))
'        End Function
'        Private Shared Function CalculateMinPrice(ByVal categoryID As Integer) As Double
'            Return CDbl((
'                From product In DB.Products
'                Where product.CategoryID = categoryID
'                Select product).Min(Function(s) s.UnitPrice))
'        End Function
'        Private Shared Function CalculateMaxPrice(ByVal categoryID As Integer) As Double
'            Return CDbl((
'                From product In DB.Products
'                Where product.CategoryID = categoryID
'                Select product).Max(Function(s) s.UnitPrice))
'        End Function
'        Public Shared Function GetCategories() As IEnumerable
'            Dim query = From category In DB.Categories
'                        Select New With {Key .CategoryID = category.CategoryID, Key .CategoryName = category.CategoryName, Key .Description = category.Description, Key .Picture = category.Picture}
'            Return query.ToList()
'        End Function
'        Public Shared Function GetCategoryByID(ByVal categoryID As Integer) As Category
'            Return (
'                From category In DB.Categories
'                Where category.CategoryID = categoryID
'                Select category).SingleOrDefault()
'        End Function
'        Public Shared Function GetCategoryNameById(ByVal id As Integer) As String
'            Dim category As Category = GetCategoryByID(id)
'            Return If(category IsNot Nothing, category.CategoryName, Nothing)
'        End Function
'        Public Shared Function GetCategoriesNames() As IEnumerable
'            Return From category In DB.Categories
'                   Select category.CategoryName
'        End Function
'        Public Shared Function GetCategoriesAverage() As IEnumerable
'            Dim query = DB.Categories.ToList().Select(Function(category) New With {Key category.CategoryName, Key .AvgPrice = CalculateAveragePrice(category.CategoryID)})
'            Return query.ToList()
'        End Function
'        Public Shared Function GetCategoriesMin() As IEnumerable
'            Dim query = DB.Categories.ToList().Select(Function(category) New With {Key category.CategoryName, Key .AvgPrice = CalculateMinPrice(category.CategoryID)})
'            Return query.ToList()
'        End Function
'        Public Shared Function GetCategoriesMax() As IEnumerable
'            Dim query = DB.Categories.ToList().Select(Function(category) New With {Key category.CategoryName, Key .AvgPrice = CalculateMaxPrice(category.CategoryID)})
'            Return query.ToList()
'        End Function
'        Public Shared Function GetSuppliers() As IEnumerable
'            Return DB.Suppliers.ToList()
'        End Function
'        Public Shared Function GetCustomers() As IEnumerable
'            Return DB.Customers.ToList()
'        End Function
'        Public Shared Function GetCustomerByID(ByVal customerID As String) As Customer
'            Return (
'                From customer In DB.Customers
'                Where customer.CustomerID = customerID
'                Select New Customer With {.CompanyName = customer.CompanyName, .ContactName = customer.ContactName, .ContactTitle = customer.ContactTitle, .Address = customer.Address, .City = customer.City, .Country = customer.Country, .Fax = customer.Fax, .Phone = customer.Phone}).SingleOrDefault()
'        End Function
'        Public Shared Function GetFirstCustomerID() As String
'            Return (
'                From customer In NorthwindDataProvider.DB.Customers
'                Select customer.CustomerID).First()
'        End Function
'        Public Shared Function GetProducts() As IEnumerable
'            Return DB.Products.ToList()
'        End Function
'        Public Shared Function GetProducts(ByVal categoryName As String) As IEnumerable
'            Dim query = From product In DB.Products
'                        Join category In DB.Categories On product.CategoryID Equals category.CategoryID
'                        Where category.CategoryName = categoryName
'                        Select product
'            Return query.ToList()
'        End Function
'        Public Shared Function GetProducts(ByVal categoryID? As Integer) As IEnumerable
'            Return DB.Products.Where(Function(p) p.CategoryID = categoryID).ToList()
'        End Function
'        Public Shared Function GetEmployees() As IEnumerable
'            Return DB.Employees.ToList()
'        End Function
'        Public Shared Function GetEmployeesOrders() As IEnumerable
'            Dim query = From orders In DB.Orders
'                        Join employee In DB.Employees On orders.EmployeeID Equals employee.EmployeeID
'                        Select New EmployeeOrder With {.OrderDate = orders.OrderDate, .Freight = orders.Freight, .LastName = employee.LastName, .FirstName = employee.FirstName, .Photo = employee.Photo, .Id = employee.EmployeeID}
'            Return query.ToList()
'        End Function
'        'Public Shared Function GetEmployeePhoto(ByVal lastName As String) As Binary
'        '    Return (
'        '        From employee In DB.Employees
'        '        Where employee.LastName = lastName
'        '        Select employee.Photo).SingleOrDefault()
'        'End Function
'        'Public Shared Function GetEmployeePhoto(ByVal employeeId As Integer) As Binary
'        '    Return (
'        '        From employee In DB.Employees
'        '        Where employee.EmployeeID = employeeId
'        '        Select employee.Photo).SingleOrDefault()
'        'End Function
'        Public Shared Function GetEmployeeNotes(ByVal employeeId As Integer) As String
'            Return (
'                From employee In DB.Employees
'                Where employee.EmployeeID = employeeId
'                Select employee.Notes).Single()
'        End Function
'        Public Shared Function GetEditableEmployees() As IList(Of EditableEmployee)
'            Const SessionKey As String = "DXDemoEmployees"
'            Dim employeesList As IList(Of EditableEmployee) = TryCast(HttpContext.Current.Session(SessionKey), IList(Of EditableEmployee))
'            If employeesList Is Nothing Then
'                employeesList = (
'                    From employee In DB.Employees
'                    Select New EditableEmployee With {.EmployeeID = employee.EmployeeID, .FirstName = employee.FirstName, .LastName = employee.LastName, .Title = employee.Title, .HomePhone = employee.HomePhone, .BirthDate = employee.BirthDate, .HireDate = employee.HireDate, .Notes = employee.Notes, .ReportsTo = employee.ReportsTo, .Photo = employee.Photo}).ToList()
'                HttpContext.Current.Session(SessionKey) = employeesList
'            End If
'            Return employeesList
'        End Function
'        Public Shared Function GetEditableEmployeeByID(ByVal employeeID As Integer) As EditableEmployee
'            Return GetEditableEmployees().Where(Function(e) e.EmployeeID = employeeID).SingleOrDefault()
'        End Function
'        Public Shared Function GetNewEditableEmployeeID() As Integer
'            Dim editableEmployees As IEnumerable(Of EditableEmployee) = GetEditableEmployees()
'            Return If(editableEmployees.Count() > 0, editableEmployees.Last().EmployeeID + 1, 0)
'        End Function
'        Public Shared Sub UpdateEditableEmployee(ByVal employee As EditableEmployee)
'            Dim editableEmployee = GetEditableEmployees().Where(Function(e) e.EmployeeID = employee.EmployeeID).SingleOrDefault()
'            If editableEmployee Is Nothing Then
'                Return
'            End If
'            editableEmployee.FirstName = employee.FirstName
'            editableEmployee.LastName = employee.LastName
'            editableEmployee.Title = employee.Title
'            editableEmployee.BirthDate = employee.BirthDate
'            editableEmployee.HireDate = employee.HireDate
'            editableEmployee.Notes = employee.Notes
'            editableEmployee.ReportsTo = employee.ReportsTo
'            editableEmployee.HomePhone = employee.HomePhone
'            editableEmployee.Photo = employee.Photo
'        End Sub
'        Public Shared Sub InsertEditableEmployee(ByVal employee As EditableEmployee)
'            Dim editEmployee As New EditableEmployee()
'            editEmployee.EmployeeID = GetNewEditableEmployeeID()
'            editEmployee.FirstName = employee.FirstName
'            editEmployee.LastName = employee.LastName
'            editEmployee.BirthDate = employee.BirthDate
'            editEmployee.HireDate = employee.HireDate
'            editEmployee.Title = employee.Title
'            editEmployee.Notes = employee.Notes
'            editEmployee.ReportsTo = employee.ReportsTo
'            editEmployee.Photo = employee.Photo
'            GetEditableEmployees().Add(editEmployee)
'        End Sub
'        Public Shared Function GetEditableCustomers() As IList(Of EditableCustomer)
'            Const SessionKey As String = "DXDemoCustomers"
'            Dim customersList As IList(Of EditableCustomer) = TryCast(HttpContext.Current.Session(SessionKey), IList(Of EditableCustomer))
'            If customersList Is Nothing Then
'                customersList = (
'                    From customer In DB.Customers
'                    Select New EditableCustomer With {.CustomerID = customer.CustomerID, .CompanyName = customer.CompanyName, .ContactName = customer.ContactName, .City = customer.City, .Region = customer.Region, .Country = customer.Country}).ToList()
'                HttpContext.Current.Session(SessionKey) = customersList
'            End If
'            Return customersList
'        End Function
'        Public Shared Function GetEditableCustomerByID(ByVal customerID As String) As EditableCustomer
'            Return GetEditableCustomers().Where(Function(c) c.CustomerID.Equals(customerID)).SingleOrDefault()
'        End Function
'        Public Shared Sub UpdateCustomer(ByVal customer As EditableCustomer)
'            Dim editableCustomer As EditableCustomer = GetEditableCustomerByID(customer.CustomerID)
'            If editableCustomer Is Nothing Then
'                Return
'            End If
'            editableCustomer.CompanyName = customer.CompanyName
'            editableCustomer.ContactName = customer.ContactName
'            editableCustomer.City = customer.City
'            editableCustomer.Country = customer.Country
'            editableCustomer.Region = customer.Region
'        End Sub
'        Public Shared Function GetOrders() As IEnumerable
'            Return DB.Orders.ToList()
'        End Function
'        Public Shared Function GetInvoices() As IEnumerable
'            Dim query = From invoice In DB.Invoices
'                        Join customer In DB.Customers On invoice.CustomerID Equals customer.CustomerID
'                        Select New Mvc.Invoice() With {.CompanyName = customer.CompanyName, .City = invoice.City, .Region = invoice.Region, .Country = invoice.Country, .UnitPrice = invoice.UnitPrice, .Quantity = invoice.Quantity, .Discount = invoice.Discount}
'            Return query.ToList()
'        End Function
'        Public Shared Function GetFullInvoices() As IEnumerable
'            Dim query = From invoice In DB.Invoices
'                        Join customer In DB.Customers On invoice.CustomerID Equals customer.CustomerID
'                        Join order In DB.Orders On invoice.OrderID Equals order.OrderID
'                        Select New With {Key .SalesPerson = order.Employee.FirstName & " " & order.Employee.LastName, Key customer.CompanyName, Key invoice.Address, Key invoice.City, Key invoice.Country, Key invoice.CustomerName, Key invoice.Discount, Key invoice.ExtendedPrice, Key invoice.Freight, Key invoice.OrderDate, Key invoice.ProductName, Key invoice.Quantity, Key invoice.Region, Key invoice.UnitPrice}
'            Return query.ToList()
'        End Function
'        Public Shared Function GetInvoices(ByVal customerID As String) As IEnumerable
'            Return DB.Invoices.Where(Function(i) i.CustomerID = customerID).ToList()
'        End Function
'        Public Shared Function GetEditableProducts() As IList(Of EditableProduct)
'            Dim products As IList(Of EditableProduct) = DirectCast(HttpContext.Current.Session("Products"), IList(Of EditableProduct))
'            If products Is Nothing Then
'                products = (
'                    From product In DB.Products
'                    Select New EditableProduct With {.ProductID = product.ProductID, .ProductName = product.ProductName, .CategoryID = product.CategoryID, .SupplierID = product.SupplierID, .QuantityPerUnit = product.QuantityPerUnit, .UnitPrice = product.UnitPrice, .UnitsInStock = product.UnitsInStock, .Discontinued = product.Discontinued}).ToList()
'                HttpContext.Current.Session("Products") = products
'            End If
'            Return products
'        End Function
'        Public Shared Function GetEditableProduct(ByVal productID As Integer) As EditableProduct
'            Return (
'                From product In GetEditableProducts()
'                Where product.ProductID = productID
'                Select product).FirstOrDefault()
'        End Function
'        Public Shared Function GetNewEditableProductID() As Integer
'            Dim editableProducts As IEnumerable(Of EditableProduct) = GetEditableProducts()
'            Return If(editableProducts.Count() > 0, editableProducts.Last().ProductID + 1, 0)
'        End Function
'        Public Shared Sub DeleteProduct(ByVal productID As Integer)
'            Dim product As EditableProduct = GetEditableProduct(productID)
'            If product IsNot Nothing Then
'                GetEditableProducts().Remove(product)
'            End If
'        End Sub
'        Public Shared Sub InsertProduct(ByVal product As EditableProduct)
'            Dim editProduct As New EditableProduct()
'            editProduct.ProductID = GetNewEditableProductID()
'            editProduct.ProductName = product.ProductName
'            editProduct.CategoryID = product.CategoryID
'            editProduct.SupplierID = product.SupplierID
'            editProduct.QuantityPerUnit = product.QuantityPerUnit
'            editProduct.UnitPrice = product.UnitPrice
'            editProduct.UnitsInStock = product.UnitsInStock
'            editProduct.Discontinued = product.Discontinued
'            GetEditableProducts().Add(editProduct)
'        End Sub
'        Public Shared Sub UpdateProduct(ByVal product As EditableProduct)
'            Dim editProduct As EditableProduct = GetEditableProduct(product.ProductID)
'            If editProduct IsNot Nothing Then
'                editProduct.ProductName = product.ProductName
'                editProduct.CategoryID = product.CategoryID
'                editProduct.SupplierID = product.SupplierID
'                editProduct.QuantityPerUnit = product.QuantityPerUnit
'                editProduct.UnitPrice = product.UnitPrice
'                editProduct.UnitsInStock = product.UnitsInStock
'                editProduct.Discontinued = product.Discontinued
'            End If
'        End Sub
'        Public Shared Function GetEmployeesList() As IEnumerable
'            Return DB.Employees.ToList().Select(Function(e) New With {Key .ID = e.EmployeeID, Key .Name = e.LastName & " " & e.FirstName})
'        End Function
'        Public Shared Function GetFirstEmployeeID() As Integer
'            Return DB.Employees.First().EmployeeID
'        End Function
'        Public Shared Function GetEmployee(ByVal employeeId As Integer) As Employee
'            Return DB.Employees.Single(Function(e) employeeId = e.EmployeeID)
'        End Function
'        Public Shared Function GetOrders(ByVal employeeID As Integer) As IEnumerable
'            Dim query = From order In DB.Orders
'                        Where order.EmployeeID = employeeID
'                        Join order_detail In DB.Order_Details On order.OrderID Equals order_detail.OrderID
'                        Join customer In DB.Customers On order.CustomerID Equals customer.CustomerID
'                        Select New With {Key order.OrderID, Key order.OrderDate, Key order.ShipName, Key order.ShipCountry, Key order.ShipCity, Key order.ShipAddress, Key order.ShippedDate, Key order_detail.Quantity, Key order_detail.UnitPrice, Key customer.CustomerID, Key customer.ContactName, Key customer.CompanyName, Key customer.City, Key customer.Address, Key customer.Phone, Key customer.Fax}
'            Return query.ToList()
'        End Function
'        Public Shared Function GetProductReports() As IEnumerable
'            Dim query = From sale In DB.Sales_by_Categories
'                        Join invoice In DB.Invoices On sale.ProductName Equals invoice.ProductName
'                        Where invoice.ShippedDate IsNot Nothing
'                        Select New With {Key sale.CategoryName, Key sale.ProductName, Key sale.ProductSales, Key invoice.ShippedDate}
'            Return query.ToList()
'        End Function
'        Public Shared Function GetCustomerReports() As IEnumerable
'            Dim maxOrderDate = If(DB.Orders.Max(Function(o) o.OrderDate), Date.Today)
'            Dim actualOrderYearDelta = Date.Today.Year - maxOrderDate.Year
'            Dim query = From customer In DB.Customers
'                        Join order In DB.Orders On customer.CustomerID Equals order.CustomerID
'                        Join order_detail In DB.Order_Details On order.OrderID Equals order_detail.OrderID
'                        Join product In DB.Products On order_detail.ProductID Equals product.ProductID
'                        Select New With {Key product.ProductName, Key customer.CompanyName, Key .OrderDate = DbFunctions.CreateDateTime(order.OrderDate.Value.Year + actualOrderYearDelta, order.OrderDate.Value.Month, order.OrderDate.Value.Day, 0, 0, 0), Key .ProductAmount = CSng(order_detail.UnitPrice * order_detail.Quantity) * (1 - order_detail.Discount / 100) * 100}
'            Return query.ToList()
'        End Function
'        Public Shared Function GetSalesPerson() As IEnumerable
'            Return DB.SalesPersons.ToList()
'        End Function
'    End Class
'    Public Class EditableProduct
'        Public Property ProductID() As Integer
'        <Required(ErrorMessage:="Product Name is required"), StringLength(50, ErrorMessage:="Must be under 50 characters")>
'        Public Property ProductName() As String
'        <Required(ErrorMessage:="Category is required")>
'        Public Property CategoryID() As Integer?
'        Public Property SupplierID() As Integer?
'        <StringLength(100, ErrorMessage:="Must be under 100 characters")>
'        Public Property QuantityPerUnit() As String
'        <Range(0, 10000, ErrorMessage:="Must be between 0 and 10000$")>
'        Public Property UnitPrice() As Decimal?
'        <Range(0, 1000, ErrorMessage:="Must be between 0 and 1000")>
'        Public Property UnitsInStock() As Short?

'        Private discontinued_Renamed? As Boolean
'        Public Property Discontinued() As Boolean?
'            Get
'                Return discontinued_Renamed
'            End Get
'            Set(ByVal value? As Boolean)
'                discontinued_Renamed = If(value Is Nothing, False, value)
'            End Set
'        End Property
'    End Class
'    Public Class Invoice
'        Public Property CompanyName() As String
'        Public Property City() As String
'        Public Property Region() As String
'        Public Property Country() As String
'        Public Property UnitPrice() As Decimal
'        Public Property Quantity() As Short
'        Public Property Discount() As Single
'    End Class
'    Public Class EditableEmployee
'        Public Property EmployeeID() As Integer
'        <Required(ErrorMessage:="First Name is required"), StringLength(10, ErrorMessage:="Must be under 10 characters"), Display(Name:="First Name")>
'        Public Property FirstName() As String
'        <Required(ErrorMessage:="Last Name is required"), StringLength(20, ErrorMessage:="Must be under 20 characters"), Display(Name:="Last Name")>
'        Public Property LastName() As String
'        <StringLength(30, ErrorMessage:="Must be under 30 characters"), Display(Name:="Position")>
'        Public Property Title() As String
'        <StringLength(24, ErrorMessage:="Must be under 24 characters"), Display(Name:="Home Phone")>
'        Public Property HomePhone() As String
'        <Display(Name:="Birth Date")>
'        Public Property BirthDate() As Date?
'        <Display(Name:="Hire Date")>
'        Public Property HireDate() As Date?
'        Public Property Notes() As String
'        Public Property ReportsTo() As Integer?
'        Public Property Photo() As Byte()
'    End Class
'    Public Class EditableCustomer
'        Public Property CustomerID() As String
'        <Required(ErrorMessage:="Company Name is required"), StringLength(40, ErrorMessage:="Must be under 40 characters")>
'        Public Property CompanyName() As String
'        <StringLength(30, ErrorMessage:="Must be under 30 characters")>
'        Public Property ContactName() As String
'        <StringLength(15, ErrorMessage:="Must be under 15 characters")>
'        Public Property City() As String
'        <StringLength(15, ErrorMessage:="Must be under 15 characters")>
'        Public Property Region() As String
'        <StringLength(15, ErrorMessage:="Must be under 15 characters")>
'        Public Property Country() As String
'    End Class
'    Public Class EmployeeOrder
'        Public Property OrderDate() As Date?
'        Public Property Freight() As Decimal?
'        Public Property Id() As Decimal?
'        Public Property LastName() As String
'        Public Property FirstName() As String
'        Public Property Photo() As Byte()
'    End Class
'End Namespace
