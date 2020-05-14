@Imports Syncfusion.EJ2
@Imports Newtonsoft.Json
<script src="https://cdn.syncfusion.com/ej2/dist/ej2.min.js" type="text/javascript"></script>
    <link href="https://cdn.syncfusion.com/ej2/material.css" rel="stylesheet">

<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet">

<style>
        body {
            touch-action: none;
        }
    </style>
      


<body>
        <div class="stackblitz-container material">
            <div class="control-section">
                <div class="content-wrapper">
                    <div id="pdfViewer" style="height:640px; width:100%;">
                    </div>
                </div>
            </div>
        </div>
    </body>


<script>
        ej.base.enableRipple(true);

        //var viewer = new ej.pdfviewer.PdfViewer({
        //    documentPath: "PDF_Succinctly.pdf",
        //    serviceUrl: 'https://ej2services.syncfusion.com/production/web-services/api/pdfviewer'         
        //});

    var viewer = new ej.pdfviewer.PdfViewer({
        documentPath: "PDF Succinctly.pdf",
        serviceUrl: '/PdfViewer'
    });

        ej.pdfviewer.PdfViewer.Inject(ej.pdfviewer.Toolbar, ej.pdfviewer.Magnification, ej.pdfviewer.BookmarkView, ej.pdfviewer.ThumbnailView, ej.pdfviewer.TextSelection, ej.pdfviewer.TextSearch, ej.pdfviewer.Print, ej.pdfviewer.Navigation, ej.pdfviewer.LinkAnnotation, ej.pdfviewer.Annotation, ej.pdfviewer.FormFields);
        viewer.appendTo('#pdfViewer');

    </script>


@*<div class="control-section">
    @Html.EJS().PdfViewer("pdfviewer").ServiceUrl(VirtualPathUtility.ToAbsolute("/PdfViewer")).DocumentPath("PDF Succinctly.pdf").Render()
</div>*@


@Html.EJS().ScriptManager()
