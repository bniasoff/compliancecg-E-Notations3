<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - My ASP.NET Application</title>


    @Scripts.Render("~/bundles/jquery")

    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("~/bundles/bootstrap")

    @Styles.Render("~/Content/ej2/dist/material.css")
    @Scripts.Render("~/Content/ej2/dist/ej2.min.js")




    <script src="~/Scripts/ej2/ej2.min.js"></script>
    <link rel="stylesheet" href="https://cdn.syncfusion.com/ej2/material.css" />
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
            </div>
        </div>
    </div>
    <div class="container body-content">

        @If Request.IsAuthenticated = False Then
            @<div id="page-content-wrapper">
                <div class="container-fluid">
                    @Using (Html.BeginForm())
                        @<div style="margin-top:20px;margin-left:20px;z-index:1;">
                            <div id="PartialView"></div>
                        </div>
                    End Using
                    @RenderBody()
                </div>
            </div>
        End If



        <hr />
        <footer>
            <p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>
        </footer>
    </div>


    @RenderSection("scripts", required:=False)
    @Html.EJS().ScriptManager()



</body>
</html>
