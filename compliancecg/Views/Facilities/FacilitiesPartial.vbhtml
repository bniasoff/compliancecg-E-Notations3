@imports  Syncfusion.EJ2.Popups
@*<script src="~/Scripts/jquery-3.3.1.js"></script>*@
@Code
    Dim commands As List(Of Object) = New List(Of Object)()
    commands.Add(New With {.Type = "Edit", .buttonOption = New With {.iconCss = "e-icons e-edit", .cssClass = "e-flat"}})
    commands.Add(New With {.Type = "Delete", .buttonOption = New With {.iconCss = "e-icons e-delete", .cssClass = "e-flat"}})
    commands.Add(New With {.Type = "Save", .buttonOption = New With {.iconCss = "e-icons e-update", .cssClass = "e-flat"}})
    commands.Add(New With {.Type = "Cancel", .buttonOption = New With {.iconCss = "e-icons e-cancel-icon", .cssClass = "e-flat"}})
    commands.Add(New With {.Type = "Update", .buttonOption = New With {.iconCss = "e-icons e-update", .cssClass = "e-flat"}})

    Dim commands2 As List(Of Object) = New List(Of Object)()
    commands2.Add(New With {.type = "userstatus", .buttonOption = New With {.content = "Users", .cssClass = "e-flat"}})
End code



@code
    Dim FacilityUserList = New Syncfusion.EJ2.DropDowns.DropDownList() With
{
.DataSource = ViewBag.FacilityUserDropDownList,
.Query = "new ej.data.Query()",
.Fields = New Syncfusion.EJ2.DropDowns.DropDownListFieldSettings() With {.Value = "value", .Text = "text"},
.AllowFiltering = True
}

    Dim JobTitleList = New Syncfusion.EJ2.DropDowns.DropDownList() With
{
.DataSource = ViewBag.JobTitlesDropDownList,
.Query = "new ej.data.Query()",
.Fields = New Syncfusion.EJ2.DropDowns.DropDownListFieldSettings() With {.Value = "value", .Text = "text"},
.AllowFiltering = False
}

    Dim UsersList = New Syncfusion.EJ2.DropDowns.DropDownList() With
{
.DataSource = ViewBag.UsersDropDownList,
.Query = "new ej.data.Query()",
.Fields = New Syncfusion.EJ2.DropDowns.DropDownListFieldSettings() With {.Value = "value", .Text = "text"},
.AllowFiltering = False
}
End Code




<div id="target" class="col-lg-12 control-section" style="display:none; height:600px;">
    @Html.EJS().Dialog("ExternalDialog").IsModal(True).AnimationSettings(New DialogAnimationSettings() With {.Effect = DialogEffect.None}).ContentTemplate(@@<div>

        <div style="margin-left:20px;margin-top:10px;margin-bottom:20px;">
            <div class="custom-control custom-radio custom-control-inline" style="float:left">
                <input id="radioButton3" type="radio" class="custom-control-input" checked name="inlineDefaultRadiosExample" onclick="onRadioChange2()">
                <label style="margin-right:20px;" class="custom-control-label" for="defaultInline1">Active</label>
            </div>

            <!-- Default inline 2-->
            <div class="custom-control custom-radio custom-control-inline" style="float:left">
                <input id="radioButton4" type="radio" class="custom-control-input" name="inlineDefaultRadiosExample" onclick="onRadioChange2()">
                <label style="margin-right:20px;" class="custom-control-label" for="defaultInline2">InActive</label>
            </div>
        </div>

        <div style="clear:left;"></div>
        <div style="margin-top:20px;" id="childLabel"></div>
        <div id="childGrid"></div>
    </div>).ShowCloseIcon(True).CloseOnEscape(True).Width("1300px").Height("1400px").Target("#Grid").Created("Created").Visible(False).Render()
</div>

<div id="target2" Class="col-lg-12 control-section" style="display:none; height:200px;">
    @Html.EJS().Dialog("ExternalDialog2").IsModal(True).AnimationSettings(New DialogAnimationSettings() With {.Effect = DialogEffect.None}).ContentTemplate(@@<div>
        <div id="childGrid2"></div>
    </div>).ShowCloseIcon(True).CloseOnEscape(True).Width("350px").Height("600px").Target("#Grid").Created("Created").Visible(False).Render()
</div>


<div style="margin-left:20px;margin-top:10px;margin-bottom:20px;">
    <div style="float:left;margin-right:20px">
        @Html.EJS().RadioButton("radio1").Label("Active").Name("DisplayActive").Change("onRadioChange").Checked(True).Render()
    </div>
    <div style="float:left">
        @Html.EJS().RadioButton("radio2").Label("InActive").Name("DisplayActive").Change("onRadioChange").Render()
    </div>
