﻿Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http
Imports compliancecg.Controllers

Public Module WebApiConfig
    Public Sub Register(ByVal config As HttpConfiguration)
        ' Web API configuration and services

        ' Web API routes
        config.MapHttpAttributeRoutes()

        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )
        config.MessageHandlers.Add(New IPFilterHandler())
    End Sub
End Module



