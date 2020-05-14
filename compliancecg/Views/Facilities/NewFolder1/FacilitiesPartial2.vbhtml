@Imports Syncfusion.EJ2
@imports Syncfusion.EJ2.Grids
@imports  Syncfusion.EJ2.DropDowns
@imports  Syncfusion.EJ2.Popups
@Imports CCGData.CCGData.Facility


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

    Dim commands4 As List(Of Object) = New List(Of Object)()
    commands4.Add(New With {.type = "userstatus", .buttonOption = New With {.content = "Password", .cssClass = "e-flat"}})
End code

<script>


</script>






<div style="margin-left:10px;margin-top:20px;">
    <div><h4>Facilities</h4></div>
    @Html.EJS().Grid("Grid").DataSource(Function(DataManager) DataManager.Url("/Facilities/UrlDatasource").InsertUrl("/Facilities/Insert").UpdateUrl("/Facilities/Update").RemoveUrl("/Facilities/Delete").Adaptor("UrlAdaptor")).Height("300").Width("1300").Columns(Sub(col)
                                                                                                                                                                                                                                                                           col.HeaderText("Users").Width("80").Commands(commands2).Add()
                                                                                                                                                                                                                                                                           col.Field("FacilityID").HeaderText("FacilityID").IsPrimaryKey(True).Visible(False).Add()
                                                                                                                                                                                                                                                                           col.Field("Name").HeaderText("Name").Width("200").ValidationRules(New With {.required = True, .minLength = 3}).Add()
                                                                                                                                                                                                                                                                           col.Field("Address").HeaderText("Address").Width("200").Add()
                                                                                                                                                                                                                                                                           col.Field("City").HeaderText("City").Width("120").Add()
                                                                                                                                                                                                                                                                           col.Field("State").HeaderText("State").Width("110").Add()
                                                                                                                                                                                                                                                                           col.Field("Zip").HeaderText("Zip").Width("90").Add()
                                                                                                                                                                                                                                                                           col.Field("Phone1").HeaderText("Phone1").Width("115").Add()
                                                                                                                                                                                                                                                                           col.Field("Phone2").HeaderText("Phone2").Width("115").Add()
                                                                                                                                                                                                                                                                           col.Field("Beds").HeaderText("Beds").Width("75").Add()
                                                                                                                                                                                                                                                                           col.Field("ShiftChange").HeaderText("ShfChnge").Width("75").Add()
                                                                                                                                                                                                                                                                           col.Field("InActive").HeaderText("InActive").EditType("booleanedit").DisplayAsCheckBox(True).Type("boolean").Width("100").Add()
                                                                                                                                                                                                                                                                       End Sub).Load("load").ActionComplete("actionComplete").ActionBegin("actionBegin").SelectionSettings(Sub(selection) selection.Type(Syncfusion.EJ2.Grids.SelectionType.Single)).AllowSorting().AllowFiltering().AllowGrouping().AllowResizing(True).AllowPaging().PageSettings(Sub(page) page.PageCount(2)).EditSettings(Sub(edit) edit.AllowAdding(True).AllowEditing(True).AllowDeleting(True).Mode(Syncfusion.EJ2.Grids.EditMode.Dialog)).FilterSettings(Sub(Filter) Filter.Type(Syncfusion.EJ2.Grids.FilterType.Excel)).Toolbar(New List(Of String)(New String() {"Add", "Edit", "Delete", "Update", "Cancel"})).Render()
</div>


