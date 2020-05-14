Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports Syncfusion.EJ2.PdfViewer
Imports System.Web.Http
Imports Newtonsoft.Json
Imports System.IO
Imports System.Reflection

Namespace Controllers
    Partial Public Class PdfViewerController
        Inherits Controller

        Public Sub New()
        End Sub

        Public Function [Default]() As ActionResult
            Return View()
        End Function
        Public Function Index() As ActionResult
            Return View()
        End Function
        Public Function Index7() As ActionResult
            Return View()
        End Function
        Public Function Index8() As ActionResult
            Return View()
        End Function
        <System.Web.Mvc.HttpPost>
        Public Function Load(ByVal jsonObject As jsonObjects) As ActionResult
            Try

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

            Catch ex As Exception

            End Try
        End Function

        Public Function JsonConverter(ByVal results As jsonObjects) As Dictionary(Of String, String)
            Try
                Dim resultObjects As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
                resultObjects = results.[GetType]().GetProperties(BindingFlags.Instance Or BindingFlags.[Public]).ToDictionary(Function(prop) prop.Name, Function(prop) prop.GetValue(results, Nothing))
                Dim emptyObjects = (From kv In resultObjects Where kv.Value IsNot Nothing Select kv).ToDictionary(Function(kv) kv.Key, Function(kv) kv.Value)
                Dim jsonResult As Dictionary(Of String, String) = emptyObjects.ToDictionary(Function(k) k.Key, Function(k) k.Value.ToString())
                Return jsonResult
            Catch ex As Exception

            End Try
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function RenderPdfPages(ByVal jsonObject As jsonObjects) As ActionResult
            Try
                Dim pdfviewer As PdfRenderer = New PdfRenderer()
                Dim jsonData = JsonConverter(jsonObject)
                Dim jsonResult As Object = pdfviewer.GetPage(jsonData)
                Return Content(JsonConvert.SerializeObject(jsonResult))
            Catch ex As Exception

            End Try
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function RenderAnnotationComments(ByVal jsonObject As jsonObjects) As ActionResult
            Try
                Dim pdfviewer As PdfRenderer = New PdfRenderer()
                Dim jsonData = JsonConverter(jsonObject)
                Dim jsonResult As Object = pdfviewer.GetAnnotationComments(jsonData)
                Return Content(JsonConvert.SerializeObject(jsonResult))
            Catch ex As Exception

            End Try
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function Unload(ByVal jsonObject As jsonObjects) As ActionResult
            Try
                Dim pdfviewer As PdfRenderer = New PdfRenderer()
                Dim jsonData = JsonConverter(jsonObject)
                pdfviewer.ClearCache(jsonData)
                Return Me.Content("Document cache is cleared")
            Catch ex As Exception

            End Try
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function RenderThumbnailImages(ByVal jsonObject As jsonObjects) As ActionResult
            Try
                Dim pdfviewer As PdfRenderer = New PdfRenderer()
                Dim jsonData = JsonConverter(jsonObject)
                Dim result As Object = pdfviewer.GetThumbnailImages(jsonData)
                Return Content(JsonConvert.SerializeObject(result))
            Catch ex As Exception

            End Try
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function Bookmarks(ByVal jsonObject As jsonObjects) As ActionResult
            Try
                Dim pdfviewer As PdfRenderer = New PdfRenderer()
                Dim jsonData = JsonConverter(jsonObject)
                Dim jsonResult As Object = pdfviewer.GetBookmarks(jsonData)
                Return Content(JsonConvert.SerializeObject(jsonResult))
            Catch ex As Exception

            End Try
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function Download(ByVal jsonObject As jsonObjects) As ActionResult
            Try
                Dim pdfviewer As PdfRenderer = New PdfRenderer()
                Dim jsonData = JsonConverter(jsonObject)
                Dim documentBase As String = pdfviewer.GetDocumentAsBase64(jsonData)
                Return Content(documentBase)
            Catch ex As Exception

            End Try
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function PrintImages(ByVal jsonObject As jsonObjects) As ActionResult
            Try
                Dim pdfviewer As PdfRenderer = New PdfRenderer()
                Dim jsonData = JsonConverter(jsonObject)
                Dim pageImage As Object = pdfviewer.GetPrintImage(jsonData)
                Return Content(JsonConvert.SerializeObject(pageImage))
            Catch ex As Exception

            End Try
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function ExportAnnotations(ByVal jsonObject As jsonObjects) As ActionResult
            Try
                Dim pdfviewer As PdfRenderer = New PdfRenderer()
                Dim jsonData = JsonConverter(jsonObject)
                Dim jsonResult As String = pdfviewer.GetAnnotations(jsonData)
                Return Content(jsonResult)
            Catch ex As Exception

            End Try
        End Function

        <System.Web.Mvc.HttpPost>
        Public Function ImportAnnotations(ByVal jsonObject As jsonObjects) As ActionResult
            Try
                Dim pdfviewer As PdfRenderer = New PdfRenderer()
                Dim jsonResult As String = String.Empty
                Dim jsonData = JsonConverter(jsonObject)

                If jsonObject IsNot Nothing AndAlso jsonData.ContainsKey("fileName") Then
                    Dim documentPath As String = GetDocumentPath(jsonData("fileName"))

                    If Not String.IsNullOrEmpty(documentPath) Then
                        jsonResult = System.IO.File.ReadAllText(documentPath)
                    Else
                        Return Content(jsonData("document") & " is not found")
                    End If
                End If

                Return Content(jsonResult)
            Catch ex As Exception

            End Try
        End Function

        Private Function GetDocumentPath(ByVal document As String) As String
            Try
                Dim documentPath As String = String.Empty

                If Not System.IO.File.Exists(document) Then
                    Dim path = HttpContext.Request.PhysicalApplicationPath
                    If System.IO.File.Exists(path & "App_Data\Data\" & document) Then documentPath = path & "App_Data\Data\" & document
                Else
                    documentPath = document
                End If

                Return documentPath
            Catch ex As Exception

            End Try
        End Function

        <System.Web.Http.HttpGet>
        Public Function [Get]() As IEnumerable(Of String)
            Return New String() {"value1", "value2"}
        End Function



        Public Function Details(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        Public Function Create() As ActionResult
            Return View()
        End Function

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

        Public Function Delete(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function
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
End Namespace
