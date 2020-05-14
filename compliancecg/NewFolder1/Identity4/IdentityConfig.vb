Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Data.Entity.SqlServer.Utilities
Imports System.Linq
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports System.Web
Imports System.Web.Helpers
Imports compliancecg
Imports compliancecg.Membership
Imports compliancecg.Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports Crypto = compliancecg.Microsoft.AspNet.Identity.Crypto

Module Logger2
    Sub Log(ByVal fmt As String, ParamArray args As Object())
        Dim s As String = String.Format(fmt, args)
        Debug.WriteLine(s)
    End Sub

    Friend Sub Log(v As String, userName As String, password As String)
        Throw New NotImplementedException()
    End Sub
End Module



Public Class ApplicationUserManager
    Inherits UserManager(Of UserAccount)

    'Private Property UserId As String

    Public Sub New(ByVal store As IUserStore(Of UserAccount))
        MyBase.New(store)
    End Sub
    Public Shared Function DoPasswordsMatch(ByVal trialpassword As String, ByVal matchUser As UserAccount) As Boolean
        Dim ret As Boolean = False

        'Dim Encoded = Crypto.HashPassword(trialpassword)
        Dim Match = Crypto.VerifyHashedPassword(matchUser.PasswordHash, trialpassword)

        'Dim trialpassword2 = crypto.Encode(trialpassword)
        'If crypto.Encode(trialpassword, matchUser.Salt).Equals(matchUser.PasswordHash) = True Then
        '    ret = True
        'End If
        'crypto = Nothing
        Return Match
    End Function
    Public Overrides Function FindAsync(ByVal userName As String, ByVal password As String) As Task(Of UserAccount)
        Logger2.Log("ApplicationUserManager:FindAsync (userName = {0}, password = {1})", userName, password)
        Return MyBase.FindAsync(userName, password)
    End Function

    Public Overrides Function FindByEmailAsync(ByVal email As String) As Task(Of UserAccount)
        Logger2.Log("ApplicationUserManager:FindAsync (email = {0})", email)

    End Function

    Public Overrides Function FindByNameAsync(ByVal userName As String) As Task(Of UserAccount)

        Logger2.Log("ApplicationUserManager:FindByNameAsync (userName = {0})", userName)
        Return MyBase.FindByNameAsync(userName)
    End Function

    Protected Overrides Function VerifyPasswordAsync(ByVal store As IUserPasswordStore(Of UserAccount, String), ByVal user As UserAccount, ByVal password As String) As Task(Of Boolean)
        'Logger2.Log("ApplicationUserManager:VerifyPasswordAsync (user = {0}, password = {1})", user, password)
        Return MyBase.VerifyPasswordAsync(store, user, password)

        'Dim Match As Boolean = False
        ' If DoPasswordsMatch(password, user) Then Match = True

        ' Return Task.FromResult(Of Boolean)(Match)
        ' Return Task.FromResult(Of Boolean)(True)
    End Function



    Public Overrides Function IsLockedOutAsync(ByVal userId As String) As Task(Of Boolean)
        'Logger2.Log("ApplicationUserManager:IsLockedOutAsync (userId = {0})", userId)
        Return MyBase.IsLockedOutAsync(userId)
    End Function

    Public Overrides Function GetTwoFactorEnabledAsync(ByVal userId As String) As Task(Of Boolean)
        ' Logger2.Log("ApplicationUserManager:GetTwoFactorEnabledAsync (userId = {0})", userId)
        Return Task.FromResult(Of Boolean)(False)
    End Function

    Public Overrides Function GetSecurityStampAsync(ByVal userId As String) As Task(Of String)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetRolesAsync(ByVal userId As String) As Task(Of IList(Of String))

        'Throw New NotImplementedException()
    End Function

    Public Overrides Function GetClaimsAsync(ByVal userId As String) As Task(Of IList(Of Claim))
        Throw New NotImplementedException()
    End Function

    Public Overrides Function CreateIdentityAsync(ByVal user As UserAccount, ByVal authenticationType As String) As Task(Of ClaimsIdentity)
        Logger2.Log("ApplicationUserManager:CreateIdentityAsync (user = {0}, authenticationType = {1})", user, authenticationType)
        Dim claims As ClaimsIdentity = New ClaimsIdentity(authenticationType)

        claims.AddClaim(New Claim("UserID", user.UserID))
        claims.AddClaim(New Claim("UserName", user.UserName))
        claims.AddClaim(New Claim("LastName", user.LastName))
        claims.AddClaim(New Claim("FirstName", user.FirstName))
        claims.AddClaim(New Claim("FullName", user.FullName))

        ' claims.AddClaim(New Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", user.UserName))

        claims.AddClaim(New Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", user.UserName))
        claims.AddClaim(New Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", user.UserName))
        Return Task.FromResult(claims)
    End Function

    Public Shared Function Create(ByVal options As IdentityFactoryOptions(Of ApplicationUserManager), ByVal context As IOwinContext) As ApplicationUserManager
        '    Logger.Log("ApplicationUserManager:Create ()")
        Dim manager = New ApplicationUserManager(New XmlUserStore)
        Dim dataProtectionProvider = options.DataProtectionProvider
        If dataProtectionProvider IsNot Nothing Then
            manager.UserTokenProvider = New DataProtectorTokenProvider(Of UserAccount)(dataProtectionProvider.Create("ASP.NET Identity"))
        End If


        '' Configure validation logic for usernames
        'manager.UserValidator = New UserValidator(Of ApplicationUser)(manager) With {
        '    .AllowOnlyAlphanumericUserNames = False,
        '    .RequireUniqueEmail = True
        '}
        Dim Hasher = New CustomPasswordHasher
        manager.PasswordHasher = New CustomPasswordHasher


        ' Configure validation logic for passwords
        manager.PasswordValidator = New PasswordValidator With {
            .RequiredLength = 3,
            .RequireNonLetterOrDigit = False,
            .RequireDigit = True,
            .RequireLowercase = False,
            .RequireUppercase = False
        }
        Return manager
    End Function
End Class

Public Class ApplicationSignInManager
    Inherits SignInManager(Of UserAccount, String)

    Public Sub New(ByVal userManager As ApplicationUserManager, ByVal authenticationManager As IAuthenticationManager)
        MyBase.New(userManager, authenticationManager)
        Logger2.Log("ApplicationSignInManager:ApplicationSignInManager ()")
    End Sub

    Public Overrides Async Function SignInAsync(ByVal user As UserAccount, ByVal isPersistent As Boolean, ByVal rememberBrowser As Boolean) As Task
        Dim userIdentity = Await CreateUserIdentityAsync(user).WithCurrentCulture()
        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie)

        If rememberBrowser Then
            Dim rememberBrowserIdentity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(ConvertIdToString(user.Id))
            AuthenticationManager.SignIn(New AuthenticationProperties With {.IsPersistent = isPersistent}, userIdentity, rememberBrowserIdentity)
        Else
            AuthenticationManager.SignIn(New AuthenticationProperties With {.IsPersistent = isPersistent}, userIdentity)
        End If
    End Function

    'Public Overrides Function CreateUserIdentityAsync(ByVal user As User) As Task(Of ClaimsIdentity)
    '    Return IdentityHelper.GenerateUserIdentityHelperAsync(user, CType(UserManager, ApplicationUserManager))
    'End Function

    'Public Overrides Function CreateUserIdentityAsync(ByVal user As AppUser) As Task(Of ClaimsIdentity)
    '    If Not System.String.IsNullOrEmpty(user.Avatar) Then user.Claims.Add(New AppUserClaim() With {.ClaimType = "avatar", .ClaimValue = user.Avatar})
    '    Return user.GenerateUserIdentityAsync(CType(UserManager, AppUserManager))
    'End Function


    Public Overrides Function CreateUserIdentityAsync(ByVal user As UserAccount) As Task(Of ClaimsIdentity)
        ' If Not System.String.IsNullOrEmpty(user.ClientID) Then user.Claims.Add(New AppUserClaim() With {.ClaimType = "ClientID", .ClaimValue = user.ClientID})

        'Dim claims As ClaimsIdentity = New ClaimsIdentity(AuthenticationType)
        'claims.AddClaim(New Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", user.UserName))
        'claims.AddClaim(New Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", user.UserName))
        Logger2.Log("ApplicationSignInManager:CreateUserIdentityAsync (user = {0})", user)
        Return user.GenerateUserIdentityAsync(CType(UserManager, ApplicationUserManager))
    End Function



    Public Overrides Function PasswordSignInAsync(ByVal userName As String, ByVal password As String, ByVal isPersistent As Boolean, ByVal shouldLockout As Boolean) As Task(Of SignInStatus)
        Logger2.Log("ApplicationSignInManager:PasswordSignInAsync (userName = {0}, password = {1}, isPersistent = {2}, shouldLockout = {3})", userName, password, isPersistent, shouldLockout)
        Return MyBase.PasswordSignInAsync(userName, password, isPersistent, shouldLockout)
    End Function

    Public Shared Function Create(ByVal options As IdentityFactoryOptions(Of ApplicationSignInManager), ByVal context As IOwinContext) As ApplicationSignInManager
        Logger2.Log("ApplicationSignInManager:Create ()")
        Return New ApplicationSignInManager(context.GetUserManager(Of ApplicationUserManager)(), context.Authentication)
    End Function


End Class

'Public Class ApplicationRoleManager

'    Inherits RoleManager(Of UserRole)
'    Public Sub New(roleStore As IRoleStore(Of UserRole, String))
'        MyBase.New(roleStore)
'    End Sub

'    Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationRoleManager), context As IOwinContext) As ApplicationRoleManager
'        Return New ApplicationRoleManager(New RoleStore(Of UserRole)(context.[Get](Of ApplicationDbContext)()))
'    End Function

'End Class
' Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly

'Public Class ApplicationRoleManager
'    Inherits RoleManager(Of IdentityRole)

'    Public Sub New(ByVal roleStore As IRoleStore(Of IdentityRole, String))
'        MyBase.New(roleStore)
'    End Sub

'    Public Shared Function Create(ByVal options As IdentityFactoryOptions(Of ApplicationRoleManager), ByVal context As IOwinContext) As ApplicationRoleManager
'        Dim manager = New ApplicationRoleManager(New RoleStore(Of IdentityRole)(context.[Get](Of ApplicationDbContext)()))
'        Return manager
'    End Function
'End Class


Public Class ApplicationRoleManager
    Inherits RoleManager(Of IdentityRole)

    Public Sub New(ByVal roleStore As IRoleStore(Of IdentityRole, String))
        MyBase.New(roleStore)
    End Sub

    Public Shared Function Create(ByVal options As IdentityFactoryOptions(Of ApplicationRoleManager), ByVal context As IOwinContext) As ApplicationRoleManager
        Dim manager = New ApplicationRoleManager(New RoleStore(Of IdentityRole)(context.[Get](Of ApplicationDbContext)()))
        Return manager
    End Function
End Class


'Public Class ApplicationRoleManager
'    Inherits RoleManager(Of CustomRole, String)

'    Public Sub New(ByVal roleStore As IRoleStore(Of CustomRole, String))
'        MyBase.New(roleStore)
'    End Sub

'    Public Shared Function Create(ByVal options As IdentityFactoryOptions(Of ApplicationRoleManager), ByVal context As IOwinContext) As ApplicationRoleManager
'        Return New ApplicationRoleManager(New RoleStore(Of CustomRole, String, CustomRole)(context.[Get](Of ApplicationDbContext)()))
'    End Function
'End Class




'Public Class ApplicationSignInManager
'    Inherits SignInManager(Of UserAccount, String)

'    Public Sub New(ByVal userManager As ApplicationUserManager, ByVal authenticationManager As IAuthenticationManager)
'        MyBase.New(userManager, authenticationManager)
'        Logger2.Log("ApplicationSignInManager:ApplicationSignInManager ()")
'    End Sub

'    Public Overrides Function CreateUserIdentityAsync(ByVal user As UserAccount) As Task(Of ClaimsIdentity)


'        Logger2.Log("ApplicationSignInManager:CreateUserIdentityAsync (user = {0})", user)
'        Return user.GenerateUserIdentityAsync(CType(UserManager, ApplicationUserManager))
'    End Function

'    Public Overrides Function PasswordSignInAsync(ByVal userName As String, ByVal password As String, ByVal isPersistent As Boolean, ByVal shouldLockout As Boolean) As Task(Of SignInStatus)
'        Logger2.Log("ApplicationSignInManager:PasswordSignInAsync (userName = {0}, password = {1}, isPersistent = {2}, shouldLockout = {3})", userName, password, isPersistent, shouldLockout)
'        Return MyBase.PasswordSignInAsync(userName, password, isPersistent, shouldLockout)
'    End Function

'    Public Shared Function Create(ByVal options As IdentityFactoryOptions(Of ApplicationSignInManager), ByVal context As IOwinContext) As ApplicationSignInManager
'        Logger2.Log("ApplicationSignInManager:Create ()")
'        Return New ApplicationSignInManager(context.GetUserManager(Of ApplicationUserManager)(), context.Authentication)
'    End Function


'End Class

