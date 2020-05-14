Imports System
Imports System.Data.Entity
Imports System.Globalization
Imports System.Linq
Imports System.Security.Claims
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security.DataProtection
Imports Xunit
Imports Moq

Namespace Identity.Test
    Public Class UserManagerTest
        <Fact>
        Public Sub UsersQueryableFailWhenStoreNotImplementedTest()
            Dim manager = New UserManager(Of IdentityUser)(New NoopUserStore())
            Assert.[False](manager.SupportsQueryableUsers)
            Assert.Throws(Of NotSupportedException)(Function() manager.Users.Count())
        End Sub

        <Fact>
        Public Sub UsersEmailMethodsFailWhenStoreNotImplementedTest()
            Dim manager = New UserManager(Of IdentityUser)(New NoopUserStore())
            Assert.[False](manager.SupportsUserEmail)
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.FindByEmailAsync(Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() manager.FindByEmail(Nothing))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.SetEmailAsync(Nothing, Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() manager.SetEmail(Nothing, Nothing))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.GetEmailAsync(Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() manager.GetEmail(Nothing))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.IsEmailConfirmedAsync(Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() manager.IsEmailConfirmed(Nothing))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.ConfirmEmailAsync(Nothing, Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() manager.ConfirmEmail(Nothing, Nothing))
        End Sub

        <Fact>
        Public Sub UsersPhoneNumberMethodsFailWhenStoreNotImplementedTest()
            Dim manager = New UserManager(Of IdentityUser)(New NoopUserStore())
            Assert.[False](manager.SupportsUserPhoneNumber)
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.SetPhoneNumberAsync(Nothing, Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() manager.SetPhoneNumber(Nothing, Nothing))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.GetPhoneNumberAsync(Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() manager.GetPhoneNumber(Nothing))
        End Sub

        <Fact>
        Public Sub TokenMethodsThrowWithNoTokenProviderTest()
            Dim manager = New UserManager(Of IdentityUser)(New NoopUserStore())
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.GenerateUserTokenAsync(Nothing, Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.VerifyUserTokenAsync(Nothing, Nothing, Nothing)))
        End Sub

        <Fact>
        Public Sub PasswordMethodsFailWhenStoreNotImplementedTest()
            Dim manager = New UserManager(Of IdentityUser)(New NoopUserStore())
            Assert.[False](manager.SupportsUserPassword)
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.CreateAsync(Nothing, Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.ChangePasswordAsync(Nothing, Nothing, Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.AddPasswordAsync(Nothing, Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.RemovePasswordAsync(Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.CheckPasswordAsync(Nothing, Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.HasPasswordAsync(Nothing)))
        End Sub

        <Fact>
        Public Sub SecurityStampMethodsFailWhenStoreNotImplementedTest()
            Dim manager = New UserManager(Of IdentityUser)(New NoopUserStore())
            Assert.[False](manager.SupportsUserSecurityStamp)
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.UpdateSecurityStampAsync("bogus")))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.GetSecurityStampAsync("bogus")))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.VerifyChangePhoneNumberTokenAsync("bogus", "1", "111-111-1111")))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.GenerateChangePhoneNumberTokenAsync("bogus", "111-111-1111")))
        End Sub

        <Fact>
        Public Sub LoginMethodsFailWhenStoreNotImplementedTest()
            Dim manager = New UserManager(Of IdentityUser)(New NoopUserStore())
            Assert.[False](manager.SupportsUserLogin)
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.AddLoginAsync("bogus", Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.RemoveLoginAsync("bogus", Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.GetLoginsAsync("bogus")))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.FindAsync(Nothing)))
        End Sub

        <Fact>
        Public Sub ClaimMethodsFailWhenStoreNotImplementedTest()
            Dim manager = New UserManager(Of IdentityUser)(New NoopUserStore())
            Assert.[False](manager.SupportsUserClaim)
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.AddClaimAsync("bogus", Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.RemoveClaimAsync("bogus", Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.GetClaimsAsync("bogus")))
        End Sub

        <Fact>
        Public Sub TwoFactorStoreMethodsFailWhenStoreNotImplementedTest()
            Dim manager = New UserManager(Of IdentityUser)(New NoopUserStore())
            Assert.[False](manager.SupportsUserTwoFactor)
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.GetTwoFactorEnabledAsync("bogus")))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.SetTwoFactorEnabledAsync("bogus", True)))
        End Sub

        <Fact>
        Public Sub RoleMethodsFailWhenStoreNotImplementedTest()
            Dim manager = New UserManager(Of IdentityUser)(New NoopUserStore())
            Assert.[False](manager.SupportsUserRole)
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.AddToRoleAsync("bogus", Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.GetRolesAsync("bogus")))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.RemoveFromRoleAsync("bogus", Nothing)))
            Assert.Throws(Of NotSupportedException)(Function() AsyncHelper.RunSync(Function() manager.IsInRoleAsync("bogus", "bogus")))
        End Sub

        <Fact>
        Public Sub DisposeAfterDisposeWorksTest()
            Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)())
            manager.Dispose()
            manager.Dispose()
        End Sub

        <Fact>
        Public Sub ManagerPublicNullCheckTest()
            ExceptionHelper.ThrowsArgumentNull(Function() New UserValidator(Of IdentityUser)(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() New UserManager(Of IdentityUser)(Nothing), "store")
            Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)())
            ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.UserValidator.ValidateAsync(Nothing)), "item")
            ExceptionHelper.ThrowsArgumentNull(Function() CSharpImpl.__Assign(manager.ClaimsIdentityFactory, Nothing), "value")
            ExceptionHelper.ThrowsArgumentNull(Function() CSharpImpl.__Assign(manager.UserValidator, Nothing), "value")
            ExceptionHelper.ThrowsArgumentNull(Function() CSharpImpl.__Assign(manager.PasswordValidator, Nothing), "value")
            ExceptionHelper.ThrowsArgumentNull(Function() CSharpImpl.__Assign(manager.PasswordHasher, Nothing), "value")
            ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.CreateIdentityAsync(Nothing, "whatever")), "user")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.CreateIdentity(Nothing, "whatever"), "user")
            ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.CreateAsync(Nothing)), "user")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.Create(Nothing), "user")
            ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.CreateAsync(Nothing, Nothing)), "user")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.Create(Nothing, Nothing), "user")
            ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.CreateAsync(New IdentityUser(), Nothing)), "password")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.Create(New IdentityUser(), Nothing), "password")
            ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.UpdateAsync(Nothing)), "user")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.Update(Nothing), "user")
            ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.DeleteAsync(Nothing)), "user")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.Delete(Nothing), "user")
            ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.AddClaimAsync("bogus", Nothing)), "claim")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.AddClaim("bogus", Nothing), "claim")
            ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.FindByNameAsync(Nothing)), "userName")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.FindByName(Nothing), "userName")
            ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.FindAsync(Nothing, Nothing)), "userName")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.Find(Nothing, Nothing), "userName")
            ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.AddLoginAsync("bogus", Nothing)), "login")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.AddLogin("bogus", Nothing), "login")
            ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.RemoveLoginAsync("bogus", Nothing)), "login")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.RemoveLogin("bogus", Nothing), "login")
            ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.FindByEmailAsync(Nothing)), "email")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.FindByEmail(Nothing), "email")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.RegisterTwoFactorProvider(Nothing, Nothing), "twoFactorProvider")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.RegisterTwoFactorProvider("bogus", Nothing), "provider")
        End Sub

        <Fact>
        Public Sub MethodsFailWithUnknownUserTest()
            Dim db = UnitTestHelper.CreateDefaultDb()
            Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
            manager.UserTokenProvider = New NoOpTokenProvider()
            Dim [error] = "UserId not found."
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.AddClaimAsync(Nothing, New Claim("a", "b"))), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.AddLoginAsync(Nothing, New UserLoginInfo("", ""))), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.AddPasswordAsync(Nothing, Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.AddToRoleAsync(Nothing, Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.AddToRolesAsync(Nothing, "")), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.ChangePasswordAsync(Nothing, Nothing, Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.GetClaimsAsync(Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.GetLoginsAsync(Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.GetRolesAsync(Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.IsInRoleAsync(Nothing, Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.RemoveClaimAsync(Nothing, New Claim("a", "b"))), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.RemoveLoginAsync(Nothing, New UserLoginInfo("", ""))), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.RemovePasswordAsync(Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.RemoveFromRoleAsync(Nothing, Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.RemoveFromRolesAsync(Nothing, "")), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.UpdateSecurityStampAsync(Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.GetSecurityStampAsync(Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.HasPasswordAsync(Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.GeneratePasswordResetTokenAsync(Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.ResetPasswordAsync(Nothing, Nothing, Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.IsEmailConfirmedAsync(Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.GenerateEmailConfirmationTokenAsync(Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.ConfirmEmailAsync(Nothing, Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.GetEmailAsync(Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.SetEmailAsync(Nothing, Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.IsPhoneNumberConfirmedAsync(Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.ChangePhoneNumberAsync(Nothing, Nothing, Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.VerifyChangePhoneNumberTokenAsync(Nothing, Nothing, Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.GetPhoneNumberAsync(Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.SetPhoneNumberAsync(Nothing, Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.GetTwoFactorEnabledAsync(Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.SetTwoFactorEnabledAsync(Nothing, True)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.GenerateTwoFactorTokenAsync(Nothing, Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.VerifyTwoFactorTokenAsync(Nothing, Nothing, Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.NotifyTwoFactorTokenAsync(Nothing, Nothing, Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.GetValidTwoFactorProvidersAsync(Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.VerifyUserTokenAsync(Nothing, Nothing, Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.AccessFailedAsync(Nothing)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.SetLockoutEnabledAsync(Nothing, False)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.SetLockoutEndDateAsync(Nothing, DateTimeOffset.UtcNow)), [error])
            ExceptionHelper.ThrowsWithError(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.IsLockedOutAsync(Nothing)), [error])
        End Sub

        <Fact>
        Public Sub MethodsThrowWhenDisposedTest()
            Dim db = UnitTestHelper.CreateDefaultDb()
            Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
            manager.Dispose()
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.AddClaimAsync("bogus", Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.AddClaim("bogus", Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.AddLoginAsync("bogus", Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.AddLogin("bogus", Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.AddPasswordAsync("bogus", Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.AddPassword("bogus", Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.AddToRoleAsync("bogus", Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.AddToRole("bogus", Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.AddToRolesAsync("bogus", Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.AddToRoles("bogus", Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.ChangePasswordAsync("bogus", Nothing, Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.ChangePassword("bogus", Nothing, Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.GetClaimsAsync("bogus")))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.GetClaims("bogus"))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.GetLoginsAsync("bogus")))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.GetLogins("bogus"))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.GetRolesAsync("bogus")))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.GetRoles("bogus"))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.IsInRoleAsync("bogus", Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.IsInRole("bogus", Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.RemoveClaimAsync("bogus", Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.RemoveClaim("bogus", Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.RemoveLoginAsync("bogus", Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.RemoveLogin("bogus", Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.RemovePasswordAsync("bogus")))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.RemovePassword("bogus"))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.RemoveFromRoleAsync("bogus", Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.RemoveFromRole("bogus", Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.RemoveFromRolesAsync("bogus", Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.RemoveFromRoles("bogus", Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.RemoveClaimAsync("bogus", Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.RemoveClaim("bogus", Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.FindAsync("bogus", Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.Find("bogus", Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.FindAsync(Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.Find(Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.FindByIdAsync(Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.FindById(Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.FindByNameAsync(Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.FindByName(Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.CreateAsync(Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.Create(Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.CreateAsync(Nothing, Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.Create(Nothing, Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.CreateIdentityAsync(Nothing, Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.CreateIdentity(Nothing, Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.UpdateAsync(Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.Update(Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.DeleteAsync(Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.Delete(Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.UpdateSecurityStampAsync(Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.UpdateSecurityStamp(Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.GetSecurityStampAsync(Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.GetSecurityStamp(Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.GeneratePasswordResetTokenAsync(Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.GeneratePasswordResetToken(Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.ResetPasswordAsync(Nothing, Nothing, Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.ResetPassword(Nothing, Nothing, Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.GenerateEmailConfirmationTokenAsync(Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.GenerateEmailConfirmationToken(Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.IsEmailConfirmedAsync(Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.IsEmailConfirmed(Nothing))
            Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.ConfirmEmailAsync(Nothing, Nothing)))
            Assert.Throws(Of ObjectDisposedException)(Function() manager.ConfirmEmail(Nothing, Nothing))
        End Sub

        <Fact>
        Public Sub SyncManagerNullCheckTest()
            Dim manager As UserManager(Of IdentityUser) = Nothing
            ExceptionHelper.ThrowsArgumentNull(Function() manager.AddClaim("bogus", Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.AddLogin("bogus", Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.AddPassword("bogus", Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.AddToRole("bogus", Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.AddToRoles("bogus", Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.ChangePassword("bogus", Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.HasPassword("bogus"), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.GetClaims("bogus"), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.GetLogins("bogus"), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.GetRoles("bogus"), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.IsInRole("bogus", Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.RemoveClaim("bogus", Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.RemoveLogin("bogus", Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.RemovePassword("bogus"), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.RemoveFromRole("bogus", Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.RemoveFromRoles("bogus", Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.RemoveClaim("bogus", Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.Find("bogus", Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.Find(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.FindById(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.FindByName(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.Create(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.Create(Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.CreateIdentity(Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.Update(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.Delete(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.UpdateSecurityStamp(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.GetSecurityStamp(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.IsEmailConfirmed(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.ConfirmEmail(Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.GeneratePasswordResetToken(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.ResetPassword(Nothing, Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.FindByEmail(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.GetEmail(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.SetEmail(Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.GenerateEmailConfirmationToken(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.GenerateTwoFactorToken(Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.VerifyTwoFactorToken(Nothing, Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.GetValidTwoFactorProviders(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.GetPhoneNumber(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.SetPhoneNumber(Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.IsPhoneNumberConfirmed(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.ChangePhoneNumber(Nothing, Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.VerifyChangePhoneNumberToken(Nothing, Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.GenerateUserToken(Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.VerifyUserToken(Nothing, Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.GetTwoFactorEnabled(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.SetTwoFactorEnabled(Nothing, True), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.SetLockoutEnabled(Nothing, True), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.GetLockoutEnabled(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.IsLockedOut(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.SetLockoutEndDate(Nothing, DateTimeOffset.UtcNow), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.GetLockoutEndDate(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.AccessFailed(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.GetAccessFailedCount(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.ResetAccessFailedCount(Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.CheckPassword(Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.GenerateChangePhoneNumberToken(Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.NotifyTwoFactorToken(Nothing, Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.SendSms(Nothing, Nothing), "manager")
            ExceptionHelper.ThrowsArgumentNull(Function() manager.SendEmail(Nothing, Nothing, Nothing), "manager")
        End Sub

        <Fact>
        Public Sub IdentityContextWithNullDbContextThrowsTest()
            ExceptionHelper.ThrowsArgumentNull(Function() New UserStore(Of IdentityUser)(Nothing), "context")
        End Sub

        <Fact>
        Public Async Function PasswordLengthSuccessValidatorTest() As Task
            Dim validator = New MinimumLengthValidator(1)
            Dim result = Await validator.ValidateAsync("11")
            UnitTestHelper.IsSuccess(result)
        End Function

        <Fact>
        Public Async Function PasswordTooShortValidatorTest() As Task
            Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)())
            UnitTestHelper.IsFailure(Await manager.CreateAsync(New IdentityUser("Hao"), "11"), "Passwords must be at least 6 characters.")
        End Function

        <Fact>
        Public Async Function CustomPasswordValidatorTest() As Task
            Dim manager = TestUtil.CreateManager()
            manager.PasswordValidator = New AlwaysBadValidator(Of String)()
            UnitTestHelper.IsFailure(Await manager.CreateAsync(New IdentityUser("Hao"), "password"), AlwaysBadValidator(Of String).ErrorMessage)
        End Function

        <Fact>
        Public Async Function PasswordValidatorTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("passwordValidator")
            manager.PasswordValidator = New PasswordValidator With {
                .RequiredLength = 6,
                .RequireNonLetterOrDigit = True
            }
            Const alphaError As String = "Passwords must have at least one non letter or digit character."
            Const lengthError As String = "Passwords must be at least 6 characters."
            UnitTestHelper.IsFailure(Await manager.CreateAsync(user, "ab@de"), lengthError)
            UnitTestHelper.IsFailure(Await manager.CreateAsync(user, "abcdef"), alphaError)
            UnitTestHelper.IsFailure(Await manager.CreateAsync(user, "___"), lengthError)
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user, "abcd@e!ld!kajfd"))
            UnitTestHelper.IsFailure(Await manager.CreateAsync(user, "abcde"), lengthError & " " & alphaError)
        End Function

        <Fact>
        Public Async Function CustomPasswordValidatorBlocksAddPasswordTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("test")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            manager.PasswordValidator = New AlwaysBadValidator(Of String)()
            UnitTestHelper.IsFailure(Await manager.AddPasswordAsync(user.Id, "password"), AlwaysBadValidator(Of String).ErrorMessage)
        End Function

        <Fact>
        Public Async Function CustomUserNameValidatorTest() As Task
            Dim manager = TestUtil.CreateManager()
            manager.UserValidator = New AlwaysBadValidator(Of IdentityUser)()
            Dim result = Await manager.CreateAsync(New IdentityUser("Hao"))
            UnitTestHelper.IsFailure(result, AlwaysBadValidator(Of IdentityUser).ErrorMessage)
        End Function

        <Fact>
        Public Async Function BadValidatorBlocksAllUpdatesTest() As Task
            Dim db = UnitTestHelper.CreateDefaultDb()
            Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
            Dim user = New IdentityUser("poorguy")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Const [error] As String = AlwaysBadValidator(Of IdentityUser).ErrorMessage
            manager.UserValidator = New AlwaysBadValidator(Of IdentityUser)()
            manager.PasswordValidator = New NoopValidator(Of String)()
            UnitTestHelper.IsFailure(Await manager.AddClaimAsync(user.Id, New Claim("a", "b")), [error])
            UnitTestHelper.IsFailure(Await manager.AddLoginAsync(user.Id, New UserLoginInfo("", "")), [error])
            UnitTestHelper.IsFailure(Await manager.AddPasswordAsync(user.Id, "a"), [error])
            UnitTestHelper.IsFailure(Await manager.ChangePasswordAsync(user.Id, "a", "b"), [error])
            UnitTestHelper.IsFailure(Await manager.RemoveClaimAsync(user.Id, New Claim("a", "b")), [error])
            UnitTestHelper.IsFailure(Await manager.RemoveLoginAsync(user.Id, New UserLoginInfo("aa", "bb")), [error])
            UnitTestHelper.IsFailure(Await manager.RemovePasswordAsync(user.Id), [error])
            UnitTestHelper.IsFailure(Await manager.UpdateSecurityStampAsync(user.Id), [error])
        End Function

        <Fact>
        Public Async Function CreateLocalUserWithOnlyWhitespaceUserNameFails() As Task
            Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)())
            Dim result = Await manager.CreateAsync(New IdentityUser With {
                .UserName = " "
            }, "password")
            UnitTestHelper.IsFailure(result, "Name cannot be null or empty.")
        End Function

        <Fact>
        Public Async Function CreateLocalUserWithInvalidUserNameFails() As Task
            Dim manager = TestUtil.CreateManager()
            Dim result = Await manager.CreateAsync(New IdentityUser With {
                .UserName = "a" & vbNullChar & "b"
            }, "password")
            UnitTestHelper.IsFailure(result, "User name a" & vbNullChar & "b is invalid, can only contain letters or digits.")
        End Function

        <Fact>
        Public Async Function CreateLocalUserWithInvalidPasswordThrows() As Task
            Dim manager = TestUtil.CreateManager()
            Dim result = Await manager.CreateAsync(New IdentityUser("Hao"), "aa")
            UnitTestHelper.IsFailure(result, "Passwords must be at least 6 characters.")
        End Function

        <Fact>
        Public Async Function CreateExternalUserWithNullFails() As Task
            Dim manager = TestUtil.CreateManager()
            UnitTestHelper.IsFailure(Await manager.CreateAsync(New IdentityUser With {
                .UserName = Nothing
            }), "Name cannot be null or empty.")
        End Function

        <Fact>
        Public Async Function AddPasswordWhenPasswordSetFails() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("HasPassword")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user, "password"))
            UnitTestHelper.IsFailure(Await manager.AddPasswordAsync(user.Id, "User already has a password."))
        End Function

        Private Async Function LazyLoadTestSetup(ByVal db As DbContext, ByVal user As IdentityUser) As Task
            Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
            Dim role = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(db))
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            UnitTestHelper.IsSuccess(Await manager.AddLoginAsync(user.Id, New UserLoginInfo("provider", "key")))
            UnitTestHelper.IsSuccess(Await role.CreateAsync(New IdentityRole("Admin")))
            UnitTestHelper.IsSuccess(Await role.CreateAsync(New IdentityRole("Local")))
            UnitTestHelper.IsSuccess(Await manager.AddToRoleAsync(user.Id, "Admin"))
            UnitTestHelper.IsSuccess(Await manager.AddToRoleAsync(user.Id, "Local"))
            Dim userClaims As Claim() = {New Claim("Whatever", "Value"), New Claim("Whatever2", "Value2")}

            For Each c In userClaims
                UnitTestHelper.IsSuccess(Await manager.AddClaimAsync(user.Id, c))
            Next
        End Function

        <Fact>
        Public Async Function LazyLoadDisabledFindByIdTest() As Task
            Dim db = UnitTestHelper.CreateDefaultDb()
            Dim user = New IdentityUser("Hao")
            user.Email = "hao@foo.com"
            Await LazyLoadTestSetup(db, user)
            db = New IdentityDbContext()
            db.Configuration.LazyLoadingEnabled = False
            db.Configuration.ProxyCreationEnabled = False
            Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
            Dim userById = Await manager.FindByIdAsync(user.Id)
            Assert.[True](userById.Claims.Count = 2)
            Assert.[True](userById.Logins.Count = 1)
            Assert.[True](userById.Roles.Count = 2)
        End Function

        <Fact>
        Public Async Function LazyLoadDisabledFindByNameTest() As Task
            Dim db = UnitTestHelper.CreateDefaultDb()
            Dim user = New IdentityUser("Hao") With {
                .Email = "hao@foo.com"
            }
            Await LazyLoadTestSetup(db, user)
            db = New IdentityDbContext()
            db.Configuration.LazyLoadingEnabled = False
            db.Configuration.ProxyCreationEnabled = False
            Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
            Dim userByName = Await manager.FindByNameAsync(user.UserName)
            Assert.[True](userByName.Claims.Count = 2)
            Assert.[True](userByName.Logins.Count = 1)
            Assert.[True](userByName.Roles.Count = 2)
        End Function

        <Fact>
        Public Async Function LazyLoadDisabledFindByLoginTest() As Task
            Dim db = UnitTestHelper.CreateDefaultDb()
            Dim user = New IdentityUser("Hao") With {
                .Email = "hao@foo.com"
            }
            Await LazyLoadTestSetup(db, user)
            db = New IdentityDbContext()
            db.Configuration.LazyLoadingEnabled = False
            db.Configuration.ProxyCreationEnabled = False
            Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
            Dim userByLogin = Await manager.FindAsync(New UserLoginInfo("provider", "key"))
            Assert.[True](userByLogin.Claims.Count = 2)
            Assert.[True](userByLogin.Logins.Count = 1)
            Assert.[True](userByLogin.Roles.Count = 2)
        End Function

        <Fact>
        Public Async Function LazyLoadDisabledFindByEmailTest() As Task
            Dim db = UnitTestHelper.CreateDefaultDb()
            Dim user = New IdentityUser("Hao") With {
                .Email = "hao@foo.com"
            }
            Await LazyLoadTestSetup(db, user)
            db = New IdentityDbContext()
            db.Configuration.LazyLoadingEnabled = False
            db.Configuration.ProxyCreationEnabled = False
            Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
            Dim userByEmail = Await manager.FindByEmailAsync(user.Email)
            Assert.[True](userByEmail.Claims.Count = 2)
            Assert.[True](userByEmail.Logins.Count = 1)
            Assert.[True](userByEmail.Roles.Count = 2)
        End Function

        <Fact>
        Public Async Function FindNullIdTest() As Task
            Dim manager = TestUtil.CreateManager()
            Assert.Null(Await manager.FindByIdAsync(Nothing))
        End Function

        <Fact>
        Public Async Function CreateLocalUserTest() As Task
            Dim manager = TestUtil.CreateManager()
            Const password As String = "password"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(New IdentityUser("CreateLocalUserTest"), password))
            Dim user = Await manager.FindByNameAsync("CreateLocalUserTest")
            Assert.NotNull(user)
            Assert.NotNull(user.PasswordHash)
            Assert.[True](Await manager.HasPasswordAsync(user.Id))
            Dim logins = Await manager.GetLoginsAsync(user.Id)
            Assert.NotNull(logins)
            Assert.Equal(0, logins.Count())
        End Function

        <Fact>
        Public Sub CreateLocalUserTestSync()
            Dim manager = TestUtil.CreateManager()
            Const password As String = "password"
            UnitTestHelper.IsSuccess(manager.Create(New IdentityUser("CreateLocalUserTest"), password))
            Dim user = manager.FindByName("CreateLocalUserTest")
            Assert.NotNull(user)
            Assert.NotNull(user.PasswordHash)
            Assert.[True](manager.HasPassword(user.Id))
            Dim logins = manager.GetLogins(user.Id)
            Assert.NotNull(logins)
            Assert.Equal(0, logins.Count())
        End Sub

        <Fact>
        Public Async Function DeleteUserTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("Delete")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            UnitTestHelper.IsSuccess(Await manager.DeleteAsync(user))
            Assert.Null(Await manager.FindByIdAsync(user.Id))
        End Function

        <Fact>
        Public Sub DeleteUserSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("Delete")
            UnitTestHelper.IsSuccess(manager.Create(user))
            UnitTestHelper.IsSuccess(manager.Delete(user))
            Assert.Null(manager.FindById(user.Id))
        End Sub

        <Fact>
        Public Async Function CreateUserAddLoginTest() As Task
            Dim manager = TestUtil.CreateManager()
            Const userName As String = "CreateExternalUserTest"
            Const provider As String = "ZzAuth"
            Const providerKey As String = "HaoKey"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(New IdentityUser(userName)))
            Dim user = Await manager.FindByNameAsync(userName)
            Dim login = New UserLoginInfo(provider, providerKey)
            UnitTestHelper.IsSuccess(Await manager.AddLoginAsync(user.Id, login))
            Dim logins = Await manager.GetLoginsAsync(user.Id)
            Assert.NotNull(logins)
            Assert.Equal(1, logins.Count())
            Assert.Equal(provider, logins.First().LoginProvider)
            Assert.Equal(providerKey, logins.First().ProviderKey)
        End Function

        <Fact>
        Public Sub CreateUserAddLoginSyncTest()
            Dim manager = TestUtil.CreateManager()
            Const userName As String = "CreateExternalUserTest"
            Const provider As String = "ZzAuth"
            Const providerKey As String = "HaoKey"
            UnitTestHelper.IsSuccess(manager.Create(New IdentityUser(userName)))
            Dim user = manager.FindByName(userName)
            Dim login = New UserLoginInfo(provider, providerKey)
            UnitTestHelper.IsSuccess(manager.AddLogin(user.Id, login))
            Dim logins = manager.GetLogins(user.Id)
            Assert.NotNull(logins)
            Assert.Equal(1, logins.Count())
            Assert.Equal(provider, logins.First().LoginProvider)
            Assert.Equal(providerKey, logins.First().ProviderKey)
        End Sub

        <Fact>
        Public Async Function CreateUserLoginAndAddPasswordTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim login = New UserLoginInfo("Provider", "key")
            Dim user = New IdentityUser("CreateUserLoginAddPasswordTest")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            UnitTestHelper.IsSuccess(Await manager.AddLoginAsync(user.Id, login))
            UnitTestHelper.IsSuccess(Await manager.AddPasswordAsync(user.Id, "password"))
            Dim logins = Await manager.GetLoginsAsync(user.Id)
            Assert.NotNull(logins)
            Assert.Equal(1, logins.Count())
            Assert.Equal(user, Await manager.FindAsync(login))
            Assert.Equal(user, Await manager.FindAsync(user.UserName, "password"))
            Assert.[True](Await manager.CheckPasswordAsync(user, "password"))
        End Function

        <Fact>
        Public Sub CreateUserLoginAndAddPasswordSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim login = New UserLoginInfo("Provider", "key")
            Dim user = New IdentityUser("CreateUserLoginAddPasswordTest")
            UnitTestHelper.IsSuccess(manager.Create(user))
            UnitTestHelper.IsSuccess(manager.AddLogin(user.Id, login))
            UnitTestHelper.IsSuccess(manager.AddPassword(user.Id, "password"))
            Dim logins = manager.GetLogins(user.Id)
            Assert.NotNull(logins)
            Assert.Equal(1, logins.Count())
            Assert.Equal(user, manager.Find(login))
            Assert.Equal(user, manager.Find(user.UserName, "password"))
            Assert.[True](manager.CheckPassword(user, "password"))
        End Sub

        <Fact>
        Public Async Function CreateUserAddRemoveLoginTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("CreateUserAddRemoveLoginTest")
            Dim login = New UserLoginInfo("Provider", "key")
            Const password As String = "password"
            Dim result = Await manager.CreateAsync(user, password)
            Assert.NotNull(user)
            UnitTestHelper.IsSuccess(result)
            UnitTestHelper.IsSuccess(Await manager.AddLoginAsync(user.Id, login))
            Assert.Equal(user, Await manager.FindAsync(login))
            Dim logins = Await manager.GetLoginsAsync(user.Id)
            Assert.NotNull(logins)
            Assert.Equal(1, logins.Count())
            Assert.Equal(login.LoginProvider, logins.Last().LoginProvider)
            Assert.Equal(login.ProviderKey, logins.Last().ProviderKey)
            Dim stamp = Await manager.GetSecurityStampAsync(user.Id)
            UnitTestHelper.IsSuccess(Await manager.RemoveLoginAsync(user.Id, login))
            Assert.Null(Await manager.FindAsync(login))
            logins = Await manager.GetLoginsAsync(user.Id)
            Assert.NotNull(logins)
            Assert.Equal(0, logins.Count())
            Assert.NotEqual(stamp, user.SecurityStamp)
        End Function

        <Fact>
        Public Sub CreateUserAddRemoveLoginSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("CreateUserAddRemoveLoginTest")
            Dim login = New UserLoginInfo("Provider", "key")
            Const password As String = "password"
            Dim result = manager.Create(user, password)
            Assert.NotNull(user)
            UnitTestHelper.IsSuccess(result)
            UnitTestHelper.IsSuccess(manager.AddLogin(user.Id, login))
            Assert.Equal(user, manager.Find(login))
            Dim logins = manager.GetLogins(user.Id)
            Assert.NotNull(logins)
            Assert.Equal(1, logins.Count())
            Assert.Equal(login.LoginProvider, logins.Last().LoginProvider)
            Assert.Equal(login.ProviderKey, logins.Last().ProviderKey)
            Dim stamp = manager.GetSecurityStamp(user.Id)
            UnitTestHelper.IsSuccess(manager.RemoveLogin(user.Id, login))
            Assert.Null(manager.Find(login))
            logins = manager.GetLogins(user.Id)
            Assert.NotNull(logins)
            Assert.Equal(0, logins.Count())
            Assert.NotEqual(stamp, user.SecurityStamp)
        End Sub

        <Fact>
        Public Async Function RemovePasswordTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("RemovePasswordTest")
            Const password As String = "password"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user, password))
            Dim stamp = Await manager.GetSecurityStampAsync(user.Id)
            UnitTestHelper.IsSuccess(Await manager.RemovePasswordAsync(user.Id))
            Dim u = Await manager.FindByNameAsync(user.UserName)
            Assert.NotNull(u)
            Assert.Null(u.PasswordHash)
            Assert.NotEqual(stamp, user.SecurityStamp)
        End Function

        <Fact>
        Public Sub RemovePasswordSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("RemovePasswordTest")
            Const password As String = "password"
            UnitTestHelper.IsSuccess(manager.Create(user, password))
            Dim stamp = manager.GetSecurityStamp(user.Id)
            UnitTestHelper.IsSuccess(manager.RemovePassword(user.Id))
            Dim u = manager.FindByName(user.UserName)
            Assert.NotNull(u)
            Assert.Null(u.PasswordHash)
            Assert.NotEqual(stamp, user.SecurityStamp)
        End Sub

        <Fact>
        Public Async Function ChangePasswordTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("ChangePasswordTest")
            Const password As String = "password"
            Const newPassword As String = "newpassword"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user, password))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            UnitTestHelper.IsSuccess(Await manager.ChangePasswordAsync(user.Id, password, newPassword))
            Assert.Null(Await manager.FindAsync(user.UserName, password))
            Assert.Equal(user, Await manager.FindAsync(user.UserName, newPassword))
            Assert.NotEqual(stamp, user.SecurityStamp)
        End Function

        <Fact>
        Public Sub ChangePasswordSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("ChangePasswordTest")
            Const password As String = "password"
            Const newPassword As String = "newpassword"
            UnitTestHelper.IsSuccess(manager.Create(user, password))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            UnitTestHelper.IsSuccess(manager.ChangePassword(user.Id, password, newPassword))
            Assert.Null(manager.Find(user.UserName, password))
            Assert.Equal(user, manager.Find(user.UserName, newPassword))
            Assert.NotEqual(stamp, user.SecurityStamp)
        End Sub

        <Fact>
        Public Async Function ResetPasswordTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("ResetPasswordTest")
            Const password As String = "password"
            Const newPassword As String = "newpassword"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user, password))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = Await manager.GeneratePasswordResetTokenAsync(user.Id)
            Assert.NotNull(token)
            UnitTestHelper.IsSuccess(Await manager.ResetPasswordAsync(user.Id, token, newPassword))
            Assert.Null(Await manager.FindAsync(user.UserName, password))
            Assert.Equal(user, Await manager.FindAsync(user.UserName, newPassword))
            Assert.NotEqual(stamp, user.SecurityStamp)
        End Function

        <Fact>
        Public Async Function ResetPasswordWithNoStampTest() As Task
            Dim manager = New NoStampUserManager()
            Dim user = New IdentityUser("ResetPasswordTest")
            Const password As String = "password"
            Const newPassword As String = "newpassword"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user, password))
            Dim token = Await manager.GeneratePasswordResetTokenAsync(user.Id)
            Assert.NotNull(token)
            UnitTestHelper.IsSuccess(Await manager.ResetPasswordAsync(user.Id, token, newPassword))
            Assert.Null(Await manager.FindAsync(user.UserName, password))
            Assert.Equal(user, Await manager.FindAsync(user.UserName, newPassword))
        End Function

        <Fact>
        Public Async Function GenerateUserTokenTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("UserTokenTest")
            Dim user2 = New IdentityUser("UserTokenTest2")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user2))
            Dim token = Await manager.GenerateUserTokenAsync("test", user.Id)
            Assert.[True](Await manager.VerifyUserTokenAsync(user.Id, "test", token))
            Assert.[False](Await manager.VerifyUserTokenAsync(user.Id, "test2", token))
            Assert.[False](Await manager.VerifyUserTokenAsync(user.Id, "test", token & "a"))
            Assert.[False](Await manager.VerifyUserTokenAsync(user2.Id, "test", token))
        End Function

        <Fact>
        Public Sub GenerateUserTokenSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("UserTokenTest")
            Dim user2 = New IdentityUser("UserTokenTest2")
            UnitTestHelper.IsSuccess(manager.Create(user))
            UnitTestHelper.IsSuccess(manager.Create(user2))
            Dim token = manager.GenerateUserToken("test", user.Id)
            Assert.[True](manager.VerifyUserToken(user.Id, "test", token))
            Assert.[False](manager.VerifyUserToken(user.Id, "test2", token))
            Assert.[False](manager.VerifyUserToken(user.Id, "test", token & "a"))
            Assert.[False](manager.VerifyUserToken(user2.Id, "test", token))
        End Sub

        <Fact>
        Public Async Function GetTwoFactorEnabledTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("TwoFactorEnabledTest")
            UnitTestHelper.IsSuccess(manager.Create(user))
            Assert.[False](Await manager.GetTwoFactorEnabledAsync(user.Id))
            UnitTestHelper.IsSuccess(Await manager.SetTwoFactorEnabledAsync(user.Id, True))
            Assert.[True](Await manager.GetTwoFactorEnabledAsync(user.Id))
            UnitTestHelper.IsSuccess(Await manager.SetTwoFactorEnabledAsync(user.Id, False))
            Assert.[False](Await manager.GetTwoFactorEnabledAsync(user.Id))
        End Function

        <Fact>
        Public Sub GetTwoFactorEnabledSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("TwoFactorEnabledTest")
            UnitTestHelper.IsSuccess(manager.Create(user))
            Assert.[False](manager.GetTwoFactorEnabled(user.Id))
            UnitTestHelper.IsSuccess(manager.SetTwoFactorEnabled(user.Id, True))
            Assert.[True](manager.GetTwoFactorEnabled(user.Id))
            UnitTestHelper.IsSuccess(manager.SetTwoFactorEnabled(user.Id, False))
            Assert.[False](manager.GetTwoFactorEnabled(user.Id))
        End Sub

        <Fact>
        Public Async Function ResetPasswordWithConfirmTokenFailsTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("ResetPasswordTest")
            Dim password = "password"
            Dim newPassword = "newpassword"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user, password))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = Await manager.GenerateEmailConfirmationTokenAsync(user.Id)
            Assert.NotNull(token)
            UnitTestHelper.IsFailure(Await manager.ResetPasswordAsync(user.Id, token, newPassword))
            Assert.Null(Await manager.FindAsync(user.UserName, newPassword))
            Assert.Equal(user, Await manager.FindAsync(user.UserName, password))
            Assert.Equal(stamp, user.SecurityStamp)
        End Function

        <Fact>
        Public Async Function ResetPasswordWithExpiredTokenFailsTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim provider = New DpapiDataProtectionProvider()
            manager.UserTokenProvider = New DataProtectorTokenProvider(Of IdentityUser)(provider.Create("ResetPassword")) With {
                .TokenLifespan = TimeSpan.FromTicks(0)
            }
            Dim user = New IdentityUser("ResetPasswordTest")
            Dim password = "password"
            Dim newPassword = "newpassword"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user, password))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = Await manager.GeneratePasswordResetTokenAsync(user.Id)
            Assert.NotNull(token)
            Thread.Sleep(10)
            UnitTestHelper.IsFailure(Await manager.ResetPasswordAsync(user.Id, token, newPassword))
            Assert.Null(Await manager.FindAsync(user.UserName, newPassword))
            Assert.Equal(user, Await manager.FindAsync(user.UserName, password))
            Assert.Equal(stamp, user.SecurityStamp)
        End Function

        <Fact>
        Public Sub ResetPasswordSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("ResetPasswordTest")
            Dim password = "password"
            Dim newPassword = "newpassword"
            UnitTestHelper.IsSuccess(manager.Create(user, password))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = manager.GeneratePasswordResetToken(user.Id)
            Assert.NotNull(token)
            UnitTestHelper.IsSuccess(manager.ResetPassword(user.Id, token, newPassword))
            Assert.Null(manager.Find(user.UserName, password))
            Assert.Equal(user, manager.Find(user.UserName, newPassword))
            Assert.NotEqual(stamp, user.SecurityStamp)
        End Sub

        <Fact>
        Public Async Function ResetPasswordFailsWithWrongTokenTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("ResetPasswordTest")
            Dim password = "password"
            Dim newPassword = "newpassword"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user, password))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            UnitTestHelper.IsFailure(Await manager.ResetPasswordAsync(user.Id, "bogus", newPassword), "Invalid token.")
            Assert.Null(Await manager.FindAsync(user.UserName, newPassword))
            Assert.Equal(user, Await manager.FindAsync(user.UserName, password))
            Assert.Equal(stamp, user.SecurityStamp)
        End Function

        <Fact>
        Public Async Function ResetPasswordFailsAfterPasswordChangeTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("ResetPasswordTest")
            Dim password = "password"
            Dim newPassword = "newpassword"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user, password))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = manager.GeneratePasswordResetToken(user.Id)
            Assert.NotNull(token)
            UnitTestHelper.IsSuccess(Await manager.ChangePasswordAsync(user.Id, password, "bogus1"))
            UnitTestHelper.IsFailure(Await manager.ResetPasswordAsync(user.Id, token, newPassword), "Invalid token.")
            Assert.Null(Await manager.FindAsync(user.UserName, newPassword))
            Assert.Equal(user, Await manager.FindAsync(user.UserName, "bogus1"))
        End Function

        <Fact>
        Public Async Function AddRemoveUserClaimTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("ClaimsAddRemove")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Dim claims As Claim() = {New Claim("c", "v"), New Claim("c2", "v2"), New Claim("c2", "v3")}

            For Each c As Claim In claims
                UnitTestHelper.IsSuccess(Await manager.AddClaimAsync(user.Id, c))
            Next

            Dim userClaims = Await manager.GetClaimsAsync(user.Id)
            Assert.Equal(3, userClaims.Count)
            UnitTestHelper.IsSuccess(Await manager.RemoveClaimAsync(user.Id, claims(0)))
            userClaims = Await manager.GetClaimsAsync(user.Id)
            Assert.Equal(2, userClaims.Count)
            UnitTestHelper.IsSuccess(Await manager.RemoveClaimAsync(user.Id, claims(1)))
            userClaims = Await manager.GetClaimsAsync(user.Id)
            Assert.Equal(1, userClaims.Count)
            UnitTestHelper.IsSuccess(Await manager.RemoveClaimAsync(user.Id, claims(2)))
            userClaims = Await manager.GetClaimsAsync(user.Id)
            Assert.Equal(0, userClaims.Count)
        End Function

        <Fact>
        Public Sub AddRemoveUserClaimSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("ClaimsAddRemove")
            UnitTestHelper.IsSuccess(manager.Create(user))
            Dim claims As Claim() = {New Claim("c", "v"), New Claim("c2", "v2"), New Claim("c2", "v3")}

            For Each c As Claim In claims
                UnitTestHelper.IsSuccess(manager.AddClaim(user.Id, c))
            Next

            Dim userClaims = manager.GetClaims(user.Id)
            Assert.Equal(3, userClaims.Count)
            UnitTestHelper.IsSuccess(manager.RemoveClaim(user.Id, claims(0)))
            userClaims = manager.GetClaims(user.Id)
            Assert.Equal(2, userClaims.Count)
            UnitTestHelper.IsSuccess(manager.RemoveClaim(user.Id, claims(1)))
            userClaims = manager.GetClaims(user.Id)
            Assert.Equal(1, userClaims.Count)
            UnitTestHelper.IsSuccess(manager.RemoveClaim(user.Id, claims(2)))
            userClaims = manager.GetClaims(user.Id)
            Assert.Equal(0, userClaims.Count)
        End Sub

        <Fact>
        Public Async Function ChangePasswordFallsIfPasswordTooShortTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("user")
            Dim password = "password"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user, password))
            Dim result = Await manager.ChangePasswordAsync(user.Id, password, "n")
            UnitTestHelper.IsFailure(result, "Passwords must be at least 6 characters.")
        End Function

        <Fact>
        Public Async Function ChangePasswordFallsIfPasswordWrongTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("user")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user, "password"))
            Dim result = Await manager.ChangePasswordAsync(user.Id, "bogus", "newpassword")
            UnitTestHelper.IsFailure(result, "Incorrect password.")
        End Function

        <Fact>
        Public Sub ChangePasswordFallsIfPasswordWrongSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("user")
            UnitTestHelper.IsSuccess(manager.Create(user, "password"))
            Dim result = manager.ChangePassword(user.Id, "bogus", "newpassword")
            UnitTestHelper.IsFailure(result, "Incorrect password.")
        End Sub

        <Fact>
        Public Async Function CanRelaxUserNameAndPasswordValidationTest() As Task
            Dim manager = TestUtil.CreateManager()
            manager.UserValidator = New UserValidator(Of IdentityUser)(manager) With {
                .AllowOnlyAlphanumericUserNames = False
            }
            manager.PasswordValidator = New MinimumLengthValidator(1)
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(New IdentityUser("Some spaces"), "pwd"))
        End Function

        <Fact>
        Public Async Function CanUseEmailAsUserNameTest() As Task
            Dim manager = TestUtil.CreateManager()
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(New IdentityUser("test_email@foo.com")))
        End Function

        <Fact>
        Public Async Function AddDupeUserFailsTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("dupe")
            Dim user2 = New IdentityUser("dupe")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            UnitTestHelper.IsFailure(Await manager.CreateAsync(user2), "Name dupe is already taken.")
        End Function

        <Fact>
        Public Async Function FindWithPasswordUnknownUserReturnsNullTest() As Task
            Dim manager = TestUtil.CreateManager()
            Assert.Null(Await manager.FindAsync("bogus", "sdlkfsadf"))
            Assert.Null(manager.Find("bogus", "sdlkfsadf"))
        End Function

        <Fact>
        Public Async Function UpdateSecurityStampTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("stampMe")
            Assert.Null(user.SecurityStamp)
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            UnitTestHelper.IsSuccess(Await manager.UpdateSecurityStampAsync(user.Id))
            Assert.NotEqual(stamp, user.SecurityStamp)
        End Function

        <Fact>
        Public Sub UpdateSecurityStampSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("stampMe")
            Assert.Null(user.SecurityStamp)
            UnitTestHelper.IsSuccess(manager.Create(user))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            UnitTestHelper.IsSuccess(manager.UpdateSecurityStamp(user.Id))
            Assert.NotEqual(stamp, user.SecurityStamp)
        End Sub

        <Fact>
        Public Async Function AddDupeLoginFailsTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("DupeLogin")
            Dim login = New UserLoginInfo("provder", "key")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            UnitTestHelper.IsSuccess(Await manager.AddLoginAsync(user.Id, login))
            Dim result = Await manager.AddLoginAsync(user.Id, login)
            UnitTestHelper.IsFailure(result, "A user with that external login already exists.")
        End Function

        <Fact>
        Public Async Function AddLoginDoesNotChangeStampTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("stampTest")
            Dim login = New UserLoginInfo("provder", "key")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Dim stamp = Await manager.GetSecurityStampAsync(user.Id)
            UnitTestHelper.IsSuccess(Await manager.AddLoginAsync(user.Id, login))
            Assert.Equal(stamp, Await manager.GetSecurityStampAsync(user.Id))
        End Function

        <Fact>
        Public Async Function MixManagerAndEfTest() As Task
            Dim db = UnitTestHelper.CreateDefaultDb()
            Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
            Dim user = New IdentityUser("MixEFManagerTest")
            Dim password = "password"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user, password))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            user.SecurityStamp = "bogus"
            UnitTestHelper.IsSuccess(Await manager.UpdateAsync(user))
            Assert.Equal("bogus", db.Users.Find(user.Id).SecurityStamp)
        End Function

        <Fact>
        Public Async Function CreateUserBasicStoreTest() As Task
            Dim manager = New UserManager(Of IdentityUser)(New NoopUserStore())
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(New IdentityUser("test")))
        End Function

        <Fact>
        Public Async Function GetAllUsersTest() As Task
            Dim mgr = TestUtil.CreateManager()
            Dim users = {New IdentityUser("user1"), New IdentityUser("user2"), New IdentityUser("user3")}

            For Each u As IdentityUser In users
                UnitTestHelper.IsSuccess(Await mgr.CreateAsync(u))
            Next

            Dim usersQ As IQueryable(Of IUser) = mgr.Users
            Assert.Equal(3, usersQ.Count())
            Assert.NotNull(usersQ.Where(Function(u) u.UserName = "user1").FirstOrDefault())
            Assert.NotNull(usersQ.Where(Function(u) u.UserName = "user2").FirstOrDefault())
            Assert.NotNull(usersQ.Where(Function(u) u.UserName = "user3").FirstOrDefault())
            Assert.Null(usersQ.Where(Function(u) u.UserName = "bogus").FirstOrDefault())
        End Function

        <Fact>
        Public Async Function ConfirmEmailFalseByDefaultTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("test")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Assert.[False](Await manager.IsEmailConfirmedAsync(user.Id))
        End Function

        <Fact>
        Public Async Function ConfirmEmailTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("test")
            Assert.[False](user.EmailConfirmed)
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Dim token = Await manager.GenerateEmailConfirmationTokenAsync(user.Id)
            Assert.NotNull(token)
            UnitTestHelper.IsSuccess(Await manager.ConfirmEmailAsync(user.Id, token))
            Assert.[True](Await manager.IsEmailConfirmedAsync(user.Id))
            UnitTestHelper.IsSuccess(Await manager.SetEmailAsync(user.Id, Nothing))
            Assert.[False](Await manager.IsEmailConfirmedAsync(user.Id))
        End Function

        <Fact>
        Public Sub ConfirmEmailSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("test")
            Assert.[False](user.EmailConfirmed)
            UnitTestHelper.IsSuccess(manager.Create(user))
            Dim token = manager.GenerateEmailConfirmationToken(user.Id)
            Assert.NotNull(token)
            UnitTestHelper.IsSuccess(manager.ConfirmEmail(user.Id, token))
            Assert.[True](manager.IsEmailConfirmed(user.Id))
            UnitTestHelper.IsSuccess(manager.SetEmail(user.Id, Nothing))
            Assert.[False](manager.IsEmailConfirmed(user.Id))
        End Sub

        <Fact>
        Public Async Function ConfirmTokenFailsAfterPasswordChangeTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("test")
            Assert.[False](user.EmailConfirmed)
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user, "password"))
            Dim token = Await manager.GenerateEmailConfirmationTokenAsync(user.Id)
            Assert.NotNull(token)
            UnitTestHelper.IsSuccess(Await manager.ChangePasswordAsync(user.Id, "password", "newpassword"))
            UnitTestHelper.IsFailure(Await manager.ConfirmEmailAsync(user.Id, token), "Invalid token.")
            Assert.[False](Await manager.IsEmailConfirmedAsync(user.Id))
        End Function

        <Fact>
        Public Async Function FindByEmailTest() As Task
            Dim manager = TestUtil.CreateManager()
            Const userName As String = "EmailTest"
            Const email As String = "email@test.com"
            Dim user = New IdentityUser(userName) With {
                .Email = email
            }
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Dim fetch = Await manager.FindByEmailAsync(email)
            Assert.Equal(user, fetch)
        End Function

        <Fact>
        Public Sub FindByEmailSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim userName = "EmailTest"
            Dim email = "email@test.com"
            Dim user = New IdentityUser(userName) With {
                .Email = email
            }
            UnitTestHelper.IsSuccess(manager.Create(user))
            Dim fetch = manager.FindByEmail(email)
            Assert.Equal(user, fetch)
        End Sub

        <Fact>
        Public Async Function SetEmailTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim userName = "EmailTest"
            Dim email = "email@test.com"
            Dim user = New IdentityUser(userName)
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Assert.Null(Await manager.FindByEmailAsync(email))
            Dim stamp = Await manager.GetSecurityStampAsync(user.Id)
            UnitTestHelper.IsSuccess(Await manager.SetEmailAsync(user.Id, email))
            Dim fetch = Await manager.FindByEmailAsync(email)
            Assert.Equal(user, fetch)
            Assert.Equal(email, Await manager.GetEmailAsync(user.Id))
            Assert.[False](Await manager.IsEmailConfirmedAsync(user.Id))
            Assert.NotEqual(stamp, user.SecurityStamp)
        End Function

        <Fact>
        Public Async Function CreateDupeEmailFailsTest() As Task
            Dim manager = TestUtil.CreateManager()
            manager.UserValidator = New UserValidator(Of IdentityUser)(manager) With {
                .RequireUniqueEmail = True
            }
            Dim userName = "EmailTest"
            Dim email = "email@test.com"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(New IdentityUser(userName) With {
                .Email = email
            }))
            Dim user = New IdentityUser("two") With {
                .Email = email
            }
            UnitTestHelper.IsFailure(Await manager.CreateAsync(user), "Email 'email@test.com' is already taken.")
        End Function

        <Fact>
        Public Async Function SetEmailToDupeFailsTest() As Task
            Dim manager = TestUtil.CreateManager()
            manager.UserValidator = New UserValidator(Of IdentityUser)(manager) With {
                .RequireUniqueEmail = True
            }
            Dim email = "email@test.com"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(New IdentityUser("emailtest") With {
                .Email = email
            }))
            Dim user = New IdentityUser("two") With {
                .Email = "something@else.com"
            }
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            UnitTestHelper.IsFailure(Await manager.SetEmailAsync(user.Id, email), "Email 'email@test.com' is already taken.")
        End Function

        <Fact>
        Public Async Function RequireUniqueEmailBlocksBasicCreateTest() As Task
            Dim manager = TestUtil.CreateManager()
            manager.UserValidator = New UserValidator(Of IdentityUser)(manager) With {
                .RequireUniqueEmail = True
            }
            UnitTestHelper.IsFailure(Await manager.CreateAsync(New IdentityUser("emailtest"), "Email is too short."))
        End Function

        <Fact>
        Public Async Function RequireUniqueEmailBlocksInvalidEmailTest() As Task
            Dim manager = TestUtil.CreateManager()
            manager.UserValidator = New UserValidator(Of IdentityUser)(manager) With {
                .RequireUniqueEmail = True
            }
            UnitTestHelper.IsFailure(Await manager.CreateAsync(New IdentityUser("emailtest") With {
                .Email = "hi"
            }), "Email 'hi' is invalid.")
        End Function

        <Fact>
        Public Async Function SetPhoneNumberTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim userName = "PhoneTest"
            Dim user = New IdentityUser(userName)
            user.PhoneNumber = "123-456-7890"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Dim stamp = Await manager.GetSecurityStampAsync(user.Id)
            Assert.Equal(Await manager.GetPhoneNumberAsync(user.Id), "123-456-7890")
            UnitTestHelper.IsSuccess(Await manager.SetPhoneNumberAsync(user.Id, "111-111-1111"))
            Assert.Equal(Await manager.GetPhoneNumberAsync(user.Id), "111-111-1111")
            Assert.NotEqual(stamp, user.SecurityStamp)
        End Function

        <Fact>
        Public Sub SetPhoneNumberSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim userName = "PhoneTest"
            Dim user = New IdentityUser(userName)
            user.PhoneNumber = "123-456-7890"
            UnitTestHelper.IsSuccess(manager.Create(user))
            Dim stamp = manager.GetSecurityStamp(user.Id)
            Assert.Equal(manager.GetPhoneNumber(user.Id), "123-456-7890")
            UnitTestHelper.IsSuccess(manager.SetPhoneNumber(user.Id, "111-111-1111"))
            Assert.Equal(manager.GetPhoneNumber(user.Id), "111-111-1111")
            Assert.NotEqual(stamp, user.SecurityStamp)
        End Sub

        <Fact>
        Public Async Function ChangePhoneNumberTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim userName = "PhoneTest"
            Dim user = New IdentityUser(userName)
            user.PhoneNumber = "123-456-7890"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Assert.[False](Await manager.IsPhoneNumberConfirmedAsync(user.Id))
            Dim stamp = Await manager.GetSecurityStampAsync(user.Id)
            Dim token1 = Await manager.GenerateChangePhoneNumberTokenAsync(user.Id, "111-111-1111")
            UnitTestHelper.IsSuccess(Await manager.ChangePhoneNumberAsync(user.Id, "111-111-1111", token1))
            Assert.[True](Await manager.IsPhoneNumberConfirmedAsync(user.Id))
            Assert.Equal(Await manager.GetPhoneNumberAsync(user.Id), "111-111-1111")
            Assert.NotEqual(stamp, user.SecurityStamp)
        End Function

        <Fact>
        Public Sub ChangePhoneNumberSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim userName = "PhoneTest"
            Dim user = New IdentityUser(userName)
            user.PhoneNumber = "123-456-7890"
            UnitTestHelper.IsSuccess(manager.Create(user))
            Dim stamp = manager.GetSecurityStamp(user.Id)
            Assert.[False](manager.IsPhoneNumberConfirmed(user.Id))
            Dim token1 = manager.GenerateChangePhoneNumberToken(user.Id, "111-111-1111")
            UnitTestHelper.IsSuccess(manager.ChangePhoneNumber(user.Id, "111-111-1111", token1))
            Assert.[True](manager.IsPhoneNumberConfirmed(user.Id))
            Assert.Equal(manager.GetPhoneNumber(user.Id), "111-111-1111")
            Assert.NotEqual(stamp, user.SecurityStamp)
        End Sub

        <Fact>
        Public Async Function ChangePhoneNumberFailsWithWrongTokenTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim userName = "PhoneTest"
            Dim user = New IdentityUser(userName)
            user.PhoneNumber = "123-456-7890"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Assert.[False](Await manager.IsPhoneNumberConfirmedAsync(user.Id))
            Dim stamp = Await manager.GetSecurityStampAsync(user.Id)
            UnitTestHelper.IsFailure(Await manager.ChangePhoneNumberAsync(user.Id, "111-111-1111", "bogus"), "Invalid token.")
            Assert.[False](Await manager.IsPhoneNumberConfirmedAsync(user.Id))
            Assert.Equal(Await manager.GetPhoneNumberAsync(user.Id), "123-456-7890")
            Assert.Equal(stamp, user.SecurityStamp)
        End Function

        <Fact>
        Public Async Function VerifyPhoneNumberTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim userName = "VerifyPhoneTest"
            Dim user = New IdentityUser(userName)
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Dim num1 = "111-123-4567"
            Dim num2 = "111-111-1111"
            Dim token1 = Await manager.GenerateChangePhoneNumberTokenAsync(user.Id, num1)
            Dim token2 = Await manager.GenerateChangePhoneNumberTokenAsync(user.Id, num2)
            Assert.NotEqual(token1, token2)
            Assert.[True](Await manager.VerifyChangePhoneNumberTokenAsync(user.Id, token1, num1))
            Assert.[True](Await manager.VerifyChangePhoneNumberTokenAsync(user.Id, token2, num2))
            Assert.[False](Await manager.VerifyChangePhoneNumberTokenAsync(user.Id, token2, num1))
            Assert.[False](Await manager.VerifyChangePhoneNumberTokenAsync(user.Id, token1, num2))
        End Function

        <Fact>
        Public Sub VerifyPhoneNumberSyncTest()
            Dim manager = TestUtil.CreateManager()
            Const userName As String = "VerifyPhoneTest"
            Dim user = New IdentityUser(userName)
            UnitTestHelper.IsSuccess(manager.Create(user))
            Const num1 As String = "111-123-4567"
            Const num2 As String = "111-111-1111"
            Assert.[False](manager.IsPhoneNumberConfirmed(user.Id))
            Dim token1 = manager.GenerateChangePhoneNumberToken(user.Id, num1)
            Dim token2 = manager.GenerateChangePhoneNumberToken(user.Id, num2)
            Assert.NotEqual(token1, token2)
            Assert.[True](manager.VerifyChangePhoneNumberToken(user.Id, token1, num1))
            Assert.[True](manager.VerifyChangePhoneNumberToken(user.Id, token2, num2))
            Assert.[False](manager.VerifyChangePhoneNumberToken(user.Id, token2, num1))
            Assert.[False](manager.VerifyChangePhoneNumberToken(user.Id, token1, num2))
        End Sub

        <Fact>
        Public Async Function EmailTokenFactorTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim messageService = New TestMessageService()
            manager.EmailService = messageService
            Const factorId As String = "EmailCode"
            manager.RegisterTwoFactorProvider(factorId, New EmailTokenProvider(Of IdentityUser)())
            Dim user = New IdentityUser("EmailCodeTest") With {
                .Email = "foo@foo.com"
            }
            Const password As String = "password"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user, password))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = Await manager.GenerateTwoFactorTokenAsync(user.Id, factorId)
            Assert.NotNull(token)
            Assert.Null(messageService.Message)
            UnitTestHelper.IsSuccess(Await manager.NotifyTwoFactorTokenAsync(user.Id, factorId, token))
            Assert.NotNull(messageService.Message)
            Assert.Equal(String.Empty, messageService.Message.Subject)
            Assert.Equal(token, messageService.Message.Body)
            Assert.[True](Await manager.VerifyTwoFactorTokenAsync(user.Id, factorId, token))
        End Function

        <Fact>
        Public Async Function EmailTokenFactorWithFormatTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim messageService = New TestMessageService()
            manager.EmailService = messageService
            Const factorId As String = "EmailCode"
            manager.RegisterTwoFactorProvider(factorId, New EmailTokenProvider(Of IdentityUser) With {
                .Subject = "Security Code",
                .BodyFormat = "Your code is: {0}"
            })
            Dim user = New IdentityUser("EmailCodeTest") With {
                .Email = "foo@foo.com"
            }
            Const password As String = "password"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user, password))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = Await manager.GenerateTwoFactorTokenAsync(user.Id, factorId)
            Assert.NotNull(token)
            Assert.Null(messageService.Message)
            UnitTestHelper.IsSuccess(Await manager.NotifyTwoFactorTokenAsync(user.Id, factorId, token))
            Assert.NotNull(messageService.Message)
            Assert.Equal("Security Code", messageService.Message.Subject)
            Assert.Equal("Your code is: " & token, messageService.Message.Body)
            Assert.[True](Await manager.VerifyTwoFactorTokenAsync(user.Id, factorId, token))
        End Function

        <Fact>
        Public Sub EmailTokenFactorWithFormatSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim messageService = New TestMessageService()
            manager.EmailService = messageService
            Const factorId As String = "EmailCode"
            manager.RegisterTwoFactorProvider(factorId, New EmailTokenProvider(Of IdentityUser) With {
                .Subject = "Security Code",
                .BodyFormat = "Your code is: {0}"
            })
            Dim user = New IdentityUser("EmailCodeTest") With {
                .Email = "foo@foo.com"
            }
            Const password As String = "password"
            UnitTestHelper.IsSuccess(manager.Create(user, password))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = manager.GenerateTwoFactorToken(user.Id, factorId)
            Assert.NotNull(token)
            Assert.Null(messageService.Message)
            UnitTestHelper.IsSuccess(manager.NotifyTwoFactorToken(user.Id, factorId, token))
            Assert.NotNull(messageService.Message)
            Assert.Equal("Security Code", messageService.Message.Subject)
            Assert.Equal("Your code is: " & token, messageService.Message.Body)
            Assert.[True](manager.VerifyTwoFactorToken(user.Id, factorId, token))
        End Sub

        <Fact>
        Public Async Function EmailFactorFailsAfterSecurityStampChangeTest() As Task
            Dim manager = TestUtil.CreateManager()
            Const factorId As String = "EmailCode"
            manager.RegisterTwoFactorProvider(factorId, New EmailTokenProvider(Of IdentityUser)())
            Dim user = New IdentityUser("EmailCodeTest") With {
                .Email = "foo@foo.com"
            }
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = Await manager.GenerateTwoFactorTokenAsync(user.Id, factorId)
            Assert.NotNull(token)
            UnitTestHelper.IsSuccess(Await manager.UpdateSecurityStampAsync(user.Id))
            Assert.[False](Await manager.VerifyTwoFactorTokenAsync(user.Id, factorId, token))
        End Function

        <Fact>
        Public Sub EmailTokenFactorSyncTest()
            Dim manager = TestUtil.CreateManager()
            Const factorId As String = "EmailCode"
            manager.RegisterTwoFactorProvider(factorId, New EmailTokenProvider(Of IdentityUser)())
            Dim user = New IdentityUser("EmailCodeTest") With {
                .Email = "foo@foo.com"
            }
            Const password As String = "password"
            UnitTestHelper.IsSuccess(manager.Create(user, password))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = manager.GenerateTwoFactorToken(user.Id, factorId)
            Assert.NotNull(token)
            Assert.[True](manager.VerifyTwoFactorToken(user.Id, factorId, token))
        End Sub

        <Fact>
        Public Sub EmailFactorFailsAfterSecurityStampChangeSyncTest()
            Dim manager = TestUtil.CreateManager()
            Const factorId As String = "EmailCode"
            manager.RegisterTwoFactorProvider(factorId, New EmailTokenProvider(Of IdentityUser)())
            Dim user = New IdentityUser("EmailCodeTest") With {
                .Email = "foo@foo.com"
            }
            UnitTestHelper.IsSuccess(manager.Create(user))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = manager.GenerateTwoFactorToken(user.Id, factorId)
            Assert.NotNull(token)
            UnitTestHelper.IsSuccess(manager.UpdateSecurityStamp(user.Id))
            Assert.[False](manager.VerifyTwoFactorToken(user.Id, factorId, token))
        End Sub

        <Fact>
        Public Async Function UserTwoFactorProviderTest() As Task
            Dim manager = TestUtil.CreateManager()
            Const factorId As String = "PhoneCode"
            manager.RegisterTwoFactorProvider(factorId, New PhoneNumberTokenProvider(Of IdentityUser)())
            Dim user = New IdentityUser("PhoneCodeTest")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            UnitTestHelper.IsSuccess(Await manager.SetTwoFactorEnabledAsync(user.Id, True))
            Assert.NotEqual(stamp, Await manager.GetSecurityStampAsync(user.Id))
            Assert.[True](Await manager.GetTwoFactorEnabledAsync(user.Id))
        End Function

        <Fact>
        Public Async Function SendSms() As Task
            Dim manager = TestUtil.CreateManager()
            Dim messageService = New TestMessageService()
            manager.SmsService = messageService
            Dim user = New IdentityUser("SmsTest") With {
                .PhoneNumber = "4251234567"
            }
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Await manager.SendSmsAsync(user.Id, "Hi")
            Assert.NotNull(messageService.Message)
            Assert.Equal("Hi", messageService.Message.Body)
        End Function

        <Fact>
        Public Async Function SendEmail() As Task
            Dim manager = TestUtil.CreateManager()
            Dim messageService = New TestMessageService()
            manager.EmailService = messageService
            Dim user = New IdentityUser("EmailTest") With {
                .Email = "foo@foo.com"
            }
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Await manager.SendEmailAsync(user.Id, "Hi", "Body")
            Assert.NotNull(messageService.Message)
            Assert.Equal("Hi", messageService.Message.Subject)
            Assert.Equal("Body", messageService.Message.Body)
        End Function

        <Fact>
        Public Sub SendSmsSync()
            Dim manager = TestUtil.CreateManager()
            Dim messageService = New TestMessageService()
            manager.SmsService = messageService
            Dim user = New IdentityUser("SmsTest") With {
                .PhoneNumber = "4251234567"
            }
            UnitTestHelper.IsSuccess(manager.Create(user))
            manager.SendSms(user.Id, "Hi")
            Assert.NotNull(messageService.Message)
            Assert.Equal("Hi", messageService.Message.Body)
        End Sub

        <Fact>
        Public Sub SendEmailSync()
            Dim manager = TestUtil.CreateManager()
            Dim messageService = New TestMessageService()
            manager.EmailService = messageService
            Dim user = New IdentityUser("EmailTest") With {
                .Email = "foo@foo.com"
            }
            UnitTestHelper.IsSuccess(manager.Create(user))
            manager.SendEmail(user.Id, "Hi", "Body")
            Assert.NotNull(messageService.Message)
            Assert.Equal("Hi", messageService.Message.Subject)
            Assert.Equal("Body", messageService.Message.Body)
        End Sub

        <Fact>
        Public Async Function PhoneTokenFactorTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim messageService = New TestMessageService()
            manager.SmsService = messageService
            Const factorId As String = "PhoneCode"
            manager.RegisterTwoFactorProvider(factorId, New PhoneNumberTokenProvider(Of IdentityUser)())
            Dim user = New IdentityUser("PhoneCodeTest") With {
                .PhoneNumber = "4251234567"
            }
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = Await manager.GenerateTwoFactorTokenAsync(user.Id, factorId)
            Assert.NotNull(token)
            Assert.Null(messageService.Message)
            UnitTestHelper.IsSuccess(Await manager.NotifyTwoFactorTokenAsync(user.Id, factorId, token))
            Assert.NotNull(messageService.Message)
            Assert.Equal(token, messageService.Message.Body)
            Assert.[True](Await manager.VerifyTwoFactorTokenAsync(user.Id, factorId, token))
        End Function

        <Fact>
        Public Sub PhoneTokenFactorSyncTest()
            Dim manager = TestUtil.CreateManager()
            Dim messageService = New TestMessageService()
            manager.SmsService = messageService
            Const factorId As String = "PhoneCode"
            manager.RegisterTwoFactorProvider(factorId, New PhoneNumberTokenProvider(Of IdentityUser)())
            Dim user = New IdentityUser("PhoneCodeTest") With {
                .PhoneNumber = "4251234567"
            }
            UnitTestHelper.IsSuccess(manager.Create(user))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = manager.GenerateTwoFactorToken(user.Id, factorId)
            Assert.NotNull(token)
            Assert.Null(messageService.Message)
            UnitTestHelper.IsSuccess(manager.NotifyTwoFactorToken(user.Id, factorId, token))
            Assert.NotNull(messageService.Message)
            Assert.Equal(token, messageService.Message.Body)
            Assert.[True](manager.VerifyTwoFactorToken(user.Id, factorId, token))
        End Sub

        <Fact>
        Public Async Function PhoneTokenFactorFormatTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim messageService = New TestMessageService()
            manager.SmsService = messageService
            Const factorId As String = "PhoneCode"
            manager.RegisterTwoFactorProvider(factorId, New PhoneNumberTokenProvider(Of IdentityUser) With {
                .MessageFormat = "Your code is: {0}"
            })
            Dim user = New IdentityUser("PhoneCodeTest") With {
                .PhoneNumber = "4251234567"
            }
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = Await manager.GenerateTwoFactorTokenAsync(user.Id, factorId)
            Assert.NotNull(token)
            Assert.Null(messageService.Message)
            UnitTestHelper.IsSuccess(Await manager.NotifyTwoFactorTokenAsync(user.Id, factorId, token))
            Assert.NotNull(messageService.Message)
            Assert.Equal("Your code is: " & token, messageService.Message.Body)
            Assert.[True](Await manager.VerifyTwoFactorTokenAsync(user.Id, factorId, token))
        End Function

        <Fact>
        Public Async Function NoFactorProviderTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("PhoneCodeTest")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Const [error] As String = "No IUserTwoFactorProvider for 'bogus' is registered."
            ExceptionHelper.ThrowsWithError(Of NotSupportedException)(Function() manager.GenerateTwoFactorToken(user.Id, "bogus"), [error])
            ExceptionHelper.ThrowsWithError(Of NotSupportedException)(Function() manager.VerifyTwoFactorToken(user.Id, "bogus", "bogus"), [error])
        End Function

        <Fact>
        Public Async Function GetValidTwoFactorTestEmptyWithNoProviders() As Task
            Dim manager = TestUtil.CreateManager()
            Dim user = New IdentityUser("test")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Dim factors = Await manager.GetValidTwoFactorProvidersAsync(user.Id)
            Assert.NotNull(factors)
            Assert.[True](Not factors.Any())
        End Function

        <Fact>
        Public Async Function GetValidTwoFactorTest() As Task
            Dim manager = TestUtil.CreateManager()
            manager.RegisterTwoFactorProvider("phone", New PhoneNumberTokenProvider(Of IdentityUser)())
            manager.RegisterTwoFactorProvider("email", New EmailTokenProvider(Of IdentityUser)())
            Dim user = New IdentityUser("test")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Dim factors = Await manager.GetValidTwoFactorProvidersAsync(user.Id)
            Assert.NotNull(factors)
            UnitTestHelper.IsSuccess(Await manager.SetPhoneNumberAsync(user.Id, "111-111-1111"))
            factors = Await manager.GetValidTwoFactorProvidersAsync(user.Id)
            Assert.[False](factors.Any())
            user.PhoneNumberConfirmed = True
            UnitTestHelper.IsSuccess(manager.Update(user))
            factors = Await manager.GetValidTwoFactorProvidersAsync(user.Id)
            Assert.[True](factors.Count() = 1)
            Assert.Equal("phone", factors(0))
            Assert.NotNull(factors)
            Assert.[True](factors.Count() = 1)
            Assert.Equal("phone", factors(0))
            UnitTestHelper.IsSuccess(Await manager.SetEmailAsync(user.Id, "test@test.com"))
            factors = Await manager.GetValidTwoFactorProvidersAsync(user.Id)
            Assert.NotNull(factors)
            Assert.[True](factors.Count() = 1)
            user.EmailConfirmed = True
            UnitTestHelper.IsSuccess(Await manager.UpdateAsync(user))
            factors = Await manager.GetValidTwoFactorProvidersAsync(user.Id)
            Assert.NotNull(factors)
            Assert.[True](factors.Count() = 2)
            UnitTestHelper.IsSuccess(Await manager.SetEmailAsync(user.Id, "somethingelse"))
            factors = Await manager.GetValidTwoFactorProvidersAsync(user.Id)
            Assert.NotNull(factors)
            Assert.[True](factors.Count() = 1)
            Assert.Equal("phone", factors(0))
        End Function

        <Fact>
        Public Sub GetValidTwoFactorSyncTest()
            Dim manager = TestUtil.CreateManager()
            manager.RegisterTwoFactorProvider("phone", New PhoneNumberTokenProvider(Of IdentityUser)())
            manager.RegisterTwoFactorProvider("email", New EmailTokenProvider(Of IdentityUser)())
            Dim user = New IdentityUser("test")
            UnitTestHelper.IsSuccess(manager.Create(user))
            Dim factors = manager.GetValidTwoFactorProviders(user.Id)
            Assert.NotNull(factors)
            Assert.[False](factors.Any())
            UnitTestHelper.IsSuccess(manager.SetPhoneNumber(user.Id, "111-111-1111"))
            factors = manager.GetValidTwoFactorProviders(user.Id)
            Assert.NotNull(factors)
            Assert.[False](factors.Any())
            user.PhoneNumberConfirmed = True
            UnitTestHelper.IsSuccess(manager.Update(user))
            factors = manager.GetValidTwoFactorProviders(user.Id)
            Assert.[True](factors.Count() = 1)
            Assert.Equal("phone", factors(0))
            UnitTestHelper.IsSuccess(manager.SetEmail(user.Id, "test@test.com"))
            factors = manager.GetValidTwoFactorProviders(user.Id)
            Assert.NotNull(factors)
            Assert.[True](factors.Count() = 1)
            user.EmailConfirmed = True
            UnitTestHelper.IsSuccess(manager.Update(user))
            factors = manager.GetValidTwoFactorProviders(user.Id)
            Assert.NotNull(factors)
            Assert.[True](factors.Count() = 2)
            UnitTestHelper.IsSuccess(manager.SetEmail(user.Id, Nothing))
            factors = manager.GetValidTwoFactorProviders(user.Id)
            Assert.NotNull(factors)
            Assert.[True](factors.Count() = 1)
            Assert.Equal("phone", factors(0))
        End Sub

        <Fact>
        Public Async Function PhoneFactorFailsAfterSecurityStampChangeTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim factorId = "PhoneCode"
            manager.RegisterTwoFactorProvider(factorId, New PhoneNumberTokenProvider(Of IdentityUser)())
            Dim user = New IdentityUser("PhoneCodeTest")
            user.PhoneNumber = "4251234567"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = Await manager.GenerateTwoFactorTokenAsync(user.Id, factorId)
            Assert.NotNull(token)
            UnitTestHelper.IsSuccess(Await manager.UpdateSecurityStampAsync(user.Id))
            Assert.[False](Await manager.VerifyTwoFactorTokenAsync(user.Id, factorId, token))
        End Function

        <Fact>
        Public Async Function WrongTokenProviderFailsTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim factorId = "PhoneCode"
            manager.RegisterTwoFactorProvider(factorId, New PhoneNumberTokenProvider(Of IdentityUser)())
            manager.RegisterTwoFactorProvider("EmailCode", New EmailTokenProvider(Of IdentityUser)())
            Dim user = New IdentityUser("PhoneCodeTest")
            user.PhoneNumber = "4251234567"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = Await manager.GenerateTwoFactorTokenAsync(user.Id, factorId)
            Assert.NotNull(token)
            Assert.[False](Await manager.VerifyTwoFactorTokenAsync(user.Id, "EmailCode", token))
        End Function

        <Fact>
        Public Async Function WrongTokenFailsTest() As Task
            Dim manager = TestUtil.CreateManager()
            Dim factorId = "PhoneCode"
            manager.RegisterTwoFactorProvider(factorId, New PhoneNumberTokenProvider(Of IdentityUser)())
            Dim user = New IdentityUser("PhoneCodeTest")
            user.PhoneNumber = "4251234567"
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            Dim stamp = user.SecurityStamp
            Assert.NotNull(stamp)
            Dim token = Await manager.GenerateTwoFactorTokenAsync(user.Id, factorId)
            Assert.NotNull(token)
            Assert.[False](Await manager.VerifyTwoFactorTokenAsync(user.Id, factorId, "abc"))
        End Function

        <Fact>
        Public Async Function ResetTokenCallNoopForTokenValueZero() As Task
            Dim user = New IdentityUser() With {
                .UserName = "foo"
            }
            Dim store = New Mock(Of UserStore(Of IdentityUser))()
            store.Setup(Function(x) x.ResetAccessFailedCountAsync(user)).Returns(Function()
                                                                                     Throw New Exception()
                                                                                 End Function)
            store.Setup(Function(x) x.FindByIdAsync(It.IsAny(Of String)())).Returns(Function() Task.FromResult(user))
            store.Setup(Function(x) x.GetAccessFailedCountAsync(It.IsAny(Of IdentityUser)())).Returns(Function() Task.FromResult(0))
            Dim manager = New UserManager(Of IdentityUser)(store.Object)
            UnitTestHelper.IsSuccess(Await manager.ResetAccessFailedCountAsync(user.Id))
        End Function

        <Fact>
        Public Sub Create_preserves_culture()
            Dim originalCulture = Thread.CurrentThread.CurrentCulture
            Dim originalUICulture = Thread.CurrentThread.CurrentUICulture
            Dim expectedCulture = New CultureInfo("de-DE")
            Thread.CurrentThread.CurrentCulture = expectedCulture
            Thread.CurrentThread.CurrentUICulture = expectedCulture
            Dim manager = TestUtil.CreateManager()

            Try
                Dim cultures = GetCurrentCultureAfter(Function() manager.CreateAsync(New IdentityUser("whatever"))).Result
                Assert.Equal(expectedCulture, cultures.Item1)
                Assert.Equal(expectedCulture, cultures.Item2)
            Finally
                Thread.CurrentThread.CurrentCulture = originalCulture
                Thread.CurrentThread.CurrentUICulture = originalUICulture
            End Try
        End Sub

        <Fact>
        Public Sub CreateSync_preserves_culture()
            Dim originalCulture = Thread.CurrentThread.CurrentCulture
            Dim originalUICulture = Thread.CurrentThread.CurrentUICulture
            Dim expectedCulture = New CultureInfo("de-DE")
            Thread.CurrentThread.CurrentCulture = expectedCulture
            Thread.CurrentThread.CurrentUICulture = expectedCulture
            Dim manager = TestUtil.CreateManager()

            Try
                Dim cultures = GetCurrentCultureAfter(Function() manager.Create(New IdentityUser("whatever")))
                Assert.Equal(expectedCulture, cultures.Item1)
                Assert.Equal(expectedCulture, cultures.Item2)
            Finally
                Thread.CurrentThread.CurrentCulture = originalCulture
                Thread.CurrentThread.CurrentUICulture = originalUICulture
            End Try
        End Sub

        Private Shared Async Function GetCurrentCultureAfter(ByVal action As Func(Of Task)) As Task(Of Tuple(Of CultureInfo, CultureInfo))
            Await action()
            Return New Tuple(Of CultureInfo, CultureInfo)(Thread.CurrentThread.CurrentCulture, Thread.CurrentThread.CurrentUICulture)
        End Function

        Private Shared Function GetCurrentCultureAfter(ByVal action As Action) As Tuple(Of CultureInfo, CultureInfo)
            action()
            Return New Tuple(Of CultureInfo, CultureInfo)(Thread.CurrentThread.CurrentCulture, Thread.CurrentThread.CurrentUICulture)
        End Function

        Private Class NoOpTokenProvider
            Inherits IUserTokenProvider(Of IdentityUser, String)

            Public Function GenerateAsync(ByVal purpose As String, ByVal manager As UserManager(Of IdentityUser, String), ByVal user As IdentityUser) As Task(Of String)
                Throw New NotImplementedException()
            End Function

            Public Function ValidateAsync(ByVal purpose As String, ByVal token As String, ByVal manager As UserManager(Of IdentityUser, String), ByVal user As IdentityUser) As Task(Of Boolean)
                Throw New NotImplementedException()
            End Function

            Public Function NotifyAsync(ByVal token As String, ByVal manager As UserManager(Of IdentityUser, String), ByVal user As IdentityUser) As Task
                Throw New NotImplementedException()
            End Function

            Public Function IsValidProviderForUserAsync(ByVal manager As UserManager(Of IdentityUser, String), ByVal user As IdentityUser) As Task(Of Boolean)
                Throw New NotImplementedException()
            End Function

            Private Class CSharpImpl
                <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
                Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                    target = value
                    Return value
                End Function
            End Class
        End Class

        Private Class NoStampUserManager
            Inherits UserManager(Of IdentityUser)

            Public Sub New()
                MyBase.New(New UserStore(Of IdentityUser)(UnitTestHelper.CreateDefaultDb()))
                UserValidator = New UserValidator(Of IdentityUser)(Me) With {
                    .AllowOnlyAlphanumericUserNames = True,
                    .RequireUniqueEmail = False
                }
                Dim dpp = New DpapiDataProtectionProvider()
                UserTokenProvider = New DataProtectorTokenProvider(Of IdentityUser)(dpp.Create("ASP.NET Identity"))
            End Sub

            Public Overrides ReadOnly Property SupportsUserSecurityStamp As Boolean
                Get
                    Return False
                End Get
            End Property

            Private Class CSharpImpl
                <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
                Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                    target = value
                    Return value
                End Function
            End Class
        End Class

        Private Class NoopUserStore
            Inherits IUserStore(Of IdentityUser)

            Public Function CreateAsync(ByVal user As IdentityUser) As Task
                Return Task.FromResult(0)
            End Function

            Public Function UpdateAsync(ByVal user As IdentityUser) As Task
                Return Task.FromResult(0)
            End Function

            Public Function FindByIdAsync(ByVal userId As String) As Task(Of IdentityUser)
                Return Task.FromResult(Of IdentityUser)(Nothing)
            End Function

            Public Function FindByNameAsync(ByVal userName As String) As Task(Of IdentityUser)
                Return Task.FromResult(Of IdentityUser)(Nothing)
            End Function

            Public Sub Dispose()
            End Sub

            Public Function DeleteAsync(ByVal user As IdentityUser) As Task
                Return Task.FromResult(0)
            End Function

            Private Class CSharpImpl
                <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
                Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                    target = value
                    Return value
                End Function
            End Class
        End Class

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class
End Namespace
