Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports DevExpress.Web.Mvc




Namespace Controllers
    Public Class DocumentManagerController
        Inherits Controller

        Function Index() As ActionResult
            Return PartialView()
        End Function
        Public Function FileManagerPartial() As ActionResult
            Return PartialView("_FileManagerPartial", HomeControllerFileManagerSettings.Model)
        End Function

        Public Function FileManagerPartialDownload() As FileStreamResult
            '   Return FileManagerExtension.DownloadFiles("FileManager", HomeControllerFileManagerSettings.Model)
            Return FileManagerExtension.DownloadFiles(HomeControllerFileManagerSettings.CreateFileManagerDownloadSettings(), "C:\Projects\Policies\")
        End Function
    End Class


    Public Class HomeControllerFileManagerSettings
        Public Const RootFolder As String = "~\"

        Public Shared ReadOnly Property Model() As String
            Get
                Return "C:\Projects\Policies\"
                ' Return RootFolder
            End Get
        End Property

        Public Shared Function CreateFileManagerDownloadSettings() As FileManagerSettings
            Dim settings = New FileManagerSettings()
            settings.SettingsEditing.AllowDownload = True
            settings.Name = "FileManager"
            Return settings
        End Function

    End Class



End Namespace