
<link href="~/Content/ej/web/material/ej.web.all.min.css" rel="stylesheet" />
<script src="~/Scripts/ej/web/ej.web.all.min.js"></script>


<style>

    #pdfviewer {
        display: block;
        height: 641px;
    }
</style>

<div>
    <div id="viewer" style="height: 650px;width: 950px;min-height:404px;"></div>
    <div id="pdfViewer" style="height:750px"></div>
</div>


<script type="text/javascript">
    //debugger;
    //var filePath = "Sample UnSecured.pdf";
    //var viewer = new ej.pdfviewer.PdfViewer({
    //    serviceUrl: '/PdfViewer',
    //    documentPath: filePath,
    //});
    //viewer.appendTo('#pdfViewer');

    //$(function () {
    //    //var pdfviewer = $('#viewer').data('ejPdfViewer');
    //    //pdfviewer.load('HTTP Succinctly');
    //    var filePath = "Sample UnSecured.pdf";
    //    var viewer = new ej.pdfviewer.PdfViewer({
    //        serviceUrl: '/PdfViewer',
    //        documentPath: filePath,
    //    });

    //    viewer.appendTo('#pdfViewer');
    //});

    $(function () {
        var pdfviewer;
        var filePath = "Sample UnSecured.pdf";
  

        $("#viewer").ejPdfViewer({ serviceUrl: '/PdfViewer', documentPath: filePath });
        viewer.appendTo('#pdfViewer');


        //$("#viewer").ejPdfViewer({ serviceUrl: '/PdfViewer', serverActionSettings: { load: "Load", fileUpload: "FileUpload"} });

    });
    //function documentLoaded(args) {
    //    alert("The document" + args.fileName + "is ready to view");
    //}
</script>