</div>

@*<div><span>Test</span></div>*@

<div style="margin-left:20px;margin-top:40px;">
    @Html.EJS().Grid("Grid").DataSource(Function(DataManager) DataManager.Url("/Facilities/UrlDatasource").InsertUrl("/Facilities/Insert").UpdateUrl("/Facilities/Update").RemoveUrl("/Facilities/Delete").Adaptor("UrlAdaptor")).Height("600").Width("1450").AllowPaging(True).ActionComplete("actionComplete").Load("load").Columns(Sub(col)
                                                                                                                                                                                                                                                                                                                                                       col.HeaderText("Users").Width("90").Commands(commands2).Add()
                                                                                                                                                                                                                                                                                                                                                       col.Field("FacilityID").HeaderText("FacilityID").IsPrimaryKey(True).Visible(False).Add()
                                                                                                                                                                                                                                                                                                                                                       col.Field("Name").HeaderText("Name").Width("250").ValidationRules(New With {.required = True, .minLength = 3}).Add()
                                                                                                                                                                                                                                                                                                                                                       col.Field("Address").HeaderText("Address").Width("200").Add()
                                                                                                                                                                                                                                                                                                                                                       col.Field("City").HeaderText("City").Width("120").Add()
                                                                                                                                                                                                                                                                                                                                                       col.Field("State").HeaderText("State").Width("110").Add()
                                                                                                                                                                                                                                                                                                                                                       col.Field("Zip").HeaderText("Zip").Width("100").Add()
                                                                                                                                                                                                                                                                                                                                                       col.Field("Phone1").HeaderText("Phone1").Width("115").Add()
                                                                                                                                                                                                                                                                                                                                                       col.Field("Phone2").HeaderText("Phone2").Width("115").Add()
                                                                                                                                                                                                                                                                                                                                                       col.Field("Beds").HeaderText("Beds").Width("75").Add()
                                                                                                                                                                                                                                                                                                                                                       col.Field("ShiftChange").HeaderText("ShfChnge").Width("75").Add()
                                                                                                                                                                                                                                                                                                                                                       col.Field("InActive").HeaderText("InActive").EditType("booleanedit").DisplayAsCheckBox(True).Type("boolean").Width("150").Add()
                                                                                                                                                                                                                                                                                                                                                   End Sub).SelectionSettings(Sub(selection) selection.Type(Syncfusion.EJ2.Grids.SelectionType.Single)).AllowSorting().AllowFiltering().AllowGrouping().AllowResizing(True).AllowPaging().PageSettings(Sub(page) page.PageCount(2)).EditSettings(Sub(edit) edit.AllowAdding(True).AllowEditing(True).AllowDeleting(True).Mode(Syncfusion.EJ2.Grids.EditMode.Normal)).FilterSettings(Sub(Filter) Filter.Type(Syncfusion.EJ2.Grids.FilterType.Excel)).Toolbar(New List(Of String)(New String() {"Add", "Edit", "Delete", "Update", "Cancel"})).Render()


</div>



<script id="datetemplate" type="text/x-template">
    <span class="e-icon-calender e-icons headericon"></span> DateofEntry
</script>

<script type="text/javascript">

    function onChange2() {
        debugger;
        var radioButton3 = document.getElementById("radioButton3");
        var radioButton4 = document.getElementById("radioButton4");

        if (radioButton3.checked == true) {

        } else {

        }
    }
</script>

