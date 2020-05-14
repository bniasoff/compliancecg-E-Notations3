Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure.Annotations
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework


Public Class UserRole
    Inherits IdentityUserRole(Of Integer)

End Class

Public Class UserClaim
    Inherits IdentityUserClaim(Of Integer)
End Class

Public Class UserLogin
    Inherits IdentityUserLogin(Of Integer)
End Class

Public Class Role
    Inherits IdentityRole(Of Integer, UserRole)

    Public Sub New()
    End Sub

    Public Sub New(ByVal name As String)
        name = name
    End Sub
End Class

Public Class UserStore
    Inherits UserStore(Of ApplicationUser, Role, Integer, UserLogin, UserRole, UserClaim)
    ' Implements IUserPasswordStore(Of ApplicationUser), IUserSecurityStampStore(Of ApplicationUser)
    Private userStore As UserStore(Of IdentityUser) = New UserStore(Of IdentityUser)(New ApplicationDbContext())
    Public Sub New(ByVal context As ApplicationDbContext)
        MyBase.New(context)
    End Sub


    Public Overrides Function CreateAsync(ByVal user As ApplicationUser) As Task

        Dim context = TryCast(UserStore.Context, ApplicationDbContext)
        context.Users.Add(user)
        'context.Configuration.ValidateOnSaveEnabled = False
        Dim Rec = context.SaveChangesAsync()
        Return Rec

    End Function

    Public Overrides Function DeleteAsync(ByVal user As ApplicationUser) As Task
        Dim context = TryCast(userStore.Context, ApplicationDbContext)
        context.Users.Remove(user)
        'context.Configuration.ValidateOnSaveEnabled = False
        Return context.SaveChangesAsync()
    End Function
    Public Overrides Function UpdateAsync(ByVal user As ApplicationUser) As Task
        Dim context = TryCast(userStore.Context, ApplicationDbContext)
        context.Users.Attach(user)
        context.Entry(user).State = EntityState.Modified
        ' context.Configuration.ValidateOnSaveEnabled = False
        Return context.SaveChangesAsync()
    End Function


    Public Overrides Function FindByIdAsync(ByVal userId As Integer) As Task(Of ApplicationUser)
        Dim context = TryCast(userStore.Context, ApplicationDbContext)
        Return context.Users.Where(Function(u) u.Id = userId).FirstOrDefaultAsync()
    End Function

    Public Overrides Function FindByNameAsync(ByVal userName As String) As Task(Of ApplicationUser)
        Dim context = TryCast(userStore.Context, ApplicationDbContext)
        Return context.Users.Where(Function(u) u.UserName.ToLower() = userName.ToLower()).FirstOrDefaultAsync()
    End Function



    'Public Function FindByIdAsync(userId As String) As Task(Of ApplicationUser) Implements IUserStore(Of ApplicationUser, String).FindByIdAsync
    '    Throw New NotImplementedException()
    'End Function

    'Private Function IUserPasswordStore_SetPasswordHashAsync(user As ApplicationUser, passwordHash As String) As Task Implements IUserPasswordStore(Of ApplicationUser, String).SetPasswordHashAsync
    '    Throw New NotImplementedException()
    'End Function

    'Private Function IUserPasswordStore_GetPasswordHashAsync(user As ApplicationUser) As Task(Of String) Implements IUserPasswordStore(Of ApplicationUser, String).GetPasswordHashAsync
    '    Throw New NotImplementedException()
    'End Function

    'Private Function IUserPasswordStore_HasPasswordAsync(user As ApplicationUser) As Task(Of Boolean) Implements IUserPasswordStore(Of ApplicationUser, String).HasPasswordAsync
    '    Throw New NotImplementedException()
    'End Function

    'Private Function IUserStore_CreateAsync(user As ApplicationUser) As Task Implements IUserStore(Of ApplicationUser, String).CreateAsync
    '    Throw New NotImplementedException()
    'End Function

    'Private Function IUserStore_UpdateAsync(user As ApplicationUser) As Task Implements IUserStore(Of ApplicationUser, String).UpdateAsync
    '    Throw New NotImplementedException()
    'End Function

    'Private Function IUserStore_DeleteAsync(user As ApplicationUser) As Task Implements IUserStore(Of ApplicationUser, String).DeleteAsync
    '    Throw New NotImplementedException()
    'End Function

    'Private Function IUserStore_FindByNameAsync(userName As String) As Task(Of ApplicationUser) Implements IUserStore(Of ApplicationUser, String).FindByNameAsync
    '    Throw New NotImplementedException()
    'End Function

    'Private Function IUserSecurityStampStore_SetSecurityStampAsync(user As ApplicationUser, stamp As String) As Task Implements IUserSecurityStampStore(Of ApplicationUser, String).SetSecurityStampAsync
    '    Throw New NotImplementedException()
    'End Function

    'Private Function IUserSecurityStampStore_GetSecurityStampAsync(user As ApplicationUser) As Task(Of String) Implements IUserSecurityStampStore(Of ApplicationUser, String).GetSecurityStampAsync
    '    Throw New NotImplementedException()
    'End Function


    'Public Function GetSecurityStampAsync(ByVal user As ApplicationUser) As Task(Of String)

    '    ' Return Me.GetSecurityStampAsync(user)

    '    Dim identityUser = ToIdentityUser(user)
    '    Dim task = UserStore.GetSecurityStampAsync(identityUser)
    '    SetApplicationUser(user, identityUser)
    '    Return task
    'End Function

    'Public Function SetSecurityStampAsync(ByVal user As ApplicationUser, ByVal stamp As String) As Task
    '    Dim identityUser = ToIdentityUser(user)
    '    Dim task = UserStore.SetSecurityStampAsync(identityUser, stamp)
    '    SetApplicationUser(user, identityUser)
    '    Return task
    'End Function

    'Private Function ToIdentityUser(ByVal user As ApplicationUser) As IdentityUser
    '    Return New IdentityUser With {
    '        .Id = user.Id,
    '        .PasswordHash = user.PasswordHash,
    '        .SecurityStamp = user.SecurityStamp,
    '        .UserName = user.UserName
    '    }
    'End Function
    'Private Shared Sub SetApplicationUser(ByVal user As ApplicationUser, ByVal identityUser As IdentityUser)
    '    user.PasswordHash = identityUser.PasswordHash
    '    user.SecurityStamp = identityUser.SecurityStamp
    '    user.Id = identityUser.Id
    '    user.UserName = identityUser.UserName
    'End Sub
