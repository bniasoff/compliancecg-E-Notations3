Imports Microsoft.AspNet.Identity
Imports System.Data.Entity
Imports compliancecg.compliancecg

Imports System
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports System.Xml



'Public Class  FacilityUser
'    Implements IUser

'    Public Property Id As String Implements IUser.Id
'    Public Property UserName As String Implements IUser.UserName
'    Public Property PasswordHash As String
'    Public Property AccessFailedCount As Integer
'    Public Property Salt As String
'    Public Property Password As String

'    Public Async Function GenerateUserIdentityAsync(ByVal manager As UserManager(Of  FacilityUser)) As Task(Of ClaimsIdentity)
'        Logger2.Log(" FacilityUser:GenerateUserIdentityAsync ()")
'        Dim userIdentity = Await manager.CreateIdentityAsync(Me, DefaultAuthenticationTypes.ApplicationCookie)
'        Return userIdentity
'    End Function

'    Public Overrides Function ToString() As String
'        Return String.Format("Id={0} PasswordHash={1}", Id, (If(PasswordHash Is Nothing, String.Empty, PasswordHash)))
'    End Function
'End Class

Public Class XmlUserStore
    Implements IUserStore(Of FacilityUser), IUserPasswordStore(Of FacilityUser), IUserLockoutStore(Of FacilityUser, Object), IUserTwoFactorStore(Of FacilityUser, Object), IUserLoginStore(Of FacilityUser, Object), IUserPhoneNumberStore(Of FacilityUser, Object)

    Private AuthorizationService As New AuthorizationService



    Public Sub New()
    End Sub

    Private Function CreateAsync(user As FacilityUser) As Task Implements IUserStore(Of FacilityUser, String).CreateAsync
        Throw New NotImplementedException()
    End Function

    Private Function UpdateAsync(user As FacilityUser) As Task Implements IUserStore(Of FacilityUser, String).UpdateAsync
        Throw New NotImplementedException()
    End Function

    Private Function DeleteAsync(user As FacilityUser) As Task Implements IUserStore(Of FacilityUser, String).DeleteAsync
        Throw New NotImplementedException()
    End Function

    Private Function FindByIdAsync(userId As String) As Task(Of FacilityUser) Implements IUserStore(Of FacilityUser, String).FindByIdAsync
        'Logger2.Log("XmlUserStore:FindByIdAsync (userId = {0})", userId)

        Dim User As FacilityUser = Nothing
        User = AuthorizationService.GetUser(userId)
        If User IsNot Nothing Then
            Dim context As HttpContext = HttpContext.Current
            If context.Session IsNot Nothing Then context.Session(" FacilityUser") = User
        End If

        'If User IsNot Nothing Then
        '    User.Id = userId
        'End If

        Return Task.FromResult(Of FacilityUser)(User)
    End Function

    Private Function FindByNameAsync(userName As String) As Task(Of FacilityUser) Implements IUserStore(Of FacilityUser, String).FindByNameAsync
        Return FindByIdAsync(userName)
    End Function

    Private Sub Dispose() Implements IDisposable.Dispose

    End Sub

    Private Function SetPasswordHashAsync(user As FacilityUser, passwordHash As String) As Task Implements IUserPasswordStore(Of FacilityUser, String).SetPasswordHashAsync
        Throw New NotImplementedException()
    End Function

    Private Function GetPasswordHashAsync(user As FacilityUser) As Task(Of String) Implements IUserPasswordStore(Of FacilityUser, String).GetPasswordHashAsync
        'Logger2.Log("XmlUserStore:GetPasswordHashAsync (user = {0})", user)
        Dim hash As String = user.PasswordHash
        Return Task.FromResult(Of String)(hash)
    End Function

    Private Function HasPasswordAsync(user As FacilityUser) As Task(Of Boolean) Implements IUserPasswordStore(Of FacilityUser, String).HasPasswordAsync
        Throw New NotImplementedException()
    End Function

    Private Function GetLockoutEndDateAsync(user As FacilityUser) As Task(Of DateTimeOffset) Implements IUserLockoutStore(Of FacilityUser, Object).GetLockoutEndDateAsync
        Throw New NotImplementedException()
    End Function

    Private Function SetLockoutEndDateAsync(user As FacilityUser, lockoutEnd As DateTimeOffset) As Task Implements IUserLockoutStore(Of FacilityUser, Object).SetLockoutEndDateAsync
        Throw New NotImplementedException()
    End Function

    Private Function IncrementAccessFailedCountAsync(user As FacilityUser) As Task(Of Integer) Implements IUserLockoutStore(Of FacilityUser, Object).IncrementAccessFailedCountAsync
        Throw New NotImplementedException()
    End Function

    Private Function ResetAccessFailedCountAsync(user As FacilityUser) As Task Implements IUserLockoutStore(Of FacilityUser, Object).ResetAccessFailedCountAsync
        Throw New NotImplementedException()
    End Function

    Private Function GetAccessFailedCountAsync(user As FacilityUser) As Task(Of Integer) Implements IUserLockoutStore(Of FacilityUser, Object).GetAccessFailedCountAsync
        Return Task.FromResult(user.AccessFailedCount)
    End Function

    Private Function GetLockoutEnabledAsync(user As FacilityUser) As Task(Of Boolean) Implements IUserLockoutStore(Of FacilityUser, Object).GetLockoutEnabledAsync
        Logger2.Log("XmlUserStore:GetLockoutEnabledAsync (user = {0})", user)
        Return Task.FromResult(Of Boolean)(False)
    End Function

    Private Function SetLockoutEnabledAsync(user As FacilityUser, enabled As Boolean) As Task Implements IUserLockoutStore(Of FacilityUser, Object).SetLockoutEnabledAsync
        Throw New NotImplementedException()
    End Function

    Private Function CreateAsync1(user As FacilityUser) As Task Implements IUserStore(Of FacilityUser, Object).CreateAsync
        Throw New NotImplementedException()
    End Function

    Private Function UpdateAsync1(user As FacilityUser) As Task Implements IUserStore(Of FacilityUser, Object).UpdateAsync
        Throw New NotImplementedException()
    End Function

    Private Function DeleteAsync1(user As FacilityUser) As Task Implements IUserStore(Of FacilityUser, Object).DeleteAsync
        Throw New NotImplementedException()
    End Function

    Private Function FindByIdAsync1(userId As Object) As Task(Of FacilityUser) Implements IUserStore(Of FacilityUser, Object).FindByIdAsync
        Throw New NotImplementedException()
    End Function

    Private Function FindByNameAsync1(userName As String) As Task(Of FacilityUser) Implements IUserStore(Of FacilityUser, Object).FindByNameAsync
        Throw New NotImplementedException()
    End Function

    Public Function SetTwoFactorEnabledAsync(user As FacilityUser, enabled As Boolean) As Task Implements IUserTwoFactorStore(Of FacilityUser, Object).SetTwoFactorEnabledAsync
        Throw New NotImplementedException()
    End Function

    Public Function GetTwoFactorEnabledAsync(user As FacilityUser) As Task(Of Boolean) Implements IUserTwoFactorStore(Of FacilityUser, Object).GetTwoFactorEnabledAsync

    End Function

    Public Function AddLoginAsync(user As FacilityUser, login As UserLoginInfo) As Task Implements IUserLoginStore(Of FacilityUser, Object).AddLoginAsync
        Throw New NotImplementedException()
    End Function

    Public Function RemoveLoginAsync(user As FacilityUser, login As UserLoginInfo) As Task Implements IUserLoginStore(Of FacilityUser, Object).RemoveLoginAsync
        Throw New NotImplementedException()
    End Function

    Public Function GetLoginsAsync(user As FacilityUser) As Task(Of IList(Of UserLoginInfo)) Implements IUserLoginStore(Of FacilityUser, Object).GetLoginsAsync
        Dim result As IList(Of UserLoginInfo) '= user.Logins.[Select](Function(l) New UserLoginInfo(l.LoginProvider, l.ProviderKey, l.ProviderDisplayName)).ToList()
        Return Task.FromResult(result)
    End Function

    Public Function FindAsync(login As UserLoginInfo) As Task(Of FacilityUser) Implements IUserLoginStore(Of FacilityUser, Object).FindAsync
        Throw New NotImplementedException()
    End Function

    Public Function SetPhoneNumberAsync(user As FacilityUser, phoneNumber As String) As Task Implements IUserPhoneNumberStore(Of FacilityUser, Object).SetPhoneNumberAsync
        Throw New NotImplementedException()
    End Function

    Public Function GetPhoneNumberAsync(user As FacilityUser) As Task(Of String) Implements IUserPhoneNumberStore(Of FacilityUser, Object).GetPhoneNumberAsync
        Dim PhoneNumber As String = user.FullName
        Return Task.FromResult(Of String)(PhoneNumber)
    End Function

    Public Function GetPhoneNumberConfirmedAsync(user As FacilityUser) As Task(Of Boolean) Implements IUserPhoneNumberStore(Of FacilityUser, Object).GetPhoneNumberConfirmedAsync
        Throw New NotImplementedException()
    End Function

    Public Function SetPhoneNumberConfirmedAsync(user As FacilityUser, confirmed As Boolean) As Task Implements IUserPhoneNumberStore(Of FacilityUser, Object).SetPhoneNumberConfirmedAsync
        Throw New NotImplementedException()
    End Function
End Class

