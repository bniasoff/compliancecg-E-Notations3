<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>QBSync</title>

</head>
<body>
    @*<nav class="navbar navbar-expand-sm navbar-dark fixed-top bg-dark">
        <div class="container">
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation"></button>
            <a class="navbar-brand" href="/">QBSync Online</a>
            <div Class="navbar-brand small"><span id="AccountName"></span></div>

            <div class="navbar-collapse collapse" id="navbarSupportedContent">
                <ul class="nav navbar-nav mr-auto"></ul>
                @Html.Partial("_LoginPartial")
            </div>
        </div>
    </nav>*@


    <div class="row" style="margin-top:20px;margin-left:400px;">
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

    @Html.EJS().ScriptManager()

</body>
</html>

<style>
    .body-content {
        padding-left: 5px;
        padding-right: 5px;
    }

    body {
        padding-top: 20px;
        padding-bottom: 20px;
        padding-left: 5px;
        padding-right: 5px;
    }
</style>

