﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core.Objects
Imports System.Linq

Namespace CCGData

    Partial Public Class CCGDataEntities
        Inherits DbContext
    
        Public Sub New()
            MyBase.New("name=CCGDataEntities")
        End Sub
    
        Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
            Throw New UnintentionalCodeFirstException()
        End Sub
    
        Public Overridable Property AspNetRoles() As DbSet(Of AspNetRole)
        Public Overridable Property AspNetUserClaims() As DbSet(Of AspNetUserClaim)
        Public Overridable Property AspNetUserLogins() As DbSet(Of AspNetUserLogin)
        Public Overridable Property AspNetUsers() As DbSet(Of AspNetUser)
        Public Overridable Property UserHistories() As DbSet(Of UserHistory)
        Public Overridable Property JobTitles() As DbSet(Of JobTitle)
        Public Overridable Property LoginsAdds() As DbSet(Of LoginsAdd)
        Public Overridable Property ClientContacts() As DbSet(Of ClientContact)
        Public Overridable Property FacilityUsersJobs() As DbSet(Of FacilityUsersJob)
        Public Overridable Property ChangeLogs() As DbSet(Of ChangeLog)
        Public Overridable Property FacilityOwners() As DbSet(Of FacilityOwner)
        Public Overridable Property Facilities() As DbSet(Of Facility)
        Public Overridable Property FacilityGroups() As DbSet(Of FacilityGroup)
        Public Overridable Property Users() As DbSet(Of User)
        Public Overridable Property FacilityUserRoles() As DbSet(Of FacilityUserRole)
        Public Overridable Property FacilityUsers() As DbSet(Of FacilityUser)
        Public Overridable Property UsersByTitles() As DbSet(Of UsersByTitle)
        Public Overridable Property GetFacilities() As DbSet(Of GetFacility)
        Public Overridable Property GetUsers() As DbSet(Of GetUser)
        Public Overridable Property SectionIndexes() As DbSet(Of SectionIndex)
        Public Overridable Property StaffMembers() As DbSet(Of StaffMember)
    
        Public Overridable Function GetFacilitiesFromUser(userName As String) As ObjectResult(Of Nullable(Of Integer))
            Dim userNameParameter As ObjectParameter = If(userName IsNot Nothing, New ObjectParameter("UserName", userName), New ObjectParameter("UserName", GetType(String)))
    
            Return DirectCast(Me, IObjectContextAdapter).ObjectContext.ExecuteFunction(Of Nullable(Of Integer))("GetFacilitiesFromUser", userNameParameter)
        End Function
    
        Public Overridable Function GetFacilityUsersWithRole(facilityID As Nullable(Of Integer)) As ObjectResult(Of GetFacilityUsersWithRole_Result)
            Dim facilityIDParameter As ObjectParameter = If(facilityID.HasValue, New ObjectParameter("FacilityID", facilityID), New ObjectParameter("FacilityID", GetType(Integer)))
    
            Return DirectCast(Me, IObjectContextAdapter).ObjectContext.ExecuteFunction(Of GetFacilityUsersWithRole_Result)("GetFacilityUsersWithRole", facilityIDParameter)
        End Function
    
        Public Overridable Function GetGroupsFromUser(userName As String) As ObjectResult(Of Nullable(Of Integer))
            Dim userNameParameter As ObjectParameter = If(userName IsNot Nothing, New ObjectParameter("UserName", userName), New ObjectParameter("UserName", GetType(String)))
    
            Return DirectCast(Me, IObjectContextAdapter).ObjectContext.ExecuteFunction(Of Nullable(Of Integer))("GetGroupsFromUser", userNameParameter)
        End Function
    
        Public Overridable Function GetGroupUserAdminsWithRole(facilityGroupID As Nullable(Of Integer)) As ObjectResult(Of GetGroupUserAdminsWithRole_Result)
            Dim facilityGroupIDParameter As ObjectParameter = If(facilityGroupID.HasValue, New ObjectParameter("FacilityGroupID", facilityGroupID), New ObjectParameter("FacilityGroupID", GetType(Integer)))
    
            Return DirectCast(Me, IObjectContextAdapter).ObjectContext.ExecuteFunction(Of GetGroupUserAdminsWithRole_Result)("GetGroupUserAdminsWithRole", facilityGroupIDParameter)
        End Function
    
        Public Overridable Function GetGroupUsers(facilityGroupID As Nullable(Of Integer)) As ObjectResult(Of Nullable(Of Integer))
            Dim facilityGroupIDParameter As ObjectParameter = If(facilityGroupID.HasValue, New ObjectParameter("FacilityGroupID", facilityGroupID), New ObjectParameter("FacilityGroupID", GetType(Integer)))
    
            Return DirectCast(Me, IObjectContextAdapter).ObjectContext.ExecuteFunction(Of Nullable(Of Integer))("GetGroupUsers", facilityGroupIDParameter)
        End Function
    
        Public Overridable Function GetUserInfo(userName As String) As ObjectResult(Of GetUserInfo_Result)
            Dim userNameParameter As ObjectParameter = If(userName IsNot Nothing, New ObjectParameter("UserName", userName), New ObjectParameter("UserName", GetType(String)))
    
            Return DirectCast(Me, IObjectContextAdapter).ObjectContext.ExecuteFunction(Of GetUserInfo_Result)("GetUserInfo", userNameParameter)
        End Function
    
        Public Overridable Function GetFacilityUsersWithTitle(facilityID As Nullable(Of Integer)) As ObjectResult(Of GetFacilityUsersWithTitle_Result)
            Dim facilityIDParameter As ObjectParameter = If(facilityID.HasValue, New ObjectParameter("FacilityID", facilityID), New ObjectParameter("FacilityID", GetType(Integer)))
    
            Return DirectCast(Me, IObjectContextAdapter).ObjectContext.ExecuteFunction(Of GetFacilityUsersWithTitle_Result)("GetFacilityUsersWithTitle", facilityIDParameter)
        End Function
    
    End Class

End Namespace
