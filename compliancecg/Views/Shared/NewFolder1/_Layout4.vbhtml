﻿<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>@ViewBag.Title - My ASP.NET Application</title>
    <link href='http://fonts.googleapis.com/css?family=Aldrich' rel='stylesheet'>
    <link href="~/css/screen.css" rel="stylesheet" />

    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")

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
                @Html.ActionLink("ADAPTOR jQuery 3D content slider", "Index", "Home", Nothing, New With {.class = "navbar-brand"})
            </div>

        </div>
    </div>
    <div>
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>
        </footer>
    </div>

    @Scripts.Render("~/bundles/jquery")

    @*<script src="~/js/box-slider.jquery.js"></script>
        <script src="~/js/effects/box-slider-fx-scroll-3d.js"></script>
        <script src="~/js/effects/box-slider-fx-fade.js"></script>
        <script src="~/js/effects/box-slider-fx-scroll.js"></script>
        <script src="~/js/effects/box-slider-fx-blinds.js"></script>
        <script src="~/js/effects/box-slider-fx-carousel-3d.js"></script>
        <script src="~/js/effects/box-slider-fx-tile-3d.js"></script>*@


    @*<script src="//code.jquery.com/jquery-1.7.2.min.js"></script>
        <script>window.jQuery || document.write('<script src="js/lib/jquery-1.7.2.min.js"><\/script>')</script>*@



    <script src="~/js/box-slider-all.jquery.min.js"></script>

    @*@Scripts.Render("~/bundles/bootstrap")*@
    @RenderSection("scripts", required:=False)
</body>
</html>
