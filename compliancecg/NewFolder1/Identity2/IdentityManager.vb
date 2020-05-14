Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity
Imports System.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Imports compliancecg.AspNetExtendingIdentityRoles.Models
Imports System.Linq

Namespace AspNetExtendingIdentityRoles.Models
    Public Class IdentityManager
        Private _roleManager As RoleManager(Of ApplicationRole) = New RoleManager(Of ApplicationRole)(New RoleStore(Of ApplicationRole)(New ApplicationDbContext()))
        Private _userManager As UserManager(Of ApplicationUser) = New UserManager(Of ApplicationUser)(New UserStore(Of ApplicationUser)(New ApplicationDbContext()))
        Private _db As ApplicationDbContext = New ApplicationDbContext()

        Public Function RoleExists(ByVal name As String) As Boolean
            Return _roleManager.RoleExists(name)
        End Function

        Public Function CreateRole(ByVal name As String, ByVal Optional description As String = "") As Boolean
            Dim idResult = _roleManager.Create(New ApplicationRole(name, description))
            Return idResult.Succeeded
        End Function

        Public Function CreateUser(ByVal user As ApplicationUser, ByVal password As String) As Boolean
            Dim idResult = _userManager.Create(user, password)
            Return idResult.Succeeded
        End Function

        Public Function AddUserToRole(ByVal userId As String, ByVal roleName As String) As Boolean
            Dim idResult = _userManager.AddToRole(userId, roleName)
            Return idResult.Succeeded
        End Function

        Public Sub ClearUserRoles(ByVal userId As String)
            Try


                Dim user = _userManager.FindById(userId)
                Dim currentRoles = New List(Of IdentityUserRole)()
                currentRoles.AddRange(user.Roles)

                'For Each role In currentRoles
                '    _userManager.RemoveFromRole(userId, role.Role.Name)
                'Next
            Catch ex As Exception

            End Try
        End Sub

        Public Sub RemoveFromRole(ByVal userId As String, ByVal roleName As String)
            _userManager.RemoveFromRole(userId, roleName)
        End Sub

        Public Sub DeleteRole(ByVal roleId As String)
            Dim roleUsers = _db.Users.Where(Function(u) u.Roles.Any(Function(r) r.RoleId = roleId))
            Dim role = _db.Roles.Find(roleId)

            For Each user In roleUsers
                Me.RemoveFromRole(user.Id, role.Name)
            Next

            _db.Roles.Remove(role)
            _db.SaveChanges()
        End Sub
    End Class
End Namespace
