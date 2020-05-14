Imports System.Security.Claims
Imports System.Threading.Tasks
Imports compliancecg
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin

' You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
Public Class ApplicationUser
    Inherits IdentityUser
    Public Async Function GenerateUserIdentityAsync(manager As UserManager(Of ApplicationUser)) As Task(Of ClaimsIdentity)
        Dim UserID As Integer
        Dim Id As Integer
        'Dim UserID As String
        ' Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        Dim userIdentity = Await manager.CreateIdentityAsync(Me, DefaultAuthenticationTypes.ApplicationCookie)
        ' Add custom user claims here
        Return userIdentity
    End Function

    'Public Shared Widening Operator CType(v As ApplicationUser) As ApplicationUser
    '    Throw New NotImplementedException()
    'End Operator
End Class

Public Class ApplicationDbContext
    Inherits IdentityDbContext(Of ApplicationUser)
    'Public Sub New()
    '    MyBase.New("DefaultConnection2", throwIfV1Schema:=False)
    'End Sub

    Public Sub New()
        MyBase.New("IdentityConnection", throwIfV1Schema:=False)
    End Sub



    'Public Sub New()
    '    MyBase.New("CCGDataEntities", throwIfV1Schema:=False)
    'End Sub

    Public Shared Function Create() As ApplicationDbContext
        Return New ApplicationDbContext()
    End Function
End Class
