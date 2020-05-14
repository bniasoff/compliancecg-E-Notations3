Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.Owin.Security
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Web
Imports System.Web.Mvc

Imports compliancecg.AspNetExtendingIdentityRoles.Models

Namespace AspNetExtendingIdentityRoles.Controllers
    <Authorize>
    Public Class AccountController
        Inherits Controller

        Public Sub New()
            Me.New(New UserManager(Of ApplicationUser)(New UserStore(Of ApplicationUser)(New ApplicationDbContext())))
        End Sub

        Public Sub New(ByVal userManager As UserManager(Of ApplicationUser))
            userManager = userManager
        End Sub

        Public Property UserManager As UserManager(Of ApplicationUser)

        <AllowAnonymous>
        Public Function Login(ByVal returnUrl As String) As ActionResult
            ViewBag.ReturnUrl = returnUrl
            Return View()
        End Function

        <HttpPost>
        <AllowAnonymous>
        <ValidateAntiForgeryToken>
        Public Async Function Login(ByVal model As LoginViewModel, ByVal returnUrl As String) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Dim user = Await UserManager.FindAsync(model.UserName, model.Password)

                If user IsNot Nothing Then
                    Await SignInAsync(user, model.RememberMe)
                    Return RedirectToLocal(returnUrl)
                Else
                    ModelState.AddModelError("", "Invalid username or password.")
                End If
            End If

            Return View(model)
        End Function

        <Authorize(Roles:="Admin")>
        Public Function Register() As ActionResult
            Return View()
        End Function

        <HttpPost>
        <Authorize(Roles:="Admin")>
        <ValidateAntiForgeryToken>
        Public Async Function Register(ByVal model As RegisterViewModel) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Dim user = model.GetUser()
                Dim result = Await UserManager.CreateAsync(user, model.Password)

                If result.Succeeded Then
                    Return RedirectToAction("Index", "Account")
                End If
            End If

            Return View(model)
        End Function

        <Authorize(Roles:="Admin")>
        Public Function Manage(ByVal message As ManageMessageId?) As ActionResult
            ViewBag.StatusMessage = If(message = ManageMessageId.ChangePasswordSuccess, "Your password has been changed.", If(message = ManageMessageId.SetPasswordSuccess, "Your password has been set.", If(message = ManageMessageId.RemoveLoginSuccess, "The external login was removed.", If(message = ManageMessageId.[Error], "An error has occurred.", ""))))
            ViewBag.HasLocalPassword = HasPassword()
            ViewBag.ReturnUrl = Url.Action("Manage")
            Return View()
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        <Authorize(Roles:="Admin")>
        Public Async Function Manage(ByVal model As ManageUserViewModel) As Task(Of ActionResult)
            Dim hasPassword As Boolean = hasPassword
            ViewBag.HasLocalPassword = hasPassword
            ViewBag.ReturnUrl = Url.Action("Manage")

            If hasPassword Then

                If ModelState.IsValid Then
                    Dim result As IdentityResult = Await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword)

                    If result.Succeeded Then
                        Return RedirectToAction("Manage", New With {.Message = ManageMessageId.ChangePasswordSuccess})
                    Else
                        AddErrors(result)
                    End If
                End If
            Else
                Dim state As ModelState = ModelState("OldPassword")

                If state IsNot Nothing Then
                    state.Errors.Clear()
                End If

                If ModelState.IsValid Then
                    Dim result As IdentityResult = Await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword)

                    If result.Succeeded Then
                        Return RedirectToAction("Manage", New With {.Message = ManageMessageId.SetPasswordSuccess})
                    Else
                        AddErrors(result)
                    End If
                End If
            End If

            Return View(model)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Function LogOff() As ActionResult
            AuthenticationManager.SignOut()
            Return RedirectToAction("Index", "Home")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso UserManager IsNot Nothing Then
                UserManager.Dispose()
                UserManager = Nothing
            End If

            MyBase.Dispose(disposing)
        End Sub

        <Authorize(Roles:="Admin")>
        Public Function Index() As ActionResult
            Dim Db = New ApplicationDbContext()
            Dim users = Db.Users
            Dim model = New List(Of EditUserViewModel)()

            For Each User1 In users
                Dim u = New EditUserViewModel(User1)
                model.Add(u)
            Next

            Return View(model)
        End Function

        <Authorize(Roles:="Admin")>
        Public Function Edit(ByVal id As String, ByVal Optional Message As ManageMessageId? = Nothing) As ActionResult
            Dim Db = New ApplicationDbContext()
            Dim user As ApplicationUser = Db.Users.First(Function(u) u.UserName = id)
            Dim model = New EditUserViewModel(user)
            ViewBag.MessageId = Message
            Return View(model)
        End Function

        <HttpPost>
        <Authorize(Roles:="Admin")>
        <ValidateAntiForgeryToken>
        Public Async Function Edit(ByVal model As EditUserViewModel) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Dim Db = New ApplicationDbContext()
                Dim user = Db.Users.First(Function(u) u.UserName = model.UserName)
                user.FirstName = model.FirstName
                user.LastName = model.LastName
                user.Email = model.Email
                Db.Entry(user).State = System.Data.Entity.EntityState.Modified
                Await Db.SaveChangesAsync()
                Return RedirectToAction("Index")
            End If

            Return View(model)
        End Function

        <Authorize(Roles:="Admin")>
        Public Function Delete(ByVal Optional id As String = Nothing) As ActionResult
            Dim Db = New ApplicationDbContext()
            Dim user = Db.Users.First(Function(u) u.UserName = id)
            Dim model = New EditUserViewModel(user)

            If user Is Nothing Then
                Return HttpNotFound()
            End If

            Return View(model)
        End Function

        <HttpPost, ActionName("Delete")>
        <ValidateAntiForgeryToken>
        <Authorize(Roles:="Admin")>
        Public Function DeleteConfirmed(ByVal id As String) As ActionResult
            Dim Db = New ApplicationDbContext()
            Dim user = Db.Users.First(Function(u) u.UserName = id)
            Db.Users.Remove(user)
            Db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        <Authorize(Roles:="Admin")>
        Public Function UserRoles(ByVal id As String) As ActionResult
            Dim Db = New ApplicationDbContext()
            Dim user = Db.Users.First(Function(u) u.UserName = id)
            Dim model = New SelectUserRolesViewModel(user)
            Return View(model)
        End Function

        <HttpPost>
        <Authorize(Roles:="Admin")>
        <ValidateAntiForgeryToken>
        Public Function UserRoles(ByVal model As SelectUserRolesViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim idManager = New IdentityManager()
                Dim Db = New ApplicationDbContext()
                Dim user = Db.Users.First(Function(u) u.UserName = model.UserName)
                idManager.ClearUserRoles(user.Id)

                For Each role In model.Roles

                    If role.Selected Then
                        idManager.AddUserToRole(user.Id, role.RoleName)
                    End If
                Next

                Return RedirectToAction("index")
            End If

            Return View()
        End Function

        Private ReadOnly Property AuthenticationManager As IAuthenticationManager
            Get
                Return HttpContext.GetOwinContext().Authentication
            End Get
        End Property

        Private Async Function SignInAsync(ByVal user As ApplicationUser, ByVal isPersistent As Boolean) As Task
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie)
            Dim identity = Await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie)
            AuthenticationManager.SignIn(New AuthenticationProperties() With {
                .IsPersistent = isPersistent
            }, identity)
        End Function

        Private Sub AddErrors(ByVal result As IdentityResult)
            For Each [error] In result.Errors
                ModelState.AddModelError("", [error])
            Next
        End Sub

        'Private Function HasPassword(Id As String) As Boolean
        '    ' Dim user = UserManager.FindById(user.Identity.GetUserId())
        '    Dim user = UserManager.FindById(Id)
        '    If user IsNot Nothing Then
        '        Return user.PasswordHash IsNot Nothing
        '    End If

        '    Return False
        'End Function
        Private Function HasPassword() As Boolean
            'Dim user = UserManager.FindById(user.Identity.GetUserId())

            'If user IsNot Nothing Then
            '    Return user.PasswordHash IsNot Nothing
            'End If

            Return False
        End Function
        Public Enum ManageMessageId
            ChangePasswordSuccess
            SetPasswordSuccess
            RemoveLoginSuccess
            [Error]
        End Enum

        Private Function RedirectToLocal(ByVal returnUrl As String) As ActionResult
            If Url.IsLocalUrl(returnUrl) Then
                Return Redirect(returnUrl)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function
    End Class
End Namespace
