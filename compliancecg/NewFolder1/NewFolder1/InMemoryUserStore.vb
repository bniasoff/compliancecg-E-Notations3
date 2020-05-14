Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Security.Claims
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNetCore.Identity.Test

Namespace Microsoft.AspNetCore.Identity.InMemory
    Public Class InMemoryUserStore(Of TUser As PocoUser)
        Inherits IUserLoginStore(Of TUser)
        Implements IUserClaimStore(Of TUser), IUserPasswordStore(Of TUser), IUserSecurityStampStore(Of TUser), IUserEmailStore(Of TUser), IUserLockoutStore(Of TUser), IUserPhoneNumberStore(Of TUser), IQueryableUserStore(Of TUser), IUserTwoFactorStore(Of TUser), IUserAuthenticationTokenStore(Of TUser), IUserAuthenticatorKeyStore(Of TUser), IUserTwoFactorRecoveryCodeStore(Of TUser)

        Private ReadOnly _logins As Dictionary(Of String, TUser) = New Dictionary(Of String, TUser)()
        Private ReadOnly _users As Dictionary(Of String, TUser) = New Dictionary(Of String, TUser)()

        Public ReadOnly Property Users As IQueryable(Of TUser)
            Get
                Return _users.Values.AsQueryable()
            End Get
        End Property

        Public Function GetClaimsAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of IList(Of Claim))
            Dim claims = user.Claims.[Select](Function(c) New Claim(c.ClaimType, c.ClaimValue)).ToList()
            Return Task.FromResult(Of IList(Of Claim))(claims)
        End Function

        Public Function AddClaimsAsync(ByVal user As TUser, ByVal claims As IEnumerable(Of Claim), ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            For Each claim In claims
                user.Claims.Add(New PocoUserClaim With {
                    .ClaimType = claim.Type,
                    .ClaimValue = claim.Value,
                    .UserId = user.Id
                })
            Next

            Return Task.FromResult(0)
        End Function

        Public Function ReplaceClaimAsync(ByVal user As TUser, ByVal claim As Claim, ByVal newClaim As Claim, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            Dim matchedClaims = user.Claims.Where(Function(uc) uc.ClaimValue = claim.Value AndAlso uc.ClaimType = claim.Type).ToList()

            For Each matchedClaim In matchedClaims
                matchedClaim.ClaimValue = newClaim.Value
                matchedClaim.ClaimType = newClaim.Type
            Next

            Return Task.FromResult(0)
        End Function

        Public Function RemoveClaimsAsync(ByVal user As TUser, ByVal claims As IEnumerable(Of Claim), ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            For Each claim In claims
                Dim entity = user.Claims.FirstOrDefault(Function(uc) uc.UserId = user.Id AndAlso uc.ClaimType = claim.Type AndAlso uc.ClaimValue = claim.Value)

                If entity IsNot Nothing Then
                    user.Claims.Remove(entity)
                End If
            Next

            Return Task.FromResult(0)
        End Function

        Public Function SetEmailAsync(ByVal user As TUser, ByVal email As String, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            user.Email = email
            Return Task.FromResult(0)
        End Function

        Public Function GetEmailAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of String)
            Return Task.FromResult(user.Email)
        End Function

        Public Function GetNormalizedEmailAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of String)
            Return Task.FromResult(user.NormalizedEmail)
        End Function

        Public Function SetNormalizedEmailAsync(ByVal user As TUser, ByVal normalizedEmail As String, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            user.NormalizedEmail = normalizedEmail
            Return Task.FromResult(0)
        End Function

        Public Function GetEmailConfirmedAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of Boolean)
            Return Task.FromResult(user.EmailConfirmed)
        End Function

        Public Function SetEmailConfirmedAsync(ByVal user As TUser, ByVal confirmed As Boolean, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            user.EmailConfirmed = confirmed
            Return Task.FromResult(0)
        End Function

        Public Function FindByEmailAsync(ByVal email As String, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of TUser)
            Return Task.FromResult(Users.FirstOrDefault(Function(u) u.NormalizedEmail = email))
        End Function

        Public Function GetLockoutEndDateAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of DateTimeOffset?)
            Return Task.FromResult(user.LockoutEnd)
        End Function

        Public Function SetLockoutEndDateAsync(ByVal user As TUser, ByVal lockoutEnd As DateTimeOffset?, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            user.LockoutEnd = lockoutEnd
            Return Task.FromResult(0)
        End Function

        Public Function IncrementAccessFailedCountAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of Integer)
            user.AccessFailedCount += 1
            Return Task.FromResult(user.AccessFailedCount)
        End Function

        Public Function ResetAccessFailedCountAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            user.AccessFailedCount = 0
            Return Task.FromResult(0)
        End Function

        Public Function GetAccessFailedCountAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of Integer)
            Return Task.FromResult(user.AccessFailedCount)
        End Function

        Public Function GetLockoutEnabledAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of Boolean)
            Return Task.FromResult(user.LockoutEnabled)
        End Function

        Public Function SetLockoutEnabledAsync(ByVal user As TUser, ByVal enabled As Boolean, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            user.LockoutEnabled = enabled
            Return Task.FromResult(0)
        End Function

        Private Function GetLoginKey(ByVal loginProvider As String, ByVal providerKey As String) As String
            Return loginProvider & "|" & providerKey
        End Function

        Public Overridable Function AddLoginAsync(ByVal user As TUser, ByVal login As UserLoginInfo, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            user.Logins.Add(New PocoUserLogin With {
                .UserId = user.Id,
                .ProviderKey = login.ProviderKey,
                .LoginProvider = login.LoginProvider,
                .ProviderDisplayName = login.ProviderDisplayName
            })
            _logins(GetLoginKey(login.LoginProvider, login.ProviderKey)) = user
            Return Task.FromResult(0)
        End Function

        Public Function RemoveLoginAsync(ByVal user As TUser, ByVal loginProvider As String, ByVal providerKey As String, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            Dim loginEntity = user.Logins.SingleOrDefault(Function(l) l.ProviderKey = providerKey AndAlso l.LoginProvider = loginProvider AndAlso l.UserId = user.Id)

            If loginEntity IsNot Nothing Then
                user.Logins.Remove(loginEntity)
            End If

            _logins(GetLoginKey(loginProvider, providerKey)) = Nothing
            Return Task.FromResult(0)
        End Function

        Public Function GetLoginsAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of IList(Of UserLoginInfo))
            Dim result As IList(Of UserLoginInfo) = user.Logins.[Select](Function(l) New UserLoginInfo(l.LoginProvider, l.ProviderKey, l.ProviderDisplayName)).ToList()
            Return Task.FromResult(result)
        End Function

        Public Function FindByLoginAsync(ByVal loginProvider As String, ByVal providerKey As String, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of TUser)
            Dim key As String = GetLoginKey(loginProvider, providerKey)

            If _logins.ContainsKey(key) Then
                Return Task.FromResult(_logins(key))
            End If

            Return Task.FromResult(Of TUser)(Nothing)
        End Function

        Public Function GetUserIdAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of String)
            Return Task.FromResult(user.Id)
        End Function

        Public Function GetUserNameAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of String)
            Return Task.FromResult(user.UserName)
        End Function

        Public Function SetUserNameAsync(ByVal user As TUser, ByVal userName As String, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            user.UserName = userName
            Return Task.FromResult(0)
        End Function

        Public Function CreateAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of IdentityResult)
            _users(user.Id) = user
            Return Task.FromResult(IdentityResult.Success)
        End Function

        Public Function UpdateAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of IdentityResult)
            _users(user.Id) = user
            Return Task.FromResult(IdentityResult.Success)
        End Function

        Public Function FindByIdAsync(ByVal userId As String, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of TUser)
            If _users.ContainsKey(userId) Then
                Return Task.FromResult(_users(userId))
            End If

            Return Task.FromResult(Of TUser)(Nothing)
        End Function

        Public Sub Dispose()
        End Sub

        Public Function FindByNameAsync(ByVal userName As String, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of TUser)
            Return Task.FromResult(Users.FirstOrDefault(Function(u) u.NormalizedUserName = userName))
        End Function

        Public Function DeleteAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of IdentityResult)
            If user Is Nothing OrElse Not _users.ContainsKey(user.Id) Then
                Throw New InvalidOperationException("Unknown user")
            End If

            _users.Remove(user.Id)
            Return Task.FromResult(IdentityResult.Success)
        End Function

        Public Function SetPasswordHashAsync(ByVal user As TUser, ByVal passwordHash As String, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            user.PasswordHash = passwordHash
            Return Task.FromResult(0)
        End Function

        Public Function GetPasswordHashAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of String)
            Return Task.FromResult(user.PasswordHash)
        End Function

        Public Function HasPasswordAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of Boolean)
            Return Task.FromResult(user.PasswordHash IsNot Nothing)
        End Function

        Public Function SetPhoneNumberAsync(ByVal user As TUser, ByVal phoneNumber As String, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            user.PhoneNumber = phoneNumber
            Return Task.FromResult(0)
        End Function

        Public Function GetPhoneNumberAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of String)
            Return Task.FromResult(user.PhoneNumber)
        End Function

        Public Function GetPhoneNumberConfirmedAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of Boolean)
            Return Task.FromResult(user.PhoneNumberConfirmed)
        End Function

        Public Function SetPhoneNumberConfirmedAsync(ByVal user As TUser, ByVal confirmed As Boolean, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            user.PhoneNumberConfirmed = confirmed
            Return Task.FromResult(0)
        End Function

        Public Function SetSecurityStampAsync(ByVal user As TUser, ByVal stamp As String, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            user.SecurityStamp = stamp
            Return Task.FromResult(0)
        End Function

        Public Function GetSecurityStampAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of String)
            Return Task.FromResult(user.SecurityStamp)
        End Function

        Public Function SetTwoFactorEnabledAsync(ByVal user As TUser, ByVal enabled As Boolean, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            user.TwoFactorEnabled = enabled
            Return Task.FromResult(0)
        End Function

        Public Function GetTwoFactorEnabledAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of Boolean)
            Return Task.FromResult(user.TwoFactorEnabled)
        End Function

        Public Function GetNormalizedUserNameAsync(ByVal user As TUser, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of String)
            Return Task.FromResult(user.NormalizedUserName)
        End Function

        Public Function SetNormalizedUserNameAsync(ByVal user As TUser, ByVal userName As String, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task
            user.NormalizedUserName = userName
            Return Task.FromResult(0)
        End Function

        Public Function GetUsersForClaimAsync(ByVal claim As Claim, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of IList(Of TUser))
            If claim Is Nothing Then
                Throw New ArgumentNullException(NameOf(claim))
            End If

            Dim query = From user In Users Where user.Claims.Where(Function(x) x.ClaimType = claim.Type AndAlso x.ClaimValue = claim.Value).FirstOrDefault() IsNot Nothing Select user
            Return Task.FromResult(Of IList(Of TUser))(query.ToList())
        End Function

        Public Function SetTokenAsync(ByVal user As TUser, ByVal loginProvider As String, ByVal name As String, ByVal value As String, ByVal cancellationToken As CancellationToken) As Task
            Dim tokenEntity = user.Tokens.SingleOrDefault(Function(l) l.TokenName = name AndAlso l.LoginProvider = loginProvider AndAlso l.UserId = user.Id)

            If tokenEntity IsNot Nothing Then
                tokenEntity.TokenValue = value
            Else
                user.Tokens.Add(New PocoUserToken With {
                    .UserId = user.Id,
                    .loginProvider = loginProvider,
                    .TokenName = name,
                    .TokenValue = value
                })
            End If

            Return Task.FromResult(0)
        End Function

        Public Function RemoveTokenAsync(ByVal user As TUser, ByVal loginProvider As String, ByVal name As String, ByVal cancellationToken As CancellationToken) As Task
            Dim tokenEntity = user.Tokens.SingleOrDefault(Function(l) l.TokenName = name AndAlso l.LoginProvider = loginProvider AndAlso l.UserId = user.Id)

            If tokenEntity IsNot Nothing Then
                user.Tokens.Remove(tokenEntity)
            End If

            Return Task.FromResult(0)
        End Function

        Public Function GetTokenAsync(ByVal user As TUser, ByVal loginProvider As String, ByVal name As String, ByVal cancellationToken As CancellationToken) As Task(Of String)
            Dim tokenEntity = user.Tokens.SingleOrDefault(Function(l) l.TokenName = name AndAlso l.LoginProvider = loginProvider AndAlso l.UserId = user.Id)
            Return Task.FromResult(tokenEntity?.TokenValue)
        End Function

        Private Const AuthenticatorStoreLoginProvider As String = "[AspNetAuthenticatorStore]"
        Private Const AuthenticatorKeyTokenName As String = "AuthenticatorKey"
        Private Const RecoveryCodeTokenName As String = "RecoveryCodes"

        Public Function SetAuthenticatorKeyAsync(ByVal user As TUser, ByVal key As String, ByVal cancellationToken As CancellationToken) As Task
            Return SetTokenAsync(user, AuthenticatorStoreLoginProvider, AuthenticatorKeyTokenName, key, cancellationToken)
        End Function

        Public Function GetAuthenticatorKeyAsync(ByVal user As TUser, ByVal cancellationToken As CancellationToken) As Task(Of String)
            Return GetTokenAsync(user, AuthenticatorStoreLoginProvider, AuthenticatorKeyTokenName, cancellationToken)
        End Function

        Public Function ReplaceCodesAsync(ByVal user As TUser, ByVal recoveryCodes As IEnumerable(Of String), ByVal cancellationToken As CancellationToken) As Task
            Dim mergedCodes = String.Join(";", recoveryCodes)
            Return SetTokenAsync(user, AuthenticatorStoreLoginProvider, RecoveryCodeTokenName, mergedCodes, cancellationToken)
        End Function

        Public Async Function RedeemCodeAsync(ByVal user As TUser, ByVal code As String, ByVal cancellationToken As CancellationToken) As Task(Of Boolean)
            Dim mergedCodes = If(Await GetTokenAsync(user, AuthenticatorStoreLoginProvider, RecoveryCodeTokenName, cancellationToken), "")
            Dim splitCodes = mergedCodes.Split(";"c)

            If splitCodes.Contains(code) Then
                Dim updatedCodes = New List(Of String)(splitCodes.Where(Function(s) s <> code))
                Await ReplaceCodesAsync(user, updatedCodes, cancellationToken)
                Return True
            End If

            Return False
        End Function

        Public Async Function CountCodesAsync(ByVal user As TUser, ByVal cancellationToken As CancellationToken) As Task(Of Integer)
            Dim mergedCodes = If(Await GetTokenAsync(user, AuthenticatorStoreLoginProvider, RecoveryCodeTokenName, cancellationToken), "")

            If mergedCodes.Length > 0 Then
                Return mergedCodes.Split(";"c).Length
            End If

            Return 0
        End Function
    End Class
End Namespace

