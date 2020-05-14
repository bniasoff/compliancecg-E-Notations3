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
                    <li id="facilities"> <a href="javascript:SetActive('facilities')" data-hover="Search">Facilities <span data-hover="Facilities"></span></a></li>
                    <li id="policies"><a href="javascript:SetActive('policies')" data-hover="policies">Policies <span data-hover="policies"></span></a></li>

                    <li>@Html.ActionLink("About", "About", "Home")</li>
                    <li>@Html.ActionLink("Contact", "Contact", "Home")</li>
                </ul>
            </div>
        </div>
    </div>


    <div class="container body-content">
        <div style="margin-top:20px;margin-left:10px;">
            <div id="PartialView"></div>
        </div>

        @RenderBody()
        @*<hr />
            <footer>
                <p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>
            </footer>*@
    </div>

    @*@Scripts.Render("~/bundles/jquery")*@

    @RenderSection("scripts", required:=False)
    @Html.EJS().ScriptManager()

    <script type="text/javascript">
        MVCxClientGlobalEvents.AddControlsInitializedEventHandler(onControlsInitialized);
        ASPxClientControl.GetControlCollection().BrowserWindowResized.AddHandler(onBrowserWindowResized);
    </script>

</body>
</html>


<script>
    function SetActive(arg) {
        if (arg == 'facilities') {
            //var ajax = new ej.base.Ajax("/Home/Index3", 'POST', true);
            //ajax.send().then((data) => {
            //    $("#PartialView").html(data);
            //});
        };

        if (arg == 'policies') {
            var ajax = new ej.base.Ajax("/Policies/TreeView", 'POST', true);
            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });
        };



        //if (arg == 'policies') {
        //    $.get("/Policies/RichEditPartial", function (data) {
        //        $("#PartialView").html(data);
        //    });



        //    //var ajax = new ej.base.Ajax("/Policies/RichEditPartial", 'POST', true);
        //    //ajax.send().then((data) => {
        //    //    $("#PartialView").html(data);
        //    //});
        //};

    }
</script>

