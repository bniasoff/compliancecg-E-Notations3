Imports System.Web.Configuration
Imports System.Configuration
Imports NLog

Public Class ConnectionStrings

    Public Shared Function CCGDataEntitiesConnectionString() As ConnectionStringSettings
        Try
            Dim myConfiguration As Configuration = WebConfigurationManager.OpenWebConfiguration("~/app.config")
            Dim myConnectionStringsSection As ConnectionStringsSection = myConfiguration.ConnectionStrings
            Dim myConnectionStringSettings As New ConnectionStringSettings

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
    Public Shared Function CCGDataConnectionString() As ConnectionStringSettings
        Try
            Dim myConfiguration As Configuration = WebConfigurationManager.OpenWebConfiguration("~/app.config")
            Dim myConnectionStringsSection As ConnectionStringsSection = myConfiguration.ConnectionStrings
            Dim myConnectionStringSettings As New ConnectionStringSettings


            'Dim myConfiguration = WebConfigurationManager.OpenWebConfiguration("~/Web.config")
            'Dim myConnectionStringsSection As ConnectionStringsSection = myConfiguration.ConnectionStrings
            'Dim myConnectionStringSettings As New ConnectionStringSettings


            Select Case My.Computer.Name
                Case "SC-BENYOMIN"
                    myConnectionStringSettings = myConnectionStringsSection.ConnectionStrings("CCGDataConnection")
                Case "NIASOFF-DESKTOP"
                    myConnectionStringSettings = myConnectionStringsSection.ConnectionStrings("CCGDataConnection2")
                Case "CCG-009"
                    myConnectionStringSettings = myConnectionStringsSection.ConnectionStrings("CCGDataConnection3")
                Case Else
                    myConnectionStringSettings = myConnectionStringsSection.ConnectionStrings("CCGDataConnection")
            End Select

            Return myConnectionStringSettings
      Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function

    Public Shared Function CCGDataEntitiesConnectionStringName() As String

        Try
            Select Case My.Computer.Name
                Case "SC-BENYOMIN"
                    Return "CCGDataEntities"
                Case "NIASOFF-DESKTOP"
                    Return "CCGDataEntities2"
                Case "CCG-009"
                    Return "CCGDataEntities3"
                Case Else
                    Return "CCGDataEntities"
            End Select

      Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Function




End Class


