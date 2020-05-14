Imports System.Net

Namespace IPAddressFiltering
    Public Class IPAddressRange
        Private _startIPAddress As IPAddress
        Private _endIPAddress As IPAddress

        Public ReadOnly Property StartIPAddress As IPAddress
            Get
                Return Me._startIPAddress
            End Get
        End Property

        Public ReadOnly Property EndIPAddress As IPAddress
            Get
                Return Me._endIPAddress
            End Get
        End Property

        Public Sub New(ByVal startIPAddress As String, ByVal endIPAddress As String)
            Me.New(IPAddress.Parse(startIPAddress), IPAddress.Parse(endIPAddress))
        End Sub

        Public Sub New(ByVal startIPAddress As IPAddress, ByVal endIPAddress As IPAddress)
            Me._startIPAddress = startIPAddress
            Me._endIPAddress = endIPAddress
        End Sub
    End Class
End Namespace
