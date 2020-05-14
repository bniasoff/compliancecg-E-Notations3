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



    <div>
 

        <div id="Grid1" style="display: none;height:800px"> @Html.Partial("../Facilities/FacilitiesPartial3")</div>
    </div>





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
        var item1 = tabObj.tbItem[0].innerText.toLowerCase()
        GetFacilityGrid(item1)
    }


    function GetFacilityGrid(item1) {
        let FacilityGroup = FacilityGroups.find(g => g.GroupName.toLowerCase() === item1.toLowerCase());

        if (typeof FacilityGroup !== 'undefined') {
            FacilityGroup2 = FacilityGroup
            var request = JSON.stringify(FacilityGroup);
            let Facilitys2 = Facilites.filter(f => f.FacilityGroupID === FacilityGroup.FacilityGroupID);

            $.post('/Facilities/SetFacilityGroupSessionValues', request)
                .done(function (data) {             
                    if (data.length = 'True') {            
                        //CheckUserRole();

                        var grid = document.getElementById("Grid").ej2_instances[0];
                        grid.dataSource = new ej.data.DataManager({
                            url: "/Facilities/UrlDatasource",
                            insertUrl: "/Facilities/Insert",
                            updateUrl: "/Facilities/Update",
                            removeUrl: "/Facilities/Delete",

                            adaptor: new ej.data.UrlAdaptor()
                            //.Query().addParams("FacilityID",FacilityGroup.FacilityGroupID)
                        });
                    };
                });
        };
    };




    function TabSelecting(e) {
    }

    function TabSelected(e) {
        //var tabObj = document.getElementById("ej2Tab").ej2_instances[0];
        //var content = tabObj.itemIndexArray[e.selectedIndex]
        //var content2 = 'a' + e.selectedItem.textContent.replace(/ /g, "_");
        GetFacilityGrid(e.selectedItem.textContent)


    }
</script>



<style>
    .e-overlay {
        background-color: #800000;
        opacity: 0.3;
        filter: alpha(opacity=30)
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
    }
</style>





<Style>

    #container {
        visibility: hidden;
    }

    #loader {
        color: #008cff;
        height: 40px;
        left: 45%;
        position: absolute;
        top: 45%;
        width: 30%;
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






@Html.EJS().ScriptManager()
