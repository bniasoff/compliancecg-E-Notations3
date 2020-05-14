Namespace Microsoft.AspNet.Identity.EntityFramework
    Public Class IdentityUserLogin
        Inherits IdentityUserLogin(Of String)
    End Class

    Public Class IdentityUserLogin(Of TKey)
        Public Overridable Property LoginProvider As String
        Public Overridable Property ProviderKey As String
        Public Overridable Property UserId As TKey
    End Class
End Namespace
