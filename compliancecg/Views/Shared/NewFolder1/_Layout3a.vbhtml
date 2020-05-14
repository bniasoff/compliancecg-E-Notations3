<head>
    @Scripts.Render("~/bundles/jquery")

    @Html.DevExpress().GetStyleSheets(
                                                                                                                                                                                      New StyleSheet With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
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

    @*<link href="~/Scripts/Bootstrap/bootsnav.css" rel="stylesheet" />*@
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

                <a href="@Url.Action("Index", "Home")" title="CCG Compliance" class="">
                    <img alt="CCG Compliance" src="~/Content/Images/Logo G White-01.png" width="50" height="50" />
                    CCG Compliance
                </a>



            </div>
            <div class="navbar-collapse collapse">
                @Html.Partial("_LoginPartial2")

            </div>
            @*<a href="javascript:SetActive('facilities')" data-hover="Search">Facilities <span data-hover="facilities"></span></a>*@
        </div>
    </div>

    @*@If Request.IsAuthenticated = True Then
            @<div id="wrapper">
                <div class="col-lg-12 col-sm-12 col-md-12">
                    @Html.Partial("_LoginAuthenticated2")
                    <div Class="main-content" id="main-text">
                        <div Class="sidebar-content" style="height:1000px;">
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
        End If*@



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


    @RenderSection("scripts", required:=False)
    @Html.EJS().ScriptManager()

    <script type="text/javascript">
        MVCxClientGlobalEvents.AddControlsInitializedEventHandler(onControlsInitialized);
        ASPxClientControl.GetControlCollection().BrowserWindowResized.AddHandler(onBrowserWindowResized);
    </script>


</body>
