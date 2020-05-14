﻿<!DOCTYPE html>
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
</head>
<body>
    <div class="content-container-fluid">
        <div class="row">
            <div class="cols-sample-area">
                <div style="width:100%">
                    <div id="selectioncontainer" class="e-pdfviewer-toolbarcontainer" style="width:100%;padding-top:5px;padding-bottom:3px;padding-right:4px">
                        <div class="dropDownList">
                            <select id="selectid">
                                <option>HTTP Succinctly</option>
                                <option>ASP.NET MVC 4 Succinctly</option>
                                <option>F# Succinctly</option>
                                <option>GIS Succinctly</option>
                                <option>Windows Store Apps Succinctly</option>
                            </select>
                        </div>
                        <label id="labelid" style="float:right;margin-right:10px;margin-top:13px">
                            Select a PDF file to view:
                        </label>
                        <div style="padding:10px">
                            <input type="file" id="fileUpload">
                            <button id="fileUploadButton">Choose file from disk</button>
                        </div>
                    </div>
                    <div class="control">
                        <div id="container" style="height: 680px;"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#container").ejPdfViewer({ serviceUrl: window.baseurl + 'api/PdfViewer', pdfService: ej.PdfViewer.PdfService.Local });
            $("#selectioncontainer").ejToolbar({ height: '56px' });
            $('#selectid').ejDropDownList({ height: '27px', width: '223px', change: 'dropDownChange', selectedIndex: 0 });
            $('#fileUploadButton').ejButton();
        });
        function dropDownChange(sender) {
            var _filename = sender.value;
            var _ejPdfViewer = $("#container").data("ejPdfViewer");
            _ejPdfViewer.load(_filename);
        }
        document.getElementById('fileUpload').addEventListener('change', readFile, false);
        function readFile(evt) {
            var upoadedFiles = evt.target.files;
            var uploadedFile = upoadedFiles[0];
            var reader = new FileReader();
            reader.readAsDataURL(uploadedFile);
            reader.onload = function () {
                var obj = $("#container").data("ejPdfViewer");
                var uploadedFileUrl = this.result;
                obj.load(uploadedFileUrl);
            }
        }
        $('#fileUploadButton').click(function () {
            $('#fileUpload').click();
        });
    </script>
    <style>
        .control {
            width: 100%;
            min-height: 680px !important;
        }

        .content-container-fluid > :first-child.row {
            width: 100% !important;
        }

        .cols-sample-area {
            min-height: 706px !important;
            width: 100% !important;
        }

        .dropDownList {
            float: right;
            margin-right: 10px;
            margin-top: 9px;
        }

        * {
            padding: 0;
            margin: 0;
        }

        .frame {
            height: 600px;
            width: 950px;
        }

        #fileUpload {
            display: block;
            visibility: hidden;
            width: 0;
            height: 0;
        }
    </style>
</body>
</html>
