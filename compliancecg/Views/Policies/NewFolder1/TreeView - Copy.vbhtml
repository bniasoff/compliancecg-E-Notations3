
@Imports Syncfusion.EJ2
@Imports Syncfusion.EJ2.Navigations

@*@code
    Layout = "_Layout3.vbhtml"
End Code*@

    @*<head>

        @Html.DevExpress().GetStyleSheets(
                                                                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
                                                                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.RichEdit},
                                                                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
                                                                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Scheduler},
                                                                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
                                                                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid},
                                                                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.HtmlEditor},
                                                                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Chart},
                                                                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Report},
                                                                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.TreeList})

        @Html.DevExpress().GetScripts(
                                               New Script With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
                                               New Script With {.ExtensionSuite = ExtensionSuite.RichEdit},
                                               New Script With {.ExtensionSuite = ExtensionSuite.Editors},
                                               New Script With {.ExtensionSuite = ExtensionSuite.Scheduler},
                                               New Script With {.ExtensionSuite = ExtensionSuite.GridView},
                                               New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid},
                                               New Script With {.ExtensionSuite = ExtensionSuite.HtmlEditor},
                                               New Script With {.ExtensionSuite = ExtensionSuite.Editors},
                                               New Script With {.ExtensionSuite = ExtensionSuite.Chart},
                                               New Script With {.ExtensionSuite = ExtensionSuite.Report},
                                               New Script With {.ExtensionSuite = ExtensionSuite.TreeList})


    </head>*@
<script type="text/javascript">
    $(document).ready(function () {

        var treeObj2 = new ej.navigations.TreeView({ fields: { id: 'NodeId', text: 'NodeText', child: 'NodeChild', iconCss: 'Icon', Expanded: 'Expanded' }, nodeSelected: onNodeSelected });
        treeObj2.appendTo('#tree2');

        $("#InvoiceID").on('change', function () {
            if (!RichEdit.InCallback())
                RichEdit.PerformCallback({ "InvoiceID": $("#InvoiceID").val() });
        });
    });
    function OnRichEditInit(s, e) {
        RichEdit.PerformCallback();
    }
</script>

@*<script src="~/ckeditor/ckeditor.js"></script>*@

@*<input type="hidden" id="iconFieldsjson" data-value="@ViewBag.iconFieldsjson" />*@
<input type="hidden" id="iconFieldsjson2" data-value="@ViewBag.iconFieldsjson2" />


<div style="float:left">
    <div class="treedata2">
        @Html.EJS().TreeView("tree1").Fields(ViewBag.iconFields2).ExpandOn(Syncfusion.EJ2.Navigations.ExpandOnSettings.DblClick).NodeSelected("onNodeSelected2").NodeClicked("onNodeClicked2").NodeExpanding("onNodeExpanded2").Render()
    </div>

    <div class="treedata3">
        <div id="tree2"></div>
    </div>
</div>

<div ID="DocumentView" style="width: 200px;height:600px;  float:left;margin-top:0px">
    <form>
        <div id="document"></div>
    </form>
</div>



