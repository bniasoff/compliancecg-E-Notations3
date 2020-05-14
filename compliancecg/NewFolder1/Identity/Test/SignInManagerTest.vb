Imports System
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security.DataProtection
Imports compliancecg.Microsoft.AspNet.Identity
Imports compliancecg.Microsoft.AspNet.Identity.EntityFramework
Imports IdentityUser = compliancecg.Microsoft.AspNet.Identity.EntityFramework.IdentityUser



Namespace Identity.Test
    Public Class SignInManagerTest
        '<Theory>
        '<InlineData(True, True)>
        '<InlineData(True, False)>
        '<InlineData(False, True)>
        '<InlineData(False, False)>
        'Public Async Function SignInAsyncCookiePersistenceTest(ByVal isPersistent As Boolean, ByVal rememberBrowser As Boolean) As Task
        '    Try
        '        Dim db = UnitTestHelper.CreateDefaultDb()
        '        Dim manager = New UserManager(Of IdentityUser)(New Microsoft.AspNet.Identity.EntityFramework.UserStore(Of IdentityUser)(db))
        '        ' Dim user = New IdentityUser("linkunlinktest")
        '        '  Dim Users = db.Users.ToList
        '        Dim User = manager.FindByName("SignInTest3")
        '        Dim Users = manager.Users
        '        'Dim manager = New UserManager(Of IdentityUser)(New XmlUserStore(Of UserAccount))

        '        'Dim manager = New UserManager(Of IdentityUser)(New Microsoft.AspNet.Identity.EntityFramework.UserStore(Of IdentityUser)(db))

        '        'Dim manager = New UserManager(Of IdentityUser)(New XmlUserStore)

        '        'UserAccountDbContext
        '        Dim owinContext = New OwinContext()
        '        'Await TestUtil.CreateManager(owinContext)
        '        ' Dim manager = owinContext.GetUserManager(Of UserManager(Of UserAccount))()



        '        User.Id = "1"
        '        'User.UserName = "bniasoff@hotmail.com"
        '        'User.PasswordHash = "Lakewood13!"


        '        'Await manager.CreateAsync(user)
        '        '    Dim signInManager = New SignInManager(Of UserAccount, String)(manager, owinContext.Authentication)
        '        '    Await signInManager.SignInAsync(User, isPersistent, rememberBrowser)
        '        '    Dim IsPersistent2 = owinContext.Authentication.AuthenticationResponseGrant.Properties.IsPersistent
        '    Catch ex As Exception

        '    End Try
        'End Function
        Public Async Function SignInAsyncCookiePersistenceTest(ByVal isPersistent As Boolean, ByVal rememberBrowser As Boolean) As Task
            Try
                Dim owinContext = New OwinContext()
                Await TestUtil.CreateManager(owinContext)
                'Dim manager = owinContext.GetUserManager(Of UserManager(Of IdentityUser))()
                'Dim manager2 = owinContext.GetUserManager(Of UserManager(Of IdentityUser))()

                Dim manager = New ApplicationUserManager(New Microsoft.AspNet.Identity.EntityFramework.UserStore(Of IdentityUser))

                Dim UserManager As ApplicationUserManager
                UserManager = New ApplicationUserManager(manager)

                ' If (UserManager, HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)()) Then


                '(Microsoft.AspNet.Identity.EntityFramework.UserStore(Of IdentityUser))

                Dim SignInManager As ApplicationSignInManager
                SignInManager = New ApplicationSignInManager(manager, owinContext.Authentication)

                'Return If(_signInManager, HttpContext.GetOwinContext().[Get](Of ApplicationSignInManager)())


                ''Dim user = New IdentityUser("SignInTest3")
                'Dim user = manager.FindByName("SignInTest3")

                ' Await manager.CreateAsync(user)

                ' Dim ApplicationSignInManager As New ApplicationSignInManager(manager, owinContext.Authentication)


                '  Dim signInManager = New SignInManager(Of IdentityUser, String)(manager, owinContext.Authentication)
                '  Await signInManager.SignInAsync(user, isPersistent, rememberBrowser)
                Dim IsPersistent2 = owinContext.Authentication.AuthenticationResponseGrant.Properties.IsPersistent
            Catch ex As Exception

            End Try
        End Function
        'Public Async Function SignInAsyncCookiePersistenceTest2(ByVal isPersistent As Boolean, ByVal rememberBrowser As Boolean) As Task
        '    Try
        '        Dim owinContext = New OwinContext()
        '        Await TestUtil.CreateManager(owinContext)
        '        Dim manager = owinContext.GetUserManager(Of UserManager(Of IdentityUser))()

        '        'Dim user = New IdentityUser("SignInTest3")
        '        Dim user = manager.FindByName("SignInTest3")

        '        'Await manager.CreateAsync(user)
        '        Dim signInManager = New SignInManager(Of IdentityUser, String)(manager, owinContext.Authentication)
        '        Await signInManager.SignInAsync(user, isPersistent, rememberBrowser)
        '        Dim IsPersistent2 = owinContext.Authentication.AuthenticationResponseGrant.Properties.IsPersistent
        '    Catch ex As Exception

        '    End Try
        'End Function
    End Class
End Namespace
