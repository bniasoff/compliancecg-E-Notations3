@Imports Syncfusion.EJ2
@Imports Syncfusion.EJ2.Navigations
@imports  Syncfusion.EJ2.Popups
@imports  Syncfusion.EJ2.DropDowns
@Imports CCGData
@Imports CCGData.CCGData
@Imports Newtonsoft.Json
@*<link rel="stylesheet" href="https://cdn.syncfusion.com/ej2/material.css" />
    <script src="https://kendo.cdn.telerik.com/2018.3.1017/js/jquery.min.js"></script>
    <script src="https://kendo.cdn.telerik.com/2018.3.1017/js/jszip.min.js"></script>
    <script src="http://cdn.syncfusion.com/ej2/ej2-data/dist/global/ej2-data.min.js" type="text/javascript"></script>*@

@*<script src="~/Scripts/jquery-3.3.1.min.js"></script>*@
<link href="~/Content/ej2/material.css" rel="stylesheet" />
<script src="~/Content/ej2/dist/ej2.min.js"></script>

<input type="hidden" id="FacilityGroups" data-value="@ViewBag.FacilityGroups" />
<input type="hidden" id="Facilites" data-value="@ViewBag.Facilites" />
<input type="hidden" id="UserEmails" data-value="@ViewBag.UserEmails" />


@code
    Dim TabTabItems As List(Of Syncfusion.EJ2.Navigations.TabTabItem) = New List(Of Syncfusion.EJ2.Navigations.TabTabItem)()
    For Each TabHeader As TabHeader In ViewBag.TabHeaders
        'TabTabItems.Add(New Syncfusion.EJ2.Navigations.TabTabItem With {.Header = TabHeader, .Content = "#Grid3a"})
        TabTabItems.Add(New Syncfusion.EJ2.Navigations.TabTabItem With {.Header = TabHeader, .Content = "#Grid1"})
    Next

    Dim FacilityUserList = New Syncfusion.EJ2.DropDowns.DropDownList() With
    {
    .DataSource = ViewBag.FacilityUserDropDownList,
    .Query = "new ej.data.Query()",
    .Fields = New Syncfusion.EJ2.DropDowns.DropDownListFieldSettings() With {.Value = "value", .Text = "text"},
    .AllowFiltering = False
    }

    Dim JobTitleList = New Syncfusion.EJ2.DropDowns.DropDownList() With
    {
    .DataSource = ViewBag.JobTitlesDropDownList,
    .Query = "new ej.data.Query()",
    .Fields = New Syncfusion.EJ2.DropDowns.DropDownListFieldSettings() With {.Value = "value", .Text = "text"},
    .AllowFiltering = False
    }

    'Dim TabTabActionSettings As List(Of TabTabActionSettings) = New List(Of TabTabActionSettings)
    'TabTabActionSettings.Add(New TabTabActionSettings With {.Effect = "SlideRight"})


End code


@*<div style="border-style: solid;border-color: coral;"><span>Facility</span></div>*@






<div>
    <div style="width:10px">
        @*@Html.EJS().Tab("ej2Tab").Animation(Function(anim) anim.Previous(New TabTabActionSettings With {.Effect = "SlideRight"})).HeightAdjustMode(HeightStyles.Auto).OverflowMode(OverflowMode.Popup).Selected("TabSelected").Items(TabTabItems).Width("1500px").Height("600px").SelectedItem(0).Created("TabCreated").HeaderPlacement(HeaderPosition.Left).HeightAdjustMode(HeightStyles.Content).Render()*@
        @Html.EJS().Tab("ej2Tab").HeightAdjustMode(HeightStyles.Auto).OverflowMode(OverflowMode.Scrollable).Selected("TabSelected").Items(TabTabItems).Width("1500px").Height("800px").SelectedItem(0).Created("TabCreated").HeaderPlacement(HeaderPosition.Left).HeightAdjustMode(HeightStyles.Content).Render()
        
        @If ViewBag.IsAdmin = True Then
            @<div id="GroupButton">
                <div class="col-xs-12 col-sm-12 col-lg-6 col-md-6">
                    <button type="button" class="btn btn-primary" id="ajaxSubmit" onclick="onClick();">Edit Groups</button>
                </div>
             </div>
        End If

    </div>


    <div id = "target3" Class="col-lg-12 control-section" style="display:none; height:600px;">
        @Html.EJS().Dialog("ExternalDialog3").IsModal(True).AnimationSettings(New DialogAnimationSettings() With {.Effect = DialogEffect.None}).ContentTemplate(@@<div>

            <div id="Grid2">Test</div>
        </div>).ShowCloseIcon(True).CloseOnEscape(True).Width("600px").Height("500px").Target("#Grid1").Created("Created").Visible(False).Render()
    </div>



    <div id="Grid1" style="display: none;height:800px"> @Html.Partial("../Facilities/FacilitiesPartial")</div>

    @*<div id="Grid2" style="display: none;height:800px"> @Html.Partial("../Facilities/FacilityGroupsPartial2")</div>*@
    <div id="Grid2"></div>
</div>




