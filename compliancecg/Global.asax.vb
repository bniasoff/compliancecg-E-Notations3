Imports System.Security.Claims
Imports System.Web.Helpers
Imports System.Web.Http
Imports System.Web.Optimization
Imports CCGData.CCGData
Imports compliancecg.Controllers
Imports Newtonsoft.Json
Imports NLog

Public Class MvcApplication
    Inherits System.Web.HttpApplication
    'Private Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Protected Sub Application_Start()
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTg0NDI3QDMxMzcyZTM0MmUzMEdlN3NqODNMUStGZTIwTWs0THlTeUJlWEd0RlNZaUw3Q3cwUXNoSFhOc0E9")

        '        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTY5MjQyQDMxMzcyZTMzMmUzMFdVbWd0ZmYvN0p4NW9zYjZkWTU1SjZlM3BGaHFkR3lONkQxYkJNb2FJV289")

        'Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTY5MjgwQDMxMzcyZTMzMmUzMEV2WldVd09URi9DVjlVU2dIRFJ0Q0NMSGpXKzB6cEZxUmFMRjRkNW1Zdmc9")


        AreaRegistration.RegisterAllAreas()
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)

        'Dim config As HttpConfiguration = GlobalConfiguration.Configuration
        'config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore

        'Dim json = GlobalConfiguration.Configuration.Formatters.JsonFormatter
        'json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All

        '  Dim JsonImageModel = Newtonsoft.Json.JsonConvert.SerializeObject(Images, New JsonSerializerSettings With {.ReferenceLoopHandling = ReferenceLoopHandling.Ignore})


        ' AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Email

        'ModelBinders.Binders.DefaultBinder = New DevExpress.Web.Mvc.DevExpressEditorsBinder()
        'AddHandler DevExpress.Web.ASPxWebControl.CallbackError, AddressOf Application_Error
    End Sub




    Protected Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Dim exception As Exception = System.Web.HttpContext.Current.Server.GetLastError()
        '  _Logger.[Error](ex)
        'TODO: Handle Exception
    End Sub


    Private Sub Session_Start(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Session("FacilityUser") Is Nothing Then
                Dim FacilityUser As User = HomeController.GetCurrentUser()
            End If
        Catch ex As Exception
            'logger.Error(ex)

        End Try
    End Sub
End Class
