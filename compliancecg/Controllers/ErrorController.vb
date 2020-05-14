Imports System.Web.Mvc

'Namespace Controllers
'    Public Class ErrorController
'        Inherits Controller

'        ' GET: Error
'        Function Index() As ActionResult
'            Return View()
'        End Function
'    End Class
'End Namespace


Public Class UserMvcController
    Inherits Controller

    Protected Overrides Sub OnException(ByVal filterContext As ExceptionContext)
        filterContext.ExceptionHandled = True
        '  _Logger.[Error](filterContext.Exception)
        filterContext.Result = RedirectToAction("Index", "ErrorHandler")
        filterContext.Result = New ViewResult With {.ViewName = "~/Views/ErrorHandler/Index.cshtml"}
    End Sub
End Class