<script>

    function actionBegin(args) {
        if (args.requestType === 'save') {
        }
    }

    function actionComplete(args) {
        if (args.requestType === 'beginEdit') {

            //    debugger;
            //     args.model.query.addParams("ID", 10248);
            //var ajax = new ej.base.Ajax({
            //    url: "/Home/Editpartial", //render the partial view
            //    type: "POST",
            //    contentType: "application/json",
            //    data: JSON.stringify({ value: args.rowData })
            //});
            //ajax.send().then(function (data) {
            // //   $("#dialogTemp").html(data); //Render the edit form with selected record
            // //   args.form.elements.namedItem('CustomerID').focus();
            //}).catch(function (xhr) {
            //    console.log(xhr);
            //});
        }

        if (args.requestType === 'add') {
            // debugger;
            //args.data.FacilityGroupID = FacilityGroup2.FacilityGroupID;


            //var ajax = new ej.base.Ajax({
            //    url: "/Home/Addpartial", //render the partial view
            //    type: "POST",
            //    contentType: "application/json",
            //});
            //ajax.send().then(function (data) {
            // //   $("#dialogTemp").html(data); //Render the edit form with selected record
            ////    args.form.elements.namedItem('OrderID').focus();
            //}).catch(function (xhr) {
            //    console.log(xhr);
            //});
        }

        if (args.requestType === 'save') {
            var grid = document.getElementById("Grid").ej2_instances[0];
            grid.refresh();
        }

    }


    function load() {
        this.columns[0].commands[0].buttonOption.click = function (args) {

            //var dialogObj = document.getElementById('dialog').ej2_instances[0];
            //dialogObj.show();

            //var row = new ej.base.closest(event.target, '.e-row');
            //var index = row.getAttribute('aria-rowindex')
            //var grid = document.getElementById('Grid').ej2_instances[0];
            //var rowData = grid.currentViewData[index];
            //var request = JSON.stringify(rowData);


            //$.post('/Facilities/SetFacilitySessionValues', request)
            //    .done(function (data) {
            //        if (data.length = 'True') {

            //            var grid4 = document.getElementById("Grid4").ej2_instances[0];
            //            grid4.dataSource = new ej.data.DataManager({
            //                url: "/Facilities/UrlDatasource4",
            //                insertUrl: "/Facilities/Insert4",
            //                updateUrl: "/Facilities/Update4",
            //                removeUrl: "/Facilities/Delete4",
            //                adaptor: new ej.data.UrlAdaptor()
            //                //.Query().addParams("UserID",FacilityGroup.FacilityGroupID)
            //            });
            //            // CheckUserRole();
            //        };
                //});
        };
           };

</script>




<div>
    @Html.EJS().Dialog("dialog").Visible(False).AnimationSettings(New DialogAnimationSettings() With {.Effect = DialogEffect.Zoom}).Header("Facility Users").Open("dialogOpen").Close("dialogClose").Position(Function(obj) obj.X("center").Y("center")).IsModal(False).AllowDragging(True).ShowCloseIcon(True).CloseOnEscape(False).Width("1270").Height("600").Target("#Grid1").Buttons(Sub(btn) btn.Click("dlgButtonClick").ButtonModel(ViewBag.button1).Add()).ContentTemplate(@@<div class='dialog-content'>
        <div class='msg-wrapper  col-lg-12'>
            <div>
                {@Html.EJS().Grid("Grid4").RowDataBound("bound").DataSource(Function(DataManager) DataManager.Url("/Facilities/UrlDatasource4").InsertUrl("/Facilities/Insert4").UpdateUrl("/Facilities/Update4").RemoveUrl("/Facilities/Delete4").Adaptor("UrlAdaptor")).Height("300").Width("1220").Columns(Sub(col)
                                                                                                                                                                                                                                                                                                            col.HeaderText("Roles").Width("90").Commands(commands3).Add()
                                                                                                                                                                                                                                                                                                            'col.HeaderText("Password").Width("120").Commands(commands4).Add()
                                                                                                                                                                                                                                                                                                            col.Field("UserID").HeaderText("UserID").IsPrimaryKey(True).Visible(False).Add()
                                                                                                                                                                                                                                                                                                            col.Field("LastName").HeaderText("Last Name").Width("150").AllowEditing(True).Add()
                                                                                                                                                                                                                                                                                                            col.Field("FirstName").HeaderText("First Name").Width("150").Add()
                                                                                                                                                                                                                                                                                                            col.Field("EmailAddress").HeaderText("Email Address").Width("250").Add()
                                                                                                                                                                                                                                                                                                            col.Field("Phone1").HeaderText("Phone1").Width("150").Add()
                                                                                                                                                                                                                                                                                                            'col.Field("Phone2").HeaderText("Phone2").Width("150").Add()
                                                                                                                                                                                                                                                                                                            col.Field("DateCreated").HeaderText("Date Created").HeaderTemplate("#datetemplate").Format("yMd").Width("160").AllowEditing(False).Add()
                                                                                                                                                                                                                                                                                                            col.Field("InActive").HeaderText("InActive").EditType("booleanedit").DisplayAsCheckBox(True).Type("boolean").Width("120").Add()
                                                                                                                                                                                                                                                                                                        End Sub).Load("load2").ActionComplete("actionComplete4").ActionBegin("actionBegin4").SelectionSettings(Sub(selection) selection.Type(Syncfusion.EJ2.Grids.SelectionType.Single)).AllowSorting().AllowFiltering().AllowGrouping().AllowResizing(True).AllowPaging().PageSettings(Sub(page) page.PageCount(2)).EditSettings(Sub(edit) edit.AllowAdding(True).AllowEditing(True).AllowDeleting(True).Mode(Syncfusion.EJ2.Grids.EditMode.Dialog)).FilterSettings(Sub(Filter) Filter.Type(Syncfusion.EJ2.Grids.FilterType.Excel)).Toolbar(New List(Of String)(New String() {"Add", "Edit", "Delete", "Update", "Cancel"})).Render()}
            </div>
        </div>
    </div>).Render()
