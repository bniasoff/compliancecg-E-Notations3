@Imports Syncfusion.EJ2
@Imports Newtonsoft.Json
<script src="https://cdn.syncfusion.com/ej2/dist/ej2.min.js" type="text/javascript"></script>
<link href="https://cdn.syncfusion.com/ej2/material.css" rel="stylesheet">

<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet">




<div id="target" style="display:block;height:600px">
    @Html.EJS().Button("targetButton").Content("Open PDF viewer").Render()

    <div id="pdfViewer" style="height:750px"></div>
</div>

<style>
    #pdfviewer {
        display: block;
        height: 641px;
    }
</style>

<script>
    var pdfviewer;
    // var dialogContent = document.getElementById("dialog_content");
    document.getElementById('targetButton').onclick = () => {
        var filePath = "PDF Succinctly";

        if (pdfviewer) {
            pdfviewer.load(filePath, null);
        } else {
            var viewer = new ej.pdfviewer.PdfViewer({
                serviceUrl: '/PdfViewer',
                documentPath: filePath,
            });

            viewer.appendTo('#pdfViewer');
            pdfviewer = document.getElementById("pdfViewer").ej2_instances[0];
            //ej.pdfviewer.PdfViewer.Inject(ej.pdfviewer.TextSelection, ej.pdfviewer.TextSearch, ej.pdfviewer.Print, ej.pdfviewer.Navigation);
        }
    };
</script>

@Html.EJS().ScriptManager()
