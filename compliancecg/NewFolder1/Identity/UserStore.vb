Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Globalization
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Reflection
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports System.Runtime.InteropServices
Imports Microsoft.AspNet.Identity
Imports System.Data.Entity.SqlServer.Utilities
Imports compliancecg.My.Resources

Namespace Microsoft.AspNet.Identity.EntityFramework
    Public Class UserStore(Of TUser As IdentityUser)
        Inherits UserStore(Of TUser, IdentityRole, String, IdentityUserLogin, IdentityUserRole, IdentityUserClaim)
        Implements IUserStore(Of TUser)

        Public Sub New()
            Me.New(New IdentityDbContext())
            If Context Is Nothing Then
                Dim IdentityDbContext As New IdentityDbContext
                Context = IdentityDbContext
            End If

            ' DisposeContext = True
        End Sub

        Public Sub New(ByVal context As DbContext)
            MyBase.New(context)

        End Sub
    End Class

    Public Class UserStore(Of TUser As IdentityUser(Of TKey, TUserLogin, TUserRole, TUserClaim), TRole As IdentityRole(Of TKey, TUserRole), TKey As IEquatable(Of TKey), TUserLogin As {IdentityUserLogin(Of TKey), New}, TUserRole As {IdentityUserRole(Of TKey), New}, TUserClaim As {IdentityUserClaim(Of TKey), New})
        '  Inherits IUserLoginStore(Of TUser, TKey)
        Implements IUserClaimStore(Of TUser, TKey), IUserRoleStore(Of TUser, TKey), IUserPasswordStore(Of TUser, TKey), IUserSecurityStampStore(Of TUser, TKey), IQueryableUserStore(Of TUser, TKey), IUserEmailStore(Of TUser, TKey), IUserPhoneNumberStore(Of TUser, TKey), IUserTwoFactorStore(Of TUser, TKey), IUserLockoutStore(Of TUser, TKey)

        Private ReadOnly _logins As IDbSet(Of TUserLogin)
        Private ReadOnly _roleStore As EntityStore(Of TRole)
        Private ReadOnly _userClaims As IDbSet(Of TUserClaim)
        Private ReadOnly _userRoles As IDbSet(Of TUserRole)
        Private _disposed As Boolean
        Private _userStore As EntityStore(Of TUser)

        Public Sub New(ByVal context As DbContext)
            If context Is Nothing Then
                Throw New ArgumentNullException("context")
            End If

            context = context
            AutoSaveChanges = True
            _userStore = New EntityStore(Of TUser)(context)
            _roleStore = New EntityStore(Of TRole)(context)
            _logins = context.[Set](Of TUserLogin)()
            _userClaims = context.[Set](Of TUserClaim)()
            _userRoles = context.[Set](Of TUserRole)()
        End Sub

        Public Property _Context As DbContext
        Public Property DisposeContext As Boolean
        Public Property AutoSaveChanges As Boolean



        Public Property Context As DbContext
            Get
                If _Context Is Nothing Then
                    Dim IdentityDbContext As New IdentityDbContext
                    _Context = IdentityDbContext
                End If
                Return _Context
            End Get
            Set(value As DbContext)
                If Context Is Nothing Then
                    Dim IdentityDbContext As New IdentityDbContext
                    _Context = IdentityDbContext
                End If
            End Set
        End Property
        Public ReadOnly Property Users As IQueryable(Of TUser)
            Get
                Return _userStore.EntitySet
            End Get
        End Property

        Private ReadOnly Property IQueryableUserStore_Users As IQueryable(Of TUser) Implements IQueryableUserStore(Of TUser, TKey).Users
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public Overridable Async Function GetClaimsAsync(ByVal user As TUser) As Task(Of IList(Of Claim))
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            Await EnsureClaimsLoaded(user).WithCurrentCulture()
            Return user.Claims.[Select](Function(c) New Claim(c.ClaimType, c.ClaimValue)).ToList()
        End Function

        Public Overridable Function AddClaimAsync(ByVal user As TUser, ByVal claim As Claim) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            If claim Is Nothing Then
                Throw New ArgumentNullException("claim")
            End If

            _userClaims.Add(New TUserClaim With {
                .UserId = user.Id,
                .ClaimType = claim.Type,
                .ClaimValue = claim.Value
            })
            Return Task.FromResult(0)
        End Function

        Public Overridable Async Function RemoveClaimAsync(ByVal user As TUser, ByVal claim As Claim) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            If claim Is Nothing Then
                Throw New ArgumentNullException("claim")
            End If

            Dim claims As IEnumerable(Of TUserClaim)
            Dim claimValue = claim.Value
            Dim claimType = claim.Type

            If AreClaimsLoaded(user) Then
                claims = user.Claims.Where(Function(uc) uc.ClaimValue = claimValue AndAlso uc.ClaimType = claimType).ToList()
            Else
                Dim userId = user.Id
                claims = Await _userClaims.Where(Function(uc) uc.ClaimValue = claimValue AndAlso uc.ClaimType = claimType AndAlso uc.UserId.Equals(userId)).ToListAsync().WithCurrentCulture()
            End If

            For Each c In claims
                _userClaims.Remove(c)
            Next
        End Function

        Public Overridable Function GetEmailConfirmedAsync(ByVal user As TUser) As Task(Of Boolean)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            Return Task.FromResult(user.EmailConfirmed)
        End Function

        Public Overridable Function SetEmailConfirmedAsync(ByVal user As TUser, ByVal confirmed As Boolean) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            user.EmailConfirmed = confirmed
            Return Task.FromResult(0)
        End Function

        Public Overridable Function SetEmailAsync(ByVal user As TUser, ByVal email As String) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            user.Email = email
            Return Task.FromResult(0)
        End Function

        Public Overridable Function GetEmailAsync(ByVal user As TUser) As Task(Of String)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            Return Task.FromResult(user.Email)
        End Function

        Public Overridable Function FindByEmailAsync(ByVal email As String) As Task(Of TUser)
            ThrowIfDisposed()
            Return GetUserAggregateAsync(Function(u) u.Email.ToUpper() = email.ToUpper())
        End Function

        Public Overridable Function GetLockoutEndDateAsync(ByVal user As TUser) As Task(Of DateTimeOffset)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            Return Task.FromResult(If(user.LockoutEndDateUtc.HasValue, New DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc)), New DateTimeOffset()))
        End Function

        Public Overridable Function SetLockoutEndDateAsync(ByVal user As TUser, ByVal lockoutEnd As DateTimeOffset) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            user.LockoutEndDateUtc = If(lockoutEnd = DateTimeOffset.MinValue, CType(Nothing, DateTime?), lockoutEnd.UtcDateTime)
            Return Task.FromResult(0)
        End Function

        Public Overridable Function IncrementAccessFailedCountAsync(ByVal user As TUser) As Task(Of Integer)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            user.AccessFailedCount += 1
            Return Task.FromResult(user.AccessFailedCount)
        End Function

        Public Overridable Function ResetAccessFailedCountAsync(ByVal user As TUser) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            user.AccessFailedCount = 0
            Return Task.FromResult(0)
        End Function

        Public Overridable Function GetAccessFailedCountAsync(ByVal user As TUser) As Task(Of Integer)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            Return Task.FromResult(user.AccessFailedCount)
        End Function

        Public Overridable Function GetLockoutEnabledAsync(ByVal user As TUser) As Task(Of Boolean)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            Return Task.FromResult(user.LockoutEnabled)
        End Function

        Public Overridable Function SetLockoutEnabledAsync(ByVal user As TUser, ByVal enabled As Boolean) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            user.LockoutEnabled = enabled
            Return Task.FromResult(0)
        End Function

        Public Overridable Function FindByIdAsync(ByVal userId As TKey) As Task(Of TUser)
            ThrowIfDisposed()
            Return GetUserAggregateAsync(Function(u) u.Id.Equals(userId))
        End Function

        Public Overridable Function FindByNameAsync(ByVal userName As String) As Task(Of TUser)
            ThrowIfDisposed()
            Return GetUserAggregateAsync(Function(u) u.UserName.ToUpper() = userName.ToUpper())
        End Function

        Public Overridable Async Function CreateAsync(ByVal user As TUser) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            _userStore.Create(user)
            Await SaveChanges().WithCurrentCulture()
        End Function

        Public Overridable Async Function DeleteAsync(ByVal user As TUser) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            _userStore.Delete(user)
            Await SaveChanges().WithCurrentCulture()
        End Function

        Public Overridable Async Function UpdateAsync(ByVal user As TUser) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            _userStore.Update(user)
            Await SaveChanges().WithCurrentCulture()
        End Function

        Public Sub Dispose()
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Public Overridable Async Function FindAsync(ByVal login As UserLoginInfo) As Task(Of TUser)
            ThrowIfDisposed()

            If login Is Nothing Then
                Throw New ArgumentNullException("login")
            End If

            Dim provider = login.LoginProvider
            Dim key = login.ProviderKey
            Dim userLogin = Await _logins.FirstOrDefaultAsync(Function(l) l.LoginProvider = provider AndAlso l.ProviderKey = key).WithCurrentCulture()

            If userLogin IsNot Nothing Then
                Dim userId = userLogin.UserId
                Return Await GetUserAggregateAsync(Function(u) u.Id.Equals(userId)).WithCurrentCulture()
            End If

            Return Nothing
        End Function

        Public Overridable Function AddLoginAsync(ByVal user As TUser, ByVal login As UserLoginInfo) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            If login Is Nothing Then
                Throw New ArgumentNullException("login")
            End If

            _logins.Add(New TUserLogin With {
                .UserId = user.Id,
                .ProviderKey = login.ProviderKey,
                .LoginProvider = login.LoginProvider
            })
            Return Task.FromResult(0)
        End Function

        Public Overridable Async Function RemoveLoginAsync(ByVal user As TUser, ByVal login As UserLoginInfo) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            If login Is Nothing Then
                Throw New ArgumentNullException("login")
            End If

            Dim entry As TUserLogin
            Dim provider = login.LoginProvider
            Dim key = login.ProviderKey

            If AreLoginsLoaded(user) Then
                entry = user.Logins.SingleOrDefault(Function(ul) ul.LoginProvider = provider AndAlso ul.ProviderKey = key)
            Else
                Dim userId = user.Id
                entry = Await _logins.SingleOrDefaultAsync(Function(ul) ul.LoginProvider = provider AndAlso ul.ProviderKey = key AndAlso ul.UserId.Equals(userId)).WithCurrentCulture()
            End If

            If entry IsNot Nothing Then
                _logins.Remove(entry)
            End If
        End Function

        Public Overridable Async Function GetLoginsAsync(ByVal user As TUser) As Task(Of IList(Of UserLoginInfo))
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            Await EnsureLoginsLoaded(user).WithCurrentCulture()
            Return user.Logins.[Select](Function(l) New UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList()
        End Function

        Public Overridable Function SetPasswordHashAsync(ByVal user As TUser, ByVal passwordHash As String) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            user.PasswordHash = passwordHash
            Return Task.FromResult(0)
        End Function

        Public Overridable Function GetPasswordHashAsync(ByVal user As TUser) As Task(Of String)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            Return Task.FromResult(user.PasswordHash)
        End Function

        Public Overridable Function HasPasswordAsync(ByVal user As TUser) As Task(Of Boolean)
            Return Task.FromResult(user.PasswordHash IsNot Nothing)
        End Function

        Public Overridable Function SetPhoneNumberAsync(ByVal user As TUser, ByVal phoneNumber As String) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            user.PhoneNumber = phoneNumber
            Return Task.FromResult(0)
        End Function

        Public Overridable Function GetPhoneNumberAsync(ByVal user As TUser) As Task(Of String)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            Return Task.FromResult(user.PhoneNumber)
        End Function

        Public Overridable Function GetPhoneNumberConfirmedAsync(ByVal user As TUser) As Task(Of Boolean)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            Return Task.FromResult(user.PhoneNumberConfirmed)
        End Function

        Public Overridable Function SetPhoneNumberConfirmedAsync(ByVal user As TUser, ByVal confirmed As Boolean) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            user.PhoneNumberConfirmed = confirmed
            Return Task.FromResult(0)
        End Function

        Public Overridable Async Function AddToRoleAsync(ByVal user As TUser, ByVal roleName As String) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            If String.IsNullOrWhiteSpace(roleName) Then
                Throw New ArgumentException(IdentityResources.ValueCannotBeNullOrEmpty, "roleName")
            End If

            Dim roleEntity = Await _roleStore.DbEntitySet.SingleOrDefaultAsync(Function(r) r.Name.ToUpper() = roleName.ToUpper()).WithCurrentCulture()

            If roleEntity Is Nothing Then
                Throw New InvalidOperationException(String.Format(CultureInfo.CurrentCulture, IdentityResources.RoleNotFound, roleName))
            End If

            Dim ur = New TUserRole With {
                .UserId = user.Id,
                .RoleId = roleEntity.Id
            }
            _userRoles.Add(ur)
        End Function

        Public Overridable Async Function RemoveFromRoleAsync(ByVal user As TUser, ByVal roleName As String) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            If String.IsNullOrWhiteSpace(roleName) Then
                Throw New ArgumentException(IdentityResources.ValueCannotBeNullOrEmpty, "roleName")
            End If

            Dim roleEntity = Await _roleStore.DbEntitySet.SingleOrDefaultAsync(Function(r) r.Name.ToUpper() = roleName.ToUpper()).WithCurrentCulture()

            If roleEntity IsNot Nothing Then
                Dim roleId = roleEntity.Id
                Dim userId = user.Id
                Dim userRole = Await _userRoles.FirstOrDefaultAsync(Function(r) roleId.Equals(r.RoleId) AndAlso r.UserId.Equals(userId)).WithCurrentCulture()

                If userRole IsNot Nothing Then
                    _userRoles.Remove(userRole)
                End If
            End If
        End Function

        Public Overridable Async Function GetRolesAsync(ByVal user As TUser) As Task(Of IList(Of String))
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            Dim userId = user.Id
            Dim query = From userRole In _userRoles Where userRole.UserId.Equals(userId) Join role In _roleStore.DbEntitySet On userRole.RoleId Equals role.Id Select role.Name
            Return Await query.ToListAsync().WithCurrentCulture()
        End Function

        Public Overridable Async Function IsInRoleAsync(ByVal user As TUser, ByVal roleName As String) As Task(Of Boolean)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            If String.IsNullOrWhiteSpace(roleName) Then
                Throw New ArgumentException(IdentityResources.ValueCannotBeNullOrEmpty, "roleName")
            End If

            Dim role = Await _roleStore.DbEntitySet.SingleOrDefaultAsync(Function(r) r.Name.ToUpper() = roleName.ToUpper()).WithCurrentCulture()

            If role IsNot Nothing Then
                Dim userId = user.Id
                Dim roleId = role.Id
                Return Await _userRoles.AnyAsync(Function(ur) ur.RoleId.Equals(roleId) AndAlso ur.UserId.Equals(userId)).WithCurrentCulture()
            End If

            Return False
        End Function

        Public Overridable Function SetSecurityStampAsync(ByVal user As TUser, ByVal stamp As String) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            user.SecurityStamp = stamp
            Return Task.FromResult(0)
        End Function

        Public Overridable Function GetSecurityStampAsync(ByVal user As TUser) As Task(Of String)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            Return Task.FromResult(user.SecurityStamp)
        End Function

        Public Overridable Function SetTwoFactorEnabledAsync(ByVal user As TUser, ByVal enabled As Boolean) As Task
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            user.TwoFactorEnabled = enabled
            Return Task.FromResult(0)
        End Function

        Public Overridable Function GetTwoFactorEnabledAsync(ByVal user As TUser) As Task(Of Boolean)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            Return Task.FromResult(user.TwoFactorEnabled)
        End Function

        Private Async Function SaveChanges() As Task
            If AutoSaveChanges Then
                Await Context.SaveChangesAsync().WithCurrentCulture()
            End If
        End Function

        Private Function AreClaimsLoaded(ByVal user As TUser) As Boolean
            Dim Claims = Context.Entry(user).Collection(Function(u) u.Claims)
            Return Claims.IsLoaded
        End Function

        Private Async Function EnsureClaimsLoaded(ByVal user As TUser) As Task
            If Not AreClaimsLoaded(user) Then
                Dim userId = user.Id
                Await _userClaims.Where(Function(uc) uc.UserId.Equals(userId)).LoadAsync().WithCurrentCulture()
                Context.Entry(user).Collection(Function(u) u.Claims).IsLoaded = True
            End If
        End Function

        Private Async Function EnsureRolesLoaded(ByVal user As TUser) As Task
            If Not Context.Entry(user).Collection(Function(u) u.Roles).IsLoaded Then
                Dim userId = user.Id
                Await _userRoles.Where(Function(uc) uc.UserId.Equals(userId)).LoadAsync().WithCurrentCulture()
                Context.Entry(user).Collection(Function(u) u.Roles).IsLoaded = True
            End If
        End Function

        Private Function AreLoginsLoaded(ByVal user As TUser) As Boolean
            Return Context.Entry(user).Collection(Function(u) u.Logins).IsLoaded
        End Function

        Private Async Function EnsureLoginsLoaded(ByVal user As TUser) As Task
            If Not AreLoginsLoaded(user) Then
                Dim userId = user.Id
                Await _logins.Where(Function(uc) uc.UserId.Equals(userId)).LoadAsync().WithCurrentCulture()
                Context.Entry(user).Collection(Function(u) u.Logins).IsLoaded = True
            End If
        End Function

        Protected Overridable Async Function GetUserAggregateAsync(ByVal filter As Expression(Of Func(Of TUser, Boolean))) As Task(Of TUser)
            'Dim id As TKey
            Dim user As TUser

            '  Dim user As IdentityUser
            'If FindByIdFilterParser.TryMatchAndGetId(filter, id) Then
            '    user = Await _userStore.GetByIdAsync(id).WithCurrentCulture()
            'Else
            '    user = Await Users.FirstOrDefaultAsync(filter).WithCurrentCulture()
            'End If
            user = Await Users.FirstOrDefaultAsync(filter).WithCurrentCulture()
            'If user IsNot Nothing Then
            '    Await EnsureClaimsLoaded(user).WithCurrentCulture()
            '    Await EnsureLoginsLoaded(user).WithCurrentCulture()
            '    Await EnsureRolesLoaded(user).WithCurrentCulture()
            'End If

            Return user
        End Function

        Private Sub ThrowIfDisposed()
            If _disposed Then
                Throw New ObjectDisposedException([GetType]().Name)
            End If
        End Sub

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If DisposeContext AndAlso disposing AndAlso Context IsNot Nothing Then
                Context.Dispose()
            End If

            _disposed = True
            Context = Nothing
            _userStore = Nothing
        End Sub

        Private Function IUserClaimStore_GetClaimsAsync(user As TUser) As Task(Of IList(Of Claim)) Implements IUserClaimStore(Of TUser, TKey).GetClaimsAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserClaimStore_AddClaimAsync(user As TUser, claim As Claim) As Task Implements IUserClaimStore(Of TUser, TKey).AddClaimAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserClaimStore_RemoveClaimAsync(user As TUser, claim As Claim) As Task Implements IUserClaimStore(Of TUser, TKey).RemoveClaimAsync
            Throw New NotImplementedException()
        End Function

        Private Async Function IUserStore_CreateAsync(user As TUser) As Task Implements IUserStore(Of TUser, TKey).CreateAsync
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            _userStore.Create(user)
            Await SaveChanges().WithCurrentCulture()

            '  Throw New NotImplementedException()
        End Function

        Private Function IUserStore_UpdateAsync(user As TUser) As Task Implements IUserStore(Of TUser, TKey).UpdateAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserStore_DeleteAsync(user As TUser) As Task Implements IUserStore(Of TUser, TKey).DeleteAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserStore_FindByIdAsync(userId As TKey) As Task(Of TUser) Implements IUserStore(Of TUser, TKey).FindByIdAsync
            'Throw New NotImplementedException()
        End Function

        Private Function IUserStore_FindByNameAsync(userName As String) As Task(Of TUser) Implements IUserStore(Of TUser, TKey).FindByNameAsync
            ThrowIfDisposed()
            Return GetUserAggregateAsync(Function(u) u.UserName.ToUpper() = userName.ToUpper())
            'Throw New NotImplementedException()
        End Function

        Private Sub IDisposable_Dispose() Implements IDisposable.Dispose
            Throw New NotImplementedException()
        End Sub

        Private Function IUserRoleStore_AddToRoleAsync(user As TUser, roleName As String) As Task Implements IUserRoleStore(Of TUser, TKey).AddToRoleAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserRoleStore_RemoveFromRoleAsync(user As TUser, roleName As String) As Task Implements IUserRoleStore(Of TUser, TKey).RemoveFromRoleAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserRoleStore_GetRolesAsync(user As TUser) As Task(Of IList(Of String)) Implements IUserRoleStore(Of TUser, TKey).GetRolesAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserRoleStore_IsInRoleAsync(user As TUser, roleName As String) As Task(Of Boolean) Implements IUserRoleStore(Of TUser, TKey).IsInRoleAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserPasswordStore_SetPasswordHashAsync(user As TUser, passwordHash As String) As Task Implements IUserPasswordStore(Of TUser, TKey).SetPasswordHashAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserPasswordStore_GetPasswordHashAsync(user As TUser) As Task(Of String) Implements IUserPasswordStore(Of TUser, TKey).GetPasswordHashAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserPasswordStore_HasPasswordAsync(user As TUser) As Task(Of Boolean) Implements IUserPasswordStore(Of TUser, TKey).HasPasswordAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserSecurityStampStore_SetSecurityStampAsync(user As TUser, stamp As String) As Task Implements IUserSecurityStampStore(Of TUser, TKey).SetSecurityStampAsync

            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException("user")
            End If

            user.SecurityStamp = stamp
            Return Task.FromResult(0)


            '   Throw New NotImplementedException()
        End Function

        Private Function IUserSecurityStampStore_GetSecurityStampAsync(user As TUser) As Task(Of String) Implements IUserSecurityStampStore(Of TUser, TKey).GetSecurityStampAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserEmailStore_SetEmailAsync(user As TUser, email As String) As Task Implements IUserEmailStore(Of TUser, TKey).SetEmailAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserEmailStore_GetEmailAsync(user As TUser) As Task(Of String) Implements IUserEmailStore(Of TUser, TKey).GetEmailAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserEmailStore_GetEmailConfirmedAsync(user As TUser) As Task(Of Boolean) Implements IUserEmailStore(Of TUser, TKey).GetEmailConfirmedAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserEmailStore_SetEmailConfirmedAsync(user As TUser, confirmed As Boolean) As Task Implements IUserEmailStore(Of TUser, TKey).SetEmailConfirmedAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserEmailStore_FindByEmailAsync(email As String) As Task(Of TUser) Implements IUserEmailStore(Of TUser, TKey).FindByEmailAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserPhoneNumberStore_SetPhoneNumberAsync(user As TUser, phoneNumber As String) As Task Implements IUserPhoneNumberStore(Of TUser, TKey).SetPhoneNumberAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserPhoneNumberStore_GetPhoneNumberAsync(user As TUser) As Task(Of String) Implements IUserPhoneNumberStore(Of TUser, TKey).GetPhoneNumberAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserPhoneNumberStore_GetPhoneNumberConfirmedAsync(user As TUser) As Task(Of Boolean) Implements IUserPhoneNumberStore(Of TUser, TKey).GetPhoneNumberConfirmedAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserPhoneNumberStore_SetPhoneNumberConfirmedAsync(user As TUser, confirmed As Boolean) As Task Implements IUserPhoneNumberStore(Of TUser, TKey).SetPhoneNumberConfirmedAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserTwoFactorStore_SetTwoFactorEnabledAsync(user As TUser, enabled As Boolean) As Task Implements IUserTwoFactorStore(Of TUser, TKey).SetTwoFactorEnabledAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserTwoFactorStore_GetTwoFactorEnabledAsync(user As TUser) As Task(Of Boolean) Implements IUserTwoFactorStore(Of TUser, TKey).GetTwoFactorEnabledAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserLockoutStore_GetLockoutEndDateAsync(user As TUser) As Task(Of DateTimeOffset) Implements IUserLockoutStore(Of TUser, TKey).GetLockoutEndDateAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserLockoutStore_SetLockoutEndDateAsync(user As TUser, lockoutEnd As DateTimeOffset) As Task Implements IUserLockoutStore(Of TUser, TKey).SetLockoutEndDateAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserLockoutStore_IncrementAccessFailedCountAsync(user As TUser) As Task(Of Integer) Implements IUserLockoutStore(Of TUser, TKey).IncrementAccessFailedCountAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserLockoutStore_ResetAccessFailedCountAsync(user As TUser) As Task Implements IUserLockoutStore(Of TUser, TKey).ResetAccessFailedCountAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserLockoutStore_GetAccessFailedCountAsync(user As TUser) As Task(Of Integer) Implements IUserLockoutStore(Of TUser, TKey).GetAccessFailedCountAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserLockoutStore_GetLockoutEnabledAsync(user As TUser) As Task(Of Boolean) Implements IUserLockoutStore(Of TUser, TKey).GetLockoutEnabledAsync
            Throw New NotImplementedException()
        End Function

        Private Function IUserLockoutStore_SetLockoutEnabledAsync(user As TUser, enabled As Boolean) As Task Implements IUserLockoutStore(Of TUser, TKey).SetLockoutEnabledAsync
            Throw New NotImplementedException()
        End Function

        Public Class FindByIdFilterParser
            Private Shared ReadOnly Predicate As Expression(Of Func(Of TUser, Boolean)) = Function(u) u.Id.Equals(Nothing)
            Private Shared ReadOnly EqualsMethodInfo As MethodInfo = (CType(Predicate.Body, MethodCallExpression)).Method
            Private Shared ReadOnly UserIdMemberInfo As MemberInfo = (CType((CType(Predicate.Body, MethodCallExpression)).Object, MemberExpression)).Member

            Friend Function TryMatchAndGetId(ByVal filter As Expression(Of Func(Of TUser, Boolean)), <Out> ByRef id As TKey) As Boolean
                id = Nothing

                If filter.Body.NodeType <> ExpressionType.[Call] Then
                    Return False
                End If

                Dim callExpression = CType(filter.Body, MethodCallExpression)

                If callExpression.Method <> EqualsMethodInfo Then
                    Return False
                End If

                If callExpression.Object Is Nothing OrElse callExpression.Object.NodeType <> ExpressionType.MemberAccess OrElse (CType(callExpression.Object, MemberExpression)).Member <> UserIdMemberInfo Then
                    Return False
                End If

                If callExpression.Arguments.Count <> 1 Then
                    Return False
                End If

                Dim fieldAccess As MemberExpression

                If callExpression.Arguments(0).NodeType = ExpressionType.Convert Then
                    Dim convert = CType(callExpression.Arguments(0), UnaryExpression)

                    If convert.Operand.NodeType <> ExpressionType.MemberAccess Then
                        Return False
                    End If

                    fieldAccess = CType(convert.Operand, MemberExpression)
                ElseIf callExpression.Arguments(0).NodeType = ExpressionType.MemberAccess Then
                    fieldAccess = CType(callExpression.Arguments(0), MemberExpression)
                Else
                    Return False
                End If

                If fieldAccess.Member.MemberType <> MemberTypes.Field OrElse fieldAccess.Expression.NodeType <> ExpressionType.Constant Then
                    Return False
                End If

                Dim fieldInfo = CType(fieldAccess.Member, FieldInfo)
                Dim closure = (CType(fieldAccess.Expression, ConstantExpression)).Value
                id = CType(fieldInfo.GetValue(closure), TKey)
                Return True
            End Function
        End Class
    End Class
End Namespace
