@Imports Microsoft.AspNet.Identity
@imports  System.Security.Claims

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

    @*@Styles.Render("~/Content/ej2/dist/material.css")
    @Scripts.Render("~/Content/ej2/dist/ej2.min.js")*@


    <link href="~/Content/ej/web/bootstrap-theme/ej.web.all.min.css" rel="stylesheet" />
    <script src="~/Scripts/jquery-3.1.1.min.js"></script>
    <script src="~/Scripts/ej/web/ej.web.all.min.js"></script>


    @*<script src="~/Scripts/ej2/ej2.min.js"></script>
        <link rel="stylesheet" href="https://cdn.syncfusion.com/ej2/material.css" />*@

</head>



@code
    Dim Acknowledgement As Boolean = False
    Dim Principal As ClaimsPrincipal = TryCast(HttpContext.Current.User, ClaimsPrincipal)
    Dim claimsIdentity = CType(Principal.Identities.FirstOrDefault, ClaimsIdentity)
    Dim identity = New ClaimsIdentity(claimsIdentity)

    If Principal IsNot Nothing Then
        If Principal.Claims IsNot Nothing Then
            Dim Claims = Principal.Claims
            For Each claim As Claim In Principal.Claims
                If claim.Type = "Acknowledgement" Then
                    Acknowledgement = claim.Value
                    Session("Acknowledgement") = Acknowledgement
                End If

            Next
        End If
    End If

    'Session("Acknowledgement") = True

End Code
<body>

    @If Request.IsAuthenticated = True And Session("Acknowledgement") = True Then
        @<div Class="navbar navbar-fixed-top">
            <div style="position:relative;margin-top:2px; width:100%;">
                <div id="container" class="container" style="width:100%;">
                    @Html.Partial("_LoginAuthenticated")
                </div>
            </div>
        </div>



        @<div id="page-content-wrapper" style="width:100%;">
            <div class="container-fluid">
                @Using (Html.BeginForm())
                    @<div id="PartialViewContainer" style="margin-top:80px;margin-left:0px;">
                        <div id="PartialView"></div>
                        @*<div class="watermark2">
                                <div class="watermark-image" style="background-repeat: no-repeat;"></div>
                                <div id="PartialView"></div>
                            </div>*@
                    </div>
                End Using
                @RenderBody()
            </div>
        </div>

    End If


    @If Request.IsAuthenticated = False Or Session("Acknowledgement") = False Then
        @<div Class="navbar navbar-fixed-top">
            <div style="position:relative;margin-top:2px; width:100%;">
                <div id="container" class="container" style="width:100%;">
                    @Html.Partial("_Login")
                </div>
            </div>
        </div>

        @<div id="page-content-wrapper" style="width:100%;">
            <div class="container-fluid">        
                    <div id="PartialViewContainer" style="margin-top:80px;margin-left:0px;">
                        <div id="PartialView"></div>
                        @RenderBody()
                    </div>
               
            </div>
        </div>
        @*@<div id="page-content-wrapper" style="width:100%;">
            <div class="container-fluid">
                @Using (Html.BeginForm())
                    @<div id="PartialViewContainer" style="margin-top:80px;margin-left:0px;">
                        <div id="PartialView"></div>
                        @RenderBody()
                    </div>
                End Using
            </div>
        </div>*@

        @*@<div id="page-content-wrapper">
                <div class="container-fluid">
                    @Using (Html.BeginForm())
                        @<div style="margin-top:40px;margin-left:20px;">
                            <div class="watermark">
                                <div class="watermark-image" style="background-repeat: no-repeat;"></div>
                                <div id="PartialView"></div>
                                @RenderBody()
                            </div>
                        </div>
                    End Using
                </div>
            </div>*@
    End If


    @*<script type="text/javascript" src="https://code.jquery.com/jquery-1.12.0.min.js"></script>*@
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" integrity="sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS" crossorigin="anonymous"></script>
    <script src="~/Scripts/Bootstrap/bootsnav.js"></script>


    @RenderSection("scripts", required:=False)
    @*@Html.EJS().ScriptManager()*@

    <script type="text/javascript">
        MVCxClientGlobalEvents.AddControlsInitializedEventHandler(onControlsInitialized);
        ASPxClientControl.GetControlCollection().BrowserWindowResized.AddHandler(onBrowserWindowResized);
    </script>
</body>
</html>



@*<style>
        .watermark {
            /*width: 100%;*/
            height: 710px;
            display: block;
            position: relative;
            /*border: 3px solid #73AD21;*/
            padding: 10px;
            /*margin: auto;*/
        }

        .watermark-image {
            content: "";
            background-repeat: no-repeat;
            /*background: url('../../Content/Images/Logos/CCG_LOGO_COLOR.png');*/
            background-image: url('../../Content/Images/Logos/CCG_LOGO_COLOR2.png');
            background-size: 800px,800px;
            background-position: center;
            opacity: 0.5;
            top: 0;
            left: 0;
            bottom: 0;
            right: 0;
            position: absolute;
            z-index: -1;
            /*border: 2px solid;
            border-color: #CD853F;*/
        }
    </style>*@




