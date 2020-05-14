Imports System.IO
Imports System.Web.Mvc

Namespace Controllers
    Public Class TrainingController
        Inherits Controller

        ' GET: Training
        Function Index(Folder As String) As ActionResult
            Dim viewModel As DocFiles = DisplayFilesForDownload(Folder)

            Return PartialView("Index", viewModel)
        End Function

        'Function AcknolwedgmentForms(File As String) As ActionResult
        '    Dim viewModel As DocFiles = DisplayFilesForDownload(File)

        '    Return PartialView("Resources", viewModel)
        'End Function

        Public Function DisplayFilesForDownload(Folder As String) As DocFiles
            Dim SubFolder As String = String.Empty
            Select Case Folder
                Case "training"
                    SubFolder = "Yearly Required Trainings"

            End Select

            Dim viewModel = New DocFiles With {.Path = Server.MapPath("../App_Data/Forms/") + SubFolder, .Files = New List(Of DocFile)()}
            Dim paths = Directory.GetFiles(viewModel.Path).ToList()
            ' Dim paths = Directory.GetFiles(viewModel.Path).ToList()


            For Each path In paths
                Dim fileInfo = New FileInfo(path)
                Dim File = New DocFile With {
                    .Path = path,
                    .Name = fileInfo.Name,
                    .Ext = fileInfo.Extension
                }
                viewModel.Files.Add(File)
            Next

            Return viewModel
        End Function

        Public Function Download(ByVal FilePath As String, ByVal FileName As String) As FileResult
            Return File(FilePath, System.Net.Mime.MediaTypeNames.Text.Xml, FileName)
            'Dim fileBytes As Byte() = System.IO.File.ReadAllBytes("c:\folder\myfile.ext")
            ''Dim fileName As String = "myfile.ext"
            'Return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName)
        End Function
        Public Function DownloadFiles(ByVal fileName As String) As FilePathResult
            Return New FilePathResult(String.Format("~\App_Data\Forms\Yearly Required Trainings\{0}", fileName & ".pdf"), "application/pdf")
        End Function

        ''C:\Users\bnias\source\repos\compliancecg\compliancecg\App_Data\Forms\Yearly Required Trainings\Basics of the Compliance Program Department Head Sign-in Sheet.pdf
    End Class




End Namespace
