<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Essential JS 1 : PDF viewer</title>
    <meta charset="utf-8" />
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />

    <link href="~/Content/ej/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/ej/default.css" rel="stylesheet" />
    <link href="~/Content/ej/default-responsive.css" rel="stylesheet" />


    @*<link href="../content/bootstrap.min.css" rel="stylesheet" />
        <link href="../content/default.css" rel="stylesheet" />
        <link href="../content/default-responsive.css" rel="stylesheet" />*@
    @*<link href="../content/ejthemes/default-theme/ej.web.all.min.css" rel="stylesheet" type="text/css" />*@




    <link href="~/Content/ej/web/material/ej.web.all.min.css" rel="stylesheet" />
    <!--[if lt IE 9]>
    <script src="../scripts/jquery-1.11.3.min.js" type="text/javascript"></script>
    <![endif]-->
    <!--[if gte IE 9]><!-->
    <script src="../scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <!--<![endif]-->
    <script src="~/Scripts/ej/web/ej.web.all.min.js"></script>
    @*<script src="../scripts/ej.web.all.min.js" type="text/javascript"></script>*@
    <script src="../scripts/properties.js" type="text/javascript"></script>

    <style type="text/css">
        .control {
            padding-left: 10px;
            min-height: 680px !important;
        }

        .content-container-fluid > :first-child.row {
            width: 100% !important;
        }

        .cols-sample-area {
            min-width: 800px !important;
            min-height: 706px !important;
            width: 100% !important;
        }
    </style>
</head>


<body>
    <div class="content-container-fluid">
        <div class="row">
            <div class="cols-sample-area">
                <div class="control">
                    <div id="container" style="width:100%;height:780px;"></div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#container").ejPdfViewer({ serviceUrl: '../PdfViewer' });
        });
    </script>
    <style>
        .frame {
            height: 600px;
            width: 950px;
        }
    </style>
</body>


</html>
