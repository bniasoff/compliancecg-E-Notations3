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

    Partial Public Class Facility
        Public Property FacilityID As Integer
        Public Property FacilityGroupID As Integer
        Public Property InActive As Nullable(Of Boolean)
        Public Property Del As Nullable(Of Boolean)
        Public Property Alt As Nullable(Of Integer)
        Public Property Name As String
        Public Property FullAddress As String
        Public Property Address As String
        Public Property City As String
        Public Property State As String
        Public Property Zip As String
        Public Property Beds As String
        Public Property ShiftChange As String
        Public Property Phone1 As String
        Public Property Phone2 As String
        Public Property Fax As String
        Public Property FacilityType As String
        Public Property BeginDate As Nullable(Of Date)
        Public Property ComplianceOfficer As String
        Public Property Logo As Byte()
        Public Property LogoPath As String
        Public Property UserEditor As String
        Public Property DateCreated As Nullable(Of Date)
        Public Property DateModified As Nullable(Of Date)
        Public Property FacilityGroupName As String
        Public Property GroupPolicyPrint As Nullable(Of Boolean)
    
        Public Overridable Property FacilityGroup As FacilityGroup
        Public Overridable Property FacilityUsers As ICollection(Of FacilityUser) = New HashSet(Of FacilityUser)
    
    End Class

End Namespace
