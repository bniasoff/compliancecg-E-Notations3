Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports Newtonsoft.Json
Imports Syncfusion.EJ.PdfViewer
Imports System.IO
Imports System.Reflection


Namespace Controllers
    Partial Public Class PdfViewerController
        Inherits Controller

        Public Function [Default]() As ActionResult
            Return View()
        End Function
        Public Function Index6() As ActionResult

            Return View()
        End Function
        Public Function Index7() As ActionResult

            Return View()
        End Function
        '<HttpPost>
        'Public Function Load(ByVal results As jsonObjects) As ActionResult
        '    Dim helper2 As PdfViewerHelper = New PdfViewerHelper()
        '    Dim jsonResult = JsonConverterstring(results)

        '    If jsonResult.ContainsKey("newFileName") Then
        '        Dim name = jsonResult("newFileName")
        '        Dim pdfName = name.ToString() & ".pdf"
        '        helper2.Load(Helper.GetFilePath("" & pdfName))
        '    Else

        '        If jsonResult.ContainsKey("isInitialLoading") Then
        '            helper2.Load(Helper.GetFilePath("FormFillingDocument.pdf"))
        '        End If
        '    End If

        '    Return Content(JsonConvert.SerializeObject(helper2.ProcessPdf(jsonResult)))
        'End Function

        'Public Function JsonConverterstring(ByVal results As jsonObjects) As Dictionary(Of String, String)
        '    Dim resultObjects As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
        '    resultObjects = results.[GetType]().GetProperties(BindingFlags.Instance Or BindingFlags.[Public]).ToDictionary(Function(prop) prop.Name, Function(prop) prop.GetValue(results, Nothing))
        '    Dim emptyObjects = (From kv In resultObjects Where kv.Value IsNot Nothing Select kv).ToDictionary(Function(kv) kv.Key, Function(kv) kv.Value)
        '    Dim jsonResult As Dictionary(Of String, String) = emptyObjects.ToDictionary(Function(k) k.Key, Function(k) k.Value.ToString())
        '    Return jsonResult
        'End Function

        'Public Function Download(ByVal result As jsonObjects) As JsonResult
        '    Dim helper As PdfViewerHelper = New PdfViewerHelper()
        '    Dim jsonResult = JsonConverterstring(result)

        '    If jsonResult.ContainsKey("savedFields") Then
        '        Dim values = jsonResult("savedFields")
        '        Dim data As Dictionary(Of String, String) = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(values)
        '        jsonResult = data
        '    End If

        '    Return Json(helper.GetDocumentData(jsonResult))
        'End Function

        'Public Function FileUpload(ByVal result As jsonObjects) As ActionResult
        '    Dim helper As PdfViewerHelper = New PdfViewerHelper()
        '    Dim jsonResult = JsonConverterstring(result)

        '    If jsonResult.ContainsKey("uploadedFile") Then
        '        Dim fileurl = jsonResult("uploadedFile")
        '        Dim byteArray As Byte() = Convert.FromBase64String(fileurl)
        '        Dim stream As MemoryStream = New MemoryStream(byteArray)
        '        helper.Load(stream)
        '    End If

        '    Return Content(JsonConvert.SerializeObject(helper.ProcessPdf(jsonResult)))
        'End Function
    End Class

    Public Class jsonObjects
        Public Property viewerAction As String
        Public Property pageindex As String
        Public Property controlId As String
        Public Property isInitialLoading As String
        Public Property id As String
        Public Property isPageScrolled As String
        Public Property Download As String
        Public Property uploadedFile As String
        Public Property newFileName As String
        Public Property savedFields As String
        Public Property enableOfflineMode As String
    End Class

    Public Class Helper
        Public Shared Function GetFilePath(ByVal path As String) As String
            Dim _dataPath As String = New System.IO.DirectoryInfo(HttpContext.Current.Request.PhysicalApplicationPath & "..\..\..\Common\Data\PDF").FullName
            _dataPath += "\" & path
            Return _dataPath
        End Function
    End Class
End Namespace