<script type="text/javascript">
    function onClick(e) {

        @*$('#divid').load('@Url.Action("About", "Home")');*@

      // $("#Grid1").empty();
        //$("#Grid2").show();

        //$('#Grid2').load("../Facilities/FacilitiesPartial2");

        //$("#target").show();


        var dialog = document.getElementById("ExternalDialog3").ej2_instances[0];
        dialog.show();

        //var button = new ej.buttons.Button({ isPrimary: true });
        //button.appendTo('#Grid2');


        gridInstance = new ej.grids.Grid({
            allowPaging: false,

            dataSource: new ej.data.DataManager({
                url: "/Facilities/UrlDatasource5",
                insertUrl: "/Facilities/Insert5",
                updateUrl: "/Facilities/Update5",
                removeUrl: "/Facilities/Delete5",
                adaptor: new ej.data.UrlAdaptor()
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
   }
                if (args.requestType === 'add') {
                    EditMode = 'Add'
                }
            },


            //load: load2,
            height: 165,
            width: 550,
            allowSorting: true,
            allowFiltering: true,
            allowResizing: true,
            filterSettings: { type: "Excel" },

            editSettings: { allowAdding: true, allowEditing: true, allowDeleting: true, mode: "Normal" },
            toolbar: ["Add", "Edit", "Update", "Delete", "Cancel"],
            columns:
                [
                    { field: "FacilityGroupID", headerText: "FacilityGroupID", width: 0, isPrimaryKey: true, visible: false },
                    { field: "GroupName", headerText: "GroupName", width: 200, visible: true },
                    { field: "AllowPolicyPrint", headerText: "AllowPolicyPrint", width: 150, editType: "booleanedit", displayAsCheckBox: true, type: 'boolean', textAlign: 'Center' },
                    { field: "InActive", headerText: "InActive", width: 150, editType: "booleanedit", displayAsCheckBox: true, type: 'boolean', textAlign: 'Center' }

                ]
        });


        //$("#Grid1").empty();

        $("#Grid2").empty();
        gridInstance.appendTo('#Grid2');



   //     // debugger;
   //     //alert('Clicked');
    }

    function Created() {
        this.target = ".e-gridcontent";
    }
</script>






<script type="text/javascript">
    var FacilityGroups = $("#FacilityGroups").data("value");
    var Facilites = $("#Facilites").data("value");
    var UserEmails = $("#UserEmails").data("value");
    var EditMode
    var rowData
    //var FacilityUserDropDownList2 = $("#FacilityUserDropDownList2").data("value");



    var FacilityGroup2

    function TabCreated(e) {
        var tabObj = document.getElementById("ej2Tab").ej2_instances[0];
        //debugger;
        tabObj.element.classList.add('e-fill');
        //tabObj.element.classList.add('TabWidth');

        var item1 = tabObj.tbItem[0].innerText.toLowerCase()
        GetFacilityGrid(item1)
    }


    function CheckUserRole() {


    }



    function GetFacilityGrid(item1) {
        let FacilityGroup = FacilityGroups.find(g => g.GroupName.toLowerCase() === item1.toLowerCase());
        if (typeof FacilityGroup !== 'undefined') {
            FacilityGroup2 = FacilityGroup
            var request = JSON.stringify(FacilityGroup);
            let Facilitys2 = Facilites.filter(f => f.FacilityGroupID === FacilityGroup.FacilityGroupID);

            $.post('/Facilities/SetFacilityGroupSessionValues', request)
                .done(function (data) {
                    // debugger;
                    if (data == 'True') {
                        CheckUserRole();

                        var grid = document.getElementById("Grid").ej2_instances[0];
                        grid.dataSource = new ej.data.DataManager({
                            url: "/Facilities/UrlDatasource",
                            insertUrl: "/Facilities/Insert",
                            updateUrl: "/Facilities/Update",
                            removeUrl: "/Facilities/Delete",

                            adaptor: new ej.data.UrlAdaptor()
                            //.Query().addParams("FacilityID",FacilityGroup.FacilityGroupID)
                        });
                    }
                });


        };
    };




    function TabSelecting(e) {
    }

    function TabSelected(e) {
        //var tabObj = document.getElementById("ej2Tab").ej2_instances[0];
        //var content = tabObj.itemIndexArray[e.selectedIndex]
        //var content2 = 'a' + e.selectedItem.textContent.replace(/ /g, "_");

        //debugger;
        GetFacilityGrid(e.selectedItem.textContent)



    }
</script>



<style>

    .e-tab .e-tab-header.e-vertical {
        max-width: 300px;
        z-index: 1;
    }

    /*.e-tab .e-tab-header .e-toolbar-items .e-active .e-tab-wrap {
        background-color: #08c;
        width: 500px;
    }*/

    .e-tab .e-tab-header {
        /*background-color: #e6e6e6;*/
        /*background-color: green;*/
        width: 500px;
    }



    /*.e-tab-custom-class {
    }


    .e-tab-header {
        background-color: green;
    }

    .control-section {
        padding-left: 10px;
    }

    #target {
        height: 100%;
        min-height: 350px;
        min-height: 350px;
    }

    @@media screen and (min-width: 150px) and (max-width: 1200px) {
        .control-section {
            margin-bottom: 30px;
        }

        .control-wrapper {
            margin-bottom: 0px;
        }
    }*/
</style>





<Style>

    /*#container {
        visibility: hidden;
    }*/

    #loader {
        color: green;
        height: 40px;
        left: 45%;
        position: absolute;
        top: 45%;
        width: 30%;
    }

    #TabWidth {
        width: 300px;
    }


    .e-content.e - item {
        font-size: 12px;
        margin: 10px;
        text-align: justify;
    }

    .empImage {
        margin: 6px 16px;
        float: left;
        width: 25px;
        height: 25px;
    }


    .topdiv {
        padding-top: 35px;
        margin-left: 50px
    }

    .k-tabstrip:focus {
        -webkit-box-shadow: none;
        box-shadow: none;
    }
</Style>


<style type="text/css">
    .e-tab.e-fill {
        width: 500px
    }



    .test .e-box.e-left .e-item.e-select {
        background-color: bisque;
    }

        .test .e-box.e-left .e-item.e-select .e-link {
            color: red;
        }

    .test .e-box.e-left .e-item.e-active {
        border-left: 1px solid #c8c8c8;
    }
</style>





@Html.EJS().ScriptManager()