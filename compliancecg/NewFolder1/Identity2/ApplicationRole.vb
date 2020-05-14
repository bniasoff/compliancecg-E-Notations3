Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity
Imports System.ComponentModel.DataAnnotations
Imports System.Collections.Generic

Namespace AspNetExtendingIdentityRoles.Models
    Public Class ApplicationRole
        Inherits IdentityRole

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal name As String, ByVal description As String)
            MyBase.New(name)
            Me.Description = description
        End Sub

        Public Overridable Property Description As String
    End Class
End Namespace
