@Imports Syncfusion.EJ2
@imports Syncfusion.EJ2.Grids
@imports  Syncfusion.EJ2.DropDowns
@Imports CCGData.CCGData.Facility


@Code
    ViewData("Title") = "Search"
End Code

@Code
    Dim commands2 As List(Of Object) = New List(Of Object)()
    commands2.Add(New With {.type = "userstatus", .buttonOption = New With {.content = "Users", .cssClass = "e-flat"}})

    Dim commands3 As List(Of Object) = New List(Of Object)()
    commands3.Add(New With {.type = "userstatus", .buttonOption = New With {.content = "Roles", .cssClass = "e-flat"}})
End code


<input type="hidden" id="jsonFacilities" data-value="@ViewBag.jsonFacilities" />
<input type="hidden" id="jsonJobTitles" data-value="@ViewBag.jsonJobTitles" />


<div style="clear:left;margin:0px">
    <div style='float:left;margin-right:30px;margin-bottom: 0px;' , class="control-wrapper">
        <div style='margin-bottom: 0px;'>
            <span class="label label-info">Users</span>
        </div>
        <div id="default" style='padding-top:10px;'>
            @Html.EJS().ComboBox("Users").Placeholder("Select User").PopupHeight("230px").ShowClearButton().Width("250").Autofill(True).AllowCustom(True).Change("valueChange").DataSource(ViewBag.jsonUsers).Fields(New Syncfusion.EJ2.DropDowns.ComboBoxFieldSettings With {.Text = "LastName", .Value = "UserID"}).ItemTemplate("<div><div class=\""detail1\""'> ${LastName} </div><div class=\""detail1 detail2\""> ${FirstName} </div></div>").HeaderTemplate("<div><div class=\""header1\"">Last Name</div><div class=\""header1 header2\"">First Name</div></div>)").Render()
        </div>
    </div>

    <div style='float:left;margin-right:30px;margin-bottom: 50px;' , class="control-wrapper">
        <div style='margin-bottom: 0px;'>
            <span class="label label-info">States</span>
        </div>
        <div id="default" style='padding-top:10px;'>
            @Html.EJS().ComboBox("States").Placeholder("Select State").PopupHeight("230px").ShowClearButton().Width("150").Autofill(True).AllowCustom(True).Change("valueChange").DataSource(ViewBag.jsonStates).Fields(New Syncfusion.EJ2.DropDowns.ComboBoxFieldSettings With {.Text = "State", .Value = "State"}).Render()
        </div>
    </div>

    <div style='float:left;margin-right:30px;margin-bottom: 50px;' , class="control-wrapper">
        <div style='margin-bottom: 0px;'>
            <span class="label label-info">Titles</span>
        </div>
        <div id="default" style='padding-top:10px;'>
            @*@Html.EJS().ComboBox("Titles").Placeholder("Select Title").PopupHeight("230px").ShowClearButton().Width("250").Autofill(True).AllowCustom(True).Change("valueChange3").DataSource(ViewBag.jsonJobTitles).Fields(New Syncfusion.EJ2.DropDowns.ComboBoxFieldSettings With {.Text = "Title", .Value = "JobTitleID"}).ItemTemplate("<div><div class=\""detail1\""'> ${Title} </div><div class=\""detail1 detail2\""> ${JobTitleID} </div></div>").HeaderTemplate("<div><div class=\""header1\"">Title</div><div class=\""header1 header2\"">JobTitleID</div></div>)").Render()*@
            @Html.EJS().ComboBox("Titles").Placeholder("Select Title").PopupHeight("230px").ShowClearButton().Width("250").Autofill(True).AllowCustom(True).Change("valueChange").DataSource(ViewBag.jsonJobTitles).Fields(New Syncfusion.EJ2.DropDowns.ComboBoxFieldSettings With {.Text = "Title", .Value = "JobTitleID"}).Render()
        </div>
    </div>


    <div style='float:left;margin-right:30px;margin-bottom: 50px;' , class="control-wrapper">
        <div style='margin-bottom: 0px;'>
            <span class="label label-info">Groups</span>
        </div>
        <div id="default" style='padding-top:10px;'>
            @Html.EJS().ComboBox("FacilityGroups").Placeholder("Select Group").PopupHeight("230px").ShowClearButton().Width("250").Autofill(True).AllowCustom(True).Change("valueChange").DataSource(ViewBag.jsonFacilityGroups).Fields(New Syncfusion.EJ2.DropDowns.ComboBoxFieldSettings With {.Text = "GroupName", .Value = "FacilityGroupID"}).Render()
        </div>
    </div>


    <div style='margin-right:30px;margin-bottom: 50px;' , class="control-wrapper">
        <div style='margin-bottom: 0px;'>
            <span class="label label-info">Facilities</span>
        </div>
        <div id="default" style='padding-top:10px;'>
            @Html.EJS().ComboBox("Facilities").Placeholder("Select Facility").PopupHeight("230px").ShowClearButton().Width("250").Autofill(True).AllowCustom(True).Change("valueChange").DataSource(ViewBag.jsonFacilities).Fields(New Syncfusion.EJ2.DropDowns.ComboBoxFieldSettings With {.Text = "Name", .Value = "FacilityID"}).Render()
        </div>
    </div>


    @*<script id="template" type="text/x-template">
            <a href="#">${Facilities.LastName}</a>
            ${format(Facilities.LastName)}
            ${'Roles'}
        </script>*@




    <div ID="DivFacilities" style="margin-left:10px;margin-top:20px;">
        <div><h4>Facilities</h4></div>
        @Html.EJS().Grid("Grid").Height("400").Width("1500").Columns(Sub(col)
                                                                         col.HeaderText("Users").Width("90").Commands(commands2).Add()
                                                                         col.Field("FacilityID").HeaderText("FacilityID").IsPrimaryKey(True).Visible(False).Add()
                                                                         col.Field("Roles").HeaderText("Roles").Width("200").Add()
                                                                         'col.HeaderText("Roles").Width("200").Template("#template").Add()
                                                                         col.Field("FacilityGroupName").HeaderText("Facility Group").Width("150").Add()
                                                                         col.Field("Name").HeaderText("Facility Name").Width("200").Add()
                                                                         ' col.Field("Address").HeaderText("Address").Width("200").CustomAttributes(New With {.class = "customcss"}).Add()
                                                                         col.Field("Address").HeaderText("Address").Width("150").Add()
                                                                         col.Field("City").HeaderText("City").Width("100").Add()
                                                                         col.Field("State").HeaderText("State").Width("100").Add()
                                                                         col.Field("Zip").HeaderText("Zip").Width("100").Add()
                                                                         col.Field("Phone1").HeaderText("Phone").Width("115").Add()
                                                                         'col.Field("Phone2").HeaderText("Phone2").Width("115").Add()
                                                                         col.Field("Beds").HeaderText("Beds").Width("90").Add()
                                                                         col.Field("ShiftChange").HeaderText("ShftChnge").Width("90").Add()
                                                                         col.Field("InActive").HeaderText("InActive").EditType("booleanedit").DisplayAsCheckBox(True).Type("boolean").Width("100").Add()
                                                                     End Sub).EditSettings(Sub(edit) edit.AllowAdding(True).AllowEditing(True).AllowDeleting(True).Mode(Syncfusion.EJ2.Grids.EditMode.Dialog)).Render()
    </div>


    <div ID="DivUsers" style="margin-left:10px;margin-top:20px;">
        <div><h4>Users</h4></div>
        <div>
            @Html.EJS().Grid("Grid2").Height("300").Width("1100").Columns(Sub(col)
                                                                              col.Field("UserID").HeaderText("UserID").IsPrimaryKey(True).Visible(False).Add()
                                                                              col.Field("FacilityName").HeaderText("FacilityName").Width("150").Add()
                                                                              col.Field("LastName").HeaderText("Last Name").Width("150").AllowEditing(True).Add()
                                                                              col.Field("FirstName").HeaderText("First Name").Width("150").Add()
                                                                              col.Field("EmailAddress").HeaderText("Email Address").Width("250").Template("#template").Add()
                                                                              col.Field("Phone1").HeaderText("Phone1").Width("150").Add()
                                                                              'col.Field("Phone2").HeaderText("Phone2").Width("150").Add()
                                                                              'col.Field("DateCreated").HeaderText("Date Created").HeaderTemplate("#datetemplate").Format("yMd").Width("160").AllowEditing(False).Add()
                                                                              col.Field("InActive").HeaderText("InActive").EditType("booleanedit").DisplayAsCheckBox(True).Type("boolean").Width("150").Add()
                                                                          End Sub).ActionComplete("actionComplete2").ActionBegin("actionBegin2").SelectionSettings(Sub(selection) selection.Type(Syncfusion.EJ2.Grids.SelectionType.Single)).AllowSorting().AllowFiltering().AllowGrouping().AllowResizing(True).AllowPaging().PageSettings(Sub(page) page.PageCount(2)).FilterSettings(Sub(Filter) Filter.Type(Syncfusion.EJ2.Grids.FilterType.Excel)).Render()

        </div>
    </div>





    <div>
        @Html.EJS().Dialog("dialog").Visible(False).AnimationSettings(New DialogAnimationSettings() With {.Effect = DialogEffect.Zoom}).Header("Facility Users").Open("dialogOpen").Close("dialogClose").Position(Function(obj) obj.X("center").Y("center")).IsModal(False).AllowDragging(True).ShowCloseIcon(True).CloseOnEscape(False).Width("1150").Height("500").Target("#Grid").Buttons(Sub(btn) btn.Click("dlgButtonClick").ButtonModel(ViewBag.button1).Add()).ContentTemplate(@@<div class='dialog-content'>
            <div class='msg-wrapper  col-lg-12'>
                <div>
                    {@Html.EJS().Grid("Grid3").Height("200").Width("1100").Columns(Sub(col)
                                                                                      col.Field("Roles").HeaderText("Roles").Width("200").Add()
                                                                                      col.Field("UserID").HeaderText("UserID").IsPrimaryKey(True).Visible(False).Add()
                                                                                      col.Field("LastName").HeaderText("Last Name").Width("150").AllowEditing(True).Add()
                                                                                      col.Field("FirstName").HeaderText("First Name").Width("150").Add()
                                                                                      col.Field("EmailAddress").HeaderText("Email Address").Width("250").Template("#template").Add()
                                                                                      col.Field("Phone1").HeaderText("Phone1").Width("150").Add()
                                                                                      'col.Field("Phone2").HeaderText("Phone2").Width("150").Add()
                                                                                      'col.Field("DateCreated").HeaderText("Date Created").HeaderTemplate("#datetemplate").Format("yMd").Width("160").AllowEditing(False).Add()
                                                                                      col.Field("InActive").HeaderText("InActive").EditType("booleanedit").DisplayAsCheckBox(True).Type("boolean").Width("150").Add()
                                                                                  End Sub).ActionComplete("actionComplete2").ActionBegin("actionBegin2").SelectionSettings(Sub(selection) selection.Type(Syncfusion.EJ2.Grids.SelectionType.Single)).AllowSorting().AllowFiltering().AllowGrouping().AllowResizing(True).AllowPaging().PageSettings(Sub(page) page.PageCount(2)).FilterSettings(Sub(Filter) Filter.Type(Syncfusion.EJ2.Grids.FilterType.Excel)).Render()}
                </div>
            </div>
        </div>).Render()
    </div>

</div>

@*.QueryCellInfo("dropdown")*@
@*.QueryCellInfo("tooltip")*@


@*<div id='dropdown'>
        <select class="e-control e-dropdownlist">
            <option value="1" selected="selected">Order Placed</option>
            <option value="2">Processing</option>
            <option value="3">Delivered</option>
        </select>
    </div>*@

<script id="template" type="text/x-template">
    <div>
        <a href="mailto: ${EmailAddress}">${EmailAddress}</a>
    </div>
</script>

<script>
    function tooltip(args) { // event triggers on every cell render.
        new ej.popups.Tooltip({
            content: args.data[args.column.field].toString() // add Essential JS2 tooltip for every cell.
        }, args.cell);
    }

    function dropdown(args) {
        var ele = args.cell.querySelector('select');
        var drop = new ej.dropdowns.DropDownList({ popupHeight: 150, popupWidth: 150 });
        drop.appendTo(ele);
    }
</script>

<script>


    function dialogClose() {
        //document.getElementById('normalbtn').style.display = 'inline-block';
    }
    function dialogOpen() {
        //document.getElementById('normalbtn').style.display = 'none';
    }
    function dlgButtonClick() {
        // window.open('https://www.syncfusion.com/company/about-us');
    }

    function load() {
        this.columns[0].commands[0].buttonOption.click = function (args) {

            var dialogObj = document.getElementById('dialog').ej2_instances[0];
            dialogObj.show();

            var row = new ej.base.closest(event.target, '.e-row');
            var index = row.getAttribute('aria-rowindex')
            var grid = document.getElementById('Grid').ej2_instances[0];
            var rowData = grid.currentViewData[index];
            var request = JSON.stringify(rowData);
            var grid3 = document.getElementById('Grid3').ej2_instances[0];

            $.post('/Facilities/GetFacilityUsers', request)
                .done(function (data) {
                    if (data) {
                        var data2 = JSON.parse(data)
                        grid3.dataSource = data2
                    };
                });
        };
    };
</script>

<script>
    var jsonFacilityGroups = $("#jsonFacilityGroups").data("value");
    var jsonFacilities = $("#jsonFacilities").data("value");
    var jsonStates = $("#jsonStates").data("value");
    var jsonJobTitles = $("#jsonJobTitles").data("value");




    $('#DivUsers').show();
    $('#DivFacilities').hide();

    //var MySearch = new Object();

    class Search {
        constructor(User, State, Title, Group, Facility, Control) {
            this.User = User;
            this.State = State;
            this.Title = Title;
            this.Group = Group;
            this.Facility = Facility;
            this.Control = Control;
        }
    }
    mySearch = new Search();

    function actionBegin(args) {
        if (args.requestType === 'save') {
        }
    }
    function actionComplete(args) {
        if (args.requestType === 'beginEdit') {
        }

        if (args.requestType === 'add') {
        }

        if (args.requestType === 'save') {
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
        }
    }



    function valueChange(args) {
        var grid = document.getElementById('Grid').ej2_instances[0];
        var grid2 = document.getElementById('Grid2').ej2_instances[0];

        var FacilityGroups = document.getElementById("FacilityGroups").ej2_instances[0];
        var Facilities = document.getElementById("Facilities").ej2_instances[0];
        var User = document.getElementById("Users").ej2_instances[0];
        var State = document.getElementById("States").ej2_instances[0];
        var Title = document.getElementById("Titles").ej2_instances[0];

        mySearch.User = User.value
        mySearch.State = State.text
        mySearch.Title = Title.value
        mySearch.Group = FacilityGroups.value
        mySearch.Facility = Facilities.value

        if (args.value == mySearch.User) {
            mySearch.Control = 'User';
        }

        if (args.value == mySearch.State) {
            mySearch.Control = 'State';
        }

        if (args.value == mySearch.Title) {
            mySearch.Control = 'Title';
        }

        if (args.value == mySearch.Group) {
            mySearch.Control = 'Group';
        }

        if (args.value == mySearch.Facility) {
            mySearch.Control = 'Facility';
        }

        if (args.value == mySearch.State) {
            var column = grid.getColumnByField("Roles");
            grid.showHider.show(column.headerText, 'headerText');
        }

        var mySearchJSON = JSON.stringify(mySearch);

        $.post('../Facilities/SearchRequest', mySearchJSON)
            .done(function (data) {
                var data2 = JSON.parse(data)

                if (args.value == mySearch.User) {
                    $('#DivUsers').hide();
                    $('#DivFacilities').show();
                    grid.dataSource = data2
                }
                if (args.value == mySearch.State) {
                    var column = grid.getColumnByField("Roles");
                    grid.showHider.hide(column.headerText, 'headerText');

                    $('#DivUsers').hide();
                    $('#DivFacilities').show();
                    grid.dataSource = data2
                }
                if (args.value == mySearch.Title) {
                    $('#DivUsers').show();
                    $('#DivFacilities').hide();
                    grid2.dataSource = data2
                }
                if (args.value == mySearch.Group) {
                    var column = grid.getColumnByField("Roles");
                    grid.showHider.hide(column.headerText, 'headerText');

                    $('#DivUsers').hide();
                    $('#DivFacilities').show();
                    grid.dataSource = data2
                }
                if (args.value == mySearch.Facility) {
                    var column = grid.getColumnByField("Roles");
                    grid.showHider.hide(column.headerText, 'headerText');

                    $('#DivUsers').hide();
                    $('#DivFacilities').show();
                    grid.dataSource = data2
                }
            });
    };

    function filtering1() {
        var data = document.getElementById("Users").ej2_instances[0];
        var query = new Query();
        query = (e.text != "") ? query.where("LastName", "startswith", e.text, true) : query;
        e.updateData(data.dataSource, query);
    }

    function filtering2() {
        //debugger;
        var data = document.getElementById("States").ej2_instances[0];
        var query = new Query();
        query = (e.text != "") ? query.where("State", "startswith", e.text, true) : query;
        e.updateData(data.dataSource, query);
    }

    function filtering3() {
        //debugger;
        var data = document.getElementById("Titles").ej2_instances[0];
        var query = new Query();
        query = (e.text != "") ? query.where("Title", "startswith", e.text, true) : query;
        e.updateData(data.dataSource, query);
    }

    function filtering4() {
        //debugger;
        var data = document.getElementById("FacilityGroups").ej2_instances[0];
        var query = new Query();
        query = (e.text != "") ? query.where("GroupName", "startswith", e.text, true) : query;
        e.updateData(data.dataSource, query);
    }

    function filtering5() {
        //debugger;
        var data = document.getElementById("Facilities").ej2_instances[0];
        var query = new Query();
        query = (e.text != "") ? query.where("Name", "startswith", e.text, true) : query;
        e.updateData(data.dataSource, query);
    }

</script>






@*function valueChange1(args) {


    //let school = {
    //    name: 'Vivekananda School',
    //    location : 'Delhi',
    //    established : '1971',
    //    displayInfo: function () {
    //        debugger;
    //        console.log(`${school.name} was established
    //              in ${school.established} at ${school.location}`);
    //    }
    //}
    //school.displayInfo();



            var FacilityGroups = document.getElementById("FacilityGroups").ej2_instances[0];
            var Facilities = document.getElementById("Facilities").ej2_instances[0];
            var User = document.getElementById("Users").ej2_instances[0];
            var State = document.getElementById("States").ej2_instances[0];
            var Title = document.getElementById("Titles").ej2_instances[0];

            if (User.value !== null) {
                //FacilityGroups = null;
                //Facilities = null;
                //State.value = null;
                //Title.value = null;
                User.value = parseInt(User.value);




                var grid = document.getElementById('Grid').ej2_instances[0];

                var column = grid.getColumnByField("Roles");
                grid.showHider.show(column.headerText, 'headerText');


                //for (var i = 0; i < grid.columns.length; i++) {
                //    if (grid.columns[i].field == "Roles") {
                //       grid.columns[i].visible = true;
                //   }
                //}

                //var tempQuery = new ej.data.Query().where('LastName', 'equal', User.value);
                //state.query = tempQuery;

                 mySearch.User = User.value
                var mySearchJSON = JSON.stringify(mySearch);

                $.post('../Facilities/SearchRequest', mySearchJSON)
                    .done(function (data) {
                        if (data.length = 'True') {
                        }
                    });



                //$.ajax({
                //    type: "Post",
                //    url: '../Facilities/SearchRequest', mySearchJSON,
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: function (data) {
                //        grid.dataSource = data

                //        $('#DivUsers').hide();
                //        $('#DivFacilities').show();

                //    }
                //});



                // $.ajax({
                //    type: "Post",
                //    url: '../Facilities/SearchFacilityByUser?UserID=' + mySearchJSON,
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: function (data) {
                //        grid.dataSource = data
                //        $('#DivUsers').hide();
                //        $('#DivFacilities').show();
                //    }
                //});

                //$.ajax({
                //    type: "Post",
                //    url: '../Facilities/SearchFacilityByUser?UserID=' + User.value,
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: function (data) {
                //        grid.dataSource = data
                //        $('#DivUsers').hide();
                //        $('#DivFacilities').show();
                //        //$("#myDiv").css("visibility", "hidden");
                //        //var formatData = ej.parseJSON(data);
                //        //var gridObj = $("#FlatGrid").ejGrid("instance");
                //        //var query = ej.Query().sortBy("ShipCity", ej.sortOrder.Ascending, false);
                //        //var dataManager = ej.DataManager(formatData).executeLocal(query);
                //        //gridObj.dataSource(formatData);//dataSource method

                //    }
                //});
            };
        };

        function filtering2() {
            //debugger;
            var data = document.getElementById("States").ej2_instances[0];
            var query = new Query();
            query = (e.text != "") ? query.where("State", "startswith", e.text, true) : query;
            e.updateData(data.dataSource, query);
        }
        function valueChange2(args) {
            var FacilityGroups = document.getElementById("FacilityGroups").ej2_instances[0];
            var Facilities = document.getElementById("Facilities").ej2_instances[0];
            var User = document.getElementById("Users").ej2_instances[0];
            var State = document.getElementById("States").ej2_instances[0];
            var Title = document.getElementById("Titles").ej2_instances[0];

            if (State.text !== null) {
                //FacilityGroups = null;
                //Facilities = null;
                //User.value = null;
                //Title.value = null;

                var grid = document.getElementById('Grid').ej2_instances[0];

                var column = grid.getColumnByField("Roles");
                grid.showHider.hide(column.headerText, 'headerText');


               mySearch.State =  State.text
                var mySearchJSON = JSON.stringify(mySearch);

                $.post('../Facilities/SearchRequest', mySearchJSON)
                    .done(function (data) {
                        if (data.length = 'True') {
                        }
                    });




                //for (var i = 0; i < grid.columns.length; i++) {
                //    if (grid.columns[i].field == "Roles") {
                //        debugger;
                //       grid.columns[i].visible = false;
                //   }
                //}

                //mySearch.State =  State.text
                //var mySearchJSON = JSON.stringify(mySearch);

                //$.ajax({
                //    type: "Post",
                //    url: '../Facilities/SearchFacilityByState?State=' + mySearchJSON,
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: function (data) {
                //        grid.dataSource = data

                //        $('#DivUsers').hide();
                //        $('#DivFacilities').show();

                //    }
                //});
            };
        };




        function filtering3() {
            //debugger;
            var data = document.getElementById("Titles").ej2_instances[0];
            var query = new Query();
            query = (e.text != "") ? query.where("Title", "startswith", e.text, true) : query;
            e.updateData(data.dataSource, query);
        }
        function valueChange3(args) {
            var FacilityGroups = document.getElementById("FacilityGroups").ej2_instances[0];
            var Facilities = document.getElementById("Facilities").ej2_instances[0];
            var User = document.getElementById("Users").ej2_instances[0];
            var State = document.getElementById("States").ej2_instances[0];
            var Title = document.getElementById("Titles").ej2_instances[0];

            if (Title.value !== null) {
                //FacilityGroups = null;
                //Facilities = null;
                //User.value = null;
                //State.value = null;
                Title.value = parseInt(Title.value);

                var grid2 = document.getElementById('Grid2').ej2_instances[0];

                mySearch.Title =  Title.value
                var mySearchJSON = JSON.stringify(mySearch);

                $.post('../Facilities/SearchRequest', mySearchJSON)
                    .done(function (data) {
                        if (data.length = 'True') {
                        }
                    });



                //$.ajax({
                //    type: "Post",
                //    url: '../Facilities/SearchUsersByTitle?TitleID=' + Title.value,
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: function (data) {
                //        grid2.dataSource = data
                //        $('#DivUsers').show();
                //        $('#DivFacilities').hide();
                //    }
                //});
            };
        };

        function filtering4() {
            //debugger;
            var data = document.getElementById("FacilityGroups").ej2_instances[0];
            var query = new Query();
            query = (e.text != "") ? query.where("GroupName", "startswith", e.text, true) : query;
            e.updateData(data.dataSource, query);
        }
        function valueChange4(args) {
            var FacilityGroups = document.getElementById("FacilityGroups").ej2_instances[0];
            var Facilities = document.getElementById("Facilities").ej2_instances[0];
            var User = document.getElementById("Users").ej2_instances[0];
            var State = document.getElementById("States").ej2_instances[0];
            var Title = document.getElementById("Titles").ej2_instances[0];

            if (FacilityGroups.value !== null) {
                //Facilities = null;
                //User.value = null;
                //State.value = null;
                //Title.value = null;

                FacilityGroups.value = parseInt(FacilityGroups.value);

                var grid = document.getElementById('Grid').ej2_instances[0];

                var column = grid.getColumnByField("Roles");
                grid.showHider.hide(column.headerText, 'headerText');

                mySearch.Group =  FacilityGroups.value
                var mySearchJSON = JSON.stringify(mySearch);

                $.post('../Facilities/SearchRequest', mySearchJSON)
                    .done(function (data) {
                        if (data.length = 'True') {
                        }
                    });


                //$.ajax({
                //    type: "Post",
                //    url: '../Facilities/SearchFacilityByGroup?GroupID=' + FacilityGroups.value,
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: function (data) {
                //        grid.dataSource = data
                //        $('#DivUsers').hide();
                //        $('#DivFacilities').show();
                //    }
                //});
            };
        };



        function filtering5() {
            //debugger;
            var data = document.getElementById("Facilities").ej2_instances[0];
            var query = new Query();
            query = (e.text != "") ? query.where("Name", "startswith", e.text, true) : query;
            e.updateData(data.dataSource, query);
        }
        function valueChange5(args) {
            var FacilityGroups = document.getElementById("FacilityGroups").ej2_instances[0];
            var Facilities = document.getElementById("Facilities").ej2_instances[0];
            var User = document.getElementById("Users").ej2_instances[0];
            var State = document.getElementById("States").ej2_instances[0];
            var Title = document.getElementById("Titles").ej2_instances[0];

            if (Facilities.value !== null) {
                //FacilityGroups = null;
                //User.value = null;
                //State.value = null;
                //Title.value = null;
                Facilities.value = parseInt(Facilities.value);

                var grid = document.getElementById('Grid').ej2_instances[0];

                var column = grid.getColumnByField("Roles");
                grid.showHider.hide(column.headerText, 'headerText');

                mySearch.Facility =  Facilities.value
                var mySearchJSON = JSON.stringify(mySearch);

                $.post('../Facilities/SearchRequest', mySearchJSON)
                    .done(function (data) {
                        if (data.length = 'True') {
                        }
                    });


                //$.ajax({
                //    type: "Post",
                //    url: '../Facilities/SearchFacilityByFacility?FacilityID=' + Facilities.value,
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: function (data) {
                //        grid.dataSource = data
                //        $('#DivUsers').hide();
                //        $('#DivFacilities').show();
                //    }
                //});
            };
        };
    </script>*@



<style>

    .header1 {
        font - weight: 600;
        color: rgba(0, 0, 0, .54);
        width: 100px;
        height: 35px;
        float: left;
        padding: 5px 0 0 16px;
        font-size: 16px;
        background-color: #f5f5f5;
        font-family: "Segoe UI", "GeezaPro", "DejaVu Serif";
    }

    .detail1 {
        float: left;
        width: 100px;
    }

    .detail2 {
        /* margin - left:  17px;*/
    }

    .DivHide {
        display: none;
    }

    .DivShow {
        display: block
    }

    /*.hide {
      opacity: 0;
    }*/
    /*.hide {
       visibility: hidden;
    }*/

    /*.hide {
       position: absolute;
       top: -9999px;
       left: -9999px;*/
    }
</style>

<style>

    .e-grid.e - rowcell.customcss {
        background-color: #ecedee;
        color: red;
        font-family: 'Bell MT';
        font-size: 20px;
    }

    .e-grid.e - headercell.customcss {
        background-color: #2382c3;
        color: white;
        font-family: 'Bell MT';
        font-size: 20px;
    }
</style>

@Html.EJS().ScriptManager()
