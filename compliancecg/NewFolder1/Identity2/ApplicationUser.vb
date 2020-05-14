Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity
Imports System.ComponentModel.DataAnnotations
Imports System.Collections.Generic

Namespace AspNetExtendingIdentityRoles.Models
    Public Class ApplicationUser
        Inherits IdentityUser

        <Required>
        Public Property FirstName As String
        <Required>
        Public Property LastName As String
        <Required>
        Public Property Email As String
        Public Property UserId As String
    End Class
End Namespace
