Imports System
Imports System.Configuration
Imports System.Web
Imports System.Web.Http
Imports System.Web.Http.Controllers

Namespace Miscellaneous.Attributes.Controller
    Public Class FilterIPAttribute
        Inherits AuthorizeAttribute

        Public Property AllowedSingleIPs As String
        Public Property AllowedMaskedIPs As String
        Public Property ConfigurationKeyAllowedSingleIPs As String
        Public Property ConfigurationKeyAllowedMaskedIPs As String
        Private allowedIPListToCheck As IPList = New IPList()
        Public Property DeniedSingleIPs As String
        Public Property DeniedMaskedIPs As String
        Public Property ConfigurationKeyDeniedSingleIPs As String
        Public Property ConfigurationKeyDeniedMaskedIPs As String
        Private deniedIPListToCheck As IPList = New IPList()

        Private Function AuthorizeCore(ByVal httpContext As HttpContextBase) As Boolean
            If httpContext Is Nothing Then Throw New ArgumentNullException("httpContext")
            Dim userIpAddress As String = httpContext.Request.UserHostAddress

            Try
                Dim ipAllowed As Boolean = CheckAllowedIPs(userIpAddress)
                Dim ipDenied As Boolean = CheckDeniedIPs(userIpAddress)
                Dim finallyAllowed As Boolean = ipAllowed AndAlso Not ipDenied
                Return finallyAllowed
            Catch e As Exception
            End Try

            Return True
        End Function

        Private Function CheckAllowedIPs(ByVal userIpAddress As String) As Boolean
            If Not String.IsNullOrEmpty(AllowedSingleIPs) Then
                SplitAndAddSingleIPs(AllowedSingleIPs, allowedIPListToCheck)
            End If

            If Not String.IsNullOrEmpty(AllowedMaskedIPs) Then
                SplitAndAddMaskedIPs(AllowedMaskedIPs, allowedIPListToCheck)
            End If

            If Not String.IsNullOrEmpty(ConfigurationKeyAllowedSingleIPs) Then
                Dim configurationAllowedAdminSingleIPs As String = ConfigurationManager.AppSettings(ConfigurationKeyAllowedSingleIPs)

                If Not String.IsNullOrEmpty(configurationAllowedAdminSingleIPs) Then
                    SplitAndAddSingleIPs(configurationAllowedAdminSingleIPs, allowedIPListToCheck)
                End If
            End If

            If Not String.IsNullOrEmpty(ConfigurationKeyAllowedMaskedIPs) Then
                Dim configurationAllowedAdminMaskedIPs As String = ConfigurationManager.AppSettings(ConfigurationKeyAllowedMaskedIPs)

                If Not String.IsNullOrEmpty(configurationAllowedAdminMaskedIPs) Then
                    SplitAndAddMaskedIPs(configurationAllowedAdminMaskedIPs, allowedIPListToCheck)
                End If
            End If

            Return allowedIPListToCheck.CheckNumber(userIpAddress)
        End Function

        Private Function CheckDeniedIPs(ByVal userIpAddress As String) As Boolean
            If Not String.IsNullOrEmpty(DeniedSingleIPs) Then
                SplitAndAddSingleIPs(DeniedSingleIPs, deniedIPListToCheck)
            End If

            If Not String.IsNullOrEmpty(DeniedMaskedIPs) Then
                SplitAndAddMaskedIPs(DeniedMaskedIPs, deniedIPListToCheck)
            End If

            If Not String.IsNullOrEmpty(ConfigurationKeyDeniedSingleIPs) Then
                Dim configurationDeniedAdminSingleIPs As String = ConfigurationManager.AppSettings(ConfigurationKeyDeniedSingleIPs)

                If Not String.IsNullOrEmpty(configurationDeniedAdminSingleIPs) Then
                    SplitAndAddSingleIPs(configurationDeniedAdminSingleIPs, deniedIPListToCheck)
                End If
            End If

            If Not String.IsNullOrEmpty(ConfigurationKeyDeniedMaskedIPs) Then
                Dim configurationDeniedAdminMaskedIPs As String = ConfigurationManager.AppSettings(ConfigurationKeyDeniedMaskedIPs)

                If Not String.IsNullOrEmpty(configurationDeniedAdminMaskedIPs) Then
                    SplitAndAddMaskedIPs(configurationDeniedAdminMaskedIPs, deniedIPListToCheck)
                End If
            End If

            Return deniedIPListToCheck.CheckNumber(userIpAddress)
        End Function

        Private Sub SplitAndAddSingleIPs(ByVal ips As String, ByVal list As IPList)
            Dim splitSingleIPs = ips.Split(","c)

            For Each ip As String In splitSingleIPs
                list.Add(ip)
            Next
        End Sub

        Private Sub SplitAndAddMaskedIPs(ByVal ips As String, ByVal list As IPList)
            Dim splitMaskedIPs = ips.Split(","c)

            For Each maskedIp As String In splitMaskedIPs
                Dim ipAndMask = maskedIp.Split(";"c)
                list.Add(ipAndMask(0), ipAndMask(1))
            Next
        End Sub

        Public Overrides Sub OnAuthorization(ByVal actionContext As HttpActionContext)
            If AuthorizeCore(CType(actionContext.Request.Properties("MS_HttpContext"), HttpContextBase)) Then Return
            MyBase.HandleUnauthorizedRequest(actionContext)
        End Sub
    End Class
End Namespace

Public Class IPList
    Private ipRangeList As ArrayList = New ArrayList()
    Private maskList As SortedList = New SortedList()
    Private usedList As ArrayList = New ArrayList()

    Public Sub New()
    End Sub

    Private Function parseIP(ByVal IPNumber As String) As UInteger
    Public Sub Add(ByVal ipNumber As String)
    Public Sub Add(ByVal ip As UInteger)
    Public Sub Add(ByVal ipNumber As String, ByVal mask As String)
    Public Sub Add(ByVal ip As UInteger, ByVal umask As UInteger)
    Public Sub Add(ByVal ipNumber As String, ByVal maskLevel As Integer)
    Public Sub AddRange(ByVal fromIP As String, ByVal toIP As String)
    Public Sub AddRange(ByVal fromIP As UInteger, ByVal toIP As UInteger)
    Public Function CheckNumber(ByVal ipNumber As String) As Boolean
    Public Function CheckNumber(ByVal ip As UInteger) As Boolean
    Public Sub Clear()
    Public Overrides Function ToString() As String
End Class

