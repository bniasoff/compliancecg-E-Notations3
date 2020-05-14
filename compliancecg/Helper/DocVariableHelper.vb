Imports System.Collections.Generic
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Linq
Imports System.Web.UI.WebControls
Imports CCGData.compliancecg

Imports DevExpress.Web.Mvc
Imports DevExpress.XtraCharts
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native

Namespace Helper

    'Public Module DocVariableHelper
    'Function GetClientName(ByVal categoryName As String) As String
    '    ' Dim Faclity As FacilityUserResponse
    '    Dim FacilityName As String
    '    FacilityName = "Benyomin Niasoff"
    '    Return FacilityName
    '    ' Return GetSales(categoryName).Sum(Function(s) s.ProductSales.Value)

    'End Function
    'Function GetCommonSales(ByVal categoryName As String) As String
    '    Dim FacilityName As String
    '    FacilityName = "Benyomin Niasoff"
    '    Return FacilityName

    '    ' Return GetSales(categoryName).Sum(Function(s) s.ProductSales.Value)
    'End Function

    '    Function GetCommonSales(ByVal categoryName As String) As Decimal
    '        Return GetSales(categoryName).Sum(Function(s) s.ProductSales.Value)
    '    End Function

    '    Function GetDocumentWithChart(ByVal categoryName As String) As Document
    '        Dim chart As DocumentImageSource = DocumentImageSource.FromStream(CreateChart(categoryName))
    '        Dim richServer As RichEditDocumentServer = New RichEditDocumentServer()
    '        richServer.Document.Images.Append(chart)
    '        Return richServer.Document
    '    End Function

    '    Private Function GetSales(ByVal categoryName As String) As IEnumerable(Of Sales_by_Category)
    '        Using context = New NorthwindContext()
    '            Return context.Sales_by_Categories.Where(Function(s) s.CategoryName = categoryName).ToArray()
    '        End Using
    '    End Function

    '    Private Function CreateChart(ByVal categoryName As String) As Stream
    '        Dim sales As IEnumerable(Of Sales_by_Category) = GetSales(categoryName)
    '        Dim settings As ChartControlSettings = CreateChartSettings()
    '        Dim stream As MemoryStream = New MemoryStream()
    '        ChartControlExtension.ExportToImage(settings, sales, ImageFormat.Png, stream)
    '        stream.Position = 0
    '        Return stream
    '    End Function

    '    Private Function CreateChartSettings() As ChartControlSettings
    '        Dim settings As ChartControlSettings = New ChartControlSettings()
    '        settings.Name = "Chart"
    '        settings.Width = Unit.Pixel(600)
    '        settings.Height = Unit.Pixel(400)
    '        settings.Legend.Visibility = False 'DevExpress.Utils.DefaultBoolean.[False]
    '        Dim series = New Series("Products", ViewType.Bar)
    '        series.ArgumentDataMember = "ProductName"
    '        series.ValueScaleType = ScaleType.Numerical
    '        series.ValueDataMembers.AddRange(New String() {"ProductSales"})
    '        settings.Series.Add(series)
    '        Return settings
    '    End Function
    'End Module
End Namespace

