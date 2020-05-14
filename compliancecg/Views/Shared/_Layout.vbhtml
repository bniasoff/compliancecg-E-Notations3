@Imports Microsoft.AspNet.Identity
@imports  System.Security.Claims

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - CCG Compliance</title>
    @Scripts.Render("~/bundles/jquery")
    @*<script src="https://code.jquery.com/jquery-2.2.4.min.js"  integrity="sha256-BbhdlvQf/xTY9gja0Dq3HiwQF8LaCRTXxZKRutelT44=" crossorigin="anonymous"></script>*@
    @Html.DevExpress().GetStyleSheets(New StyleSheet With {.ExtensionSuite = ExtensionSuite.RichEdit})
    @Html.DevExpress().GetScripts(New Script With {.ExtensionSuite = ExtensionSuite.RichEdit})
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">
    <link rel="stylesheet" href="/content/Site.css">
    <link href="https://fonts.googleapis.com/css?family=Raleway:400,700&display=swap" rel="stylesheet">
    @*Styles.Render("~/Content/css")*@
    @Scripts.Render("~/bundles/modernizr")
    @*Scripts.Render("~/bundles/bootstrap")*@
    @Styles.Render("~/Content/ej2/dist/material.css")
    @Scripts.Render("~/Content/ej2/dist/ej2.min.js")
    @*<script src="~/Scripts/jquery-3.1.1.min.js"></script>*@
    @*<link href="~/Content/ej/web/bootstrap-theme/ej.web.all.min.css" rel="stylesheet" />
      <script src="~/Scripts/jquery-3.1.1.min.js"></script>
      <script src="~/Scripts/ej/web/ej.web.all.min.js"></script>*@
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
        @<div Class="container-fluid">
            @Html.Partial("_Login")
            @Html.Partial("_LoginAuthenticated")
        </div>
        @<div id="page-content-wrapper" >
            <div class="container-fluid">
                @Using (Html.BeginForm())
                    @<div id="PartialViewContainer">
                        <div id="PartialView" class="container"></div>
                    </div>
                End Using
                @RenderBody()
            </div>
        </div>
        @<div class="footerRow"></div>
    End If
    @*AE 2/20/20 adding these 2 hiddens so I can reference values on home index page*@
    <input type="hidden" value="@(Request.IsAuthenticated.ToString() )" id="isAuthenticated" />
    @*If Request.IsAuthenticated = True And Session("Acknowledgement") = False Then
            @Html.Partial("../Home/Acknowledgement/")
        End If*@

    @If Request.IsAuthenticated = False Or Session("Acknowledgement") = False Then
        @<div Class="container-fluid">
            @Html.Partial("_Login")
            <div Class="row menuRow p-1">
                @Html.Partial("_LoginPartial")
            </div>
        </div>


        @<div id="page-content-wrapper" >
            <div class="container-fluid p-0" style="height:100%;">
                @RenderBody()
            </div>
        </div>
        @<div class="footerRow"></div>
    End If


    @*<script type="text/javascript" src="https://code.jquery.com/jquery-1.12.0.min.js"></script>*@
    <!--script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" integrity="sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS" crossorigin="anonymous"></script-->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js" integrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6" crossorigin="anonymous"></script>
    <script src="~/Scripts/Bootstrap/bootsnav.js"></script>
    @RenderSection("scripts", required:=False)


    <script type="text/javascript">
        MVCxClientGlobalEvents.AddControlsInitializedEventHandler(onControlsInitialized);
        ASPxClientControl.GetControlCollection().BrowserWindowResized.AddHandler(onBrowserWindowResized);
    </script>
</body>
</html>