<script type="text/javascript">


        var radiobutton1
        var radiobutton2
        var UserEmails2

        var Active1
    function onRadioChange() {
            var RadioButton1 = document.getElementById('radio1').ej2_instances[0];
            var RadioButton2 = document.getElementById('radio2').ej2_instances[0];

            if (RadioButton1.checked == true) { Active1 = 1 }
            if (RadioButton2.checked == true) { Active1 = 0 }

            $.ajax({
                url: '/Facilities/SetFacilityActiveSessionValues',
                type: 'POST',
                data: jQuery.param({ Active: Active1 }),
                success: function (data) {
                    var grid = document.getElementById('Grid').ej2_instances[0];
                    grid.refresh();
                },
                error: function () {
                    alert("error");
                }
            });
        };

        var Active2
    function onRadioChange2() {
            var radioButton3 = document.getElementById("radioButton3");
            var radioButton4 = document.getElementById("radioButton4");

            if (radioButton3.checked == true) { Active2 = 1 }
            if (radioButton4.checked == true) { Active2 = 0 }

                $.ajax({
                    url: '/Facilities/SetFacilityUsersActiveSessionValues',
                    type: 'POST',
                    data: jQuery.param({ Active: Active2 }),
                    success: function (data) {
                        gridInstance.refresh();
                    },
                    error: function () {
                        alert("error");
                    }
                });
         };

    function actionComplete(args) {
        if (args.requestType == "beginEdit") {
            var form = args.form;

            //debugger;
            //var dialog = closestNode(form, "e-dialog");

            //var externalDialog = document.getElementById("ExternalDialog").ej2_instances[0];
            //if (externalDialog && externalDialog.visible) {
            //    dialog.style.zIndex = externalDialog.element.style.zIndex;
            //    dialog.parentNode.style.zIndex = externalDialog.element.style.zIndex;
            //}
        }
    }

    function closestNode(node, className) {
        while (!node.classList.contains(className)) {
            node = node.parentNode;
        }
        return node;

    }



    var gridInstance
    var gridInstance2
    var rowData1
    var rowData2
    var FacilityUsers
    var FacilitUserEmails

    function load() {
        this.columns[0].commands[0].buttonOption.click = function (args) {
            var row = new ej.base.closest(event.target, '.e-row');
            var index = row.getAttribute('aria-rowindex')
            var grid = document.getElementById('Grid').ej2_instances[0];

            rowData1 = grid.currentViewData[index];
            var request = JSON.stringify(rowData1);

            $.post('/Facilities/SetFacilitySessionValues', request)
                .done(function (data) {
                    if (data.length = 'True') {
                        $.ajax({
                            url: '/Facilities/GetFacilityUsers2',
                            type: 'POST',
                            data: jQuery.param({ FacilityID: rowData1.FacilityID }),
                            success: function (data) {
                                var dataObject = JSON.parse(data)

                                var dialog = document.getElementById("ExternalDialog").ej2_instances[0];
                                dialog.show();

                                var childGridElement = document.getElementById("childGrid");
                                if (childGridElement && childGridElement.classList.contains("e-grid")) {
                                    childGridElement.ej2_instances[0].destroy();
                                }

                                gridInstance = new ej.grids.Grid({
                                    allowPaging: true,

                                    dataSource: new ej.data.DataManager({
                                        url: "/Facilities/UrlDatasource4",
                                        insertUrl: "/Facilities/Insert4",
                                        updateUrl: "/Facilities/Update4",
                                        removeUrl: "/Facilities/Delete4",
                                        adaptor: new ej.data.UrlAdaptor()
                                        //.Query().addParams("UserID",FacilityGroup.FacilityGroupID)
                                    }),

                                    actionComplete: (args) => {
                                       if (args.requestType == 'save') {
                                            gridInstance.refresh();
                                        }
                                        if (args.requestType == 'update') {
                                            gridInstance.refresh();
                                        }
                                        if (args.requestType === 'beginEdit') {
                                            EditMode = 'Edit'
                                            //rowData = args.rowData
                                        }
                                        if (args.requestType === 'add') {
                                            EditMode = 'Add'
                                        }
                                    },

                                    actionBegin: (args) => {
                                        if (args.requestType === 'beginEdit') {
                                            EditMode = 'Edit'
                                            var row = new ej.base.closest(event.target, '.e-row');
                                            var index = row.getAttribute('aria-rowindex')
                                            rowData2 = gridInstance.currentViewData[index];
                                        }

                                        //if (args.requestType == 'save') {
                                        //    gridInstance2.refresh();
                                        //}
                                        //if (args.requestType == 'update') {
                                        //}
                                    },

                                    //dataSource: dataObject,
                                    load: load2,
                                    height: 500,
                                    width: 1250,
                                    allowSorting: true,
                                    allowFiltering: true,
                                    allowResizing: true,
                                    filterSettings: { type: "Excel" },

                                    editSettings: { allowAdding: true, allowEditing: true, allowDeleting: true, mode: "Normal" },
                                    toolbar: ["Add", "Edit", "Update", "Delete", "Cancel"],
                                    columns:
                                        [
                                            { headerText: 'Roles', width: 120, commands: [{ buttonOption: { content: 'Roles', cssClass: 'e-flat' } }] },
                                            { field: "UserID", headerText: "UserID", width: 80, isPrimaryKey: true, visible: false },
                                            { field: "LastName", headerText: "LastName", width: 150, minWidth: 100, maxWidth: 200 },
                                            { field: "FirstName", headerText: "FirstName", width: 150 },
                                            { field: "EmailAddress", headerText: "Email", width: 250 },
                                            { field: "Phone1", headerText: "Phone1", width: 150 },
                                            { field: "DateCreated", headerText: "DateCreated", width: 100, allowEditing: false, format: { type: 'date', format: 'MM/dd/yyyy' }, editType: 'datepickeredit', textAlign: 'Left' },
                                            { field: "FacilityInActive", headerText: "InActive", width: 150, editType: "booleanedit", displayAsCheckBox: true, type: 'boolean', textAlign: 'Center' }
                                        ]
                                    //{ field: 'OrderDate', headerText: 'Order Date', type: 'date', width: 120, format: { type: 'date', format: 'dd.MM.yyyy' }, editType: 'datepickeredit', edit: { params: { format: 'dd.MM.yy' } } },
                                    //{ field: 'OrderID', headerText: 'Order ID', textAlign: 'Right', minWidth: 100, width: 150, maxWidth: 300 },

                                    //{
                                    //    headerText: 'Manage Records', width: 160,
                                    //    commands: [{ type: 'Edit', buttonOption: { iconCss: ' e-icons e-edit', cssClass: 'e-flat' } },
                                    //    { type: 'Delete', buttonOption: { iconCss: 'e-icons e-delete', cssClass: 'e-flat' } },
                                    //    { type: 'Save', buttonOption: { iconCss: 'e-icons e-update', cssClass: 'e-flat' } },
                                    //    { type: 'Cancel', buttonOption: { iconCss: 'e-icons e-cancel-icon', cssClass: 'e-flat' } }]
                                    //},
                                })
                                gridInstance.appendTo("#childGrid");

                                $("#childLabel").empty();
                                $("#childLabel").append("<h4>" + rowData1.Name + "</h4>");
                            },
                            error: function () {
                                alert("error");
                            }
                        });

                    }
                });
        }
    }







    function load2() {

           $.ajax({
               url: '/Facilities/GetFacilitUserEmails',
                type: 'POST',
                data: jQuery.param({ FacilityID: rowData1.FacilityID}),
                success: function (data) {
                    FacilitUserEmails = JSON.parse(data)
                },
                error: function () {
                    alert("error");
                }
            });

        //$.ajax({
        //    url: '/Facilities/GetFacilitUserEmails',
        //    type: 'POST',
        //    success: function (data) {
        //        FacilitUserEmails = JSON.parse(data)
        //    }
        //});

        this.columns[4].validationRules = { email: true, minLength: [customFn, 'Duplicate Email'] };

        this.columns[0].commands[0].buttonOption.click = function (args) {

            var row = new ej.base.closest(event.target, '.e-row');
            var index = row.getAttribute('aria-rowindex')
            //var grid = document.getElementById('Grid').ej2_instances[0];

            rowData2 = gridInstance.currentViewData[index];
            var request = JSON.stringify(rowData2);
            var JobTitlesDropDownList =@Html.Raw(Json.Encode(ViewBag.JobTitlesDropDownList));

            $.post('/Facilities/SetUserSessionValues', request)
                .done(function (data) {


                    if (data == 'True') {


                        $.ajax({
                            url: '/Facilities/GetFacilityUsersRoles',
                            type: 'POST',
                            data: jQuery.param({ FacilityID: rowData1.FacilityID, UserID: rowData2.UserID }),
                            success: function (data) {

                                debugger;
                                var dataObject = JSON.parse(data)

                                var dialog = document.getElementById("ExternalDialog2").ej2_instances[0];
                                dialog.show();
                                var childGridElement2 = document.getElementById("childGrid2");

                                if (childGridElement2 && childGridElement2.classList.contains("e-grid")) {
                                    childGridElement2.ej2_instances[0].destroy();
                                }

                                gridInstance2 = new ej.grids.Grid({
                                    allowPaging: true,

                                    dataSource: new ej.data.DataManager({
                                        url: "/Facilities/UrlDatasource3",
                                        insertUrl: "/Facilities/Insert3",
                                        updateUrl: "/Facilities/Update3",
                                        removeUrl: "/Facilities/Delete3",
                                        adaptor: new ej.data.UrlAdaptor()
                                        //.Query().addParams("UserID",FacilityGroup.FacilityGroupID)
                                    }),

                                    actionComplete: (e) => {
                                        if (e.requestType == 'save') {
                                            gridInstance2.refresh();
                                        }
                                        if (e.requestType == 'update') {

                                        }
                                    },


                                    //                                                                                                                                                                                                                                                                                col.Field("DateCreated").HeaderText("Date Created").HeaderTemplate("#datetemplate").Format("yMd").Width("160").AllowEditing(False).Add()
                                    //                                                                                                                                                                                                                                                                                col.Field("InActive").HeaderText("InActive").EditType("booleanedit").DisplayAsCheckBox(True).Type("boolean").Width("150").Add()
                                    //col.Field("JobTitleID").HeaderText("Title").ValueAccessor("DisplayJobTitleDescription").EditType("dropdownedit").Edit(New With {.params = JobTitleList }).Width("150").Add()

                                    height: 165,
                                    width: 300,
                                    allowSorting: true,
                                    allowFiltering: true,
                                    allowResizing: true,
                                    filterSettings: { type: "Excel" },
                                    rowDataBound: rowBound,
                                    editSettings: { allowAdding: true, allowEditing: true, allowDeleting: true, mode: "Normal" },
                                    toolbar: ["Add", "Edit", "Update", "Delete","Cancel"],
                                    columns: [
                                        { field: "FacilityUserJobID", headerText: "FacilityUserJobID", width: 80, isPrimaryKey: true, visible: false },
                                        //{ field: "JobTitleID", headerText: "JobTitleID", width: 150 },
                                        {
                                            field: "JobTitleID", headerText: "Title", width: 150, valueAccessor: "DisplayJobTitleDescription", editType: "dropdownedit",
                                            edit: {
                                                params: {
                                                    query: new ej.data.Query(),
                                                    dataSource: JobTitlesDropDownList,
                                                    fields: { value: 'value', text: 'text' },
                                                    allowFiltering: true
                                                }
                                            }
                                        }
                                    ]
                                })
                                gridInstance2.appendTo("#childGrid2");
                            }
                        });
                    };
                });


        };
    }



    function rowBound(args) {
        if (args.data.JobTitleID == '51') {
            args.row.classList.add('Owner')
        }
        if (args.data.JobTitleID == '50') {
            args.row.classList.add('Operator')
        }

        if (args.data.JobTitleID == '67') {
            args.row.classList.add('VPofOperations')
        }

        if (args.data.JobTitleID == '55') {
            args.row.classList.add('RegionalAdministrator')
        }

        if (args.data.JobTitleID == '3') {
            args.row.classList.add('Administrator')
        }

        if (args.data.JobTitleID == '127') {
            args.row.classList.add('DON')
        }

        if (args.data.JobTitleID == '19') {
            args.row.classList.add('ComplianceOfficer')
        }

        if (args.data.JobTitleID == '41') {
            args.row.classList.add('In-ServiceCoordinator')
        }
    }

    function DisplayJobTitleDescription(field, data, column) {
        var coldata = column.edit.params.dataSource;

        for (var i = 0; i < coldata.length; i++) {
            if (data.JobTitleID == coldata[i]['value'])
                return coldata[i]['text'];
        }
    }


    function customFn(args) {
        let FacilitUserEmail = FacilitUserEmails.find(g => g.toLowerCase() === args['value'].toLowerCase());

        if (EditMode == 'Edit') {
            if (FacilitUserEmail == undefined) { return true };
            if (rowData2.EmailAddress == FacilitUserEmail) {return true };
            if (rowData2.EmailAddress !== FacilitUserEmail) { return true};
        };

        if (EditMode == 'Add') {
            if (FacilitUserEmail == undefined) { return true };
            if (rowData2.EmailAddress == FacilitUserEmail) { return false };
        };

        //return true

    }


    //function load(args) {
    //    this.columns[1].validationRules = { required: true, minLength: [customFn, 'Need atleast 5 letters'] };
    //}

    //function customFn(args) {
    //    return args['value'].length >= 5;
    //}




    function Created() {
        this.target = ".e-gridcontent";
    }
</script>



<style>
    .Administrator {
        color: red;
        font-weight: bold;
    }

    .Owner {
        color: red;
        font-weight: bold;
    }

    .Operator {
        color: red;
        font-weight: bold;
    }

    .VPofOperations {
        color: red;
        font-weight: bold;
    }

    .RegionalAdministrator {
        color: red;
        font-weight: bold;
    }

    .DON {
        color: red;
        font-weight: bold;
    }

    .ComplianceOfficer {
        color: red;
        font-weight: bold;
    }

    .In-ServiceCoordinator {
        color: red;
        font-weight: bold;
    }
</style>
<style>
    input[type=radio] {
        transform: scale(1.5);
    }
</style>
