Imports System.ComponentModel.DataAnnotations
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.Collections.Generic

Namespace AspNetExtendingIdentityRoles.Models
    Public Class ManageUserViewModel
        <Required>
        <DataType(DataType.Password)>
        <Display(Name:="Current password")>
        Public Property OldPassword As String
        <Required>
        <StringLength(100, ErrorMessage:="The {0} must be at least {2} characters long.", MinimumLength:=6)>
        <DataType(DataType.Password)>
        <Display(Name:="New password")>
        Public Property NewPassword As String
        <DataType(DataType.Password)>
        <Display(Name:="Confirm new password")>
        <Compare("NewPassword", ErrorMessage:="The new password and confirmation password do not match.")>
        Public Property ConfirmPassword As String
    End Class

    Public Class LoginViewModel
        <Required>
        <Display(Name:="User name")>
        Public Property UserName As String
        <Required>
        <DataType(DataType.Password)>
        <Display(Name:="Password")>
        Public Property Password As String
        <Display(Name:="Remember me?")>
        Public Property RememberMe As Boolean
    End Class

    Public Class RegisterViewModel
        <Required>
        <Display(Name:="User name")>
        Public Property UserName As String
        <Required>
        <StringLength(100, ErrorMessage:="The {0} must be at least {2} characters long.", MinimumLength:=6)>
        <DataType(DataType.Password)>
        <Display(Name:="Password")>
        Public Property Password As String
        <DataType(DataType.Password)>
        <Display(Name:="Confirm password")>
        <Compare("Password", ErrorMessage:="The password and confirmation password do not match.")>
        Public Property ConfirmPassword As String
        <Required>
        <Display(Name:="First Name")>
        Public Property FirstName As String
        <Required>
        <Display(Name:="Last Name")>
        Public Property LastName As String
        <Required>
        Public Property Email As String

        Public Function GetUser() As ApplicationUser
            Dim user = New ApplicationUser() With {
                .UserName = Me.UserName,
                .FirstName = Me.FirstName,
                .LastName = Me.LastName,
                .Email = Me.Email
            }
            Return user
        End Function
    End Class

    Public Class EditUserViewModel
        Public Sub New()
        End Sub

        Public Sub New(ByVal user As ApplicationUser)
            Me.UserName = user.UserName
            Me.FirstName = user.FirstName
            Me.LastName = user.LastName
            Me.Email = user.Email
        End Sub

        <Required>
        <Display(Name:="User Name")>
        Public Property UserName As String
        <Required>
        <Display(Name:="First Name")>
        Public Property FirstName As String
        <Required>
        <Display(Name:="Last Name")>
        Public Property LastName As String
        <Required>
        Public Property Email As String
    End Class

    Public Class SelectUserRolesViewModel
        Public Sub New()
            Me.Roles = New List(Of SelectRoleEditorViewModel)()
        End Sub

        Public Sub New(ByVal user As ApplicationUser)
            Me.New()
            Me.UserName = user.UserName
            Me.FirstName = user.FirstName
            Me.LastName = user.LastName
            Dim Db = New ApplicationDbContext()
            Dim allRoles = Db.Roles

            For Each role In allRoles
                Dim rvm = New SelectRoleEditorViewModel(role)
                Me.Roles.Add(rvm)
            Next

            'For Each userRole In user.Roles
            '    Dim checkUserRole = Me.Roles.Find(Function(r) r.RoleName = userRole.Role.Name)
            '    checkUserRole.Selected = True
            'Next
        End Sub

        Public Property UserName As String
        Public Property FirstName As String
        Public Property LastName As String
        Public Property Roles As List(Of SelectRoleEditorViewModel)
    End Class

    Public Class SelectRoleEditorViewModel
        Public Sub New()
        End Sub

        Public Sub New(ByVal role As ApplicationRole)
            Me.RoleName = role.Name
            Me.Description = role.Description
        End Sub

        Public Property Selected As Boolean
        <Required>
        Public Property RoleName As String
        Public Property Description As String
    End Class

    Public Class RoleViewModel
        Public Property RoleName As String
        Public Property Description As String

        Public Sub New()
        End Sub

        Public Sub New(ByVal role As ApplicationRole)
            Me.RoleName = role.Name
            Me.Description = role.Description
        End Sub
    End Class

    Public Class EditRoleViewModel
        Public Property OriginalRoleName As String
        Public Property RoleName As String
        Public Property Description As String

        Public Sub New()
        End Sub

        Public Sub New(ByVal role As ApplicationRole)
            Me.OriginalRoleName = role.Name
            Me.RoleName = role.Name
            Me.Description = role.Description
        End Sub
    End Class
End Namespace
