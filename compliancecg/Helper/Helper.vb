Imports CCGData
Imports CCGData.CCGData
Imports compliancecg.Controllers
Imports NLog

Public Class Functions
    Private DataRepository As New DataRepository
    Private Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Public Function CheckUserRole() As Integer

        Try
            '    Dim CurrentUser As User = HomeController.GetCurrentUser()
            '    Dim User As User = DataRepository.GetUser(CurrentUser.EmailAddress)
            '    If User.EmailAddress = "info@compliancecg.com" Then
            '        Return 1
            '    End If



            '    If Session("FacilityGroup") IsNot Nothing Then
            '        Dim FacilityGroup As FacilityGroup = Session("FacilityGroup")
            '        Dim IsUserGroupOwner As Boolean = DataRepository.IsUserGroupOwner(User.EmailAddress, FacilityGroup.FacilityGroupID)
            '        If IsUserGroupOwner = True Then Return 1
            '        ' Dim Group As FacilityGroup = DataRepository.UserGroupOwner(User.EmailAddress, FacilityGroup.FacilityGroupID)
            '    End If

            '    If Session("Facility") IsNot Nothing Then
            '        Dim Facility As Facility = Session("Facility")
            '        Dim IsUserFacilityAdmin As Boolean = DataRepository.IsUserFacilityAdmin(User.EmailAddress, Facility.FacilityID)
            '        If IsUserFacilityAdmin = True Then Return 2
            '    End If

            Return 0



        Catch ex As Exception
            logger.Error(ex)

        End Try
    End Function
End Class
