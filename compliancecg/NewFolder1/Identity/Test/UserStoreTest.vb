Imports System
Imports System.Data.Entity.Validation
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Xunit
Imports IdentityUser = compliancecg.Microsoft.AspNet.Identity.EntityFramework.IdentityUser


Namespace Identity.Test
    Public Class UserStoreTest
        '<Fact>
        'Public Sub AddUserWithNoUserNameFailsTest()
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim store = New UserStore(Of IdentityUser)(db)
        '    Assert.Throws(Of DbEntityValidationException)(Function() AsyncHelper.RunSync(Function() store.CreateAsync(New IdentityUser())))
        'End Sub

        '<Fact>
        'Public Async Function CanDisableAutoSaveChangesTest() As Task
        '    Dim db = New NoopIdentityDbContext()
        '    Dim store = New UserStore(Of IdentityUser)(db)
        '    store.AutoSaveChanges = False
        '    Dim user = New IdentityUser("test")
        '    Await store.CreateAsync(user)
        '    Assert.[False](db.SaveChangesCalled)
        'End Function

        '<Fact>
        'Public Async Function CreateAutoSavesTest() As Task
        '    Dim db = New NoopIdentityDbContext()
        '    db.Configuration.ValidateOnSaveEnabled = False
        '    Dim store = New UserStore(Of IdentityUser)(db)
        '    Dim user = New IdentityUser("test")
        '    Await store.CreateAsync(user)
        '    Assert.[True](db.SaveChangesCalled)
        'End Function

        '<Fact>
        'Public Async Function UpdateAutoSavesTest() As Task
        '    Dim db = New NoopIdentityDbContext()
        '    Dim store = New UserStore(Of IdentityUser)(db)
        '    Dim user = New IdentityUser("test")
        '    Await store.UpdateAsync(user)
        '    Assert.[True](db.SaveChangesCalled)
        'End Function

        '<Fact>
        'Public Async Function AddDupeUserIdWithStoreFailsTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim store = New UserStore(Of IdentityUser)(db)
        '    Dim user = New IdentityUser("dupemgmt")
        '    Await store.CreateAsync(user)
        '    Dim u2 = New IdentityUser With {
        '        .Id = user.Id,
        '        .UserName = "User"
        '    }

        '    Try
        '        Await store.CreateAsync(u2)
        '        Assert.[False](True)
        '    Catch e As Exception
        '        Assert.[True](e.InnerException.InnerException.Message.Contains("duplicate key"))
        '    End Try
        'End Function

        '<Fact>
        'Public Sub UserStoreMethodsThrowWhenDisposedTest()
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim store = New UserStore(Of IdentityUser)(db)
        '    store.Dispose()
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.AddClaimAsync(Nothing, Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.AddLoginAsync(Nothing, Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.AddToRoleAsync(Nothing, Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.GetClaimsAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.GetLoginsAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.GetRolesAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.IsInRoleAsync(Nothing, Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.RemoveClaimAsync(Nothing, Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.RemoveLoginAsync(Nothing, Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.RemoveFromRoleAsync(Nothing, Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.RemoveClaimAsync(Nothing, Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.FindAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.FindByIdAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.FindByNameAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.UpdateAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.DeleteAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.SetEmailConfirmedAsync(Nothing, True)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.GetEmailConfirmedAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.SetPhoneNumberConfirmedAsync(Nothing, True)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.GetPhoneNumberConfirmedAsync(Nothing)))
        'End Sub

        '<Fact>
        'Public Sub UserStorePublicNullCheckTest()
        '    Dim store = New UserStore(Of IdentityUser)()
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.CreateAsync(Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.UpdateAsync(Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.DeleteAsync(Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.AddClaimAsync(Nothing, Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.RemoveClaimAsync(Nothing, Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.GetClaimsAsync(Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.GetLoginsAsync(Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.GetRolesAsync(Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.AddLoginAsync(Nothing, Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.RemoveLoginAsync(Nothing, Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.AddToRoleAsync(Nothing, Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.RemoveFromRoleAsync(Nothing, Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.IsInRoleAsync(Nothing, Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.GetPasswordHashAsync(Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.SetPasswordHashAsync(Nothing, Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.GetSecurityStampAsync(Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.SetSecurityStampAsync(Nothing, Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.AddClaimAsync(New IdentityUser("fake"), Nothing)), "claim")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.RemoveClaimAsync(New IdentityUser("fake"), Nothing)), "claim")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.AddLoginAsync(New IdentityUser("fake"), Nothing)), "login")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.RemoveLoginAsync(New IdentityUser("fake"), Nothing)), "login")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.FindAsync(Nothing)), "login")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.GetEmailConfirmedAsync(Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.SetEmailConfirmedAsync(Nothing, True)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.GetEmailAsync(Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.SetEmailAsync(Nothing, Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.GetPhoneNumberAsync(Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.SetPhoneNumberAsync(Nothing, Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.GetPhoneNumberConfirmedAsync(Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.SetPhoneNumberConfirmedAsync(Nothing, True)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.GetTwoFactorEnabledAsync(Nothing)), "user")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.SetTwoFactorEnabledAsync(Nothing, True)), "user")
        '    ExceptionHelper.ThrowsArgumentNullOrEmpty(Function() AsyncHelper.RunSync(Function() store.AddToRoleAsync(New IdentityUser("fake"), Nothing)), "roleName")
        '    ExceptionHelper.ThrowsArgumentNullOrEmpty(Function() AsyncHelper.RunSync(Function() store.RemoveFromRoleAsync(New IdentityUser("fake"), Nothing)), "roleName")
        '    ExceptionHelper.ThrowsArgumentNullOrEmpty(Function() AsyncHelper.RunSync(Function() store.IsInRoleAsync(New IdentityUser("fake"), Nothing)), "roleName")
        'End Sub

        '<Fact>
        'Public Async Function AddDupeUserNameWithStoreFailsTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim mgr = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '    Dim store = New UserStore(Of IdentityUser)(db)
        '    Dim user = New IdentityUser("dupe")
        '    UnitTestHelper.IsSuccess(Await mgr.CreateAsync(user))
        '    Dim u2 = New IdentityUser("DUPe")
        '    Assert.Throws(Of DbEntityValidationException)(Function() AsyncHelper.RunSync(Function() store.CreateAsync(u2)))
        'End Function

        '<Fact>
        'Public Async Function AddDupeEmailWithStoreFailsTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    db.RequireUniqueEmail = True
        '    Dim mgr = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '    Dim store = New UserStore(Of IdentityUser)(db)
        '    Dim user = New IdentityUser("u1") With {
        '        .Email = "email"
        '    }
        '    UnitTestHelper.IsSuccess(Await mgr.CreateAsync(user))
        '    Dim u2 = New IdentityUser("u2") With {
        '        .Email = "email"
        '    }
        '    Assert.Throws(Of DbEntityValidationException)(Function() AsyncHelper.RunSync(Function() store.CreateAsync(u2)))
        'End Function

        '<Fact>
        'Public Async Function DeleteUserTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim store = New UserStore(Of IdentityUser)(db)
        '    Dim mgmt = New IdentityUser("deletemgmttest")
        '    Await store.CreateAsync(mgmt)
        '    Assert.NotNull(Await store.FindByIdAsync(mgmt.Id))
        '    Await store.DeleteAsync(mgmt)
        '    Assert.Null(Await store.FindByIdAsync(mgmt.Id))
        'End Function

        '<Fact>
        'Public Async Function CreateLoadDeleteUserTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim store = New UserStore(Of IdentityUser)(db)
        '    Dim user = New IdentityUser("Test")
        '    Assert.Null(Await store.FindByIdAsync(user.Id))
        '    Await store.CreateAsync(user)
        '    Dim loadUser = Await store.FindByIdAsync(user.Id)
        '    Assert.NotNull(loadUser)
        '    Assert.Equal(user.Id, loadUser.Id)
        '    Await store.DeleteAsync(loadUser)
        '    loadUser = Await store.FindByIdAsync(user.Id)
        '    Assert.Null(loadUser)
        'End Function

        '<Fact>
        Public Async Function FindByUserName() As Task
            Try


                Dim db = UnitTestHelper.CreateDefaultDb()
                Dim store = New Microsoft.AspNet.Identity.EntityFramework.UserStore(Of IdentityUser)(db)
                'Dim user = New IdentityUser("bniasoff@hotmail.com")
                'Await store.CreateAsync(user)
                Dim found = Await store.FindByNameAsync("bniasoff@hotmail.com")
                '    Assert.NotNull(found)
                '    Assert.Equal(user.Id, found.Id)
                '    found = Await store.FindByNameAsync("HAO")
                '    Assert.NotNull(found)
                '    Assert.Equal(user.Id, found.Id)
                'found = Await store.FindByNameAsync("bniasoff@hotmail.com")
                '    Assert.NotNull(found)
                '    Assert.Equal(user.Id, found.Id)
            Catch ex As Exception

            End Try
        End Function

        ' <Fact>
        Public Async Function GetAllUsersTest() As Task
            'Dim db = UnitTestHelper.CreateDefaultDb()
            'Dim store = New UserStore(Of Microsoft.AspNet.Identity.EntityFramework.UserStore.IdentityUser)(db)
            'Dim users = {New IdentityUser("user1"), New IdentityUser("user2"), New IdentityUser("user3")}

            'For Each u As IdentityUser In users
            '    Await store.CreateAsync(u)
            'Next

            'Dim usersQ As IQueryable(Of IUser) = store.Users
            'Assert.Equal(3, usersQ.Count())
            '    Assert.NotNull(usersQ.Where(Function(u) u.UserName = "user1").FirstOrDefault())
            '    Assert.NotNull(usersQ.Where(Function(u) u.UserName = "user2").FirstOrDefault())
            '    Assert.NotNull(usersQ.Where(Function(u) u.UserName = "user3").FirstOrDefault())
            '    Assert.Null(usersQ.Where(Function(u) u.UserName = "bogus").FirstOrDefault())
        End Function

        Private Class NoopIdentityDbContext
            Inherits IdentityDbContext

            Public Property SaveChangesCalled As Boolean

            Public Overrides Function SaveChangesAsync(ByVal token As CancellationToken) As Task(Of Integer)
                SaveChangesCalled = True
                Return Task.FromResult(0)
            End Function
        End Class
    End Class
End Namespace
