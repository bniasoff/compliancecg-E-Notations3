Imports System.Data.Entity
Imports System.Globalization
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports Owin
Imports CCGData
Imports CCGData.CCGData
Imports Newtonsoft.Json
Imports NLog
Imports compliancecg.Controllers

<Authorize>
Public Class AccountController
    Inherits Controller
    Private _signInManager As ApplicationSignInManager
    Private _userManager As ApplicationUserManager
    Private DataRepository As New DataRepository
    Private Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()


    Public Sub New()
    End Sub

    Public Sub New(appUserMan As ApplicationUserManager, signInMan As ApplicationSignInManager)
        UserManager = appUserMan
        SignInManager = signInMan
    End Sub

    Public Property SignInManager() As ApplicationSignInManager
        Get
            Return If(_signInManager, HttpContext.GetOwinContext().[Get](Of ApplicationSignInManager)())
        End Get
        Private Set
            _signInManager = Value
        End Set
    End Property

    Public Property UserManager() As ApplicationUserManager
        Get
            'Dim UserMan = If(_userManager, HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)())
            Return If(_userManager, HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)())
        End Get
        Private Set
            _userManager = Value
        End Set
    End Property

    '
    ' GET: /Account/Login
    <AllowAnonymous>
    Public Function Login(returnUrl As String) As ActionResult
        ViewData!ReturnUrl = returnUrl
        Return View()
    End Function



    ' POST: /Account/Login
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function Login(model As LoginViewModel, returnUrl As String) As Task(Of ActionResult)
        'model.LastName = "Niasoff"
        'model.FirstName = "Benyomin"


        If Not ModelState.IsValid Then
            Return View(model)
        End If

        ' This doesn't count login failures towards account lockout
        ' To enable password failures to trigger account lockout, change to shouldLockout := True
        'Dim result = Await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout:=False)



        Dim result = Await SignInManager.PasswordSignInAsync(model.Email, model.Password, True, shouldLockout:=False)
        Dim CurrentUser = Await Users.FindUserbyUserName(model.Email)
        ' Dim CurrentUser As Membership.UserAccou = UserManager.FindById(model.Email)
        'Dim Result2 = SignInManager.UserManager.CheckPassword(CurrentUser, model.Password)

        Select Case result
            Case SignInStatus.Success

                If Not IsNothing(CurrentUser) Then
                    CurrentUser.LastLoginDate = Now
                    Session("CurrentUser") = CurrentUser
                    Users.Update(CurrentUser)
                    UserHistory.AddUserHistory(CurrentUser, result, LoginType.LogIn)



                    'Dim Facilites As List(Of Facility) = DataRepository.GetFacilities(CurrentUser.UserName)
                    ' If Facilites IsNot Nothing Then Session("FacilityName") = Facilites.FirstOrDefault.Name


                    'Dim Facility As Facility = DataRepository.GetFacilities3(CurrentUser.UserName)
                    'If Facility IsNot Nothing Then Session("FacilityName") = Facility.Name

                    Session("MasterDocument") = Nothing
                    Session("SectionIndexes") = Nothing


                    If CurrentUser.Email <> "info@compliancecg.com" Then
                        Return RedirectToAction("Acknowledgement", "Home")
                    End If
                    If CurrentUser.Email = "info@compliancecg.com" Then
                        Session("Acknowledgement") = True

                        'Dim Home As New HomeController
                        'Home.SetAcknowledgementSessionValue(True)
                    End If



                End If

                    Return RedirectToLocal(returnUrl)
            Case SignInStatus.LockedOut
                '   If Not IsNothing(CurrentUser) Then AddUserHistory(CurrentUser, result, LoginType.LogIn)
                Return View("Lockout")
            Case SignInStatus.RequiresVerification
                '  If Not IsNothing(CurrentUser) Then AddUserHistory(CurrentUser, result, LoginType.LogIn)
                Return RedirectToAction("SendCode", New With {
                    returnUrl,
                    model.RememberMe
                })
            Case Else
                ModelState.AddModelError("", "Invalid login attempt.")
                Return View(model)
        End Select
    End Function

    '
    ' GET: /Account/VerifyCode
    <AllowAnonymous>
    Public Async Function VerifyCode(provider As String, returnUrl As String, rememberMe As Boolean) As Task(Of ActionResult)
        ' Require that the user has already logged in via username/password or external login
        If Not Await SignInManager.HasBeenVerifiedAsync() Then
            Return View("Error")
        End If
        Return View(New VerifyCodeViewModel() With {
            .Provider = provider,
            .ReturnUrl = returnUrl,
            .RememberMe = rememberMe
        })
    End Function

    '
    ' POST: /Account/VerifyCode
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function VerifyCode(model As VerifyCodeViewModel) As Task(Of ActionResult)
        If Not ModelState.IsValid Then
            Return View(model)
        End If

        ' The following code protects for brute force attacks against the two factor codes. 
        ' If a user enters incorrect codes for a specified amount of time then the user account 
        ' will be locked out for a specified amount of time. 
        ' You can configure the account lockout settings in IdentityConfig
        Dim result = Await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:=model.RememberMe, rememberBrowser:=model.RememberBrowser)
        Select Case result
            Case SignInStatus.Success
                Return RedirectToLocal(model.ReturnUrl)
            Case SignInStatus.LockedOut
                Return View("Lockout")
            Case Else
                ModelState.AddModelError("", "Invalid code.")
                Return View(model)
        End Select
    End Function

    Public Async Function AddUser(model As RegisterViewModel) As Task(Of Boolean)
        Try

            Dim user = New ApplicationUser() With {.UserName = model.Email, .Email = model.Email}
            'Dim user = New ApplicationUser() With {.UserName = model.Email, .Email = model.Email, .FirstName = model.FirstName, .LastName = model.LastName}
            Dim UserAccount = user
            Dim UserRemoved As Boolean = False
            Dim Users As New Users
            Dim RoleAdded As Boolean = False
            Dim UserRoles As New List(Of String)

            Dim ApplicationUser As New ApplicationUser
            ApplicationUser = Await Users.FindUserbyUserName(user.Email)
            '  If ApplicationUser IsNot Nothing Then UserRemoved = Await Users.RemoveUser(ApplicationUser)
            If UserRemoved Or ApplicationUser Is Nothing Then
                ' ApplicationUser = Await Users.AddUser(user.LastName, user.FirstName, model.Email, model.Email, model.Password)
                ApplicationUser = Await Users.AddUser(model.LastName, model.FirstName, model.Email, model.Email, model.Password)
            End If

            If ApplicationUser IsNot Nothing Then
                RoleAdded = Await Users.AddUserToRole(ApplicationUser, "Client")
                UserRoles = Await Users.GetRoles(ApplicationUser)
                'PasswordChanged = Await Users.ChangePassword(ApplicationUser, "Lakewood18!")
            End If

            Return True
        Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function

    <AllowAnonymous>
    Public Function DisplayEmail() As ActionResult
        Return View()
    End Function


    ' GET: /Account/Register
    <AllowAnonymous>
    Public Function Register() As ActionResult
        Return View()
    End Function

    ''
    '' POST: /Account/Register
    '<HttpPost>
    '<AllowAnonymous>
    '<ValidateAntiForgeryToken>
    'Public Async Function Register(model As RegisterViewModel) As Task(Of ActionResult)
    '    If ModelState.IsValid Then
    '        Dim user = New ApplicationUser() With {.UserName = model.Email, .Email = model.Email}
    '        'Dim user = New ApplicationUser() With {.UserName = model.Email, .Email = model.Email, .FirstName = model.FirstName, .LastName = model.LastName}
    '        Dim UserAccount = user


    '        'Dim result2 = Await AddUser(model)
    '        ' Dim UserAccount As New UserAccount
    '        'UserAccount.UserName = model.Email
    '        'UserAccount.Email = model.Email
    '        'UserAccount.Password = model.Password

    '        Dim result = Await UserManager.CreateAsync(user, model.Password)
    '        If result.Succeeded Then
    '            Await SignInManager.SignInAsync(UserAccount, isPersistent:=False, rememberBrowser:=False)

    '            ' For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
    '            ' Send an email with this link
    '            ' Dim code = Await UserManager.GenerateEmailConfirmationTokenAsync(user.Id)
    '            ' Dim callbackUrl = Url.Action("ConfirmEmail", "Account", New With { .userId = user.Id, code }, protocol := Request.Url.Scheme)
    '            ' Await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=""" & callbackUrl & """>here</a>")

    '            Return RedirectToAction("Index", "Home")
    '        End If
    '        AddErrors(result)
    '    End If

    '    ' If we got this far, something failed, redisplay form
    '    Return View(model)
    'End Function


    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function Register(model As RegisterViewModel) As Task(Of ActionResult)
        If ModelState.IsValid Then
            Dim ApplicationUser = New ApplicationUser With {.UserName = model.Email, .Email = model.Email, .FirstName = model.FirstName, .LastName = model.LastName}
            Dim User As User = DataRepository.GetUser(model.Email)
            Dim appDbContext = HttpContext.GetOwinContext().[Get](Of ApplicationDbContext)()

            'Using context = New MyEntities()

            Using transaction = appDbContext.Database.BeginTransaction()

                Try
                    'Dim DataModel = New UserMaster()
                    'DataModel.Gender = model.Gender.ToString()
                    'DataModel.Name = String.Empty

                    Dim result As New IdentityResult

                    ApplicationUser.Password2 = model.Password
                    If User IsNot Nothing Then
                        result = Await UserManager.CreateAsync(ApplicationUser, model.Password)

                        If result.Succeeded Then
                            'Await AddUser(model)

                            Dim code = Await UserManager.GenerateEmailConfirmationTokenAsync(ApplicationUser.Id)
                            Dim callbackUrl = Url.Action("ConfirmEmail", "Account", New With {.userId = ApplicationUser.Id, .code = code}, protocol:=Request.Url.Scheme)
                            Await UserManager.SendEmailAsync(ApplicationUser.Id, "Confirm your account", "Please confirm your account by clicking this link: <a href=""" & callbackUrl & """>link</a>")


                            Me.UserManager.AddToRole(ApplicationUser.Id, "Client")


                            'transaction.Commit()
                            'Await SignInManager.SignInAsync(ApplicationUser, isPersistent:=False, rememberBrowser:=False)
                            Return View("DisplayEmail")
                        End If


                        'If result.Succeeded Then
                        '    Await Me.UserManager.AddToRoleAsync(ApplicationUser.Id, "Client")


                        '    'Me.AddUser(DataModel, context)
                        '    transaction.Commit()
                        '    Await SignInManager.SignInAsync(ApplicationUser, isPersistent:=False, rememberBrowser:=False)
                        '    Return RedirectToAction("Index", "Home")

                        '    'Return View("DisplayEmail")
                        'End If
                    End If

                    If User Is Nothing Then
                        result = IdentityResult.Failed("Can't find User " + ApplicationUser.Email)
                    End If
                    AddErrors(result)
                Catch ex As Exception
                    logger.Error(ex)
                    transaction.Rollback()
                    Return Nothing
                End Try
            End Using
            'End Using
        End If

        ' If we got this far, something failed, redisplay form
        Return View(model)
    End Function




    'Public Async Function Register2(Email As String, Password As String) As Task(Of String)

    '    Dim User = New ApplicationUser With {.UserName = Email, .Email = Email}
    '    Dim appDbContext = HttpContext.GetOwinContext().[Get](Of ApplicationDbContext)()

    '    Using transaction = appDbContext.Database.BeginTransaction()

    '        Try
    '            User.Password2 = Password
    '            Dim result = Await UserManager.CreateAsync(User, Password)

    '            If result.Succeeded Then
    '                Await Me.UserManager.AddToRoleAsync(User.Id, "Client")
    '                transaction.Commit()
    '                Await SignInManager.SignInAsync(User, isPersistent:=False, rememberBrowser:=False)
    '            End If

    '            Dim LoginErrors As List(Of String) = AddErrors2(result)
    '            Return JsonConvert.SerializeObject(LoginErrors)
    '      Catch ex As Exception
    'logger.Error(ex)
    '            transaction.Rollback()
    '            ' Return Nothing
    '        End Try
    '    End Using

    Public Async Function Register2(Email As String, Password As String) As Task(Of String)

        Dim User = New ApplicationUser With {.UserName = Email, .Email = Email}
        Dim appDbContext = HttpContext.GetOwinContext().[Get](Of ApplicationDbContext)()

        Using transaction = appDbContext.Database.BeginTransaction()

            Try
                'User.Password2 = Password
                Dim result = Await UserManager.CreateAsync(User, "Test")

                If result.Succeeded Then
                    Await Me.UserManager.AddToRoleAsync(User.Id, "Client")
                    transaction.Commit()
                    Await SignInManager.SignInAsync(User, isPersistent:=False, rememberBrowser:=False)
                End If

                Dim LoginErrors As List(Of String) = AddErrors2(result)
                Return JsonConvert.SerializeObject(LoginErrors)
            Catch ex As Exception
                logger.Error(ex)
                transaction.Rollback()
                ' Return Nothing
            End Try
        End Using



        ' If we got this far, something failed, redisplay form
        'Return View(model)
    End Function


    '
    ' GET: /Account/ConfirmEmail
    <AllowAnonymous>
    Public Async Function ConfirmEmail(userId As Integer, code As String) As Task(Of ActionResult)
        If userId = 0 OrElse code Is Nothing Then
            Return View("Error")
        End If
        Dim result = Await UserManager.ConfirmEmailAsync(userId, code)
        Return View(If(result.Succeeded, "ConfirmEmail", "Error"))
    End Function

    '
    ' GET: /Account/ForgotPassword
    <AllowAnonymous>
    Public Function ForgotPassword() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Account/ForgotPassword
    '<HttpPost>
    '<AllowAnonymous>
    '<ValidateAntiForgeryToken>
    'Public Async Function ForgotPassword(model As ForgotPasswordViewModel) As Task(Of ActionResult)
    '    If ModelState.IsValid Then
    '        Dim user = Await UserManager.FindByNameAsync(model.Email)
    '        If user Is Nothing OrElse Not (Await UserManager.IsEmailConfirmedAsync(user.Id)) Then
    '            ' Don't reveal that the user does not exist or is not confirmed
    '            Return View("ForgotPasswordConfirmation")
    '        End If
    '        ' For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
    '        ' Send an email with this link
    '        ' Dim code = Await UserManager.GeneratePasswordResetTokenAsync(user.Id)
    '        ' Dim callbackUrl = Url.Action("ResetPassword", "Account", New With { .userId = user.Id, code }, protocol := Request.Url.Scheme)
    '        ' Await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=""" & callbackUrl & """>here</a>")
    '        ' Return RedirectToAction("ForgotPasswordConfirmation", "Account")
    '    End If

    '    ' If we got this far, something failed, redisplay form
    '    Return View(model)
    'End Function
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function ForgotPassword(ByVal model As ForgotPasswordViewModel) As Task(Of ActionResult)
        If ModelState.IsValid Then
            Dim user = Await UserManager.FindByNameAsync(model.Email)

            If user Is Nothing OrElse Not (Await UserManager.IsEmailConfirmedAsync(user.Id)) Then
                ModelState.AddModelError(String.Empty, "Invalid Email")
                Return View("ForgotPassword")
            End If

            Dim code = Await UserManager.GeneratePasswordResetTokenAsync(user.Id)
            Dim callbackUrl = Url.Action("ResetPassword", "Account", New With {.UserId = user.Id, .code = code}, protocol:=Request.Url.Scheme)
            Await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking here: <a href=""" & callbackUrl & """>link</a>")
            Return View("ForgotPasswordConfirmation")
        End If

        Return View(model)
    End Function

    '
    ' GET: /Account/ForgotPasswordConfirmation
    <AllowAnonymous>
    Public Function ForgotPasswordConfirmation() As ActionResult
        Return View()
    End Function

    '
    ' GET: /Account/ResetPassword
    <AllowAnonymous>
    Public Function ResetPassword(code As String) As ActionResult
        Return If(code Is Nothing, View("Error"), View())
    End Function

    '
    ' POST: /Account/ResetPassword
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function ResetPassword(model As ResetPasswordViewModel) As Task(Of ActionResult)
        If Not ModelState.IsValid Then
            Return View(model)
        End If
        Dim user = Await UserManager.FindByNameAsync(model.Email)
        If user Is Nothing Then
            ' Don't reveal that the user does not exist
            Return RedirectToAction("ResetPasswordConfirmation", "Account")
        End If
        Dim result = Await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password)
        If result.Succeeded Then
            Return RedirectToAction("ResetPasswordConfirmation", "Account")
        End If
        AddErrors(result)
        Return View()
    End Function

    '
    ' GET: /Account/ResetPasswordConfirmation
    <AllowAnonymous>
    Public Function ResetPasswordConfirmation() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Account/ExternalLogin
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Function ExternalLogin(provider As String, returnUrl As String) As ActionResult
        ' Request a redirect to the external login provider
        Return New ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", New With {
            returnUrl
        }))
    End Function

    '
    ' GET: /Account/SendCode
    <AllowAnonymous>
    Public Async Function SendCode(returnUrl As String, rememberMe As Boolean) As Task(Of ActionResult)
        Dim userId = Await SignInManager.GetVerifiedUserIdAsync()
        If userId = 0 Then
            Return View("Error")
        End If
        Dim userFactors = Await UserManager.GetValidTwoFactorProvidersAsync(userId)
        Dim factorOptions = userFactors.[Select](Function(purpose) New SelectListItem() With {
            .Text = purpose,
            .Value = purpose
        }).ToList()
        Return View(New SendCodeViewModel() With {
            .Providers = factorOptions,
            .ReturnUrl = returnUrl,
            .RememberMe = rememberMe
        })
    End Function

    '
    ' POST: /Account/SendCode
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function SendCode(model As SendCodeViewModel) As Task(Of ActionResult)
        If Not ModelState.IsValid Then
            Return View()
        End If

        ' Generate the token and send it
        If Not Await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider) Then
            Return View("Error")
        End If
        Return RedirectToAction("VerifyCode", New With {
            .Provider = model.SelectedProvider,
            model.ReturnUrl,
            model.RememberMe
        })
    End Function

    '
    ' GET: /Account/ExternalLoginCallback
    <AllowAnonymous>
    Public Async Function ExternalLoginCallback(returnUrl As String) As Task(Of ActionResult)
        Dim loginInfo = Await AuthenticationManager.GetExternalLoginInfoAsync()
        If loginInfo Is Nothing Then
            Return RedirectToAction("Login")
        End If

        ' Sign in the user with this external login provider if the user already has a login
        Dim result = Await SignInManager.ExternalSignInAsync(loginInfo, isPersistent:=False)
        Select Case result
            Case SignInStatus.Success
                Return RedirectToLocal(returnUrl)
            Case SignInStatus.LockedOut
                Return View("Lockout")
            Case SignInStatus.RequiresVerification
                Return RedirectToAction("SendCode", New With {
                    returnUrl,
                    .RememberMe = False
                })
            Case Else
                ' If the user does not have an account, then prompt the user to create an account
                ViewData!ReturnUrl = returnUrl
                ViewData!LoginProvider = loginInfo.Login.LoginProvider
                Return View("ExternalLoginConfirmation", New ExternalLoginConfirmationViewModel() With {
                    .Email = loginInfo.Email
                })
        End Select
    End Function

    ''
    '' POST: /Account/ExternalLoginConfirmation
    '<HttpPost>
    '<AllowAnonymous>
    '<ValidateAntiForgeryToken>
    'Public Async Function ExternalLoginConfirmation(model As ExternalLoginConfirmationViewModel, returnUrl As String) As Task(Of ActionResult)
    '    If User.Identity.IsAuthenticated Then
    '      Return RedirectToAction("Index", "Manage")
    '    End If

    '    If ModelState.IsValid Then
    '      ' Get the information about the user from the external login provider
    '      Dim info = Await AuthenticationManager.GetExternalLoginInfoAsync()
    '      If info Is Nothing Then
    '          Return View("ExternalLoginFailure")
    '      End If
    '      Dim userInfo = New ApplicationUser() With {
    '          .UserName = model.Email,
    '          .Email = model.Email
    '      }
    '        Dim result = Await UserManager.CreateAsync(userInfo)
    '        If result.Succeeded Then
    '        result = Await UserManager.AddLoginAsync(userInfo.Id, info.Login)
    '        If result.Succeeded Then
    '            Await SignInManager.SignInAsync(userInfo, isPersistent := False, rememberBrowser := False)
    '            Return RedirectToLocal(returnUrl)
    '        End If
    '      End If
    '      AddErrors(result)
    '    End If

    '    ViewData!ReturnUrl = returnUrl
    '    Return View(model)
    'End Function

    '
    ' POST: /Account/LogOff
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function LogOff() As ActionResult
        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie)
        Return RedirectToAction("Index", "Home")
    End Function

    '
    ' GET: /Account/ExternalLoginFailure
    <AllowAnonymous>
    Public Function ExternalLoginFailure() As ActionResult
        Return View()
    End Function

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            If _userManager IsNot Nothing Then
                _userManager.Dispose()
                _userManager = Nothing
            End If
            If _signInManager IsNot Nothing Then
                _signInManager.Dispose()
                _signInManager = Nothing
            End If
        End If

        MyBase.Dispose(disposing)
    End Sub

#Region "Helpers"
    ' Used for XSRF protection when adding external logins
    Private Const XsrfKey As String = "XsrfId"

    Private ReadOnly Property AuthenticationManager() As IAuthenticationManager
        Get
            Return HttpContext.GetOwinContext().Authentication
        End Get
    End Property

    Private Sub AddErrors(result As IdentityResult)
        For Each [error] In result.Errors
            ModelState.AddModelError("", [error])
        Next
    End Sub

    Private Function AddErrors2(result As IdentityResult) As List(Of String)
        Dim LoginErrors As New List(Of String)

        For Each [error] In result.Errors
            LoginErrors.Add([error])
        Next
        Return LoginErrors
    End Function

    Private Function RedirectToLocal(returnUrl As String) As ActionResult
        If Url.IsLocalUrl(returnUrl) Then
            Return Redirect(returnUrl)
        End If
        Return RedirectToAction("Index", "Home")
    End Function

    Friend Class ChallengeResult
        Inherits HttpUnauthorizedResult
        Public Sub New(provider As String, redirectUri As String)
            Me.New(provider, redirectUri, Nothing)
        End Sub

        Public Sub New(provider As String, redirect As String, user As String)
            LoginProvider = provider
            RedirectUri = redirect
            UserId = user
        End Sub

        Public Property LoginProvider As String
        Public Property RedirectUri As String
        Public Property UserId As String

        Public Overrides Sub ExecuteResult(context As ControllerContext)
            Dim properties = New AuthenticationProperties() With {
                .RedirectUri = RedirectUri
            }
            If UserId IsNot Nothing Then
                properties.Dictionary(XsrfKey) = UserId
            End If
            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider)
        End Sub
    End Class
#End Region
End Class


'Public Class UserValidator
'    Implements IIdentityValidator(Of ApplicationUser)

'    Public Async Function ValidateAsync(ByVal item As ApplicationUser) As Task(Of IdentityResult) Implements IIdentityValidator(Of ApplicationUser).ValidateAsync
'        If String.IsNullOrWhiteSpace(item.UserName) Then
'            'Return IdentityResult.Failed("Really?!")
'            Return Await Task.FromResult(IdentityResult.Failed("Really?!"))
'        End If

'        Return Await Task.FromResult(IdentityResult.Success)

'        'Return IdentityResult.Success
'    End Function

'    'Private Function IIdentityValidator_ValidateAsync(item As ApplicationUser) As Task(Of IdentityResult) Implements IIdentityValidator(Of ApplicationUser).ValidateAsync
'    '    Throw New NotImplementedException()
'    'End Function
'End Class
