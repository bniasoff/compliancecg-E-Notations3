
@Imports Syncfusion.EJ2
@Imports Syncfusion.EJ2.Navigations
@Imports Syncfusino.EJ2.DropDowns

<script type="text/javascript">

    $(document).ready(function () {

        $("#InvoiceID").on('change', function () {
            if (!RichEdit.InCallback())
                RichEdit.PerformCallback({ "InvoiceID": $("#InvoiceID").val() });
        });
    });
    function OnRichEditInit(s, e) {
        RichEdit.PerformCallback();
    }
</script>

<input type="hidden" id="iconFieldsjson2" data-value="@ViewBag.iconFieldsjson2" />
<input type="hidden" id="FacilityGroup" data-value="@ViewBag.FacilityGroup" />
<input type="hidden" id="FacilitysHidden" data-value="@ViewBag.dataJson" />
@code Dim FacilityGroup = ViewBag.FacilityGroup End Code
<div class="mainRow row justify-content-center may-4">
    <div class="col colfacDD">
        <h2>Select your facility:</h2>
        @Html.EJS().DropDownList("policiesDDL").Placeholder("Select your facility...").PopupHeight("200px").Change("onNodeSelected2").DataSource(ViewBag.data).GroupTemplate("<strong>${State}</strong>").Fields(Function(field) field.Value("FacilityID").Text("Name").GroupBy("State")).Render()
    </div>
</div>
<div class="row secondRow">
    @*<div class="treedata2">
             @Html.EJS().TreeView("tree1").Fields(ViewBag.iconFields2).ExpandOn(Syncfusion.EJ2.Navigations.ExpandOnSettings.Click).NodeSelected("onNodeSelected2").NodeClicked("onNodeClicked2").NodeExpanding("onNodeExpanded2").Render()   (IEnumerable < Object >).Fields(CType({Text = "Name", Value = "FacilityGroupID"}, Syncfusion.EJ2.DropDowns.DropDownListFieldSettings))
        </div>*@

    <div class="col-lg-5 mainRow col-md-12">
        <h2 style="font-size:1.5rem;">Select your policy:</h2>
        <div id="tree2"></div>
    </div>
    <div class="col-lg-7 col-md-12 documentView">
        <div ID="DocumentView" class="col-12 mainRow ">
            <form>
                <div id="document"></div>
            </form>
        </div>
    </div>
</div>





<script type="text/javascript">
    var close = false;
    var iconFields;

    function loadEfile(file, State, FacilityName) {
        $.ajax({
            type: "POST",
            cache: false,
            async: true,
            datatype: 'json',
            url: '../Policies/LoadDocument?FileName=' + file + '&State=' + State + '&SelectedFacilityName=' + FacilityName,
            contentType: 'application/json',
            success: function (response) {
                $('#document').html(response);
                return false;
            },
            error: function (xhr, status, message) {
            }
        });
        return false;
    }

    function json2array(json) {
        var result = [];
        var keys = Object.keys(json);
        keys.forEach(function (key) {
            result.push(json[key]);
        });
        return result;
    }


    function loadDocIndex(State, FacilityName) {

         $.ajax({
            type: "POST",
            cache: false,
            async: true,
            datatype: 'json',
            url: '../Policies/TreeViewPolicies?State=' + State + '&SelectedFacilityName=' + FacilityName,
            contentType: 'application/json',
             success: function (response) {
                var Policies = [];
                $.each($.parseJSON(response), function (idx, obj) {
                    if (obj.NodeId == "1")
                        obj.HasChild = true;

                    Policies.push(obj);
                });

                var tree2 = document.getElementById('tree2').ej2_instances[0];

                // var tree2 = ej.base.getComponent(document.querySelector('#tree2'), 'treeview');
                tree2.fields.dataSource = Policies
                iconFields = Policies
                //tree2.refresh();
                treeData = tree2.getTreeData()
                //      var node = tree2.getNode(treeData[0]);
                // tree2.expandAll(2);
                //     debugger;
                //  openDoc(treeData[0].NodeChild[0].FullPath);
                //loadEfile(treeData[0].NodeChild[0].FullPath, State, FacilityName);
                //tree2.expandNode($("#tree2_active")[0]);//treeObj.getNodeByIndex(0), $("#tree2"), false
                //  $("#tree2_active").nodeSelected();
                //  document.getElementById("tree2_active").style.display = "block";
                // ;
                // tree2.expandNode($("#tree2_active"));//treeObj.getNodeByIndex(0), $("#tree2"), false
                //
                //var treeObj = new ej.navigations.TreeView({ fields: { dataSource: response, id:  'NodeId', text: 'NodeText', child: 'NodeChild' } });
                //treeObj.appendTo('#tree');

                //var treeObj = document.getElementById('tree').ej2_instances[0];
                //treeObj.fields=response
                // document.getElementById("TreeView1").style.display = "block";
            },
            error: function (xhr, status, message) {
            }
        });
        return false;
    }

    //var iconFields = $("#iconFieldsjson").data("value");
    var iconFields2 = $("#iconFieldsjson2").data("value");
    var facilities = $("#FacilitysHidden").data("value");
    var SelectedFacility, FacilityName, FacilityState


    function onNodeCollapsed2(args) {
    }

    function onNodeExpanded2(args) {
    }

    function onNodeClicked2(args) {
    }

    function onNodeSelected2(args) {
       $(".secondRow").css("visibility", "visible");
        var treeObj2 = new ej.navigations.TreeView({ fields: { id: 'NodeId', text: 'NodeText', child: 'NodeChild', iconCss: 'Icon', Expanded: 'Expanded', HasChild: true }, expandOn: "Click", nodeSelected: onNodeSelected });
        treeObj2.appendTo('#tree2');


        let SelectedNode = facilities.find(g => g.FacilityID === args.itemData.FacilityID);
        FacilityName = args.itemData.Name;
        FacilityState = args.itemData.State;
        SelectedFacility = SelectedNode
        loadDocIndex(SelectedNode.State, SelectedNode.Name)
    }

    function onNodeCollapsed(args) {
    }

    function onNodeExpanded(args) {
    }

    function onNodeSelected(args) {
    }

    function onNodeClicked(args) {
    }

    function onNodeSelected(args) {

        // openDoc(args.nodeData.text);
        if (args.nodeData.parentID != null)
            loadEfile(args.nodeData.text, FacilityState, FacilityName);

    }
    function openDoc(FullPath) {
        //   var treeObj = document.getElementById('tree2').ej2_instances[0];
        // var iconFields = treeObj.treeData[args.nodeData.parentID - 1]
        //  let SelectedNode = iconFields.NodeChild.find(g => g.NodeText === args.nodeText);

        loadEfile(FullPath, FullPath, FacilityName);

    }

