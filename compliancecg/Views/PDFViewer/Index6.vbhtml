@imports Syncfusion.JavaScript
@imports Syncfusion.MVC.EJ

@*<link href="~/Content/ej/web/material/ej.web.all.min.css" rel="stylesheet" />
<script src="~/Scripts/ej/web/ej.web.all.min.js"></script>*@

<span class="sampleName">PdfViewer-Getting Started-ASP.NET MVC-SYNCFUSION</span>
<meta name="description" content="This sample demonstrates viewing, reviewing, printing, and signing functionalities in the Syncfusion ASP.NET MVC PDF viewer control">



<style>

    #fileUpload {
        display: block;
        visibility: hidden;
        width: 0;
        height: 0;
    }
</style>




<div class="content-container-fluid">
    <div class="cols-sample-area">
        <div style="width:100%">
            <div style="float:left; padding-bottom:5px; display:inline-flex;">
                <input type="file" id="fileUpload">
                @Html.EJ().Button("fileUploadButton").Text("Choose file from disk")
            </div>
            <div style="float:right; padding-bottom:5px; display:inline-flex;">

                <label id="labelid">
                    Select a PDF file to view:
                </label>

                <div>
                    @Html.EJ().DropDownList("selectPDF").TargetID("pdfList").Width("100%").WatermarkText("Select a PDF").SelectedIndex(0).ClientSideEvents(Sub(events) events.Change("dropDownChange"))
                    <div id="pdfList">
                        <ul>
                            <li>HTTP Succinctly</li>
                            <li>ASP.NET MVC 4 Succinctly</li>
                            <li>F# Succinctly</li>
                            <li>GIS Succinctly</li>
                            <li>Windows Store Apps Succinctly</li>
                        </ul>
                    </div>
                </div>
            </div>

            <div style="width:100%;height:100%;padding-left: 10px;min-height: 680px;float:right">
                @Html.EJ().PdfViewer("pdfviewer").ServiceUrl(VirtualPathUtility.ToAbsolute("/PdfViewer")).PdfService(Syncfusion.JavaScript.PdfViewerEnums.PdfService.Local)
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    function dropDownChange(sender) {
        var _filename = sender.value;
        var _ejPdfViewer = $("#pdfviewer").data("ejPdfViewer");
        _ejPdfViewer.load(_filename);
    }
    $(document).ready(function () {
        document.getElementById('fileUpload').addEventListener('change', readFile, false);
        $('#fileUploadButton').click(function () {
            $('#fileUpload').click();
        });
    });
    function readFile(evt) {
        var uploadedFiles = evt.target.files;
        if (uploadedFiles.length > 0) {
            var uploadedFile = uploadedFiles[0];
            var reader = new FileReader();
            reader.readAsDataURL(uploadedFile);
            reader.onload = function () {
                var obj = $("#pdfviewer").data("ejPdfViewer");
                var uploadedFileUrl = this.result;
                obj.load(uploadedFileUrl);
            }
            this.value = null;
        }
    }
</script>

@(Html.EJ().ScriptManager())