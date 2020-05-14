'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration
Namespace DevExpress.Web.Demos
    Partial Public Class NorthwindContext
        Inherits ContextBase

        Shared Sub New()
            Database.SetInitializer(Of NorthwindContext)(Nothing)
        End Sub
        Public Sub New()
            MyBase.New("NorthwindConnectionString")
        End Sub
        Public Property Alphabetical_list_of_products() As DbSet(Of Alphabetical_list_of_product)
        Public Property Categories() As DbSet(Of Category)
        Public Property Category_Sales_for_1997() As DbSet(Of Category_Sales_for_1997)
        Public Property Current_Product_Lists() As DbSet(Of Current_Product_List)
        Public Property Customer_and_Suppliers_by_Cities() As DbSet(Of Customer_and_Suppliers_by_City)
        Public Property CustomerCustomerDemoes() As DbSet(Of CustomerCustomerDemo)
        Public Property CustomerDemographics() As DbSet(Of CustomerDemographic)
        Public Property Customers() As DbSet(Of Customer)
        Public Property Employees() As DbSet(Of Employee)
        Public Property Order_Details() As DbSet(Of Order_Detail)
        Public Property Order_Details_Extendeds() As DbSet(Of Order_Details_Extended)
        Public Property Order_Subtotals() As DbSet(Of Order_Subtotal)
        Public Property Orders() As DbSet(Of Order)
        Public Property Orders_Qries() As DbSet(Of Orders_Qry)
        Public Property Product_Sales_for_1997() As DbSet(Of Product_Sales_for_1997)
        Public Property Products() As DbSet(Of Product)
        Public Property Products_Above_Average_Prices() As DbSet(Of Products_Above_Average_Price)
        Public Property Products_by_Categories() As DbSet(Of Products_by_Category)
        Public Property Regions() As DbSet(Of Region)
        Public Property Sales_by_Categories() As DbSet(Of Sales_by_Category)
        Public Property Sales_Totals_by_Amounts() As DbSet(Of Sales_Totals_by_Amount)
        Public Property Shippers() As DbSet(Of Shipper)
        Public Property Summary_of_Sales_by_Quarters() As DbSet(Of Summary_of_Sales_by_Quarter)
        Public Property Summary_of_Sales_by_Years() As DbSet(Of Summary_of_Sales_by_Year)
        Public Property Suppliers() As DbSet(Of Supplier)
        Public Property Territories() As DbSet(Of Territory)
        Public Property CategoryProducts() As DbSet(Of CategoryProduct)
        Public Property CustomerReports() As DbSet(Of CustomerReport)
        Public Property Invoices() As DbSet(Of Invoice)
        Public Property OrderDetails() As DbSet(Of OrderDetail)
        Public Property OrderReports() As DbSet(Of OrderReport)
        Public Property ProductReports() As DbSet(Of ProductReport)
        Public Property SalesPersons() As DbSet(Of SalesPerson)
        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Configurations.Add(New Alphabetical_list_of_productMap())
            modelBuilder.Configurations.Add(New CategoryMap())
            modelBuilder.Configurations.Add(New Category_Sales_for_1997Map())
            modelBuilder.Configurations.Add(New Current_Product_ListMap())
            modelBuilder.Configurations.Add(New Customer_and_Suppliers_by_CityMap())
            modelBuilder.Configurations.Add(New CustomerCustomerDemoMap())
            modelBuilder.Configurations.Add(New CustomerDemographicMap())
            modelBuilder.Configurations.Add(New CustomerMap())
            modelBuilder.Configurations.Add(New EmployeeMap())
            modelBuilder.Configurations.Add(New Order_DetailMap())
            modelBuilder.Configurations.Add(New Order_Details_ExtendedMap())
            modelBuilder.Configurations.Add(New Order_SubtotalMap())
            modelBuilder.Configurations.Add(New OrderMap())
            modelBuilder.Configurations.Add(New Orders_QryMap())
            modelBuilder.Configurations.Add(New Product_Sales_for_1997Map())
            modelBuilder.Configurations.Add(New ProductMap())
            modelBuilder.Configurations.Add(New Products_Above_Average_PriceMap())
            modelBuilder.Configurations.Add(New Products_by_CategoryMap())
            modelBuilder.Configurations.Add(New RegionMap())
            modelBuilder.Configurations.Add(New Sales_by_CategoryMap())
            modelBuilder.Configurations.Add(New Sales_Totals_by_AmountMap())
            modelBuilder.Configurations.Add(New ShipperMap())
            modelBuilder.Configurations.Add(New Summary_of_Sales_by_QuarterMap())
            modelBuilder.Configurations.Add(New Summary_of_Sales_by_YearMap())
            modelBuilder.Configurations.Add(New SupplierMap())
            modelBuilder.Configurations.Add(New TerritoryMap())
            modelBuilder.Configurations.Add(New CategoryProductMap())
            modelBuilder.Configurations.Add(New CustomerReportMap())
            modelBuilder.Configurations.Add(New InvoiceMap())
            modelBuilder.Configurations.Add(New OrderDetailMap())
            modelBuilder.Configurations.Add(New OrderReportMap())
            modelBuilder.Configurations.Add(New ProductReportMap())
            modelBuilder.Configurations.Add(New SalesPersonMap())
        End Sub
    End Class
    Partial Public Class NorthwindContextSL
        Inherits DbContext

        Shared Sub New()
            Database.SetInitializer(Of NorthwindContextSL)(Nothing)
        End Sub
        Public Sub New()
            MyBase.New("NorthwindConnectionStringSL")
        End Sub
        Public Property AlphabeticalListOfProducts() As DbSet(Of Alphabetical_list_of_product)
        Public Property Categories() As DbSet(Of Category)
        Public Property CurrentProductList() As DbSet(Of Current_Product_List)
        Public Property CustomerAndSuppliersByCity() As DbSet(Of Customer_and_Suppliers_by_City)
        Public Property CustomerCustomerDemoes() As DbSet(Of CustomerCustomerDemo)
        Public Property CustomerDemographics() As DbSet(Of CustomerDemographic)
        Public Property Customers() As DbSet(Of Customer)
        Public Property Employees() As DbSet(Of Employee)
        Public Property OrderDetailsExtended() As DbSet(Of Order_Details_Extended)
        Public Property OrderSubtotals() As DbSet(Of Order_Subtotal)
        Public Property Orders() As DbSet(Of Order)
        Public Property Order_Details() As DbSet(Of Order_Detail)
        Public Property OrdersQry() As DbSet(Of Orders_Qry)
        Public Property Products() As DbSet(Of Product)
        Public Property ProductsAboveAveragePrice() As DbSet(Of Products_Above_Average_Price)
        Public Property ProductsByCategory() As DbSet(Of Products_by_Category)
        Public Property Regions() As DbSet(Of Region)
        Public Property SalesByCategory() As DbSet(Of Sales_by_Category)
        Public Property SalesTotalsByAmount() As DbSet(Of Sales_Totals_by_Amount)
        Public Property Shippers() As DbSet(Of Shipper)
        Public Property SummaryOfSalesByQuarter() As DbSet(Of Summary_of_Sales_by_Quarter)
        Public Property SummaryOfSalesByYear() As DbSet(Of Summary_of_Sales_by_Year)
        Public Property Suppliers() As DbSet(Of Supplier)
        Public Property Territories() As DbSet(Of Territory)
        Public Property CategoryProducts() As DbSet(Of CategoryProduct)
        Public Property CustomerReports() As DbSet(Of CustomerReport)
        Public Property Invoices() As DbSet(Of Invoice)
        Public Property OrderDetails() As DbSet(Of OrderDetail)
        Public Property OrderReports() As DbSet(Of OrderReport)
        Public Property ProductReports() As DbSet(Of ProductReport)
        Public Property SalesPersons() As DbSet(Of SalesPerson)
        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Configurations.Add(New AlphabeticalListOfProductsMap())
            modelBuilder.Configurations.Add(New CategoryMap())
            modelBuilder.Configurations.Add(New CurrentProductListMap())
            modelBuilder.Configurations.Add(New CustomerAndSuppliersByCityMap())
            modelBuilder.Configurations.Add(New CustomerCustomerDemoMap())
            modelBuilder.Configurations.Add(New CustomerDemographicMap())
            modelBuilder.Configurations.Add(New CustomerMap())
            modelBuilder.Configurations.Add(New EmployeeMap())
            modelBuilder.Configurations.Add(New OrderDetailsExtendedMap())
            modelBuilder.Configurations.Add(New OrderSubtotalsMap())
            modelBuilder.Configurations.Add(New OrderMap())
            modelBuilder.Configurations.Add(New OrdersQryMap())
            modelBuilder.Configurations.Add(New Order_DetailMap())
            modelBuilder.Configurations.Add(New ProductMap())
            modelBuilder.Configurations.Add(New ProductsAboveAveragePriceMap())
            modelBuilder.Configurations.Add(New ProductsByCategoryMap())
            modelBuilder.Configurations.Add(New RegionMap())
            modelBuilder.Configurations.Add(New SalesByCategoryMap())
            modelBuilder.Configurations.Add(New SalesTotalsByAmountMap())
            modelBuilder.Configurations.Add(New ShipperMap())
            modelBuilder.Configurations.Add(New SummaryOfSalesByQuarterMap())
            modelBuilder.Configurations.Add(New SummaryOfSalesByYearMap())
            modelBuilder.Configurations.Add(New SupplierMap())
            modelBuilder.Configurations.Add(New TerritoryMap())
            modelBuilder.Configurations.Add(New CategoryProductMap())
            modelBuilder.Configurations.Add(New CustomerReportMap())
            modelBuilder.Configurations.Add(New InvoiceMap())
            modelBuilder.Configurations.Add(New OrderDetailMap())
            modelBuilder.Configurations.Add(New OrderReportMap())
            modelBuilder.Configurations.Add(New ProductReportMap())
            modelBuilder.Configurations.Add(New SalesPersonMap())
        End Sub
    End Class
    Partial Public Class Alphabetical_list_of_product
        Public Property ProductID() As Integer
        Public Property ProductName() As String
        Public Property SupplierID() As Integer?
        Public Property CategoryID() As Integer?
        Public Property QuantityPerUnit() As String
        Public Property UnitPrice() As Decimal?
        Public Property UnitsInStock() As Short?
        Public Property UnitsOnOrder() As Short?
        Public Property ReorderLevel() As Short?
        Public Property Discontinued() As Boolean
        Public Property CategoryName() As String
        Public Overridable Property Category() As Category
        Public Overridable Property Product() As Product
        Public Overridable Property Supplier() As Supplier
    End Class
    Partial Public Class Category
        Public Sub New()
            Me.Products = New List(Of Product)()
        End Sub
        Public Property CategoryID() As Integer
        Public Property CategoryName() As String
        Public Property Description() As String
        Public Property Picture() As Byte()
        Public Overridable Property Products() As ICollection(Of Product)
    End Class
    Partial Public Class Category_Sales_for_1997
        Public Property CategoryName() As String
        Public Property CategorySales() As Decimal?
    End Class
    Partial Public Class CategoryProduct
        Public Property ProductID() As Integer
        Public Property SupplierID() As Integer?
        Public Property ProductName() As String
        Public Property CategoryName() As String
        Public Property Picture() As Byte()
        Public Property Description() As String
    End Class
    Partial Public Class Current_Product_List
        Public Property ProductID() As Integer
        Public Property ProductName() As String
        Public Overridable Property Product() As Product
    End Class
    Partial Public Class Customer
        Public Sub New()
            Me.Orders_Qry = New List(Of Orders_Qry)()
        End Sub
        Public Property CustomerID() As String
        Public Property CompanyName() As String
        Public Property ContactName() As String
        Public Property ContactTitle() As String
        Public Property Address() As String
        Public Property City() As String
        Public Property Region() As String
        Public Property PostalCode() As String
        Public Property Country() As String
        Public Property Phone() As String
        Public Property Fax() As String
        Public Overridable Property Orders_Qry() As ICollection(Of Orders_Qry)
    End Class
    Partial Public Class Customer_and_Suppliers_by_City
        Public Property City() As String
        Public Property CompanyName() As String
        Public Property ContactName() As String
        Public Property Relationship() As String
    End Class
    Partial Public Class CustomerCustomerDemo
        Public Property CustomerID() As String
        Public Property CustomerTypeID() As String
    End Class
    Partial Public Class CustomerDemographic
        Public Property CustomerTypeID() As String
        Public Property CustomerDesc() As String
    End Class
    Partial Public Class CustomerReport
        Public Property ProductName() As String
        Public Property CompanyName() As String
        Public Property OrderDate() As Date?
        Public Property ProductAmount() As Decimal?
    End Class
    Partial Public Class Employee
        Public Sub New()
            Me.Orders_Qry = New List(Of Orders_Qry)()
            Me.Orders = New List(Of Order)()
            Me.Orders1 = New List(Of Order)()
            Me.Territories = New List(Of Territory)()
        End Sub
        Public Property EmployeeID() As Integer
        Public Property LastName() As String
        Public Property FirstName() As String
        Public Property Title() As String
        Public Property TitleOfCourtesy() As String
        Public Property BirthDate() As Date?
        Public Property HireDate() As Date?
        Public Property Address() As String
        Public Property City() As String
        Public Property Region() As String
        Public Property PostalCode() As String
        Public Property Country() As String
        Public Property HomePhone() As String
        Public Property Extension() As String
        Public Property Photo() As Byte()
        Public Property Notes() As String
        Public Property ReportsTo() As Integer?
        Public Property PhotoPath() As String
        Public Overridable Property Orders_Qry() As ICollection(Of Orders_Qry)
        Public Overridable Property Orders() As ICollection(Of Order)
        Public Overridable Property Orders1() As ICollection(Of Order)
        Public Overridable Property Territories() As ICollection(Of Territory)
    End Class
    Partial Public Class Invoice
        Public Property ShipName() As String
        Public Property ShipAddress() As String
        Public Property ShipCity() As String
        Public Property ShipRegion() As String
        Public Property ShipPostalCode() As String
        Public Property ShipCountry() As String
        Public Property CustomerID() As String
        Public Property CustomerName() As String
        Public Property Address() As String
        Public Property City() As String
        Public Property Region() As String
        Public Property PostalCode() As String
        Public Property Country() As String
        Public Property Salesperson() As String
        Public Property OrderID() As Integer
        Public Property OrderDate() As Date?
        Public Property RequiredDate() As Date?
        Public Property ShippedDate() As Date?
        Public Property ShipperName() As String
        Public Property ProductID() As Integer
        Public Property ProductName() As String
        Public Property UnitPrice() As Decimal
        Public Property Quantity() As Short
        Public Property Discount() As Single
        Public Property ExtendedPrice() As Decimal?
        Public Property Freight() As Decimal?
    End Class
    Partial Public Class Order
        Public Sub New()
            Me.Order_Details = New List(Of Order_Detail)()
            Me.Order_Details_Extended = New List(Of Order_Details_Extended)()
            Me.Orders_Qry = New List(Of Orders_Qry)()
            Me.Sales_Totals_by_Amount = New List(Of Sales_Totals_by_Amount)()
        End Sub
        Public Property OrderID() As Integer
        Public Property CustomerID() As String
        Public Property EmployeeID() As Integer?
        Public Property OrderDate() As Date?
        Public Property RequiredDate() As Date?
        Public Property ShippedDate() As Date?
        Public Property ShipVia() As Integer?
        Public Property Freight() As Decimal?
        Public Property ShipName() As String
        Public Property ShipAddress() As String
        Public Property ShipCity() As String
        Public Property ShipRegion() As String
        Public Property ShipPostalCode() As String
        Public Property ShipCountry() As String
        Public Overridable Property Employee() As Employee
        Public Overridable Property Employee1() As Employee
        Public Overridable Property Order_Details() As ICollection(Of Order_Detail)
        Public Overridable Property Order_Details_Extended() As ICollection(Of Order_Details_Extended)
        Public Overridable Property Order_Subtotals() As Order_Subtotal
        Public Overridable Property Orders_Qry() As ICollection(Of Orders_Qry)
        Public Overridable Property Sales_Totals_by_Amount() As ICollection(Of Sales_Totals_by_Amount)
        Public Overridable Property Summary_of_Sales_by_Quarter() As Summary_of_Sales_by_Quarter
        Public Overridable Property Summary_of_Sales_by_Year() As Summary_of_Sales_by_Year
    End Class
    Partial Public Class Order_Detail
        Public Property OrderID() As Integer
        Public Property ProductID() As Integer
        Public Property UnitPrice() As Decimal
        Public Property Quantity() As Short
        Public Property Discount() As Single
        Public Overridable Property Order() As Order
        Public Overridable Property Product() As Product
    End Class
    Partial Public Class Order_Details_Extended
        Public Property OrderID() As Integer
        Public Property ProductID() As Integer
        Public Property ProductName() As String
        Public Property UnitPrice() As Decimal
        Public Property Quantity() As Short
        Public Property Discount() As Single
        Public Property ExtendedPrice() As Decimal?
        Public Overridable Property Order() As Order
        Public Overridable Property Product() As Product
    End Class
    Partial Public Class Order_Subtotal
        Public Property OrderID() As Integer
        Public Property Subtotal() As Decimal?
        Public Overridable Property Order() As Order
    End Class
    Partial Public Class OrderDetail
        Public Property OrderID() As Integer
        Public Property ProductID() As Integer
        Public Property Quantity() As Short
        Public Property UnitPrice() As Decimal
        Public Property Discount() As Single
        Public Property ProductName() As String
        Public Property Supplier() As String
    End Class
    Partial Public Class OrderReport
        Public Property OrderID() As Integer
        Public Property ProductID() As Integer
        Public Property ProductName() As String
        Public Property UnitPrice() As Decimal
        Public Property Quantity() As Short
        Public Property Discount() As Single
        Public Property Extended_Price() As Decimal?
    End Class
    Partial Public Class Orders_Qry
        Public Property OrderID() As Integer
        Public Property CustomerID() As String
        Public Property EmployeeID() As Integer?
        Public Property OrderDate() As Date?
        Public Property RequiredDate() As Date?
        Public Property ShippedDate() As Date?
        Public Property ShipVia() As Integer?
        Public Property Freight() As Decimal?
        Public Property ShipName() As String
        Public Property ShipAddress() As String
        Public Property ShipCity() As String
        Public Property ShipRegion() As String
        Public Property ShipPostalCode() As String
        Public Property ShipCountry() As String
        Public Property CompanyName() As String
        Public Property Address() As String
        Public Property City() As String
        Public Property Region() As String
        Public Property PostalCode() As String
        Public Property Country() As String
        Public Overridable Property Customer() As Customer
        Public Overridable Property Employee() As Employee
        Public Overridable Property Order() As Order
    End Class
    Partial Public Class Product
        Public Sub New()
        End Sub
        Public Property ProductID() As Integer
        Public Property ProductName() As String
        Public Property SupplierID() As Integer?
        Public Property CategoryID() As Integer?
        Public Property QuantityPerUnit() As String
        Public Property UnitPrice() As Decimal?
        Public Property UnitsInStock() As Short?
        Public Property UnitsOnOrder() As Short?
        Public Property ReorderLevel() As Short?
        Public Property Discontinued() As Boolean
        Public Property EAN13() As String
        Public Overridable Property Category() As Category
        Public Overridable Property Supplier() As Supplier
    End Class
    Partial Public Class Product_Sales_for_1997
        Public Property CategoryName() As String
        Public Property ProductName() As String
        Public Property ProductSales() As Decimal?
    End Class
    Partial Public Class ProductReport
        Public Property CategoryName() As String
        Public Property ProductName() As String
        Public Property ProductSales() As Decimal?
        Public Property ShippedDate() As Date?
    End Class
    Partial Public Class Products_Above_Average_Price
        Public Property ProductName() As String
        Public Property UnitPrice() As Decimal?
    End Class
    Partial Public Class Products_by_Category
        Public Property CategoryName() As String
        Public Property ProductName() As String
        Public Property QuantityPerUnit() As String
        Public Property UnitsInStock() As Short?
        Public Property Discontinued() As Boolean
    End Class
    Partial Public Class Region
        Public Sub New()
            Me.Territories = New List(Of Territory)()
        End Sub
        Public Property RegionID() As Integer
        Public Property RegionDescription() As String
        Public Overridable Property Territories() As ICollection(Of Territory)
    End Class
    Partial Public Class Sales_by_Category
        Public Property CategoryID() As Integer
        Public Property CategoryName() As String
        Public Property ProductName() As String
        Public Property ProductSales() As Decimal?
        Public Overridable Property Category() As Category
    End Class
    Partial Public Class Sales_Totals_by_Amount
        Public Property SaleAmount() As Decimal?
        Public Property OrderID() As Integer
        Public Property CompanyName() As String
        Public Property ShippedDate() As Date?
        Public Overridable Property Order() As Order
    End Class
    Partial Public Class SalesTotalsByAmount
        Public Property SaleAmount() As Decimal?
        Public Property OrderID() As Integer
        Public Property CompanyName() As String
        Public Property ShippedDate() As Date?
        Public Overridable Property Order() As Order
    End Class
    Partial Public Class SalesPerson
        Public Property OrderID() As Integer
        Public Property Country() As String
        Public Property FirstName() As String
        Public Property LastName() As String
        Public Property ProductName() As String
        Public Property CategoryName() As String
        Public Property OrderDate() As Date?
        Public Property UnitPrice() As Decimal
        Public Property Quantity() As Short
        Public Property Discount() As Single
        Public Property Extended_Price() As Decimal?
        Public Property Sales_Person() As String
    End Class
    Partial Public Class Shipper
        Public Property ShipperID() As Integer
        Public Property CompanyName() As String
        Public Property Phone() As String
    End Class
    Partial Public Class Summary_of_Sales_by_Quarter
        Public Property ShippedDate() As Date?
        Public Property OrderID() As Integer
        Public Property Subtotal() As Decimal?
        Public Overridable Property Order() As Order
    End Class
    Partial Public Class Summary_of_Sales_by_Year
        Public Property ShippedDate() As Date?
        Public Property OrderID() As Integer
        Public Property Subtotal() As Decimal?
        Public Overridable Property Order() As Order
    End Class
    Partial Public Class Supplier
        Public Sub New()
            Me.Products = New List(Of Product)()
        End Sub
        Public Property SupplierID() As Integer
        Public Property CompanyName() As String
        Public Property ContactName() As String
        Public Property ContactTitle() As String
        Public Property Address() As String
        Public Property City() As String
        Public Property Region() As String
        Public Property PostalCode() As String
        Public Property Country() As String
        Public Property Phone() As String
        Public Property Fax() As String
        Public Property HomePage() As String
        Public Overridable Property Products() As ICollection(Of Product)
    End Class
    Partial Public Class Territory
        Public Sub New()
            Me.Employees = New List(Of Employee)()
        End Sub
        Public Property TerritoryID() As String
        Public Property TerritoryDescription() As String
        Public Property RegionID() As Integer
        Public Overridable Property Region() As Region
        Public Overridable Property Employees() As ICollection(Of Employee)
    End Class
