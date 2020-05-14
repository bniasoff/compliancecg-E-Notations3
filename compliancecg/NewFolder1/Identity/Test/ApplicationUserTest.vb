Imports System.Data.Entity
Imports System.Linq
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security.DataProtection
Imports Xunit

Namespace Identity.Test
    Public Class ApplicationUserTest
        Private Async Function CreateManager(ByVal context As OwinContext) As Task
            Dim options = New IdentityFactoryOptions(Of ApplicationUserManager) With {
                .DataProtectionProvider = New DpapiDataProtectionProvider(),
                .Provider = New IdentityFactoryProvider(Of ApplicationUserManager) With {
                    .OnCreate = Function(o, c) ApplicationUserManager.Create(o, c)
                }
            }
            Dim middleware = New IdentityFactoryMiddleware(Of ApplicationUserManager, IdentityFactoryOptions(Of ApplicationUserManager))(Nothing, options)
            Dim dbMiddle = New IdentityFactoryMiddleware(Of ApplicationDbContext, IdentityFactoryOptions(Of ApplicationDbContext))(middleware, New IdentityFactoryOptions(Of ApplicationDbContext) With {
                .Provider = New IdentityFactoryProvider(Of ApplicationDbContext) With {
                    .OnCreate = Function(o, c) CreateDb()
                }
            })
            Await dbMiddle.Invoke(context)
        End Function

        '<Fact>
        'Public Sub EnsureDefaultSchemaWithApplicationUser()
        '    IdentityDbContextTest.VerifyDefaultSchema(CreateDb())
        'End Sub

        '<Fact>
        Public Async Function ApplicationUserCreateTest() As Task
            Try
                Dim owinContext = New OwinContext()
                Await CreateManager(owinContext)
                Dim manager = owinContext.GetUserManager(Of ApplicationUserManager)()


                Dim users As ApplicationUser() = {New ApplicationUser With {
                    .UserName = "test",
                    .Email = "test@test.com"
                }, New ApplicationUser With {
                    .UserName = "test1",
                    .Email = "test1@test.com"
                }, New ApplicationUser With {
                    .UserName = "test2",
                    .Email = "test2@test.com"
                }, New ApplicationUser With {
                    .UserName = "test3",
                    .Email = "test3@test.com"
                }}

                For Each user As ApplicationUser In users
                    ' UnitTestHelper.IsSuccess(Await manager.CreateAsync(user))
                    Await manager.CreateAsync(user)
                Next

                For Each user As ApplicationUser In users
                    Dim u = Await manager.FindByIdAsync(user.Id)
                    'Assert.NotNull(u)
                    'Assert.Equal(u.UserName, user.UserName)
                Next
            Catch ex As Exception

            End Try
        End Function

        Private Shared Function CreateDb() As ApplicationDbContext
            'Database.SetInitializer(New DropCreateDatabaseAlways(Of ApplicationDbContext)())
            Dim db = ApplicationDbContext.Create()
            '  db.Database.Initialize(True)
            Return db
        End Function

        '<Fact>
        'Public Async Function ApplicationUserGetRolesForUserTest() As Task
        '    Dim db = CreateDb()
        '    Dim userManager = New ApplicationUserManager(New UserStore(Of ApplicationUser)(db))
        '    Dim roleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(db))
        '    Dim users As ApplicationUser() = {New ApplicationUser("u1"), New ApplicationUser("u2"), New ApplicationUser("u3"), New ApplicationUser("u4")}
        '    Dim roles As IdentityRole() = {New IdentityRole("r1"), New IdentityRole("r2"), New IdentityRole("r3"), New IdentityRole("r4")}

        '    For Each u As ApplicationUser In users
        '        UnitTestHelper.IsSuccess(Await userManager.CreateAsync(u))
        '    Next

        '    For Each r As IdentityRole In roles
        '        UnitTestHelper.IsSuccess(Await roleManager.CreateAsync(r))

        '        For Each u As ApplicationUser In users
        '            UnitTestHelper.IsSuccess(Await userManager.AddToRoleAsync(u.Id, r.Name))
        '            Assert.[True](Await userManager.IsInRoleAsync(u.Id, r.Name))
        '        Next

        '        Assert.Equal(users.Length, r.Users.Count())
        '    Next

        '    For Each u As ApplicationUser In users
        '        Dim rs = Await userManager.GetRolesAsync(u.Id)
        '        Assert.Equal(roles.Length, rs.Count)

        '        For Each r As IdentityRole In roles
        '            Assert.[True](rs.Any(Function(role) role = r.Name))
        '        Next
        '    Next
        'End Function

        Public Class ApplicationDbContext
            Inherits IdentityDbContext(Of ApplicationUser)

            Public Sub New()
                MyBase.New("DefaultConnection", False)
            End Sub

            Public Shared Function Create() As ApplicationDbContext
                Return New ApplicationDbContext()
            End Function
        End Class

        Public Class ApplicationUser
            Inherits IdentityUser

            Public Sub New()
            End Sub

            Public Sub New(ByVal name As String)
                MyBase.New(name)
            End Sub

            Public Function GenerateUserIdentityAsync(ByVal manager As ApplicationUserManager) As Task(Of ClaimsIdentity)
                Return Task.FromResult(GenerateUserIdentity(manager))
            End Function

            Public Function GenerateUserIdentity(ByVal manager As ApplicationUserManager) As ClaimsIdentity
                Dim userIdentity = manager.CreateIdentity(Me, DefaultAuthenticationTypes.ApplicationCookie)
                Return userIdentity
            End Function
        End Class

        Public Class ApplicationUserManager
            Inherits UserManager(Of ApplicationUser)

            Public Sub New(ByVal store As IUserStore(Of ApplicationUser))
                MyBase.New(store)
            End Sub

            Public Shared Function Create(ByVal options As IdentityFactoryOptions(Of ApplicationUserManager), ByVal context As IOwinContext) As ApplicationUserManager
                Dim manager = New ApplicationUserManager(New UserStore(Of ApplicationUser)(context.[Get](Of ApplicationDbContext)()))
                manager.UserValidator = New UserValidator(Of ApplicationUser)(manager) With {
                    .AllowOnlyAlphanumericUserNames = False,
                    .RequireUniqueEmail = True
                }
                manager.PasswordValidator = New MinimumLengthValidator(6)
                manager.RegisterTwoFactorProvider("PhoneCode", New PhoneNumberTokenProvider(Of ApplicationUser) With {
                    .MessageFormat = "Your security code is: {0}"
                })
                manager.RegisterTwoFactorProvider("EmailCode", New EmailTokenProvider(Of ApplicationUser) With {
                    .Subject = "SecurityCode",
                    .BodyFormat = "Your security code is {0}"
                })
                'manager.EmailService = New EmailService()
                'manager.SmsService = New SMSService()
                'Dim dataProtectionProvider = options.DataProtectionProvider

                'If dataProtectionProvider IsNot Nothing Then
                '    manager.UserTokenProvider = New DataProtectorTokenProvider(Of ApplicationUser)(dataProtectionProvider.Create("ASP.NET Identity"))
                'End If

                Return manager
            End Function
        End Class

        Public Class EmailService
            'Inherits IIdentityMessageService

            'Public Function SendAsync(ByVal message As IdentityMessage) As Task
            '    Return Task.FromResult(0)
            'End Function
        End Class

        Public Class SMSService
            'Inherits IIdentityMessageService

            'Public Function SendAsync(ByVal message As IdentityMessage) As Task
            '    Return Task.FromResult(0)
            'End Function
        End Class
    End Class
End Namespace
