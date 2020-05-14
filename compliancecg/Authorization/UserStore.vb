'Imports System.Threading.Tasks
'Imports Microsoft.AspNet.Identity
'Imports Microsoft.AspNet.Identity.EntityFramework

'Public Class MyUserStore
'    '  Inherits IUserStore(Of ApplicationUser)

'    Implements IUserPasswordStore(Of ApplicationUser), IUserSecurityStampStore(Of ApplicationUser)

'    Private userStore As UserStore(Of IdentityUser) = New UserStore(Of IdentityUser)(New ApplicationDbContext())

'    Public Sub New()
'    End Sub

'    Public Function CreateAsync(ByVal user As ApplicationUser) As Task
'        Dim context = TryCast(userStore.Context, ApplicationDbContext)
'        context.Users.Add(user)
'        context.Configuration.ValidateOnSaveEnabled = False
'        Return context.SaveChangesAsync()
'    End Function

'    Public Function DeleteAsync(ByVal user As ApplicationUser) As Task
'        Dim context = TryCast(userStore.Context, ApplicationDbContext)
'        context.Users.Remove(user)
'        context.Configuration.ValidateOnSaveEnabled = False
'        Return context.SaveChangesAsync()
'    End Function

'    Public Function FindByIdAsync(ByVal userId As String) As Task(Of ApplicationUser)
'        Dim context = TryCast(userStore.Context, ApplicationDbContext)
'        Return context.Users.Where(Function(u) u.Id.ToLower() = userId.ToLower()).FirstOrDefaultAsync()
'    End Function

'    Public Function FindByNameAsync(ByVal userName As String) As Task(Of ApplicationUser)
'        Dim context = TryCast(userStore.Context, ApplicationDbContext)
'        Return context.Users.Where(Function(u) u.UserName.ToLower() = userName.ToLower()).FirstOrDefaultAsync()
'    End Function

'    Public Function UpdateAsync(ByVal user As ApplicationUser) As Task
'        Dim context = TryCast(userStore.Context, ApplicationDbContext)
'        context.Users.Attach(user)
'        context.Entry(user).State = EntityState.Modified
'        context.Configuration.ValidateOnSaveEnabled = False
'        Return context.SaveChangesAsync()
'    End Function

'    Public Sub Dispose()
'        userStore.Dispose()
'    End Sub

'    Public Function GetPasswordHashAsync(ByVal user As ApplicationUser) As Task(Of String)
'        Dim identityUser = ToIdentityUser(user)
'        Dim task = userStore.GetPasswordHashAsync(identityUser)
'        SetApplicationUser(user, identityUser)
'        Return task
'    End Function

'    Public Function HasPasswordAsync(ByVal user As ApplicationUser) As Task(Of Boolean)
'        Dim identityUser = ToIdentityUser(user)
'        Dim task = userStore.HasPasswordAsync(identityUser)
'        SetApplicationUser(user, identityUser)
'        Return task
'    End Function

'    Public Function SetPasswordHashAsync(ByVal user As ApplicationUser, ByVal passwordHash As String) As Task
'        Dim identityUser = ToIdentityUser(user)
'        Dim task = userStore.SetPasswordHashAsync(identityUser, passwordHash)
'        SetApplicationUser(user, identityUser)
'        Return task
'    End Function

'    Public Function GetSecurityStampAsync(ByVal user As ApplicationUser) As Task(Of String)
'        Dim identityUser = ToIdentityUser(user)
'        Dim task = userStore.GetSecurityStampAsync(identityUser)
'        SetApplicationUser(user, identityUser)
'        Return task
'    End Function

'    Public Function SetSecurityStampAsync(ByVal user As ApplicationUser, ByVal stamp As String) As Task
'        Dim identityUser = ToIdentityUser(user)
'        Dim task = userStore.SetSecurityStampAsync(identityUser, stamp)
'        SetApplicationUser(user, identityUser)
'        Return task
'    End Function

'    Private Shared Sub SetApplicationUser(ByVal user As ApplicationUser, ByVal identityUser As IdentityUser)
'        user.PasswordHash = identityUser.PasswordHash
'        user.SecurityStamp = identityUser.SecurityStamp
'        user.Id = identityUser.Id
'        user.UserName = identityUser.UserName
'    End Sub

'    Private Function ToIdentityUser(ByVal user As ApplicationUser) As IdentityUser
'        Return New IdentityUser With {
'            .Id = user.Id,
'            .PasswordHash = user.PasswordHash,
'            .SecurityStamp = user.SecurityStamp,
'            .UserName = user.UserName
'        }
'    End Function

'    Private Function IUserPasswordStore_SetPasswordHashAsync(user As ApplicationUser, passwordHash As String) As Task Implements IUserPasswordStore(Of ApplicationUser, String).SetPasswordHashAsync
'        Throw New NotImplementedException()
'    End Function

'    Private Function IUserPasswordStore_GetPasswordHashAsync(user As ApplicationUser) As Task(Of String) Implements IUserPasswordStore(Of ApplicationUser, String).GetPasswordHashAsync
'        Throw New NotImplementedException()
'    End Function

'    Private Function IUserPasswordStore_HasPasswordAsync(user As ApplicationUser) As Task(Of Boolean) Implements IUserPasswordStore(Of ApplicationUser, String).HasPasswordAsync
'        Throw New NotImplementedException()
'    End Function

'    Private Function IUserStore_CreateAsync(user As ApplicationUser) As Task Implements IUserStore(Of ApplicationUser, String).CreateAsync
'        Throw New NotImplementedException()
'    End Function

'    Private Function IUserStore_UpdateAsync(user As ApplicationUser) As Task Implements IUserStore(Of ApplicationUser, String).UpdateAsync
'        Throw New NotImplementedException()
'    End Function

'    Private Function IUserStore_DeleteAsync(user As ApplicationUser) As Task Implements IUserStore(Of ApplicationUser, String).DeleteAsync
'        Throw New NotImplementedException()
'    End Function

'    Private Function IUserStore_FindByIdAsync(userId As String) As Task(Of ApplicationUser) Implements IUserStore(Of ApplicationUser, String).FindByIdAsync
'        Throw New NotImplementedException()
'    End Function

'    Private Function IUserStore_FindByNameAsync(userName As String) As Task(Of ApplicationUser) Implements IUserStore(Of ApplicationUser, String).FindByNameAsync
'        Throw New NotImplementedException()
'    End Function

'    Private Sub IDisposable_Dispose() Implements IDisposable.Dispose
'        Throw New NotImplementedException()
'    End Sub

'    Private Function IUserSecurityStampStore_SetSecurityStampAsync(user As ApplicationUser, stamp As String) As Task Implements IUserSecurityStampStore(Of ApplicationUser, String).SetSecurityStampAsync
'        Throw New NotImplementedException()
'    End Function

'    Private Function IUserSecurityStampStore_GetSecurityStampAsync(user As ApplicationUser) As Task(Of String) Implements IUserSecurityStampStore(Of ApplicationUser, String).GetSecurityStampAsync
'        Throw New NotImplementedException()
'    End Function
'End Class
