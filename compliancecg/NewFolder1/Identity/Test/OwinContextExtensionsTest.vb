Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Xunit

Namespace Identity.Test
    Public Class OwinContextExtensionsTest
        ' <Fact>
        Public Sub MiddlewareExtensionsNullCheckTest()
            Dim context As IOwinContext = Nothing
            'ExceptionHelper.ThrowsArgumentNull(Function() context.[Get](Of Object)(), "context")
            'ExceptionHelper.ThrowsArgumentNull(Function() context.GetUserManager(Of Object)(), "context")
            'ExceptionHelper.ThrowsArgumentNull(Function() context.[Set](Of Object)(Nothing), "context")
        End Sub
    End Class
End Namespace
