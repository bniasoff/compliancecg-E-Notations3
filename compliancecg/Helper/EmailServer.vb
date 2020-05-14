
Imports System.Net.Mail
Imports System.Threading
Imports System.ComponentModel
Imports System.IO
Imports System.Threading.Tasks
Imports System.ServiceModel
Imports CCGData.CCGData
Imports CCGData.Enums
Imports NLog

Public Class EmailServer
    Public Event SendCompleted As SendCompletedEventHandler
    Private Shared waitHandle As New AutoResetEvent(False)
    Private Shared EmailCompletionNotifier As New AutoResetEvent(False)
    Private Shared mailSent As Boolean = False

    Private Shared EmailRemaining As Integer = 0
    Private Shared ServiceType2 As EmailType
    Public Shared IDs As New List(Of Integer)
    Private _EmailMessages As New List(Of EmailMessage)
    Private _EmailMessage As EmailMessage
    Private Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()


    Public Property EmailMessages() As List(Of EmailMessage)
        Get
            Return _EmailMessages
        End Get
        Set(ByVal value As List(Of EmailMessage))
            Me.EmailMessages.Clear()
            _EmailMessages = value
        End Set
    End Property

    Public Property EmailMessage() As EmailMessage
        Get
            Return _EmailMessage
        End Get
        Set(ByVal value As EmailMessage)
            _EmailMessage = value
        End Set
    End Property


    Public Async Function EmailSendAsync() As Task(Of Boolean)

        Try
            EmailRemaining = EmailMessages.Count
            Dim Client As New SmtpClient
            Client = EmailClient()
            AddHandler Client.SendCompleted, AddressOf SendCompletedCallback

            For Each EmailMessage As EmailMessage In EmailMessages
                If EmailAddressChecker(EmailMessage.ToEmail) Then

                    Dim MailMessage = EmailClientMessage(EmailMessage)
                    Dim userState As EmailMessage = EmailMessage
                    Me.EmailMessage = EmailMessage

                    Try
                        Await Client.SendMailAsync(MailMessage)
                        ' Client.Send(MailMessage)
                        MailMessage.Dispose()
                    Catch smtpEx As SmtpException
                        Dim FaultError As New FaultError
                        'If Employee.Employee_ID > 0 Then FaultError.ID = Employee.Employee_ID
                        FaultError.Details = smtpEx.Message
                        FaultError.Issue = smtpEx.Source
                        Throw New FaultException(Of FaultError)(FaultError)
                    End Try
                End If
            Next
            EmailCompletionNotifier.WaitOne()

        Catch ex As Exception
            logger.Error(ex)
            'Log.LogException(ex, 0, 0)

            Dim FaultError As New FaultError
            'If Employee.Employee_ID > 0 Then FaultError.ID = Employee.Employee_ID
            FaultError.Details = ex.Message
            FaultError.Issue = ex.Source
            Throw New FaultException(Of FaultError)(FaultError)
        End Try
    End Function


    Private Sub SendCompletedCallback(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs)
        Dim Client As SmtpClient = sender
        Try

            mailSent = False
            If e.Cancelled Then
            End If
            If e.Error IsNot Nothing Then
                Dim EmailMessage As EmailMessage = TryCast(Me.EmailMessage, EmailMessage)
                '  EmailMessage.Sent = False
                ' EmailMessage.ErrorMessage = e.Error.Message
                UpdateRecords2(EmailMessage, ServiceType2)
            Else
                Console.WriteLine(EmailRemaining & ". Message sent.")
                mailSent = True
                Dim EmailMessage As EmailMessage = TryCast(Me.EmailMessage, EmailMessage)
                '   EmailMessage.Sent = True
                UpdateRecords2(EmailMessage, ServiceType2)
            End If

            Dim numRemainingLock As New Object()
            SyncLock numRemainingLock
                If System.Threading.Interlocked.Decrement(EmailRemaining) = 0 Then
                    EmailCompletionNotifier.Set()
                End If
            End SyncLock
        Catch ex As Exception
            logger.Error(ex)
            'Log.LogException(ex, 0, 0)
            '  Console.WriteLine(ex.Message)
            Dim FaultError As New FaultError
            'If Employee.Employee_ID > 0 Then FaultError.ID = Employee.Employee_ID
            FaultError.Details = ex.Message
            FaultError.Issue = ex.Source
            Throw New FaultException(Of FaultError)(FaultError)
        Finally
            ' Client.Dispose()
        End Try
    End Sub


    Private Function EmailClient() As SmtpClient

        'Dim client As New SmtpClient("smtp.gmail.com", 587) 'gmail
        'client.EnableSsl = True
        'client.UseDefaultCredentials = False
        'client.DeliveryMethod = SmtpDeliveryMethod.Network
        'client.Credentials = New System.Net.NetworkCredential("bniasoff@gmail.com", "Lakewood13")  'gmail

        'Dim client As New SmtpClient("smtp.live.com", "587") ' Hotmail
        'client.EnableSsl = True
        'client.UseDefaultCredentials = False
        'client.DeliveryMethod = SmtpDeliveryMethod.Network
        'client.Credentials = New System.Net.NetworkCredential("bniasoff@hotmail.com", "Lakewood18")

        Dim client As New SmtpClient("smtp-auth.no-ip.com", "3325") ' Hotmail
        client.EnableSsl = True
        client.UseDefaultCredentials = False
        client.DeliveryMethod = SmtpDeliveryMethod.Network
        client.Credentials = New System.Net.NetworkCredential("compliancecg.com@noip-smtp", "CCG123")

        Return client
    End Function

    Private Function EmailClientMessage(EmailMessage As EmailMessage) As MailMessage
        Dim message As New MailMessage()
        Try

            Select Case EmailMessage.EmailType
                Case EmailType.LoginReset
            End Select

            'If EmailMessage.CC.Count > 0 Then
            '    For Each Address As String In EmailMessage.CC
            '        message.CC.Add(Address)
            '    Next
            'End If


            message.IsBodyHtml = True
            message.Subject = EmailMessage.Subject
            message.Body = EmailMessage.Message


            If EmailMessage.FromEmail <> "" Then message.[From] = New MailAddress(EmailMessage.FromEmail)
            If EmailMessage.ToEmail <> "" Then message.[To].Add(New MailAddress(EmailMessage.ToEmail))


            If EmailMessage.Attachments IsNot Nothing Then
                If EmailMessage.Attachments.Count > 0 Then
                    For Each Attachment As String In EmailMessage.Attachments
                        Dim fi As FileInfo = New FileInfo(Attachment)

                        If fi.Exists Then
                            Dim newAttachment = New System.Net.Mail.Attachment(Attachment)
                            message.Attachments.Add(newAttachment)
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            logger.Error(ex)
            'Log.LogException(ex, 0, 0)
            Dim FaultError As New FaultError
            'If Employee.Employee_ID > 0 Then FaultError.ID = Employee.Employee_ID
            FaultError.Details = ex.Message
            FaultError.Issue = ex.Source
            Throw New FaultException(Of FaultError)(FaultError)
        End Try

        Return message
    End Function

    Public Shared Function UpdateRecords2(EmailMessage As EmailMessage, ServiceType As EmailType)
        Try

            'Dim NewEmail = New Email
            'With NewEmail
            '    .EmailAddress = EmailMessage.EmailAddress
            '    .EmailBody = EmailMessage.EmailMessage
            '    .EmailSent = EmailMessage.Sent
            '    .EmailType = ServiceType
            '    .EmailTo = EmailMessage.EmailTo
            '    .ErrorMessage = EmailMessage?.ErrorMessage
            '    .EmailDate = Now
            '    .DateOfEntry = Now
            'End With



        Catch ex As Exception
            logger.Error(ex)
            'Log.LogException(ex, 0, 0)
            Dim FaultError As New FaultError
            'If Employee.Employee_ID > 0 Then FaultError.ID = Employee.Employee_ID
            FaultError.Details = ex.Message
            FaultError.Issue = ex.Source
            Throw New FaultException(Of FaultError)(FaultError)
        End Try
    End Function

    Shared Function EmailAddressChecker(ByVal emailAddress As String) As Boolean
        Dim regExPattern As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, regExPattern)
        If emailAddressMatch.Success Then
            Return True
        Else
            Return False
        End If
    End Function


    'Public Function EmailSend(ServiceType As EmailType)
    '    Task.Factory.StartNew(Function() EmailSendAsync(ServiceType))
    'End Function
    ''Public Async Function EmailSendAsync(ServiceType As EmailType) As Task(Of Boolean)

    '    Try
    '        EmailRemaining = EmailMessages.Count
    '        Dim Client As New SmtpClient
    '        Client = EmailClient()
    '        AddHandler Client.SendCompleted, AddressOf SendCompletedCallback

    '        For Each EmailMessage As EmailMessage In EmailMessages
    '            If EmailAddressChecker(EmailMessage.ToEmail) Then
    '                ServiceType2 = ServiceType


    '                Dim MailMessage = EmailClientMessage(EmailMessage, ServiceType)
    '                Dim userState As EmailMessage = EmailMessage
    '                Me.EmailMessage = EmailMessage

    '                Try
    '                    Await Client.SendMailAsync(MailMessage)
    '                    ' Client.Send(MailMessage)
    '                    MailMessage.Dispose()
    '                Catch smtpEx As SmtpException
    '                    Dim FaultError As New FaultError
    '                    'If Employee.Employee_ID > 0 Then FaultError.ID = Employee.Employee_ID
    '                    FaultError.Details = smtpEx.Message
    '                    FaultError.Issue = smtpEx.Source
    '                    Throw New FaultException(Of FaultError)(FaultError)
    '                End Try
    '            End If
    '        Next
    '        EmailCompletionNotifier.WaitOne()

    '  Catch ex As Exception
    'logger.Error(ex)
    '        'Log.LogException(ex, 0, 0)

    '        Dim FaultError As New FaultError
    '        'If Employee.Employee_ID > 0 Then FaultError.ID = Employee.Employee_ID
    '        FaultError.Details = ex.Message
    '        FaultError.Issue = ex.Source
    '        Throw New FaultException(Of FaultError)(FaultError)
    '    End Try
    'End Function



End Class



