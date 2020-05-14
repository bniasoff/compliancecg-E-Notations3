Namespace Microsoft.AspNet.Identity.EntityFramework
    Public Class IdentityUserClaim
        Inherits IdentityUserClaim(Of String)
    End Class

    Public Class IdentityUserClaim(Of TKey)
        Public Overridable Property Id As Integer
        Public Overridable Property UserId As TKey
        Public Overridable Property ClaimType As String
        Public Overridable Property ClaimValue As String
    End Class
End Namespace
