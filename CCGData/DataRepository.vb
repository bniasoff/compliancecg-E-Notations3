Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Runtime.Serialization
Imports CCGData.CCGData

Imports Microsoft.EntityFrameworkCore.ChangeTracking
Imports NLog

Public Class DataRepository
    Inherits DbContext
    Private Shared CCGDataEntities As New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
    'Private Shared CCGDataEntities As New CCGDataEntities
    Private Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Public Sub New()
        'CCGDataEntities.Database.CommandTimeout = 600
        'CCGDataEntities.Configuration.LazyLoadingEnabled = False
        'CCGDataEntities.Configuration.LazyLoadingEnabled = True
    End Sub
    Public Function GetLoginUser(UserName As String) As AspNetUser
        Try
            Dim AspNetUser = CCGDataEntities.AspNetUsers.Where(Function(u) u.Email = UserName).SingleOrDefault

            Return AspNetUser
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Shared Function GetStaffMember(EmailAddress As String) As StaffMember
        Try
            Dim StaffMember = CCGDataEntities.StaffMembers.Where(Function(u) u.EmailAddress = EmailAddress).SingleOrDefault

            Return StaffMember
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacility(FacilityID As Integer) As Facility
        Try
            Dim Facility = CCGDataEntities.Facilities.Where(Function(u) u.FacilityID = FacilityID And Not u.InActive.Value.Equals(True)).SingleOrDefault

            Return Facility
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilityUsers(FacilityName As String) As List(Of User)
        Try
            Dim Users As New List(Of User)

            Dim Facility = CCGDataEntities.Facilities.Where(Function(u) u.Name = FacilityName).SingleOrDefault

            If Facility IsNot Nothing Then
                Users = CCGDataEntities.FacilityUsers.Where(Function(u) u.FacilityID = Facility.FacilityID And Not u.InActive.Value.Equals(True)).Select(Function(u) u.User).ToList
            End If

            Return Users
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function


    Public Function GetFacilityUsers(FacilityID As Integer) As List(Of User)
        Try
            Dim Users As New List(Of User)

            Dim Facility = CCGDataEntities.Facilities.Where(Function(u) u.FacilityID = FacilityID And Not u.InActive.Value.Equals(True)).SingleOrDefault

            If Facility IsNot Nothing Then
                Users = CCGDataEntities.FacilityUsers.Where(Function(u) u.FacilityID = Facility.FacilityID And Not u.InActive.Value.Equals(True)).Select(Function(u) u.User).ToList
            End If

            Return Users
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilityUsers2(FacilityID As Integer) As List(Of User)

        Try
            Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                'Dim Facility = CCGDataEntities.Facilities.Where(Function(u) u.FacilityID = FacilityID).SingleOrDefault

                Dim Users As New List(Of User)
                Users = CCGDataEntities.FacilityUsers.Where(Function(u) u.FacilityID = FacilityID And Not u.InActive.Value.Equals(True)).Select(Function(u) u.User).ToList

                Return Users
            End Using
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function


    'Public Shared Function GetFacilities(UserName As String) As List(Of Facility)
    '    Try
    '        Dim FacilityUser = GetFacilityUser(UserName)

    '        Dim Facilities As List(Of Facility) = CCGDataEntities.Facilities.Where(Function(u) u.FacilityUsers.FirstOrDefault.UserID = FacilityUser.UserID).ToList

    '        Return Facilities
    '  Catch ex As Exception
    'logger.Error(ex)

    '    End Try
    'End Function

    Public Shared Function GetFacilities3(UserName As String) As Facility
        Try
            Dim FacilityUser = GetFacilityUser(UserName)

            Dim FacilitiesForUser As List(Of FacilityUser) = CCGDataEntities.FacilityUsers.Where(Function(u) u.UserID = FacilityUser.UserID And Not u.InActive.Value.Equals(True)).ToList


            If FacilitiesForUser IsNot Nothing Then
                Dim FacilityID = FacilitiesForUser.FirstOrDefault.FacilityID
                Dim Facility As Facility = CCGDataEntities.Facilities.Where(Function(u) u.FacilityID = FacilityID And Not u.InActive.Value.Equals(True)).SingleOrDefault
                Return Facility
            End If
            ' 
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Shared Function GetFacilities2(UserName As String) As List(Of Facility)
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.Configuration.ProxyCreationEnabled = False

                Dim FacilityUser = GetFacilityUser(UserName)

                Dim Facilities As List(Of Facility) = CCGDataEntities.Facilities.Where(Function(u) u.FacilityUsers.FirstOrDefault.UserID = FacilityUser.UserID And Not u.InActive.Value.Equals(True)).ToList

                Return Facilities
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function

    Public Shared Function GetFacilitiesGroup2(UserName As String) As List(Of FacilityGroup)

        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.Configuration.ProxyCreationEnabled = False

                Dim GroupsIDs As Core.Objects.ObjectResult(Of Integer?) = CCGDataEntities.GetGroupsFromUser(UserName)

                Dim FacilityGroups As New List(Of FacilityGroup)
                For Each ID As Integer In GroupsIDs
                    Dim FacilityGroup = CCGDataEntities.FacilityGroups.Where(Function(u) u.FacilityGroupID = ID And Not u.InActive.Value.Equals(True)).SingleOrDefault
                    If FacilityGroup IsNot Nothing Then FacilityGroups.Add(FacilityGroup)
                Next
                Return FacilityGroups
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function

    Private Shared Sub UpdateLoginToAdd()
        Try
            Dim Logins As List(Of LoginsAdd) = CCGDataEntities.LoginsAdds.Where(Function(l) l.Added Is Nothing).ToList

            For Each Login As LoginsAdd In Logins
                Dim User As User = GetFacilityUser(Login.Email)
                If User IsNot Nothing Then
                    If CCGDataEntities.AspNetUsers.Where(Function(u) u.Email = User.EmailAddress).Any Then
                        Login.Added = True
                    End If
                End If
            Next
            CCGDataEntities.SaveChanges()
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Sub

    Public Shared Function GetStaffMembers() As List(Of StaffMember)
        Try
            Dim StaffMembers As List(Of StaffMember) = CCGDataEntities.StaffMembers.ToList

            Return StaffMembers
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Shared Function GetLoginsToAdd() As List(Of User)
        Try
            UpdateLoginToAdd()

            Dim Users As New List(Of User)
            Dim Logins As List(Of LoginsAdd) = CCGDataEntities.LoginsAdds.Where(Function(l) l.Added Is Nothing).ToList

            For Each Login As LoginsAdd In Logins
                Dim User As User = GetFacilityUser(Login.Email)
                Dim AspNetUser As AspNetUser = GetAspNetUser(Login.Email)

                If AspNetUser IsNot Nothing Then
                    Login.Added = True
                End If

                If User IsNot Nothing Then
                    Users.Add(User)
                End If
            Next
            Dim Update = CCGDataEntities.SaveChanges()
            Return Users
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function


    Public Function GetFacilityGroupAdmin(FacilityGroupID As Integer, UserID As Integer) As FacilityOwner
        Try
            Dim FacilityOwners = CCGDataEntities.FacilityOwners.Where(Function(u) u.FacilityGroupID = FacilityGroupID And u.UserID = UserID).SingleOrDefault

            Return FacilityOwners
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilityGroupAdmin(FacilityOwnerID As Integer) As FacilityOwner
        Try
            Dim FacilityOwners = CCGDataEntities.FacilityOwners.Where(Function(u) u.FacilityOwnerID = FacilityOwnerID).SingleOrDefault

            Return FacilityOwners
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilityUser(FacilityUserID As Integer) As FacilityUser
        Try
            Dim FacilityUser = CCGDataEntities.FacilityUsers.Where(Function(u) u.FacilityUserID = FacilityUserID And Not u.InActive.Value.Equals(True)).SingleOrDefault

            Return FacilityUser
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilityUser(FacilityID As Integer, UserID As Integer, Active As Boolean) As FacilityUser
        Try
            Dim FacilityUser As FacilityUser = Nothing
            If Active = True Then FacilityUser = CCGDataEntities.FacilityUsers.Where(Function(u) u.FacilityID = FacilityID And u.UserID = UserID And Not u.InActive.Value.Equals(True)).SingleOrDefault
            If Active = False Then FacilityUser = CCGDataEntities.FacilityUsers.Where(Function(u) u.FacilityID = FacilityID And u.UserID = UserID And u.InActive.Value.Equals(True)).SingleOrDefault

            Return FacilityUser
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function UpdateUsersID() As Boolean
        Try
            Dim Users = CCGDataEntities.Users.Where(Function(U) U.EmailAddress IsNot Nothing).ToList

            For Each User As User In Users
                Dim AspNetUser = GetLoginUser(User.EmailAddress)
                If AspNetUser IsNot Nothing Then
                    User.AspNetUserID = AspNetUser.Id
                End If
            Next

            Return True
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function
    Public Function UpdateUserID(EmailAddress As String) As Boolean
        Try
            Dim Users = CCGDataEntities.Users.Where(Function(U) U.EmailAddress = EmailAddress).ToList
            Dim AspNetUser = GetLoginUser(EmailAddress)

            For Each User As User In Users
                If User IsNot Nothing Then
                    User.UserID = AspNetUser.Id
                End If
            Next

            Return True
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function



    Public Function GetFacilityGroups() As List(Of FacilityGroup)
        Try
            Dim FacilityGroups As List(Of FacilityGroup) = CCGDataEntities.FacilityGroups.Where(Function(g) Not g.InActive.Value.Equals(True)).OrderBy(Function(g) g.GroupName).ToList
            Return FacilityGroups
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetJobTitles() As List(Of JobTitle)

        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                Dim JobTitles As List(Of JobTitle) = CCGDataEntities.JobTitles.OrderBy(Function(g) g.Title).ToList
                Return JobTitles
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using

    End Function

    Public Function GetFacilityGroups2() As List(Of FacilityGroup)
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                'Dim FacilityGroups As List(Of FacilityGroup) = CCGDataEntities.FacilityGroups.Where(Function(g) Not g.InActive.Value.Equals(True)).OrderBy(Function(g) g.GroupName).ToList
                Dim FacilityGroups As List(Of FacilityGroup) = CCGDataEntities.FacilityGroups.OrderBy(Function(g) g.GroupName).ToList
                Return FacilityGroups
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function
    Public Function GetFacilites() As List(Of Facility)
        Try
            Dim Facilities As List(Of Facility) = CCGDataEntities.Facilities.Where(Function(f) Not f.InActive.Value.Equals(True)).OrderBy(Function(f) f.Name).ToList
            Return Facilities
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilites2() As List(Of Facility)
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                Dim Facilities As List(Of Facility) = CCGDataEntities.Facilities.Where(Function(f) Not f.InActive.Value.Equals(True)).OrderBy(Function(f) f.Name).ToList
                Return Facilities
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function

    Public Function GetFaciliteByGroups2(GroupID As Integer, Active As Boolean) As List(Of Facility)
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.Configuration.ProxyCreationEnabled = False

                Dim Facilities As List(Of Facility)
                If Active = True Then Facilities = CCGDataEntities.Facilities.Where(Function(f) f.FacilityGroupID = GroupID And Not f.InActive.Value.Equals(True)).ToList
                If Active = False Then Facilities = CCGDataEntities.Facilities.Where(Function(f) f.FacilityGroupID = GroupID And f.InActive.Value.Equals(True)).ToList

                Return Facilities
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function

    Public Function GetGroupUsers2(GroupID As Integer) As List(Of User)
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.Configuration.ProxyCreationEnabled = False

                Dim UserIds As Core.Objects.ObjectResult(Of Integer?) = CCGDataEntities.GetGroupUsers(GroupID)

                Dim Users As New List(Of User)
                For Each ID As Integer In UserIds
                    Dim User = CCGDataEntities.Users.Where(Function(u) u.UserID = ID).SingleOrDefault
                    If User IsNot Nothing Then Users.Add(User)
                Next
                Return Users
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function

    Public Function GetFacilitesByGroup(GroupID As Integer) As List(Of Facility)

        Try
            Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                Dim Facilities As List(Of Facility) = CCGDataEntities.Facilities.Where(Function(f) f.FacilityGroupID = GroupID And Not f.InActive.Value.Equals(True) And f.State IsNot Nothing).ToList
                Return Facilities
            End Using
        Catch ex As Exception
            logger.Error(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetFacilityGroup(GroupID As Integer) As FacilityGroup
        Try
            'Dim FacilityGroup As FacilityGroup = CCGDataEntities.FacilityGroups.Where(Function(f) f.FacilityGroupID = GroupID And Not f.InActive.Value.Equals(True)).SingleOrDefault

            Dim FacilityGroup As FacilityGroup = CCGDataEntities.FacilityGroups.Where(Function(f) f.FacilityGroupID = GroupID).SingleOrDefault

            Return FacilityGroup

        Catch ex As Exception
            logger.Error(ex)
            Return Nothing
        End Try

    End Function

    Public Function GetFacilityByID(FacilityID As Integer) As List(Of Facility)
        Try
            Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                Dim Facilities As List(Of Facility) = CCGDataEntities.Facilities.Where(Function(f) f.FacilityID = FacilityID And Not f.InActive.Value.Equals(True)).ToList
                Return Facilities
            End Using
        Catch ex As Exception
            logger.Error(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetFacilityFromUser(UserName As String, Active As Boolean) As List(Of Facility)
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.Configuration.ProxyCreationEnabled = False

                Dim FacilityIds As Core.Objects.ObjectResult(Of Integer?) = CCGDataEntities.GetFacilitiesFromUser(UserName)

                Dim Facilities As New List(Of Facility)
                For Each ID As Integer In FacilityIds
                    Dim Facility As Facility = Nothing
                    If Active = True Then Facility = CCGDataEntities.Facilities.Where(Function(u) u.FacilityID = ID And Not u.InActive.Value.Equals(True)).SingleOrDefault
                    If Active = False Then Facility = CCGDataEntities.Facilities.Where(Function(u) u.FacilityID = ID And u.InActive.Value.Equals(True)).SingleOrDefault

                    If Facility IsNot Nothing Then Facilities.Add(Facility)
                Next
                Return Facilities
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function

    Public Function GetGroupsFromUser(UserName As String) As List(Of FacilityGroup)
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.Configuration.ProxyCreationEnabled = False

                Dim GroupIds As Core.Objects.ObjectResult(Of Integer?) = CCGDataEntities.GetGroupsFromUser(UserName)

                Dim Groups As New List(Of FacilityGroup)
                For Each ID As Integer In GroupIds
                    Dim Group = CCGDataEntities.FacilityGroups.Where(Function(u) u.FacilityGroupID = ID And Not u.InActive.Value.Equals(True)).SingleOrDefault
                    If Group IsNot Nothing Then Groups.Add(Group)
                Next
                Return Groups
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function


    'Public Function GetPolicyReplacements() As List(Of PolicyReplacement)
    '    Try
    '        Dim PolicyReplacements As List(Of PolicyReplacement) = CCGDataEntities.PolicyReplacements.ToList
    '        Return PolicyReplacements
    '  Catch ex As Exception
    'logger.Error(ex)

    '    End Try
    'End Function

    'Public Function GetGroupUserAdmins() As List(Of Facility)
    '    Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
    '        Try
    '            CCGDataEntities.Configuration.ProxyCreationEnabled = False
    '            Dim Facilities As List(Of Facility) = CCGDataEntities.Facilities.OrderBy(Function(f) f.Name).ToList
    '            Return Facilities
    '      Catch ex As Exception
    'logger.Error(ex)
    '            Return Nothing
    '        End Try
    '    End Using
    'End Function


    Public Function GetJobTitle(Title As String) As JobTitle
        Try
            Dim JobTitle As JobTitle = CCGDataEntities.JobTitles.Where(Function(j) j.Title = Title).SingleOrDefault

            Return JobTitle
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function


    Public Function GetFacilityUsersJob(FacilityUserID As Integer, JobTitleID As Integer) As FacilityUsersJob
        Try
            Dim FacilityUsersJob As FacilityUsersJob = CCGDataEntities.FacilityUsersJobs.Where(Function(f) f.FacilityUserID = FacilityUserID And f.JobTitleID = JobTitleID).SingleOrDefault

            Return FacilityUsersJob
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function
    Public Function GetFacilityUser2(FacilityID As Integer, UserID As Integer) As FacilityUser
        Try
            Dim FacilityUser As FacilityUser = CCGDataEntities.FacilityUsers.Where(Function(u) u.FacilityID = FacilityID And u.UserID = UserID).SingleOrDefault

            Return FacilityUser
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Shared Function GetFacilityUser(UserName As String) As CCGData.User
        Try

            'If UserName = "info@compliancecg.com" Then
            '    UserName = "Bbaxter@cypressga.com"
            ' Dim User As CCGData.User = CCGDataEntities.Users.Where(Function(u) u.UserID = 2153).SingleOrDefault

            Dim User As CCGData.User = CCGDataEntities.Users.Where(Function(u) u.EmailAddress = UserName And Not u.InActive.Value.Equals(True)).SingleOrDefault

            Return User
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Shared Function GetAspNetUser(UserName As String) As AspNetUser
        Try
            Dim AspNetUser As AspNetUser = CCGDataEntities.AspNetUsers.Where(Function(u) u.Email = UserName).SingleOrDefault

            Return AspNetUser
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function


    Public Function GetUser(UserID As Integer) As User
        Try
            Dim User As User = CCGDataEntities.Users.Where(Function(u) u.UserID = UserID And Not u.InActive.Value.Equals(True)).SingleOrDefault

            Return User
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function
    Public Function GetUser(UserName As String) As User
        Try
            Dim User As User = CCGDataEntities.Users.Where(Function(u) u.EmailAddress = UserName).SingleOrDefault
            'Dim User As User = CCGDataEntities.Users.Where(Function(u) u.EmailAddress = UserName And Not u.InActive.Value.Equals(True)).SingleOrDefault

            Return User
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    'Public Function AddUser(ByVal user As ApplicationUser) As User
    '    Try
    '        Dim User As User = CCGDataEntities.Users.Where(Function(u) u.EmailAddress = UserName).SingleOrDefault

    '        Return User
    '    Catch ex As Exception
    '        logger.Error(ex)

    '    End Try
    'End Function

    Public Function UserCheck(UserID As Integer) As Boolean
        Try
            If CCGDataEntities.Users.Where(Function(u) u.UserID = UserID And Not u.InActive.Value.Equals(True)).Any Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function


    Public Function GetUser2(UserID As Integer) As User
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                Dim User As User = CCGDataEntities.Users.Where(Function(u) u.UserID = UserID And Not u.InActive.Value.Equals(True)).SingleOrDefault
                Return User
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using



        'Try
        '    Dim Users As List(Of User) = CCGDataEntities.Users.Where(Function(u) u.UserID = UserID).ToList

        '    Return Users
        'Catch ex As Exception

        'End Try
    End Function

    Public Function GetUserInfo(UserName As String) As List(Of GetUserInfo_Result)
        Try
            Dim UserInfo As Core.Objects.ObjectResult(Of GetUserInfo_Result) = CCGDataEntities.GetUserInfo(UserName)

            Return UserInfo.ToList
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    'Public Function GetGroupOwnerUsers(GroupID As Integer) As List(Of CCGData.User)
    '    Try
    '        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
    '            Try
    '                CCGDataEntities.Configuration.ProxyCreationEnabled = False
    '                Dim Users As List(Of CCGData.User) = CCGDataEntities.FacilityOwners.Where(Function(g) g.FacilityGroupID = GroupID).Select(Function(u) u.User).ToList
    '                Return Users
    '          Catch ex As Exception
    'logger.Error(ex)
    '                Return Nothing
    '            End Try
    '        End Using
    '  Catch ex As Exception
    'logger.Error(ex)

    '    End Try
    'End Function

    Public Function GetGroupFromID(GroupID As Integer) As FacilityGroup
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                Dim FacilityGroup As FacilityGroup = CCGDataEntities.FacilityGroups.Where(Function(g) g.FacilityGroupID = GroupID).SingleOrDefault
                Return FacilityGroup
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function

    Public Function GetGroupFromGroupName(GroupName As String) As FacilityGroup
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                Dim FacilityGroup As FacilityGroup = CCGDataEntities.FacilityGroups.Where(Function(g) g.GroupName = GroupName).SingleOrDefault
                Return FacilityGroup
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function

    Public Function GetGroupOwnerUsers(GroupID As Integer) As List(Of FacilityOwner)
        Try
            Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
                Try
                    CCGDataEntities.Configuration.ProxyCreationEnabled = False
                    Dim FacilityOwners As List(Of CCGData.FacilityOwner) = CCGDataEntities.FacilityOwners.Where(Function(g) g.FacilityGroupID = GroupID).Select(Function(u) u).ToList
                    Return FacilityOwners
                Catch ex As Exception
                    logger.Error(ex)
                    Return Nothing
                End Try
            End Using
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetUsersFromGroup(GroupID As Integer) As List(Of User)
        Try
            Dim UserIDs As Core.Objects.ObjectResult(Of Integer?) = CCGDataEntities.GetGroupUsers(GroupID)
            Dim Users As New List(Of User)

            For Each UserID As Integer In UserIDs
                Dim User = CCGDataEntities.Users.Where(Function(u) u.UserID = UserID And Not u.InActive.Value.Equals(True)).SingleOrDefault
                If User IsNot Nothing Then Users.Add(User)
            Next

            Return Users

        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    'Public Function GetUserGroupOwner(UserName As String) As List(Of FacilityGroup)
    '    Try
    '        Dim Groups As New List(Of FacilityGroup)
    '        Dim User = CCGDataEntities.Users.Where(Function(u) u.EmailAddress = UserName).SingleOrDefault
    '        If User IsNot Nothing Then
    '            'Groups = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUser.User.UserID = User.UserID And j.FacilityUser.Facility.FacilityGroup.FacilityGroupID = GroupID).Select(Function(o) o.FacilityUser.Facility.FacilityGroup).ToList
    '            'Dim FacilityUserJob = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUser.User.UserID = User.UserID)
    '            'Groups = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUser.User.UserID = User.UserID).Select(Function(o) o.FacilityUser.Facility.FacilityGroup).ToList

    '            'Groups = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUser.User.UserID = User.UserID And j.JobTitle.Title = "Owner").Select(Function(o) o.FacilityUser.Facility.FacilityGroup).ToList


    '            If Groups.Count > 0 Then
    '                For Each Group As FacilityGroup In Groups
    '                    ' If Group.Facilities.Where(Function(f) f.FacilityUsers.) Then
    '                Next
    '            End If

    '        End If
    '        Dim c = Groups.Count
    '        'Dim GroupOwnes = CCGDataEntities.FacilityOwners.Where(Function(g) g.UserID = User.UserID).ToList


    '        'If GroupOwnes IsNot Nothing Then
    '        '    For Each Group As FacilityOwner In GroupOwnes
    '        '        Dim FacilityGroup As FacilityGroup = CCGDataEntities.FacilityGroups.Where(Function(u) u.FacilityGroupID = Group.FacilityGroupID).SingleOrDefault
    '        '        If FacilityGroup IsNot Nothing Then Groups.Add(FacilityGroup)
    '        '    Next
    '        'End If
    '        'End If

    '        Return Groups
    '  Catch ex As Exception
    'logger.Error(ex)

    '    End Try
    'End Function

    Public Function UserGroupOwner(UserName As String, GroupID As Integer) As FacilityGroup
        Try

            'Dim FacilityUserJob = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUser.User.UserID = User.UserID)
            'Groups = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUser.User.UserID = User.UserID).Select(Function(o) o.FacilityUser.Facility.FacilityGroup).ToList
            'Groups = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUser.User.UserID = User.UserID And j.JobTitle.Title = "Owner").Select(Function(o) o.FacilityUser.Facility.FacilityGroup).ToList

            Dim User = CCGDataEntities.Users.Where(Function(u) u.EmailAddress = UserName).SingleOrDefault
            If User IsNot Nothing Then
                Dim Group As FacilityGroup = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUser.User.UserID = User.UserID And j.FacilityUser.Facility.FacilityGroup.FacilityGroupID = GroupID And j.JobTitle.Title = "Owner" And Not j.FacilityUser.Facility.FacilityGroup.InActive.Value.Equals(True)).Select(Function(o) o.FacilityUser.Facility.FacilityGroup).SingleOrDefault
                If Group IsNot Nothing Then Return Group
            End If

        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function
    Public Function IsUserGroupOwner(UserName As String, GroupID As Integer) As Boolean
        Try
            'Dim FacilityUserJob = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUser.User.UserID = User.UserID)
            'Groups = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUser.User.UserID = User.UserID).Select(Function(o) o.FacilityUser.Facility.FacilityGroup).ToList
            'Groups = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUser.User.UserID = User.UserID And j.JobTitle.Title = "Owner").Select(Function(o) o.FacilityUser.Facility.FacilityGroup).ToList

            'Owner
            'Operator
            'Regional Administrator

            Dim User = CCGDataEntities.Users.Where(Function(u) u.EmailAddress = UserName).SingleOrDefault
            If User IsNot Nothing Then
                If User.EmailAddress = "info@compliancecg.com" Then Return True
                Dim FacilityUserRoles As List(Of FacilityUserRole) = CCGDataEntities.FacilityUserRoles.Where(Function(j) j.EmailAddress = User.EmailAddress And j.FacilityGroupID = GroupID And (j.Title = "Owner" Or j.Title = "Operator" Or j.Title = "Regional Administrator" Or j.Title = "Regional Human Resources" Or j.Title.Substring(0, 7) = "Regional")).ToList
                If FacilityUserRoles.Count > 0 Then Return True
                'Dim FacilityGroups As List(Of FacilityGroup) = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUser.User.UserID = User.UserID And j.FacilityUser.Facility.FacilityGroup.FacilityGroupID = GroupID And (j.JobTitle.Title = "Owner" Or j.JobTitle.Title = "Operator" Or j.JobTitle.Title = "Regional Administrator")).Select(Function(o) o.FacilityUser.Facility.FacilityGroup).ToList
                'If FacilityGroups.Count > 0 Then Return True
            End If

        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function IsUserFacilityAdmin(UserName As String, FacilityID As Integer) As Boolean
        Try

            'Administrator
            'Compliance Officer

            Dim User = CCGDataEntities.Users.Where(Function(u) u.EmailAddress = UserName).SingleOrDefault
            If User IsNot Nothing Then
                If User.EmailAddress = "info@compliancecg.com" Then Return True
                Dim FacilityUserRoles As List(Of FacilityUserRole) = CCGDataEntities.FacilityUserRoles.Where(Function(j) j.EmailAddress = User.EmailAddress And j.FacilityID = FacilityID And (j.Title = "Administrator" Or j.Title = "Compliance Officer")).ToList
                If FacilityUserRoles.Count > 0 Then Return True
            End If

        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function IsUserAdmin(UserName As String) As Boolean
        Try

            'Administrator
            'Compliance Officer

            Dim User = CCGDataEntities.Users.Where(Function(u) u.EmailAddress = UserName).SingleOrDefault
            If User IsNot Nothing Then
                If User.EmailAddress = "info@compliancecg.com" Then Return True
                Dim FacilityUserRoles As List(Of FacilityUserRole) = CCGDataEntities.FacilityUserRoles.Where(Function(j) j.EmailAddress = User.EmailAddress And (j.Title = "Administrator" Or j.Title = "Compliance Officer" Or j.Title = "Owner" Or j.Title = "Operator" Or j.Title = "Regional Administrator" Or j.Title = "Regional Human Resources" Or j.Title.Substring(0, 7) = "Regional")).ToList
                If FacilityUserRoles.Count > 0 Then Return True
            End If

        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetUsers() As List(Of CCGData.User)
        Try
            Dim Users As List(Of CCGData.User) = CCGDataEntities.Users.Where(Function(u) Not u.InActive.Value.Equals(True)).OrderBy(Function(f) f.LastName).ThenBy(Function(f) f.FirstName).ToList
            Return Users
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetCovidMemos() As List(Of CCGData.CovidMemo)
        Try
            Dim Memos As List(Of CCGData.CovidMemo) = CCGDataEntities.CovidMemos.OrderBy(Function(f) f.DateSent).ToList
            Return Memos
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function
    Public Function GetCovidTools() As List(Of CCGData.CovidTool)
        Try
            Dim Tools As List(Of CCGData.CovidTool) = CCGDataEntities.CovidTools.ToList
            Return Tools
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function
    Public Function GetFacilityOwner(FacilityOwnerID As Integer) As FacilityOwner
        Try
            Dim FacilityOwner As FacilityOwner = CCGDataEntities.FacilityOwners.Where(Function(f) f.FacilityOwnerID = FacilityOwnerID).SingleOrDefault
            Return FacilityOwner
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetUsers2() As List(Of CCGData.User)
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                Dim Users As List(Of CCGData.User) = CCGDataEntities.Users.Where(Function(u) Not u.InActive.Value.Equals(True)).OrderBy(Function(f) f.LastName).ThenBy(Function(f) f.FirstName).ToList
                Return Users
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function

    Public Function GetSates2() As List(Of String)
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                Dim States As List(Of String) = CCGDataEntities.Facilities.Where(Function(u) Not u.InActive.Value.Equals(True)).GroupBy(Function(f) f.State).Select(Function(f) f.FirstOrDefault.State).ToList
                Return States
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function

    Public Function GetUsersForGroup2() As List(Of CCGData.User)
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                Dim Users As List(Of CCGData.User) = CCGDataEntities.Users.Where(Function(u) Not u.InActive.Value.Equals(True)).OrderBy(Function(f) f.LastName).ThenBy(Function(f) f.FirstName).ToList
                Return Users
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function

    'Public Function GetUserAdmins(GroupName As String) As List(Of CCGData.User)
    '    Try
    '        'Dim FacilityGroup As FacilityGroup = CCGDataEntities.FacilityGroups.Where(Function(g) g.GroupName = "Cypress Skilled Nursing").SingleOrDefault
    '        'FacilityGroup.Facilities.Where(Function(f) f.FacilityUsers.)
    '        Dim UserIDs = CCGDataEntities.GetGroupAdmins(GroupName).ToList
    '        Dim Users As New List(Of User)
    '        For Each ID As Integer In UserIDs
    '            Users.Add(GetUser(ID))
    '        Next
    '        Return Users
    '    Catch ex As Exception
    '        logger.Error(ex)

    '    End Try
    'End Function

    Public Function GetSectionIndexForState(State As String) As List(Of SectionIndex)
        Try
            Dim SectionIndexs As List(Of SectionIndex) = CCGDataEntities.SectionIndexes.Where(Function(s) s.State = State).OrderBy(Function(s) s.PolicyNo).ThenBy(Function(s) s.SectionFrom).ToList
            Return SectionIndexs
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetClientContacts() As List(Of ClientContact)
        Try
            Dim ClientContact As List(Of ClientContact) = CCGDataEntities.ClientContacts.OrderBy(Function(f) f.Last_Name).ThenBy(Function(f) f.First_Name).ToList
            Return ClientContact
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilityUser(FacilityID As Integer, EmailAddress As String) As FacilityUser
        Try
            Dim FacilityUser As FacilityUser = Nothing
            Dim User As User = GetUser(EmailAddress)

            If User IsNot Nothing Then FacilityUser = CCGDataEntities.FacilityUsers.Where(Function(fu) fu.FacilityID = FacilityID And fu.UserID = User.UserID).SingleOrDefault

            'If Active = True Then FacilityUser = CCGDataEntities.FacilityUsers.Where(Function(fu) fu.FacilityID = FacilityID And Not fu.InActive.Value.Equals(True) And fu.UserID = User.UserID).SingleOrDefault
            'If Active = False Then FacilityUser = CCGDataEntities.FacilityUsers.Where(Function(fu) fu.FacilityID = FacilityID And fu.InActive.Value.Equals(True) And fu.UserID = User.UserID).SingleOrDefault

            Return FacilityUser
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilitesUsers() As List(Of FacilityUser)
        Try
            Dim FacilityUsers As List(Of FacilityUser) = CCGDataEntities.FacilityUsers.Where(Function(u) Not u.InActive.Value.Equals(True)).OrderBy(Function(f) f.User.LastName).ThenBy(Function(f) f.User.FirstName).ToList
            Return FacilityUsers
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilitesUsers(FacilityID As Integer) As List(Of FacilityUser)
        Try
            Dim FacilityUsers As List(Of FacilityUser) = CCGDataEntities.FacilityUsers.Where(Function(fu) fu.FacilityID = FacilityID).ToList
            '            Dim FacilityUsers As List(Of FacilityUser) = CCGDataEntities.FacilityUsers.Where(Function(fu) fu.FacilityID = FacilityID And Not fu.InActive.Value.Equals(True)).ToList

            Return FacilityUsers
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilitesUsers2(FacilityID As Integer) As List(Of FacilityUser)
        Try
            Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                Dim FacilityUsers As List(Of FacilityUser) = CCGDataEntities.FacilityUsers.Where(Function(fu) fu.FacilityID = FacilityID And Not fu.InActive.Value.Equals(True)).ToList
                Return FacilityUsers
            End Using


        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function


    'Public Function GetFacilitesUsers2(FacilityID As Integer) As List(Of FacilityUser)
    '    Try
    '        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
    '            CCGDataEntities.Configuration.ProxyCreationEnabled = False
    '            Dim FacilityUsers As List(Of FacilityUser) = CCGDataEntities.FacilityUsers.Where(Function(fu) fu.FacilityID = FacilityID).ToList
    '            Return FacilityUsers
    '        End Using


    '  Catch ex As Exception
    'logger.Error(ex)

    '    End Try
    'End Function
    Public Function GetFacilityUserJob(FacilityUserJobID As Integer?) As FacilityUsersJob
        Try
            Dim FacilityUserJob As FacilityUsersJob = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUserJobID = FacilityUserJobID).SingleOrDefault
            Return FacilityUserJob
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilityUsersJob(FacilityUserJobID As Integer) As FacilityUsersJob
        Try
            Dim FacilityUserJob As FacilityUsersJob = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUserJobID And Not j.FacilityUser.InActive.Value.Equals(True)).SingleOrDefault
            Return FacilityUserJob
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function
    Public Function GetFacilityUsersJobs(FacilityID As Integer, UserID As Integer) As List(Of FacilityUsersJob)

        Try
            Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
                CCGDataEntities.Configuration.ProxyCreationEnabled = False

                Dim FacilityUserJobs As List(Of FacilityUsersJob) = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUser.FacilityID = FacilityID And j.FacilityUser.UserID = UserID And Not j.FacilityUser.InActive.Value.Equals(True)).ToList
                Return FacilityUserJobs

            End Using
        Catch ex As Exception
            logger.Error(ex)

        End Try




        Try

        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilityUsersJobs2(FacilityID As Integer, UserID As Integer) As List(Of FacilityUsersJob)
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                Dim FacilityUserJobs As List(Of FacilityUsersJob) = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUser.FacilityID = FacilityID And j.FacilityUser.UserID = UserID And Not j.FacilityUser.InActive.Value.Equals(True)).ToList

                Return FacilityUserJobs
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function

    Public Function GetFacilityUsersJobTitles2(FacilityID As Integer, UserID As Integer) As List(Of JobTitle)
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                Dim JobTitles As List(Of JobTitle) = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUser.FacilityID = FacilityID And j.FacilityUser.UserID = UserID And Not j.FacilityUser.InActive.Value.Equals(True)).Select(Function(j) j.JobTitle).ToList

                Return JobTitles
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function

    Public Function DeleteFacilityUsersJobs(FacilityUserJobID As Integer) As Boolean
        Try
            Dim FacilityUserJob As FacilityUsersJob = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUserJobID = FacilityUserJobID).SingleOrDefault
            CCGDataEntities.FacilityUsersJobs.Remove(FacilityUserJob)
            CCGDataEntities.SaveChanges()
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilitesUsersWithRole() As List(Of FacilityUser)
        Try
            'Dim FacilityUsers As List(Of FacilityUser) = CCGDataEntities.FacilityUsers.Where(Function(u) u.User.FacilityUserRoles.Count > 0).OrderBy(Function(f) f.User.LastName).ThenBy(Function(f) f.User.FirstName).ToList
            'Return FacilityUsers
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilitesUsers2() As List(Of FacilityUser)
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                Dim FacilityUsers As List(Of FacilityUser) = CCGDataEntities.FacilityUsers.Where(Function(f) Not f.InActive.Value.Equals(True)).OrderBy(Function(f) f.User.LastName).ThenBy(Function(f) f.User.FirstName).ToList
                Return FacilityUsers
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function

    Public Function GetFacilityUsersWithTitle2(FacilityID As Integer) As Core.Objects.ObjectResult(Of GetFacilityUsersWithTitle_Result)
        Try
            Dim FacilityUsersWithTitle As Core.Objects.ObjectResult(Of GetFacilityUsersWithTitle_Result) = CCGDataEntities.GetFacilityUsersWithTitle(FacilityID)
            Return FacilityUsersWithTitle
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetGroupUserAdminsByFacilityGroup(FacilityGroupID As Integer) As Core.Objects.ObjectResult(Of GetGroupUserAdminsWithRole_Result)
        Try
            Dim GroupUserAdmins As Core.Objects.ObjectResult(Of GetGroupUserAdminsWithRole_Result) = CCGDataEntities.GetGroupUserAdminsWithRole(FacilityGroupID)
            Return GroupUserAdmins
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function




    Public Function GetFacilitesUsersByFacility(FacilityID As Integer, Active As Boolean) As List(Of FacilityUser)
        Try
            Dim FacilityUsers As New List(Of FacilityUser)

            If Active = True Then FacilityUsers = CCGDataEntities.FacilityUsers.Where(Function(u) u.FacilityID = FacilityID And Not u.InActive.Value.Equals(True)).ToList
            If Active = False Then FacilityUsers = CCGDataEntities.FacilityUsers.Where(Function(u) u.FacilityID = FacilityID And u.InActive.Value.Equals(True)).ToList

            Return FacilityUsers
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function


    Public Function GetUsersByFacility(FacilityID As Integer, Active As Boolean) As List(Of User)
        Try
            Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
                CCGDataEntities.Configuration.ProxyCreationEnabled = False

                Dim FacilityUsers As List(Of FacilityUser) = GetFacilitesUsersByFacility(FacilityID, Active)
                Dim Users As New List(Of User)

                For Each FacilityUser As FacilityUser In FacilityUsers
                    Dim User As User = Nothing
                    'If Active = True Then User = CCGDataEntities.Users.Where(Function(u) u.UserID = FacilityUser.UserID And Not u.InActive.Value.Equals(True)).SingleOrDefault
                    'If Active = False Then User = CCGDataEntities.Users.Where(Function(u) u.UserID = FacilityUser.UserID And u.InActive.Value.Equals(True)).SingleOrDefault
                    User = CCGDataEntities.Users.Where(Function(u) u.UserID = FacilityUser.UserID).SingleOrDefault
                    User.FacilityInActive = Not Active
                    If User IsNot Nothing Then Users.Add(User)
                Next

                Return Users
            End Using
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilityByUser(UserID As Integer) As List(Of Facility)
        Try
            Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                'CCGDataEntities2.Configuration.LazyLoadingEnabled = False

                Dim FacilityUsers As List(Of FacilityUser) = CCGDataEntities.FacilityUsers.Where(Function(u) u.UserID = UserID).ToList
                Dim Facilities As New List(Of Facility)

                For Each FacilityUser As FacilityUser In FacilityUsers
                    Dim Facility As Facility = CCGDataEntities.Facilities.Where(Function(u) u.FacilityID = FacilityUser.FacilityID And Not u.InActive.Value.Equals(True)).SingleOrDefault
                    If Facility IsNot Nothing Then
                        Facilities.Add(Facility)
                    End If
                Next
                ' Dim Facilities As List(Of Facility) = CCGDataEntities2.Facilities.OrderBy(Function(f) f.Name).Take(2).ToList

                Return Facilities
            End Using
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetRole(JobTitleID As Integer) As String
        Dim JobTitle As String = CCGDataEntities.JobTitles.Where(Function(j) j.JobTitleID = JobTitleID).Select(Function(j) j.Title).SingleOrDefault
        Return JobTitle
    End Function

    Public Function GetRolesFromFacilityUser(FacilityID As Integer, UserID As Integer) As List(Of FacilityUsersJob)
        Dim FacilityUsersJobs As New List(Of FacilityUsersJob)

        Dim FacilityUser As FacilityUser = CCGDataEntities.FacilityUsers.Where(Function(j) j.FacilityID = FacilityID And j.UserID = UserID).SingleOrDefault

        If FacilityUser IsNot Nothing Then
            FacilityUsersJobs = CCGDataEntities.FacilityUsersJobs.Where(Function(j) j.FacilityUserID = FacilityUser.FacilityUserID).ToList
        End If
        Return FacilityUsersJobs
    End Function




    Public Function GetUsersByTitle(TitleID As Integer) As List(Of UsersByTitle)
        Try
            Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                'CCGDataEntities.Configuration.LazyLoadingEnabled = False
                Dim UsersByTitle As List(Of UsersByTitle) = CCGDataEntities.UsersByTitles.Where(Function(u) u.JobTitleID = TitleID And Not u.InActive.Value.Equals(True)).OrderBy(Function(u) u.LastName).ThenBy(Function(u) u.FirstName).ToList

                'Dim Users As New List(Of User)

                'For Each UserByTitle As UsersByTitle In UsersByTitle
                '    Users.Add(CCGDataEntities.Users.Where(Function(u) u.UserID = UserByTitle.UserID).SingleOrDefault)
                '    'Dim User = CCGDataEntities.Users.Where(Function(u) u.UserID = UserByTitle.UserID).SingleOrDefault
                '    'If User IsNot Nothing Then Users.Add(User)
                'Next

                'Return Users.OrderBy(Function(u) u.LastName).ThenBy(Function(u) u.FirstName)
                Return UsersByTitle
            End Using
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetUsersByFacility2(FacilityID As Integer) As List(Of GetUser)
        Try
            Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                'CCGDataEntities.Configuration.LazyLoadingEnabled = False
                Dim Users As List(Of GetUser) = CCGDataEntities.GetUsers.Where(Function(u) u.FacilityID = FacilityID And Not u.InActive.Value.Equals(True)).OrderBy(Function(u) u.LastName).ThenBy(Function(u) u.FirstName).ToList

                'Dim Users As New List(Of User)

                'For Each UserByTitle As UsersByTitle In UsersByTitle
                '    Users.Add(CCGDataEntities.Users.Where(Function(u) u.UserID = UserByTitle.UserID).SingleOrDefault)
                '    'Dim User = CCGDataEntities.Users.Where(Function(u) u.UserID = UserByTitle.UserID).SingleOrDefault
                '    'If User IsNot Nothing Then Users.Add(User)
                'Next

                'Return Users.OrderBy(Function(u) u.LastName).ThenBy(Function(u) u.FirstName)
                Return Users
            End Using
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetUsersByState(State As String) As List(Of GetUser)
        Try
            Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                Dim Users As List(Of GetUser) = CCGDataEntities.GetUsers.Where(Function(u) u.State = State And Not u.InActive.Value.Equals(True)).OrderBy(Function(u) u.LastName).ThenBy(Function(u) u.FirstName).ToList

                Return Users
            End Using
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function


    Public Function GetUsersSearch(Search As Search) As List(Of GetUser)
        Try
            Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
                CCGDataEntities.Configuration.ProxyCreationEnabled = False

                Dim Users As New List(Of GetUser)
                Dim UsersIQ As IQueryable(Of GetUser) = Nothing

                If Search.Active = True Then UsersIQ = CCGDataEntities.GetUsers.Where(Function(u) Not u.InActive.Value.Equals(True))
                If Search.Active = False Then UsersIQ = CCGDataEntities.GetUsers.Where(Function(u) u.InActive.Value.Equals(True))

                If Search.Group > 0 Then UsersIQ = UsersIQ.Where(Function(u) u.FacilityGroupID = Search.Group)
                If Search.Facility > 0 Then UsersIQ = UsersIQ.Where(Function(u) u.FacilityID = Search.Facility)

                '    If Search?.Title?.Length > 0 Then UsersIQ = UsersIQ.Where(Function(u) u.JobTitleID = Search.Title)


                If Search?.State?.Length > 0 Then UsersIQ = UsersIQ.Where(Function(u) u.State = Search.State)
                If Search.User > 0 Then UsersIQ = UsersIQ.Where(Function(u) u.UserID = Search.User)

                If Search?.Title?.Length > 0 Then UsersIQ = UsersIQ.Where(Function(u) u.RolesID.Contains(", " + Search.Title + ";"))


                Users = UsersIQ.ToList
                Return Users
            End Using
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilitiesBySearch(Search As Search) As List(Of GetFacility)
        Try
            Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
                CCGDataEntities.Configuration.ProxyCreationEnabled = False
                'CCGDataEntities.Configuration.LazyLoadingEnabled = False

                Dim Facilities As New List(Of GetFacility)
                Dim FacilitiesIQ As IQueryable(Of GetFacility) = Nothing

                If Search.Active = True Then FacilitiesIQ = CCGDataEntities.GetFacilities.Where(Function(u) Not u.InActive.Value.Equals(True))
                If Search.Active = False Then FacilitiesIQ = CCGDataEntities.GetFacilities.Where(Function(u) u.InActive.Value.Equals(True))

                If Search.Group > 0 Then FacilitiesIQ = FacilitiesIQ.Where(Function(u) u.FacilityGroupID = Search.Group)
                If Search.Facility > 0 Then FacilitiesIQ = FacilitiesIQ.Where(Function(u) u.FacilityID = Search.Facility)
                If Search?.State?.Length > 0 Then FacilitiesIQ = FacilitiesIQ.Where(Function(u) u.State = Search.State)

                'If Search?.Title?.Length > 0 Then FacilitiesIQ = FacilitiesIQ.Where(Function(u) u.JobTitleID = Search.Title)
                'If Search.User > 0 Then FacilitiesIQ = FacilitiesIQ.Where(Function(u) u.UserID = Search.User)

                Facilities = FacilitiesIQ.ToList
                Return Facilities
            End Using
        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function

    Public Function GetFacilitesUsersByFacility2(FacilityID As Integer) As List(Of FacilityUser)
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                ' CCGDataEntities.Configuration.ProxyCreationEnabled = False
                Dim FacilityUsers As List(Of FacilityUser) = CCGDataEntities.FacilityUsers.Where(Function(u) u.FacilityID = FacilityID And Not u.InActive.Value.Equals(True)).OrderBy(Function(f) f.User.LastName).ThenBy(Function(f) f.User.FirstName).ToList
                'Dim FacilityUsers As List(Of FacilityUser) = CCGDataEntities.FacilityUsers.Where(Function(u) u.FacilityID = FacilityID).ToList
                Return FacilityUsers
            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try
        End Using
    End Function


    Public Sub Add(Of T As Class)(ByVal newItem As T)
        Try
            CCGDataEntities.[Set](Of T)().Add(newItem)
        Catch ex As Exception
            logger.Error(ex)
        End Try
    End Sub

    Public Sub Remove(Of T As Class)(ByVal newItem As T)
        Try
            CCGDataEntities.[Set](Of T)().Remove(newItem)
        Catch ex As Exception
            logger.Error(ex)
        End Try
    End Sub
    Public Sub Attach(Of T As Class)(ByVal newItem As T)
        Try
            CCGDataEntities.[Set](Of T)().Attach(newItem)
            ' CCGDataEntities.Entry(newItem).State = System.Data.Entity.EntityState.Modified
        Catch ex As Exception
            logger.Error(ex)
        End Try
    End Sub

    'Public Function AttachAndSave(Of T As Class)(ByVal newItem As T) As Integer
    '    Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
    '        Try

    '            'Dim trackedEntity = CCGDataEntities.MyObjectsPropertys.Find(newItem.Id)
    '            'CCGDataEntities.Entry(trackedEntity).CurrentValues.SetValues(updatedObject)
    '            'CCGDataEntities.Entry(trackedEntity).State = EntityState.Modified
    '            'CCGDataEntities.SaveChanges()


    '            CCGDataEntities.[Set](Of T)().Attach(newItem)
    '            CCGDataEntities.Entry(newItem).State = System.Data.Entity.EntityState.Modified

    '            ' Dim modifiedEntities = ChangeTracker.Entries().Where(Function(p) p.State = EntityState.Modified).ToList()

    '            Dim Updated As Integer = CCGDataEntities.SaveChanges()
    '            'Console.Write(Updated)
    '            Return Updated
    '        Catch ex As Exception
    '            logger.Error(ex)
    '        End Try
    '    End Using
    'End Function



    Public Overridable Function AttachAndSave(Of TEntity As Class)(ByVal entityToUpdate As TEntity) As Integer
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try

                If CCGDataEntities.Entry(entityToUpdate).State = EntityState.Added Then Return 0

                Try
                    CCGDataEntities.[Set](Of TEntity).Attach(entityToUpdate)
                Catch ex As InvalidOperationException
                    Dim trackedEntity = CCGDataEntities.[Set](Of TEntity).Find(GetKeyValues(CCGDataEntities, entityToUpdate))
                    If trackedEntity Is Nothing Then Throw
                    If CCGDataEntities.Entry(trackedEntity).State <> EntityState.Unchanged Then Throw
                    CCGDataEntities.Entry(trackedEntity).State = EntityState.Detached
                    CCGDataEntities.[Set](Of TEntity).Attach(entityToUpdate)
                End Try

                CCGDataEntities.Entry(entityToUpdate).State = EntityState.Modified
                Dim Updated As Integer = CCGDataEntities.SaveChanges()
                Return Updated
            Catch ex As Exception
                logger.Error(ex)
            End Try
        End Using
    End Function

    Public Function GetKeyValues(Of TEntity As Class)(CCGDataEntities As CCGDataEntities, ByVal entity As TEntity) As Object()
        Dim objectContextAdapter = (CType(CCGDataEntities, IObjectContextAdapter))
        Dim name = GetType(TEntity).Name
        Dim entityKey = objectContextAdapter.ObjectContext.CreateEntityKey(name, entity)
        Dim result = entityKey.EntityKeyValues.[Select](Function(kv) kv.Value).ToArray()
        Return result
    End Function


    'Public Function AttachAndSave(Of T As Class)(ByVal newItem As T) As Integer

    '    Try
    '        CCGDataEntities.[Set](Of T)().Attach(newItem)
    '        CCGDataEntities.Entry(newItem).State = System.Data.Entity.EntityState.Modified

    '        ' Dim modifiedEntities = ChangeTracker.Entries().Where(Function(p) p.State = EntityState.Modified).ToList()

    '        Dim Updated As Integer = CCGDataEntities.SaveChanges()
    '        'Console.Write(Updated)
    '        Return Updated
    '    Catch ex As Exception
    '        logger.Error(ex)
    '    End Try

    'End Function


    Public Sub InsertAndSave(Of T As Class)(ByVal newItem As T)
        Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
            Try
                CCGDataEntities.[Set](Of T)().Add(newItem)
                CCGDataEntities.Entry(newItem).State = System.Data.Entity.EntityState.Added
                'Dim AddedEntities = ChangeTracker.Entries().Where(Function(p) p.State = EntityState.Added).ToList()

                CCGDataEntities.SaveChanges()
            Catch ex As Exception
                logger.Error(ex)
            End Try
        End Using
    End Sub



    'Public Sub InsertAndSave(Of T As Class)(ByVal newItem As T)
    '    Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
    '        Try
    '            CCGDataEntities.[Set](Of T)().Attach(newItem)
    '            CCGDataEntities.Entry(newItem).State = System.Data.Entity.EntityState.Added

    '            'For Each entity As T In CCGDataEntities.Users
    '            '    Dim myEntity = context.Entry(entity)
    '            '    myEntity.State = EntityState.Added
    '            'Next


    '            CCGDataEntities.SaveChanges()
    '      Catch ex As Exception
    'logger.Error(ex)
    '        End Try
    '    End Using
    'End Sub

    'Public Sub InsertAndSave(Of T As Class)(ByVal newItem As T)
    '    Try




    '        ' CCGDataEntities.SaveChanges()
    '        CCGDataEntities.[Set](Of T)().Add(newItem)


    '        CCGDataEntities.Entry(newItem).State = System.Data.Entity.EntityState.Modified
    '        Dim CT = CCGDataEntities.ChangeTracker

    '        Dim modifiedEntities = CT.Entries().Where(Function(p) p.State = EntityState.Modified).ToList()
    '        Dim AddedEntities = CT.Entries().Where(Function(p) p.State = EntityState.Added).ToList()



    '        ' GetPrimaryKeyValue2(AddedEntities.FirstOrDefault)
    '        CCGDataEntities.SaveChanges()
    '        'Me.dbSet = context.[Set](Of T)()

    '        'DbSet.Add(entity)
    '        '     context.SaveChanges()



    '        'Dim modifiedEntities = ChangeTracker.Entries().Where(Function(p) p.State = EntityState.Modified).ToList()
    '        'Dim AdddEntities = ChangeTracker.Entries().Where(Function(p) p.State = EntityState.Added).ToList()


    '        ' CCGDataEntities.SaveChanges()


    '        'Me.[Set](Of T)().Attach(newItem)
    '        'Me.Entry(newItem).State = System.Data.Entity.EntityState.Added
    '        'Me.SaveChanges()
    '  Catch ex As Exception
    'logger.Error(ex)

    '    End Try

    '    'Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
    '    '    Try
    '    '        CCGDataEntities.[Set](Of T)().Attach(newItem)
    '    '        CCGDataEntities.Entry(newItem).State = System.Data.Entity.EntityState.Added


    '    '        CCGDataEntities.SaveChanges()
    '    '  Catch ex As Exception
    'logger.Error(ex)
    '    '    End Try
    '    'End Using
    'End Sub
    'Public Sub InsertAndSave(Of T As Class)(ByVal newItem As T)
    '    Try




    '        ' CCGDataEntities.SaveChanges()
    '        CCGDataEntities.[Set](Of T)().Add(newItem)


    '        CCGDataEntities.Entry(newItem).State = System.Data.Entity.EntityState.Modified
    '        Dim CT = CCGDataEntities.ChangeTracker

    '        Dim modifiedEntities = CT.Entries().Where(Function(p) p.State = EntityState.Modified).ToList()
    '        Dim AddedEntities = CT.Entries().Where(Function(p) p.State = EntityState.Added).ToList()



    '        ' GetPrimaryKeyValue2(AddedEntities.FirstOrDefault)
    '        CCGDataEntities.SaveChanges()
    '        'Me.dbSet = context.[Set](Of T)()

    '        'DbSet.Add(entity)
    '        '     context.SaveChanges()



    '        'Dim modifiedEntities = ChangeTracker.Entries().Where(Function(p) p.State = EntityState.Modified).ToList()
    '        'Dim AdddEntities = ChangeTracker.Entries().Where(Function(p) p.State = EntityState.Added).ToList()


    '        ' CCGDataEntities.SaveChanges()


    '        'Me.[Set](Of T)().Attach(newItem)
    '        'Me.Entry(newItem).State = System.Data.Entity.EntityState.Added
    '        'Me.SaveChanges()
    '  Catch ex As Exception
    'logger.Error(ex)

    '    End Try

    '    'Using CCGDataEntities = New CCGDataEntities(ConnectionStrings.CCGEntityConnectionString.ToString)
    '    '    Try
    '    '        CCGDataEntities.[Set](Of T)().Attach(newItem)
    '    '        CCGDataEntities.Entry(newItem).State = System.Data.Entity.EntityState.Added


    '    '        CCGDataEntities.SaveChanges()
    '    '  Catch ex As Exception
    'logger.Error(ex)
    '    '    End Try
    '    'End Using
    'End Sub
    Private Function GetPrimaryKeyValue2(ByVal entry As DbEntityEntry) As Object
        Dim objectStateEntry = (CType(Me, IObjectContextAdapter)).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity)
        Return objectStateEntry.EntityKey.EntityKeyValues(0).Value
    End Function

    'Public Overrides Function SaveChanges() As Integer
    '    Dim modifiedEntities = ChangeTracker.Entries().Where(Function(p) p.State = EntityState.Modified).ToList()
    '    Dim now = DateTime.UtcNow

    '    For Each change In modifiedEntities
    '        Dim entityName = change.Entity.[GetType]().Name
    '        Dim primaryKey = GetPrimaryKeyValue(change)

    '        For Each prop In change.OriginalValues.PropertyNames
    '            Dim originalValue = change.OriginalValues(prop).ToString()
    '            Dim currentValue = change.CurrentValues(prop).ToString()

    '            If originalValue <> currentValue Then
    '            End If
    '        Next
    '    Next

    '    Return MyBase.SaveChanges()
    'End Function

    'Public Sub Update(ParamArray entities As T())
    '    Using context = New BorselDBEntities()

    '        For Each entity As T In entities
    '            Dim myEntity = context.Entry(entity)
    '            myEntity.State = EntityState.Modified
    '        Next

    '        context.SaveChanges()
    '    End Using
    'End Sub
    Public Sub State(Of T As Class)(ByVal newItem As T, State As System.Data.Entity.EntityState)
        Try

            CCGDataEntities.Entry(newItem).State = State
        Catch ex As Exception
            logger.Error(ex)
        End Try
    End Sub


    'Public Function AddError(Err As ErrorLog) As Boolean
    '    CCGDataEntities.ErrorLogs.Add(Err)
    '    SaveChanges()
    'End Function
    Public Overrides Function SaveChanges() As Integer
        Try
            'SellerCloudEntities.QBCompanyID = QBCompanyID
            Dim Updated As Integer = CCGDataEntities.SaveChanges()
            'Console.Write(Updated)
            Return Updated
        Catch ex As Exception
            logger.Error(ex)
            ' logger.Error(ex)

        End Try
    End Function

    'Private Sub SurroundingSub()
    '    Dim movie = context.Movies.Find(2)
    '    movie.Title = "The Great Gatsby"
    '    context.SaveChanges()
    'End Sub
    Private Function GetPrimaryKeyValue(ByVal entry As DbEntityEntry) As Object
        Dim objectStateEntry = (CType(Me, IObjectContextAdapter)).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity)
        Return objectStateEntry.EntityKey.EntityKeyValues(0).Value
    End Function

    'Public Overrides Function SaveChanges() As Integer

    '    Dim modifiedEntities = ChangeTracker.Entries().Where(Function(p) p.State = EntityState.Modified).ToList()
    '    Dim AdddEntities = ChangeTracker.Entries().Where(Function(p) p.State = EntityState.Added).ToList()

    '    For Each change In modifiedEntities
    '        Dim entityName = change.Entity.[GetType]().Name
    '        Dim primaryKey = GetPrimaryKeyValue(change)

    '        For Each prop In change.OriginalValues.PropertyNames
    '            Dim originalValue = change.OriginalValues(prop).ToString()
    '            Dim currentValue = change.CurrentValues(prop).ToString()

    '            If originalValue <> currentValue Then
    '                Dim log As ChangeLog = New ChangeLog() With {
    '                    .EntityName = entityName,
    '                    .PrimaryKeyValue = primaryKey.ToString(),
    '                .PropertyName = prop,
    '                    .OldValue = originalValue,
    '                    .NewValue = currentValue,
    '                    .DateChanged = Now
    '                }
    '                '  ChangeLog.Add(log)
    '            End If
    '        Next
    '    Next

    '    Return MyBase.SaveChanges()
    'End Function

End Class


Public Class ChangeLog
    Public Property Id As Integer
    Public Property EntityName As String
    Public Property PropertyName As String
    Public Property PrimaryKeyValue As String
    Public Property OldValue As String
    Public Property NewValue As String
    Public Property DateChanged As DateTime
End Class
'Public Class GenericRepository(Of T As Class)
'    Inherits DbContext

'    Friend context As YourConext
'    Friend dbSet As DbSet(Of T)

'    Public Sub New(ByVal context As YourContext)
'        Me.context = context
'        Me.dbSet = context.[Set](Of T)()
'    End Sub

'    Public Overridable Sub Insert(ByVal entity As T)
'        dbSet.Add(entity)
'        context.SaveChanges()
'    End Sub
'End Class
'<Extension()>
'Public Shared Sub Create(Of T As Class)(ByVal db As DbContext, ByVal entityToCreate As T)
'    db.[Set](Of T)().Add(entityToCreate)
'    db.SaveChanges()
'End Sub