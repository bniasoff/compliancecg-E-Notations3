@Imports Syncfusion.EJ2
@Imports Newtonsoft.Json
<script src="https://cdn.syncfusion.com/ej2/dist/ej2.min.js" type="text/javascript"></script>
<link href="https://cdn.syncfusion.com/ej2/material.css" rel="stylesheet">

<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet">




<div id="target" style="display:block;height:600px">
    @Html.EJS().Button("targetButton").Content("Open PDF viewer").Render()
    @Html.EJS().Dialog("dialog").Height("600px").Visible(False).Width("1000px").IsModal(True).Target("#target").Header("PDF Viewer").ShowCloseIcon(True).Render()
    <div id="dialog_content" style="display:none;border:1px solid #E0E0E0; width:100%;height:600px">
        <div id="pdfViewer" style="height:750px"></div>
    </div>
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
        var dialog = document.getElementById("dialog").ej2_instances[0];
        var filePath = "PDF Succinctly.pdf";
        dialog.show();
        if (pdfviewer) {
            //Load the document in PDF viewer

            pdfviewer.load(filePath, null);
        } else {
            // initialize the PDF viewer control
            var viewer = new ej.pdfviewer.PdfViewer({
                enableNavigation: true,
                enableHyperlink: true,
                hyperlinkOpenState: 'NewTab',
                enableBookmark: true,
                enableTextSelection: true,
                serviceUrl: '/Policies',
                documentPath: filePath,
            });
            ej.pdfviewer.PdfViewer.Inject(ej.pdfviewer.TextSelection, ej.pdfviewer.TextSearch, ej.pdfviewer.Print, ej.pdfviewer.Navigation, ej.pdfviewer.TextSelection);
            viewer.appendTo('#pdfViewer');
            pdfviewer = document.getElementById("pdfViewer").ej2_instances[0];
            //Append the PDF viewer control to the dialog window.
            pdfviewer.appendTo('#dialog');
        }
    };


</script>


@Html.EJS().ScriptManager()