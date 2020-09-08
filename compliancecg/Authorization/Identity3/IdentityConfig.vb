Imports System.Threading.Tasks
Imports System.Security.Claims
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports NLog
Imports Crypto = compliancecg.Microsoft.AspNet.Identity.Crypto
Imports System.Net.Mail
Imports CCGData.CCGData
Imports CCGData.Enums
Imports System.Data.Entity.SqlServer.Utilities
Imports System.Globalization
'Imports ~compliancecg.Microsoft.AspNet.Identity~
Imports compliancecg.My.Resources
Imports Microsoft.Ajax.Utilities

Public Class EmailService
    Implements IIdentityMessageService

    Private Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Public Async Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
        Try
            Dim EmailMessages As New List(Of EmailMessage)
            Dim EmailMessage As EmailMessage = New EmailMessage With {.IsBodyHtml = True, .FromEmail = "compliancecg.com@noip-smtp", .ToEmail = message.Destination, .Subject = message.Subject, .Message = message.Body, .EmailType = EmailType.LoginReset}
            EmailMessages.Add(EmailMessage)
            Await Emailer(EmailMessages)

        Catch ex As Exception
            'logger.Error(ex)

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

End Class




Public Class SmsService
    Implements IIdentityMessageService


    Public Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
        Try
            ' Plug in your SMS service here to send a text message.
            Return Task.FromResult(0)
        Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function
