Imports System.Web.Configuration
Imports System.Configuration

Public Class ConnectionStrings



    Public Shared Function CCGEntityConnectionString() As ConnectionStringSettings
        Try
            Dim myConfiguration As Configuration = WebConfigurationManager.OpenWebConfiguration("~/app.config")
            Dim myConnectionStringsSection As ConnectionStringsSection = myConfiguration.ConnectionStrings
            Dim myConnectionStringSettings As New ConnectionStringSettings

            'Dim myConfiguration = WebConfigurationManager.OpenWebConfiguration("~/Web.config")
            'Dim myConnectionStringsSection As ConnectionStringsSection = myConfiguration.ConnectionStrings
            'Dim myConnectionStringSettings As New ConnectionStringSettings


            Select Case My.Computer.Name
                Case "SC-BENYOMIN"
                    myConnectionStringSettings = myConnectionStringsSection.ConnectionStrings("CCGDataEntities")
                Case "NIASOFF-DESKTOP"
                    myConnectionStringSettings = myConnectionStringsSection.ConnectionStrings("CCGDataEntities2")
                Case "CCG-009"
                    myConnectionStringSettings = myConnectionStringsSection.ConnectionStrings("CCGDataEntities3")
                Case Else
                    myConnectionStringSettings = myConnectionStringsSection.ConnectionStrings("CCGDataEntities")
            End Select

            Return myConnectionStringSettings
      Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function
End Class


