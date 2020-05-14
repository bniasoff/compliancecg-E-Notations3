@imports Syncfusion.JavaScript
@imports Syncfusion.MVC.EJ

    @*<link href="~/assets/css/web/default-theme/ej.web.all.min.css" rel="stylesheet" />
    <script src="~/assets/external/jquery-2.1.4.min.js"></script>
    <script src="~/assets/scripts/web/ej.web.all.min.js"></script>*@

    <link href="~/Content/ej/web/material/ej.web.all.min.css" rel="stylesheet" />
        @*<script src="../scripts/jquery-3.4.1.min.js" type="text/javascript"></script>*@
    <script src="~/Scripts/ej/web/ej.web.all.min.js"></script>




    <div class="content-container-fluid">
        <div class="cols-sample-area">
            <div style="width:100%">
                <div style="width:100%;min-height:600px">
                    <table>
                        <tr>
                            <td>
                                <label>Enter the documentName:</label>
                                <input type="text" id="documentName" />
                            </td>
                            <td>
                                <button id="viewButton">View</button>
                            </td>
                        </tr>
                    </table>
                    <div id="pdfviewer"></div>
                </div>
            </div>
        </div>
    </div>



    <script type="text/javascript">
        var isControlInitialized = false;

        $('#viewButton').click(function (event) { view(event) });
        function view(event) {       
            var jsonObject = new Object();
            var docIDVal = $("#documentName").val();
            if (docIDVal || docIDVal != "") {
               
                jsonObject['documentName'] = docIDVal;
                var jsonResult2 = JSON.stringify(jsonObject);

               // alert(jsonResult2 )
                $.ajax({
                    type: 'POST',
                    url: '/PDFViewer/DocumentRetrieve',
                    //data: { deviceId: id },
                    data: jsonResult2,
                    success: function (data) {
                        //alert('Succes')
                        //debugger;
                        openDoc(data);
                    },
                    error: function (xhr, textStatus, error) {
                        alert('fail')
                        //console.log(xhr.statusText);
                        //console.log(textStatus);
                        //console.log(error);
                    }
                });





                //$.ajax({
                //    type: "POST",
                //    url: '/PDFViewer/DocumentRetrieve',
                //    crossDomain: true,
                //    contentType: 'application/json; charset=utf-8',
                //    dataType: 'json',
                //    data: jsonResult,
                //    traditional: true,
                //    success: function (data2) {
                //        alert('Succes')
                //        debugger;
                //        //openDoc(data2);
                //    }
                //});
            }
        }


        function openDoc(data) {
            //debugger;
            if (!isControlInitialized) {
                $("#pdfviewer").ejPdfViewer({ serviceUrl: "/PDFViewer", pdfService: ej.PdfViewer.PdfService.Local });
                isControlInitialized = true;
            }
            var obj = $("#pdfviewer").data("ejPdfViewer");
            obj.load(data);
        }
    </script>