End Class
Public Class ApplicationUserManager
    Inherits UserManager(Of ApplicationUser, Integer)
    Private _disposed As Boolean
    Public Sub New(ByVal store As IUserStore(Of ApplicationUser, Integer))
        MyBase.New(store)
    End Sub
    Public Shared Function Create(ByVal options As IdentityFactoryOptions(Of ApplicationUserManager), ByVal context As IOwinContext) As ApplicationUserManager
        Try
            Dim manager = New ApplicationUserManager(New UserStore(context.[Get](Of ApplicationDbContext)()))

            manager.UserValidator = New UserValidator(Of ApplicationUser, Integer)(manager) With {.AllowOnlyAlphanumericUserNames = False, .RequireUniqueEmail = True}
            manager.PasswordValidator = New PasswordValidator With {.RequiredLength = 6, .RequireDigit = True, .RequireLowercase = False, .RequireUppercase = True}

            manager.UserLockoutEnabledByDefault = True
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5)
            manager.MaxFailedAccessAttemptsBeforeLockout = 5

            'manager.RegisterTwoFactorProvider("Phone Code", New PhoneNumberTokenProvider(Of ApplicationUser, Integer) With {.MessageFormat = "Your security code is {0}"})
            ' Dim taskResult = manager.RegisterTwoFactorProviderAsync("Email Code", New EmailTokenProvider(Of ApplicationUser, Integer) With {.Subject = "Security Code", .BodyFormat = "Your security code is {0}"})

            manager.EmailService = New EmailService()
            manager.SmsService = New SmsService()

            Dim dataProtectionProvider = options.DataProtectionProvider

            If dataProtectionProvider IsNot Nothing Then
                manager.UserTokenProvider = New DataProtectorTokenProvider(Of ApplicationUser, Integer)(dataProtectionProvider.Create("ASP.NET Identity"))
            End If

            'var manager = New UserManager < IdentityUser > (New NoopUserStore());
            ' Dim VerifyUserToken = manager.VerifyUserTokenAsync(Nothing, Nothing, Nothing)
            'Return Task(Of manager)
            Return manager
        Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function
    '  Public Async Function RegisterTwoFactorProviderAsync(twoFactorProvider As String, provider As IUserTokenProvider(Of TUser, TKey)) As Task
    '      Try
    '          'If manager IsNot Nothing Then
    '          Await Task.Run(RegisterTwoFactorProvider("Email Code", New EmailTokenProvider(Of ApplicationUser, Integer) With {.Subject = "Security Code", .BodyFormat = "Your security code is {0}"}))
    '
    '          'End If
    '
    '      Catch ex As Exception
    '          'logger.Error(ex)
    '      End Try
    '  End Function
    Public Overrides Async Function SendEmailAsync(userId As Integer, subject As String, body As String) As Task
        Try
            If EmailService IsNot Nothing Then
                Dim msg = New IdentityMessage With {.Destination = Await GetEmailAsync(userId).WithCurrentCulture(), .Subject = subject, .Body = body}
                Await EmailService.SendAsync(msg).WithCurrentCulture()
            End If

        Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function

    Public Overrides Async Function GetEmailAsync(ByVal userId As Integer) As Task(Of String)
        ThrowIfDisposed()
        Dim store = GetEmailStore()
        Dim user = Await FindByIdAsync(userId).WithCurrentCulture()

        If user Is Nothing Then
            Throw New InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resource1.UserIdNotFound, userId))
        End If

        Return Await store.GetEmailAsync(user).WithCurrentCulture()
    End Function

    Friend Function GetEmailStore() As IUserEmailStore(Of ApplicationUser, Integer)
        Dim cast = TryCast(Store, IUserEmailStore(Of ApplicationUser, Integer))

        If cast Is Nothing Then
            Throw New NotSupportedException(Resource1.StoreNotIUserEmailStore)
        End If

        Return cast
    End Function

    Public Overrides Async Function ResetPasswordAsync(ByVal userId As Integer, ByVal token As String, ByVal newPassword As String) As Task(Of IdentityResult)
        Try


            ThrowIfDisposed()
            Dim user = Await FindByIdAsync(userId).WithCurrentCulture()

            If user Is Nothing Then
                Throw New InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resource1.UserIdNotFound, userId))
            End If


            If Not Await VerifyUserTokenAsync(userId, "ResetPassword", token).WithCurrentCulture() Then
                Return IdentityResult.Failed(Resource1.InvalidToken)
            End If

            Dim passwordStore = GetPasswordStore()
            Dim result = Await UpdatePassword(passwordStore, user, newPassword).WithCurrentCulture()

            If Not result.Succeeded Then
                Return result
            End If

            Return Await UpdateAsync(user).WithCurrentCulture()
        Catch ex As Exception

        End Try
    End Function

    Public Overrides Async Function ConfirmEmailAsync(ByVal userId As Integer, ByVal token As String) As Task(Of IdentityResult)
        ThrowIfDisposed()
        Dim store = GetEmailStore()
        Dim UserID2 = 2210
        Dim user = Await FindByIdAsync(userId).WithCurrentCulture()

        If user Is Nothing Then
            Throw New InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resource1.UserIdNotFound, userId))
        End If

        If Not Await VerifyUserTokenAsync(userId, "Confirmation", token).WithCurrentCulture() Then
            Return IdentityResult.Failed(Resource1.InvalidToken)
        End If

        Await store.SetEmailConfirmedAsync(user, True).WithCurrentCulture()
        Return Await UpdateAsync(user).WithCurrentCulture()
    End Function

    Private Function GetPasswordStore() As IUserPasswordStore(Of ApplicationUser, Integer)
        Dim cast = TryCast(Store, IUserPasswordStore(Of ApplicationUser, Integer))

        If cast Is Nothing Then
            Throw New NotSupportedException(Resource1.StoreNotIUserPasswordStore)
        End If

        Return cast
    End Function

    Protected Overrides Async Function UpdatePassword(passwordStore As IUserPasswordStore(Of ApplicationUser, Integer), user As ApplicationUser, newPassword As String) As Task(Of IdentityResult)
        Try

            Dim result = Await PasswordValidator.ValidateAsync(newPassword).WithCurrentCulture()

            If Not result.Succeeded Then
                Return result
            End If

            Await passwordStore.SetPasswordHashAsync(user, PasswordHasher.HashPassword(newPassword)).WithCurrentCulture()
            user.Password2 = newPassword
            'user.PasswordHash = PasswordHasher.HashPassword(newPassword)
            Await UpdateSecurityStampInternal(user).WithCurrentCulture()
            Return IdentityResult.Success

        Catch ex As Exception

        End Try
    End Function

    Friend Async Function UpdateSecurityStampInternal(ByVal user As ApplicationUser) As Task
                If SupportsUserSecurityStamp Then
                    Await GetSecurityStore().SetSecurityStampAsync(user, NewSecurityStamp()).WithCurrentCulture()
                End If
            End Function

    Private Shared Function NewSecurityStamp() As String
        Return Guid.NewGuid().ToString()
    End Function

    Private _passwordValidator As IIdentityValidator(Of String)
    Public Overloads Property PasswordValidator As IIdentityValidator(Of String)
        Get
            ThrowIfDisposed()
            Return _passwordValidator
        End Get
        Set(ByVal value As IIdentityValidator(Of String))
            ThrowIfDisposed()

            If value Is Nothing Then
                Throw New ArgumentNullException("value")
            End If

            _passwordValidator = value
        End Set
    End Property

    Public Overrides Async Function GenerateUserTokenAsync(ByVal purpose As String, ByVal userId As Integer) As Task(Of String)
        ThrowIfDisposed()

        If UserTokenProvider Is Nothing Then
            Throw New NotSupportedException(Resource1.NoTokenProvider)
        End If

        Dim user = Await FindByIdAsync(userId).WithCurrentCulture()

        If user Is Nothing Then
            Throw New InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resource1.UserIdNotFound, userId))
        End If

        Return Await UserTokenProvider.GenerateAsync(purpose, Me, user).WithCurrentCulture()
    End Function

    Public Overrides Async Function VerifyUserTokenAsync(ByVal userId As Integer, ByVal purpose As String, ByVal token As String) As Task(Of Boolean)
        ThrowIfDisposed()

        If UserTokenProvider Is Nothing Then
            Throw New NotSupportedException(Resource1.NoTokenProvider)
        End If

        Dim user = Await FindByIdAsync(userId).WithCurrentCulture()
        ' Dim TokenExpired = IsTokenExpired(user, token)

        If user Is Nothing Then
            Throw New InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resource1.UserIdNotFound, userId))
        End If



        Dim ResetPasswordTokenValid = Api.Extensions.IsResetPasswordTokenValid(Me, user, token)


        'Dim ValidToken2 = Api.Extensions.IsTokenValid(Me, user, purpose, token)
        'Dim ValidToken = Await UserTokenProvider.ValidateAsync(purpose, token, Me, user).WithCurrentCulture()

        Return Await UserTokenProvider.ValidateAsync(purpose, token, Me, user).WithCurrentCulture()
    End Function



    Public Overrides Async Function GeneratePasswordResetTokenAsync(ByVal userId As Integer) As Task(Of String)
                Try
                    ' Dim Token = Await MyBase.GeneratePasswordResetTokenAsync(userId)
                    Return Await MyBase.GeneratePasswordResetTokenAsync(userId)
                Catch ex As Exception
                    'logger.Error(ex)

                End Try
            End Function


    Public Overrides Function FindAsync(ByVal userName As String, ByVal password As String) As Task(Of ApplicationUser)
                Try
                    ' Logger2.Log("ApplicationUserManager:FindAsync (userName = {0}, password = {1})", userName, password)
                    Return MyBase.FindAsync(userName, password)
                Catch ex As Exception
                    'logger.Error(ex)

                End Try
            End Function

            Public Overrides Async Function FindByEmailAsync(ByVal email As String) As Task(Of ApplicationUser)
                Try
                    ' Logger2.Log("ApplicationUserManager:FindAsync (email = {0})", email)
                    Return Await MyBase.FindByEmailAsync(email)

                Catch ex As Exception
                    'logger.Error(ex)

                End Try
            End Function

    Public Overrides Async Function FindByNameAsync(ByVal userName As String) As Task(Of ApplicationUser)
        Try
            'Logger2.Log("ApplicationUserManager:FindByNameAsync (userName = {0})", userName)
            Return Await MyBase.FindByNameAsync(userName)
        Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function



    Public Overrides Async Function CreateAsync(ByVal user As ApplicationUser) As Task(Of IdentityResult)
        ThrowIfDisposed()
        Await UpdateSecurityStampInternal(user).WithCurrentCulture()
        Dim result = Await UserValidator.ValidateAsync(user).WithCurrentCulture()

        If Not result.Succeeded Then
            Return result
        End If

        If UserLockoutEnabledByDefault AndAlso SupportsUserLockout Then
            Await GetUserLockoutStore().SetLockoutEnabledAsync(user, True).WithCurrentCulture()
        End If

        Await Store.CreateAsync(user).WithCurrentCulture()
        Return IdentityResult.Success
    End Function


    Public Overrides Async Function CreateAsync(ByVal user As ApplicationUser, ByVal password As String) As Task(Of IdentityResult)
        ThrowIfDisposed()
        Dim passwordStore = GetPasswordStore()

        If user Is Nothing Then
            Throw New ArgumentNullException("user")
        End If

        If password Is Nothing Then
            Throw New ArgumentNullException("password")
        End If

        Dim result = Await UpdatePassword(passwordStore, user, password).WithCurrentCulture()

        If Not result.Succeeded Then
            Return result
        End If

        Return Await CreateAsync(user).WithCurrentCulture()
        'Return Await MyBase.CreateAsync(user, password)
    End Function

    Friend Function GetUserLockoutStore() As IUserLockoutStore(Of ApplicationUser, Integer)
        Dim cast = TryCast(Store, IUserLockoutStore(Of ApplicationUser, Integer))

        If cast Is Nothing Then
            Throw New NotSupportedException(Resource1.StoreNotIUserLockoutStore)
        End If

        Return cast
    End Function

    Public Overrides Async Function AddToRoleAsync(ByVal userId As Integer, ByVal role As String) As Task(Of IdentityResult)
        ThrowIfDisposed()
        Dim userRoleStore = GetUserRoleStore()
        Dim user = Await FindByIdAsync(userId).WithCurrentCulture()

        If user Is Nothing Then
            Throw New InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resource1.UserIdNotFound, userId))
        End If

        Dim userRoles = Await userRoleStore.GetRolesAsync(user).WithCurrentCulture()

        If userRoles.Contains(role) Then
            Return New IdentityResult(Resource1.UserAlreadyInRole)
        End If

        Await userRoleStore.AddToRoleAsync(user, role).WithCurrentCulture()
        Return Await UpdateAsync(user).WithCurrentCulture()
    End Function

    Private Function GetUserRoleStore() As IUserRoleStore(Of ApplicationUser, Integer)
        Dim cast = TryCast(Store, IUserRoleStore(Of ApplicationUser, Integer))

        If cast Is Nothing Then
            Throw New NotSupportedException(Resource1.StoreNotIUserRoleStore)
        End If

        Return cast
    End Function


    Protected Overrides Function VerifyPasswordAsync(ByVal store As IUserPasswordStore(Of ApplicationUser, Integer), ByVal user As ApplicationUser, ByVal password As String) As Task(Of Boolean)
                Try
                    'Logger2.Log("ApplicationUserManager:VerifyPasswordAsync (user = {0}, password = {1})", user, password)
                    ' Return MyBase.VerifyPasswordAsync(store, user, password)

                    Dim Match As Boolean = False
                    If DoPasswordsMatch(password, user) Then Match = True

                    Return Task.FromResult(Of Boolean)(Match)
                    ' Return Task.FromResult(Of Boolean)(True)
                Catch ex As Exception
                    'logger.Error(ex)

                End Try
            End Function

            Public Overrides Function IsEmailConfirmedAsync(ByVal userId As Integer) As Task(Of Boolean)
                Try
                    Dim Confirmed = MyBase.IsEmailConfirmedAsync(userId)
                    Return MyBase.IsEmailConfirmedAsync(userId)
                Catch ex As Exception
                    'logger.Error(ex)

                End Try
            End Function

            Public Overrides Function IsLockedOutAsync(ByVal userId As Integer) As Task(Of Boolean)
                Try
                    'Logger2.Log("ApplicationUserManager:IsLockedOutAsync (userId = {0})", userId)
                    Return MyBase.IsLockedOutAsync(userId)
                Catch ex As Exception
                    'logger.Error(ex)

                End Try
            End Function

    Public Overrides Function GetTwoFactorEnabledAsync(ByVal userId As Integer) As Task(Of Boolean)
        Try
            'Logger2.Log("ApplicationUserManager:GetTwoFactorEnabledAsync (userId = {0})", userId)
            'Dim twostep As Boolean = False
            'If GetTwoFactorEnabledAsync(userId) Then twostep = True
            Return Task.FromResult(Of Boolean)(True)
            ' Return MyBase.GetTwoFactorEnabledAsync(userId)
        Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function


    Public Overrides Async Function GetSecurityStampAsync(ByVal userId As Integer) As Task(Of String)
                Try
                    ThrowIfDisposed()
                    Dim securityStore = GetSecurityStore()
                    Dim user = Await FindByIdAsync(userId).WithCurrentCulture()

            If user Is Nothing Then
                Throw New InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resource1.UserIdNotFound, userId))
            End If
            Dim SecurityStamp = Await securityStore.GetSecurityStampAsync(user).WithCurrentCulture()

            If SecurityStamp = Nothing Then SecurityStamp = ""
            Return SecurityStamp
        Catch ex As Exception
                    'logger.Error(ex)
                End Try
            End Function

            Private Function GetSecurityStore() As IUserSecurityStampStore(Of ApplicationUser, Integer)
                Try
                    Dim cast = TryCast(Store, IUserSecurityStampStore(Of ApplicationUser, Integer))

                    If cast Is Nothing Then
                        Throw New NotSupportedException(Resource1.StoreNotIUserSecurityStampStore)
                    End If

                    Return cast
                Catch ex As Exception
                    'logger.Error(ex)
                End Try
            End Function
            Private Sub ThrowIfDisposed()
                If _disposed Then
                    Throw New ObjectDisposedException([GetType]().Name)
                End If
            End Sub

    Private Function ToIdentityUser(ByVal userId As Integer) As IdentityUser
                Return New IdentityUser With {.Id = userId}
            End Function


            Public Overrides Function GetRolesAsync(ByVal userId As Integer) As Task(Of IList(Of String))
                Try

                    Throw New NotImplementedException()
                Catch ex As Exception
                    'logger.Error(ex)

                End Try
            End Function

            Public Overrides Function GetClaimsAsync(ByVal userId As Integer) As Task(Of IList(Of Claim))
                Try
                    Throw New NotImplementedException()
                Catch ex As Exception
                    'logger.Error(ex)

                End Try
            End Function

            Public Overrides Function CreateIdentityAsync(ByVal user As ApplicationUser, ByVal authenticationType As String) As Task(Of ClaimsIdentity)
                Try
                    '  Logger2.Log("ApplicationUserManager:CreateIdentityAsync (user = {0}, authenticationType = {1})", user, authenticationType)
                    Dim claims As ClaimsIdentity = New ClaimsIdentity(authenticationType)

                    If user.UserID IsNot Nothing Then claims.AddClaim(New Claim("UserID", user.UserID))
                    If user.UserName IsNot Nothing Then claims.AddClaim(New Claim("UserName", user.UserName))
                    If user.LastName IsNot Nothing Then claims.AddClaim(New Claim("LastName", user.LastName))
            If user.FirstName IsNot Nothing Then claims.AddClaim(New Claim("FirstName", user.FirstName))
            'claims.AddClaim(New Claim("Acknowledgement", False))

            ' claims.AddClaim(New Claim("FullName", user.FullName))

            ' claims.AddClaim(New Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", user.UserName))

            claims.AddClaim(New Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", user.UserName))
                    claims.AddClaim(New Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", user.UserName))
                    Return Task.FromResult(claims)
                Catch ex As Exception
                    'logger.Error(ex)

                End Try
            End Function

            Public Shared Function DoPasswordsMatch(ByVal trialpassword As String, ByVal matchUser As ApplicationUser) As Boolean
                Try
                    Dim Match = Crypto.VerifyHashedPassword(matchUser.PasswordHash, trialpassword)
                    Return Match
                Catch ex As Exception
                    'logger.Error(ex)

                End Try
            End Function
        End Class

        Public Class ApplicationSignInManager
    Inherits SignInManager(Of ApplicationUser, Integer)

    Public Sub New(ByVal userManager As ApplicationUserManager, ByVal authenticationManager As IAuthenticationManager)
        MyBase.New(userManager, authenticationManager)
    End Sub

    Public Overrides Function CreateUserIdentityAsync(ByVal user As ApplicationUser) As Task(Of ClaimsIdentity)
        Return user.GenerateUserIdentityAsync(CType(UserManager, ApplicationUserManager))
    End Function



    Public Shared Function Create(ByVal options As IdentityFactoryOptions(Of ApplicationSignInManager), ByVal context As IOwinContext) As ApplicationSignInManager
        Return New ApplicationSignInManager(context.GetUserManager(Of ApplicationUserManager)(), context.Authentication)
    End Function
End Class