</div>



<script>
        function bound(args) {
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


        function actionBegin4(args) {
                            if (args.requestType === 'save') {
                            }
                        }


        function actionComplete4(args) {

                            if (args.requestType === 'beginEdit') {
                                EditMode = 'Edit'
                                rowData = args.rowData

                                //var ajax = new ej.base.Ajax({
                                //    url: "/Home/Editpartial", //render the partial view
                                //    type: "POST",
                                //    contentType: "application/json",
                                //    data: JSON.stringify({ value: args.rowData })
                                //});
                                //ajax.send().then(function (data) {
                                //    $("#dialogTemp").html(data); //Render the edit form with selected record
                                //    args.form.elements.namedItem('CustomerID').focus();
                                //}).catch(function (xhr) {
                                //    console.log(xhr);
                                //});
                            }
                            if (args.requestType === 'add') {
                                EditMode = 'Add'
                                //var ajax = new ej.base.Ajax({
                                //    url: "/Home/Addpartial", //render the partial view
                                //    type: "POST",
                                //    contentType: "application/json",
                                //});
                                //ajax.send().then(function (data) {
                                //    $("#dialogTemp").html(data); //Render the edit form with selected record
                                //    args.form.elements.namedItem('OrderID').focus();
                                //}).catch(function (xhr) {
                                //    console.log(xhr);
                                //});
                            }
                        }



        function load2() {
                            this.columns[4].validationRules = { email: true, minLength: [customFn, 'Duplicate Email'] };

                            this.columns[0].commands[0].buttonOption.click = function (args) {
                                debugger;
                                //var dialogObj = document.getElementById('dialog2').ej2_instances[0];
                                //dialogObj.show();

                                var row = new ej.base.closest(event.target, '.e-row');
                                var index = row.getAttribute('aria-rowindex')
                                var grid = document.getElementById('Grid4').ej2_instances[0];
                                var rowData = grid.currentViewData[index];
                                var request = JSON.stringify(rowData);

                                $.post('/Facilities/SetUserSessionValues', request)
                                    .done(function (data) {
                                        if (data.length = 'True') {
                                            var grid2 = document.getElementById("Grid2").ej2_instances[0];
                                            grid2.dataSource = new ej.data.DataManager({
                                                url: "/Facilities/UrlDatasource3",
                                                insertUrl: "/Facilities/Insert3",
                                                updateUrl: "/Facilities/Update3",
                                                removeUrl: "/Facilities/Delete3",
                                                adaptor: new ej.data.UrlAdaptor()
                                                //.Query().addParams("FacilityID",FacilityGroup.FacilityGroupID)
                                            });
                                        }
                                    });

                            }

                            // Password Dialoge Column
                            //var rowData2
                            //this.columns[1].commands[0].buttonOption.click = function (args) {
                            //    var row = new ej.base.closest(event.target, '.e-row');
                            //    var index = row.getAttribute('aria-rowindex')
                            //    var grid = document.getElementById('Grid4').ej2_instances[0];
                            //    var rowData = grid.currentViewData[index];
                            //    var request = JSON.stringify(rowData);
                            //    rowData2 = rowData

                            //    var dialogObj = document.getElementById('dialog3').ej2_instances[0];
                            //    dialogObj.show();

                            //    document.getElementById('Reveal').onclick = function () {
                            //        var textObj = document.getElementById("Password").ej2_instances[0];
                            //        //var togglebtn = document.getElementById("Reveal");
                            //        var request = JSON.stringify(rowData2);

                            //        var Email = rowData2.EmailAddress
                            //        var Password=textObj.value

                            //        $.ajax({
                            //            url: "/Account/Register2",
                            //            type: "POST",
                            //            data: { Email: Email , Password :Password} ,
                            //            error: function (response) {
                            //                alert(response);
                            //            },
                            //            success: function (response) {
                            //                var response2 = JSON.parse(response)

                            //        //            ////$("li").each(function () {
                            //        //            ////    $(this).addClass("foo");
                            //        //            ////});


                            //                    var trHTML = '';
                            //                    $.each(response2, function (i, item) {
                            //                        trHTML += '<tr><td><font color="red">' + item + '</font></td></tr></tr>';
                            //                    });
                            //                    $('#records_table').append(trHTML);
                            //                //alert(response);
                            //            }
                            //        });

                            //        return false;


                            //        //if (textObj.type == 'Password') {
                            //        //    textObj.type = "Text";
                            //        //    togglebtn.textContent = 'Hide';
                            //        //} else {
                            //        //    textObj.type = "Password";
                            //        //    togglebtn.textContent = 'Show';
                            //        //}
                            //    };



                            //}
                        }

</script>



<div>
    @Html.EJS().Dialog("dialog2").Visible(False).AnimationSettings(New DialogAnimationSettings() With {.Effect = DialogEffect.Zoom}).Header("Facility Role").Open("dialogOpen2").Close("dialogClose2").Position(Function(obj) obj.X("center").Y("center")).IsModal(False).AllowDragging(True).ShowCloseIcon(True).CloseOnEscape(False).Width("400").Height("800").Target("#Grid4").Buttons(Sub(btn) btn.Click("dlgButtonClick").ButtonModel(ViewBag.button1).Add()).ContentTemplate(@@<div class='dialog-content'>
        <div class='msg-wrapper  col-lg-12'>
            <div>
                {@Html.EJS().Grid("Grid2").RowDataBound("bound").DataSource(Function(DataManager) DataManager.Url("/Facilities/UrlDatasource3").InsertUrl("/Facilities/Insert3").UpdateUrl("/Facilities/Update3").RemoveUrl("/Facilities/Delete3").Adaptor("UrlAdaptor")).Height("200").Width("300").Columns(Sub(col)
                                                                                                                                                                                                                                                                                                              col.Field("FacilityUserJobID").HeaderText("FacilityUserJobID").IsPrimaryKey(True).Visible(False).Add()
                                                                                                                                                                                                                                                                                                              col.Field("JobTitleID").HeaderText("Title").ValueAccessor("DisplayJobTitleDescription").EditType("dropdownedit").Edit(New With {.params = JobTitleList}).Width("150").Add()
                                                                                                                                                                                                                                                                                                          End Sub).ActionComplete("actionComplete3").ActionBegin("actionBegin3").SelectionSettings(Sub(selection) selection.Type(Syncfusion.EJ2.Grids.SelectionType.Single)).AllowSorting().AllowFiltering().AllowResizing(True).EditSettings(Sub(edit) edit.AllowAdding(True).AllowEditing(True).AllowDeleting(True).Mode(Syncfusion.EJ2.Grids.EditMode.Dialog)).FilterSettings(Sub(Filter) Filter.Type(Syncfusion.EJ2.Grids.FilterType.Excel)).Toolbar(New List(Of String)(New String() {"Add", "Edit", "Delete", "Update", "Cancel"})).Render()}
            </div>
        </div>
    </div>).Render()
</div>


<script>
        function actionBegin3(args) {

                            if (args.requestType === 'save') {
                            }
                        }
        function actionComplete3(args) {
                            if (args.requestType === 'beginEdit') {
                            }

                            if (args.requestType === 'add') {
                            }

                            if (args.requestType === 'save') {
                                var grid = document.getElementById("Grid2").ej2_instances[0];
                                grid.refresh();
                            }

                        }
</script>





@*<div>
        @Html.EJS().Dialog("dialog3").Visible(False).AnimationSettings(New DialogAnimationSettings() With {.Effect = DialogEffect.Zoom}).Header("Password").Open("dialogOpen3").Close("dialogClose3").Position(Function(obj) obj.X("center").Y("center")).IsModal(False).AllowDragging(True).ShowCloseIcon(True).CloseOnEscape(False).Width("600").Height("300").Target("#Grid4").Buttons(Sub(btn) btn.Click("dlgButtonClick").ButtonModel(ViewBag.button1).Add()).ContentTemplate(@@<div class='dialog-content'>
            <div class='msg-wrapper  col-lg-12'>
                <div>
                    <table id="records_table" border='1'>
                        <tr>
                        </tr>
                    </table>
                </div>

                <div style="width:200px">
                    {@Html.EJS().TextBox("Password").Placeholder("Password").FloatLabelType(Inputs.FloatLabelType.Auto).Type("Password").Render()}
                    <button id="Reveal">Register</button>
                </div>
            </div>
        </div>).Render()
    </div>*@





<script>
            var grid = document.getElementById("Grid");
                grid.addEventListener("click", getInfo);

                function getInfo(args) {

                    if (args.target.classList.contains("e-rowcell")) {
                        var rowInfo = this.ej2_instances[0].getRowInfo(args.target);
                        var rowData = rowInfo.rowData
                        var request = JSON.stringify(rowData);

                        $.post('/Facilities/SetFacilitySessionValues', request)
                            .done(function (data) {
                                if (data.length = 'True') {
                                    CheckUserRole();
                                }
                            });





                    }
                }


                function DisplayUserIDDescription(field, data, column) {
                    var coldata = column.edit.params.dataSource;
                    for (var i = 0; i < coldata.length; i++) {
                        if (data.UserID == coldata[i]['value'])
                            return coldata[i]['text'];
                    }
                }



                function actionBegin2(args) {

                    if (args.requestType === 'save') {
                    }
                }


                function actionComplete2(args) {
                    if (args.requestType === 'beginEdit') {
                    }

                    if (args.requestType === 'add') {
                    }

                    if (args.requestType === 'save') {
                        var grid = document.getElementById("Grid3").ej2_instances[0];
                        grid.refresh();
                    }

                }

</script>

@*@Html.EJS().Dialog("dialog").Visible(False).AnimationSettings(New DialogAnimationSettings() With {.Effect = DialogEffect.Zoom}).Header("About SYNCFUSION Succinctly Series").Open("dialogOpen").Close("dialogClose").Content("<p>In the Succinctly series, Syncfusion created a robust, free library of more than 130 technical e - books formatted for PDF, Kindle, and EPUB.The Succinctly series was born in 2012 out of a desire to provide concise technical e-books for software developers.Each title in the Succinctly series is written by a carefully chosen expert and provides essential content in about 100 pages.</p>").Position(Function(obj) obj.X("center").Y("center")).IsModal(True).AllowDragging(True).EnableRtl(True).ShowCloseIcon(True).CloseOnEscape(False).Width("1100").Height("800").Target("#Grid1").Buttons(ViewBag.DefaultButtons).Render()*@

@*col.Field("OrderDate").HeaderText("Ship Name").Format("yMd").Edit(new { create = "create", read = "read", destroy = "destroy", write = "write" }).Width("150").Add();

    <script>
        var elem;
        var dObj;
        function create(args) {
            elem = document.createElement('input');
            return elem;
        }
        function write(args) {
            dObj = new ej.calendars.DatePicker({
                value: new Date(args.rowData[args.column.field]),
                placeholder: 'Select DateTime'
            });
            dObj.appendTo(elem);
        }

        function destroy() {
            dObj.destroy();
        }
        function read(args) {
            return dObj.value;
        }
    </script>*@