@imports Syncfusion.EJ2



<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - CCG Compliance</title>

    @Scripts.Render("~/bundles/jquery")

        @Html.DevExpress().GetStyleSheets(New StyleSheet With
                                                                            {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
                                                                               New StyleSheet With {.ExtensionSuite = ExtensionSuite.RichEdit},
                                                                               New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
                                                                                New StyleSheet With {.ExtensionSuite = ExtensionSuite.Scheduler},
                                                                                New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
                                                                                New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid},
                                                                                New StyleSheet With {.ExtensionSuite = ExtensionSuite.HtmlEditor},
                                                                                New StyleSheet With {.ExtensionSuite = ExtensionSuite.Chart},
                                                                                New StyleSheet With {.ExtensionSuite = ExtensionSuite.Report},
                                                                                New StyleSheet With {.ExtensionSuite = ExtensionSuite.TreeList})

        @Html.DevExpress().GetScripts(
                                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
                                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.RichEdit},
                                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.Editors},
                                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.Scheduler},
                                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.GridView},
                                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid},
                                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.HtmlEditor},
                                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.Editors},
                                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.Chart},
                                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.Report},
                                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.TreeList})



    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("~/bundles/bootstrap")

    @Styles.Render("~/Content/ej2/dist/material.css")
    @Scripts.Render("~/Content/ej2/dist/ej2.min.js")

</head>






<body>
    <div Class="navbar navbar-inverse navbar-fixed-top">
        <div Class="container">
            <div Class="navbar-header">
                <Button type="button" Class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <Span Class="icon-bar"></Span>
                    <Span Class="icon-bar"></Span>
                    <Span Class="icon-bar"></Span>
                </Button>
                @Html.ActionLink("Application name", "Index", "Home", New With {.area = ""}, New With {.class = "navbar-brand"})
            </div>
            <div Class="navbar-collapse collapse">
                <ul Class="nav navbar-nav">
                    <li>@Html.ActionLink("Home", "Index", "Home", New With {.area = ""}, Nothing)</li>
                    <li>@Html.ActionLink("API", "Index", "Help", New With {.area = ""}, Nothing)</li>
                </ul>
            </div>
        </div>
    </div>
    <div Class="container body-content">
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>
        </footer>
    </div>

    @*@Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")*@
    @Html.EJS().ScriptManager()
    @RenderSection("scripts", required:=False)


</body>
</html>
