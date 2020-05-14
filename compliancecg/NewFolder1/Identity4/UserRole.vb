Imports System.Security.Claims
Imports System.Threading.Tasks
Imports System.Xml.Serialization
Imports Microsoft.AspNet.Identity
Imports NLog

Namespace Membership
    Public Class CustomRole
        '   Inherits RoleManager(Of CustomRole)
        Implements IRole

        'Public Sub New()
        '    MyClass.New
        'End Sub

        Public ReadOnly Property Id As String Implements IRole(Of String).Id
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public Property Name As String Implements IRole(Of String).Name
            Get
                Throw New NotImplementedException()
            End Get
            Set(value As String)
                Throw New NotImplementedException()
            End Set
        End Property

        'Private ReadOnly Property IRole_Id As CustomRole Implements IRole(Of CustomRole).Id
        '    Get
        '        Throw New NotImplementedException()
        '    End Get
        'End Property

        'Private Property IRole_Name As String Implements IRole(Of CustomRole).Name
        '    Get
        '        Throw New NotImplementedException()
        '    End Get
        '    Set(value As String)
        '        Throw New NotImplementedException()
        '    End Set
        'End Property
    End Class
End Namespace