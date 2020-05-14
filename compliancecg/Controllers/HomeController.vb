'Public Class HomeController
'    Inherits System.Web.Mvc.Controller

'    Function Index() As ActionResult
'        Return View()
'    End Function

'    Function About() As ActionResult
'        ViewData("Message") = "Your application description page."

'        Return View()
'    End Function

'    Function Contact() As ActionResult
'        ViewData("Message") = "Your contact page."

'        Return View()
'    End Function
'End Class



Imports System.Data.Entity
Imports System.IO
Imports System.Net.Http
Imports System.Runtime.CompilerServices
Imports System.Security.Claims
Imports System.ServiceModel.Channels
Imports System.Threading.Tasks
Imports CCGData
Imports CCGData.CCGData
Imports Syncfusion.DocIO
Imports Syncfusion.DocIO.DLS
Imports compliancecg.Models
Imports System.Net
Imports System.Net.Mail
Imports CCGData.Enums
Imports NLog
Imports NLog.Extensions
Imports compliancecg.JQueryImageSlider.Models
Imports System.Security.RightsManagement
Imports Microsoft.Owin.Security

Namespace Controllers
    Public Class HomeController
        Inherits Controller
        'Private CCGDataEntities As New CCGDataEntities(ConnectionStrings.CCGDataEntitiesConnectionString.ToString)
        Private DataRepository As New DataRepository
        Private Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()



        Public Function GetLoginName() As String
            Try
                Dim EmailAddress = GetCurrentEmail()
                Return EmailAddress
            Catch ex As Exception

            End Try
        End Function



        Public Function ImageSlider() As ActionResult
            Return PartialView()
            'Dim imageList As List(Of ImageModel) = New List(Of ImageModel)()
            ''imageList.Add(New ImageModel With {.ImageID = 1, .ImageName = "Image 1", .ImagePath = "/Content/Images/slide1-1024x683-1024x683.jpg"})
            ''imageList.Add(New ImageModel With {.ImageID = 2, .ImageName = "Image 2", .ImagePath = "/Content/Images/slide2-1024x683-1024x683.jpg"})
            ''imageList.Add(New ImageModel With {.ImageID = 3, .ImageName = "Image 3", .ImagePath = "/Content/Images/slide3-1024x683-1024x683.jpg"})


            'imageList.Add(New ImageModel With {.ImageID = 1, .ImageName = "Image 1", .ImagePath = "/img/the-battle.jpg"})
            'imageList.Add(New ImageModel With {.ImageID = 2, .ImageName = "Image 2", .ImagePath = "/img/hiding-the-map.jpg"})
            'imageList.Add(New ImageModel With {.ImageID = 3, .ImageName = "Image 3", .ImagePath = "/img/theres-the-buoy.jpg"})
            'imageList.Add(New ImageModel With {.ImageID = 4, .ImageName = "Image 4", .ImagePath = "/img/finding-the-key.jpg"})
            'imageList.Add(New ImageModel With {.ImageID = 5, .ImageName = "Image 5", .ImagePath = "/img/lets-get-out-of-here.jpg"})

            'Return View(imageList)
        End Function

        Public Function ImageSlider2() As ActionResult


            Return View()
        End Function
        Private Shared Function SetUpDocuments(Path As String)
            Try

                Dim WordFunctions As New WordFunctions
                'Dim Path As String = Server.MapPath("../App_Data/")
                'Dim MasterFileName As String = Server.MapPath("../App_Data/" & "GA Masters.docx")

                Dim searchPattern As String = "*.docx"
                Dim DirectoryInfo As DirectoryInfo = New DirectoryInfo(Path)
                Dim directories As DirectoryInfo() = DirectoryInfo.GetDirectories(searchPattern, SearchOption.TopDirectoryOnly)
                Dim Files As FileInfo() = DirectoryInfo.GetFiles(searchPattern, SearchOption.AllDirectories)



                For Each File As FileInfo In Files '.Where(Function(f) f.Name = "GA Masters.docx")
                    WordFunctions.SetBookMarks(File.FullName)
                Next

                For Each File As FileInfo In Files '.Where(Function(f) f.Name = "GA Masters.docx")
                    WordFunctions.SetHyperLinks(File.FullName)
                Next

            Catch ex As Exception
                logger.Error(ex)

            End Try

        End Function

        Private Shared Function AddVariablesToFiles(Path As String)
            Try

                Dim WordFunctions As New WordFunctions
                ''Dim Path As String = Server.MapPath("../App_Data/")
                ''Dim MasterFileName As String = Server.MapPath("../App_Data/" & "GA Masters.docx")

                Dim searchPattern As String = "*.docx"
                Dim DirectoryInfo As DirectoryInfo = New DirectoryInfo(Path)
                'Dim directories As DirectoryInfo() = DirectoryInfo.GetDirectories(searchPattern, SearchOption.TopDirectoryOnly)
                Dim Files As FileInfo() = DirectoryInfo.GetFiles(searchPattern, SearchOption.AllDirectories)

                For Each File As FileInfo In Files '.Where(Function(f) f.Name = "RI Masters.docx")
                    If File.Length > 500 Then
                        'Dim Document As New Syncfusion.DocIO.DLS.WordDocument
                        'Document.OpenReadOnly(File.FullName, FormatType.Docx)
                        Dim Document As New Syncfusion.DocIO.DLS.WordDocument(File.FullName)
                        Dim FindText = "[FACILITY NAME]"
                        Dim ReplaceText = "{Facility}"
                        Dim Replaced As Boolean = WordFunctions.AddDocVariables(Document, FindText, ReplaceText)
                        Document.Save(File.FullName, FormatType.Docx)
                        Document.Close()
                    End If
                Next

            Catch ex As Exception
                logger.Error(ex)

            End Try

        End Function

        Public Shared Function GetCurrentUser() As User
            Try
                Dim Session = System.Web.HttpContext.Current.Session

                Dim UserIDClaim As Claim = Nothing
                Dim UserNameClaim As Claim = Nothing

                Dim ApplicationUser As ApplicationUser = Nothing
                If ApplicationUser Is Nothing Then
                    If System.Web.HttpContext.Current.User IsNot Nothing Then
                        Dim Principal As ClaimsPrincipal = TryCast(System.Web.HttpContext.Current.User, ClaimsPrincipal)
                        If Principal IsNot Nothing Then
                            If Principal.Claims IsNot Nothing Then
                                Dim Claims = Principal.Claims
                                For Each claim As Claim In Principal.Claims
                                    If claim.Type = "UserID" Then UserIDClaim = claim
                                    If claim.Type = "UserName" Then UserNameClaim = claim
                                Next
                            End If
                        End If
                    End If
                    If UserNameClaim IsNot Nothing Then
                        Dim FacilityUser As User = DataRepository.GetFacilityUser(UserNameClaim.Value)
                        If FacilityUser IsNot Nothing Then Session("FacilityUser") = FacilityUser
                        Return FacilityUser
                    End If
                End If

            Catch ex As Exception
                logger.Error(ex)
            End Try
        End Function
        Public Function AddStaffMemberUser()
            Try
                Dim StaffMembers As List(Of StaffMember) = DataRepository.GetStaffMembers
                Dim Users As New List(Of User)

                For Each StaffMember As StaffMember In StaffMembers
                    Dim FacilityGroup As FacilityGroup = Nothing
                    Dim Facility As Facility = Nothing


                    Dim User As User = DataRepository.GetUser(StaffMember.EmailAddress)
                    If User Is Nothing Then
                        If StaffMember.FacilityGroupID IsNot Nothing Then FacilityGroup = DataRepository.GetGroupFromID(StaffMember?.FacilityGroupID)
                        If StaffMember.FacilityID IsNot Nothing Then Facility = DataRepository.GetFacility(StaffMember?.FacilityID)

                        If StaffMember.FacilityGroupID IsNot Nothing And StaffMember.FacilityID IsNot Nothing Then
                            Dim newUser As New User
                            newUser.EmailAddress = StaffMember.EmailAddress
                            newUser.LastName = FacilityGroup?.GroupName
                            newUser.FirstName = Facility?.Name
                            newUser.DateCreated = Now
                            'Users.Add(newUser)
                            DataRepository.Add(Of User)(newUser)
                            DataRepository.SaveChanges()
                        End If
                    End If
                Next


                'For Each newUser As User In Users.Take(1)
                '    If newUser.LastName IsNot Nothing And newUser.FirstName IsNot Nothing Then
                '        DataRepository.Add(Of User)(newUser)
                '    End If
                'Next





            Catch ex As Exception
                logger.Error(ex)
            End Try

        End Function

        Public Function AddStaffMemberFaclityUser()
            Try
                Dim StaffMembers As List(Of StaffMember) = DataRepository.GetStaffMembers

                For Each StaffMember As StaffMember In StaffMembers
                    Dim User As User = DataRepository.GetUser(StaffMember.EmailAddress)
                    If User IsNot Nothing Then
                        If StaffMember.FacilityID IsNot Nothing Then
                            Dim FacilityUser As FacilityUser = DataRepository.GetFacilityUser2(StaffMember.FacilityID, User.UserID)

                            If FacilityUser Is Nothing Then
                                Dim newFacilityUser As New FacilityUser
                                newFacilityUser.FacilityID = StaffMember.FacilityID
                                newFacilityUser.UserID = User.UserID
                                newFacilityUser.DateCreated = Now

                                DataRepository.Add(Of FacilityUser)(newFacilityUser)
                                DataRepository.SaveChanges()
                            End If
                        End If
                    End If
                Next

            Catch ex As Exception
                logger.Error(ex)
            End Try

        End Function

        Public Function AddStaffMemberFaclityUserRole()
            Try
                Dim StaffMembers As List(Of StaffMember) = DataRepository.GetStaffMembers
                Dim JobTitle As JobTitle = DataRepository.GetJobTitle("ADON")
                If JobTitle IsNot Nothing Then
                    For Each StaffMember As StaffMember In StaffMembers
                        Dim User As User = DataRepository.GetUser(StaffMember.EmailAddress)

                        If User IsNot Nothing Then
                            If StaffMember.FacilityID IsNot Nothing Then
                                Dim FacilityUser As FacilityUser = DataRepository.GetFacilityUser2(StaffMember.FacilityID, User.UserID)
                                If FacilityUser IsNot Nothing Then
                                    Dim FacilityUsersJob As FacilityUsersJob = DataRepository.GetFacilityUsersJob(FacilityUser.FacilityUserID, JobTitle.JobTitleID)

                                    If FacilityUsersJob Is Nothing Then
                                        Dim newFacilityUsersJob As New FacilityUsersJob
                                        newFacilityUsersJob.JobTitleID = JobTitle.JobTitleID
                                        newFacilityUsersJob.FacilityUserID = FacilityUser.FacilityUserID
                                        newFacilityUsersJob.DateCreated = Now

                                        DataRepository.Add(Of FacilityUsersJob)(newFacilityUsersJob)
                                        DataRepository.SaveChanges()
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If

            Catch ex As Exception
                logger.Error(ex)
            End Try

        End Function



        Public Shared Function GetCurrentStaffMember() As StaffMember
            Try
                Dim Session = System.Web.HttpContext.Current.Session

                Dim UserIDClaim As Claim = Nothing
                Dim UserNameClaim As Claim = Nothing

                Dim ApplicationUser As ApplicationUser = Nothing
                If ApplicationUser Is Nothing Then
                    If System.Web.HttpContext.Current.User IsNot Nothing Then
                        Dim Principal As ClaimsPrincipal = TryCast(System.Web.HttpContext.Current.User, ClaimsPrincipal)
                        If Principal IsNot Nothing Then
                            If Principal.Claims IsNot Nothing Then
                                Dim Claims = Principal.Claims
                                For Each claim As Claim In Principal.Claims
                                    If claim.Type = "UserID" Then UserIDClaim = claim
                                    If claim.Type = "UserName" Then UserNameClaim = claim
                                Next
                            End If
                        End If
                    End If
                    If UserNameClaim IsNot Nothing Then
                        Dim StaffMember As StaffMember = DataRepository.GetStaffMember(UserNameClaim.Value)
                        If StaffMember IsNot Nothing Then Session("StaffMember") = StaffMember
                        Return StaffMember
                    End If
                End If

            Catch ex As Exception
                logger.Error(ex)
            End Try
        End Function

        Public Shared Function GetCurrentEmail() As String
            Try
                Dim Session = System.Web.HttpContext.Current.Session

                Dim UserIDClaim As Claim = Nothing
                Dim UserNameClaim As Claim = Nothing

                Dim ApplicationUser As ApplicationUser = Nothing
                If ApplicationUser Is Nothing Then
                    If System.Web.HttpContext.Current.User IsNot Nothing Then
                        Dim Principal As ClaimsPrincipal = TryCast(System.Web.HttpContext.Current.User, ClaimsPrincipal)
                        If Principal IsNot Nothing Then
                            If Principal.Claims IsNot Nothing Then
                                Dim Claims = Principal.Claims
                                For Each claim As Claim In Principal.Claims
                                    If claim.Type = "UserID" Then UserIDClaim = claim
                                    If claim.Type = "UserName" Then UserNameClaim = claim
                                Next
                            End If
                        End If
                    End If
                End If

                Return UserNameClaim.Value
            Catch ex As Exception
                logger.Error(ex)
            End Try
        End Function

        Private Async Function AddFacilityAdmins2() As Task
            Try
                Dim ApplicationUser As New ApplicationUser
                Dim Users As New Users
                Dim UserRemoved As Boolean = False
                Dim RoleAdded As Boolean = False
                Dim UserRoles As New List(Of String)


                For Each User As User In DataRepository.GetLoginsToAdd
                    Dim PasswordGen = PasswordGenerator.RandomPassword(4, 3)
                    ApplicationUser = Await Users.FindUserbyUserName(User.EmailAddress)
                    '  If ApplicationUser IsNot Nothing Then UserRemoved = Await Users.RemoveUser(ApplicationUser)
                    If UserRemoved Or ApplicationUser Is Nothing Then
                        ApplicationUser = Await Users.AddUser(User.LastName, User.FirstName, User.EmailAddress, User.EmailAddress, PasswordGen)
                    End If

                    If ApplicationUser IsNot Nothing Then
                        RoleAdded = Await Users.AddUserToRole(ApplicationUser, "Client")
                        UserRoles = Await Users.GetRoles(ApplicationUser)
                        'PasswordChanged = Await Users.ChangePassword(ApplicationUser, "Lakewood18!")
                    End If
                Next

            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Private Async Function AddStaffMembers() As Task
            Try
                Dim ApplicationUser As New ApplicationUser
                Dim Users As New Users
                Dim UserRemoved As Boolean = False
                Dim RoleAdded As Boolean = False
                Dim UserRoles As New List(Of String)

                For Each StaffMember As StaffMember In DataRepository.GetStaffMembers()
                    ApplicationUser = Await Users.FindUserbyUserName(StaffMember.EmailAddress)
                    'If ApplicationUser IsNot Nothing Then UserRemoved = Await Users.RemoveUser(ApplicationUser)

                    If ApplicationUser Is Nothing Then
                        ApplicationUser = Await Users.AddUser(StaffMember.GroupName, StaffMember.FacilityName, StaffMember.EmailAddress, StaffMember.EmailAddress, "CCGStaff!")

                        If ApplicationUser IsNot Nothing Then
                            RoleAdded = Await Users.AddUserToRole(ApplicationUser, "Staff")
                            UserRoles = Await Users.GetRoles(ApplicationUser)
                            'PasswordChanged = Await Users.ChangePassword(ApplicationUser, "Lakewood18!")
                        End If
                    End If
                Next

                'For Each User As User In DataRepository.GetLoginsToAdd
                '    Dim PasswordGen = PasswordGenerator.RandomPassword(4, 3)
                '    ApplicationUser = Await Users.FindUserbyUserName(User.EmailAddress)
                '    '  If ApplicationUser IsNot Nothing Then UserRemoved = Await Users.RemoveUser(ApplicationUser)
                '    If UserRemoved Or ApplicationUser Is Nothing Then
                '        ApplicationUser = Await Users.AddUser(User.LastName, User.FirstName, User.EmailAddress, User.EmailAddress, PasswordGen)
                '    End If

                '    If ApplicationUser IsNot Nothing Then
                '        RoleAdded = Await Users.AddUserToRole(ApplicationUser, "Client")
                '        UserRoles = Await Users.GetRoles(ApplicationUser)
                '        'PasswordChanged = Await Users.ChangePassword(ApplicationUser, "Lakewood18!")
                '    End If
                'Next

            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Private Async Function AddFacilityAdmins(FacilityGroup As String) As Task
            Try
                Dim ApplicationUser As New ApplicationUser
                Dim Users As New Users
                Dim UserRemoved As Boolean = False
                Dim RoleAdded As Boolean = False
                Dim UserRoles As New List(Of String)

                'For Each User As User In DataRepository.GetUserAdmins(FacilityGroup)
                '    Dim PasswordGen = PasswordGenerator.RandomPassword(4, 3)
                '    ApplicationUser = Await Users.FindUserbyUserName(User.EmailAddress)
                '    If ApplicationUser IsNot Nothing Then UserRemoved = Await Users.RemoveUser(ApplicationUser)
                '    If UserRemoved Or ApplicationUser Is Nothing Then
                '        ApplicationUser = Await Users.AddUser(User.LastName, User.FirstName, User.EmailAddress, User.EmailAddress, PasswordGen)
                '    End If

                '    If ApplicationUser IsNot Nothing Then
                '        RoleAdded = Await Users.AddUserToRole(ApplicationUser, "Client")
                '        UserRoles = Await Users.GetRoles(ApplicationUser)
                '        'PasswordChanged = Await Users.ChangePassword(ApplicationUser, "Lakewood18!")
                '    End If
                'Next

            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Private Async Function AddFacilityUsers(FacilityName As String) As Task
            Try
                Dim ApplicationUser As New ApplicationUser
                Dim Users As New Users
                Dim UserRemoved As Boolean = False
                Dim RoleAdded As Boolean = False
                Dim UserRoles As New List(Of String)

                Dim FacilityUsers = DataRepository.GetFacilityUsers(FacilityName)

                For Each User As User In FacilityUsers
                    Dim PasswordGen = PasswordGenerator.RandomPassword(4, 3)
                    ApplicationUser = Await Users.FindUserbyUserName(User.EmailAddress)
                    ' If ApplicationUser IsNot Nothing Then UserRemoved = Await Users.RemoveUser(ApplicationUser)
                    If UserRemoved Or ApplicationUser Is Nothing Then
                        ApplicationUser = Await Users.AddUser(User.LastName, User.FirstName, User.EmailAddress, User.EmailAddress, PasswordGen)
                    End If

                    If ApplicationUser IsNot Nothing Then
                        RoleAdded = Await Users.AddUserToRole(ApplicationUser, "Client")
                        UserRoles = Await Users.GetRoles(ApplicationUser)
                        'PasswordChanged = Await Users.ChangePassword(ApplicationUser, "Lakewood18!")
                    End If
                Next

            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Public Async Function Emailer(EmailMessages As List(Of EmailMessage)) As Task
            Try
                Dim EmailServer As New EmailServer
                EmailServer.EmailMessages = EmailMessages

                Dim tasks As New List(Of Task)()
                tasks.Add(Task.Run(AddressOf EmailServer.EmailSendAsync))

                Await Task.WhenAll(tasks)
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Private Async Function EmailSendTest() As Task
            Try
                Dim EmailMessages As New List(Of EmailMessage)
                Dim EmailMessage As EmailMessage = New EmailMessage With {.FromEmail = "compliancecg.com@noip-smtp", .ToEmail = "bniasoff@gmail.com", .Subject = "Subject Test", .Message = "Message Test", .EmailType = EmailType.LoginReset}
                EmailMessages.Add(EmailMessage)
                Await Emailer(EmailMessages)
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Public Function Index2() As ActionResult
            Try
                'Dim Facilites As New List(Of Facility)
                'Dim FacilityGroups As New List(Of FacilityGroup)
                'Dim UserEmails = DataRepository.GetUsers.Select(Function(u) u.EmailAddress).ToList


                'Dim CurrentUser As User = HomeController.GetCurrentUser()
                'If CurrentUser Is Nothing Then
                '    Facilites = DataRepository.GetFacilites2
                '    FacilityGroups = DataRepository.GetFacilityGroups2
                'End If

                'If CurrentUser IsNot Nothing Then
                '    If CurrentUser.EmailAddress = "info@compliancecg.com" Then
                '        Facilites = DataRepository.GetFacilites2
                '        FacilityGroups = DataRepository.GetFacilityGroups2
                '    Else
                '        Facilites = DataRepository.GetFacilities2(CurrentUser.EmailAddress)
                '        FacilityGroups = DataRepository.GetFacilitiesGroup2(CurrentUser.EmailAddress)
                '    End If
                'End If

                ''Facilites = DataRepository.GetFacilites2
                ''FacilityGroups = DataRepository.GetFacilityGroups2

                'ViewBag.FacilityGroups = JsonConvert.SerializeObject(FacilityGroups)
                'ViewBag.Facilites = JsonConvert.SerializeObject(Facilites)


                'Dim TabHeaders As New List(Of TabHeader)
                'For Each Group As FacilityGroup In FacilityGroups
                '    TabHeaders.Add(New TabHeader With {.Text = Group.GroupName, .Id = Group.FacilityGroupID})
                'Next
                'ViewBag.TabHeaders = TabHeaders


                ''Dim buttons As New List(Of DialogDialogButton)
                ''buttons.Add(New DialogDialogButton() With {.Click = "dlgButtonClick", .ButtonModel = New ButtonModel() With {.content = "LEARN ABOUT SYNCFUSION, INC.", .isPrimary = True}})
                ''ViewBag.DefaultButtons = buttons

                'Dim FacilityUserDropDownList As New List(Of DropDownDataDetails) '= GetFacilityUserDropDownList()
                'Dim JobTitlesDropDownList As List(Of DropDownDataDetails) = GetJobTitlesDropDownList()
                'Dim UsersDropDownList As List(Of DropDownDataDetails) = GetUsersDropDownList()


                'ViewBag.FacilityUserDropDownList = FacilityUserDropDownList
                'ViewBag.UsersDropDownList = UsersDropDownList
                ''ViewBag.FacilityUserDropDownList2 = JsonConvert.SerializeObject(FacilityUserDropDownList.Take(10))
                'ViewBag.JobTitlesDropDownList = JobTitlesDropDownList
                'ViewBag.UserEmails = JsonConvert.SerializeObject(UserEmails)
                Return PartialView()
            Catch ex As Exception
                logger.Error(ex)

            End Try
            Return PartialView()
        End Function
        Public Function Acknowledgement() As ActionResult
            Return View()
        End Function

        Public Function SetAcknowledgementSessionValue(Checked As Boolean) As Boolean
            Try
                'Dim jsonString As [String] = New StreamReader(Me.Request.InputStream).ReadToEnd()
                'Dim Acknowledgement As Boolean = jsonString

                Dim Acknowledgement As Boolean = Checked



                ' Dim Principal As ClaimsPrincipal = TryCast(HttpContext.User, ClaimsPrincipal)
                'If Principal IsNot Nothing Then
                '    If Principal.Claims IsNot Nothing Then
                '        Dim Claims = Principal.Claims
                '        For Each claim As Claim In Principal.Claims
                '            If claim.Type = "Acknowledgement" Then claim.Value = Checked
                '        Next
                '    End If
                'End If






                Dim Principal As ClaimsPrincipal = TryCast(HttpContext.User, ClaimsPrincipal)
                Dim claimsIdentity = CType(Principal.Identities.FirstOrDefault, ClaimsIdentity)
                Dim identity = New ClaimsIdentity(claimsIdentity)
                identity.AddClaim(New Claim("Acknowledgement", Checked))

                Dim AuthenticationResponseGrant As New AuthenticationResponseGrant(New ClaimsPrincipal(identity), New AuthenticationProperties() With {.IsPersistent = True})


                'identity.
                'Dim AuthenticationResponseGrant As AuthenticationResponseGrant = context.Authentication.AuthenticationResponseGrant
                '(New ClaimsPrincipal(identity), New AuthenticationProperties {IsPersistent = true});


                '    context.Authentication.AuthenticationResponseGrant = New AuthenticationResponseGrant
                '(New ClaimsPrincipal(identity), New AuthenticationProperties {IsPersistent = true});



                Session("Acknowledgement") = Acknowledgement
                'Dim Claim = identity.Claims.Where(Function(c) c.Type = "Acknowledgement").SingleOrDefault

                ' Claim.Value = Checked
                Return True
            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        'Public Async Function Login(ByVal model As LoginViewModel, ByVal returnUrl As String) As Task(Of ActionResult)
        '    Dim user = UserManager.Find(model.Email, model.Password)

        '    If user IsNot Nothing Then
        '        Dim ident = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie)
        '        ident.AddClaims({New Claim("MyClaimName", "MyClaimValue"), New Claim("YetAnotherClaim", "YetAnotherValue")})
        '        AuthenticationManager.SignIn(New AuthenticationProperties() With {.IsPersistent = True}, ident)
        '        Return RedirectToLocal(returnUrl)
        '    End If

        '    ModelState.AddModelError("", "Invalid login attempt.")
        '    Return View(model)
        'End Function



        Public Async Function Index() As Task(Of ActionResult)
            Try

                'Dim WordFunctions As New WordFunctions
                'Dim Document As New WordDocument
                'Dim File As New FileInfo("c:\ccg\NJ Masters.docx")
                'Document.OpenReadOnly(File.FullName, FormatType.Docx)

                'WordFunctions.CopyDocSection2(Document, "1", "Preferred Care at Absecon")

                'https://www.bestjquery.com/lab/navigation-menu/
                'https://bestjquery.com/tutorial/menu/demo62/
                'https://bestjquery.com/tutorial/menu/demo55/

                'Dim document As New WordDocument(Server.MapPath("../App_Data/Policies/GA Masters.docx"), FormatType.Docx)
                'Dim textSelections As TextSelection() = document.FindAll("rules", False, True)
                'For Each textSelection As TextSelection In textSelections
                '    Dim textRange As WTextRange = textSelection.GetAsOneRange()
                '    textRange.CharacterFormat.HighlightColor = Color.YellowGreen
                'Next




                'Dim DexExpDocumentPermission As New DexExpDocumentPermission
                'DexExpDocumentPermission.OpenFile(Server.MapPath("../App_Data/Policies/GA Masters.docx"))

                'Await AddStaffMembers()
                'AddStaffMemberUser()
                'AddStaffMemberFaclityUser()
                'AddStaffMemberFaclityUserRole()

                'Await AddFacilityAdmins2()
                'Await AddFacilityAdmins("Preferred Care at Absecon")
                'Await AddFacilityUsers("Revolution Health Care")




                'SetUpDocuments(Server.MapPath("../App_Data/Updates"))
                'AddVariablesToFiles(Server.MapPath("../App_Data/Updates"))








                ' Dim Users = DataRepository.GetUsers.Where(Function(c) c.EmailAddress IsNot Nothing And c.ValidEmail Is Nothing).ToList

                'For Each User As User In Users
                '    Dim Valid = EmailServer.EmailAddressChecker(User.EmailAddress)
                '    If Valid = True Then
                '        User.ValidEmail = True
                '    End If
                'Next




                'Dim ClientContacts = DataRepository.GetClientContacts.Where(Function(c) c.Email IsNot Nothing And c.ValidEmail Is Nothing).ToList

                'For Each ClientContact As ClientContact In ClientContacts
                '    Dim Valid = EmailServer.EmailAddressChecker(ClientContact.Email)
                '    If Valid = True Then
                '        ClientContact.ValidEmail = True
                '    End If
                'Next
                '  Dim Recordupdated = DataRepository.SaveChanges()


                'hmcgill@carmimanor.com
                'peqp61PEQ


                ' Await EmailSendTest()




                'If System.Web.HttpContext.Current.Session("FacilityUser") Is Nothing Then
                '    Dim CurrentUser As User = GetCurrentUser()
                'End If


                'Dim Users As New Users
                'Dim ApplicationUser As New ApplicationUser
                ''Dim UserRemoved As Boolean = False
                ''Dim RoleAdded As Boolean = False
                ''Dim UserRoles As New List(Of String)
                'Dim PasswordChanged As Boolean = False


                'Dim UserName As String = "bboyer@bedrockcare.com"
                ''Dim LastName As String = "Niasoff"
                ''Dim FirstName As String = "Benyomin"
                '''Dim PasswordGen = PasswordGenerator.RandomPassword(4, 3)
                'Dim PasswordGen = "Karter213"
                'ApplicationUser = Await Users.FindUserbyUserName(UserName)
                ''If ApplicationUser IsNot Nothing Then UserRemoved = Await Users.RemoveUser(ApplicationUser)
                ''If UserRemoved Or ApplicationUser Is Nothing Then
                ''    ApplicationUser = Await Users.AddUser(LastName, FirstName, UserName, UserName, PasswordGen)
                ''End If

                'If ApplicationUser IsNot Nothing Then
                '    'RoleAdded = Await Users.AddUserToRole(ApplicationUser, "Admin")
                '    'UserRoles = Await Users.GetRoles(ApplicationUser)
                '    PasswordChanged = Await Users.ChangePassword(ApplicationUser, PasswordGen)
                'End If









                '  Dim Document As New Syncfusion.DocIO.DLS.WordDocument
                'Document.OpenReadOnly(Server.MapPath("/App_Data/Policies/GA Masters.docx"), FormatType.Docx)
                'Dim SectionIndexes As List(Of SectionIndex) = WordFunctions.GetSectionTitle(Document)
                'Session("MasterDocument") = Document
                'Session("SectionIndexes") = SectionIndexes


                'WordFunctions.GetMasterDocuments()
                ' 



                'Dim ReadFile As FileInfo = Nothing
                ' Dim MasterFileName As String = Server.MapPath("../App_Data/" & "GA Masters.docx")
                ''Dim MasterFileName As String = Server.MapPath("../App_Data/" & "Sample.docx")



                'ReadFile = New FileInfo(MasterFileName)

                'Dim WordFunctions As New WordFunctions
                'Dim MasterFile = WordFunctions.GetMasterDocument(Server.MapPath("/App_Data/Policies/"), "GA")
                'If MasterFile IsNot Nothing Then
                '    Dim Document As New Syncfusion.DocIO.DLS.WordDocument
                '    Document.OpenReadOnly(MasterFile.FullName, FormatType.Docx)
                '    Dim SectionIndexes As List(Of SectionIndex) = WordFunctions.GetSectionTitle(Document)

                '    Session("MasterDocument") = Document
                '    Session("SectionIndexes") = SectionIndexes
                'End If


                '    '  Dim DocVariables As DocVariables = Document.Variables

                '    '
                '    'Document.Save(MasterFileName2, FormatType.Docx)



                '    ''Dim Selections As TextSelection() = Document.FindAll(FindText, False, False)
                '    ''Dim TextSelections As List(Of TextSelection) = Selections.ToList
                '    ''For Each Selection As TextSelection In TextSelections
                '    ''    Dim SelectionRange As WTextRange() = Selection.GetRanges
                '    ''    If SelectionRange.FirstOrDefault.Owner IsNot Nothing Then
                '    ''        If SelectionRange.FirstOrDefault.Owner.EntityType = EntityType.Paragraph Then
                '    ''            'Dim SelectionParagraph As WParagraph = SelectionRange.FirstOrDefault.Owner.Clone
                '    ''            'SelectionParagraph.AppendField("FacilityName", FieldType.FieldDocVariable)
                '    ''        End If
                '    ''    End If
                '    ''Next






















                '    '    'Iterates through form fields

                '    '    'For Each Paragraph As WParagraph In section.Paragraphs
                '    '    '    Paragraph.

                '    '    'Next
                '    '    For Each textBody As WTextBody In section.ChildEntities

                '    '        ' FieldType.FieldDocVariable

                '    '        For Each formField As WFormField In textBody.FormFields

                '    '            Select Case formField.FormFieldType

                '    '                Case FormFieldType.TextInput
                '    '            End Select

                '    '        Next
                '    '    Next
                '    'Next

                '    'Iterates through section child elements



                'End If


                'Dim Doc = WordFunctions.OpenDocument(MasterFileName)
                'Dim SectionChapters = WordFunctions.GetSectionChapters(Doc)
                'Session("SectionChapters") = SectionChapters
                'Session("MasterDocument") = Doc
                'WordDocuments.Documents.Add(Doc)
                'Session("WordDocuments") = WordDocuments

                'For Each section As WSection In Document.Sections
                '    'Accesses the Body of section where all the contents in document are apart
                '    Dim sectionBody As WTextBody = section.Body
                '    IterateTextBody(sectionBody)
                '    Dim headersFooters As WHeadersFooters = section.HeadersFooters
                '    'Consider that OddHeader and OddFooter are applied to this document
                '    'Iterates through the text body of OddHeader and OddFooter
                '    IterateTextBody(headersFooters.OddHeader)
                '    IterateTextBody(headersFooters.OddFooter)
                'Next
                'Document.Save("C:\CCG\temp\Result.docx")
                'Document.Close()


                ''Accesses the Body of section where all the contents in document are apart
                'Dim sectionBody As WTextBody = Section.Body
                'IterateTextBody(sectionBody)
                'Dim headersFooters As WHeadersFooters = Section.HeadersFooters
                ''Consider that OddHeader and OddFooter are applied to this document
                ''Iterates through the text body of OddHeader and OddFooter
                'IterateTextBody(headersFooters.OddHeader)
                'IterateTextBody(headersFooters.OddFooter)



                'Dim  destinationDocument As New WordDocument()
                'destinationDocument.Sections.Add(Document.Sections(13).Clone())
                'destinationDocument.Save("C:\CCG\temp\Sections" + ".docx")
                'destinationDocument.Close()




                'Dim destinationDocument As New WordDocument()
                'destinationDocument.Sections.Add(Document.Sections(SectionNumber).Clone())
                ''destinationDocument.Save("C:\CCG\temp\Sections" + ".docx")
                ''destinationDocument.Close()


                'Dim stream As New MemoryStream()
                'destinationDocument.Save(stream, FormatType.Docx)




                'Doc = Session("MasterDocument")
                'Session("MasterDocument") = Doc
                'Doc = Session("MasterDocument")

                'Dim templateContent As Byte() = System.IO.File.ReadAllBytes(MasterFileName)
                'Dim stream As MemoryStream = New MemoryStream()
                'stream.Write(templateContent, 0, templateContent.Length)
                'Dim wordDoc As WordprocessingDocument = WordprocessingDocument.Open(stream, True)
                'Dim contentOfWordFile As Byte() = stream.ToArray()

                ' Dim Doc2 As Word.Document = Range.InsertXML(contentOfWordFile)

                'Dim Doc2 As Word.Document = wordDoc.MainDocumentPart.Document



                'Dim server = New RichEditDocumentServer()
                'server.LoadDocument(Doc.FullName)
                'Dim retrievedRange As DocumentRange = server.Document.Sections(2).Range
                'Dim Subdocument As SubDocument = retrievedRange.BeginUpdateDocument()
                'Dim bytes = Subdocument.GetOpenXmlBytes(retrievedRange)
                'retrievedRange.EndUpdateDocument(Subdocument)

                '   Dim retrievedRange2 As DocumentRange = server.Document.Range

                '    Dim firstSection As API.Native.Section = server.Document.Sections(2)
                '    ' Create an empty header.
                '    Dim newFooter As SubDocument = firstSection.BeginUpdateFooter()

                '    'firstSection.EndUpdateFooter(newFooter)
                '    ' Check whether the document already has a header (the same header for all pages).
                '    If firstSection.HasFooter(HeaderFooterType.Primary) Then
                '        Dim footerDocument As SubDocument = firstSection.BeginUpdateFooter()
                '        server.Document.ChangeActiveDocument(footerDocument)
                '        server.Document.CaretPosition = footerDocument.CreatePosition(0)
                '        firstSection.EndUpdateHeader(footerDocument)
                '    End If





                'Dim FileName As String = Server.MapPath("../App_Data/" & "GA Masters.docx")


                'Dim Doc = WordFunctions.OpenDocument(FileName)
                ''  WordFunctions.GetSections(Doc)
                ''WordFunctions.GetFields(Doc)

                'WordFunctions.CopyDocSection(Doc)




                ' Dim UpdatedIDs As Boolean = DataRepository.UpdateFacilityUsersID()
                'Dim UpdatedID As Boolean = DataRepository.UpdateFacilityUserID("")










                ''  Await store.RemoveFromRoleAsync(IdentityUser, "Admin")

                'Dim SignInManagerTest As New SignInManagerTest
                'Await SignInManagerTest.SignInAsyncCookiePersistenceTest(True, True)

                ' Dim LoginsTest As New LoginsTest
                '  Await LoginsTest.LinkUnlinkDeletesTest()

                'Dim UserStoreTest As New UserStoreTest
                'Await UserStoreTest.FindByUserName()
                ''Dim ApplicationUserTest As New ApplicationUserTest
                'Await ApplicationUserTest.ApplicationUserCreateTest()
                '' Dim RolesTest As New RolesTest
                ' Await RolesTest.AddUserToRoleTest()


                'Dim HttpClient As New HttpClient()

                'Dim Request As New HttpRequestMessage

                'Dim Url = "https://www.google.com/"
                'Request.RequestUri = New Uri(Url)
                'Request.Method = HttpMethod.Post
                'Request.Content = New StringContent("something")

                'Dim Response As HttpResponseMessage = HttpClient.SendAsync(Request).Result




                ''Dim HttpRequestMessage As HttpRequestMessage = HttpContext.Items("MS_HttpRequestMessage") '

                ''Dim HttpRequestMessage As New System.Net.Http.HttpRequestMessage
                'Dim IP = IPExt.GetIP(Request)


                'Dim list = New List(Of BlogTable)()

                'Using context = New CCGDataEntities()
                '    list = context.BlogTables.ToList()
                'End Using
                Return View()
            'Return PartialView()
            Catch ex As Exception
            logger.Error(ex)
            End Try

        End Function




        'Public Function Index() As ActionResult
        '    '  AddUserAndRoles()
        '    'Dim HttpClient As New HttpClient()

        '    'Dim Request As New HttpRequestMessage

        '    'Dim Url = "https://www.google.com/"
        '    'Request.RequestUri = New Uri(Url)
        '    'Request.Method = HttpMethod.Post
        '    'Request.Content = New StringContent("something")

        '    'Dim Response As HttpResponseMessage = HttpClient.SendAsync(Request).Result




        '    ''Dim HttpRequestMessage As HttpRequestMessage = HttpContext.Items("MS_HttpRequestMessage") '

        '    ''Dim HttpRequestMessage As New System.Net.Http.HttpRequestMessage
        '    'Dim IP = IPExt.GetIP(Request)


        '    'Dim list = New List(Of BlogTable)()

        '    'Using context = New CCGDataEntities()
        '    '    list = context.BlogTables.ToList()
        '    'End Using

        '    Return View()
        'End Function

        Public Function About() As Object
            Return View()
        End Function
        Public Function About2() As Object
            Return View()
        End Function
        Public Function Create() As ActionResult
            AddUserAndRoles()
            ' Dim ApplicationDbContext As New ApplicationDbContext
            '  SeedUserAndRole(ApplicationDbContext)
            Return View()
        End Function

        Public Function Create2() As ActionResult
            Return View()
        End Function

        Public Function Create3() As ActionResult
            Return View()
        End Function
        '<ValidateInput(False)>
        '<HttpPost>
        'Public Function Create(ByVal bt As BlogTable) As ActionResult

        '    Using context = New CCGDataEntities()
        '        context.BlogTables.Add(bt)
        '        context.SaveChanges()
        '    End Using

        '    Return RedirectToAction("Index")
        'End Function

        'Public Function Edit(ByVal id As Integer) As ActionResult
        '    Dim model = New BlogTable()

        '    'Using context = New CCGDataEntities()
        '    '    model = context.BlogTables.Where(Function(a) a.Id = id).FirstOrDefault()
        '    'End Using

        '    Return View(model)
        'End Function

        '<ValidateInput(False)>
        '<HttpPost>
        'Public Function Edit(ByVal bt As BlogTable) As ActionResult
        '    Using context = New CCGDataEntities()
        '        context.Entry(bt).State = EntityState.Modified
        '        context.SaveChanges()
        '    End Using

        '    Return RedirectToAction("Index")
        'End Function

        'Private Sub SeedUserAndRole(context As ApplicationDbContext)
        '    Try
        '        'Database.SetInitializer(New DropCreateDatabaseAlways(Of IdentityDbContext)())
        '        Dim db = New IdentityDbContext()
        '        '   db.Database.Initialize(True)

        '        Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '        Dim roleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(db))
        '        Dim role = New IdentityRole("addUserTest")
        '        '  UnitTestHelper.IsSuccess(Await roleManager.CreateAsync(role))
        '        Dim users As IdentityUser() = {New IdentityUser("1"), New IdentityUser("2"), New IdentityUser("3"), New IdentityUser("4")}

        '        For Each u As IdentityUser In users
        '            u.UserName = "Ben" + u.Id
        '            manager.CreateAsync(u)
        '            'manager.AddToRoleAsync(u.Id, role.Name)
        '            '' u.Roles.Count(Function(ur) ur.RoleId = role.Id)
        '            'manager.IsInRoleAsync(u.Id, role.Name)
        '            db.Users.Add(u)
        '            'UnitTestHelper.IsSuccess(Await manager.CreateAsync(u))
        '            'UnitTestHelper.IsSuccess(Await manager.AddToRoleAsync(u.Id, role.Name))
        '            'Assert.Equal(1, u.Roles.Count(Function(ur) ur.RoleId = role.Id))
        '            'Assert.[True](Await manager.IsInRoleAsync(u.Id, role.Name))
        '        Next
        '        '   db.Roles.Add(role)

        '        Dim Updated = db.SaveChanges()
        '        WriteLine(Updated)

        '        'Dim mgr = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(context))
        '        'Dim user = New IdentityUser("linkunlinktest")


        '        'Dim roleStore = New RoleStore(Of IdentityRole)(context)
        '        'Dim roleManager = New RoleManager(Of IdentityRole)(roleStore)
        '        'Dim userStore = New UserStore(Of IdentityUser)(context)
        '        ''  Dim userManager = New UserManager(Of ApplicationUser)(userStore)
        '        ''Dim user = New ApplicationUser() With {.UserName = "sallen"}

        '        'Dim user = New UserAccount() With {.UserName = "sallen"}
        '        'UserManager.Create(user, "password")
        '        'roleManager.Create(New IdentityRole() With {.Name = "admin"})
        '        'UserManager.AddToRole(user.Id, "admin")
        '  Catch ex As Exception
        'logger.Error(ex)

        '    End Try
        'End Sub
        Private Sub SurroundingSub()

        End Sub
        Private Function AddUserAndRoles() As Boolean
            Try
                'Dim success As Boolean = False
                'Dim UserName As String = "bniasoff@hotmail.com"

                'Dim idManager = New IdentityManager()
                'Dim Db = New ApplicationDbContext()
                'Dim users = Db.Users.ToList
                ''  Dim roles = Db.Roles.ToList
                'Dim user = Db.Users.First(Function(u) u.UserName = UserName)
                'idManager.ClearUserRoles(user.Id)



                'success = True

                '
                'Dim Db = New ApplicationDbContext()
                'Dim manager = New UserManager(Of ApplicationUser)(New EntityFramework.UserStore(Of ApplicationUser)(Db))

                ''Dim idManager = New UserManager(OfManager()
                '

                'Dim idManager = New RoleManager(Of EntityFramework.IdentityRole)(New EntityFramework.RoleStore(Of EntityFramework.IdentityRole)(Db))
                'manager.CreateRol("Admin", "Global Access")


                '  Dim User = Db.Users.First(Function(u) u.UserName = UserName)
                'Dim role = Db.Roles.First(Function(u) u.Name = "Admin")
                'manager.AddToRole(User.Id, role.Name)
                ''idManager.ClearUserRoles(user.Id)


                'Dim success As Boolean = False
                'Dim idManager = New UserManager(Of UserRole)
                ''success = idManager.CreateRole("Admin", "Global Access")
                'If Not success = True Then Return success
                'success = idManager.CreateRole("CanEdit", "Edit existing records")
                'If Not success = True Then Return success
                'success = idManager.CreateRole("User", "Restricted to business domain activity")
                'If Not success Then Return success
                'Dim newUser = New ApplicationUser() With {
                '    .UserName = "jatten",
                '    .FirstName = "John",
                '    .LastName = "Atten",
                '    .Email = "jatten@typecastexception.com"
                '}
                'success = idManager.CreateUser(newUser, "Password1")
                'If Not success Then Return success
                'success = idManager.AddUserToRole(newUser.Id, "Admin")
                'If Not success Then Return success
                'success = idManager.AddUserToRole(newUser.Id, "CanEdit")
                'If Not success Then Return success
                'success = idManager.AddUserToRole(newUser.Id, "User")
                'If Not success Then Return success
                ' Return success

            Catch ex As Exception
                logger.Error(ex)

            End Try
        End Function

        Private Shared Sub IterateTextBody(textBody As WTextBody)

            'Iterates through the each of the child items of WTextBody

            For i As Integer = 0 To textBody.ChildEntities.Count - 1

                'IEntity is the basic unit in DocIO DOM. 

                'Accesses the body items (should be either paragraph or table) as IEntity

                Dim bodyItemEntity As IEntity = textBody.ChildEntities(i)

                'A Text body has 2 types of elements - Paragraph and Table

                'decide the element type using EntityType

                Select Case bodyItemEntity.EntityType

                    Case EntityType.Paragraph

                        Dim paragraph As WParagraph = TryCast(bodyItemEntity, WParagraph)

                        'Checks for a particular style name and removes the paragraph from DOM

                        If paragraph.StyleName = "MyStyle" Then

                            Dim index As Integer = textBody.ChildEntities.IndexOf(paragraph)

                            textBody.ChildEntities.RemoveAt(index)

                        End If

                        Exit Select

                    Case EntityType.Table

                        'Table is a collection of rows and cells

                        'Iterates through table's DOM

                        IterateTable(TryCast(bodyItemEntity, WTable))

                        Exit Select

                End Select

            Next

        End Sub

        Private Shared Sub IterateTable(table As WTable)

            'Iterates the row collection in a table

            For Each row As WTableRow In table.Rows

                'Iterates the cell collection in a table row

                For Each cell As WTableCell In row.Cells

                    'Table cell is derived from (also a) TextBody

                    'Reusing the code meant for iterating TextBody

                    IterateTextBody(cell)

                Next

            Next

        End Sub


        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Contact(ByVal model As EmailModel) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Dim body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>"
                Dim message = New MailMessage()
                message.[To].Add(New MailAddress("recipient@gmail.com"))
                message.From = New MailAddress("sender@outlook.com")
                message.Subject = "Your email subject"
                message.Body = String.Format(body, model.FromName, model.FromEmail, model.Message)
                message.IsBodyHtml = True

                Using smtp = New SmtpClient()
                    Dim credential = New NetworkCredential With {.UserName = "user@outlook.com", .Password = "password"}
                    smtp.Credentials = credential
                    smtp.Host = "smtp-mail.outlook.com"
                    smtp.Port = 587
                    smtp.EnableSsl = True
                    Await smtp.SendMailAsync(message)
                    Return RedirectToAction("Sent")
                End Using
            End If

            Return View(model)
        End Function
        Public Function Sent() As ActionResult
            Return View()
        End Function
    End Class




    'Partial Public Class UserHistory
    '    Public Property UserHistoryID As Integer
    '    Public Property EmployeeID As Nullable(Of Integer)
    '    Public Property UserName As String
    '    Public Property Login As Nullable(Of Date)
    '    Public Property Logoff As Nullable(Of Date)
    '    Public Property Success As Nullable(Of Boolean)
    '    Public Property SignInStatus As Nullable(Of Integer)
    '    Public Property URL As String
    '    Public Property Activity As String
    '    Public Property DateofEntry As Nullable(Of Date)
    '    Public Property UserAgent As String
    '    Public Property RemoteAddress As String

    'End Class

    Public Class IPFilterHandler
        Inherits DelegatingHandler

        'Protected Overrides Async Function SendAsync(ByVal request As HttpRequestMessage, ByVal cancellationToken As CancellationToken) As Task
        '    If request.AllowIP() Then
        '        Return Await MyBase.SendAsync(request, cancellationToken)
        '    End If

        '    Return request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Not authorized to view/access this resource")
        'End Function
    End Class
End Namespace


Friend Module IPExt
    <Extension()>
    Public Function GetIP(ByVal requestMessage As HttpRequestMessage) As String
        If requestMessage.Properties.ContainsKey("MS_OwinContext") Then
            Return If(System.Web.HttpContext.Current IsNot Nothing, System.Web.HttpContext.Current.Request.GetOwinContext().Request.RemoteIpAddress, Nothing)
        End If

        If requestMessage.Properties.ContainsKey("MS_HttpContext") Then
            Return If(System.Web.HttpContext.Current IsNot Nothing, System.Web.HttpContext.Current.Request.UserHostAddress, Nothing)
        End If

        If requestMessage.Properties.ContainsKey(RemoteEndpointMessageProperty.Name) Then
            Dim [property] As RemoteEndpointMessageProperty = CType(requestMessage.Properties(RemoteEndpointMessageProperty.Name), RemoteEndpointMessageProperty)
            Return If([property] IsNot Nothing, [property].Address, Nothing)
        End If

        Return Nothing
    End Function


    <Extension()>
    Public Function AllowIP(ByVal request As HttpRequestMessage) As Boolean
        Dim whiteListedIPs = ConfigurationManager.AppSettings("WhiteListedIPAddresses")

        If Not String.IsNullOrEmpty(whiteListedIPs) Then
            Dim whiteListIPList = whiteListedIPs.Split(","c).ToList()
            Dim ipAddressString = request.GetIP()
            ' Dim ipAddress = ipAddress.Parse(ipAddressString)
            Dim isInwhiteListIPList = whiteListIPList.Where(Function(a) a.Trim().Equals(ipAddressString, StringComparison.InvariantCultureIgnoreCase)).Any()
            Return isInwhiteListIPList
        End If

        Return True
    End Function
End Module

