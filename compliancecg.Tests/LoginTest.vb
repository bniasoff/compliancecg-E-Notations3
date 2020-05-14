Imports System.Linq
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Xunit

Namespace Identity.Test
    Public Class LoginsTest
        <Fact>
        Public Async Function LinkUnlinkDeletesTest() As Task
            Dim db = UnitTestHelper.CreateDefaultDb()
            Dim mgr = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
            Dim user = New IdentityUser("linkunlinktest")
            UnitTestHelper.IsSuccess(Await mgr.CreateAsync(user))
            Dim userLogin1 = New UserLoginInfo("provider1", "p1-1")
            Dim userLogin2 = New UserLoginInfo("provider2", "p2-1")
            Assert.Equal(0, (Await mgr.GetLoginsAsync(user.Id)).Count)
            UnitTestHelper.IsSuccess(Await mgr.AddLoginAsync(user.Id, userLogin1))
            Assert.Equal(1, user.Logins.Count(Function(l) l.ProviderKey = "p1-1"))
            Assert.Equal(1, (Await mgr.GetLoginsAsync(user.Id)).Count)
            UnitTestHelper.IsSuccess(Await mgr.AddLoginAsync(user.Id, userLogin2))
            Assert.Equal(1, user.Logins.Count(Function(l) l.ProviderKey = "p2-1"))
            Assert.Equal(2, (Await mgr.GetLoginsAsync(user.Id)).Count)
            UnitTestHelper.IsSuccess(Await mgr.RemoveLoginAsync(user.Id, userLogin1))
            Assert.Equal(0, user.Logins.Count(Function(l) l.ProviderKey = "p1-1"))
            Assert.Equal(1, user.Logins.Count(Function(l) l.ProviderKey = "p2-1"))
            Assert.Equal(1, (Await mgr.GetLoginsAsync(user.Id)).Count())
            UnitTestHelper.IsSuccess(Await mgr.RemoveLoginAsync(user.Id, userLogin2))
            Assert.Equal(0, (Await mgr.GetLoginsAsync(user.Id)).Count)
            Assert.Equal(0, db.[Set](Of IdentityUserLogin)().Count())
        End Function

        <Fact>
        Public Async Function AddDuplicateLoginFailsTest() As Task
            Dim db = UnitTestHelper.CreateDefaultDb()
            Dim mgr = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
            Dim user = New IdentityUser("dupeLogintest")
            UnitTestHelper.IsSuccess(Await mgr.CreateAsync(user))
            Dim userLogin1 = New UserLoginInfo("provider1", "p1-1")
            UnitTestHelper.IsSuccess(Await mgr.AddLoginAsync(user.Id, userLogin1))
            UnitTestHelper.IsFailure(Await mgr.AddLoginAsync(user.Id, userLogin1))
        End Function

        <Fact>
        Public Async Function AddLoginNullLoginFailsTest() As Task
            Dim db = UnitTestHelper.CreateDefaultDb()
            Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
            Dim user = New IdentityUser("Hao")
            UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
            ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.AddLoginAsync(user.Id, Nothing)), "login")
        End Function
    End Class
End Namespace
