<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Essential Studio for JavaScript : Dialog on Local data</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta charset="utf-8" />

    @*https://cdn.syncfusion.com/17.3.0.9/js/web/ej.web.all.min.js?_ga=2.254284104.1590529771.1574567842-1592731777.1568695234*@


    <link href="https://cdn.syncfusion.com/17.3.0.14/js/web/flat-azure/ej.web.all.min.css" rel="stylesheet" />
    <link href="14.4.0.15/themes/web/content/default.css" rel="stylesheet" />
    <link href="14.4.0.15/themes/web/content/default-responsive.css" rel="stylesheet" />
    <link href="https://cdn.syncfusion.com/17.3.0.14/js/web/responsive-css/ej.responsive.css" rel="stylesheet" />
    <!--[if lt IE 9]>
    <script src="//cdn.syncfusion.com/js/assets/external/jquery-1.10.2.min.js"></script>
    <![endif]-->
    <!--[if gte IE 9]><!-->
    <script src="https://cdn.syncfusion.com/js/assets/external/jquery-3.1.1.min.js"></script>
    <!--<![endif]-->
    <script src="https://cdn.syncfusion.com/js/assets/external/jquery.easing.1.3.min.js"></script>
    <script src="https://cdn.syncfusion.com/js/assets/external/jquery.validate.min.js"></script>
    <script src="https://cdn.syncfusion.com/js/assets/external/jquery.validate.unobtrusive.min.js"></script>
    <script src="14.4.0.15/scripts/web/jsondata.min.js"></script>
    <script src="https://cdn.syncfusion.com/js/assets/external/jsrender.min.js"></script>
    <script type="text/javascript" src="https://cdn.syncfusion.com/17.3.0.14/js/web/ej.web.all.min.js"></script>
    <script src="14.4.0.15/scripts/web/properties.js" type="text/javascript"></script>
</head>
<body>
    <div class="content-container-fluid">
        <div class="row">
            <div class="cols-sample-area">
                <div id="Grid"></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#Grid").ejGrid({
                // the datasource "window.gridData" is referred from jsondata.min.js
                dataSource: window.gridData,
                allowPaging: true,
                pageSettings: { pageSize: 30 },
                enablelAltRow: true,
                actionComplete: "complete",
                editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, editMode: ej.Grid.EditMode.Dialog },
                toolbarSettings: { showToolbar: true, toolbarItems: [ej.Grid.ToolBarItems.Add, ej.Grid.ToolBarItems.Edit, ej.Grid.ToolBarItems.Delete, ej.Grid.ToolBarItems.Update, ej.Grid.ToolBarItems.Cancel] },
                columns: [
                    { field: "OrderID", isPrimaryKey: true, headerText: "Order ID", textAlign: ej.TextAlign.Right, validationRules: { required: true, number: true }, width: 90 },
                    { field: "CustomerID", headerText: 'Customer ID', validationRules: { required: true, minlength: 3 }, width: 90 },
                    { field: "Freight", headerText: 'Freight', format: "{0:C}", textAlign: ej.TextAlign.Right, editType: ej.Grid.EditingType.Numeric, editParams: { decimalPlaces: 2 }, validationRules: { range: [0, 1000] }, width: 75 },
                    { field: "ShipCountry", headerText: "Ship Country", editType: ej.Grid.EditingType.Dropdown, width: 85 },
                    { field: "ShipCity", headerText: 'Ship City', width: 90 }
                ]
            });

        });
        function complete(args) {
            if ((args.requestType == "beginedit" || args.requestType == "add") && args.model.editSettings.editMode == "dialog") {
                var row = this.model.selectedRowIndex;
                var target = this.getRowByIndex(row);
                var $target = $(target);
                var docWidth = $(document).width(), dlgWidth = document.documentElement.clientWidth < 800 ? 200 : 250,
                    xPos = $target.position().left + 18,
                    yPos = $target.position().top + 2;
                $("#" + this._id + "_dialogEdit").ejDialog({
                    title: args.requestType == "beginedit" ? "Edit Equipment" : "Add Equipment",
                    enableResize: true, //enable resize to dialog
                    position: { X: xPos, Y: yPos },
                });
            }
        }
    </script>
</body>
</html>