<script type="text/javascript">
    var close = false;
    var iconFields


    //function OnClick(s, e) {
    //    //debugger;
    //    close = true;
    //    RichEdit.PerformCallback();
    //}


    function OnRichEditBeginCallback(s, e) {
        // debugger;
        // e.customArgs["close"] = close;
        // close = false;

        //e.customArgs["filePath"] = filePath;
        //filePath = null;
    }
    function OnRichEditEndCallback(s, e) {
        //debugger;
        //if (filePath != null)
        //    RichEdit.PerformCallback();
    }


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
                    Policies.push(obj);
                });
            
                var tree2 = ej.base.getComponent(document.querySelector('#tree2'), 'treeview');
                tree2.fields.dataSource = Policies
                iconFields=Policies

                //var treeObj = new ej.navigations.TreeView({ fields: { dataSource: response, id: 'NodeId', text: 'NodeText', child: 'NodeChild' } });
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
    var SelectedFacility


    function onNodeCollapsed2(args) {
    }

    function onNodeExpanded2(args) {
    }

    function onNodeClicked2(args) {
    }

    function onNodeSelected2(args) {    
        //debugger;
        let SelectedNode = iconFields2.find(g => g.NodeText === args.nodeData.text);
        SelectedFacility = SelectedNode
        loadDocIndex(SelectedNode.FullPath, SelectedNode.NodeText)
    }

    function onNodeCollapsed(args) {
    }

    function onNodeExpanded(args) {
    }

    function onNodeSelected(args) {
    }

    function onNodeClicked(args) {
    }

    function onNodeSelected(args)    {   
 
        var treeObj = document.getElementById('tree2').ej2_instances[0];   
        var iconFields = treeObj.treeData[args.nodeData.parentID-1]
        let SelectedNode = iconFields.NodeChild.find(g => g.NodeText === args.nodeData.text);
        loadEfile(SelectedNode.FullPath, SelectedFacility.FullPath, SelectedFacility.NodeText);

        //loadEfile(args.nodeData.text, SelectedFacility.FullPath, SelectedFacility.NodeText);

     
   


     
      
 
        //var targetNode = document.getElementById('tree').querySelector('[data-uid="' + targetNodeId + '"]');

        //var offsetHeight = targetNode.offsetHeight;
        //var offsetWidth = args.node.offsetWidth;
        //var offsetLeft = args.node.offsetLeft;
        //var offsetTop = args.node.offsetTop;

        // $("#DocumentView").css('margin-top', offsetTop);



        //var data = new ej.data.DataManager(treeObj.fields.dataSource).executeLocal(new ej.data.Query().where('isTable', 'equal', true));



        //$.post('/Policies/OpenFile', SelectedNode.FullPath)
        //    .done(function (data) {
        //        if (data.length = 'True') {
        //        }
        //    });



        //    if (args.nodeData.text == 'Facilities') {
        //    };

        //    if (args.nodeData.text == 'Users') {
        //    };
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
        }

        // Handle array which we just traverse and recurse.
        if (data.constructor === Array) {
            for (var i = 0, len = data.length; i < len; i++) {
                results = results.concat(recursiveKeySearch(key, data[i], caseSensitive));
            }
            return results;
        }

        // We know we have an general object to work with now.
        // Now we need to iterate keys
        for (var dataKey in data) {
            var isMatch = false;
            if (caseSensitive === false) {
                isMatch = (comparisonKey === dataKey.toLowerCase());
            } else {
                isMatch = (comparisonKey === dataKey);
            }

            if (isMatch) {
                results.push(data[dataKey]);
            }

            // now recurse into value at key
            results = results.concat(recursiveKeySearch(key, data[dataKey], caseSensitive));
        }

        return results;
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

        .e-treeview .e-list-icon.ppt {
            background-position: -197px -48px;
        }

        .e-treeview .e-list-icon.pdf {
            background-position: -197px -104px;
        }

        .e-treeview .e-list-icon.images {
            background-position: -197px -132px;
        }

        .e-treeview .e-list-icon.zip {
            background-position: -197px -188px;
        }

        .e-treeview .e-list-icon.audio {
            background-position: -197px -244px;
        }

        .e-treeview .e-list-icon.video {
            background-position: -197px -272px;
        }

        .e-treeview .e-list-icon.exe {
            background-position: -197px -412px;
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
        max-width: 400px;
        min-width: 350px;
        margin: 0px;
        margin-top: 10px;
        border: 1px solid #dddddd;
        border-radius: 3px;
        max-height: 100px;
        overflow: auto;
        min-height: 50px;
        margin-right: 10px
    }

    .treedata3 {
        max-width: 400px;
        min-width: 350px;
        margin: 0px;
        margin-top: 10px;
        border: 1px solid #dddddd;
        border-radius: 3px;
        max-height: 400px;
        overflow: auto;
        min-height: 50px;
        margin-right: 10px
    }
</style>
@Html.EJS().ScriptManager()
