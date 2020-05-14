Imports Microsoft.AspNet.Identity
Imports Crypto = compliancecg.Microsoft.AspNet.Identity.Crypto
Imports NLog
Public Class Users
    Private Shared Property DbContext As New ApplicationDbContext
    Private Shared Property Store As UserStore = New UserStore(New ApplicationDbContext)
    Public Sub New()
        'DbContext = New ApplicationDbContext
        'Store = New compliancecg.UserStore(DbContext)
    End Sub

    Public Shared Function Update(IdentityUser As ApplicationUser) As Boolean
        Try
            Store.UpdateAsync(IdentityUser)
            Return True
      Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function

    Public Shared Async Function FindUserbyUserName(UserName As String) As Threading.Tasks.Task(Of ApplicationUser)
        Try
            Dim IdentityUser As ApplicationUser = Await Store.FindByNameAsync(UserName)
            Return IdentityUser
      Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function

    Public Function FindRole(RoleName As String) As Role
        Try
            Dim Roles = DbContext.Roles.ToList
            Dim Role = DbContext.Roles.Where(Function(r) r.Name = RoleName).SingleOrDefault

            Return Role
      Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function

    Public Async Function GetRoles(IdentityUser As ApplicationUser) As Threading.Tasks.Task(Of List(Of String))
        Try
            Dim Roles As List(Of String) = Await Store.GetRolesAsync(IdentityUser)
            Return Roles
      Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function

    Public Async Function AddUser(LastName As String, FirstName As String, UserName As String, UserID As String, Password As String) As Threading.Tasks.Task(Of ApplicationUser)
        Try
            Dim IdentityUser As ApplicationUser = Nothing

            If IdentityUser Is Nothing Then
                IdentityUser = New ApplicationUser
                With IdentityUser
                    If LastName IsNot Nothing Then .LastName = LastName
                    If FirstName IsNot Nothing Then .FirstName = FirstName
                    If UserID IsNot Nothing Then .UserID = UserID
                    If UserName IsNot Nothing Then .Email = UserName
                    If UserName IsNot Nothing Then .UserName = UserName
                    .Password2 = Password
                End With

                If Password IsNot Nothing Then
                    Dim PasswordHash As String = Crypto.HashPassword(Password)
                    Await Store.SetPasswordHashAsync(IdentityUser, PasswordHash)
                End If

                Await Store.CreateAsync(IdentityUser)
                Dim RecordAdded = DbContext.SaveChanges()
            End If

            Return IdentityUser
      Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function

    Public Async Function AddUserToRole(IdentityUser As ApplicationUser, RoleName As String) As Threading.Tasks.Task(Of Boolean)
        Try
            Dim Role = FindRole(RoleName)

            If IdentityUser IsNot Nothing And Role IsNot Nothing Then
                Await Store.AddToRoleAsync(IdentityUser, Role.Name)
                DbContext.SaveChanges()
            End If
            Return True
      Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function

    Public Async Function RemoveUser(IdentityUser As ApplicationUser) As Threading.Tasks.Task(Of Boolean)
        Try
            If IdentityUser IsNot Nothing Then
                Await Store.DeleteAsync(IdentityUser)
                DbContext.SaveChanges()
            End If
            Return True
      Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function

    Public Async Function ChangePassword(IdentityUser As ApplicationUser, Password As String) As Threading.Tasks.Task(Of Boolean)
        Try
            If IdentityUser IsNot Nothing And Password IsNot Nothing Then
                Dim PasswordHash As String = Crypto.HashPassword(Password)
                Await Store.SetPasswordHashAsync(IdentityUser, PasswordHash)
                Dim RecordAdded = DbContext.SaveChanges()
            End If

            Return True
      Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function

    Public Async Function AddLoginToUser(IdentityUser As ApplicationUser, Login As UserLoginInfo) As Threading.Tasks.Task(Of Boolean)
        Try
            If IdentityUser IsNot Nothing And Login IsNot Nothing Then
                Await Store.AddLoginAsync(IdentityUser, Login)
                DbContext.SaveChanges()
            End If

            Return True
      Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function
End Class
