Imports System.IO
Imports CCGData
Imports CCGData.CCGData
Imports DocumentFormat.OpenXml.Office
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Word
Imports Newtonsoft.Json
Imports NLog
Imports Syncfusion.DocIO

Namespace Controllers
    Public Class AdminController
        Inherits Controller

        Private DataRepository As New DataRepository
        Private Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()


        ' GET: Admin
        Function Index() As ActionResult
            Return View()
        End Function
        'Function Search() As ActionResult
        '    Return View()
        'End Function

        Function Upload() As ActionResult
            Return View()
        End Function

        Function UploadRec() As ActionResult
            Return View()
        End Function


        Function Search() As ActionResult
            Try

                Dim FacilityGroups As List(Of FacilityGroup) = DataRepository.GetFacilityGroups2
                ViewBag.FacilityGroups = FacilityGroups
                ViewBag.jsonFacilityGroups = JsonConvert.SerializeObject(FacilityGroups)

                Dim Facilities As List(Of Facility) = DataRepository.GetFacilites2
                ViewBag.Facilities = Facilities
                ViewBag.jsonFacilities = JsonConvert.SerializeObject(Facilities)

                Dim Users As List(Of User) = DataRepository.GetUsers2
                ViewBag.Users = Users
                ViewBag.jsonUsers = JsonConvert.SerializeObject(Users)

                Dim States As List(Of String) = DataRepository.GetSates2
                ViewBag.States = States
                ViewBag.jsonStates = JsonConvert.SerializeObject(States)

                Dim JobTitles As List(Of JobTitle) = DataRepository.GetJobTitles
                ViewBag.JobTitles = JobTitles
                ViewBag.jsonJobTitles = JsonConvert.SerializeObject(JobTitles)

                Return PartialView("Search")
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function
        Function WebUsers() As ActionResult
            Try
                Dim Users As List(Of AspNetUser) = DataRepository.GetWebUsers
                ViewBag.Users = Users
                ViewBag.jsonUsers = JsonConvert.SerializeObject(Users)


                Return PartialView("WebUsers")
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function




        '   <AcceptVerbs("Post")>
        '   Public Async Sub Save(ByVal UploadFiles As IList(Of IFormFile))
        '       Try
        '
        '           For Each UpFile In UploadFiles
        '               Const accountName As String = "compliancecgstorage"
        '               Const key As String = "examplekey"
        '               Dim storageAccount = New CloudStorageAccount(New StorageCredentials(accountName, key), True)
        '               Dim blobClient = storageAccount.CreateCloudBlobClient()
        '               Dim container = blobClient.GetContainerReference("policies")
        '               Await container.CreateIfNotExistsAsync()
        '               Await container.SetPermissionsAsync(New BlobContainerPermissions() With {.PublicAccess = BlobContainerPublicAccessType.Blob})
        '               Dim blob = container.GetBlockBlobReference("test.jpg")
        '
        '               Using stream = File.OpenReadStream()
        '                   Await blob.UploadFromStreamAsync(stream)
        '               End Using
        '           Next
        '
        '       Catch e As Exception
        '           Response.Clear()
        '           Response.StatusCode = 204
        '           Response.HttpContext.Features.[Get](Of IHttpResponseFeature)().ReasonPhrase = "File failed to upload"
        '           Response.HttpContext.Features.[Get](Of IHttpResponseFeature)().ReasonPhrase = e.Message
        '       End Try
        '   End Sub
        <AcceptVerbs("Post")>
        Public Sub SaveRec() 'As Threading.Tasks.Task
            Dim file = HttpContext.Request.Form("File")

            Try

                Dim httpPostedFile = System.Web.HttpContext.Current.Request.Files("UploadFiles")

                If httpPostedFile IsNot Nothing Then
                    Dim fileSave = System.Web.HttpContext.Current.Server.MapPath("~/Resources/TrainingRecordings/")
                    Dim fileSavePath = Path.Combine(fileSave, httpPostedFile.FileName)

                    If Not System.IO.File.Exists(fileSavePath) Then
                        httpPostedFile.SaveAs(fileSavePath)

                        ' If Uploaded = True Then
                        Dim Response As HttpResponse = System.Web.HttpContext.Current.Response
                        Response.Clear()
                        Response.ContentType = "application/json; charset=utf-8"
                        Response.StatusDescription = "File uploaded succesfully"
                        Response.[End]()
                        ' End If

                        ' If Uploaded = False Then
                        ' Dim Response As HttpResponse = System.Web.HttpContext.Current.Response
                        ' Response.Clear()
                        ' Response.Status = "File failed to upload"
                        ' Response.StatusCode = 409
                        ' Response.StatusDescription = "File failed to upload"
                        ' Response.[End]()
                        'End If
                    Else
                        Dim Response As HttpResponse = System.Web.HttpContext.Current.Response
                        Response.Clear()
                        Response.Status = "File already exists"
                        Response.StatusCode = 409
                        Response.StatusDescription = "File already exists"
                        Response.[End]()
                    End If
                End If



            Catch e As Exception
                Dim Response As HttpResponse = System.Web.HttpContext.Current.Response
                Response.Clear()
                Response.ContentType = "application/json; charset=utf-8"
                Response.StatusCode = 409
                Response.Status = "No Content"
                Response.StatusDescription = e.Message
                Response.[End]()
            End Try
        End Sub
        <AcceptVerbs("Post")>
        Public Async Function Save() As Threading.Tasks.Task
            Dim file = HttpContext.Request.Form("File")

            Try

                Dim httpPostedFile = System.Web.HttpContext.Current.Request.Files("UploadFiles")

                If httpPostedFile IsNot Nothing Then
                    Dim fileSave = System.Web.HttpContext.Current.Server.MapPath("UploadedFiles")
                    Dim fileSavePath = Path.Combine(fileSave, httpPostedFile.FileName)

                    If Not System.IO.File.Exists(fileSavePath) Then
                        Dim Uploaded = Await Upload2a(httpPostedFile)
                        If Uploaded = True Then
                            Dim Response As HttpResponse = System.Web.HttpContext.Current.Response
                            Response.Clear()
                            Response.ContentType = "application/json; charset=utf-8"
                            Response.StatusDescription = "File uploaded succesfully"
                            Response.[End]()
                        End If

                        If Uploaded = False Then
                            Dim Response As HttpResponse = System.Web.HttpContext.Current.Response
                            Response.Clear()
                            Response.Status = "File failed to upload"
                            Response.StatusCode = 409
                            Response.StatusDescription = "File failed to upload"
                            Response.[End]()
                        End If
                    Else
                        Dim Response As HttpResponse = System.Web.HttpContext.Current.Response
                        Response.Clear()
                        Response.Status = "File already exists"
                        Response.StatusCode = 409
                        Response.StatusDescription = "File already exists"
                        Response.[End]()
                    End If
                End If



            Catch e As Exception
                Dim Response As HttpResponse = System.Web.HttpContext.Current.Response
                Response.Clear()
                Response.ContentType = "application/json; charset=utf-8"
                Response.StatusCode = 409
                Response.Status = "No Content"
                Response.StatusDescription = e.Message
                Response.[End]()
            End Try
        End Function


        <AcceptVerbs("Post")>
        Public Sub Remove()
            Try
                Dim httpPostedFile = System.Web.HttpContext.Current.Request.Files("UploadFiles")
                Dim fileSave = System.Web.HttpContext.Current.Server.MapPath("UploadedFiles")
                Dim fileSavePath = Path.Combine(fileSave, httpPostedFile.FileName)

                If System.IO.File.Exists(fileSavePath) Then
                    System.IO.File.Delete(fileSavePath)
                End If

                Dim Response As HttpResponse = System.Web.HttpContext.Current.Response
                Response.Clear()
                Response.Status = "200 OK"
                Response.StatusCode = 200
                Response.ContentType = "application/json; charset=utf-8"
                Response.StatusDescription = "File removed succesfully"
                Response.[End]()
            Catch e As Exception
                Dim Response As HttpResponse = System.Web.HttpContext.Current.Response
                Response.Clear()
                Response.Status = "200 OK"
                Response.StatusCode = 200
                Response.ContentType = "application/json; charset=utf-8"
                Response.StatusDescription = "File removed succesfully"
                Response.[End]()
            End Try
        End Sub

        Public Async Function Upload2a(ByVal httpPostedFile As HttpPostedFile) As Threading.Tasks.Task(Of Boolean)
            Try
                If httpPostedFile IsNot Nothing AndAlso httpPostedFile.ContentLength > 0 Then
                    Dim InputStream As Stream = httpPostedFile.InputStream

                    Dim b As BinaryReader = New BinaryReader(InputStream)
                    Dim binData As Byte() = b.ReadBytes(InputStream.Length)

                    Dim tmpFile = Path.GetTempFileName()
                    System.IO.File.WriteAllBytes(tmpFile, binData)


                    Dim File As New FileInfo(tmpFile)
                    Dim WordFunctions As New WordFunctions
                    WordFunctions.SetUpDocument2(File)


                    'Dim WordFunctions As New WordFunctions
                    'WordFunctions.SetBookMarks2(tmpFile)


                    'Dim WordDocument As New Syncfusion.DocIO.DLS.WordDocument
                    'WordDocument.Open(tmpFile, FormatType.Docx)

                    'Dim app As Application = New Word.Application()
                    'Dim WordFile As Word.Document = app.Documents.Open(tmpFile)

                    ' SetUpDocument(WordFile)

                    'WordFile.Save()
                    'WordFile.Close()
                    'app.Quit()

                    Dim Folders = tmpFile.Split("\")
                    Dim Folders2 = Folders.Take(Folders.Count - 1)
                    Dim tmpFileRename = String.Join("\", Folders2) & "\" & httpPostedFile.FileName

                    If My.Computer.FileSystem.FileExists(tmpFileRename) Then
                        System.IO.File.SetAttributes(tmpFileRename, FileAttributes.Normal)
                        System.IO.File.Delete(tmpFileRename)
                    End If
                    My.Computer.FileSystem.RenameFile(tmpFile, httpPostedFile.FileName)

                    Dim AzureFiles As New AzureFiles
                    Dim Uploaded = Await AzureFiles.UploadBlobFile("policies", tmpFileRename)
                    Return Uploaded
                    End If

            Catch ex As Exception
                logger.Error(ex)
                Return False
            End Try
        End Function

        Private Function SetUpDocument(WordFile As Word.Document)
            Try
                Dim FullName As String = WordFile.FullName
                Dim WordFunctions As New WordFunctions

                WordFunctions.SetBookMarks(WordFile)
                '  WordFunctions.SetHyperLinks(WordFile)
            Catch ex As Exception
                logger.Error(ex)
            End Try
        End Function


        Function SearchRequestWU() As String
            Try
                Dim jsonString As String = New StreamReader(Me.Request.InputStream).ReadToEnd()
                jsonString = jsonString.Replace("""User"":null", """User"":0")
                Dim Search As Search = JsonConvert.DeserializeObject(Of Search)(jsonString)

                Dim AllUsers As List(Of AspNetUser) = DataRepository.GetWebUsersSearch(Search)
                Return JsonConvert.SerializeObject(AllUsers, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})

            Catch ex As Exception
                logger.Error(ex)

            End Try


        End Function

        Function SearchRequest() As String
            Try
                Dim jsonString As String = New StreamReader(Me.Request.InputStream).ReadToEnd()
                jsonString = jsonString.Replace("""User"":null", """User"":0")
                jsonString = jsonString.Replace("""Group"":null", """Group"":0")
                jsonString = jsonString.Replace("""Facility"":null", """Facility"":0")
                Dim Search As Search = JsonConvert.DeserializeObject(Of Search)(jsonString)




                If Search.DisplayResult = "Users" Then
                    Dim AllUsers As List(Of GetUser) = DataRepository.GetUsersSearch(Search)
                    Return JsonConvert.SerializeObject(AllUsers, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
                End If

                If Search.DisplayResult = "Facilities" Then
                    Dim Facilities As List(Of GetFacility) = DataRepository.GetFacilitiesBySearch(Search)
                    Return JsonConvert.SerializeObject(Facilities, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
                End If


                If Search.Control = "Title" Then
                    Dim UsersByTitle As List(Of UsersByTitle) = DataRepository.GetUsersByTitle(Search.Title)
                    'Dim Users As List(Of User) = DataRepository.GetUsersByTitle(TitleID)
                    Return JsonConvert.SerializeObject(UsersByTitle, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
                End If

                If Search.Control = "Group" Then
                    Dim Facilities As List(Of GetFacility) = DataRepository.GetFacilitiesBySearch(Search)
                    Return JsonConvert.SerializeObject(Facilities, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
                End If


            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function
        Function SearchFacilityByGroup(GroupID As Integer) As String
            Try
                Dim Facilities As List(Of Facility) = DataRepository.GetFacilitesByGroup(GroupID)
                Return JsonConvert.SerializeObject(Facilities, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Function SearchFacilityByFacility(FacilityID As Integer) As String
            Try
                Dim Facilities As List(Of Facility) = DataRepository.GetFacilityByID(FacilityID)
                Return JsonConvert.SerializeObject(Facilities, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function
        Function SearchFacility(UserID As Integer) As String
            Try
                Dim Facilities As List(Of Facility) = DataRepository.GetFacilityByUser(UserID)


                For Each Facility As Facility In Facilities
                    Facility.Roles = Facility.FacilityUsers.Where(Function(f) f.FacilityID = Facility.FacilityID And f.UserID = UserID).Select(Function(f) f.Roles).SingleOrDefault
                Next

                Return JsonConvert.SerializeObject(Facilities, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})

            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function
        Function SearchFacilityByUser(UserID As String) As String
            Try
                Dim Facilities As List(Of Facility) = DataRepository.GetFacilityByUser(UserID)


                For Each Facility As Facility In Facilities
                    Facility.Roles = Facility.FacilityUsers.Where(Function(f) f.FacilityID = Facility.FacilityID And f.UserID = UserID).Select(Function(f) f.Roles).SingleOrDefault
                Next

                Return JsonConvert.SerializeObject(Facilities, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})

            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Function SearchFacilities(Search As Search) As String
            Try
                Dim Facilities As List(Of GetFacility) = DataRepository.GetFacilitiesBySearch(Search)
                Return JsonConvert.SerializeObject(Facilities, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})

            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Function SearchUsersByTitle(TitleID As Integer) As String
            Try
                Dim UsersByTitle As List(Of UsersByTitle) = DataRepository.GetUsersByTitle(TitleID)
                'Dim Users As List(Of User) = DataRepository.GetUsersByTitle(TitleID)
                Return JsonConvert.SerializeObject(UsersByTitle, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})

            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

    End Class
End Namespace