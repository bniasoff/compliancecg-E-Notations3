Imports System
Imports System.Collections.Generic
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework

Namespace Microsoft.AspNet.Identity.EntityFramework
    Public Class IdentityRole
        Inherits IdentityRole(Of String, IdentityUserRole)

        Public Sub New()
            Id = Guid.NewGuid().ToString()
        End Sub

        Public Sub New(ByVal roleName As String)
            Me.New()
            Name = roleName
        End Sub
    End Class

    Public Class IdentityRole(Of TKey, TUserRole As IdentityUserRole(Of TKey))
        Implements IRole(Of TKey)

        Public Sub New()
            Users = New List(Of TUserRole)()
        End Sub

        Public Overridable Property Users As ICollection(Of TUserRole)
        Public Property Id As TKey Implements IRole(Of TKey).Id
        Public Property Name As String Implements IRole(Of TKey).Name

        'Private ReadOnly Property Id As TKey Implements IRole(Of TKey).Id
        '    Get
        '        Throw New NotImplementedException()
        '    End Get
        'End Property

        'Private Property Name As String Implements IRole(Of TKey).Name
        '    Get
        '        Throw New NotImplementedException()
        '    End Get
        '    Set(value As String)
        '        Throw New NotImplementedException()
        '    End Set
        'End Property
    End Class
End Namespace
