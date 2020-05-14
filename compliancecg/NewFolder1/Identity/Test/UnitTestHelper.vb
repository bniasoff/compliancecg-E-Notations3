Imports System
Imports System.Data.Entity
Imports System.Data.Entity.SqlServer
Imports System.Globalization
Imports System.Linq
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Xunit

Namespace Identity.Test
    Module UnitTestHelper
        Public ReadOnly Property EnglishBuildAndOS As Boolean
            Get
                Dim englishBuild = String.Equals(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "en", StringComparison.OrdinalIgnoreCase)
                Dim englishOS = String.Equals(CultureInfo.CurrentCulture.TwoLetterISOLanguageName, "en", StringComparison.OrdinalIgnoreCase)
                Return englishBuild AndAlso englishOS
            End Get
        End Property

        Function CreateDefaultDb() As Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext
            ' Database.SetInitializer(New DropCreateDatabaseAlways(Of IdentityDbContext)())
            Dim db = New Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext()
            '  db.Database.Initialize(True)
            ' Dim foo = GetType(SqlProviderServices)
            Return db
        End Function

        '    Sub IsSuccess(ByVal result As IdentityResult)
        '        Assert.NotNull(result)
        '        Assert.[True](result.Succeeded)
        '    End Sub

        '    Sub IsFailure(ByVal result As IdentityResult)
        '        Assert.NotNull(result)
        '        Assert.[False](result.Succeeded)
        '    End Sub

        '    Sub IsFailure(ByVal result As IdentityResult, ByVal [error] As String)
        '        Assert.NotNull(result)
        '        Assert.[False](result.Succeeded)
        '        Assert.Equal([error], result.Errors.First())
        '    End Sub
    End Module

    'Public Class AlwaysBadValidator(Of T)
    '    Inherits IIdentityValidator(Of T)

    '    Public Const ErrorMessage As String = "I'm Bad."

    '    Public Function ValidateAsync(ByVal item As T) As Task(Of IdentityResult)
    '        Return Task.FromResult(IdentityResult.Failed(ErrorMessage))
    '    End Function
    'End Class

    'Public Class NoopValidator(Of T)
    '    Inherits IIdentityValidator(Of T)

    '    Public Function ValidateAsync(ByVal item As T) As Task(Of IdentityResult)
    '        Return Task.FromResult(IdentityResult.Success)
    '    End Function
    'End Class
End Namespace
