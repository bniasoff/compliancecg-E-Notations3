Imports System.Security.Claims
Imports System.Threading.Tasks
Imports System.Xml.Serialization
Imports Microsoft.AspNet.Identity
Imports NLog

Namespace Membership
    Public Class UserAccount
        Implements IUser
        Public Property Id As String
        Public Property UserName As String Implements IUser.UserName
        Public Property UserID As String Implements IUser.Id
        Public Property PasswordHash As String
        Public Property AccessFailedCount As Integer


        Public Property AccountLocked As Boolean = False
        Public Property AccountLockedUntil As DateTime = DateTime.MinValue
        Public Property PasswordLastChangedOn As DateTime = DateTime.MinValue
        Public Property CurrentLoginDate() As DateTime
        Public Property LastLoginFromIP() As String = ""

        Public Property AccountActivatedOn() As DateTime
        Public Property AccountActivated() As Boolean
        Public Property ActivationCode() As String = ""
        Public Property Password() As String
        Public Property FirstName() As String
        Public Property LastName() As String
        Public Property PhoneNumber As String

        Public ReadOnly Property FullName As String
            Get
                Dim names As New List(Of String)

                If Not String.IsNullOrEmpty(FirstName) Then names.Add(FirstName)
                If Not String.IsNullOrEmpty(LastName) Then names.Add(LastName)

                Return String.Join(" ", names.ToArray())
            End Get
        End Property

        Public Property Salt() As String
        ' Public Property Addresses() As Contacts.AddressCollection

        Public Property Email() As String
        '<XmlElement(ElementName:="CreationDate", IsNullable:=False, Form:=Xml.Schema.XmlSchemaForm.Qualified, DataType:="dateTime")>
        Public Property CreationDate() As Date
            Get
                Return CreationDateUTC.ToLocalTime
            End Get
            Set(ByVal Value As Date)
                CreationDateUTC = Value.ToUniversalTime
            End Set
        End Property

        <XmlIgnore()>
        Public Property CreationDateUTC() As Date

        '<XmlElement(ElementName:="LastLoginDate", IsNullable:=False, Form:=Xml.Schema.XmlSchemaForm.Qualified, DataType:="dateTime")>
        Public Property LastLoginDate() As Date
            Get
                Return LastLoginDateUTC.ToLocalTime
            End Get
            Set(ByVal Value As Date)
                LastLoginDateUTC = Value.ToUniversalTime
            End Set
        End Property

        <XmlIgnore()>
        Public Property LastLoginDateUTC() As Date
        Public Property PasswordHint() As String
        Public Property Comment() As String
        Public Property PricingLevel() As Integer

        Sub New()
            UserName = ""
            Password = ""
            FirstName = ""
            LastName = ""
            Salt = ""
            Email = ""
            CreationDateUTC = Date.UtcNow
            LastLoginDateUTC = Date.UtcNow
            PasswordHint = ""
            Comment = ""
            PricingLevel = 0
        End Sub

        Public Async Function GenerateUserIdentityAsync(ByVal manager As UserManager(Of Membership.UserAccount)) As Task(Of ClaimsIdentity)
            ' Logger.Log("Membership.UserAccount:GenerateUserIdentityAsync ()")
            Dim userIdentity = Await manager.CreateIdentityAsync(Me, DefaultAuthenticationTypes.ApplicationCookie)
            '  userIdentity.AddClaim()

            Return userIdentity
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("Id={0} PasswordHash={1}", Id, (If(PasswordHash Is Nothing, String.Empty, PasswordHash)))
        End Function

    End Class
End Namespace