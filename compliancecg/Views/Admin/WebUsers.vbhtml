@Imports Syncfusion.EJ2
@imports Syncfusion.EJ2.Grids
@imports  Syncfusion.EJ2.DropDowns
@imports  Syncfusion.EJ2.Popups
@Imports CCGData.CCGData.Facility


@Code
    ViewData("Title") = "Web Users"
End Code




<div style='float:left;margin-right:30px;margin-bottom: 50px;' , class="control-wrapper">
    <div style='margin-bottom: 0px;'>
        <span class="label label-info">Web Users</span>
    </div>
    <div id="default" style='padding-top:10px;'>
        @Html.EJS().ComboBox("WebUsers").Placeholder("Select User").PopupHeight("230px").ShowClearButton().Width("250").Autofill(True).AllowCustom(True).Change("valueChangeWU").DataSource(ViewBag.jsonUsers).Fields(New Syncfusion.EJ2.DropDowns.ComboBoxFieldSettings With {.Text = "Email", .Value = "Id"}).ItemTemplate("<div><div class=\""detail1\""'> ${Email} </div></div>").HeaderTemplate("<div><div class=\""header1\"">Email</div></div>)").Render()
    </div>
</div>




<div style='float:left;margin-right:30px;margin-bottom: 50px;' class="control-wrapper">
    <div>
        <div style="float:left;margin-right:20px">
            @Html.EJS().RadioButton("radio3").Label("Active").Name("DisplayActive").Change("onRadioChangeWU").Checked(True).Render()
        </div>
        <div style="float:left;">
            @Html.EJS().RadioButton("radio4").Label("InActive").Name("DisplayActive").Change("onRadioChangeWU").Render()
        </div>
    </div>
</div>

<div style="clear:left">
</div>




<div ID="DivUsers" style="margin-left:10px;margin-top:20px;">
    <div><h4>Users</h4></div>
    <div>

        @Html.EJS().Grid("Grid2").Height("300").Width("1400").Columns(Sub(col)
                                                                          col.Field("Id").HeaderText("ID").IsPrimaryKey(True).Visible(False).Add()
                                                                          col.Field("Email").HeaderText("Email").Width("200").Template("#template").Add()
                                                                          col.Field("LastName").HeaderText("Last Name").Width("150").Add()
                                                                          col.Field("FirstName").HeaderText("First Name").Width("150").AllowEditing(True).Add()
                                                                          col.Field("IsActive").HeaderText("IsActive").EditType("booleanedit").DisplayAsCheckBox(True).Type("boolean").Width("150").Add()
                                                                          col.Field("TwoFactorEnabled").HeaderText("TwoFactorEnabled").EditType("booleanedit").DisplayAsCheckBox(True).Type("boolean").Width("150").Add()
                                                                          col.Field("LastLoginDate").HeaderText("LastLoginDate").Format("yMd").Width("160").AllowEditing(False).Add()
                                                                          col.Field("DateofEntry").HeaderText("Date Created").Format("yMd").Width("160").AllowEditing(False).Add()
                                                                          col.Field("Password2").HeaderText("Password").Width("140").Template("#template2").AllowEditing(False).Add()
                                                                      End Sub).AllowExcelExport().ToolbarClick("toolbarClickWU").Toolbar(New List(Of String)() From {"ExcelExport"}).Load("loadWU").SelectionSettings(Sub(selection) selection.Type(Syncfusion.EJ2.Grids.SelectionType.Single)).AllowSorting().AllowFiltering().AllowGrouping().AllowResizing(True).FilterSettings(Sub(Filter) Filter.Type(Syncfusion.EJ2.Grids.FilterType.Excel)).Render()

    </div>
</div>


<script id="template" type="text/x-template">
    <div>
        <a href="mailto: ${Email}">${Email}</a>
    </div>
</script>
<script id="template2" type="text/x-template">
    <div>
        <input type="password" name="password" class="password" value="${Password2}" style="width:85%">
        <span class="togglePassword"  onclick="togglePass(this)">show</span>
    </div>
   
</script>
<script>

    function togglePass(togglePassword) {
        debugger;

        const password = $(togglePassword).prev();
        const type = $(password).attr('type') === 'password' ? 'text' : 'password';
        $(password).attr('type', type);

        var text = $(togglePassword).html() === "show" ? "hide" : "show";
        $(togglePassword).html(text)
       // togglePassword.classList.toggle('fa-eye-slash');
    }
    function toolbarClickWU(args) {
        var gridObj = document.getElementById("Grid2").ej2_instances[0];
        if (args.item.id === 'Grid2_excelexport') {
            gridObj.excelExport();
        }
    }



    // function dropdown(args) {
    //     var ele = args.cell.querySelector('select');
    //     var drop = new ej.dropdowns.DropDownList({ popupHeight: 150, popupWidth: 150 });
    //     drop.appendTo(ele);
    // }

    function loadWU() {
        debugger;
        var mySearch = GetSearchValuesWU()
        SetGridDataWU();
    };

    class SearchWU {
        constructor(User) {
            this.User = User;
        }
    }
    mySearch = new SearchWU();


    function GetSearchValuesWU() {
        var grid2 = document.getElementById('Grid2').ej2_instances[0];
        var User = document.getElementById("WebUsers").ej2_instances[0];
        var RadioButton3 = document.getElementById('radio3').ej2_instances[0];
        var RadioButton4 = document.getElementById('radio4').ej2_instances[0];

        var Active
        if (RadioButton3.checked == true) { Active = 1 }
        if (RadioButton4.checked == true) { Active = 0 }

        mySearch.User = User.value
        mySearch.DisplayResult = 'WebUsers'
        mySearch.Active = Active

        return mySearch
    }

    function SetGridDataWU() {
        var mySearchJSON = JSON.stringify(mySearch);

        $.post('../Admin/SearchRequestWU', mySearchJSON)
            .done(function (data) {
                var data2 = JSON.parse(data)
                var grid2 = document.getElementById('Grid2').ej2_instances[0];
                grid2.dataSource = data2
            });
    }

    function onRadioChangeWU() {
        var mySearch = GetSearchValuesWU()
        SetGridDataWU()
    };


    function valueChangeWU(args) {
        debugger;
        var mySearch = GetSearchValues()
        mySearch.Control = 'WebUsers';
        SetGridData()
    };

</script>


<style>

    .togglePassword {
        cursor: pointer;
        color: #007bff
    }
    .header1 {
        font-weight: 600;
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

    .DivHide {
        display: none;
    }

    .DivShow {
        display: block
    }

    .e-grid.e-rowcell.customcss {
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

    .e-radio-wrapper {
        margin-top: 18px;
    }

    li {
        list-style: none;
    }

    .e-radio:checked + .e-success::after { /* csslint allow: adjoining-classes */
        background-color: #689f38;
        border-color: #689f38;
    }

    .e-radio:checked:focus + .e-success::after, .e-radio:checked + .e-success:hover::after { /* csslint allow: adjoining-classes */
        background-color: #449d44;
        border-color: #449d44;
    }

    .e-radio:checked + .e-success::before {
        border-color: #689f38;
    }

    .e-radio:checked:focus + .e-success::before, .e-radio:checked + .e-success:hover::before { /* csslint allow: adjoining-classes */
        border-color: #449d44;
    }

    .e-radio + .e-success:hover::before {
        border-color: #b1afaf
    }

    .e-radio:checked + .e-info::after { /* csslint allow: adjoining-classes */
        background-color: #2196f3;
        border-color: #2196f3;
    }

    .e-radio:checked:focus + .e-info::after, .e-radio:checked + .e-info:hover::after { /* csslint allow: adjoining-classes */
        background-color: #0b7dda;
        border-color: #0b7dda;
    }

    .e-radio:checked + .e-info::before {
        border-color: #2196f3;
    }

    .e-radio:checked:focus + .e-info::before, .e-radio:checked + .e-info:hover::before {
        border-color: #0b7dda;
    }

    .e-radio + .e-info:hover::before {
        border-color: #b1afaf
    }

    .e-radio:checked + .e-warning::after { /* csslint allow: adjoining-classes */
        background-color: #ef6c00;
        border-color: #ef6c00;
    }

    .e-radio:checked:focus + .e-warning::after, .e-radio:checked + .e-warning:hover::after { /* csslint allow: adjoining-classes */
        background-color: #cc5c00;
    }

    .e-radio:checked + .e-warning::before {
        border-color: #ef6c00;
    }

    .e-radio:checked:focus + .e-warning::before, .e-radio:checked + .e-warning:hover::before {
        border-color: #cc5c00;
    }

    .e-radio + .e-warning:hover::before {
        border-color: #b1afaf
    }

    .e-radio:checked + .e-danger::after { /* csslint allow: adjoining-classes */
        background-color: #d84315;
        border-color: #d84315;
    }

    .e-radio:checked:focus + .e-danger::after, .e-radio:checked + .e-danger:hover::after { /* csslint allow: adjoining-classes */
        background-color: #ba330a;
        border-color: #ba330a;
    }

    .e-radio:checked + .e-danger::before {
        border-color: #d84315;
    }

    .e-radio:checked:focus + .e-danger::before, .e-radio:checked + .e-danger:hover::before {
        border-color: #ba330a;
    }

    .e-radio + .e-danger:hover::before {
        border-color: #b1afaf
    }
</style>

@Html.EJS().ScriptManager()