#Region "Mapping"
    Public Class Alphabetical_list_of_productMap
        Inherits EntityTypeConfiguration(Of Alphabetical_list_of_product)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.ProductID, Key t.ProductName, Key t.Discontinued, Key t.CategoryName})
            Me.Property(Function(t) t.ProductID)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.QuantityPerUnit).HasMaxLength(20)
            Me.Property(Function(t) t.CategoryName).IsRequired().HasMaxLength(15)
            Me.ToTable("Alphabetical list of products")
            Me.Property(Function(t) t.ProductID).HasColumnName("ProductID")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.SupplierID).HasColumnName("SupplierID")
            Me.Property(Function(t) t.CategoryID).HasColumnName("CategoryID")
            Me.Property(Function(t) t.QuantityPerUnit).HasColumnName("QuantityPerUnit")
            Me.Property(Function(t) t.UnitPrice).HasColumnName("UnitPrice")
            Me.Property(Function(t) t.UnitsInStock).HasColumnName("UnitsInStock")
            Me.Property(Function(t) t.UnitsOnOrder).HasColumnName("UnitsOnOrder")
            Me.Property(Function(t) t.ReorderLevel).HasColumnName("ReorderLevel")
            Me.Property(Function(t) t.Discontinued).HasColumnName("Discontinued")
            Me.Property(Function(t) t.CategoryName).HasColumnName("CategoryName")
        End Sub
    End Class
    Public Class AlphabeticalListOfProductsMap
        Inherits EntityTypeConfiguration(Of Alphabetical_list_of_product)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.ProductID, Key t.ProductName, Key t.Discontinued, Key t.CategoryName})
            Me.Property(Function(t) t.ProductID)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.QuantityPerUnit).HasMaxLength(20)
            Me.Property(Function(t) t.CategoryName).IsRequired().HasMaxLength(15)
            Me.ToTable("AlphabeticalListOfProducts")
            Me.Property(Function(t) t.ProductID).HasColumnName("ProductID")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.SupplierID).HasColumnName("SupplierID")
            Me.Property(Function(t) t.CategoryID).HasColumnName("CategoryID")
            Me.Property(Function(t) t.QuantityPerUnit).HasColumnName("QuantityPerUnit")
            Me.Property(Function(t) t.UnitPrice).HasColumnName("UnitPrice")
            Me.Property(Function(t) t.UnitsInStock).HasColumnName("UnitsInStock")
            Me.Property(Function(t) t.UnitsOnOrder).HasColumnName("UnitsOnOrder")
            Me.Property(Function(t) t.ReorderLevel).HasColumnName("ReorderLevel")
            Me.Property(Function(t) t.Discontinued).HasColumnName("Discontinued")
            Me.Property(Function(t) t.CategoryName).HasColumnName("CategoryName")
        End Sub
    End Class
    Public Class Category_Sales_for_1997Map
        Inherits EntityTypeConfiguration(Of Category_Sales_for_1997)

        Public Sub New()
            Me.HasKey(Function(t) t.CategoryName)
            Me.Property(Function(t) t.CategoryName).IsRequired().HasMaxLength(15)
            Me.ToTable("Category Sales for 1997")
            Me.Property(Function(t) t.CategoryName).HasColumnName("CategoryName")
            Me.Property(Function(t) t.CategorySales).HasColumnName("CategorySales")
        End Sub
    End Class
    Public Class CategoryMap
        Inherits EntityTypeConfiguration(Of Category)

        Public Sub New()
            Me.HasKey(Function(t) t.CategoryID)
            Me.Property(Function(t) t.CategoryName).IsRequired().HasMaxLength(15)
            Me.ToTable("Categories")
            Me.Property(Function(t) t.CategoryID).HasColumnName("CategoryID")
            Me.Property(Function(t) t.CategoryName).HasColumnName("CategoryName")
            Me.Property(Function(t) t.Description).HasColumnName("Description")
            Me.Property(Function(t) t.Picture).HasColumnName("Picture")
        End Sub
    End Class
    Public Class CategoryProductMap
        Inherits EntityTypeConfiguration(Of CategoryProduct)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.ProductID, Key t.ProductName, Key t.CategoryName})
            Me.Property(Function(t) t.ProductID)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.CategoryName).IsRequired().HasMaxLength(15)
            Me.ToTable("CategoryProducts")
            Me.Property(Function(t) t.ProductID).HasColumnName("ProductID")
            Me.Property(Function(t) t.SupplierID).HasColumnName("SupplierID")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.CategoryName).HasColumnName("CategoryName")
            Me.Property(Function(t) t.Picture).HasColumnName("Picture")
            Me.Property(Function(t) t.Description).HasColumnName("Description")
        End Sub
    End Class
    Public Class Current_Product_ListMap
        Inherits EntityTypeConfiguration(Of Current_Product_List)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.ProductID, Key t.ProductName})
            Me.Property(Function(t) t.ProductID)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.ToTable("Current Product List")
            Me.Property(Function(t) t.ProductID).HasColumnName("ProductID")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
        End Sub
    End Class
    Public Class CurrentProductListMap
        Inherits EntityTypeConfiguration(Of Current_Product_List)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.ProductID, Key t.ProductName})
            Me.Property(Function(t) t.ProductID)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.ToTable("CurrentProductList")
            Me.Property(Function(t) t.ProductID).HasColumnName("ProductID")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
        End Sub
    End Class
    Public Class Customer_and_Suppliers_by_CityMap
        Inherits EntityTypeConfiguration(Of Customer_and_Suppliers_by_City)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.CompanyName, Key t.Relationship})
            Me.Property(Function(t) t.City).HasMaxLength(15)
            Me.Property(Function(t) t.CompanyName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.ContactName).HasMaxLength(30)
            Me.Property(Function(t) t.Relationship).IsRequired().HasMaxLength(9)
            Me.ToTable("Customer and Suppliers by City")
            Me.Property(Function(t) t.City).HasColumnName("City")
            Me.Property(Function(t) t.CompanyName).HasColumnName("CompanyName")
            Me.Property(Function(t) t.ContactName).HasColumnName("ContactName")
            Me.Property(Function(t) t.Relationship).HasColumnName("Relationship")
        End Sub
    End Class
    Public Class CustomerAndSuppliersByCityMap
        Inherits EntityTypeConfiguration(Of Customer_and_Suppliers_by_City)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.CompanyName, Key t.Relationship})
            Me.Property(Function(t) t.City).HasMaxLength(15)
            Me.Property(Function(t) t.CompanyName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.ContactName).HasMaxLength(30)
            Me.Property(Function(t) t.Relationship).IsRequired().HasMaxLength(9)
            Me.ToTable("CustomerAndSuppliersByCity")
            Me.Property(Function(t) t.City).HasColumnName("City")
            Me.Property(Function(t) t.CompanyName).HasColumnName("CompanyName")
            Me.Property(Function(t) t.ContactName).HasColumnName("ContactName")
            Me.Property(Function(t) t.Relationship).HasColumnName("Relationship")
        End Sub
    End Class
    Public Class CustomerCustomerDemoMap
        Inherits EntityTypeConfiguration(Of CustomerCustomerDemo)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.CustomerID, Key t.CustomerTypeID})
            Me.Property(Function(t) t.CustomerID).IsRequired().IsFixedLength().HasMaxLength(5)
            Me.Property(Function(t) t.CustomerTypeID).IsRequired().IsFixedLength().HasMaxLength(10)
            Me.ToTable("CustomerCustomerDemo")
            Me.Property(Function(t) t.CustomerID).HasColumnName("CustomerID")
            Me.Property(Function(t) t.CustomerTypeID).HasColumnName("CustomerTypeID")
        End Sub
    End Class
    Public Class CustomerDemographicMap
        Inherits EntityTypeConfiguration(Of CustomerDemographic)

        Public Sub New()
            Me.HasKey(Function(t) t.CustomerTypeID)
            Me.Property(Function(t) t.CustomerTypeID).IsRequired().IsFixedLength().HasMaxLength(10)
            Me.ToTable("CustomerDemographics")
            Me.Property(Function(t) t.CustomerTypeID).HasColumnName("CustomerTypeID")
            Me.Property(Function(t) t.CustomerDesc).HasColumnName("CustomerDesc")
        End Sub
    End Class
    Public Class CustomerMap
        Inherits EntityTypeConfiguration(Of Customer)

        Public Sub New()
            Me.HasKey(Function(t) t.CustomerID)
            Me.Property(Function(t) t.CustomerID).IsRequired().IsFixedLength().HasMaxLength(5)
            Me.Property(Function(t) t.CompanyName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.ContactName).HasMaxLength(30)
            Me.Property(Function(t) t.ContactTitle).HasMaxLength(30)
            Me.Property(Function(t) t.Address).HasMaxLength(60)
            Me.Property(Function(t) t.City).HasMaxLength(15)
            Me.Property(Function(t) t.Region).HasMaxLength(15)
            Me.Property(Function(t) t.PostalCode).HasMaxLength(10)
            Me.Property(Function(t) t.Country).HasMaxLength(15)
            Me.Property(Function(t) t.Phone).HasMaxLength(24)
            Me.Property(Function(t) t.Fax).HasMaxLength(24)
            Me.ToTable("Customers")
            Me.Property(Function(t) t.CustomerID).HasColumnName("CustomerID")
            Me.Property(Function(t) t.CompanyName).HasColumnName("CompanyName")
            Me.Property(Function(t) t.ContactName).HasColumnName("ContactName")
            Me.Property(Function(t) t.ContactTitle).HasColumnName("ContactTitle")
            Me.Property(Function(t) t.Address).HasColumnName("Address")
            Me.Property(Function(t) t.City).HasColumnName("City")
            Me.Property(Function(t) t.Region).HasColumnName("Region")
            Me.Property(Function(t) t.PostalCode).HasColumnName("PostalCode")
            Me.Property(Function(t) t.Country).HasColumnName("Country")
            Me.Property(Function(t) t.Phone).HasColumnName("Phone")
            Me.Property(Function(t) t.Fax).HasColumnName("Fax")
        End Sub
    End Class
    Public Class CustomerReportMap
        Inherits EntityTypeConfiguration(Of CustomerReport)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.ProductName, Key t.CompanyName})
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.CompanyName).IsRequired().HasMaxLength(40)
            Me.ToTable("CustomerReports")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.CompanyName).HasColumnName("CompanyName")
            Me.Property(Function(t) t.OrderDate).HasColumnName("OrderDate")
            Me.Property(Function(t) t.ProductAmount).HasColumnName("ProductAmount")
        End Sub
    End Class
    Public Class EmployeeMap
        Inherits EntityTypeConfiguration(Of Employee)

        Public Sub New()
            Me.HasKey(Function(t) t.EmployeeID)
            Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(20)
            Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(10)
            Me.Property(Function(t) t.Title).HasMaxLength(30)
            Me.Property(Function(t) t.TitleOfCourtesy).HasMaxLength(25)
            Me.Property(Function(t) t.Address).HasMaxLength(60)
            Me.Property(Function(t) t.City).HasMaxLength(15)
            Me.Property(Function(t) t.Region).HasMaxLength(15)
            Me.Property(Function(t) t.PostalCode).HasMaxLength(10)
            Me.Property(Function(t) t.Country).HasMaxLength(15)
            Me.Property(Function(t) t.HomePhone).HasMaxLength(24)
            Me.Property(Function(t) t.Extension).HasMaxLength(4)
            Me.Property(Function(t) t.PhotoPath).HasMaxLength(255)
            Me.ToTable("Employees")
            Me.Property(Function(t) t.EmployeeID).HasColumnName("EmployeeID")
            Me.Property(Function(t) t.LastName).HasColumnName("LastName")
            Me.Property(Function(t) t.FirstName).HasColumnName("FirstName")
            Me.Property(Function(t) t.Title).HasColumnName("Title")
            Me.Property(Function(t) t.TitleOfCourtesy).HasColumnName("TitleOfCourtesy")
            Me.Property(Function(t) t.BirthDate).HasColumnName("BirthDate")
            Me.Property(Function(t) t.HireDate).HasColumnName("HireDate")
            Me.Property(Function(t) t.Address).HasColumnName("Address")
            Me.Property(Function(t) t.City).HasColumnName("City")
            Me.Property(Function(t) t.Region).HasColumnName("Region")
            Me.Property(Function(t) t.PostalCode).HasColumnName("PostalCode")
            Me.Property(Function(t) t.Country).HasColumnName("Country")
            Me.Property(Function(t) t.HomePhone).HasColumnName("HomePhone")
            Me.Property(Function(t) t.Extension).HasColumnName("Extension")
            Me.Property(Function(t) t.Photo).HasColumnName("Photo")
            Me.Property(Function(t) t.Notes).HasColumnName("Notes")
            Me.Property(Function(t) t.ReportsTo).HasColumnName("ReportsTo")
            Me.Property(Function(t) t.PhotoPath).HasColumnName("PhotoPath")
            Me.HasMany(Function(t) t.Territories).WithMany(Function(t) t.Employees).Map(Sub(m)
                                                                                            m.ToTable("EmployeeTerritories")
                                                                                            m.MapLeftKey("EmployeeID")
                                                                                            m.MapRightKey("TerritoryID")
                                                                                        End Sub)
        End Sub
    End Class
    Public Class InvoiceMap
        Inherits EntityTypeConfiguration(Of Invoice)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.CustomerName, Key t.Salesperson, Key t.OrderID, Key t.ShipperName, Key t.ProductID, Key t.ProductName, Key t.UnitPrice, Key t.Quantity, Key t.Discount})
            Me.Property(Function(t) t.ShipName).HasMaxLength(40)
            Me.Property(Function(t) t.ShipAddress).HasMaxLength(60)
            Me.Property(Function(t) t.ShipCity).HasMaxLength(15)
            Me.Property(Function(t) t.ShipRegion).HasMaxLength(15)
            Me.Property(Function(t) t.ShipPostalCode).HasMaxLength(10)
            Me.Property(Function(t) t.ShipCountry).HasMaxLength(15)
            Me.Property(Function(t) t.CustomerID).IsFixedLength().HasMaxLength(5)
            Me.Property(Function(t) t.CustomerName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.Address).HasMaxLength(60)
            Me.Property(Function(t) t.City).HasMaxLength(15)
            Me.Property(Function(t) t.Region).HasMaxLength(15)
            Me.Property(Function(t) t.PostalCode).HasMaxLength(10)
            Me.Property(Function(t) t.Country).HasMaxLength(15)
            Me.Property(Function(t) t.Salesperson).IsRequired().HasMaxLength(31)
            Me.Property(Function(t) t.OrderID)
            Me.Property(Function(t) t.ShipperName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.ProductID)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.UnitPrice)
            Me.Property(Function(t) t.Quantity)
            Me.ToTable("Invoices")
            Me.Property(Function(t) t.ShipName).HasColumnName("ShipName")
            Me.Property(Function(t) t.ShipAddress).HasColumnName("ShipAddress")
            Me.Property(Function(t) t.ShipCity).HasColumnName("ShipCity")
            Me.Property(Function(t) t.ShipRegion).HasColumnName("ShipRegion")
            Me.Property(Function(t) t.ShipPostalCode).HasColumnName("ShipPostalCode")
            Me.Property(Function(t) t.ShipCountry).HasColumnName("ShipCountry")
            Me.Property(Function(t) t.CustomerID).HasColumnName("CustomerID")
            Me.Property(Function(t) t.CustomerName).HasColumnName("CustomerName")
            Me.Property(Function(t) t.Address).HasColumnName("Address")
            Me.Property(Function(t) t.City).HasColumnName("City")
            Me.Property(Function(t) t.Region).HasColumnName("Region")
            Me.Property(Function(t) t.PostalCode).HasColumnName("PostalCode")
            Me.Property(Function(t) t.Country).HasColumnName("Country")
            Me.Property(Function(t) t.Salesperson).HasColumnName("Salesperson")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.OrderDate).HasColumnName("OrderDate")
            Me.Property(Function(t) t.RequiredDate).HasColumnName("RequiredDate")
            Me.Property(Function(t) t.ShippedDate).HasColumnName("ShippedDate")
            Me.Property(Function(t) t.ShipperName).HasColumnName("ShipperName")
            Me.Property(Function(t) t.ProductID).HasColumnName("ProductID")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.UnitPrice).HasColumnName("UnitPrice")
            Me.Property(Function(t) t.Quantity).HasColumnName("Quantity")
            Me.Property(Function(t) t.Discount).HasColumnName("Discount")
            Me.Property(Function(t) t.ExtendedPrice).HasColumnName("ExtendedPrice")
            Me.Property(Function(t) t.Freight).HasColumnName("Freight")
        End Sub
    End Class
    Public Class Order_DetailMap
        Inherits EntityTypeConfiguration(Of Order_Detail)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.OrderID, Key t.ProductID, Key t.UnitPrice, Key t.Quantity, Key t.Discount})
            Me.Property(Function(t) t.OrderID)
            Me.Property(Function(t) t.ProductID)
            Me.Property(Function(t) t.UnitPrice)
            Me.Property(Function(t) t.Quantity)
            Me.ToTable("Order Details")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.ProductID).HasColumnName("ProductID")
            Me.Property(Function(t) t.UnitPrice).HasColumnName("UnitPrice")
            Me.Property(Function(t) t.Quantity).HasColumnName("Quantity")
            Me.Property(Function(t) t.Discount).HasColumnName("Discount")
            Me.HasRequired(Function(t) t.Order).WithMany(Function(t) t.Order_Details).HasForeignKey(Function(d) d.OrderID)
        End Sub
    End Class
    Public Class Order_Details_ExtendedMap
        Inherits EntityTypeConfiguration(Of Order_Details_Extended)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.OrderID, Key t.ProductID, Key t.ProductName, Key t.UnitPrice, Key t.Quantity, Key t.Discount})
            Me.Property(Function(t) t.OrderID)
            Me.Property(Function(t) t.ProductID)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.UnitPrice)
            Me.Property(Function(t) t.Quantity)
            Me.ToTable("Order Details Extended")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.ProductID).HasColumnName("ProductID")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.UnitPrice).HasColumnName("UnitPrice")
            Me.Property(Function(t) t.Quantity).HasColumnName("Quantity")
            Me.Property(Function(t) t.Discount).HasColumnName("Discount")
            Me.Property(Function(t) t.ExtendedPrice).HasColumnName("ExtendedPrice")
            Me.HasRequired(Function(t) t.Order).WithMany(Function(t) t.Order_Details_Extended).HasForeignKey(Function(d) d.OrderID)
        End Sub
    End Class
    Public Class OrderDetailsExtendedMap
        Inherits EntityTypeConfiguration(Of Order_Details_Extended)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.OrderID, Key t.ProductID, Key t.ProductName, Key t.UnitPrice, Key t.Quantity, Key t.Discount})
            Me.Property(Function(t) t.OrderID)
            Me.Property(Function(t) t.ProductID)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.UnitPrice)
            Me.Property(Function(t) t.Quantity)
            Me.ToTable("OrderDetailsExtended")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.ProductID).HasColumnName("ProductID")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.UnitPrice).HasColumnName("UnitPrice")
            Me.Property(Function(t) t.Quantity).HasColumnName("Quantity")
            Me.Property(Function(t) t.Discount).HasColumnName("Discount")
            Me.Property(Function(t) t.ExtendedPrice).HasColumnName("ExtendedPrice")
            Me.HasRequired(Function(t) t.Order).WithMany(Function(t) t.Order_Details_Extended).HasForeignKey(Function(d) d.OrderID)
        End Sub
    End Class
    Public Class Order_SubtotalMap
        Inherits EntityTypeConfiguration(Of Order_Subtotal)

        Public Sub New()
            Me.HasKey(Function(t) t.OrderID)
            Me.Property(Function(t) t.OrderID)
            Me.ToTable("Order Subtotals")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.Subtotal).HasColumnName("Subtotal")
            Me.HasRequired(Function(t) t.Order).WithOptional(Function(t) t.Order_Subtotals)
        End Sub
    End Class
    Public Class OrderSubtotalsMap
        Inherits EntityTypeConfiguration(Of Order_Subtotal)

        Public Sub New()
            Me.HasKey(Function(t) t.OrderID)
            Me.Property(Function(t) t.OrderID)
            Me.ToTable("OrderSubtotals")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.Subtotal).HasColumnName("Subtotal")
            Me.HasRequired(Function(t) t.Order).WithOptional(Function(t) t.Order_Subtotals)
        End Sub
    End Class
    Public Class OrderDetailMap
        Inherits EntityTypeConfiguration(Of OrderDetail)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.OrderID, Key t.Quantity, Key t.UnitPrice, Key t.Discount, Key t.ProductName})
            Me.Property(Function(t) t.OrderID)
            Me.Property(Function(t) t.Quantity)
            Me.Property(Function(t) t.UnitPrice)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.Supplier).HasMaxLength(217)
            Me.ToTable("OrderDetails")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.Quantity).HasColumnName("Quantity")
            Me.Property(Function(t) t.UnitPrice).HasColumnName("UnitPrice")
            Me.Property(Function(t) t.Discount).HasColumnName("Discount")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.Supplier).HasColumnName("Supplier")
        End Sub
    End Class
    Public Class OrderMap
        Inherits EntityTypeConfiguration(Of Order)

        Public Sub New()
            Me.HasKey(Function(t) t.OrderID)
            Me.Property(Function(t) t.CustomerID).IsFixedLength().HasMaxLength(5)
            Me.Property(Function(t) t.ShipName).HasMaxLength(40)
            Me.Property(Function(t) t.ShipAddress).HasMaxLength(60)
            Me.Property(Function(t) t.ShipCity).HasMaxLength(15)
            Me.Property(Function(t) t.ShipRegion).HasMaxLength(15)
            Me.Property(Function(t) t.ShipPostalCode).HasMaxLength(10)
            Me.Property(Function(t) t.ShipCountry).HasMaxLength(15)
            Me.ToTable("Orders")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.CustomerID).HasColumnName("CustomerID")
            Me.Property(Function(t) t.EmployeeID).HasColumnName("EmployeeID")
            Me.Property(Function(t) t.OrderDate).HasColumnName("OrderDate")
            Me.Property(Function(t) t.RequiredDate).HasColumnName("RequiredDate")
            Me.Property(Function(t) t.ShippedDate).HasColumnName("ShippedDate")
            Me.Property(Function(t) t.ShipVia).HasColumnName("ShipVia")
            Me.Property(Function(t) t.Freight).HasColumnName("Freight")
            Me.Property(Function(t) t.ShipName).HasColumnName("ShipName")
            Me.Property(Function(t) t.ShipAddress).HasColumnName("ShipAddress")
            Me.Property(Function(t) t.ShipCity).HasColumnName("ShipCity")
            Me.Property(Function(t) t.ShipRegion).HasColumnName("ShipRegion")
            Me.Property(Function(t) t.ShipPostalCode).HasColumnName("ShipPostalCode")
            Me.Property(Function(t) t.ShipCountry).HasColumnName("ShipCountry")
            Me.HasOptional(Function(t) t.Employee).WithMany(Function(t) t.Orders).HasForeignKey(Function(d) d.EmployeeID)
            Me.HasOptional(Function(t) t.Employee1).WithMany(Function(t) t.Orders1).HasForeignKey(Function(d) d.EmployeeID)
        End Sub
    End Class
    Public Class OrderReportMap
        Inherits EntityTypeConfiguration(Of OrderReport)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.OrderID, Key t.ProductID, Key t.ProductName, Key t.UnitPrice, Key t.Quantity, Key t.Discount})
            Me.Property(Function(t) t.OrderID)
            Me.Property(Function(t) t.ProductID)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.UnitPrice)
            Me.Property(Function(t) t.Quantity)
            Me.ToTable("OrderReports")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.ProductID).HasColumnName("ProductID")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.UnitPrice).HasColumnName("UnitPrice")
            Me.Property(Function(t) t.Quantity).HasColumnName("Quantity")
            Me.Property(Function(t) t.Discount).HasColumnName("Discount")
            Me.Property(Function(t) t.Extended_Price).HasColumnName("Extended Price")
        End Sub
    End Class
    Public Class Orders_QryMap
        Inherits EntityTypeConfiguration(Of Orders_Qry)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.OrderID, Key t.CompanyName})
            Me.Property(Function(t) t.OrderID)
            Me.Property(Function(t) t.CustomerID).IsFixedLength().HasMaxLength(5)
            Me.Property(Function(t) t.ShipName).HasMaxLength(40)
            Me.Property(Function(t) t.ShipAddress).HasMaxLength(60)
            Me.Property(Function(t) t.ShipCity).HasMaxLength(15)
            Me.Property(Function(t) t.ShipRegion).HasMaxLength(15)
            Me.Property(Function(t) t.ShipPostalCode).HasMaxLength(10)
            Me.Property(Function(t) t.ShipCountry).HasMaxLength(15)
            Me.Property(Function(t) t.CompanyName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.Address).HasMaxLength(60)
            Me.Property(Function(t) t.City).HasMaxLength(15)
            Me.Property(Function(t) t.Region).HasMaxLength(15)
            Me.Property(Function(t) t.PostalCode).HasMaxLength(10)
            Me.Property(Function(t) t.Country).HasMaxLength(15)
            Me.ToTable("Orders Qry")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.CustomerID).HasColumnName("CustomerID")
            Me.Property(Function(t) t.EmployeeID).HasColumnName("EmployeeID")
            Me.Property(Function(t) t.OrderDate).HasColumnName("OrderDate")
            Me.Property(Function(t) t.RequiredDate).HasColumnName("RequiredDate")
            Me.Property(Function(t) t.ShippedDate).HasColumnName("ShippedDate")
            Me.Property(Function(t) t.ShipVia).HasColumnName("ShipVia")
            Me.Property(Function(t) t.Freight).HasColumnName("Freight")
            Me.Property(Function(t) t.ShipName).HasColumnName("ShipName")
            Me.Property(Function(t) t.ShipAddress).HasColumnName("ShipAddress")
            Me.Property(Function(t) t.ShipCity).HasColumnName("ShipCity")
            Me.Property(Function(t) t.ShipRegion).HasColumnName("ShipRegion")
            Me.Property(Function(t) t.ShipPostalCode).HasColumnName("ShipPostalCode")
            Me.Property(Function(t) t.ShipCountry).HasColumnName("ShipCountry")
            Me.Property(Function(t) t.CompanyName).HasColumnName("CompanyName")
            Me.Property(Function(t) t.Address).HasColumnName("Address")
            Me.Property(Function(t) t.City).HasColumnName("City")
            Me.Property(Function(t) t.Region).HasColumnName("Region")
            Me.Property(Function(t) t.PostalCode).HasColumnName("PostalCode")
            Me.Property(Function(t) t.Country).HasColumnName("Country")
            Me.HasOptional(Function(t) t.Customer).WithMany(Function(t) t.Orders_Qry).HasForeignKey(Function(d) d.CustomerID)
            Me.HasOptional(Function(t) t.Employee).WithMany(Function(t) t.Orders_Qry).HasForeignKey(Function(d) d.EmployeeID)
            Me.HasRequired(Function(t) t.Order).WithMany(Function(t) t.Orders_Qry).HasForeignKey(Function(d) d.OrderID)
        End Sub
    End Class
    Public Class OrdersQryMap
        Inherits EntityTypeConfiguration(Of Orders_Qry)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.OrderID, Key t.CompanyName})
            Me.Property(Function(t) t.OrderID)
            Me.Property(Function(t) t.CustomerID).IsFixedLength().HasMaxLength(5)
            Me.Property(Function(t) t.ShipName).HasMaxLength(40)
            Me.Property(Function(t) t.ShipAddress).HasMaxLength(60)
            Me.Property(Function(t) t.ShipCity).HasMaxLength(15)
            Me.Property(Function(t) t.ShipRegion).HasMaxLength(15)
            Me.Property(Function(t) t.ShipPostalCode).HasMaxLength(10)
            Me.Property(Function(t) t.ShipCountry).HasMaxLength(15)
            Me.Property(Function(t) t.CompanyName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.Address).HasMaxLength(60)
            Me.Property(Function(t) t.City).HasMaxLength(15)
            Me.Property(Function(t) t.Region).HasMaxLength(15)
            Me.Property(Function(t) t.PostalCode).HasMaxLength(10)
            Me.Property(Function(t) t.Country).HasMaxLength(15)
            Me.ToTable("OrdersQry")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.CustomerID).HasColumnName("CustomerID")
            Me.Property(Function(t) t.EmployeeID).HasColumnName("EmployeeID")
            Me.Property(Function(t) t.OrderDate).HasColumnName("OrderDate")
            Me.Property(Function(t) t.RequiredDate).HasColumnName("RequiredDate")
            Me.Property(Function(t) t.ShippedDate).HasColumnName("ShippedDate")
            Me.Property(Function(t) t.ShipVia).HasColumnName("ShipVia")
            Me.Property(Function(t) t.Freight).HasColumnName("Freight")
            Me.Property(Function(t) t.ShipName).HasColumnName("ShipName")
            Me.Property(Function(t) t.ShipAddress).HasColumnName("ShipAddress")
            Me.Property(Function(t) t.ShipCity).HasColumnName("ShipCity")
            Me.Property(Function(t) t.ShipRegion).HasColumnName("ShipRegion")
            Me.Property(Function(t) t.ShipPostalCode).HasColumnName("ShipPostalCode")
            Me.Property(Function(t) t.ShipCountry).HasColumnName("ShipCountry")
            Me.Property(Function(t) t.CompanyName).HasColumnName("CompanyName")
            Me.Property(Function(t) t.Address).HasColumnName("Address")
            Me.Property(Function(t) t.City).HasColumnName("City")
            Me.Property(Function(t) t.Region).HasColumnName("Region")
            Me.Property(Function(t) t.PostalCode).HasColumnName("PostalCode")
            Me.Property(Function(t) t.Country).HasColumnName("Country")
            Me.HasOptional(Function(t) t.Customer).WithMany(Function(t) t.Orders_Qry).HasForeignKey(Function(d) d.CustomerID)
            Me.HasOptional(Function(t) t.Employee).WithMany(Function(t) t.Orders_Qry).HasForeignKey(Function(d) d.EmployeeID)
            Me.HasRequired(Function(t) t.Order).WithMany(Function(t) t.Orders_Qry).HasForeignKey(Function(d) d.OrderID)
        End Sub
    End Class
    Public Class Product_Sales_for_1997Map
        Inherits EntityTypeConfiguration(Of Product_Sales_for_1997)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.CategoryName, Key t.ProductName})
            Me.Property(Function(t) t.CategoryName).IsRequired().HasMaxLength(15)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.ToTable("Product Sales for 1997")
            Me.Property(Function(t) t.CategoryName).HasColumnName("CategoryName")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.ProductSales).HasColumnName("ProductSales")
        End Sub
    End Class
    Public Class ProductMap
        Inherits EntityTypeConfiguration(Of Product)

        Public Sub New()
            Me.HasKey(Function(t) t.ProductID)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.QuantityPerUnit).HasMaxLength(20)
            Me.ToTable("Products")
            Me.Property(Function(t) t.ProductID).HasColumnName("ProductID")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.SupplierID).HasColumnName("SupplierID")
            Me.Property(Function(t) t.CategoryID).HasColumnName("CategoryID")
            Me.Property(Function(t) t.QuantityPerUnit).HasColumnName("QuantityPerUnit")
            Me.Property(Function(t) t.UnitPrice).HasColumnName("UnitPrice")
            Me.Property(Function(t) t.UnitsInStock).HasColumnName("UnitsInStock")
            Me.Property(Function(t) t.UnitsOnOrder).HasColumnName("UnitsOnOrder")
            Me.Property(Function(t) t.ReorderLevel).HasColumnName("ReorderLevel")
            Me.Property(Function(t) t.Discontinued).HasColumnName("Discontinued")
            Me.Property(Function(t) t.EAN13).HasColumnName("EAN13")
            Me.HasOptional(Function(t) t.Category).WithMany(Function(t) t.Products).HasForeignKey(Function(d) d.CategoryID)
            Me.HasOptional(Function(t) t.Supplier).WithMany(Function(t) t.Products).HasForeignKey(Function(d) d.SupplierID)
        End Sub
    End Class
    Public Class ProductReportMap
        Inherits EntityTypeConfiguration(Of ProductReport)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.CategoryName, Key t.ProductName})
            Me.Property(Function(t) t.CategoryName).IsRequired().HasMaxLength(15)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.ToTable("ProductReports")
            Me.Property(Function(t) t.CategoryName).HasColumnName("CategoryName")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.ProductSales).HasColumnName("ProductSales")
            Me.Property(Function(t) t.ShippedDate).HasColumnName("ShippedDate")
        End Sub
    End Class
    Public Class Products_Above_Average_PriceMap
        Inherits EntityTypeConfiguration(Of Products_Above_Average_Price)

        Public Sub New()
            Me.HasKey(Function(t) t.ProductName)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.ToTable("Products Above Average Price")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.UnitPrice).HasColumnName("UnitPrice")
        End Sub
    End Class
    Public Class ProductsAboveAveragePriceMap
        Inherits EntityTypeConfiguration(Of Products_Above_Average_Price)

        Public Sub New()
            Me.HasKey(Function(t) t.ProductName)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.ToTable("ProductsAboveAveragePrice")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.UnitPrice).HasColumnName("UnitPrice")
        End Sub
    End Class
    Public Class Products_by_CategoryMap
        Inherits EntityTypeConfiguration(Of Products_by_Category)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.CategoryName, Key t.ProductName, Key t.Discontinued})
            Me.Property(Function(t) t.CategoryName).IsRequired().HasMaxLength(15)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.QuantityPerUnit).HasMaxLength(20)
            Me.ToTable("Products by Category")
            Me.Property(Function(t) t.CategoryName).HasColumnName("CategoryName")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.QuantityPerUnit).HasColumnName("QuantityPerUnit")
            Me.Property(Function(t) t.UnitsInStock).HasColumnName("UnitsInStock")
            Me.Property(Function(t) t.Discontinued).HasColumnName("Discontinued")
        End Sub
    End Class
    Public Class ProductsByCategoryMap
        Inherits EntityTypeConfiguration(Of Products_by_Category)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.CategoryName, Key t.ProductName, Key t.Discontinued})
            Me.Property(Function(t) t.CategoryName).IsRequired().HasMaxLength(15)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.QuantityPerUnit).HasMaxLength(20)
            Me.ToTable("ProductsByCategory")
            Me.Property(Function(t) t.CategoryName).HasColumnName("CategoryName")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.QuantityPerUnit).HasColumnName("QuantityPerUnit")
            Me.Property(Function(t) t.UnitsInStock).HasColumnName("UnitsInStock")
            Me.Property(Function(t) t.Discontinued).HasColumnName("Discontinued")
        End Sub
    End Class
    Public Class RegionMap
        Inherits EntityTypeConfiguration(Of Region)

        Public Sub New()
            Me.HasKey(Function(t) t.RegionID)
            Me.Property(Function(t) t.RegionDescription).IsRequired().IsFixedLength().HasMaxLength(50)
            Me.ToTable("Region")
            Me.Property(Function(t) t.RegionID).HasColumnName("RegionID")
            Me.Property(Function(t) t.RegionDescription).HasColumnName("RegionDescription")
        End Sub
    End Class
    Public Class Sales_by_CategoryMap
        Inherits EntityTypeConfiguration(Of Sales_by_Category)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.CategoryID, Key t.CategoryName, Key t.ProductName})
            Me.Property(Function(t) t.CategoryID)
            Me.Property(Function(t) t.CategoryName).IsRequired().HasMaxLength(15)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.ToTable("Sales by Category")
            Me.Property(Function(t) t.CategoryID).HasColumnName("CategoryID")
            Me.Property(Function(t) t.CategoryName).HasColumnName("CategoryName")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.ProductSales).HasColumnName("ProductSales")
        End Sub
    End Class
    Public Class SalesByCategoryMap
        Inherits EntityTypeConfiguration(Of Sales_by_Category)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.CategoryID, Key t.CategoryName, Key t.ProductName})
            Me.Property(Function(t) t.CategoryID)
            Me.Property(Function(t) t.CategoryName).IsRequired().HasMaxLength(15)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.ToTable("SalesByCategory")
            Me.Property(Function(t) t.CategoryID).HasColumnName("CategoryID")
            Me.Property(Function(t) t.CategoryName).HasColumnName("CategoryName")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.ProductSales).HasColumnName("ProductSales")
        End Sub
    End Class
    Public Class Sales_Totals_by_AmountMap
        Inherits EntityTypeConfiguration(Of Sales_Totals_by_Amount)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.OrderID, Key t.CompanyName})
            Me.Property(Function(t) t.OrderID)
            Me.Property(Function(t) t.CompanyName).IsRequired().HasMaxLength(40)
            Me.ToTable("Sales Totals by Amount")
            Me.Property(Function(t) t.SaleAmount).HasColumnName("SaleAmount")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.CompanyName).HasColumnName("CompanyName")
            Me.Property(Function(t) t.ShippedDate).HasColumnName("ShippedDate")
            Me.HasRequired(Function(t) t.Order).WithMany(Function(t) t.Sales_Totals_by_Amount).HasForeignKey(Function(d) d.OrderID)
        End Sub
    End Class
    Public Class SalesTotalsByAmountMap
        Inherits EntityTypeConfiguration(Of Sales_Totals_by_Amount)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.OrderID, Key t.CompanyName})
            Me.Property(Function(t) t.OrderID)
            Me.Property(Function(t) t.CompanyName).IsRequired().HasMaxLength(40)
            Me.ToTable("SalesTotalsByAmount")
            Me.Property(Function(t) t.SaleAmount).HasColumnName("SaleAmount")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.CompanyName).HasColumnName("CompanyName")
            Me.Property(Function(t) t.ShippedDate).HasColumnName("ShippedDate")
            Me.HasRequired(Function(t) t.Order).WithMany(Function(t) t.Sales_Totals_by_Amount).HasForeignKey(Function(d) d.OrderID)
        End Sub
    End Class
    Public Class SalesPersonMap
        Inherits EntityTypeConfiguration(Of SalesPerson)

        Public Sub New()
            Me.HasKey(Function(t) New With {Key t.OrderID, Key t.FirstName, Key t.LastName, Key t.ProductName, Key t.CategoryName, Key t.UnitPrice, Key t.Quantity, Key t.Discount, Key t.Sales_Person})
            Me.Property(Function(t) t.OrderID)
            Me.Property(Function(t) t.Country).HasMaxLength(15)
            Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(10)
            Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(20)
            Me.Property(Function(t) t.ProductName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.CategoryName).IsRequired().HasMaxLength(15)
            Me.Property(Function(t) t.UnitPrice)
            Me.Property(Function(t) t.Quantity)
            Me.Property(Function(t) t.Sales_Person).IsRequired().HasMaxLength(31)
            Me.ToTable("SalesPerson")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.Country).HasColumnName("Country")
            Me.Property(Function(t) t.FirstName).HasColumnName("FirstName")
            Me.Property(Function(t) t.LastName).HasColumnName("LastName")
            Me.Property(Function(t) t.ProductName).HasColumnName("ProductName")
            Me.Property(Function(t) t.CategoryName).HasColumnName("CategoryName")
            Me.Property(Function(t) t.OrderDate).HasColumnName("OrderDate")
            Me.Property(Function(t) t.UnitPrice).HasColumnName("UnitPrice")
            Me.Property(Function(t) t.Quantity).HasColumnName("Quantity")
            Me.Property(Function(t) t.Discount).HasColumnName("Discount")
            Me.Property(Function(t) t.Extended_Price).HasColumnName("Extended Price")
            Me.Property(Function(t) t.Sales_Person).HasColumnName("Sales Person")
        End Sub
    End Class
    Public Class ShipperMap
        Inherits EntityTypeConfiguration(Of Shipper)

        Public Sub New()
            Me.HasKey(Function(t) t.ShipperID)
            Me.Property(Function(t) t.CompanyName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.Phone).HasMaxLength(24)
            Me.ToTable("Shippers")
            Me.Property(Function(t) t.ShipperID).HasColumnName("ShipperID")
            Me.Property(Function(t) t.CompanyName).HasColumnName("CompanyName")
            Me.Property(Function(t) t.Phone).HasColumnName("Phone")
        End Sub
    End Class
    Public Class Summary_of_Sales_by_QuarterMap
        Inherits EntityTypeConfiguration(Of Summary_of_Sales_by_Quarter)

        Public Sub New()
            Me.HasKey(Function(t) t.OrderID)
            Me.Property(Function(t) t.OrderID)
            Me.ToTable("Summary of Sales by Quarter")
            Me.Property(Function(t) t.ShippedDate).HasColumnName("ShippedDate")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.Subtotal).HasColumnName("Subtotal")
            Me.HasRequired(Function(t) t.Order).WithOptional(Function(t) t.Summary_of_Sales_by_Quarter)
        End Sub
    End Class
    Public Class SummaryOfSalesByQuarterMap
        Inherits EntityTypeConfiguration(Of Summary_of_Sales_by_Quarter)

        Public Sub New()
            Me.HasKey(Function(t) t.OrderID)
            Me.Property(Function(t) t.OrderID)
            Me.ToTable("SummaryOfSalesByQuarter")
            Me.Property(Function(t) t.ShippedDate).HasColumnName("ShippedDate")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.Subtotal).HasColumnName("Subtotal")
            Me.HasRequired(Function(t) t.Order).WithOptional(Function(t) t.Summary_of_Sales_by_Quarter)
        End Sub
    End Class
    Public Class Summary_of_Sales_by_YearMap
        Inherits EntityTypeConfiguration(Of Summary_of_Sales_by_Year)

        Public Sub New()
            Me.HasKey(Function(t) t.OrderID)
            Me.Property(Function(t) t.OrderID)
            Me.ToTable("Summary of Sales by Year")
            Me.Property(Function(t) t.ShippedDate).HasColumnName("ShippedDate")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.Subtotal).HasColumnName("Subtotal")
            Me.HasRequired(Function(t) t.Order).WithOptional(Function(t) t.Summary_of_Sales_by_Year)
        End Sub
    End Class
    Public Class SummaryOfSalesByYearMap
        Inherits EntityTypeConfiguration(Of Summary_of_Sales_by_Year)

        Public Sub New()
            Me.HasKey(Function(t) t.OrderID)
            Me.Property(Function(t) t.OrderID)
            Me.ToTable("SummaryOfSalesByYear")
            Me.Property(Function(t) t.ShippedDate).HasColumnName("ShippedDate")
            Me.Property(Function(t) t.OrderID).HasColumnName("OrderID")
            Me.Property(Function(t) t.Subtotal).HasColumnName("Subtotal")
            Me.HasRequired(Function(t) t.Order).WithOptional(Function(t) t.Summary_of_Sales_by_Year)
        End Sub
    End Class
    Public Class SupplierMap
        Inherits EntityTypeConfiguration(Of Supplier)

        Public Sub New()
            Me.HasKey(Function(t) t.SupplierID)
            Me.Property(Function(t) t.CompanyName).IsRequired().HasMaxLength(40)
            Me.Property(Function(t) t.ContactName).HasMaxLength(30)
            Me.Property(Function(t) t.ContactTitle).HasMaxLength(30)
            Me.Property(Function(t) t.Address).HasMaxLength(60)
            Me.Property(Function(t) t.City).HasMaxLength(15)
            Me.Property(Function(t) t.Region).HasMaxLength(15)
            Me.Property(Function(t) t.PostalCode).HasMaxLength(10)
            Me.Property(Function(t) t.Country).HasMaxLength(15)
            Me.Property(Function(t) t.Phone).HasMaxLength(24)
            Me.Property(Function(t) t.Fax).HasMaxLength(24)
            Me.ToTable("Suppliers")
            Me.Property(Function(t) t.SupplierID).HasColumnName("SupplierID")
            Me.Property(Function(t) t.CompanyName).HasColumnName("CompanyName")
            Me.Property(Function(t) t.ContactName).HasColumnName("ContactName")
            Me.Property(Function(t) t.ContactTitle).HasColumnName("ContactTitle")
            Me.Property(Function(t) t.Address).HasColumnName("Address")
            Me.Property(Function(t) t.City).HasColumnName("City")
            Me.Property(Function(t) t.Region).HasColumnName("Region")
            Me.Property(Function(t) t.PostalCode).HasColumnName("PostalCode")
            Me.Property(Function(t) t.Country).HasColumnName("Country")
            Me.Property(Function(t) t.Phone).HasColumnName("Phone")
            Me.Property(Function(t) t.Fax).HasColumnName("Fax")
            Me.Property(Function(t) t.HomePage).HasColumnName("HomePage")
        End Sub
    End Class
    Public Class TerritoryMap
        Inherits EntityTypeConfiguration(Of Territory)

        Public Sub New()
            Me.HasKey(Function(t) t.TerritoryID)
            Me.Property(Function(t) t.TerritoryID).IsRequired().HasMaxLength(20)
            Me.Property(Function(t) t.TerritoryDescription).IsRequired().IsFixedLength().HasMaxLength(50)
            Me.ToTable("Territories")
            Me.Property(Function(t) t.TerritoryID).HasColumnName("TerritoryID")
            Me.Property(Function(t) t.TerritoryDescription).HasColumnName("TerritoryDescription")
            Me.Property(Function(t) t.RegionID).HasColumnName("RegionID")
            Me.HasRequired(Function(t) t.Region).WithMany(Function(t) t.Territories).HasForeignKey(Function(d) d.RegionID)
        End Sub
    End Class
#End Region
End Namespace
