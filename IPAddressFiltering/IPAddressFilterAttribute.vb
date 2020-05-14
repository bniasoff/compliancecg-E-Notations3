Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Web.Http.Controllers
Imports System.Web
Imports System.Web.Http

Namespace IPAddressFiltering
    Public Class IPAddressFilterAttribute
        Inherits AuthorizeAttribute

        Private _ipAddresses As IEnumerable(Of IPAddress)
        Private _ipAddressRanges As IEnumerable(Of IPAddressRange)
        Private filteringType As IPAddressFilteringAction

        Public ReadOnly Property IPAddresses As IEnumerable(Of IPAddress)
            Get
                Return Me._ipAddresses
            End Get
        End Property

        Public ReadOnly Property IPAddressRanges As IEnumerable(Of IPAddressRange)
            Get
                Return Me._ipAddressRanges
            End Get
        End Property

        Public Sub New(ByVal ipAddress2 As String, ByVal filteringType As IPAddressFilteringAction)
            Me.New(New IPAddress() {IPAddress.Parse(ipAddress2)}, filteringType)
        End Sub

        Public Sub New(ByVal ipAddress As IPAddress, ByVal filteringType As IPAddressFilteringAction)
            Me.New(New IPAddress() {ipAddress}, filteringType)
        End Sub

        Public Sub New(ByVal _ipAddresses As IEnumerable(Of String), ByVal filteringType As IPAddressFilteringAction)
            Me.New(_ipAddresses.[Select](Function(a) IPAddress.Parse(a)), filteringType)
        End Sub

        Public Sub New(ByVal _ipAddresses As IEnumerable(Of IPAddress), ByVal filteringType As IPAddressFilteringAction)
            Me._ipAddresses = _ipAddresses
            Me.filteringType = filteringType
        End Sub

        Public Sub New(ByVal ipAddressRangeStart As String, ByVal ipAddressRangeEnd As String, ByVal filteringType As IPAddressFilteringAction)
            Me.New(New IPAddressRange() {New IPAddressRange(ipAddressRangeStart, ipAddressRangeEnd)}, filteringType)
        End Sub

        Public Sub New(ByVal ipAddressRange As IPAddressRange, ByVal filteringType As IPAddressFilteringAction)
            Me.New(New IPAddressRange() {ipAddressRange}, filteringType)
        End Sub

        Public Sub New(ByVal _ipAddressRanges As IEnumerable(Of IPAddressRange), ByVal filteringType As IPAddressFilteringAction)
            Me._ipAddressRanges = _ipAddressRanges
            Me.filteringType = filteringType
        End Sub

        Protected Overrides Function IsAuthorized(ByVal context As HttpActionContext) As Boolean
            Dim ipAddressString As String = (CType(context.Request.Properties("MS_HttpContext"), HttpContextWrapper)).Request.UserHostName
            Return IsIPAddressAllowed(ipAddressString)
        End Function

        Private Function IsIPAddressAllowed(ByVal ipAddressString As String) As Boolean
            Dim ipAddress As IPAddress = IPAddress.Parse(ipAddressString)

            If Me.filteringType = IPAddressFilteringAction.Allow Then

                If Me._ipAddresses IsNot Nothing AndAlso Me._ipAddresses.Any() AndAlso Not IsIPAddressInList(ipAddressString.Trim()) Then
                    Return False
                ElseIf Me._ipAddressRanges IsNot Nothing AndAlso Me._ipAddressRanges.Any() AndAlso Not Me._ipAddressRanges.Where(Function(r) ipAddress.IsInRange(r.StartIPAddress, r.EndIPAddress)).Any() Then
                    Return False
                End If
            Else

                If Me._ipAddresses IsNot Nothing AndAlso Me._ipAddresses.Any() AndAlso IsIPAddressInList(ipAddressString.Trim()) Then
                    Return False
                ElseIf Me._ipAddressRanges IsNot Nothing AndAlso Me._ipAddressRanges.Any() AndAlso Me._ipAddressRanges.Where(Function(r) ipAddress.IsInRange(r.StartIPAddress, r.EndIPAddress)).Any() Then
                    Return False
                End If
            End If

            Return True
        End Function

        Private Function IsIPAddressInList(ByVal ipAddress As String) As Boolean
            If Not String.IsNullOrWhiteSpace(ipAddress) Then
                Dim addresses As IEnumerable(Of String) = Me._ipAddresses.[Select](Function(a) a.ToString())
                Return addresses.Where(Function(a) a.Trim().Equals(ipAddress, StringComparison.InvariantCultureIgnoreCase)).Any()
            End If

            Return False
        End Function
    End Class
End Namespace
