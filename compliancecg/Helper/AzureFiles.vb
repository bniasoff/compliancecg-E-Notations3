Imports System.IO
Imports Microsoft.WindowsAzure.Storage
Imports Microsoft.WindowsAzure.Storage.Blob
Imports Microsoft.WindowsAzure.Storage.File
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Word
Imports Syncfusion.DocIO.DLS
Imports Syncfusion.DocIO
Imports Microsoft.WindowsAzure.Storage.Auth

Public Class AzureFiles


    'Storage account name: compliancecgstorage
    'Connection string: DefaultEndpointsProtocol=https;AccountName=compliancecgstorage;AccountKey=zfk98WNZMyvS+JBEKtxz2V+oh2bizgGvPlxZ/6SaES3hPi8Ee8V3JuKAuI9g7+33ThfXlNT0lGbweLCrXhoLDA==;EndpointSuffix=core.windows.net
    'Shared Name :  https://compliancecgstorage.blob.core.windows.net/policies

    Private Shared ConnectionString As String = "DefaultEndpointsProtocol=https;AccountName=compliancecgstorage;AccountKey=zfk98WNZMyvS+JBEKtxz2V+oh2bizgGvPlxZ/6SaES3hPi8Ee8V3JuKAuI9g7+33ThfXlNT0lGbweLCrXhoLDA==;EndpointSuffix=core.windows.net"


    '<Route("api/blobs/upload")>
    'Public Async Function UploadFile(ByVal path As String) As Threading.Tasks.Task(Of Task)
    '    Dim filePathOnServer = path.Combine(HostingEnvironment.MapPath(UPLOAD_PATH), path)

    '    Using fileStream = File.OpenRead(filePathOnServer)
    '        Dim filename = path.GetFileName(path)
    '        Dim blockBlob = _container.GetBlockBlobReference(filename)
    '        Await blockBlob.UploadFromStreamAsync(fileStream)
    '    End Using
    'End Function

    Public Async Function UploadFileToBlobContainer(ByVal AzureContainerName As String, FileToUpload As String) As Threading.Tasks.Task(Of Boolean)
        Try
            Dim file_extension, filename_withExtension, storageAccount_connectionString As String
            Dim file As Stream
            storageAccount_connectionString = ConnectionString
            file = New FileStream(FileToUpload, FileMode.Open)

            Dim mycloudStorageAccount As CloudStorageAccount = CloudStorageAccount.Parse(storageAccount_connectionString)
            Dim blobClient As CloudBlobClient = mycloudStorageAccount.CreateCloudBlobClient()
            Dim container As CloudBlobContainer = blobClient.GetContainerReference(AzureContainerName)

            If container.CreateIfNotExists() Then
                Await container.SetPermissionsAsync(New BlobContainerPermissions With {.PublicAccess = BlobContainerPublicAccessType.Blob})
            End If

            file_extension = Path.GetExtension(FileToUpload)
            filename_withExtension = Path.GetFileName(FileToUpload)
            Dim cloudBlockBlob As CloudBlockBlob = container.GetBlockBlobReference(filename_withExtension)
            cloudBlockBlob.Properties.ContentType = file_extension

            'Throw New System.Exception("An exception has occurred.")

            Await cloudBlockBlob.UploadFromStreamAsync(file)
            Return True

        Catch ex As Exception

        End Try
    End Function

    Public Async Function UploadFileToBlobContainer2(ByVal ContainerName As String, ByVal FileName As String) As Threading.Tasks.Task(Of WordDocument)
        Try
            Const accountName As String = "compliancecgstorage"
            Dim key As String = ConnectionString
            Dim storageAccount = New CloudStorageAccount(New StorageCredentials(accountName, key), True)
            Dim blobClient = storageAccount.CreateCloudBlobClient()
            Dim container = blobClient.GetContainerReference("policies")
            Await container.CreateIfNotExistsAsync()
            Await container.SetPermissionsAsync(New BlobContainerPermissions() With {.PublicAccess = BlobContainerPublicAccessType.Blob})
            'Dim blob = container.GetBlockBlobReference("test.jpg")

            'Using stream = System.IO.File.OpenReadStream()
            '    Await blob.UploadFromStreamAsync(stream)
            'End Using
        Catch ex As Exception

        End Try
    End Function

    Public Function GetBlobWordFile(ByVal ContainerName As String, ByVal FileName As String) As WordDocument
        Try
            'Dim connectionString As String = connectionString
            Dim StorageAccount As CloudStorageAccount = CloudStorageAccount.Parse(ConnectionString)
            Dim ServiceClient As CloudBlobClient = StorageAccount.CreateCloudBlobClient()
            Dim Container As CloudBlobContainer = ServiceClient.GetContainerReference($"{ContainerName}")
            Dim Blob As CloudBlockBlob = Container.GetBlockBlobReference($"{FileName}")
            ' Dim contents As String = blob.DownloadTextAsync().Result

            Dim BlobMem As MemoryStream = New MemoryStream()
            Blob.DownloadToStream(BlobMem)
            Dim BlobFile = ReadWordDocment(BlobMem)

            Return BlobFile
        Catch ex As Exception

        End Try
    End Function


    Public Function GetBlobFile(FileName As String)
        Try
            Dim Blob As New AzureFiles
            Dim WordDocument As New Syncfusion.DocIO.DLS.WordDocument

            Dim Extension = FileName.Split(".")

            If Extension.Length > 1 Then
                Select Case Extension.Last
                    Case "docx"
                        WordDocument = Blob.GetBlobWordFile("policies", FileName)
                    Case = "Pdf"

                End Select
            End If
        Catch ex As Exception
        End Try
    End Function

    Public Async Function UploadBlobFile(BlobContainer As String, FileName As String) As Threading.Tasks.Task(Of Boolean)
        Try
            Dim Uploaded = Await UploadFileToBlobContainer(BlobContainer, FileName)
            Return Uploaded
        Catch ex As Exception

        End Try
    End Function



    'Public Function ReadDocment(stream As MemoryStream) As Byte()
    '    Try
    '        Dim Document As New Syncfusion.DocIO.DLS.WordDocument
    '        Document.Open(stream, FormatType.Docx)
    '        Document.Save("C:\CCG\Temp.docx", FormatType.Docx)

    '        Return stream.ToArray
    '    Catch ex As Exception
    '    End Try
    'End Function

    Public Function ReadWordDocment(stream As MemoryStream) As Syncfusion.DocIO.DLS.WordDocument
        Try
            Dim Document As New Syncfusion.DocIO.DLS.WordDocument
            Document.Open(stream, FormatType.Docx)
            'Document.Save("C:\CCG\Temp.docx", FormatType.Docx)
            Return Document
        Catch ex As Exception
        End Try
    End Function
    Public Function ReadWordDocment2(stream As MemoryStream) As Word.Application
        Try
            Dim Document As New Word.Application
            Document.Open(stream, FormatType.Docx)
            'Document.Save("C:\CCG\Temp.docx", FormatType.Docx)
            Return Document
        Catch ex As Exception
        End Try
    End Function

End Class
