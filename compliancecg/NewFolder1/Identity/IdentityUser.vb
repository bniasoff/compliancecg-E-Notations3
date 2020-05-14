Imports System
Imports System.Collections.Generic
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity

Namespace Microsoft.AspNet.Identity.EntityFramework
    Public Class IdentityUser
        Inherits IdentityUser(Of String, IdentityUserLogin, IdentityUserRole, IdentityUserClaim)
        Implements IUser

        Public Sub New()
            Id = Guid.NewGuid().ToString()
        End Sub

        Public Sub New(ByVal userName As String)
            Me.New()
            userName = userName
        End Sub
    End Class

    Public Class IdentityUser(Of TKey, TLogin As IdentityUserLogin(Of TKey), TRole As IdentityUserRole(Of TKey), TClaim As IdentityUserClaim(Of TKey))
        Implements IUser(Of TKey)

        Public Sub New()
            'Claims = New List(Of TClaim)()
            'Roles = New List(Of TRole)()
            'Logins = New List(Of TLogin)()
        End Sub
        Public Overridable Property Hometown As String
        Public Overridable Property Email As String
        Public Overridable Property EmailConfirmed As Boolean
        Public Overridable Property PasswordHash As String
        Public Overridable Property SecurityStamp As String
        Public Overridable Property PhoneNumber As String
        Public Overridable Property PhoneNumberConfirmed As Boolean
        Public Overridable Property TwoFactorEnabled As Boolean
        Public Overridable Property LockoutEndDateUtc As DateTime?
        Public Overridable Property LockoutEnabled As Boolean
        Public Overridable Property AccessFailedCount As Integer
        Public Overridable Property Roles As ICollection(Of TRole)
        Public Overridable Property Claims As ICollection(Of TClaim)
        Public Overridable Property Logins As ICollection(Of TLogin)
        Public Overridable Property Id As TKey Implements IUser(Of TKey).Id
        Public Overridable Property UserName As String Implements IUser(Of TKey).UserName
        Public Overridable Property LastName As String
        Public Overridable Property FirstName As String
        Public Overridable Property UserID As String

        Public Async Function GenerateUserIdentityAsync(ByVal manager As UserManager(Of IdentityUser)) As Task(Of ClaimsIdentity)
            ' Logger.Log("Membership.UserAccount:GenerateUserIdentityAsync ()")
            'Dim userIdentity = Await manager.CreateIdentityAsync(Me, DefaultAuthenticationTypes.ApplicationCookie)
            Dim userIdentity As New ClaimsIdentity '= Await manager.CreateIdentityAsync(Me, DefaultAuthenticationTypes.ApplicationCookie)

            '  userIdentity.AddClaim()

            Return userIdentity
        End Function

        'Private ReadOnly Property Id As TKey
        '    Get
        '        Throw New NotImplementedException()
        '    End Get
        'End Property

        'Private Property UserName As String
        '    Get
        '        Throw New NotImplementedException()
        '    End Get
        '    Set(value As String)
        '        Throw New NotImplementedException()
        '    End Set
        'End Property
    End Class
End Namespace
