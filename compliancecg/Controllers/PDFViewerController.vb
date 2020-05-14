Imports System.IO
Imports System.Web.Mvc
Imports Newtonsoft.Json
Imports Syncfusion.EJ2.PdfViewer
Imports Syncfusion.EJ.PdfViewer
Imports System.Web.Http
Imports System
Imports System.Collections.Generic

Imports System.Linq
Imports System.Reflection
Imports System.Web





'Namespace Controllers
'    Public Class PDFViewerController
'        Inherits Controller

'        Get :  PDFViewer
'        Function Index() As ActionResult
'            Return View()
'        End Function
'    End Class
'End Namespace



Namespace Controllers
    Public Class PdfViewerController
        Inherits Controller
        Private Helper As PdfViewerHelper = New PdfViewerHelper()
        Public Function Index() As ActionResult
            Return View()
        End Function
        Public Function Index2() As ActionResult

            Return View()
        End Function

        Public Function Index4() As ActionResult

            Return View()
        End Function

        Public Function Index5() As ActionResult

            Return View()
        End Function
        Public Function Details(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        Public Function Create() As ActionResult
            Return View()
        End Function

        <Http.HttpPost>
        Public Function Create(ByVal collection As FormCollection) As ActionResult
            Try
                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        Public Function Edit(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        <Http.HttpPost>
        Public Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        Public Function Delete(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        <Http.HttpPost>
        Public Function Delete(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        Public Sub New()
        End Sub

        Public Function [Default]() As ActionResult
            Return View()
        End Function

        Public Function PostViewerAction(ByVal jsonResult As Dictionary(Of String, String)) As Object
            If jsonResult.ContainsKey("isInitialLoading") Then
                'Dim stream As MemoryStream = New MemoryStream(File.ReadAllBytes(helper.GetFilePath("F# Succinctly.pdf")))
                Dim stream As MemoryStream = New MemoryStream(System.IO.File.ReadAllBytes(Helper2.GetFilePath("F# Succinctly.pdf")))
                Helper.Load(stream)
            End If

            Dim output As String = JsonConvert.SerializeObject(Helper.ProcessPdf(jsonResult))
            Return output
        End Function
        'Public Shared Function GetFilePath(ByVal path As String) As String
        '    Dim _dataPath As String ' = GetCommonFolder(New DirectoryInfo(HttpContext.Current.Request.PhysicalApplicationPath))
        '    _dataPath += "\" & path
        '    Return _dataPath
        'End Function

        Private Shared Function GetCommonFolder(ByVal dtInfo As DirectoryInfo) As String
            Dim _folderNames = dtInfo.GetDirectories("Data")

            If _folderNames.Length > 0 Then
                Return _folderNames(0).FullName
            End If

            Return If(dtInfo.Parent IsNot Nothing, GetCommonFolder(dtInfo.Parent), String.Empty)
        End Function
        Public Function Load(ByVal FileName As String) As Object
            Dim helper As PdfViewerHelper = New PdfViewerHelper()

            'If JsonResult.ContainsKey("newFileName") Then
            '    Dim fileurl = JsonResult("newFileName")
            '    Dim byteArray As Byte() = Convert.FromBase64String(fileurl)
            '    Dim stream As MemoryStream = New MemoryStream(byteArray)
            '    helper.Load(stream)
            'End If

            'Dim output As String = String.Empty

            'If Not JsonResult.ContainsKey("isInitialLoading") Then
            '    output = JsonConvert.SerializeObject(helper.ProcessPdf(JsonResult))
            'End If

            'Return output
        End Function


        Public Function Load(ByVal jsonResult As Dictionary(Of String, String)) As Object
            Dim helper As PdfViewerHelper = New PdfViewerHelper()

            If jsonResult.ContainsKey("newFileName") Then
                Dim fileurl = jsonResult("newFileName")
                Dim byteArray As Byte() = Convert.FromBase64String(fileurl)
                Dim stream As MemoryStream = New MemoryStream(byteArray)
                helper.Load(stream)
            End If

            Dim output As String = String.Empty

            If Not jsonResult.ContainsKey("isInitialLoading") Then
                output = JsonConvert.SerializeObject(helper.ProcessPdf(jsonResult))
            End If

            Return output
        End Function

        Public Function Download(ByVal jsonResult As Dictionary(Of String, String)) As Object
            Dim helper As PdfViewerHelper = New PdfViewerHelper()
            Return New With {.DocumentStream = Convert.ToBase64String(helper.DocumentStream.ToArray())}
        End Function
        <System.Web.Mvc.HttpPost>
        Public Function FileUpload(ByVal jsonObject As jsonObjects) As ActionResult


            Try

                Dim pdfviewer As PdfRenderer = New PdfRenderer()
                Dim stream As MemoryStream = New MemoryStream()
                Dim jsonData = JsonConverter(jsonObject)
                Dim jsonResult As Object = New Object()


                Dim filename = "PDF Succinctly"
                Dim filePath = Helper2.GetFilePath(filename & ".pdf")

                Dim bytes As Byte() = System.IO.File.ReadAllBytes(filePath)
                stream = New MemoryStream(bytes)


                'Dim bytes = System.IO.File.ReadAllBytes(filePath)

                'Dim streamFile As MemoryStream = New MemoryStream(bytes)
                'Dim dataString = Convert.ToBase64String(bytes)





                'If jsonObject IsNot Nothing AndAlso jsonData.ContainsKey("document") Then

                '    If Boolean.Parse(jsonData("isFileName")) Then
                '        Dim documentPath As String = GetDocumentPath(jsonData("document"))

                '        If Not String.IsNullOrEmpty(documentPath) Then
                '            Dim bytes As Byte() = System.IO.File.ReadAllBytes(documentPath)
                '            stream = New MemoryStream(bytes)
                '        Else
                '            Return Me.Content(jsonData("document") & " is not found")
                '        End If
                '    Else
                '        Dim bytes As Byte() = Convert.FromBase64String(jsonData("document"))
                '        stream = New MemoryStream(bytes)
                '    End If
                'End If

                jsonResult = pdfviewer.Load(stream, jsonData)
                Return Content(JsonConvert.SerializeObject(jsonResult))

            Catch ex As Exception

            End Try

        End Function


        'Public Function FileUpload(ByVal jsonResult As Dictionary(Of String, String)) As Object


        '    Try



        '        Dim helper As PdfViewerHelper = New PdfViewerHelper()

        '        Dim filename = "PDF Succinctly"
        '        Dim filePath = Helper2.GetFilePath(filename & ".pdf")



        '        'Dim stream As MemoryStream = New MemoryStream(System.IO.File.ReadAllBytes(filePath))
        '        'helper.Load(stream)

        '        helper.Load(filePath)




        '        'Dim bytes = System.IO.File.ReadAllBytes(filePath)
        '        'Dim byteArray As Byte() = Convert.FromBase64String(bytes)

        '        'Dim bytes = System.IO.File.ReadAllBytes(filePath)
        '        'Dim streamFile As MemoryStream = New MemoryStream(bytes)


        '        'Dim byteArray As Byte() = Convert.FromBase64String(filePath)
        '        'Dim stream As MemoryStream = New MemoryStream(byteArray)
        '        ' helper.Load(Stream)



        '        'If jsonResult.ContainsKey("uploadedFile") Then
        '        '    Dim fileurl = jsonResult("uploadedFile")
        '        '    Dim byteArray As Byte() = Convert.FromBase64String(fileurl)
        '        '    Dim stream As MemoryStream = New MemoryStream(byteArray)
        '        '    helper.Load(stream)
        '        'End If

        '        Dim output As String = JsonConvert.SerializeObject(helper.ProcessPdf(jsonResult))
        '        Return output
        '    Catch ex As Exception

        '    End Try
        'End Function
        <System.Web.Mvc.HttpPost>
        Public Function DocumentRetrieve(ByVal jsonResult As Dictionary(Of String, String)) As String
            Try
                'Dim filename = jsonResult("documentName")
                Dim filename = "Sample Unsecured"
                Dim filePath = Helper2.GetFilePath(filename & ".pdf")
                Dim bytes = System.IO.File.ReadAllBytes(filePath)
                Dim streamFile As MemoryStream = New MemoryStream(bytes)
                Dim dataString = Convert.ToBase64String(bytes)
                Return "data:application/pdf;base64," & dataString
                'Return "True"
            Catch ex As Exception

            End Try
        End Function









        '<System.Web.Mvc.HttpPost>
        'Public Function Load(ByVal jsonObject As jsonObjects) As ActionResult
        '    Try

        '        Dim pdfviewer As PdfRenderer = New PdfRenderer()
        '        Dim stream As MemoryStream = New MemoryStream()
        '        Dim jsonData = JsonConverter(jsonObject)
        '        Dim jsonResult As Object = New Object()


        '        Dim filename = "PDF Succinctly"
        '        Dim filePath = Helper2.GetFilePath(filename & ".pdf")

        '        Dim bytes As Byte() = System.IO.File.ReadAllBytes(filePath)
        '        stream = New MemoryStream(bytes)


        '        'Dim bytes = System.IO.File.ReadAllBytes(filePath)

        '        'Dim streamFile As MemoryStream = New MemoryStream(bytes)
        '        'Dim dataString = Convert.ToBase64String(bytes)





        '        'If jsonObject IsNot Nothing AndAlso jsonData.ContainsKey("document") Then

        '        '    If Boolean.Parse(jsonData("isFileName")) Then
        '        '        Dim documentPath As String = GetDocumentPath(jsonData("document"))

        '        '        If Not String.IsNullOrEmpty(documentPath) Then
        '        '            Dim bytes As Byte() = System.IO.File.ReadAllBytes(documentPath)
        '        '            stream = New MemoryStream(bytes)
        '        '        Else
        '        '            Return Me.Content(jsonData("document") & " is not found")
        '        '        End If
        '        '    Else
        '        '        Dim bytes As Byte() = Convert.FromBase64String(jsonData("document"))
        '        '        stream = New MemoryStream(bytes)
        '        '    End If
        '        'End If

        '        jsonResult = pdfviewer.Load(stream, jsonData)
        '        Return Content(JsonConvert.SerializeObject(jsonResult))

        '    Catch ex As Exception

        '    End Try

        'End Function


        <System.Web.Mvc.HttpPost>
        Public Function Load(ByVal jsonObject As jsonObjects) As ActionResult

            'jsonObject.document = "Sample UnSecured.pdf"
            'jsonObject.isFileName = "True"


            Dim pdfviewer As PdfRenderer = New PdfRenderer()
            Dim stream As MemoryStream = New MemoryStream()
            Dim jsonData = JsonConverter(jsonObject)
            Dim jsonResult As Object = New Object()

            If jsonObject IsNot Nothing AndAlso jsonData.ContainsKey("document") Then

                If Boolean.Parse(jsonData("isFileName")) Then
                    Dim documentPath As String = GetDocumentPath(jsonData("document"))

                    If Not String.IsNullOrEmpty(documentPath) Then
                        Dim bytes As Byte() = System.IO.File.ReadAllBytes(documentPath)
                        stream = New MemoryStream(bytes)
                    Else
                        Return Me.Content(jsonData("document") & " is not found")
                    End If
                Else
                    Dim bytes As Byte() = Convert.FromBase64String(jsonData("document"))
                    stream = New MemoryStream(bytes)
                End If
            End If

            jsonResult = pdfviewer.Load(stream, jsonData)
            Return Content(JsonConvert.SerializeObject(jsonResult))
        End Function

        Public Function JsonConverter(ByVal results As jsonObjects) As Dictionary(Of String, String)
            Dim resultObjects As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
            resultObjects = results.[GetType]().GetProperties(BindingFlags.Instance Or BindingFlags.[Public]).ToDictionary(Function(prop) prop.Name, Function(prop) prop.GetValue(results, Nothing))
            Dim emptyObjects = (From kv In resultObjects Where kv.Value IsNot Nothing Select kv).ToDictionary(Function(kv) kv.Key, Function(kv) kv.Value)
            Dim jsonResult As Dictionary(Of String, String) = emptyObjects.ToDictionary(Function(k) k.Key, Function(k) k.Value.ToString())
            Return jsonResult
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function RenderPdfPages(ByVal jsonObject As jsonObjects) As ActionResult
            Dim pdfviewer As PdfRenderer = New PdfRenderer()
            Dim jsonData = JsonConverter(jsonObject)
            Dim jsonResult As Object = pdfviewer.GetPage(jsonData)
            Return Content(JsonConvert.SerializeObject(jsonResult))
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function Unload(ByVal jsonObject As jsonObjects) As ActionResult
            Dim pdfviewer As PdfRenderer = New PdfRenderer()
            Dim jsonData = JsonConverter(jsonObject)
            pdfviewer.ClearCache(jsonData)
            Return Me.Content("Document cache is cleared")
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function RenderThumbnailImages(ByVal jsonObject As jsonObjects) As ActionResult
            Dim pdfviewer As PdfRenderer = New PdfRenderer()
            Dim jsonData = JsonConverter(jsonObject)
            Dim result As Object = pdfviewer.GetThumbnailImages(jsonData)
            Return Content(JsonConvert.SerializeObject(result))
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function Bookmarks(ByVal jsonObject As jsonObjects) As ActionResult
            Dim pdfviewer As PdfRenderer = New PdfRenderer()
            Dim jsonData = JsonConverter(jsonObject)
            Dim jsonResult As Object = pdfviewer.GetBookmarks(jsonData)
            Return Content(JsonConvert.SerializeObject(jsonResult))
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function Download(ByVal jsonObject As jsonObjects) As ActionResult
            Dim pdfviewer As PdfRenderer = New PdfRenderer()
            Dim jsonData = JsonConverter(jsonObject)
            Dim documentBase As String = pdfviewer.GetDocumentAsBase64(jsonData)
            Return Content(documentBase)
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function PrintImages(ByVal jsonObject As jsonObjects) As ActionResult
            Dim pdfviewer As PdfRenderer = New PdfRenderer()
            Dim jsonData = JsonConverter(jsonObject)
            Dim pageImage As Object = pdfviewer.GetPrintImage(jsonData)
            Return Content(JsonConvert.SerializeObject(pageImage))
        End Function

        Private Function GetDocumentPath(ByVal document As String) As String
            Dim documentPath As String = String.Empty

            If Not System.IO.File.Exists(document) Then
                Dim path = HttpContext.Request.PhysicalApplicationPath
                If System.IO.File.Exists(path & "App_Data\Data\" & document) Then documentPath = path & "App_Data\Data\" & document
            Else
                documentPath = document
            End If

            Return documentPath
        End Function

        Public Function [Get]() As IEnumerable(Of String)
            Return New String() {"value1", "value2"}
        End Function

        'Private Function encodeFileToBase64Binary(ByVal file As File) As String
        '    Dim encodedfile As String = Nothing

        '    Try
        '        Dim fileInputStreamReader As FileInputStream = New FileInputStream(file)
        '        Dim bytes As Byte() = New Byte(CInt(file.length()) - 1) {}
        '        fileInputStreamReader.read(bytes)
        '        encodedfile = Base64.encodeBase64(bytes).toString()
        '    Catch e As FileNotFoundException
        '        e.printStackTrace()
        '    Catch e As IOException
        '        e.printStackTrace()
        '    End Try

        '    Return encodedfile
        'End Function
    End Class

    Public Class jsonObjects
        Public Property document As String
        Public Property password As String
        Public Property zoomFactor As String
        Public Property isFileName As String
        Public Property xCoordinate As String
        Public Property yCoordinate As String
        Public Property pageNumber As String
        Public Property documentId As String
        Public Property hashId As String
        Public Property sizeX As String
        Public Property sizeY As String
        Public Property startPage As String
        Public Property endPage As String
    End Class

    Public Class Helper2
        Public Shared Function GetFilePath(ByVal path As String) As String
            Dim _dataPath As String = GetCommonFolder(New DirectoryInfo(HttpContext.Current.Request.PhysicalApplicationPath))
            _dataPath += "\" & path
            Return _dataPath
        End Function

        Private Shared Function GetCommonFolder(ByVal dtInfo As DirectoryInfo) As String
            Dim _folderNames = dtInfo.GetDirectories("App_Data\Data")

            If _folderNames.Length > 0 Then
                Return _folderNames(0).FullName
            End If

            Return If(dtInfo.Parent IsNot Nothing, GetCommonFolder(dtInfo.Parent), String.Empty)
        End Function
    End Class

End Namespace


'Imports Newtonsoft.Json
'Imports Syncfusion.EJ.PdfViewer
'Imports System
'Imports System.Collections.Generic
'Imports System.IO
'Imports System.Linq
'Imports System.Net
'Imports System.Net.Http
'Imports System.Web
'Imports System.Web.Http

'Namespace PDFViewDemo.Controllers
'    Public Class PdfViewerController
'        Inherits ApiController

'        Public Function Load(ByVal jsonResult As Dictionary(Of String, String)) As Object
'            Dim helper As PdfViewerHelper = New PdfViewerHelper()

'            If jsonResult.ContainsKey("newFileName") Then
'                Dim fileurl = jsonResult("newFileName")
'                Dim byteArray As Byte() = Convert.FromBase64String(fileurl)
'                Dim stream As MemoryStream = New MemoryStream(byteArray)
'                helper.Load(stream)
'            End If

'            Dim output As String = String.Empty

'            If Not jsonResult.ContainsKey("isInitialLoading") Then
'                output = JsonConvert.SerializeObject(helper.ProcessPdf(jsonResult))
'            End If

'            Return output
'        End Function

'        Public Function Download(ByVal jsonResult As Dictionary(Of String, String)) As Object
'            Dim helper As PdfViewerHelper = New PdfViewerHelper()
'            Return New With {Key
'                .DocumentStream = Convert.ToBase64String(helper.DocumentStream.ToArray())
'            }
'        End Function

'        Public Function FileUpload(ByVal jsonResult As Dictionary(Of String, String)) As Object
'            Dim helper As PdfViewerHelper = New PdfViewerHelper()

'            If jsonResult.ContainsKey("uploadedFile") Then
'                Dim fileurl = jsonResult("uploadedFile")
'                Dim byteArray As Byte() = Convert.FromBase64String(fileurl)
'                Dim stream As MemoryStream = New MemoryStream(byteArray)
'                helper.Load(stream)
'            End If

'            Dim output As String = JsonConvert.SerializeObject(helper.ProcessPdf(jsonResult))
'            Return output
'        End Function

'        Public Function DocumentRetrieve(ByVal jsonResult As Dictionary(Of String, String)) As Object
'            Dim filename = jsonResult("documentName")
'            Dim filePath = Helper.GetFilePath(filename & ".pdf")
'            Dim bytes = File.ReadAllBytes(filePath)
'            Dim streamFile As MemoryStream = New MemoryStream(bytes)
'            Dim dataString = Convert.ToBase64String(bytes)
'            Return "data:application/pdf;base64," & dataString
'        End Function
'    End Class

'Public Class Helper
'    Public Shared Function GetFilePath(ByVal path As String) As String
'        Dim _dataPath As String = GetCommonFolder(New DirectoryInfo(HttpContext.Current.Request.PhysicalApplicationPath))
'        _dataPath += "\" & path
'        Return _dataPath
'    End Function

'    Private Shared Function GetCommonFolder(ByVal dtInfo As DirectoryInfo) As String
'        Dim _folderNames = dtInfo.GetDirectories("Data")

'        If _folderNames.Length > 0 Then
'            Return _folderNames(0).FullName
'        End If

'        Return If(dtInfo.Parent IsNot Nothing, GetCommonFolder(dtInfo.Parent), String.Empty)
'    End Function
'End Class
'End Namespace