</script>

<script type="text/javascript">

    function recursiveKeySearch(key, data, caseSensitive = true) {

        // not shown - perhaps validate key as non-zero length string
        // not shown - perhaps validate caseSensitive as boolean

        // Handle null edge case.
        if (data === null) {
            // nothing to do here
            return [];
        }

        // handle case of non-object, which will not be searched
        if (data !== Object(data)) {
            return [];
        }

        var results = [];

        // determine 'key' value to be used for comparison
        var comparisonKey = key;
        if (caseSensitive === false) {
            comparisonKey = key.toLowerCase();

            // Handle array which we just traverse and recurse.
            // var (data.constructor === Array) {
            for (var i = 0, len = data.length; i < len; i++) {
                results = results.concat(recursiveKeySearch(key, data[i], caseSensitive));
            }

            // We know we have an general object to work with now.
            // if Now we need to iterate keys
            for (var dataKey in data) {
                var isMatch = false;
                if (caseSensitive === false) {
                    results = (comparisonKey === dataKey.toLowerCase());
                } else {
                    isMatch = (comparisonKey === dataKey);
                }

                if (isMatch) {
                    isMatch = (comparisonKey === dataKey);
                    results.push(data[dataKey]);
                }

                // now recurse into value at key
                results = results.concat(recursiveKeySearch(key, data[dataKey], caseSensitive));
            }
        }
    }

</script>

<style>
   .control_wrapper {
        max-width: 500px;
        margin: auto;
        border: 1px solid #dddddd;
        border-radius: 3px;
    }

    @@media screen and (max-width: 768px) {
        .treeview-control-section {
            margin: 0;
        }
    }

    .e-treeview .e-list-img {
        width: 25px;
        height: 25px;
    }
    /* Loading sprite image for TreeView */
    .e-treeview .e-list-icon {
        background-repeat: no-repeat;
        background-image: url('@Url.Content("~/Content/Images/file_icons.png")');
        height: 20px;
    }
        /* Specify the icon positions based upon class name */
        .e-treeview .e-list-icon.folder {
            background-position: -197px -552px;
        }

        .e-treeview .e-list-icon.docx {
            background-position: -197px -20px;
        }

        .e-treeview.e - list - icon.ppt {
            background-position:  -197px -48px;
        }

        .e-treeview.e - list - icon.pdf {
            background-position:  -197px -104px;
        }

        .e-treeview.e - list - icon.images {
            background-position:  -197px -132px;
        }

        .e-treeview.e - list - icon.zip {
            background-position:  -197px -188px;
        }

        .e-treeview.e - list - icon.audio {
            background-position:  -197px -244px;
        }

        .e-treeview.e - list - icon.video {
            background-position:  -197px -272px;
        }

        .e-treeview.e - list - icon.exe {
            background-position:  -197px -412px;
        }
</style>

<style>
    .treedata {
        max-width: 400px;
        min-width: 350px;
        margin: 0px;
        margin-top: 10px;
        border: 1px solid #dddddd;
        border-radius: 3px;
        max-height: 530px;
        overflow: auto;
        min-height: 180px;
        float: left;
        margin-right: 10px
    }

    .treedata2 {
        max - width: 700px;
        min-width: 350px;
        margin: 0px;
        margin-top: 10px;
        border: 1px solid #dddddd;
        border-radius: 3px;
        max-height: 100px;
        overflow: auto;
        min-height: 200px;
        margin-right: 10px
    }
</style>
@Html.EJS().ScriptManager()
