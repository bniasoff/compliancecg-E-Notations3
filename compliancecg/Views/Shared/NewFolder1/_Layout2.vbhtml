<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - My ASP.NET Application</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")

    @Scripts.Render("~/bundles/bootstrap")
    <!-- Bootstrap CSS CDN -->
    @*<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" >*@
    <!-- Our Custom CSS -->

    @Styles.Render("~/Content/ej2/dist/material.css")
    @Scripts.Render("~/Content/ej2/dist/ej2.min.js")




    @Html.DevExpress().GetStyleSheets(
                                             New StyleSheet With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
                                             New StyleSheet With {.ExtensionSuite = ExtensionSuite.RichEdit},
                                             New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
                                             New StyleSheet With {.ExtensionSuite = ExtensionSuite.Scheduler},
                                             New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView}
                                         )
    @Html.DevExpress().GetScripts(
                                New Script With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
                                New Script With {.ExtensionSuite = ExtensionSuite.RichEdit},
                                New Script With {.ExtensionSuite = ExtensionSuite.Editors},
                                New Script With {.ExtensionSuite = ExtensionSuite.Scheduler},
                                New Script With {.ExtensionSuite = ExtensionSuite.GridView}
                            )


    @*<link href="~/Content/ej2/bootstrap.css" rel="stylesheet" />
        <script src="~/Content/ej2/dist/ej2.min.js"></script>*@

    @*<link rel="stylesheet" href="https://cdn.syncfusion.com/ej2/bootstrap.css" />
        <script src="https://cdn.syncfusion.com/ej2/dist/ej2.min.js"></script>*@


    @*<link href="https://kendo.cdn.telerik.com/2018.3.1017/styles/kendo.common-bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="https://kendo.cdn.telerik.com/2018.3.1017/styles/kendo.mobile.all.min.css" rel="stylesheet" type="text/css" />
        <link href="https://kendo.cdn.telerik.com/2018.3.1017/styles/kendo.bootstrap.min.css" rel="stylesheet" type="text/css" />
        <script src="https://kendo.cdn.telerik.com/2018.3.1017/js/jquery.min.js"></script>
        <script src="https://kendo.cdn.telerik.com/2018.3.1017/js/jszip.min.js"></script>
        <script src="https://kendo.cdn.telerik.com/2018.3.1017/js/kendo.all.min.js"></script>
        <script src="https://kendo.cdn.telerik.com/2018.3.1017/js/kendo.aspnetmvc.min.js"></script>
        <script src="@Url.Content("~/Scripts/kendo.modernizr.custom.js")"></script>*@


    @*<link href="~/Content/Site3.css" rel="stylesheet" />*@
    <!-- Font Awesome JS -->
    @*<script defer src="https://use.fontawesome.com/releases/v5.0.13/js/solid.js" integrity="sha384-tzzSw1/Vo+0N5UhStP3bvwWPq+uvzCMfrN1fEFe+xBmv1C/AtVX5K0uZtmcHitFZ" crossorigin="anonymous"></script>
        <script defer src="https://use.fontawesome.com/releases/v5.0.13/js/fontawesome.js" integrity="sha384-6OIrr52G08NpOFSZdxxz1xdNSndlD4vdcf/q2myIUVO0VsqaGHJsB0RaBE01VTOY" crossorigin="anonymous"></script>*@

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
                @Html.ActionLink("CCG", "Index", "Home", New With {.area = ""}, New With {.class = "navbar-brand"})
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

    @If Request.IsAuthenticated = True Then
        @<div id="wrapper">
            <div class="col-lg-12 col-sm-12 col-md-12">
                @Html.Partial("_LoginAuthenticated2")
                <div Class="main-content" id="main-text">
                    <div Class="sidebar-content" style="height:1000px;">

                        @*<div style="margin-top:20px;margin-left:20px;">
            @Html.Action("Index", "DevExpress")
        </div>*@

                        @Using (Html.BeginForm())
                            @<div style="margin-top:20px;margin-left:20px;">
                                <div id="PartialView"></div>
                            </div>
                        End Using

                        @RenderBody()
                    </div>
                                    </div>
                                </div>
                            </div>
                        End If



                        @If Request.IsAuthenticated = False Then
                            @<div id="page-content-wrapper">
                                <div class="container-fluid">
                                    @RenderBody()
                                </div>
                            </div>
                        End If





                            <!-- jQuery CDN - Slim version (=without AJAX) -->
                            <script src = "https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
                            <!-- Popper.JS -->
                            <script src = "https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.0/umd/popper.min.js" integrity="sha384-cs/chFZiN24E4KMATLdqdvsezGxaGsi4hLGOzlXwp5UZB1LY//20VyM2taTB4QvJ" crossorigin="anonymous"></script>
                            <!-- Bootstrap JS -->
                            <script src = "https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/js/bootstrap.min.js" integrity="sha384-uefMccjFJAIv6A+rW+L4AHf99KvxDjWSu1z9VI8SKNVmz4sk7buKt/6v9KI65qnm" crossorigin="anonymous"></script>



                            <script type = "text/javascript" >
                                $(document).ready(function () {

                                       //var jqxhr = $.post("/DevExpress/index", function (data) {
                                       // $("#PartialView").html(data);
                                       //})


                                    $('.dropdown-toggle').dropdown()

                                    $('#sidebarCollapse').on('click', function () {
                                        $('#sidebar').toggleClass('active');
                                        $(this).toggleClass('active');
                                    });
                                });
                            </script>

                        @Scripts.Render("~/bundles/jquery")
                        @*@Scripts.Render("~/bundles/bootstrap")*@
                        @RenderSection("scripts", required:=False)
                        @Html.EJS().ScriptManager()

                            <script type = "text/javascript" >
                                MVCxClientGlobalEvents.AddControlsInitializedEventHandler(onControlsInitialized);
                                ASPxClientControl.GetControlCollection().BrowserWindowResized.AddHandler(onBrowserWindowResized);
                            </script>
</body>
</html>
