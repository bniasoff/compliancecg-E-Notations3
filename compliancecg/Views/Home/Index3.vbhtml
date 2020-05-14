@Imports Syncfusion.EJ2
@imports Syncfusion.EJ2.Grids
@imports  Syncfusion.EJ2.DropDowns
@imports  Syncfusion.EJ2.Popups
@Imports CCGData.CCGData.Facilitydialogtemplate


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




@Code
    Dim commands As List(Of Object) = New List(Of Object)()
    commands.Add(New With {.Type = "Edit", .buttonOption = New With {.iconCss = "e-icons e-edit", .cssClass = "e-flat"}})
    commands.Add(New With {.Type = "Delete", .buttonOption = New With {.iconCss = "e-icons e-delete", .cssClass = "e-flat"}})
    commands.Add(New With {.Type = "Save", .buttonOption = New With {.iconCss = "e-icons e-update", .cssClass = "e-flat"}})
    commands.Add(New With {.Type = "Cancel", .buttonOption = New With {.iconCss = "e-icons e-cancel-icon", .cssClass = "e-flat"}})

    Dim commands2 As List(Of Object) = New List(Of Object)()
    commands2.Add(New With {.type = "userstatus", .buttonOption = New With {.content = "Users", .cssClass = "e-flat"}})

    Dim commands3 As List(Of Object) = New List(Of Object)()
    commands3.Add(New With {.type = "userstatus", .buttonOption = New With {.content = "Roles", .cssClass = "e-flat"}})
End code


<div id='container' style="height:1000px;" class="control">
    <div style="margin-left:10px;margin-top:20px;">
        <div><h4>Facilities</h4></div>
        @Html.EJS().Grid("Grid").DataSource(Function(DataManager) DataManager.Url("/Facilities/UrlDatasource").InsertUrl("/Facilities/Insert").UpdateUrl("/Facilities/Update").RemoveUrl("/Facilities/Delete").Adaptor("UrlAdaptor")).Height("300").Width("1200").Columns(Sub(col)
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
                                                                                                                                                                                                                                                                           End Sub).Load("load").SelectionSettings(Sub(selection) selection.Type(Syncfusion.EJ2.Grids.SelectionType.Single)).AllowSorting().AllowFiltering().AllowGrouping().AllowResizing(True).AllowPaging().PageSettings(Sub(page) page.PageCount(2)).EditSettings(Sub(edit) edit.AllowAdding(True).AllowEditing(True).AllowDeleting(True).Mode(Syncfusion.EJ2.Grids.EditMode.Dialog)).FilterSettings(Sub(Filter) Filter.Type(Syncfusion.EJ2.Grids.FilterType.Excel)).Toolbar(New List(Of String)(New String() {"Add", "Edit", "Delete", "Update", "Cancel"})).Render()
    </div>
