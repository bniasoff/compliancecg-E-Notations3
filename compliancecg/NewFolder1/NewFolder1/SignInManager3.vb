Imports System.Data.Entity.SqlServer.Utilities
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security

Public Class EnhancedSignInManager(Of TUser As {Class, IUser(Of TKey)}, TKey As IEquatable(Of TKey))
    Inherits SignInManager(Of TUser, TKey)

    Public Sub New(ByVal userManager As UserManager(Of TUser, TKey), ByVal authenticationManager As IAuthenticationManager)
        MyBase.New(userManager, authenticationManager)
    End Sub

    Public Overrides Async Function SignInAsync(ByVal user As TUser, ByVal isPersistent As Boolean, ByVal rememberBrowser As Boolean) As Task
        Dim userIdentity = Await CreateUserIdentityAsync(user).WithCurrentCulture()
        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie)

        If rememberBrowser Then
            Dim rememberBrowserIdentity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(ConvertIdToString(user.Id))
            AuthenticationManager.SignIn(New AuthenticationProperties With {
                .IsPersistent = isPersistent
            }, userIdentity, rememberBrowserIdentity)
        Else
            AuthenticationManager.SignIn(New AuthenticationProperties With {
                .IsPersistent = isPersistent
            }, userIdentity)
        End If
    End Function

    Private Async Function SignInOrTwoFactor(ByVal user As TUser, ByVal isPersistent As Boolean) As Task(Of SignInStatus)
        Dim id = Convert.ToString(user.Id)

        If UserManager.SupportsUserTwoFactor AndAlso Await UserManager.GetTwoFactorEnabledAsync(user.Id).WithCurrentCulture() AndAlso (Await UserManager.GetValidTwoFactorProvidersAsync(user.Id).WithCurrentCulture()).Count > 0 AndAlso Not Await AuthenticationManager.TwoFactorBrowserRememberedAsync(id).WithCurrentCulture() Then
            Dim identity = New ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie)
            identity.AddClaim(New Claim(ClaimTypes.NameIdentifier, id))
            AuthenticationManager.SignIn(identity)
            Return SignInStatus.RequiresVerification
        End If

        Await SignInAsync(user, isPersistent, False).WithCurrentCulture()
        Return SignInStatus.Success
    End Function

    Public Overrides Async Function PasswordSignInAsync(ByVal userName As String, ByVal password As String, ByVal isPersistent As Boolean, ByVal shouldLockout As Boolean) As Task(Of SignInStatus)
        If UserManager Is Nothing Then
            Return SignInStatus.Failure
        End If

        Dim user = Await UserManager.FindByNameAsync(userName).WithCurrentCulture()

        If user Is Nothing Then
            Return SignInStatus.Failure
        End If

        If UserManager.SupportsUserLockout AndAlso Await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture() Then
            Return SignInStatus.LockedOut
        End If

        If UserManager.SupportsUserPassword AndAlso Await UserManager.CheckPasswordAsync(user, password).WithCurrentCulture() Then
            Return Await SignInOrTwoFactor(user, isPersistent).WithCurrentCulture()
        End If

        If shouldLockout AndAlso UserManager.SupportsUserLockout Then
            Await UserManager.AccessFailedAsync(user.Id).WithCurrentCulture()

            If Await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture() Then
                Return SignInStatus.LockedOut
            End If
        End If

        Return SignInStatus.Failure
    End Function
End Class
