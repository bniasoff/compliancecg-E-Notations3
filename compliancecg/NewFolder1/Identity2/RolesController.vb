Imports compliancecg.AspNetExtendingIdentityRoles.Models
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web.Mvc

Namespace AspNetExtendingIdentityRoles.Controllers
    Public Class RolesController
        Inherits Controller

        Private _db As ApplicationDbContext = New ApplicationDbContext()

        Public Function Index() As ActionResult
            Dim rolesList = New List(Of RoleViewModel)()

            For Each role In _db.Roles
                Dim roleModel = New RoleViewModel(role)
                rolesList.Add(roleModel)
            Next

            Return View(rolesList)
        End Function

        <Authorize(Roles:="Admin")>
        Public Function Create(ByVal Optional message As String = "") As ActionResult
            ViewBag.Message = message
            Return View()
        End Function

        <HttpPost>
        <Authorize(Roles:="Admin")>
        Public Function Create(
        <Bind(Include:="RoleName,Description")> ByVal model As RoleViewModel) As ActionResult
            Dim message As String = "That role name has already been used"

            If ModelState.IsValid Then
                Dim role = New ApplicationRole(model.RoleName, model.Description)
                Dim idManager = New IdentityManager()

                If idManager.RoleExists(model.RoleName) Then
                    Return View(message)
                Else
                    idManager.CreateRole(model.RoleName, model.Description)
                    Return RedirectToAction("Index", "Account")
                End If
            End If

            Return View()
        End Function

        <Authorize(Roles:="Admin")>
        Public Function Edit(ByVal id As String) As ActionResult
            Dim role = _db.Roles.First(Function(r) r.Name = id)
            Dim roleModel = New EditRoleViewModel(role)
            Return View(roleModel)
        End Function

        <HttpPost>
        <Authorize(Roles:="Admin")>
        Public Function Edit(
        <Bind(Include:="RoleName,OriginalRoleName,Description")> ByVal model As EditRoleViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim role = _db.Roles.First(Function(r) r.Name = model.OriginalRoleName)
                role.Name = model.RoleName
                role.Description = model.Description
                _db.Entry(role).State = EntityState.Modified
                _db.SaveChanges()
                Return RedirectToAction("Index")
            End If

            Return View(model)
        End Function

        <Authorize(Roles:="Admin")>
        Public Function Delete(ByVal id As String) As ActionResult
            If id Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim role = _db.Roles.First(Function(r) r.Name = id)
            Dim model = New RoleViewModel(role)

            If role Is Nothing Then
                Return HttpNotFound()
            End If

            Return View(model)
        End Function

        <Authorize(Roles:="Admin")>
        <HttpPost, ActionName("Delete")>
        Public Function DeleteConfirmed(ByVal id As String) As ActionResult
            Dim role = _db.Roles.First(Function(r) r.Name = id)
            Dim idManager = New IdentityManager()
            idManager.DeleteRole(role.Id)
            Return RedirectToAction("Index")
        End Function
    End Class
End Namespace
