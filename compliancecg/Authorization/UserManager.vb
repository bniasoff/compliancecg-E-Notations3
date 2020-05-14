Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Security.Claims
Imports System.Security.Cryptography
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Identity.Core
Imports Microsoft.Extensions.Logging
Imports Microsoft.Extensions.Options

Namespace Microsoft.AspNetCore.Identity
    Public Class UserManager(Of TUser As Class)
        Implements IDisposable

        Public Const ResetPasswordTokenPurpose As String = "ResetPassword"
        Public Const ChangePhoneNumberTokenPurpose As String = "ChangePhoneNumber"
        Public Const ConfirmEmailTokenPurpose As String = "EmailConfirmation"
        Private ReadOnly _tokenProviders As Dictionary(Of String, IUserTwoFactorTokenProvider(Of TUser)) = New Dictionary(Of String, IUserTwoFactorTokenProvider(Of TUser))()
        Private _defaultLockout As TimeSpan = TimeSpan.Zero
        Private _disposed As Boolean
        Private Shared ReadOnly _rng As RandomNumberGenerator = RandomNumberGenerator.Create()
        Private _services As IServiceProvider

        Protected Overridable ReadOnly Property CancellationToken As CancellationToken
            Get
                Return CancellationToken.None
            End Get
        End Property

        Public Sub New(ByVal store As IUserStore(Of TUser), ByVal optionsAccessor As IOptions(Of IdentityOptions), ByVal passwordHasher As IPasswordHasher(Of TUser), ByVal userValidators As IEnumerable(Of IUserValidator(Of TUser)), ByVal passwordValidators As IEnumerable(Of IPasswordValidator(Of TUser)), ByVal keyNormalizer As ILookupNormalizer, ByVal errors As IdentityErrorDescriber, ByVal services As IServiceProvider, ByVal logger As ILogger(Of UserManager(Of TUser)))
            If store Is Nothing Then
                Throw New ArgumentNullException(NameOf(store))
            End If

            store = store
            Options = If(optionsAccessor?.Value, New IdentityOptions())
            passwordHasher = passwordHasher
            keyNormalizer = keyNormalizer
            ErrorDescriber = errors
            logger = logger

            If userValidators IsNot Nothing Then

                For Each v In userValidators
                    userValidators.Add(v)
                Next
            End If

            If passwordValidators IsNot Nothing Then

                For Each v In passwordValidators
                    passwordValidators.Add(v)
                Next
            End If

            _services = services

            If services IsNot Nothing Then

                For Each providerName In Options.Tokens.ProviderMap.Keys
                    Dim description = Options.Tokens.ProviderMap(providerName)
                    Dim provider = TryCast((If(description.ProviderInstance, services.GetRequiredService(description.ProviderType))), IUserTwoFactorTokenProvider(Of TUser))

                    If provider IsNot Nothing Then
                        RegisterTokenProvider(providerName, provider)
                    End If
                Next
            End If

            If Options.Stores.ProtectPersonalData Then

                If Not (TypeOf store Is IProtectedUserStore(Of TUser)) Then
                    Throw New InvalidOperationException(Resources.StoreNotIProtectedUserStore)
                End If

                If services.GetService(Of ILookupProtector)() Is Nothing Then
                    Throw New InvalidOperationException(Resources.NoPersonalDataProtector)
                End If
            End If
        End Sub

        Protected Friend Property Store As IUserStore(Of TUser)
        Public Overridable Property Logger As ILogger
        Public Property PasswordHasher As IPasswordHasher(Of TUser)
        Public ReadOnly Property UserValidators As IList(Of IUserValidator(Of TUser)) = New List(Of IUserValidator(Of TUser))()
        Public ReadOnly Property PasswordValidators As IList(Of IPasswordValidator(Of TUser)) = New List(Of IPasswordValidator(Of TUser))()
        Public Property KeyNormalizer As ILookupNormalizer
        Public Property ErrorDescriber As IdentityErrorDescriber
        Public Property Options As IdentityOptions

        Public Overridable ReadOnly Property SupportsUserAuthenticationTokens As Boolean
            Get
                ThrowIfDisposed()
                Return TypeOf Store Is IUserAuthenticationTokenStore(Of TUser)
            End Get
        End Property

        Public Overridable ReadOnly Property SupportsUserAuthenticatorKey As Boolean
            Get
                ThrowIfDisposed()
                Return TypeOf Store Is IUserAuthenticatorKeyStore(Of TUser)
            End Get
        End Property

        Public Overridable ReadOnly Property SupportsUserTwoFactorRecoveryCodes As Boolean
            Get
                ThrowIfDisposed()
                Return TypeOf Store Is IUserTwoFactorRecoveryCodeStore(Of TUser)
            End Get
        End Property

        Public Overridable ReadOnly Property SupportsUserTwoFactor As Boolean
            Get
                ThrowIfDisposed()
                Return TypeOf Store Is IUserTwoFactorStore(Of TUser)
            End Get
        End Property

        Public Overridable ReadOnly Property SupportsUserPassword As Boolean
            Get
                ThrowIfDisposed()
                Return TypeOf Store Is IUserPasswordStore(Of TUser)
            End Get
        End Property

        Public Overridable ReadOnly Property SupportsUserSecurityStamp As Boolean
            Get
                ThrowIfDisposed()
                Return TypeOf Store Is IUserSecurityStampStore(Of TUser)
            End Get
        End Property

        Public Overridable ReadOnly Property SupportsUserRole As Boolean
            Get
                ThrowIfDisposed()
                Return TypeOf Store Is IUserRoleStore(Of TUser)
            End Get
        End Property

        Public Overridable ReadOnly Property SupportsUserLogin As Boolean
            Get
                ThrowIfDisposed()
                Return TypeOf Store Is IUserLoginStore(Of TUser)
            End Get
        End Property

        Public Overridable ReadOnly Property SupportsUserEmail As Boolean
            Get
                ThrowIfDisposed()
                Return TypeOf Store Is IUserEmailStore(Of TUser)
            End Get
        End Property

        Public Overridable ReadOnly Property SupportsUserPhoneNumber As Boolean
            Get
                ThrowIfDisposed()
                Return TypeOf Store Is IUserPhoneNumberStore(Of TUser)
            End Get
        End Property

        Public Overridable ReadOnly Property SupportsUserClaim As Boolean
            Get
                ThrowIfDisposed()
                Return TypeOf Store Is IUserClaimStore(Of TUser)
            End Get
        End Property

        Public Overridable ReadOnly Property SupportsUserLockout As Boolean
            Get
                ThrowIfDisposed()
                Return TypeOf Store Is IUserLockoutStore(Of TUser)
            End Get
        End Property

        Public Overridable ReadOnly Property SupportsQueryableUsers As Boolean
            Get
                ThrowIfDisposed()
                Return TypeOf Store Is IQueryableUserStore(Of TUser)
            End Get
        End Property

        Public Overridable ReadOnly Property Users As IQueryable(Of TUser)
            Get
                Dim queryableStore = TryCast(Store, IQueryableUserStore(Of TUser))

                If queryableStore Is Nothing Then
                    Throw New NotSupportedException(Resources.StoreNotIQueryableUserStore)
                End If

                Return queryableStore.Users
            End Get
        End Property

        Public Sub Dispose()
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Public Overridable Function GetUserName(ByVal principal As ClaimsPrincipal) As String
            If principal Is Nothing Then
                Throw New ArgumentNullException(NameOf(principal))
            End If

            Return principal.FindFirstValue(Options.ClaimsIdentity.UserNameClaimType)
        End Function

        Public Overridable Function GetUserId(ByVal principal As ClaimsPrincipal) As String
            If principal Is Nothing Then
                Throw New ArgumentNullException(NameOf(principal))
            End If

            Return principal.FindFirstValue(Options.ClaimsIdentity.UserIdClaimType)
        End Function

        Public Overridable Function GetUserAsync(ByVal principal As ClaimsPrincipal) As Task(Of TUser)
            If principal Is Nothing Then
                Throw New ArgumentNullException(NameOf(principal))
            End If

            Dim id = GetUserId(principal)
            Return If(id Is Nothing, Task.FromResult(Of TUser)(Nothing), FindByIdAsync(id))
        End Function

        Public Overridable Function GenerateConcurrencyStampAsync(ByVal user As TUser) As Task(Of String)
            Return Task.FromResult(Guid.NewGuid().ToString())
        End Function

        Public Overridable Async Function CreateAsync(ByVal user As TUser) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Await UpdateSecurityStampInternal(user)
            Dim result = Await ValidateUserAsync(user)

            If Not result.Succeeded Then
                Return result
            End If

            If Options.Lockout.AllowedForNewUsers AndAlso SupportsUserLockout Then
                Await GetUserLockoutStore().SetLockoutEnabledAsync(user, True, CancellationToken)
            End If

            Await UpdateNormalizedUserNameAsync(user)
            Await UpdateNormalizedEmailAsync(user)
            Return Await Store.CreateAsync(user, CancellationToken)
        End Function

        Public Overridable Function UpdateAsync(ByVal user As TUser) As Task(Of IdentityResult)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return UpdateUserAsync(user)
        End Function

        Public Overridable Function DeleteAsync(ByVal user As TUser) As Task(Of IdentityResult)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return Store.DeleteAsync(user, CancellationToken)
        End Function

        Public Overridable Function FindByIdAsync(ByVal userId As String) As Task(Of TUser)
            ThrowIfDisposed()
            Return Store.FindByIdAsync(userId, CancellationToken)
        End Function

        Public Overridable Async Function FindByNameAsync(ByVal userName As String) As Task(Of TUser)
            ThrowIfDisposed()

            If userName Is Nothing Then
                Throw New ArgumentNullException(NameOf(userName))
            End If

            userName = NormalizeKey(userName)
            Dim user = Await Store.FindByNameAsync(userName, CancellationToken)

            If user Is Nothing AndAlso Options.Stores.ProtectPersonalData Then
                Dim keyRing = _services.GetService(Of ILookupProtectorKeyRing)()
                Dim protector = _services.GetService(Of ILookupProtector)()

                If keyRing IsNot Nothing AndAlso protector IsNot Nothing Then

                    For Each key In keyRing.GetAllKeyIds()
                        Dim oldKey = protector.Protect(key, userName)
                        user = Await Store.FindByNameAsync(oldKey, CancellationToken)

                        If user IsNot Nothing Then
                            Return user
                        End If
                    Next
                End If
            End If

            Return user
        End Function

        Public Overridable Async Function CreateAsync(ByVal user As TUser, ByVal password As String) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim passwordStore = GetPasswordStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If password Is Nothing Then
                Throw New ArgumentNullException(NameOf(password))
            End If

            Dim result = Await UpdatePasswordHash(passwordStore, user, password)

            If Not result.Succeeded Then
                Return result
            End If

            Return Await CreateAsync(user)
        End Function

        Public Overridable Function NormalizeKey(ByVal key As String) As String
            Return If((KeyNormalizer Is Nothing), key, KeyNormalizer.Normalize(key))
        End Function

        Private Function ProtectPersonalData(ByVal data As String) As String
            If Options.Stores.ProtectPersonalData Then
                Dim keyRing = _services.GetService(Of ILookupProtectorKeyRing)()
                Dim protector = _services.GetService(Of ILookupProtector)()
                Return protector.Protect(keyRing.CurrentKeyId, data)
            End If

            Return data
        End Function

        Public Overridable Async Function UpdateNormalizedUserNameAsync(ByVal user As TUser) As Task
            Dim normalizedName = NormalizeKey(Await GetUserNameAsync(user))
            normalizedName = ProtectPersonalData(normalizedName)
            Await Store.SetNormalizedUserNameAsync(user, normalizedName, CancellationToken)
        End Function

        Public Overridable Async Function GetUserNameAsync(ByVal user As TUser) As Task(Of String)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return Await Store.GetUserNameAsync(user, CancellationToken)
        End Function

        Public Overridable Async Function SetUserNameAsync(ByVal user As TUser, ByVal userName As String) As Task(Of IdentityResult)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Await Store.SetUserNameAsync(user, userName, CancellationToken)
            Await UpdateSecurityStampInternal(user)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function GetUserIdAsync(ByVal user As TUser) As Task(Of String)
            ThrowIfDisposed()
            Return Await Store.GetUserIdAsync(user, CancellationToken)
        End Function

        Public Overridable Async Function CheckPasswordAsync(ByVal user As TUser, ByVal password As String) As Task(Of Boolean)
            ThrowIfDisposed()
            Dim passwordStore = GetPasswordStore()

            If user Is Nothing Then
                Return False
            End If

            Dim result = Await VerifyPasswordAsync(passwordStore, user, password)

            If result = PasswordVerificationResult.SuccessRehashNeeded Then
                Await UpdatePasswordHash(passwordStore, user, password, validatePassword:=False)
                Await UpdateUserAsync(user)
            End If

            Dim success = result <> PasswordVerificationResult.Failed

            If Not success Then
                Logger.LogWarning(0, "Invalid password for user {userId}.", Await GetUserIdAsync(user))
            End If

            Return success
        End Function

        Public Overridable Function HasPasswordAsync(ByVal user As TUser) As Task(Of Boolean)
            ThrowIfDisposed()
            Dim passwordStore = GetPasswordStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return passwordStore.HasPasswordAsync(user, CancellationToken)
        End Function

        Public Overridable Async Function AddPasswordAsync(ByVal user As TUser, ByVal password As String) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim passwordStore = GetPasswordStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Dim hash = Await passwordStore.GetPasswordHashAsync(user, CancellationToken)

            If hash IsNot Nothing Then
                Logger.LogWarning(1, "User {userId} already has a password.", Await GetUserIdAsync(user))
                Return IdentityResult.Failed(ErrorDescriber.UserAlreadyHasPassword())
            End If

            Dim result = Await UpdatePasswordHash(passwordStore, user, password)

            If Not result.Succeeded Then
                Return result
            End If

            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function ChangePasswordAsync(ByVal user As TUser, ByVal currentPassword As String, ByVal newPassword As String) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim passwordStore = GetPasswordStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If Await VerifyPasswordAsync(passwordStore, user, currentPassword) <> PasswordVerificationResult.Failed Then
                Dim result = Await UpdatePasswordHash(passwordStore, user, newPassword)

                If Not result.Succeeded Then
                    Return result
                End If

                Return Await UpdateUserAsync(user)
            End If

            Logger.LogWarning(2, "Change password failed for user {userId}.", Await GetUserIdAsync(user))
            Return IdentityResult.Failed(ErrorDescriber.PasswordMismatch())
        End Function

        Public Overridable Async Function RemovePasswordAsync(ByVal user As TUser) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim passwordStore = GetPasswordStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Await UpdatePasswordHash(passwordStore, user, Nothing, validatePassword:=False)
            Return Await UpdateUserAsync(user)
        End Function

        Protected Overridable Async Function VerifyPasswordAsync(ByVal store As IUserPasswordStore(Of TUser), ByVal user As TUser, ByVal password As String) As Task(Of PasswordVerificationResult)
            Dim hash = Await store.GetPasswordHashAsync(user, CancellationToken)

            If hash Is Nothing Then
                Return PasswordVerificationResult.Failed
            End If

            Return PasswordHasher.VerifyHashedPassword(user, hash, password)
        End Function

        Public Overridable Async Function GetSecurityStampAsync(ByVal user As TUser) As Task(Of String)
            ThrowIfDisposed()
            Dim securityStore = GetSecurityStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return Await securityStore.GetSecurityStampAsync(user, CancellationToken)
        End Function

        Public Overridable Async Function UpdateSecurityStampAsync(ByVal user As TUser) As Task(Of IdentityResult)
            ThrowIfDisposed()
            GetSecurityStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Await UpdateSecurityStampInternal(user)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Function GeneratePasswordResetTokenAsync(ByVal user As TUser) As Task(Of String)
            ThrowIfDisposed()
            Return GenerateUserTokenAsync(user, Options.Tokens.PasswordResetTokenProvider, ResetPasswordTokenPurpose)
        End Function

        Public Overridable Async Function ResetPasswordAsync(ByVal user As TUser, ByVal token As String, ByVal newPassword As String) As Task(Of IdentityResult)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If Not Await VerifyUserTokenAsync(user, Options.Tokens.PasswordResetTokenProvider, ResetPasswordTokenPurpose, token) Then
                Return IdentityResult.Failed(ErrorDescriber.InvalidToken())
            End If

            Dim result = Await UpdatePasswordHash(user, newPassword, validatePassword:=True)

            If Not result.Succeeded Then
                Return result
            End If

            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Function FindByLoginAsync(ByVal loginProvider As String, ByVal providerKey As String) As Task(Of TUser)
            ThrowIfDisposed()
            Dim loginStore = GetLoginStore()

            If loginProvider Is Nothing Then
                Throw New ArgumentNullException(NameOf(loginProvider))
            End If

            If providerKey Is Nothing Then
                Throw New ArgumentNullException(NameOf(providerKey))
            End If

            Return loginStore.FindByLoginAsync(loginProvider, providerKey, CancellationToken)
        End Function

        Public Overridable Async Function RemoveLoginAsync(ByVal user As TUser, ByVal loginProvider As String, ByVal providerKey As String) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim loginStore = GetLoginStore()

            If loginProvider Is Nothing Then
                Throw New ArgumentNullException(NameOf(loginProvider))
            End If

            If providerKey Is Nothing Then
                Throw New ArgumentNullException(NameOf(providerKey))
            End If

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Await loginStore.RemoveLoginAsync(user, loginProvider, providerKey, CancellationToken)
            Await UpdateSecurityStampInternal(user)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function AddLoginAsync(ByVal user As TUser, ByVal login As UserLoginInfo) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim loginStore = GetLoginStore()

            If login Is Nothing Then
                Throw New ArgumentNullException(NameOf(login))
            End If

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Dim existingUser = Await FindByLoginAsync(login.LoginProvider, login.ProviderKey)

            If existingUser IsNot Nothing Then
                Logger.LogWarning(4, "AddLogin for user {userId} failed because it was already associated with another user.", Await GetUserIdAsync(user))
                Return IdentityResult.Failed(ErrorDescriber.LoginAlreadyAssociated())
            End If

            Await loginStore.AddLoginAsync(user, login, CancellationToken)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function GetLoginsAsync(ByVal user As TUser) As Task(Of IList(Of UserLoginInfo))
            ThrowIfDisposed()
            Dim loginStore = GetLoginStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return Await loginStore.GetLoginsAsync(user, CancellationToken)
        End Function

        Public Overridable Function AddClaimAsync(ByVal user As TUser, ByVal claim As Claim) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim claimStore = GetClaimStore()

            If claim Is Nothing Then
                Throw New ArgumentNullException(NameOf(claim))
            End If

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return AddClaimsAsync(user, New Claim() {claim})
        End Function

        Public Overridable Async Function AddClaimsAsync(ByVal user As TUser, ByVal claims As IEnumerable(Of Claim)) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim claimStore = GetClaimStore()

            If claims Is Nothing Then
                Throw New ArgumentNullException(NameOf(claims))
            End If

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Await claimStore.AddClaimsAsync(user, claims, CancellationToken)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function ReplaceClaimAsync(ByVal user As TUser, ByVal claim As Claim, ByVal newClaim As Claim) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim claimStore = GetClaimStore()

            If claim Is Nothing Then
                Throw New ArgumentNullException(NameOf(claim))
            End If

            If newClaim Is Nothing Then
                Throw New ArgumentNullException(NameOf(newClaim))
            End If

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Await claimStore.ReplaceClaimAsync(user, claim, newClaim, CancellationToken)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Function RemoveClaimAsync(ByVal user As TUser, ByVal claim As Claim) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim claimStore = GetClaimStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If claim Is Nothing Then
                Throw New ArgumentNullException(NameOf(claim))
            End If

            Return RemoveClaimsAsync(user, New Claim() {claim})
        End Function

        Public Overridable Async Function RemoveClaimsAsync(ByVal user As TUser, ByVal claims As IEnumerable(Of Claim)) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim claimStore = GetClaimStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If claims Is Nothing Then
                Throw New ArgumentNullException(NameOf(claims))
            End If

            Await claimStore.RemoveClaimsAsync(user, claims, CancellationToken)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function GetClaimsAsync(ByVal user As TUser) As Task(Of IList(Of Claim))
            ThrowIfDisposed()
            Dim claimStore = GetClaimStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return Await claimStore.GetClaimsAsync(user, CancellationToken)
        End Function

        Public Overridable Async Function AddToRoleAsync(ByVal user As TUser, ByVal role As String) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim userRoleStore = GetUserRoleStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Dim normalizedRole = NormalizeKey(role)

            If Await userRoleStore.IsInRoleAsync(user, normalizedRole, CancellationToken) Then
                Return Await UserAlreadyInRoleError(user, role)
            End If

            Await userRoleStore.AddToRoleAsync(user, normalizedRole, CancellationToken)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function AddToRolesAsync(ByVal user As TUser, ByVal roles As IEnumerable(Of String)) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim userRoleStore = GetUserRoleStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If roles Is Nothing Then
                Throw New ArgumentNullException(NameOf(roles))
            End If

            For Each role In roles.Distinct()
                Dim normalizedRole = NormalizeKey(role)

                If Await userRoleStore.IsInRoleAsync(user, normalizedRole, CancellationToken) Then
                    Return Await UserAlreadyInRoleError(user, role)
                End If

                Await userRoleStore.AddToRoleAsync(user, normalizedRole, CancellationToken)
            Next

            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function RemoveFromRoleAsync(ByVal user As TUser, ByVal role As String) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim userRoleStore = GetUserRoleStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Dim normalizedRole = NormalizeKey(role)

            If Not Await userRoleStore.IsInRoleAsync(user, normalizedRole, CancellationToken) Then
                Return Await UserNotInRoleError(user, role)
            End If

            Await userRoleStore.RemoveFromRoleAsync(user, normalizedRole, CancellationToken)
            Return Await UpdateUserAsync(user)
        End Function

        Private Async Function UserAlreadyInRoleError(ByVal user As TUser, ByVal role As String) As Task(Of IdentityResult)
            Logger.LogWarning(5, "User {userId} is already in role {role}.", Await GetUserIdAsync(user), role)
            Return IdentityResult.Failed(ErrorDescriber.UserAlreadyInRole(role))
        End Function

        Private Async Function UserNotInRoleError(ByVal user As TUser, ByVal role As String) As Task(Of IdentityResult)
            Logger.LogWarning(6, "User {userId} is not in role {role}.", Await GetUserIdAsync(user), role)
            Return IdentityResult.Failed(ErrorDescriber.UserNotInRole(role))
        End Function

        Public Overridable Async Function RemoveFromRolesAsync(ByVal user As TUser, ByVal roles As IEnumerable(Of String)) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim userRoleStore = GetUserRoleStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If roles Is Nothing Then
                Throw New ArgumentNullException(NameOf(roles))
            End If

            For Each role In roles
                Dim normalizedRole = NormalizeKey(role)

                If Not Await userRoleStore.IsInRoleAsync(user, normalizedRole, CancellationToken) Then
                    Return Await UserNotInRoleError(user, role)
                End If

                Await userRoleStore.RemoveFromRoleAsync(user, normalizedRole, CancellationToken)
            Next

            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function GetRolesAsync(ByVal user As TUser) As Task(Of IList(Of String))
            ThrowIfDisposed()
            Dim userRoleStore = GetUserRoleStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return Await userRoleStore.GetRolesAsync(user, CancellationToken)
        End Function

        Public Overridable Async Function IsInRoleAsync(ByVal user As TUser, ByVal role As String) As Task(Of Boolean)
            ThrowIfDisposed()
            Dim userRoleStore = GetUserRoleStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return Await userRoleStore.IsInRoleAsync(user, NormalizeKey(role), CancellationToken)
        End Function

        Public Overridable Async Function GetEmailAsync(ByVal user As TUser) As Task(Of String)
            ThrowIfDisposed()
            Dim store = GetEmailStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return Await store.GetEmailAsync(user, CancellationToken)
        End Function

        Public Overridable Async Function SetEmailAsync(ByVal user As TUser, ByVal email As String) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim store = GetEmailStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Await store.SetEmailAsync(user, email, CancellationToken)
            Await store.SetEmailConfirmedAsync(user, False, CancellationToken)
            Await UpdateSecurityStampInternal(user)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function FindByEmailAsync(ByVal email As String) As Task(Of TUser)
            ThrowIfDisposed()
            Dim store = GetEmailStore()

            If email Is Nothing Then
                Throw New ArgumentNullException(NameOf(email))
            End If

            email = NormalizeKey(email)
            Dim user = Await store.FindByEmailAsync(email, CancellationToken)

            If user Is Nothing AndAlso Options.Stores.ProtectPersonalData Then
                Dim keyRing = _services.GetService(Of ILookupProtectorKeyRing)()
                Dim protector = _services.GetService(Of ILookupProtector)()

                If keyRing IsNot Nothing AndAlso protector IsNot Nothing Then

                    For Each key In keyRing.GetAllKeyIds()
                        Dim oldKey = protector.Protect(key, email)
                        user = Await store.FindByEmailAsync(oldKey, CancellationToken)

                        If user IsNot Nothing Then
                            Return user
                        End If
                    Next
                End If
            End If

            Return user
        End Function

        Public Overridable Async Function UpdateNormalizedEmailAsync(ByVal user As TUser) As Task
            Dim store = GetEmailStore(throwOnFail:=False)

            If store IsNot Nothing Then
                Dim email = Await GetEmailAsync(user)
                Await store.SetNormalizedEmailAsync(user, ProtectPersonalData(NormalizeKey(email)), CancellationToken)
            End If
        End Function

        Public Overridable Function GenerateEmailConfirmationTokenAsync(ByVal user As TUser) As Task(Of String)
            ThrowIfDisposed()
            Return GenerateUserTokenAsync(user, Options.Tokens.EmailConfirmationTokenProvider, ConfirmEmailTokenPurpose)
        End Function

        Public Overridable Async Function ConfirmEmailAsync(ByVal user As TUser, ByVal token As String) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim store = GetEmailStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If Not Await VerifyUserTokenAsync(user, Options.Tokens.EmailConfirmationTokenProvider, ConfirmEmailTokenPurpose, token) Then
                Return IdentityResult.Failed(ErrorDescriber.InvalidToken())
            End If

            Await store.SetEmailConfirmedAsync(user, True, CancellationToken)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function IsEmailConfirmedAsync(ByVal user As TUser) As Task(Of Boolean)
            ThrowIfDisposed()
            Dim store = GetEmailStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return Await store.GetEmailConfirmedAsync(user, CancellationToken)
        End Function

        Public Overridable Function GenerateChangeEmailTokenAsync(ByVal user As TUser, ByVal newEmail As String) As Task(Of String)
            ThrowIfDisposed()
            Return GenerateUserTokenAsync(user, Options.Tokens.ChangeEmailTokenProvider, GetChangeEmailTokenPurpose(newEmail))
        End Function

        Public Overridable Async Function ChangeEmailAsync(ByVal user As TUser, ByVal newEmail As String, ByVal token As String) As Task(Of IdentityResult)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If Not Await VerifyUserTokenAsync(user, Options.Tokens.ChangeEmailTokenProvider, GetChangeEmailTokenPurpose(newEmail), token) Then
                Return IdentityResult.Failed(ErrorDescriber.InvalidToken())
            End If

            Dim store = GetEmailStore()
            Await store.SetEmailAsync(user, newEmail, CancellationToken)
            Await store.SetEmailConfirmedAsync(user, True, CancellationToken)
            Await UpdateSecurityStampInternal(user)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function GetPhoneNumberAsync(ByVal user As TUser) As Task(Of String)
            ThrowIfDisposed()
            Dim store = GetPhoneNumberStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return Await store.GetPhoneNumberAsync(user, CancellationToken)
        End Function

        Public Overridable Async Function SetPhoneNumberAsync(ByVal user As TUser, ByVal phoneNumber As String) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim store = GetPhoneNumberStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Await store.SetPhoneNumberAsync(user, phoneNumber, CancellationToken)
            Await store.SetPhoneNumberConfirmedAsync(user, False, CancellationToken)
            Await UpdateSecurityStampInternal(user)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function ChangePhoneNumberAsync(ByVal user As TUser, ByVal phoneNumber As String, ByVal token As String) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim store = GetPhoneNumberStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If Not Await VerifyChangePhoneNumberTokenAsync(user, token, phoneNumber) Then
                Logger.LogWarning(7, "Change phone number for user {userId} failed with invalid token.", Await GetUserIdAsync(user))
                Return IdentityResult.Failed(ErrorDescriber.InvalidToken())
            End If

            Await store.SetPhoneNumberAsync(user, phoneNumber, CancellationToken)
            Await store.SetPhoneNumberConfirmedAsync(user, True, CancellationToken)
            Await UpdateSecurityStampInternal(user)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Function IsPhoneNumberConfirmedAsync(ByVal user As TUser) As Task(Of Boolean)
            ThrowIfDisposed()
            Dim store = GetPhoneNumberStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return store.GetPhoneNumberConfirmedAsync(user, CancellationToken)
        End Function

        Public Overridable Function GenerateChangePhoneNumberTokenAsync(ByVal user As TUser, ByVal phoneNumber As String) As Task(Of String)
            ThrowIfDisposed()
            Return GenerateUserTokenAsync(user, Options.Tokens.ChangePhoneNumberTokenProvider, ChangePhoneNumberTokenPurpose & ":" & phoneNumber)
        End Function

        Public Overridable Function VerifyChangePhoneNumberTokenAsync(ByVal user As TUser, ByVal token As String, ByVal phoneNumber As String) As Task(Of Boolean)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return VerifyUserTokenAsync(user, Options.Tokens.ChangePhoneNumberTokenProvider, ChangePhoneNumberTokenPurpose & ":" & phoneNumber, token)
        End Function

        Public Overridable Async Function VerifyUserTokenAsync(ByVal user As TUser, ByVal tokenProvider As String, ByVal purpose As String, ByVal token As String) As Task(Of Boolean)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If tokenProvider Is Nothing Then
                Throw New ArgumentNullException(NameOf(tokenProvider))
            End If

            If Not _tokenProviders.ContainsKey(tokenProvider) Then
                Throw New NotSupportedException(Resources.FormatNoTokenProvider(NameOf(TUser), tokenProvider))
            End If

            Dim result = Await _tokenProviders(tokenProvider).ValidateAsync(purpose, token, Me, user)

            If Not result Then
                Logger.LogWarning(9, "VerifyUserTokenAsync() failed with purpose: {purpose} for user {userId}.", purpose, Await GetUserIdAsync(user))
            End If

            Return result
        End Function

        Public Overridable Function GenerateUserTokenAsync(ByVal user As TUser, ByVal tokenProvider As String, ByVal purpose As String) As Task(Of String)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If tokenProvider Is Nothing Then
                Throw New ArgumentNullException(NameOf(tokenProvider))
            End If

            If Not _tokenProviders.ContainsKey(tokenProvider) Then
                Throw New NotSupportedException(Resources.FormatNoTokenProvider(NameOf(TUser), tokenProvider))
            End If

            Return _tokenProviders(tokenProvider).GenerateAsync(purpose, Me, user)
        End Function

        Public Overridable Sub RegisterTokenProvider(ByVal providerName As String, ByVal provider As IUserTwoFactorTokenProvider(Of TUser))
            ThrowIfDisposed()

            If provider Is Nothing Then
                Throw New ArgumentNullException(NameOf(provider))
            End If

            _tokenProviders(providerName) = provider
        End Sub

        Public Overridable Async Function GetValidTwoFactorProvidersAsync(ByVal user As TUser) As Task(Of IList(Of String))
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Dim results = New List(Of String)()

            For Each f In _tokenProviders

                If Await f.Value.CanGenerateTwoFactorTokenAsync(Me, user) Then
                    results.Add(f.Key)
                End If
            Next

            Return results
        End Function

        Public Overridable Async Function VerifyTwoFactorTokenAsync(ByVal user As TUser, ByVal tokenProvider As String, ByVal token As String) As Task(Of Boolean)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If Not _tokenProviders.ContainsKey(tokenProvider) Then
                Throw New NotSupportedException(Resources.FormatNoTokenProvider(NameOf(TUser), tokenProvider))
            End If

            Dim result = Await _tokenProviders(tokenProvider).ValidateAsync("TwoFactor", token, Me, user)

            If Not result Then
                Logger.LogWarning(10, $"{NameOf(VerifyTwoFactorTokenAsync)}() failed for user {Await GetUserIdAsync(user)}.")
            End If

            Return result
        End Function

        Public Overridable Function GenerateTwoFactorTokenAsync(ByVal user As TUser, ByVal tokenProvider As String) As Task(Of String)
            ThrowIfDisposed()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If Not _tokenProviders.ContainsKey(tokenProvider) Then
                Throw New NotSupportedException(Resources.FormatNoTokenProvider(NameOf(TUser), tokenProvider))
            End If

            Return _tokenProviders(tokenProvider).GenerateAsync("TwoFactor", Me, user)
        End Function

        Public Overridable Async Function GetTwoFactorEnabledAsync(ByVal user As TUser) As Task(Of Boolean)
            ThrowIfDisposed()
            Dim store = GetUserTwoFactorStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return Await store.GetTwoFactorEnabledAsync(user, CancellationToken)
        End Function

        Public Overridable Async Function SetTwoFactorEnabledAsync(ByVal user As TUser, ByVal enabled As Boolean) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim store = GetUserTwoFactorStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Await store.SetTwoFactorEnabledAsync(user, enabled, CancellationToken)
            Await UpdateSecurityStampInternal(user)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function IsLockedOutAsync(ByVal user As TUser) As Task(Of Boolean)
            ThrowIfDisposed()
            Dim store = GetUserLockoutStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If Not Await store.GetLockoutEnabledAsync(user, CancellationToken) Then
                Return False
            End If

            Dim lockoutTime = Await store.GetLockoutEndDateAsync(user, CancellationToken)
            Return lockoutTime >= DateTimeOffset.UtcNow
        End Function

        Public Overridable Async Function SetLockoutEnabledAsync(ByVal user As TUser, ByVal enabled As Boolean) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim store = GetUserLockoutStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Await store.SetLockoutEnabledAsync(user, enabled, CancellationToken)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function GetLockoutEnabledAsync(ByVal user As TUser) As Task(Of Boolean)
            ThrowIfDisposed()
            Dim store = GetUserLockoutStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return Await store.GetLockoutEnabledAsync(user, CancellationToken)
        End Function

        Public Overridable Async Function GetLockoutEndDateAsync(ByVal user As TUser) As Task(Of DateTimeOffset?)
            ThrowIfDisposed()
            Dim store = GetUserLockoutStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return Await store.GetLockoutEndDateAsync(user, CancellationToken)
        End Function

        Public Overridable Async Function SetLockoutEndDateAsync(ByVal user As TUser, ByVal lockoutEnd As DateTimeOffset?) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim store = GetUserLockoutStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If Not Await store.GetLockoutEnabledAsync(user, CancellationToken) Then
                Logger.LogWarning(11, "Lockout for user {userId} failed because lockout is not enabled for this user.", Await GetUserIdAsync(user))
                Return IdentityResult.Failed(ErrorDescriber.UserLockoutNotEnabled())
            End If

            Await store.SetLockoutEndDateAsync(user, lockoutEnd, CancellationToken)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function AccessFailedAsync(ByVal user As TUser) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim store = GetUserLockoutStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Dim count = Await store.IncrementAccessFailedCountAsync(user, CancellationToken)

            If count < Options.Lockout.MaxFailedAccessAttempts Then
                Return Await UpdateUserAsync(user)
            End If

            Logger.LogWarning(12, "User {userId} is locked out.", Await GetUserIdAsync(user))
            Await store.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.Add(Options.Lockout.DefaultLockoutTimeSpan), CancellationToken)
            Await store.ResetAccessFailedCountAsync(user, CancellationToken)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function ResetAccessFailedCountAsync(ByVal user As TUser) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim store = GetUserLockoutStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If Await GetAccessFailedCountAsync(user) = 0 Then
                Return IdentityResult.Success
            End If

            Await store.ResetAccessFailedCountAsync(user, CancellationToken)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function GetAccessFailedCountAsync(ByVal user As TUser) As Task(Of Integer)
            ThrowIfDisposed()
            Dim store = GetUserLockoutStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return Await store.GetAccessFailedCountAsync(user, CancellationToken)
        End Function

        Public Overridable Function GetUsersForClaimAsync(ByVal claim As Claim) As Task(Of IList(Of TUser))
            ThrowIfDisposed()
            Dim store = GetClaimStore()

            If claim Is Nothing Then
                Throw New ArgumentNullException(NameOf(claim))
            End If

            Return store.GetUsersForClaimAsync(claim, CancellationToken)
        End Function

        Public Overridable Function GetUsersInRoleAsync(ByVal roleName As String) As Task(Of IList(Of TUser))
            ThrowIfDisposed()
            Dim store = GetUserRoleStore()

            If roleName Is Nothing Then
                Throw New ArgumentNullException(NameOf(roleName))
            End If

            Return store.GetUsersInRoleAsync(NormalizeKey(roleName), CancellationToken)
        End Function

        Public Overridable Function GetAuthenticationTokenAsync(ByVal user As TUser, ByVal loginProvider As String, ByVal tokenName As String) As Task(Of String)
            ThrowIfDisposed()
            Dim store = GetAuthenticationTokenStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If loginProvider Is Nothing Then
                Throw New ArgumentNullException(NameOf(loginProvider))
            End If

            If tokenName Is Nothing Then
                Throw New ArgumentNullException(NameOf(tokenName))
            End If

            Return store.GetTokenAsync(user, loginProvider, tokenName, CancellationToken)
        End Function

        Public Overridable Async Function SetAuthenticationTokenAsync(ByVal user As TUser, ByVal loginProvider As String, ByVal tokenName As String, ByVal tokenValue As String) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim store = GetAuthenticationTokenStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If loginProvider Is Nothing Then
                Throw New ArgumentNullException(NameOf(loginProvider))
            End If

            If tokenName Is Nothing Then
                Throw New ArgumentNullException(NameOf(tokenName))
            End If

            Await store.SetTokenAsync(user, loginProvider, tokenName, tokenValue, CancellationToken)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Async Function RemoveAuthenticationTokenAsync(ByVal user As TUser, ByVal loginProvider As String, ByVal tokenName As String) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim store = GetAuthenticationTokenStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            If loginProvider Is Nothing Then
                Throw New ArgumentNullException(NameOf(loginProvider))
            End If

            If tokenName Is Nothing Then
                Throw New ArgumentNullException(NameOf(tokenName))
            End If

            Await store.RemoveTokenAsync(user, loginProvider, tokenName, CancellationToken)
            Return Await UpdateUserAsync(user)
        End Function

        Public Overridable Function GetAuthenticatorKeyAsync(ByVal user As TUser) As Task(Of String)
            ThrowIfDisposed()
            Dim store = GetAuthenticatorKeyStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return store.GetAuthenticatorKeyAsync(user, CancellationToken)
        End Function

        Public Overridable Async Function ResetAuthenticatorKeyAsync(ByVal user As TUser) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim store = GetAuthenticatorKeyStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Await store.SetAuthenticatorKeyAsync(user, GenerateNewAuthenticatorKey(), CancellationToken)
            Await UpdateSecurityStampInternal(user)
            Return Await UpdateAsync(user)
        End Function

        Public Overridable Function GenerateNewAuthenticatorKey() As String
            Return NewSecurityStamp()
        End Function

        Public Overridable Async Function GenerateNewTwoFactorRecoveryCodesAsync(ByVal user As TUser, ByVal number As Integer) As Task(Of IEnumerable(Of String))
            ThrowIfDisposed()
            Dim store = GetRecoveryCodeStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Dim newCodes = New List(Of String)(number)

            For i = 0 To number - 1
                newCodes.Add(CreateTwoFactorRecoveryCode())
            Next

            Await store.ReplaceCodesAsync(user, newCodes.Distinct(), CancellationToken)
            Dim update = Await UpdateAsync(user)

            If update.Succeeded Then
                Return newCodes
            End If

            Return Nothing
        End Function

        Protected Overridable Function CreateTwoFactorRecoveryCode() As String
            Return Guid.NewGuid().ToString().Substring(0, 8)
        End Function

        Public Overridable Async Function RedeemTwoFactorRecoveryCodeAsync(ByVal user As TUser, ByVal code As String) As Task(Of IdentityResult)
            ThrowIfDisposed()
            Dim store = GetRecoveryCodeStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Dim success = Await store.RedeemCodeAsync(user, code, CancellationToken)

            If success Then
                Return Await UpdateAsync(user)
            End If

            Return IdentityResult.Failed(ErrorDescriber.RecoveryCodeRedemptionFailed())
        End Function

        Public Overridable Function CountRecoveryCodesAsync(ByVal user As TUser) As Task(Of Integer)
            ThrowIfDisposed()
            Dim store = GetRecoveryCodeStore()

            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            Return store.CountCodesAsync(user, CancellationToken)
        End Function

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso Not _disposed Then
                Store.Dispose()
                _disposed = True
            End If
        End Sub

        Private Function GetUserTwoFactorStore() As IUserTwoFactorStore(Of TUser)
            Dim cast = TryCast(Store, IUserTwoFactorStore(Of TUser))

            If cast Is Nothing Then
                Throw New NotSupportedException(Resources.StoreNotIUserTwoFactorStore)
            End If

            Return cast
        End Function

        Private Function GetUserLockoutStore() As IUserLockoutStore(Of TUser)
            Dim cast = TryCast(Store, IUserLockoutStore(Of TUser))

            If cast Is Nothing Then
                Throw New NotSupportedException(Resources.StoreNotIUserLockoutStore)
            End If

            Return cast
        End Function

        Private Function GetEmailStore(ByVal Optional throwOnFail As Boolean = True) As IUserEmailStore(Of TUser)
            Dim cast = TryCast(Store, IUserEmailStore(Of TUser))

            If throwOnFail AndAlso cast Is Nothing Then
                Throw New NotSupportedException(Resources.StoreNotIUserEmailStore)
            End If

            Return cast
        End Function

        Private Function GetPhoneNumberStore() As IUserPhoneNumberStore(Of TUser)
            Dim cast = TryCast(Store, IUserPhoneNumberStore(Of TUser))

            If cast Is Nothing Then
                Throw New NotSupportedException(Resources.StoreNotIUserPhoneNumberStore)
            End If

            Return cast
        End Function

        Public Overridable Async Function CreateSecurityTokenAsync(ByVal user As TUser) As Task(Of Byte())
            Return Encoding.Unicode.GetBytes(Await GetSecurityStampAsync(user))
        End Function

        Private Async Function UpdateSecurityStampInternal(ByVal user As TUser) As Task
            If SupportsUserSecurityStamp Then
                Await GetSecurityStore().SetSecurityStampAsync(user, NewSecurityStamp(), CancellationToken)
            End If
        End Function

        Protected Overridable Function UpdatePasswordHash(ByVal user As TUser, ByVal newPassword As String, ByVal validatePassword As Boolean) As Task(Of IdentityResult)
            Return UpdatePasswordHash(GetPasswordStore(), user, newPassword, validatePassword)
        End Function

        Private Async Function UpdatePasswordHash(ByVal passwordStore As IUserPasswordStore(Of TUser), ByVal user As TUser, ByVal newPassword As String, ByVal Optional validatePassword As Boolean = True) As Task(Of IdentityResult)
            If validatePassword Then
                Dim validate = Await ValidatePasswordAsync(user, newPassword)

                If Not validate.Succeeded Then
                    Return validate
                End If
            End If

            Dim hash = If(newPassword IsNot Nothing, PasswordHasher.HashPassword(user, newPassword), Nothing)
            Await passwordStore.SetPasswordHashAsync(user, hash, CancellationToken)
            Await UpdateSecurityStampInternal(user)
            Return IdentityResult.Success
        End Function

        Private Function GetUserRoleStore() As IUserRoleStore(Of TUser)
            Dim cast = TryCast(Store, IUserRoleStore(Of TUser))

            If cast Is Nothing Then
                Throw New NotSupportedException(Resources.StoreNotIUserRoleStore)
            End If

            Return cast
        End Function

        Private Shared Function NewSecurityStamp() As String
            Dim bytes As Byte() = New Byte(19) {}
            _rng.GetBytes(bytes)
            Return Base32.ToBase32(bytes)
        End Function

        Private Function GetLoginStore() As IUserLoginStore(Of TUser)
            Dim cast = TryCast(Store, IUserLoginStore(Of TUser))

            If cast Is Nothing Then
                Throw New NotSupportedException(Resources.StoreNotIUserLoginStore)
            End If

            Return cast
        End Function

        Private Function GetSecurityStore() As IUserSecurityStampStore(Of TUser)
            Dim cast = TryCast(Store, IUserSecurityStampStore(Of TUser))

            If cast Is Nothing Then
                Throw New NotSupportedException(Resources.StoreNotIUserSecurityStampStore)
            End If

            Return cast
        End Function

        Private Function GetClaimStore() As IUserClaimStore(Of TUser)
            Dim cast = TryCast(Store, IUserClaimStore(Of TUser))

            If cast Is Nothing Then
                Throw New NotSupportedException(Resources.StoreNotIUserClaimStore)
            End If

            Return cast
        End Function

        Protected Shared Function GetChangeEmailTokenPurpose(ByVal newEmail As String) As String
            Return "ChangeEmail:" & newEmail
        End Function

        Protected Async Function ValidateUserAsync(ByVal user As TUser) As Task(Of IdentityResult)
            If SupportsUserSecurityStamp Then
                Dim stamp = Await GetSecurityStampAsync(user)

                If stamp Is Nothing Then
                    Throw New InvalidOperationException(Resources.NullSecurityStamp)
                End If
            End If

            Dim errors = New List(Of IdentityError)()

            For Each v In UserValidators
                Dim result = Await v.ValidateAsync(Me, user)

                If Not result.Succeeded Then
                    errors.AddRange(result.Errors)
                End If
            Next

            If errors.Count > 0 Then
                Logger.LogWarning(13, "User {userId} validation failed: {errors}.", Await GetUserIdAsync(user), String.Join(";", errors.[Select](Function(e) e.Code)))
                Return IdentityResult.Failed(errors.ToArray())
            End If

            Return IdentityResult.Success
        End Function

        Protected Async Function ValidatePasswordAsync(ByVal user As TUser, ByVal password As String) As Task(Of IdentityResult)
            Dim errors = New List(Of IdentityError)()

            For Each v In PasswordValidators
                Dim result = Await v.ValidateAsync(Me, user, password)

                If Not result.Succeeded Then
                    errors.AddRange(result.Errors)
                End If
            Next

            If errors.Count > 0 Then
                Logger.LogWarning(14, "User {userId} password validation failed: {errors}.", Await GetUserIdAsync(user), String.Join(";", errors.[Select](Function(e) e.Code)))
                Return IdentityResult.Failed(errors.ToArray())
            End If

            Return IdentityResult.Success
        End Function

        Protected Overridable Async Function UpdateUserAsync(ByVal user As TUser) As Task(Of IdentityResult)
            Dim result = Await ValidateUserAsync(user)

            If Not result.Succeeded Then
                Return result
            End If

            Await UpdateNormalizedUserNameAsync(user)
            Await UpdateNormalizedEmailAsync(user)
            Return Await Store.UpdateAsync(user, CancellationToken)
        End Function

        Private Function GetAuthenticatorKeyStore() As IUserAuthenticatorKeyStore(Of TUser)
            Dim cast = TryCast(Store, IUserAuthenticatorKeyStore(Of TUser))

            If cast Is Nothing Then
                Throw New NotSupportedException(Resources.StoreNotIUserAuthenticatorKeyStore)
            End If

            Return cast
        End Function

        Private Function GetRecoveryCodeStore() As IUserTwoFactorRecoveryCodeStore(Of TUser)
            Dim cast = TryCast(Store, IUserTwoFactorRecoveryCodeStore(Of TUser))

            If cast Is Nothing Then
                Throw New NotSupportedException(Resources.StoreNotIUserTwoFactorRecoveryCodeStore)
            End If

            Return cast
        End Function

        Private Function GetAuthenticationTokenStore() As IUserAuthenticationTokenStore(Of TUser)
            Dim cast = TryCast(Store, IUserAuthenticationTokenStore(Of TUser))

            If cast Is Nothing Then
                Throw New NotSupportedException(Resources.StoreNotIUserAuthenticationTokenStore)
            End If

            Return cast
        End Function

        Private Function GetPasswordStore() As IUserPasswordStore(Of TUser)
            Dim cast = TryCast(Store, IUserPasswordStore(Of TUser))

            If cast Is Nothing Then
                Throw New NotSupportedException(Resources.StoreNotIUserPasswordStore)
            End If

            Return cast
        End Function

        Protected Sub ThrowIfDisposed()
            If _disposed Then
                Throw New ObjectDisposedException([GetType]().Name)
            End If
        End Sub
    End Class
End Namespace
