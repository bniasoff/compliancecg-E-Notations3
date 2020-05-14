Imports CCGData.CCGData
Imports Microsoft.AspNet.Identity.Owin
Imports NLog
Public Module UserHistory
    Private CCGDataEntities As New CCGDataEntities(ConnectionStrings.CCGDataEntitiesConnectionString.ToString)
    Private logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Public Function AddUserHistory(CurrentUser As ApplicationUser, result As SignInStatus, LoginType As LoginType)
        Try
            Dim IPHostGenerator As New IPHostGenerator
            Dim IPAddress As String = IPHostGenerator.GetVisitorDetails
            Dim Location As DataTable = IPHostGenerator.GetLocation(IPAddress)
            Dim MachineName As String = IPHostGenerator.GetMachineNameUsingIPAddress(IPAddress)

            If Not IsNothing(CurrentUser) Then
                Dim newUserHistory As New CCGData.CCGData.UserHistory
                With newUserHistory
                    .UserID = CurrentUser.Id
                    .UserName = CurrentUser.UserName
                    .DateofEntry = Now
                    .URL = HttpContext.Current.Request.Url.ToString
                    '.Activity = Session("SessionCounter")
                    .IPAddress = IPAddress
                    .UserAgent = HttpContext.Current.Request.UserAgent
                    If LoginType = LoginType.LogIn Then .SignInStatus = result
                    If LoginType = LoginType.LogIn Then .Login = Now
                    If LoginType = LoginType.LogOff Then .Logoff = Now
                End With
                CCGDataEntities.UserHistories.Add(newUserHistory)
                CCGDataEntities.SaveChanges()
            End If

      Catch ex As Exception
logger.Error(ex)

        End Try
    End Function

    Public Enum LoginType
        LogIn = 1
        LogOff = 2
    End Enum
End Module
