
@Imports Syncfusion.EJ2
@Imports Syncfusion.EJ2.Navigations

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

@*@using (Html.BeginForm()) {
    <div style = "padding-bottom: 10px;" >
    <label for="InvoiceID">Create customer list for invoice:&nbsp;</label>
    @Html.DropDownList(
        "InvoiceID",
         new SelectList(new[] { 0, 1, 2 }, Session["InvoiceID"])
    )
        </div>




    @Html.Action("RichEditPartial")
    }*@





@*<script src="~/ckeditor/ckeditor.js"></script>*@

<input type="hidden" id="iconFieldsjson" data-value="@ViewBag.iconFieldsjson" />

@*<div class="control_wrapper">*@

<div style="float:left">
    <div ID="TreeView1" class="treedata" style="display:block;clear:left">
        @Html.EJS().TreeView("tree").Fields(ViewBag.iconFields).ExpandOn(Syncfusion.EJ2.Navigations.ExpandOnSettings.DblClick).NodeSelected("onNodeSelected").NodeClicked("onNodeClicked").NodeExpanding("onNodeExpanded").Render()
    </div>
</div>


@*<script type="text/javascript">
        $(function () {
            $("#tree1").ejTreeView({
                showCheckbox: true,
                allowEditing: true,
                width: "100%",
                height: "100%",
                keyPress: function (args) {
                    args.currentElement && args.currentElement.find("a.e-text")[0].scrollIntoView(false); //the bottom of the element will be aligned to the bottom of the visible area of the scrollable ancestor
                }
            });
            //Control focus key
            $(document).on("keydown", function (e) {
                if (e.altKey && e.keyCode === 74) {
                    // j- key code.
                    $(".e-treeview.e-js").focus();
                }
            });
        });
    </script>*@


<script type="text/javascript">



    var close = false;

    //function OnClick(s, e) {
    //    //debugger;
    //    close = true;
    //    RichEdit.PerformCallback();
    //}


    function OnRichEditBeginCallback(s, e) {
        debugger;
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




    function loadEfile(file) {
        $.ajax({
            type: "POST",
            cache: false,
            async: true,
            datatype: 'json',
            contentType: 'application/json',
            url: '../Policies/LoadDocument?FileName=' + file,

            success: function (response) {
                $('#document').html(response);
                return false;
            },
            error: function (xhr, status, message) {
            }
        });
        return false;
    }
</script>




<div ID="DocumentView" style="width: 200px;height:600px;  float:left;margin-top:0px">
    <form>
        <div id="document"></div>
    </form>
</div>




<script type="text/javascript">

    //   const JsonFind = require('json-find');
    /* ES6 */
    //  import JsonFind from 'json-find';


    var iconFields = $("#iconFieldsjson").data("value");


    function onNodeCollapsed(args) {
        //  debugger;

    }

    function onNodeExpanded(args) {
        // debugger;

    }

    function onKeyPress(args) {
       
        //  args.currentElement && args.currentElement.find("a.e-text")[0].scrollIntoView(false); //the bottom of the element will be aligned to the bottom of the visible area of the scrollable ancestor
    }

    $(document).on("keydown", function (e) {
   
        if (e.altKey && e.keyCode === 74) {
            // j- key code.
            $(".e-treeview.e-js").focus();
        }
    });
    s
    function onNodeSelected(args) {
        let SelectedNode = iconFields.find(g => g.NodeText === args.nodeData.text);

        var treeObj = document.getElementById('tree').ej2_instances[0];
        var targetNodeId = treeObj.selectedNodes[0];
        var targetNode = document.getElementById('tree').querySelector('[data-uid="' + targetNodeId + '"]');

        var offsetHeight = targetNode.offsetHeight;
        var offsetWidth = args.node.offsetWidth;
        var offsetLeft = args.node.offsetLeft;
        var offsetTop = args.node.offsetTop;

        // $("#DocumentView").css('margin-top', offsetTop);



        //  debugger;
        //101-36
        //104-144
        //106-216
        //108-288

        //201-72
        //202-108
        //203-144
        //var data = new ej.data.DataManager(treeObj.fields.dataSource).executeLocal(new ej.data.Query().where('isTable', 'equal', true));


        loadEfile(SelectedNode.FullPath)

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

    function onNodeClicked(args) {

        //debugger;
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
        float: left;
        margin-right: 10px
    }
</style>

@Html.EJS().ScriptManager()
