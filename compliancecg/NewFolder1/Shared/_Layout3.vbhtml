<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - My ASP.NET Application</title>
    @Styles.Render("~/Content/css")
    @*@Scripts.Render("~/bundles/modernizr")*@


    @Styles.Render("~/Content/ej2/bootstrap.css")
    @Scripts.Render("~/Scripts/ej2/ej2.min.js")

    @*<link rel="stylesheet" href="https://cdn.syncfusion.com/ej2/material.css" />
    <script src="https://cdn.syncfusion.com/ej2/dist/ej2.min.js"></script>


    <script src="https://kendo.cdn.telerik.com/2018.3.1017/js/jquery.min.js"></script>
    <script src="https://kendo.cdn.telerik.com/2018.3.1017/js/jszip.min.js"></script>*@

</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                @Html.ActionLink("Application name", "Index", "Home", New With {.area = ""}, New With {.class = "navbar-brand"})
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li>@Html.ActionLink("Home", "Index", "Home")</li>
                    <li>@Html.ActionLink("About", "About", "Home")</li>
                    <li>@Html.ActionLink("Contact", "Contact", "Home")</li>
                </ul>
                @Html.Partial("_LoginPartial")
            </div>
        </div>
    </div>
    <div class="row" style="margin-top:20px;">
        @If Request.IsAuthenticated Then
            @Html.Partial("_LoginAuthenticated")
            @<div>
                @RenderBody()
            </div>
        End If

        @If Request.IsAuthenticated = False Then
            @<div Class="container body-content">
                @RenderBody()
            </div>
        End If
    </div>
    @*<div class="container body-content">
            @RenderBody()
            <hr />
            <footer>
                <p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>
            </footer>
        </div>*@

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")

    @Html.EJS().ScriptManager()
    @RenderSection("scripts", required:=False)
</body>
</html>
