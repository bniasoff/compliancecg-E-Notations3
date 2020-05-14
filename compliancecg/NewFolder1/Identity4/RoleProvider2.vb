Public Class CustomRoleProvider
    Inherits RoleProvider

    Public Sub New()
    End Sub

    Public Overrides Function IsUserInRole(ByVal username As String, ByVal roleName As String) As Boolean
        Throw New NotImplementedException()
        'Dim roles As List(Of String) = Users.GetRoles(username)
        'Return roles.Count <> 0 AndAlso roles.Contains(roleName)
    End Function

    Public Overrides Function GetRolesForUser(ByVal username As String) As String()
        Throw New NotImplementedException()
        'Dim roles As List(Of String) = Users.GetRoles(username)
        'Return roles.ToArray()
    End Function

    Public Overrides Sub AddUsersToRoles(ByVal usernames As String(), ByVal roleNames As String())
        Throw New NotImplementedException()
    End Sub

    Public Overrides Property ApplicationName As String
        Get
            Throw New NotImplementedException()
        End Get
        Set(ByVal value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Overrides Sub CreateRole(ByVal roleName As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Function DeleteRole(ByVal roleName As String, ByVal throwOnPopulatedRole As Boolean) As Boolean
        Throw New NotImplementedException()
    End Function

    Public Overrides Function FindUsersInRole(ByVal roleName As String, ByVal usernameToMatch As String) As String()
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetAllRoles() As String()
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetUsersInRole(ByVal roleName As String) As String()
        Throw New NotImplementedException()
    End Function

    Public Overrides Sub RemoveUsersFromRoles(ByVal usernames As String(), ByVal roleNames As String())
        Throw New NotImplementedException()
    End Sub

    Public Overrides Function RoleExists(ByVal roleName As String) As Boolean
        Throw New NotImplementedException()
    End Function
End Class
