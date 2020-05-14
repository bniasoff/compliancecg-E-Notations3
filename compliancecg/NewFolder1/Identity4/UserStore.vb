Imports Microsoft.AspNet.Identity
Imports System.Data.Entity


Imports System
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports System.Xml
Imports compliancecg.Membership
Imports Crypto = compliancecg.Microsoft.AspNet.Identity.Crypto
Imports CCGData.CCGData



'Public Class  membership.UserAccount
'    Implements IUser

'    Public Property Id As String Implements IUser.Id
'    Public Property UserName As String Implements IUser.UserName
'    Public Property PasswordHash As String
'    Public Property AccessFailedCount As Integer
'    Public Property Salt As String
'    Public Property Password As String

'    Public Async Function GenerateUserIdentityAsync(ByVal manager As UserManager(Of  membership.UserAccount)) As Task(Of ClaimsIdentity)
'        Logger2.Log(" membership.UserAccount:GenerateUserIdentityAsync ()")
'        Dim userIdentity = Await manager.CreateIdentityAsync(Me, DefaultAuthenticationTypes.ApplicationCookie)
'        Return userIdentity
'    End Function

'    Public Overrides Function ToString() As String
'        Return String.Format("Id={0} PasswordHash={1}", Id, (If(PasswordHash Is Nothing, String.Empty, PasswordHash)))
'    End Function
'End Class

