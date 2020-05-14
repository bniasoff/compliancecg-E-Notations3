<!doctype html>
<html lang="en">
<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">



    @*<title></title>
    <link href="@Url.Content("~/Content/bootstrap.min.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/Site.css")" rel="stylesheet" type="text/css" />

    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)

    <link rel="stylesheet" href="https://cdn.syncfusion.com/ej2/bootstrap.css" />
    <script src="https://cdn.syncfusion.com/ej2/dist/ej2.min.js"></script>


    <link href="https://kendo.cdn.telerik.com/2018.3.1017/styles/kendo.common-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="https://kendo.cdn.telerik.com/2018.3.1017/styles/kendo.mobile.all.min.css" rel="stylesheet" type="text/css" />
    <link href="https://kendo.cdn.telerik.com/2018.3.1017/styles/kendo.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="https://kendo.cdn.telerik.com/2018.3.1017/js/jquery.min.js"></script>
    <script src="https://kendo.cdn.telerik.com/2018.3.1017/js/jszip.min.js"></script>
    <script src="https://kendo.cdn.telerik.com/2018.3.1017/js/kendo.all.min.js"></script>
    <script src="https://kendo.cdn.telerik.com/2018.3.1017/js/kendo.aspnetmvc.min.js"></script>
    <script src="@Url.Content("~/Scripts/kendo.modernizr.custom.js")"></script>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" />
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />*@
    
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">


</head>


<body>
    @If Request.IsAuthenticated = True Then
        @Html.Partial("_LoginAuthenticated")
        @<!-- /#sidebar-wrapper -->
        @<!-- Page Content -->

        @<div id="page-content-wrapper">
            <div class="container-fluid">
                <a href="#menu-toggle" class="btn btn-secondary" id="menu-toggle">Toggle Menu</a>
                @RenderBody()
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

    <!-- /#page-content-wrapper -->
    </div>


    <!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>


</body>
</html>


<script>
            $("#menu-toggle").click(function (e) {
                   e.preventDefault();
                $("#wrapper").toggleClass("toggled");
            });
</script>




<!-- end snippet -->
