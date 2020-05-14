Imports System
Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security.DataProtection

Namespace Identity.Test
    Module TestUtil
        Sub SetupDatabase(Of TDbContext As DbContext)(ByVal dataDirectory As String)
            AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory)
            Database.SetInitializer(New DropCreateDatabaseAlways(Of TDbContext)())
        End Sub

        Function CreateManager(ByVal db As DbContext) As UserManager(Of IdentityUser)
            Dim options = New IdentityFactoryOptions(Of UserManager(Of IdentityUser)) With {
                .Provider = New TestProvider(db),
                .DataProtectionProvider = New DpapiDataProtectionProvider()
            }
            Return options.Provider.Create(options, New OwinContext())
        End Function

        Function CreateManager() As UserManager(Of IdentityUser)
            Return CreateManager(UnitTestHelper.CreateDefaultDb())
        End Function

        Async Function CreateManager(ByVal context As OwinContext) As Task
            Dim options = New IdentityFactoryOptions(Of UserManager(Of IdentityUser)) With {
                .Provider = New TestProvider(UnitTestHelper.CreateDefaultDb()),
                .DataProtectionProvider = New DpapiDataProtectionProvider()
            }
            Dim middleware = New IdentityFactoryMiddleware(Of UserManager(Of IdentityUser), IdentityFactoryOptions(Of UserManager(Of IdentityUser)))(Nothing, options)
            Await middleware.Invoke(context)
        End Function
    End Module

    Public Class TestProvider
        Inherits IdentityFactoryProvider(Of UserManager(Of IdentityUser))

        Public Sub New(ByVal db As DbContext)
            OnCreate = (Function(options, context)
                            Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
                            manager.UserValidator = New UserValidator(Of IdentityUser)(manager) With {
                                .AllowOnlyAlphanumericUserNames = True,
                                .RequireUniqueEmail = False
                            }
                            'manager.EmailService = New TestMessageService()
                            'manager.SmsService = New TestMessageService()

                            'If options.DataProtectionProvider IsNot Nothing Then
                            '    manager.UserTokenProvider = New DataProtectorTokenProvider(Of IdentityUser)(options.DataProtectionProvider.Create("ASP.NET Identity"))
                            'End If

                            Return manager
                        End Function)
        End Sub
    End Class

    'Public Class TestMessageService
    '    Inherits IIdentityMessageService

    '    Public Property Message As IdentityMessage

    '    Public Function SendAsync(ByVal message As IdentityMessage) As Task
    '        message = message
    '        Return Task.FromResult(0)
    '    End Function
    'End Class
End Namespace
