Imports System.Collections.Generic
Imports compliancecg.My.Resources

Namespace Microsoft.AspNet.Identity
    Public Class IdentityResult
        Private Shared ReadOnly _success As IdentityResult = New IdentityResult(True)

        Public Sub New(ParamArray errors As String())
            Me.New(CType(errors, IEnumerable(Of String)))
        End Sub

        Public Sub New(ByVal errors As IEnumerable(Of String))
            If errors Is Nothing Then
                errors = {Resource1.DefaultError}
            End If

            Succeeded = False
            errors = errors
        End Sub

        Protected Sub New(ByVal success As Boolean)
            Succeeded = success
            Errors = New String(-1) {}
        End Sub

        Public Property Succeeded As Boolean
        Public Property Errors As IEnumerable(Of String)

        Public Shared ReadOnly Property Success As IdentityResult
            Get
                Return _success
            End Get
        End Property

        Public Shared Function Failed(ParamArray errors As String()) As IdentityResult
            Return New IdentityResult(errors)
        End Function
    End Class
End Namespace