Public Class XmlUserStore
    Implements IUserStore(Of Membership.UserAccount), IUserPasswordStore(Of Membership.UserAccount), IUserLockoutStore(Of Membership.UserAccount, Object), IUserTwoFactorStore(Of Membership.UserAccount, Object), IUserLoginStore(Of Membership.UserAccount, Object), IUserPhoneNumberStore(Of Membership.UserAccount, Object)

    Private AuthorizationService As New AuthorizationService
    Private CCGDataEntities As New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)



    Public Sub New()
    End Sub

    Private Function CreateAsync(user As Membership.UserAccount) As Task Implements IUserStore(Of Membership.UserAccount, String).CreateAsync

        If user IsNot Nothing Then
            Dim newUserAccount As New CCGData.CCGData.UserAccount
            newUserAccount.UserName = user.UserName
            newUserAccount.Email = user.Email
            'newUserAccount.LastName = user.LastName
            'newUserAccount.FirstName = user.FirstName
            newUserAccount.PasswordHash = user.PasswordHash
            CCGDataEntities.UserAccounts.Add(newUserAccount)
            Dim Updated = CCGDataEntities.SaveChanges()
        End If


        Return Task.FromResult(Of Membership.UserAccount)(user)
        'Throw New NotImplementedException()
    End Function

    Private Function UpdateAsync(user As Membership.UserAccount) As Task Implements IUserStore(Of Membership.UserAccount, String).UpdateAsync
        Throw New NotImplementedException()
    End Function

    Private Function DeleteAsync(user As Membership.UserAccount) As Task Implements IUserStore(Of Membership.UserAccount, String).DeleteAsync
        Throw New NotImplementedException()
    End Function

    Private Function FindByIdAsync(userId As String) As Task(Of Membership.UserAccount) Implements IUserStore(Of Membership.UserAccount, String).FindByIdAsync
        'Logger2.Log("XmlUserStore:FindByIdAsync (userId = {0})", userId)

        Dim User As Membership.UserAccount = Nothing
        User = AuthorizationService.GetUser(userId)
        If User IsNot Nothing Then
            Dim context As HttpContext = HttpContext.Current
            If context IsNot Nothing Then context.Session(" membership.UserAccount") = User
        End If

        'If User IsNot Nothing Then
        '    User.Id = userId
        'End If

        Return Task.FromResult(Of Membership.UserAccount)(User)
    End Function

    Private Function FindByNameAsync(userName As String) As Task(Of Membership.UserAccount) Implements IUserStore(Of Membership.UserAccount, String).FindByNameAsync
        Return FindByIdAsync(userName)
    End Function

    Private Sub Dispose() Implements IDisposable.Dispose

    End Sub

    Private Function SetPasswordHashAsync(user As Membership.UserAccount, passwordHash As String) As Task Implements IUserPasswordStore(Of Membership.UserAccount, String).SetPasswordHashAsync
        user.PasswordHash = passwordHash
        Return Task.FromResult(Of String)(passwordHash)
        ' user.PasswordHash = Crypto.HashPassword(user.Password)
    End Function

    Private Function GetPasswordHashAsync(user As Membership.UserAccount) As Task(Of String) Implements IUserPasswordStore(Of Membership.UserAccount, String).GetPasswordHashAsync
        'Logger2.Log("XmlUserStore:GetPasswordHashAsync (user = {0})", user)
        Dim hash As String = user.PasswordHash
        Return Task.FromResult(Of String)(hash)
    End Function

    Private Function HasPasswordAsync(user As Membership.UserAccount) As Task(Of Boolean) Implements IUserPasswordStore(Of Membership.UserAccount, String).HasPasswordAsync
        Throw New NotImplementedException()
    End Function

    Private Function GetLockoutEndDateAsync(user As Membership.UserAccount) As Task(Of DateTimeOffset) Implements IUserLockoutStore(Of Membership.UserAccount, Object).GetLockoutEndDateAsync
        Throw New NotImplementedException()
    End Function

    Private Function SetLockoutEndDateAsync(user As Membership.UserAccount, lockoutEnd As DateTimeOffset) As Task Implements IUserLockoutStore(Of Membership.UserAccount, Object).SetLockoutEndDateAsync
        Throw New NotImplementedException()
    End Function

    Private Function IncrementAccessFailedCountAsync(user As Membership.UserAccount) As Task(Of Integer) Implements IUserLockoutStore(Of Membership.UserAccount, Object).IncrementAccessFailedCountAsync
        Throw New NotImplementedException()
    End Function

    Private Function ResetAccessFailedCountAsync(user As Membership.UserAccount) As Task Implements IUserLockoutStore(Of Membership.UserAccount, Object).ResetAccessFailedCountAsync
        Throw New NotImplementedException()
    End Function

    Private Function GetAccessFailedCountAsync(user As Membership.UserAccount) As Task(Of Integer) Implements IUserLockoutStore(Of Membership.UserAccount, Object).GetAccessFailedCountAsync
        Return Task.FromResult(user.AccessFailedCount)
    End Function

    Private Function GetLockoutEnabledAsync(user As Membership.UserAccount) As Task(Of Boolean) Implements IUserLockoutStore(Of Membership.UserAccount, Object).GetLockoutEnabledAsync
        Logger2.Log("XmlUserStore:GetLockoutEnabledAsync (user = {0})", user)
        Return Task.FromResult(Of Boolean)(False)
    End Function

    Private Function SetLockoutEnabledAsync(user As Membership.UserAccount, enabled As Boolean) As Task Implements IUserLockoutStore(Of Membership.UserAccount, Object).SetLockoutEnabledAsync
        Throw New NotImplementedException()
    End Function

    Private Function CreateAsync1(user As Membership.UserAccount) As Task Implements IUserStore(Of Membership.UserAccount, Object).CreateAsync
        Throw New NotImplementedException()
    End Function

    Private Function UpdateAsync1(user As Membership.UserAccount) As Task Implements IUserStore(Of Membership.UserAccount, Object).UpdateAsync
        Throw New NotImplementedException()
    End Function

    Private Function DeleteAsync1(user As Membership.UserAccount) As Task Implements IUserStore(Of Membership.UserAccount, Object).DeleteAsync
        Throw New NotImplementedException()
    End Function

    Private Function FindByIdAsync1(userId As Object) As Task(Of Membership.UserAccount) Implements IUserStore(Of Membership.UserAccount, Object).FindByIdAsync
        Throw New NotImplementedException()
    End Function

    Private Function FindByNameAsync1(userName As String) As Task(Of Membership.UserAccount) Implements IUserStore(Of Membership.UserAccount, Object).FindByNameAsync
        Throw New NotImplementedException()
    End Function

    Public Function SetTwoFactorEnabledAsync(user As Membership.UserAccount, enabled As Boolean) As Task Implements IUserTwoFactorStore(Of Membership.UserAccount, Object).SetTwoFactorEnabledAsync
        Throw New NotImplementedException()
    End Function

    Public Function GetTwoFactorEnabledAsync(user As Membership.UserAccount) As Task(Of Boolean) Implements IUserTwoFactorStore(Of Membership.UserAccount, Object).GetTwoFactorEnabledAsync

    End Function

    Public Function AddLoginAsync(user As Membership.UserAccount, login As UserLoginInfo) As Task Implements IUserLoginStore(Of Membership.UserAccount, Object).AddLoginAsync
        Throw New NotImplementedException()
    End Function

    Public Function RemoveLoginAsync(user As Membership.UserAccount, login As UserLoginInfo) As Task Implements IUserLoginStore(Of Membership.UserAccount, Object).RemoveLoginAsync
        Throw New NotImplementedException()
    End Function

    Public Function GetLoginsAsync(user As Membership.UserAccount) As Task(Of IList(Of UserLoginInfo)) Implements IUserLoginStore(Of Membership.UserAccount, Object).GetLoginsAsync
        Dim result As IList(Of UserLoginInfo) '= user.Logins.[Select](Function(l) New UserLoginInfo(l.LoginProvider, l.ProviderKey, l.ProviderDisplayName)).ToList()
        Return Task.FromResult(result)
    End Function

    Public Function FindAsync(login As UserLoginInfo) As Task(Of Membership.UserAccount) Implements IUserLoginStore(Of Membership.UserAccount, Object).FindAsync
        Throw New NotImplementedException()
    End Function

    Public Function SetPhoneNumberAsync(user As Membership.UserAccount, phoneNumber As String) As Task Implements IUserPhoneNumberStore(Of Membership.UserAccount, Object).SetPhoneNumberAsync
        Throw New NotImplementedException()
    End Function

    Public Function GetPhoneNumberAsync(user As Membership.UserAccount) As Task(Of String) Implements IUserPhoneNumberStore(Of Membership.UserAccount, Object).GetPhoneNumberAsync
        Dim PhoneNumber As String = user.FullName
        Return Task.FromResult(Of String)(PhoneNumber)
    End Function

    Public Function GetPhoneNumberConfirmedAsync(user As Membership.UserAccount) As Task(Of Boolean) Implements IUserPhoneNumberStore(Of Membership.UserAccount, Object).GetPhoneNumberConfirmedAsync
        Throw New NotImplementedException()
    End Function

    Public Function SetPhoneNumberConfirmedAsync(user As Membership.UserAccount, confirmed As Boolean) As Task Implements IUserPhoneNumberStore(Of Membership.UserAccount, Object).SetPhoneNumberConfirmedAsync
        Throw New NotImplementedException()
    End Function
End Class

