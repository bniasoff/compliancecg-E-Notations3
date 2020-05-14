Namespace Microsoft.AspNet.Identity.EntityFramework
    Public Class IdentityUserRole
        Inherits IdentityUserRole(Of String)
    End Class

    Public Class IdentityUserRole(Of TKey)
        Public Overridable Property UserId As TKey
        Public Overridable Property RoleId As TKey
    End Class
End Namespace
