Imports System.ComponentModel.DataAnnotations

Namespace Models
    Public Class EmailModel
        <Required, Display(Name:="Your name")>
        Public Property FromName As String
        <Required, Display(Name:="Your email"), EmailAddress>
        Public Property FromEmail As String
        <Required>
        Public Property Message As String
    End Class


End Namespace