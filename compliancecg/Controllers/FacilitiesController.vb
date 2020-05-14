Imports System.IO
Imports System.Web.Http
Imports System.Web.Mvc
Imports CCGData
Imports CCGData.CCGData
Imports CCGData.CCGData.CCGDataEntities
Imports Newtonsoft.Json
Imports Syncfusion.EJ2.Base
Imports Syncfusion.EJ2.Navigations
Imports Syncfusion.EJ2.Popups
Imports NLog




Namespace Controllers

    '<System.Web.Http.Authorize(Roles:="Admin")>
    Public Class FacilitiesController
        Inherits Controller

        Private DataRepository As New DataRepository
        Private Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()



        'Public Sub New(ByVal OrderID As Integer, ByVal CustomerId As String, ByVal EmployeeId As Integer, ByVal Freight As String, ByVal Verified As Boolean, ByVal OrderDate As DateTime, ByVal ShipCity As String, ByVal ShipName As String, ByVal ShipCountry As String, ByVal ShippedDate As DateTime, ByVal ShipAddress As String)
        '    Me.OrderID = OrderID
        '    Me.CustomerID = CustomerId
        '    Me.EmployeeID = EmployeeId
        '    Me.Freight = Freight
        '    Me.ShipCity = ShipCity
        '    Me.Verified = Verified
        '    Me.OrderDate = OrderDate
        '    Me.ShipName = ShipName
        '    Me.ShipCountry = ShipCountry
        '    Me.ShippedDate = ShippedDate
        '    Me.ShipAddress = ShipAddress
        'End Sub

        Public Shared Function GetAllRecords() As List(Of OrdersDetails)
            Dim order As List(Of OrdersDetails) = New List(Of OrdersDetails)()
            Dim code As Integer = 10000

            For i As Integer = 1 To 2 - 1
                order.Add(New OrdersDetails(code + 1, "ALFKI", i + 0, "2.3", False, New DateTime(2016, 12, 25), "Berlin", "Simons bistro", "Denmark", New DateTime(1996, 7, 16), "Kirchgasse 6"))
                order.Add(New OrdersDetails(code + 2, "ANATR", i + 2, "3.3", True, New DateTime(2015, 1, 1), "Madrid", "Queen Cozinha", "Brazil", New DateTime(1996, 9, 11), "Avda. Azteca 123"))
                order.Add(New OrdersDetails(code + 3, "ANTON", i + 1, "4.3", True, New DateTime(2016, 8, 15), "Cholchester", "Frankenversand", "Germany", New DateTime(1996, 10, 7), "Carrera 52 con Ave. Bolívar #65-98 Llano Largo"))
                order.Add(New OrdersDetails(code + 4, "BLONP", i + 3, "5.3", False, New DateTime(2016, 12, 8), "Marseille", "Ernst Handel", "Austria", New DateTime(1996, 12, 30), "Magazinweg 7"))
                order.Add(New OrdersDetails(code + 5, "BOLID", i + 4, "6.3", True, New DateTime(2016, 12, 9), "Tsawassen", "Hanari Carnes", "Switzerland", New DateTime(1997, 12, 3), "1029 - 12th Ave. S."))
                order.Add(New OrdersDetails(code + 5, "BOLID", i + 4, "6.3", True, New DateTime(2017, 1, 1), "Tsawassen", "Hanari Carnes", "Switzerland", New DateTime(1997, 12, 3), "1029 - 12th Ave. S."))
                code += 5
            Next

            Return order
        End Function

        Public Property OrderID As Integer?
        Public Property CustomerID As String
        Public Property EmployeeID As Integer?
        Public Property Freight As String
        Public Property ShipCity As String
        Public Property Verified As Boolean
        Public Property OrderDate As DateTime
        Public Property ShipName As String
        Public Property ShipCountry As String
        Public Property ShippedDate As DateTime
        Public Property ShipAddress As String




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




                'If Search.Control = "User" Then
                '    Dim Facilities As List(Of Facility) = DataRepository.GetFacilityByUser(Search.User)
                '    For Each Facility As Facility In Facilities
                '        Facility.Roles = Facility.FacilityUsers.Where(Function(f) f.FacilityID = Facility.FacilityID And f.UserID = Search.User).Select(Function(f) f.Roles).SingleOrDefault
                '    Next
                '    Return JsonConvert.SerializeObject(Facilities, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
                'End If

                'If Search.Control = "State" Then
                '    If Search.DisplayResult = "Users" Then
                '        Dim AllUsers As List(Of GetUser) = DataRepository.GetUsersByState(Search.State)

                '        'Dim Facilities As List(Of Facility) = DataRepository.GetFacilitiesBySearch(Search)
                '        'Dim AllUsers As New List(Of GetUser)=
                '        'For Each Facility As Facility In Facilities
                '        '    Dim Users As List(Of GetUser) = DataRepository.GetUsersByState(Facility.FacilityID)
                '        '    If Users IsNot Nothing Then
                '        '        AllUsers.AddRange(Users)
                '        '    End If
                '        'Next

                '        Return JsonConvert.SerializeObject(AllUsers, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})

                '    End If
                '    If Search.DisplayResult = "Facilities" Then
                '        Dim Facilities As List(Of Facility) = DataRepository.GetFacilitiesBySearch(Search)
                '        Return JsonConvert.SerializeObject(Facilities, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
                '    End If
                'End If

                If Search.Control = "Title" Then
                    Dim UsersByTitle As List(Of UsersByTitle) = DataRepository.GetUsersByTitle(Search.Title)
                    'Dim Users As List(Of User) = DataRepository.GetUsersByTitle(TitleID)
                    Return JsonConvert.SerializeObject(UsersByTitle, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
                End If

                If Search.Control = "Group" Then
                    Dim Facilities As List(Of GetFacility) = DataRepository.GetFacilitiesBySearch(Search)
                    Return JsonConvert.SerializeObject(Facilities, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
                End If

                'If Search.Control = "Facility" Then
                '    Dim Facilities As List(Of Facility) = DataRepository.GetFacilitiesBySearch(Search)
                '    Return JsonConvert.SerializeObject(Facilities, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
                'End If
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




        Function Index() As ActionResult
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






        Public Function GetFacilityUserDropDownList() As List(Of DropDownDataDetails)
            Try

                Dim Users As List(Of CCGData.CCGData.User) = DataRepository.GetUsers
                Dim FacilityUserDropDownList As New List(Of DropDownDataDetails)


                For Each User As CCGData.CCGData.User In Users
                    Dim FullName As String = Nothing

                    ' FullName = $"{FacilityUser?.FirstName} {FacilityUser?.LastName}"
                    If User?.FirstName?.Length > 0 Then FullName = $"{User?.LastName}"
                    If User?.LastName?.Length > 0 Then FullName = $"{FullName}, {User?.FirstName}"
                    ' If User?.EmailAddress?.Length > 0 Then FullName = $"{FullName}: ({User?.EmailAddress})"

                    If FullName?.Length > 2 Then
                        FacilityUserDropDownList.Add(New DropDownDataDetails() With {.text = FullName, .value = User.UserID, .value2 = 123})
                    End If
                Next



                'For Each User As CCGData.CCGData.User In Users
                '    FacilityUserDropDownList.Add(New DropDownDataDetails() With {.text = User.LastName, .value = User.UserID})
                'Next

                Return FacilityUserDropDownList
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Public Function GetUsersDropDownList() As List(Of DropDownDataDetails)
            Try

                Dim Users As List(Of CCGData.CCGData.User) = DataRepository.GetUsers
                Dim FacilityUserDropDownList As New List(Of DropDownDataDetails)


                For Each User As CCGData.CCGData.User In Users
                    Dim FullName As String = Nothing

                    ' FullName = $"{FacilityUser?.FirstName} {FacilityUser?.LastName}"
                    If User?.FirstName?.Length > 0 Then FullName = $"{User?.LastName}"
                    If User?.LastName?.Length > 0 Then FullName = $"{FullName}, {User?.FirstName}"
                    ' If User?.EmailAddress?.Length > 0 Then FullName = $"{FullName}: ({User?.EmailAddress})"

                    If FullName?.Length > 2 Then
                        FacilityUserDropDownList.Add(New DropDownDataDetails() With {.text = FullName, .value = User.UserID, .value2 = 123})
                    End If
                Next



                'For Each User As CCGData.CCGData.User In Users
                '    FacilityUserDropDownList.Add(New DropDownDataDetails() With {.text = User.LastName, .value = User.UserID})
                'Next

                Return FacilityUserDropDownList
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function


        Public Function GetGroupUserDropDownList2(GroupID) As List(Of DropDownDataDetails)
            Try

                Dim Users As List(Of CCGData.CCGData.User) = DataRepository.GetUsersFromGroup(GroupID)
                Dim FacilityUserDropDownList As New List(Of DropDownDataDetails)


                For Each User As CCGData.CCGData.User In Users
                    Dim FullName As String = Nothing

                    ' FullName = $"{FacilityUser?.FirstName} {FacilityUser?.LastName}"
                    If User?.FirstName?.Length > 0 Then FullName = $"{User?.LastName}"
                    If User?.LastName?.Length > 0 Then FullName = $"{FullName}, {User?.FirstName}"
                    ' If User?.EmailAddress?.Length > 0 Then FullName = $"{FullName}: ({User?.EmailAddress})"

                    If FullName?.Length > 2 Then
                        FacilityUserDropDownList.Add(New DropDownDataDetails() With {.text = FullName, .value = User.UserID})
                    End If
                Next



                'For Each User As CCGData.CCGData.User In Users
                '    FacilityUserDropDownList.Add(New DropDownDataDetails() With {.text = User.LastName, .value = User.UserID})
                'Next

                Return FacilityUserDropDownList
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Public Function GetJobTitlesDropDownList() As List(Of DropDownDataDetails)
            Try
                Dim JobTitles As List(Of JobTitle) = DataRepository.GetJobTitles
                Dim FacilityUserDropDownList As New List(Of DropDownDataDetails)

                If JobTitles.Count > 0 Then
                    For Each JobTitle As JobTitle In JobTitles
                        If JobTitle.Title?.Length > 0 Then
                            FacilityUserDropDownList.Add(New DropDownDataDetails() With {.text = JobTitle.Title, .value = JobTitle.JobTitleID})
                        End If
                    Next
                End If

                Return FacilityUserDropDownList
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function


        Function FacilitiesPartial() As ActionResult
            Return PartialView()
        End Function

        Function GroupAdmins() As ActionResult
            Return PartialView()
        End Function
        Function GetUserEmails() As String
            Dim UserEmails = DataRepository.GetUsers.Select(Function(u) u.EmailAddress).ToList
            ' Return UserEmails
            Return JsonConvert.SerializeObject(UserEmails, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
        End Function
        Function GetFacilitUserEmails(FacilityID As Integer) As String
            Dim UserEmails = DataRepository.GetFacilitesUsers(FacilityID).Select(Function(u) u.User.EmailAddress).ToList
            ' Return UserEmails
            Return JsonConvert.SerializeObject(UserEmails, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
        End Function
        Function GetFacilityInfo() As String
            Dim UserEmails = DataRepository.GetUsers.Select(Function(u) u.EmailAddress).ToList

            GetFacilityUsers()
            ' Return UserEmails
            Return JsonConvert.SerializeObject(UserEmails, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
        End Function

        '<AcceptVerbs(HttpVerbs.Post)>
        Function Main() As ActionResult
            Try
                Dim Facilites As New List(Of Facility)
                Dim FacilityGroups As New List(Of FacilityGroup)
                Dim UserEmails = DataRepository.GetUsers.Select(Function(u) u.EmailAddress).ToList


                Dim CurrentUser As User = HomeController.GetCurrentUser()
                If CurrentUser Is Nothing Then
                    Facilites = DataRepository.GetFacilites2
                    FacilityGroups = DataRepository.GetFacilityGroups2
                End If

                If CurrentUser IsNot Nothing Then
                    If CurrentUser.EmailAddress = "info@compliancecg.com" Then
                        Facilites = DataRepository.GetFacilites2
                        FacilityGroups = DataRepository.GetFacilityGroups2
                    Else
                        Facilites = DataRepository.GetFacilities2(CurrentUser.EmailAddress)
                        FacilityGroups = DataRepository.GetFacilitiesGroup2(CurrentUser.EmailAddress)
                    End If
                End If

                'Facilites = DataRepository.GetFacilites2
                'FacilityGroups = DataRepository.GetFacilityGroups2

                ViewBag.FacilityGroups = JsonConvert.SerializeObject(FacilityGroups)
                ViewBag.Facilites = JsonConvert.SerializeObject(Facilites)


                Dim TabHeaders As New List(Of TabHeader)
                For Each Group As FacilityGroup In FacilityGroups
                    TabHeaders.Add(New TabHeader With {.Text = Group.GroupName, .Id = Group.FacilityGroupID})
                Next
                ViewBag.TabHeaders = TabHeaders


                'Dim buttons As New List(Of DialogDialogButton)
                'buttons.Add(New DialogDialogButton() With {.Click = "dlgButtonClick", .ButtonModel = New ButtonModel() With {.content = "LEARN ABOUT SYNCFUSION, INC.", .isPrimary = True}})
                'ViewBag.DefaultButtons = buttons

                Dim FacilityUserDropDownList As New List(Of DropDownDataDetails) '= GetFacilityUserDropDownList()
                Dim JobTitlesDropDownList As List(Of DropDownDataDetails) = GetJobTitlesDropDownList()
                Dim UsersDropDownList As List(Of DropDownDataDetails) = GetUsersDropDownList()


                ViewBag.FacilityUserDropDownList = FacilityUserDropDownList
                ViewBag.UsersDropDownList = UsersDropDownList
                'ViewBag.FacilityUserDropDownList2 = JsonConvert.SerializeObject(FacilityUserDropDownList.Take(10))
                ViewBag.JobTitlesDropDownList = JobTitlesDropDownList
                ViewBag.UserEmails = JsonConvert.SerializeObject(UserEmails)


                ViewBag.ordersData = OrdersDetails.GetAllRecords().ToArray()

                Return PartialView()
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Function Details() As ActionResult

            Try

                'Dim buttons As New List(Of DialogDialogButton)

                'buttons.Add(New DialogDialogButton() With {.Click = "dlgButtonClick", .ButtonModel = New DefaultButtonModel() With {.content = "LEARN ABOUT SYNCFUSION, INC.", .isPrimary = True}})
                'ViewBag.DefaultButtons = buttons

                Dim Facilites = DataRepository.GetFacilites2
                Dim FacilityGroups = DataRepository.GetFacilityGroups2

                Dim TabHeaders As New List(Of TabHeader)
                For Each Group As FacilityGroup In FacilityGroups
                    TabHeaders.Add(New TabHeader With {.Text = Group.GroupName, .Id = Group.FacilityGroupID})
                Next

                ViewBag.FacilityGroups = JsonConvert.SerializeObject(FacilityGroups)
                ViewBag.Facilites = JsonConvert.SerializeObject(Facilites)
                ViewBag.TabHeaders = TabHeaders



                ViewBag.button1 = New With {.content = "YES", .isPrimary = True}
                ViewBag.button2 = New With {.content = "NO"}

                Return View()
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function



        Function GetModule(partialName As String) As ActionResult
            Return PartialView(partialName)
        End Function

        Public Function SetFacilityGroupSessionValues() As Boolean
            Try
                Dim jsonString As [String] = New StreamReader(Me.Request.InputStream).ReadToEnd()
                Dim FacilityGroup As FacilityGroup = JsonConvert.DeserializeObject(Of FacilityGroup)(jsonString)

                Session("FacilityGroup") = FacilityGroup
                Return True
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function


        Public Function GetGroupUsers() As String
            Try
                Dim jsonString As [String] = New StreamReader(Me.Request.InputStream).ReadToEnd()
                Dim FacilityGroup As FacilityGroup = JsonConvert.DeserializeObject(Of FacilityGroup)(jsonString)

                Dim GroupUserDropDownList As List(Of DropDownDataDetails) = GetGroupUserDropDownList2(FacilityGroup.FacilityGroupID)
                Return JsonConvert.SerializeObject(GroupUserDropDownList)
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function


        Public Function GetFacilityUsers() As String
            Try
                Dim jsonString As [String] = New StreamReader(Me.Request.InputStream).ReadToEnd()
                Dim Facility As Facility = JsonConvert.DeserializeObject(Of Facility)(jsonString)

                Dim FacilityUsers As List(Of FacilityUser) = DataRepository.GetFacilitesUsers2(Facility.FacilityID)

                Dim Users As New List(Of User)
                For Each FacilityUser As FacilityUser In FacilityUsers
                    Dim User = DataRepository.GetUser2(FacilityUser.UserID)
                    User.Roles = FacilityUser.Roles
                    Users.Add(User)
                Next
                Return JsonConvert.SerializeObject(Users, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function
        Public Function GetFacilityUsers(FacilityID As Integer) As String
            Try

                Dim FacilityUsers As List(Of FacilityUser) = DataRepository.GetFacilitesUsers2(FacilityID)

                Dim UserEmails As New List(Of String)
                ' Return UserEmails



                Dim Users As New List(Of User)
                For Each FacilityUser As FacilityUser In FacilityUsers
                    Dim User = DataRepository.GetUser2(FacilityUser.UserID)
                    UserEmails.Add(User.EmailAddress)
                Next

                Return JsonConvert.SerializeObject(UserEmails, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Public Function SetFacilitySessionValues() As Boolean
            Try
                Dim jsonString As [String] = New StreamReader(Me.Request.InputStream).ReadToEnd()
                Dim Facility As Facility = JsonConvert.DeserializeObject(Of Facility)(jsonString)

                Session("Facility") = Facility
                Return True
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function
        Public Function SetFacilityActiveSessionValues(Active As Integer) As Boolean
            Try

                Session("FacilityGridActive") = Active
                Return True
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Public Function SetFacilityUsersActiveSessionValues(Active As Integer) As Boolean
            Try

                Session("FacilityUsersGridActive") = Active
                Return True
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function


        Public Function SetFacilitySessionValues2(FacilityID As Integer) As Boolean
            Try
                Dim jsonString As [String] = New StreamReader(Me.Request.InputStream).ReadToEnd()
                Dim Facility As Facility = JsonConvert.DeserializeObject(Of Facility)(jsonString)

                Session("Facility") = Facility
                Return True
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Public Function FacilityUserExist(FacilityID As Integer, EmailAddress As String) As Boolean
            Try
                Dim Exist As Boolean = False
                Dim FacilitesUser As FacilityUser = DataRepository.GetFacilityUser(FacilityID, EmailAddress)

                If FacilitesUser IsNot Nothing Then Exist = True
                Return Exist
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function


        Public Function SetUserSessionValues() As Boolean
            Try
                Dim jsonString As [String] = New StreamReader(Me.Request.InputStream).ReadToEnd()
                Dim User As User = JsonConvert.DeserializeObject(Of User)(jsonString)

                Session("User") = User
                Return True
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Public Function GetFacilityUsers2(FacilityID As Integer) As String

            Try
                Dim Active As Boolean = True
                Dim Users = DataRepository.GetUsersByFacility(FacilityID, Active)
                Return JsonConvert.SerializeObject(Users, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})

                ' Return Users

            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Public Function GetFacilityUsersRoles(FacilityID As Integer, UserID As Integer) As String

            Try
                Dim FacilityUsersJob As List(Of FacilityUsersJob) = DataRepository.GetFacilityUsersJobs(FacilityID, UserID)
                Return JsonConvert.SerializeObject(FacilityUsersJob, Formatting.Indented, New JsonSerializerSettings With {.PreserveReferencesHandling = PreserveReferencesHandling.Objects, .ReferenceLoopHandling = ReferenceLoopHandling.Ignore})


            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function


        Public Function UrlDatasource(ByVal dm As DataManagerRequest) As ActionResult
            Try
                Dim User As User = HomeController.GetCurrentUser()
                Dim UserInfo As New List(Of GetUserInfo_Result)
                Dim GroupsOwner As New List(Of FacilityGroup)
                Dim Facilites As New List(Of Facility)
                Dim FacilityGroups As New List(Of FacilityGroup)
                Dim FacilityGroup As FacilityGroup = Nothing

                Dim DataSource As IEnumerable = Nothing
                Dim Active As Boolean = True
                'FacilityGroup = DataRepository.GetGroupFromID(2)
                'Facilites.AddRange(DataRepository.GetFaciliteByGroups2(FacilityGroup.FacilityGroupID))

                'FacilityGroup = Session("FacilityGroup")
                'Facilites.AddRange(DataRepository.GetFaciliteByGroups2(FacilityGroup.FacilityGroupID))
                If Session("FacilityGridActive") IsNot Nothing Then
                    If Session("FacilityGridActive") = 0 Then Active = False
                End If

                If User Is Nothing Then
                    Facilites = DataRepository.GetFacilites2
                    FacilityGroups = DataRepository.GetFacilityGroups2
                End If

                If User IsNot Nothing Then
                    UserInfo = DataRepository.GetUserInfo(User.EmailAddress)

                    If Session("FacilityGroup") IsNot Nothing Then
                        FacilityGroup = Session("FacilityGroup")
                        Dim IsUserGroupOwner As Boolean = DataRepository.IsUserGroupOwner(User.EmailAddress, FacilityGroup.FacilityGroupID)


                        If IsUserGroupOwner = False Then
                            Facilites = DataRepository.GetFacilityFromUser(User.EmailAddress, Active)
                        End If

                        If IsUserGroupOwner = True Then
                            ' Dim Group As FacilityGroup = DataRepository.UserGroupOwner(User.EmailAddress, FacilityGroup.FacilityGroupID)
                            Facilites.AddRange(DataRepository.GetFaciliteByGroups2(FacilityGroup.FacilityGroupID, Active))
                        End If
                    End If


                    'Facilites = DataRepository.GetFacilityFromUser(User.EmailAddress)


                    ' GroupsOwner = DataRepository.GetUserGroupOwner(User.EmailAddress)

                    'If GroupsOwner.Count = 0 Then
                    '    Facilites = DataRepository.GetFacilityFromUser(User.EmailAddress)

                    '    'Users.Add(DataRepository.GetUser2(User.UserID))
                    'End If


                    'If GroupsOwner.Count > 0 Then
                    '    For Each Group As FacilityGroup In GroupsOwner
                    '        Facilites.AddRange(DataRepository.GetFaciliteByGroups2(Group.FacilityGroupID))
                    '        'Dim FacilityGroup As FacilityGroup = Nothing
                    '        'If Session("FacilityGroup") IsNot Nothing Then
                    '        '    FacilityGroup = Session("FacilityGroup")
                    '        '    DataSource = DataRepository.GetFacilites2.Where(Function(g) g.FacilityGroupID = FacilityGroup?.FacilityGroupID)
                    '        'End If


                    '        'If Session("FacilityGroup") Is Nothing Then
                    '        '    DataSource = DataRepository.GetFacilites2.Where(Function(g) g.FacilityGroupID = 0)
                    '        'End If
                    '    Next
                    'End If
                End If

                DataSource = Facilites


                Dim operation As DataOperations = New DataOperations()

                If dm.Search IsNot Nothing AndAlso dm.Search.Count > 0 Then
                    DataSource = operation.PerformSearching(DataSource, dm.Search)
                End If

                If dm.Sorted IsNot Nothing AndAlso dm.Sorted.Count > 0 Then
                    DataSource = operation.PerformSorting(DataSource, dm.Sorted)
                End If

                If dm.Where IsNot Nothing AndAlso dm.Where.Count > 0 Then
                    DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where(0).[Operator])
                End If

                Dim count As Integer = DataSource.Cast(Of Facility)().Count()

                If dm.Skip <> 0 Then
                    DataSource = operation.PerformSkip(DataSource, dm.Skip)
                End If

                If dm.Take <> 0 Then
                    DataSource = operation.PerformTake(DataSource, dm.Take)
                End If

                Return If(dm.RequiresCounts, Json(New With {.result = DataSource, .count = count}), Json(DataSource))
            Catch ex As Exception
                logger.Error(ex)

            End Try

        End Function

        Public Function Update(ByVal CRUDModel As ICRUDModel(Of Facility)) As ActionResult
            Try
                Dim Facility As Facility = CRUDModel.Value
                Dim FoundFacility = DataRepository.GetFacility(Facility.FacilityID)

                If FoundFacility IsNot Nothing Then
                    Dim CurrentUser As ApplicationUser = Nothing
                    If Session("CurrentUser") IsNot Nothing Then
                        CurrentUser = Session("CurrentUser")
                        ' Facility.UserEditor = CurrentUser.Email
                    End If
                    'Facility.DateCreated = Now
                    Facility.DateModified = Now

                    'DataRepository.State(Of Facility)(Facility, System.Data.Entity.EntityState.Modified)
                End If
                Dim Updated = DataRepository.AttachAndSave(Of Facility)(Facility)

                ' Dim Updated = DataRepository.SaveChanges()
            Catch ex As Exception
                logger.Error(ex)

            End Try

        End Function



        Public Function Insert(ByVal CRUDModel As ICRUDModel(Of Facility)) As ActionResult
            Try
                Dim FacilityGroup As FacilityGroup = Nothing

                If Session("FacilityGroup") IsNot Nothing Then
                    FacilityGroup = Session("FacilityGroup")
                End If


                Dim Facility As Facility = CRUDModel.Value

                If FacilityGroup IsNot Nothing Then Facility.FacilityGroupID = FacilityGroup.FacilityGroupID
                DataRepository.Attach(Of Facility)(Facility)

                Dim FoundFacility = DataRepository.GetFacility(Facility.FacilityID)
                If FoundFacility Is Nothing Then
                    Dim CurrentUser As ApplicationUser = Nothing
                    If Session("CurrentUser") IsNot Nothing Then
                        CurrentUser = Session("CurrentUser")
                        Facility.UserEditor = CurrentUser.Email
                    End If
                    Facility.DateCreated = Now
                    Facility.DateModified = Now

                    DataRepository.State(Of Facility)(Facility, System.Data.Entity.EntityState.Added)
                End If

                DataRepository.SaveChanges()

            Catch ex As Exception
                logger.Error(ex)

            End Try

        End Function


        Public Function Delete(ByVal key As Integer) As ActionResult
            Dim CurrentUser As User = HomeController.GetCurrentUser()
            'OrdersDetails.GetAllRecords().Remove(OrdersDetails.GetAllRecords().Where(Function([or]) [or].OrderID = key).FirstOrDefault())
            'Dim data = OrdersDetails.GetAllRecords()
            'Return Json(data)
        End Function


        Public Function UrlDatasource2(ByVal dm As DataManagerRequest) As ActionResult

            Try

                Dim User As User = HomeController.GetCurrentUser()
                Dim UserInfo As New List(Of GetUserInfo_Result)

                Dim Users As New List(Of User)
                Dim FacilityGroup As FacilityGroup = Nothing

                Dim GroupsOwnes As New List(Of FacilityGroup)
                Dim GroupsOwn As FacilityGroup = Nothing
                Dim DataSource As IEnumerable = Nothing
                Dim FacilityOwners As New List(Of FacilityOwner)


                'If User Is Nothing Then
                '    Facilites = DataRepository.GetFacilites2
                '    FacilityGroups = DataRepository.GetFacilityGroups2
                'End If





                If User IsNot Nothing Then
                    UserInfo = DataRepository.GetUserInfo(User.EmailAddress)

                    If Session("FacilityGroup") IsNot Nothing Then
                        FacilityGroup = Session("FacilityGroup")
                        Dim IsUserGroupOwner As Boolean = DataRepository.IsUserGroupOwner(User.EmailAddress, FacilityGroup.FacilityGroupID)
                        Dim Group As FacilityGroup = DataRepository.UserGroupOwner(User.EmailAddress, FacilityGroup.FacilityGroupID)

                        If Group IsNot Nothing Then
                            'GroupsOwn = GroupsOwnes.Where(Function(g) g.FacilityGroupID = FacilityGroup.FacilityGroupID).SingleOrDefault
                            ' FacilityOwners = DataRepository.GetGroupOwnerUsers(Group.FacilityGroupID)
                        End If
                    End If





                    ' GroupsOwnes = DataRepository.GetUserGroupOwner(User.EmailAddress)


                    'If GroupsOwnes.Count = 0 Then
                    '    Users.Add(DataRepository.GetUser2(User.UserID))
                    'End If
                    'If GroupsOwnes.Count > 0 Then
                    '    GroupsOwn = GroupsOwnes.Where(Function(g) g.FacilityGroupID = FacilityGroup.FacilityGroupID).SingleOrDefault
                    '    FacilityOwners = DataRepository.GetGroupOwnerUsers(GroupsOwn.FacilityGroupID)

                    '    'Users = DataRepository.GetGroupOwnerUsers(GroupsOwn.FacilityGroupID)

                    '    'Users.AddRange(DataRepository.GetGroupUsers2(GroupsOwn.FacilityGroupID))
                    '    'DataSource = DataRepository.GetGroupUserAdminsByFacilityGroup(GroupsOwn.FacilityGroupID).ToList
                    'End If
                End If
                DataSource = FacilityOwners








                'If Session("FacilityGroup") IsNot Nothing Then
                '    FacilityGroup = Session("FacilityGroup")
                '    DataSource = DataRepository.GetGroupUserAdminsByFacilityGroup(FacilityGroup?.FacilityGroupID).ToList
                'End If


                'If Session("FacilityGroup") Is Nothing Then
                '    DataSource = DataRepository.GetGroupUserAdminsByFacilityGroup(0)
                'End If

                'If Session("FacilityGroup") IsNot Nothing Then
                '    DataSource = DataRepository.GetGroupUserAdminsByFacilityGroup(GroupsOwn.FacilityGroupID)
                'End If


                Dim operation As DataOperations = New DataOperations()

                If dm.Search IsNot Nothing AndAlso dm.Search.Count > 0 Then
                    DataSource = operation.PerformSearching(DataSource, dm.Search)
                End If

                If dm.Sorted IsNot Nothing AndAlso dm.Sorted.Count > 0 Then
                    DataSource = operation.PerformSorting(DataSource, dm.Sorted)
                End If

                If dm.Where IsNot Nothing AndAlso dm.Where.Count > 0 Then
                    DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where(0).[Operator])
                End If

                '    Dim count As Integer = DataSource.Cast(Of GetGroupUserAdminsWithRole_Result)().Count()
                'Dim count As Integer = DataSource.Cast(Of User)().Count()
                Dim count As Integer = DataSource.Cast(Of FacilityOwner)().Count()



                If dm.Skip <> 0 Then
                    DataSource = operation.PerformSkip(DataSource, dm.Skip)
                End If

                If dm.Take <> 0 Then
                    DataSource = operation.PerformTake(DataSource, dm.Take)
                End If

                Return If(dm.RequiresCounts, Json(New With {.result = DataSource, .count = count}), Json(DataSource))
            Catch ex As Exception
                logger.Error(ex)

            End Try

        End Function

        Public Function Update2(ByVal CRUDModel As ICRUDModel(Of FacilityOwner)) As ActionResult
            Try
                'Dim User As User = HomeController.GetCurrentUser()

                'Dim FacilityOwner As FacilityOwner = CRUDModel.Value
                'If FacilityOwner IsNot Nothing Then
                '    Dim FoundFacilityOwner As FacilityOwner = DataRepository.GetFacilityOwner(FacilityOwner.FacilityOwnerID)
                '    If FoundFacilityOwner IsNot Nothing Then
                '        FoundFacilityOwner.UserID = FacilityOwner.UserID
                '        FoundFacilityOwner.DateModified = Now
                '        FoundFacilityOwner.UserEditor = User.EmailAddress
                '        DataRepository.SaveChanges()
                '    End If
                'End If

                ''Dim GroupUserAdmin As GetGroupUserAdminsWithRole_Result = CRUDModel.Value
                ''Dim FoundFacility = DataRepository.GetFacilityGroupAdmin(GroupUserAdmin.FacilityGroupAdminsID)

                ''If FoundFacility IsNot Nothing Then
                ''    FoundFacility.FacilityGroupID = GroupUserAdmin.FacilityGroupID
                ''    FoundFacility.UserID = GroupUserAdmin.UserID
                ''    DataRepository.SaveChanges()
                ''End If

            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Public Function Insert2(ByVal CRUDModel As ICRUDModel(Of FacilityOwner)) As ActionResult
            Try
                Dim FacilityGroup As FacilityGroup = Nothing


                If Session("FacilityGroup") IsNot Nothing Then
                    FacilityGroup = Session("FacilityGroup")
                End If

                Dim CurrentUser As User = HomeController.GetCurrentUser()
                Dim FacilityOwner As FacilityOwner = CRUDModel.Value
                Dim newFacilityOwner As New FacilityOwner With {.UserID = FacilityOwner.UserID, .FacilityGroupID = FacilityGroup.FacilityGroupID, .DateCreated = Now, .DateModified = Now, .UserEditor = CurrentUser.EmailAddress}

                DataRepository.Add(Of FacilityOwner)(newFacilityOwner)
                DataRepository.SaveChanges()
            Catch ex As Exception
                logger.Error(ex)

            End Try

        End Function
        Public Function Delete2(ByVal key As Integer) As ActionResult
            'orddata.Remove(orddata.Where(Function([or]) [or].OrderID = key).FirstOrDefault())
            'Return Json(New With {.result = orddata, .count = orddata.Count})
        End Function

        Public Function UrlDatasource3(ByVal dm As DataManagerRequest) As ActionResult
            Try
                Dim CurrentUser As User = HomeController.GetCurrentUser()
                Dim Facility As Facility = Nothing
                Dim User As User = Nothing
                Dim DataSource As IEnumerable = Nothing
                Dim FacilityUserJobs As New List(Of FacilityUsersJob)

                'FacilityUserJobs = DataRepository.GetFacilityUsersJobs2(Facility.FacilityID, 2)
                'DataSource = FacilityUserJobs


                If Session("Facility") IsNot Nothing Then Facility = Session("Facility")
                If Session("User") IsNot Nothing Then User = Session("User")

                If Facility IsNot Nothing And User IsNot Nothing Then
                    FacilityUserJobs = DataRepository.GetFacilityUsersJobs2(Facility.FacilityID, User.UserID)
                    DataSource = FacilityUserJobs
                End If


                Dim operation As DataOperations = New DataOperations()
                Dim count As Integer

                If dm.Search IsNot Nothing AndAlso dm.Search.Count > 0 Then
                    DataSource = operation.PerformSearching(DataSource, dm.Search)
                End If

                If dm.Sorted IsNot Nothing AndAlso dm.Sorted.Count > 0 Then
                    DataSource = operation.PerformSorting(DataSource, dm.Sorted)
                End If

                If dm.Where IsNot Nothing AndAlso dm.Where.Count > 0 Then
                    DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where(0).[Operator])
                End If
                If DataSource IsNot Nothing Then
                    count = DataSource.Cast(Of FacilityUsersJob)().Count()
                End If
                If dm.Skip <> 0 Then
                    DataSource = operation.PerformSkip(DataSource, dm.Skip)
                End If

                If dm.Take <> 0 Then
                    DataSource = operation.PerformTake(DataSource, dm.Take)
                End If

                Return If(dm.RequiresCounts, Json(New With {.result = DataSource, .count = count}), Json(DataSource))
            Catch ex As Exception
                logger.Error(ex)

            End Try

        End Function
        'Public Function UrlDatasource3(ByVal dm As DataManagerRequest) As ActionResult
        '    Try
        '        Dim CurrentUser As User = HomeController.GetCurrentUser()
        '        Dim Facility As Facility = Nothing
        '        Dim User As User = Nothing
        '        Dim DataSource As IEnumerable = Nothing
        '        Dim FacilityUserJobTitles As List(Of JobTitle)


        '        If Session("Facility") IsNot Nothing Then Facility = Session("Facility")
        '        If Session("User") IsNot Nothing Then User = Session("User")

        '        If Facility IsNot Nothing And User IsNot Nothing Then
        '            FacilityUserJobTitles = DataRepository.GetFacilityUsersJobTitles2(Facility.FacilityID, User.UserID)
        '            DataSource = FacilityUserJobTitles
        '        End If


        '        Dim operation As DataOperations = New DataOperations()

        '        If dm.Search IsNot Nothing AndAlso dm.Search.Count > 0 Then
        '            DataSource = operation.PerformSearching(DataSource, dm.Search)
        '        End If

        '        If dm.Sorted IsNot Nothing AndAlso dm.Sorted.Count > 0 Then
        '            DataSource = operation.PerformSorting(DataSource, dm.Sorted)
        '        End If

        '        If dm.Where IsNot Nothing AndAlso dm.Where.Count > 0 Then
        '            DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where(0).[Operator])
        '        End If

        '        Dim count As Integer = DataSource.Cast(Of JobTitle)().Count()

        '        If dm.Skip <> 0 Then
        '            DataSource = operation.PerformSkip(DataSource, dm.Skip)
        '        End If

        '        If dm.Take <> 0 Then
        '            DataSource = operation.PerformTake(DataSource, dm.Take)
        '        End If

        '        Return If(dm.RequiresCounts, Json(New With {.result = DataSource, .count = count}), Json(DataSource))
        '  Catch ex As Exception
        'logger.Error(ex)

        '    End Try

        'End Function

        Public Function Update3(ByVal CRUDModel As ICRUDModel(Of FacilityUsersJob)) As ActionResult
            Try
                Dim CurrentUser As User = HomeController.GetCurrentUser()
                Dim Facility As Facility = Nothing
                Dim User As User = Nothing

                Dim FacilityUsersJob As FacilityUsersJob = CRUDModel.Value

                If Session("Facility") IsNot Nothing Then Facility = Session("Facility")
                If Session("User") IsNot Nothing Then User = Session("User")

                If FacilityUsersJob IsNot Nothing Then
                    FacilityUsersJob.DateModified = Now
                    FacilityUsersJob.UserEditor = CurrentUser.EmailAddress
                    DataRepository.AttachAndSave(Of FacilityUsersJob)(FacilityUsersJob)
                End If
            Catch ex As Exception
                logger.Error(ex)

            End Try

        End Function


        Public Function Insert3(ByVal CRUDModel As ICRUDModel(Of FacilityUsersJob)) As ActionResult
            Try

                Dim CurrentUser As User = HomeController.GetCurrentUser()
                Dim Facility As Facility = Nothing
                Dim User As User = Nothing

                Dim UpdatedRecords As Integer = 0
                Dim FacilityUsersJob As FacilityUsersJob = CRUDModel.Value

                If Session("Facility") IsNot Nothing Then Facility = Session("Facility")
                If Session("User") IsNot Nothing Then User = Session("User")

                Dim Active As Boolean = True
                If Facility IsNot Nothing And User IsNot Nothing Then
                    Dim FacilityUser As FacilityUser = DataRepository.GetFacilityUser(Facility.FacilityID, User.UserID, Active)

                    If FacilityUser IsNot Nothing Then
                        Dim newFacilityUsersJob As New FacilityUsersJob
                        newFacilityUsersJob.FacilityUserID = FacilityUser.FacilityUserID
                        newFacilityUsersJob.JobTitleID = FacilityUsersJob.JobTitleID
                        newFacilityUsersJob.DateCreated = Now
                        newFacilityUsersJob.DateModified = Now
                        newFacilityUsersJob.UserEditor = User.EmailAddress
                        DataRepository.Add(Of FacilityUsersJob)(newFacilityUsersJob)
                        UpdatedRecords = DataRepository.SaveChanges()
                    End If
                End If
            Catch ex As Exception
                logger.Error(ex)

            End Try

        End Function

        Public Function Delete3(ByVal key As Integer) As ActionResult
            Try
                Dim CurrentUser As User = HomeController.GetCurrentUser()
                DataRepository.DeleteFacilityUsersJobs(key)
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        'Public Function Delete3(ByVal CRUDModel As ICRUDModel(Of FacilityUsersJob)) As ActionResult

        '    Try

        '        Dim CurrentUser As User = HomeController.GetCurrentUser()
        '        'Dim Facility As Facility = Nothing
        '        'Dim User As User = Nothing

        '        'Dim UpdatedRecords As Integer = 0
        '        'Dim FacilityUsersJob As FacilityUsersJob = CRUDModel.Value

        '        'If Session("Facility") IsNot Nothing Then Facility = Session("Facility")
        '        'If Session("User") IsNot Nothing Then User = Session("User")

        '        'If Facility IsNot Nothing And User IsNot Nothing Then
        '        '    Dim FacilityUser As FacilityUser = DataRepository.GetFacilityUser(Facility.FacilityID, User.UserID)

        '        '    If FacilityUser IsNot Nothing Then
        '        '        Dim newFacilityUsersJob As New FacilityUsersJob
        '        '        newFacilityUsersJob.FacilityUserID = FacilityUser.FacilityUserID
        '        '        newFacilityUsersJob.JobTitleID = FacilityUsersJob.JobTitleID
        '        '        newFacilityUsersJob.DateCreated = Now
        '        '        newFacilityUsersJob.DateModified = Now
        '        '        newFacilityUsersJob.UserEditor = User.EmailAddress
        '        '        DataRepository.Add(Of FacilityUsersJob)(newFacilityUsersJob)
        '        '        UpdatedRecords = DataRepository.SaveChanges()
        '        '    End If
        '        'End If
        '  Catch ex As Exception
        'logger.Error(ex)

        '    End Try

        '    'orddata.Remove(orddata.Where(Function([or]) [or].OrderID = key).FirstOrDefault())
        '    'Return Json(New With {.result = orddata, .count = orddata.Count})
        'End Function






        Public Function UrlDatasource4(ByVal dm As DataManagerRequest) As ActionResult
            Try
                Dim User As User = HomeController.GetCurrentUser()
                Dim UserInfo As New List(Of GetUserInfo_Result)
                Dim GroupsOwnes As New List(Of FacilityGroup)
                Dim Facilites As New List(Of Facility)
                Dim Users As New List(Of User)
                Dim FacilityGroup As FacilityGroup = Nothing
                Dim Facility As Facility = Nothing
                Dim Active As Boolean = True

                If Session("Facility") IsNot Nothing Then
                    Facility = Session("Facility")
                End If

                If Session("FacilityUsersGridActive") IsNot Nothing Then
                    If Session("FacilityUsersGridActive") = 0 Then Active = False
                End If
                'Users = DataRepository.GetUsersByFacility(2)

                If User IsNot Nothing Then
                    UserInfo = DataRepository.GetUserInfo(User.EmailAddress)

                    If Session("FacilityGroup") IsNot Nothing Then
                        FacilityGroup = Session("FacilityGroup")
                        Dim IsUserGroupOwner As Boolean = DataRepository.IsUserGroupOwner(User.EmailAddress, FacilityGroup.FacilityGroupID)
                        Dim Group As FacilityGroup = DataRepository.UserGroupOwner(User.EmailAddress, FacilityGroup.FacilityGroupID)

                        If IsUserGroupOwner = False Then
                            Users.Add(DataRepository.GetUser2(User.UserID))
                        End If

                        If IsUserGroupOwner = True Then
                            Users = DataRepository.GetUsersByFacility(Facility.FacilityID, Active)
                        End If
                    End If






                    'If Session("FacilityGroup") IsNot Nothing Then
                    '    Dim FacilityGroup As FacilityGroup = Session("FacilityGroup")
                    '    Dim Group As FacilityGroup = DataRepository.IsUserGroupOwner(User.EmailAddress, FacilityGroup.FacilityGroupID)
                    'End If


                    ''GroupsOwnes = DataRepository.GetUserGroupOwner(User.EmailAddress)

                    'If GroupsOwnes.Count = 0 Then
                    '    Users.Add(DataRepository.GetUser2(User.UserID))
                    'End If
                    'If GroupsOwnes.Count > 0 Then
                    '    If Facility IsNot Nothing Then
                    '        Users = DataRepository.GetUsersByFacility(Facility.FacilityID)
                    '    End If
                    'End If
                End If


                Dim DataSource As IEnumerable = Nothing
                DataSource = Users

                Dim operation As DataOperations = New DataOperations()

                If dm.Search IsNot Nothing AndAlso dm.Search.Count > 0 Then
                    DataSource = operation.PerformSearching(DataSource, dm.Search)
                End If

                If dm.Sorted IsNot Nothing AndAlso dm.Sorted.Count > 0 Then
                    DataSource = operation.PerformSorting(DataSource, dm.Sorted)
                End If

                If dm.Where IsNot Nothing AndAlso dm.Where.Count > 0 Then
                    DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where(0).[Operator])
                End If

                Dim count As Integer = DataSource.Cast(Of User)().Count()

                If dm.Skip <> 0 Then
                    DataSource = operation.PerformSkip(DataSource, dm.Skip)
                End If

                If dm.Take <> 0 Then
                    DataSource = operation.PerformTake(DataSource, dm.Take)
                End If

                Return If(dm.RequiresCounts, Json(New With {.result = DataSource, .count = count}), Json(DataSource))
            Catch ex As Exception
                logger.Error(ex)

            End Try

        End Function

        Public Function Update4(ByVal CRUDModel As ICRUDModel(Of User)) As ActionResult
            Try
                Dim User As User = CRUDModel.Value

                Dim CurrentUser As User = HomeController.GetCurrentUser()
                Dim FacilityUser As FacilityUser = Nothing
                Dim Facility As Facility = Nothing
                Dim FoundUser As User = Nothing
                Dim FoundUserbyEmail As User = Nothing
                Dim Active As Boolean = True


                If Session("Facility") IsNot Nothing Then
                    Facility = Session("Facility")
                End If


                If Session("FacilityUsersGridActive") IsNot Nothing Then
                    If Session("FacilityUsersGridActive") = 0 Then Active = False
                End If




                If User.UserID > 0 Then
                    FoundUser = DataRepository.GetUser(User.UserID)

                    If FoundUser IsNot Nothing Then
                        FacilityUser = DataRepository.GetFacilityUser(Facility.FacilityID, FoundUser.UserID, Active)
                        If FacilityUser IsNot Nothing Then
                            FacilityUser.InActive = User.FacilityInActive

                            'If User.FacilityInActive = False Then
                            '    FacilityUser.InActive = False
                            '    ' DataRepository.SaveChanges()
                            'End If
                            'If User.FacilityInActive = True Then
                            '    FacilityUser.InActive = True
                            '    'DataRepository.SaveChanges()
                            'End If
                        End If
                    End If



                    'If User.EmailAddress IsNot Nothing Then
                    '    FoundUserbyEmail = DataRepository.GetUser(User.EmailAddress)
                    'End If

                    'If FoundUserbyEmail IsNot Nothing And FoundUser IsNot Nothing Then
                    '    If FoundUserbyEmail.UserID <> User.UserID Then
                    '        Return Nothing
                    '    End If
                    'End If


                    FoundUser.InActive = 0
                    FoundUser.LastName = User.LastName
                    FoundUser.FirstName = User.FirstName
                    FoundUser.EmailAddress = User.EmailAddress
                    FoundUser.Phone1 = User.Phone1


                    DataRepository.SaveChanges()
                    'DataRepository.AttachAndSave(Of User)(User)

                End If





            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function



        Public Function Insert4(ByVal CRUDModel As ICRUDModel(Of User)) As ActionResult
            Try
                Dim User As User = CRUDModel.Value
                Dim CurrentUser As User = HomeController.GetCurrentUser()
                Dim Facility As Facility = Nothing
                Dim UpdatedRecords As Integer = 0
                Dim Active As Boolean = True

                If Session("Facility") IsNot Nothing Then
                    Facility = Session("Facility")
                End If

                If Session("FacilityUsersGridActive") IsNot Nothing Then
                    If Session("FacilityUsersGridActive") = 0 Then Active = False
                End If

                Dim FoundUser As User = Nothing
                Dim FacilityUser As FacilityUser = Nothing

                If User.UserID > 0 Then
                    FoundUser = DataRepository.GetUser(User.UserID)
                End If

                If FoundUser Is Nothing And User.EmailAddress IsNot Nothing Then
                    FoundUser = DataRepository.GetUser(User.EmailAddress)
                    If FoundUser Is Nothing Then
                        Dim newUser As New User
                        'newUser.LastName = User.LastName
                        'newUser.FirstName = User.FirstName
                        'newUser.EmailAddress = User.EmailAddress
                        'newUser.Phone1 = CurrentUser.Phone1
                        'newUser.Phone2 = CurrentUser.Phone2
                        User.UserEditor = CurrentUser.EmailAddress
                        User.DateCreated = Now
                        User.DateModified = Now
                        DataRepository.InsertAndSave(Of User)(User)
                        FoundUser = DataRepository.GetUser(User.EmailAddress)
                    End If
                End If


                If FoundUser IsNot Nothing And Facility IsNot Nothing Then
                    FacilityUser = DataRepository.GetFacilityUser(Facility.FacilityID, FoundUser.UserID, Active)

                    If FacilityUser Is Nothing Then
                        Dim newFacilityUser As New FacilityUser
                        newFacilityUser.FacilityID = Facility.FacilityID
                        newFacilityUser.UserID = FoundUser.UserID
                        newFacilityUser.DateCreated = Now
                        newFacilityUser.DateModified = Now
                        newFacilityUser.UserEditor = CurrentUser.EmailAddress
                        DataRepository.InsertAndSave(Of FacilityUser)(newFacilityUser)
                    End If
                End If

            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Public Function Delete4(ByVal key As Integer) As ActionResult
            'orddata.Remove(orddata.Where(Function([or]) [or].OrderID = key).FirstOrDefault())
            'Return Json(New With {.result = orddata, .count = orddata.Count})
        End Function


        Public Function UrlDatasource5(ByVal dm As DataManagerRequest) As ActionResult
            Try
                Dim User As User = HomeController.GetCurrentUser()
                Dim UserInfo As New List(Of GetUserInfo_Result)
                Dim FacilityGroups As New List(Of FacilityGroup)
                Dim DataSource As IEnumerable = Nothing

                FacilityGroups = DataRepository.GetFacilityGroups2

                DataSource = FacilityGroups


                Dim operation As DataOperations = New DataOperations()

                If dm.Search IsNot Nothing AndAlso dm.Search.Count > 0 Then
                    DataSource = operation.PerformSearching(DataSource, dm.Search)
                End If

                If dm.Sorted IsNot Nothing AndAlso dm.Sorted.Count > 0 Then
                    DataSource = operation.PerformSorting(DataSource, dm.Sorted)
                End If

                If dm.Where IsNot Nothing AndAlso dm.Where.Count > 0 Then
                    DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where(0).[Operator])
                End If

                Dim count As Integer = DataSource.Cast(Of FacilityGroup)().Count()

                If dm.Skip <> 0 Then
                    DataSource = operation.PerformSkip(DataSource, dm.Skip)
                End If

                If dm.Take <> 0 Then
                    DataSource = operation.PerformTake(DataSource, dm.Take)
                End If

                Return If(dm.RequiresCounts, Json(New With {.result = DataSource, .count = count}), Json(DataSource))
            Catch ex As Exception
                logger.Error(ex)

            End Try

        End Function

        Public Function Update5(ByVal CRUDModel As ICRUDModel(Of FacilityGroup)) As ActionResult
            Try
                Dim FacilityGroup As FacilityGroup = CRUDModel.Value
                Dim FoundFacilityGroup = DataRepository.GetFacilityGroup(FacilityGroup.FacilityGroupID)

                Dim CurrentUser As ApplicationUser = Nothing
                If Session("CurrentUser") IsNot Nothing Then
                    CurrentUser = Session("CurrentUser")
                    ' Facility.UserEditor = CurrentUser.Email
                End If

                If FoundFacilityGroup IsNot Nothing Then
                    FoundFacilityGroup.GroupName = FacilityGroup.GroupName
                    FoundFacilityGroup.InActive = FacilityGroup.InActive
                    FoundFacilityGroup.AllowPolicyPrint = FacilityGroup.AllowPolicyPrint
                    FoundFacilityGroup.DateModified = Now
                End If
                Dim Updated = DataRepository.SaveChanges

                ' Dim Updated = DataRepository.SaveChanges()
            Catch ex As Exception
                logger.Error(ex)

            End Try

        End Function

        Public Function Insert5(ByVal CRUDModel As ICRUDModel(Of FacilityGroup)) As ActionResult
            Try
                Dim FacilityGroup As FacilityGroup = CRUDModel.Value
                Dim CurrentUser As User = HomeController.GetCurrentUser()
                Dim Facility As Facility = Nothing
                Dim UpdatedRecords As Integer = 0
                Dim Active As Boolean = True

                Dim FoundFacilityGroup = DataRepository.GetGroupFromGroupName(FacilityGroup.GroupName)

                If FoundFacilityGroup Is Nothing Then
                    Dim newFacilityGroup As New FacilityGroup
                    newFacilityGroup.GroupName = FacilityGroup.GroupName
                    newFacilityGroup.AllowPolicyPrint = FacilityGroup.AllowPolicyPrint
                    newFacilityGroup.DateCreated = Now
                    newFacilityGroup.DateModified = Now
                    DataRepository.InsertAndSave(Of FacilityGroup)(newFacilityGroup)
                    FoundFacilityGroup = DataRepository.GetGroupFromGroupName(FacilityGroup.GroupName)
                End If


                'If Session("Facility") IsNot Nothing Then
                '    Facility = Session("Facility")
                'End If

                'If Session("FacilityUsersGridActive") IsNot Nothing Then
                '    If Session("FacilityUsersGridActive") = 0 Then Active = False
                'End If

                'Dim FoundUser As User = Nothing
                'Dim FacilityUser As FacilityUser = Nothing

                'If User.UserID > 0 Then
                '    FoundUser = DataRepository.GetUser(User.UserID)
                'End If

                'If FoundUser Is Nothing And User.EmailAddress IsNot Nothing Then
                '    FoundUser = DataRepository.GetUser(User.EmailAddress)
                '    If FoundUser Is Nothing Then
                '        Dim newUser As New User
                '        'newUser.LastName = User.LastName
                '        'newUser.FirstName = User.FirstName
                '        'newUser.EmailAddress = User.EmailAddress
                '        'newUser.Phone1 = CurrentUser.Phone1
                '        'newUser.Phone2 = CurrentUser.Phone2
                '        User.UserEditor = CurrentUser.EmailAddress
                '        User.DateCreated = Now
                '        User.DateModified = Now
                '        DataRepository.InsertAndSave(Of User)(User)
                '        FoundUser = DataRepository.GetUser(User.EmailAddress)
                '    End If
                'End If


                'If FoundUser IsNot Nothing And Facility IsNot Nothing Then
                '    FacilityUser = DataRepository.GetFacilityUser(Facility.FacilityID, FoundUser.UserID, Active)

                '    If FacilityUser Is Nothing Then
                '        Dim newFacilityUser As New FacilityUser
                '        newFacilityUser.FacilityID = Facility.FacilityID
                '        newFacilityUser.UserID = FoundUser.UserID
                '        newFacilityUser.DateCreated = Now
                '        newFacilityUser.DateModified = Now
                '        newFacilityUser.UserEditor = CurrentUser.EmailAddress
                '        DataRepository.InsertAndSave(Of FacilityUser)(newFacilityUser)
                '    End If
                'End If

            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function


        Public Function Delete5(ByVal key As Integer) As ActionResult
            Dim CurrentUser As User = HomeController.GetCurrentUser()
            'OrdersDetails.GetAllRecords().Remove(OrdersDetails.GetAllRecords().Where(Function([or]) [or].OrderID = key).FirstOrDefault())
            'Dim data = OrdersDetails.GetAllRecords()
            'Return Json(data)
        End Function
        Public Function CheckUserRole() As Integer

            Try
                Dim CurrentUser As User = HomeController.GetCurrentUser()
                Dim User As User = DataRepository.GetUser(CurrentUser.EmailAddress)
                If User.EmailAddress = "info@compliancecg.com" Then
                    Return 1
                End If

                If Session("FacilityGroup") IsNot Nothing Then
                    Dim FacilityGroup As FacilityGroup = Session("FacilityGroup")
                    Dim IsUserGroupOwner As Boolean = DataRepository.IsUserGroupOwner(User.EmailAddress, FacilityGroup.FacilityGroupID)
                    If IsUserGroupOwner = True Then Return 1
                    ' Dim Group As FacilityGroup = DataRepository.UserGroupOwner(User.EmailAddress, FacilityGroup.FacilityGroupID)
                End If

                If Session("Facility") IsNot Nothing Then
                    Dim Facility As Facility = Session("Facility")
                    Dim IsUserFacilityAdmin As Boolean = DataRepository.IsUserFacilityAdmin(User.EmailAddress, Facility.FacilityID)
                    If IsUserFacilityAdmin = True Then Return 2
                End If

                Return 0

                'Dim GroupsOwnes = DataRepository.GetUserGroupOwner(CurrentUser.EmailAddress)

                'If GroupsOwnes.Count = 0 Then
                '    Return False
                'End If
                'If GroupsOwnes.Count > 0 Then
                '    Return True
                'End If


            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function




        Function Details2() As ActionResult
            Try
                Return PartialView()
            Catch ex As Exception
                logger.Error(ex)
            End Try
        End Function


        Function Details3() As ActionResult
            Try
                Return View()
            Catch ex As Exception
                logger.Error(ex)
            End Try
        End Function

    End Class

    'Public Class Data
    '    Public Property Facility As Facility
    '    Public Property requiresCounts As Boolean
    '    Public Property skip As Integer
    '    Public Property take As Integer
    'End Class
    'Public Class ICRUDModel
    '    Public Property Added As List(Of Facility)
    '    Public Property Changed As List(Of Facility)
    '    Public Property Deleted As List(Of Facility)
    '    Public Property Value As Facility
    '    Public Property key As Integer
    '    Public Property action As String
    'End Class



    Public Class ButtonModel
        Public Property content As String
        Public Property isPrimary As Boolean
    End Class

    Public Class ICRUDModel(Of T As Class)
        Public Property Action As String
        Public Property Table As String
        Public Property KeyColumn As String
        Public Property Key As Object
        Public Property Value As T
        Public Property Added As List(Of T)
        Public Property Changed As List(Of T)
        Public Property Deleted As List(Of T)
        Public Property Params As IDictionary(Of String, Object)
    End Class


    Public Class DropDownDataDetails
        Public Sub New()
        End Sub

        Public Sub New(ByVal text As String, ByVal value As Integer, ByVal value2 As Integer)
            Me.text = text
            Me.value = value
            Me.value2 = value2
        End Sub

        Public Property text As String
        Public Property value As Integer
        Public Property value2 As Integer
    End Class
End Namespace



'Public Class CRUDModel2
'    Public Property Added As List(Of FacilityGroupAdmin)
'    Public Property Changed As List(Of FacilityGroupAdmin)
'    Public Property Deleted As List(Of FacilityGroupAdmin)
'    Public Property Value As FacilityGroupAdmin
'    Public Property key As Integer
'    Public Property action As String
'End Class


'Public Class CRUDModel3
'    Public Property Added As List(Of GetGroupUserAdminsWithRole_Result)
'    Public Property Changed As List(Of GetGroupUserAdminsWithRole_Result)
'    Public Property Deleted As List(Of GetGroupUserAdminsWithRole_Result)
'    Public Property Value As GetGroupUserAdminsWithRole_Result
'    Public Property key As Integer
'    Public Property action As String
'End Class


'Public Class ICRUDModel(Of T As Class)
'        Public Property action As String
'        Public Property table As String
'        Public Property keyColumn As String
'        Public Property key As Object
'        Public Property value As T
'        Public Property added As List(Of T)
'        Public Property changed As List(Of T)
'        Public Property deleted As List(Of T)
'        Public Property params As IDictionary(Of String, Object)
'    End Class



'Public Function Delete(<FromBody> ByVal value As ICRUDModel(Of OrdersDetails)) As ActionResult
'    OrdersDetails.GetAllRecords().Remove(OrdersDetails.GetAllRecords().Where(Function([or]) [or].OrderID = Integer.Parse(value.key.ToString())).FirstOrDefault())
'    Return Json(value)
'End Function