End Class

Public Class RoleStore
    Inherits RoleStore(Of Role, Integer, UserRole)

    Public Sub New(ByVal context As ApplicationDbContext)
        MyBase.New(context)
    End Sub

    'Public Overrides Async Function CreateAsync(role As String) As Task
    '    Me.Roles.
    'End Function

End Class

'<Extension()>
'Public Shared Function AddToRoles(Of TUser As {Class, IUser(Of TKey)}, TKey As IEquatable(Of TKey))(ByVal manager As UserManager(Of TUser, TKey), ByVal userId As TKey, ParamArray roles As String()) As IdentityResult
'    If manager Is Nothing Then
'        Throw New ArgumentNullException("manager")
'    End If

'    Return AsyncHelper.RunSync(Function() manager.AddToRolesAsync(userId, roles))
'End Function

Public Class ApplicationUser
    Inherits IdentityUser(Of Integer, UserLogin, UserRole, UserClaim)

    'Public Property ActiveUntil As DateTime?
    Public Property LastName As String
    Public Property FirstName As String
    Public Property UserID As String
    Public Property LastLoginDate As DateTime?
    Public Property Password2 As String

    Public Async Function GenerateUserIdentityAsync(ByVal manager As ApplicationUserManager) As Task(Of ClaimsIdentity)
        Dim userIdentity = Await manager.CreateIdentityAsync(Me, DefaultAuthenticationTypes.ApplicationCookie)
        Return userIdentity
    End Function
End Class

Public Class ApplicationDbContext
    Inherits IdentityDbContext(Of ApplicationUser, Role, Integer, UserLogin, UserRole, UserClaim)

    'Public Sub New()
    '    MyBase.New("DefaultConnection")
    'End Sub
    Public Sub New()
        MyBase.New(ConnectionStrings.CCGDataConnectionString.ToString)
        ' MyBase.New("CCGDataConnection")
    End Sub



    Public Shared Function Create() As ApplicationDbContext
        Return New ApplicationDbContext()
    End Function

    'Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
    '    If modelBuilder Is Nothing Then
    '        Throw New ArgumentNullException("modelBuilder")
    '    End If

    '    Dim user = modelBuilder.Entity(Of ApplicationUser)().ToTable("AspNetUsers")
    '    user.HasMany(Function(u) u.Roles).WithRequired().HasForeignKey(Function(ur) ur.UserId)
    '    '     user.HasMany(Function(u) u.Claims).WithRequired().HasForeignKey(Function(uc) uc.UserId)
    '    user.HasMany(Function(u) u.Logins).WithRequired().HasForeignKey(Function(ul) ul.UserId)
    '    user.[Property](Function(u) u.UserName).IsRequired().HasMaxLength(256).HasColumnAnnotation("Index", New IndexAnnotation(New IndexAttribute("UserNameIndex") With {.IsUnique = True}))
    '    user.[Property](Function(u) u.Email).HasMaxLength(256)
    '    modelBuilder.Entity(Of UserRole)().HasKey(Function(r) New With {r.UserId, r.RoleId}).ToTable("AspNetUserRoles")
    '    modelBuilder.Entity(Of UserLogin)().HasKey(Function(l) New With {l.LoginProvider, l.ProviderKey, l.UserId}).ToTable("AspNetUserLogins")
    '    'modelBuilder.Entity(Of TUserClaim)().ToTable("AspNetUserClaims")
    '    Dim role = modelBuilder.Entity(Of Role)().ToTable("AspNetRoles")
    '    role.[Property](Function(r) r.Name).IsRequired().HasMaxLength(256).HasColumnAnnotation("Index", New IndexAnnotation(New IndexAttribute("RoleNameIndex") With {.IsUnique = True}))
    '    role.HasMany(Function(r) r.Users).WithRequired().HasForeignKey(Function(ur) ur.RoleId)
    'End Sub
End Class