</div>

  

    @*<div>
        @Html.EJS().Dialog("dialog").Visible(False).AnimationSettings(New DialogAnimationSettings() With {.Effect = DialogEffect.Zoom}).Header("Facility Users").Open("dialogOpen").Close("dialogClose").Position(Function(obj) obj.X("center").Y("center")).IsModal(False).AllowDragging(True).ShowCloseIcon(True).CloseOnEscape(False).Width("1000").Height("600").Target("#container").ContentTemplate(@@<div class='dialog-content'>
            <div class='msg-wrapper  col-lg-12'>
                <div>
                    {@Html.EJS().Grid("Grid4").RowDataBound("bound").DataSource(Function(DataManager) DataManager.Url("/Facilities/UrlDatasource4").InsertUrl("/Facilities/Insert4").UpdateUrl("/Facilities/Update4").RemoveUrl("/Facilities/Delete4").Adaptor("UrlAdaptor")).Height("300").Width("900").Columns(Sub(col)
                            col.HeaderText("Roles").Width("90").Commands(commands3).Add()
                            col.Field("UserID").HeaderText("UserID").IsPrimaryKey(True).Visible(False).Add()
                            col.Field("LastName").HeaderText("Last Name").Width("150").AllowEditing(True).Add()
                            col.Field("FirstName").HeaderText("First Name").Width("150").Add()
                            col.Field("EmailAddress").HeaderText("Email Address").Width("250").Add()
                            col.Field("Phone1").HeaderText("Phone1").Width("150").Add()
                            col.Field("Phone2").HeaderText("Phone2").Width("150").Add()
                            col.Field("DateCreated").HeaderText("Date Created").HeaderTemplate("#datetemplate").Format("yMd").Width("160").AllowEditing(False).Add()
                            col.Field("InActive").HeaderText("InActive").EditType("booleanedit").DisplayAsCheckBox(True).Type("boolean").Width("150").Add()
                        End Sub).Load("load2").ActionComplete("actionComplete4").ActionBegin("actionBegin4").SelectionSettings(Sub(selection) selection.Type(Syncfusion.EJ2.Grids.SelectionType.Single)).AllowSorting().AllowFiltering().AllowGrouping().AllowResizing(True).AllowPaging().PageSettings(Sub(page) page.PageCount(2)).EditSettings(Sub(edit) edit.AllowAdding(True).AllowEditing(True).AllowDeleting(True).Mode(Syncfusion.EJ2.Grids.EditMode.Dialog)).FilterSettings(Sub(Filter) Filter.Type(Syncfusion.EJ2.Grids.FilterType.Excel)).Toolbar(New List(Of String)(New String() {"Add", "Edit", "Delete", "Update", "Cancel"})).Render()}
                </div>
            </div>
        </div>).Render()
    </div>*@



    <script>
        function dialogClose() {
            //document.getElementById('normalbtn').style.display = 'inline-block';
        }
        function dialogOpen() {
            //document.getElementById('normalbtn').style.display = 'none';
        }

        function dialogClose2() {
            //document.getElementById('normalbtn').style.display = 'inline-block';
        }
        function dialogOpen2() {
            //document.getElementById('normalbtn').style.display = 'none';
        }
        //document.getElementById('normalbtn').onclick = function () {
        //    var dialogObj = document.getElementById('default_dialog').ej2_instances[0];
        //    dialogObj.show();
        //};
        function dlgButtonClick() {
            //window.open('https://www.syncfusion.com/company/about-us');
        }

        function bound(args) {
            //if (args.data.JobTitleID == '51') {
            //    args.row.classList.add('Owner')
            //}
            //if (args.data.JobTitleID == '50') {
            //    args.row.classList.add('Operator')
            //}

            //if (args.data.JobTitleID == '67') {
            //    args.row.classList.add('VPofOperations')
            //}

            //if (args.data.JobTitleID == '55') {
            //    args.row.classList.add('RegionalAdministrator')
            //}

            //if (args.data.JobTitleID == '3') {
            //    args.row.classList.add('Administrator')
            //}

            //if (args.data.JobTitleID == '127') {
            //    args.row.classList.add('DON')
            //}

            //if (args.data.JobTitleID == '19') {
            //    args.row.classList.add('ComplianceOfficer')
            //}

            //if (args.data.JobTitleID == '41') {
            //    args.row.classList.add('In-ServiceCoordinator')
            //}
        }
    </script>


    <script>

       

          


            function load() {

                //this.columns[0].commands[0].buttonOption.click = function (args) {
                //    debugger;
                //    var dialogObj = document.getElementById('dialog').ej2_instances[0];
                //    dialogObj.show();

                //    var row = new ej.base.closest(event.target, '.e-row');
                //    var index = row.getAttribute('aria-rowindex')
                //    var grid = document.getElementById('Grid').ej2_instances[0];
                //    var rowData = grid.currentViewData[index];
                //    var request = JSON.stringify(rowData);


                //    $.post('/Facilities/SetFacilitySessionValues', request)
                //        .done(function (data) {
                //            if (data.length = 'True') {

                //                var grid4 = document.getElementById("Grid4").ej2_instances[0];
                //                grid4.dataSource = new ej.data.DataManager({
                //                    url: "/Facilities/UrlDatasource4",
                //                    insertUrl: "/Facilities/Insert4",
                //                    updateUrl: "/Facilities/Update4",
                //                    removeUrl: "/Facilities/Delete4",
                //                    adaptor: new ej.data.UrlAdaptor()
                //                    //.Query().addParams("UserID",FacilityGroup.FacilityGroupID)
                //                });


                //            }
                //        });

                //    //CheckUserRole();
                //}
            }



            function load2() {
                //this.columns[4].validationRules = { email: true, minLength: [customFn, 'Duplicate Email'] };

                //this.columns[0].commands[0].buttonOption.click = function (args) {


                //    var dialogObj = document.getElementById('dialog2').ej2_instances[0];
                //    dialogObj.show();

                //    var row = new ej.base.closest(event.target, '.e-row');
                //    var index = row.getAttribute('aria-rowindex')
                //    var grid = document.getElementById('Grid4').ej2_instances[0];
                //    var rowData = grid.currentViewData[index];
                //    var request = JSON.stringify(rowData);

                //    $.post('/Facilities/SetUserSessionValues', request)
                //        .done(function (data) {
                //            if (data.length = 'True') {
                //                var grid2 = document.getElementById("Grid2").ej2_instances[0];
                //                grid2.dataSource = new ej.data.DataManager({
                //                    url: "/Facilities/UrlDatasource3",
                //                    insertUrl: "/Facilities/Insert3",
                //                    updateUrl: "/Facilities/Update3",
                //                    removeUrl: "/Facilities/Delete3",
                //                    adaptor: new ej.data.UrlAdaptor()
                //                    //.Query().addParams("FacilityID",FacilityGroup.FacilityGroupID)
                //                });
                //            }
                //        });

                //}
            }



            
    </script>


