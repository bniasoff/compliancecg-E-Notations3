'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Namespace CCGData

    Partial Public Class FacilityUsersJob
        Public Property FacilityUserJobID As Integer
        Public Property FacilityUserID As Integer
        Public Property JobTitleID As Integer
        Public Property DateCreated As Nullable(Of Date)
        Public Property DateModified As Nullable(Of Date)
        Public Property UserEditor As String
    
        Public Overridable Property JobTitle As JobTitle
        Public Overridable Property FacilityUser As FacilityUser
    
    End Class

End Namespace
