
Imports System.Net
Imports System.Xml
Imports NLog

Public Class IPHostGenerator
    Private Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Friend Function GetCurrentPageUrl() As String
        Return HttpContext.Current.Request.Url.AbsoluteUri
    End Function
    Friend Function GetVisitorDetails() As String
        Dim IPAddress__1 As String = String.Empty
        Dim IpAddress__2 As String = String.Empty
        Dim VisitorCountry As String = String.Empty

        IpAddress__2 = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If String.IsNullOrEmpty(IpAddress__2) Then
            If HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR") IsNot Nothing Then
                IpAddress__2 = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            End If
        End If

        'IPAddress = (System.Web.UI.Page)Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        If IPAddress__1 = "" OrElse IPAddress__1 Is Nothing Then
            If HttpContext.Current.Request.ServerVariables("REMOTE_ADDR") IsNot Nothing Then
                IpAddress__2 = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
            End If
        End If
        'IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        Return IpAddress__2
    End Function

    Friend Function GetLocation(IPAddress As String) As DataTable
        Dim WebRequest As WebRequest = WebRequest.Create(Convert.ToString("http://freegeoip.net/xml/") & IPAddress)
        Dim px As New WebProxy(Convert.ToString("http://freegeoip.net/xml/") & IPAddress, True)

        WebRequest.Proxy = px
        WebRequest.Timeout = 2000

        Try
            Dim rep As WebResponse = WebRequest.GetResponse()
            Dim xtr As New XmlTextReader(rep.GetResponseStream())
            Dim ds As New DataSet()
            ds.ReadXml(xtr)
            Return ds.Tables(0)
        Catch
            Return Nothing
        End Try
    End Function

    Friend Function GetMachineNameUsingIPAddress(IpAdress As String) As String
        Dim machineName As String = String.Empty
        Try
            Dim hostEntry As IPHostEntry = Dns.GetHostEntry(IpAdress)

            machineName = hostEntry.HostName
            ' Machine not found...
        Catch ex As Exception
            Logger.Error(ex)
        End Try
        Return machineName
    End Function
End Class