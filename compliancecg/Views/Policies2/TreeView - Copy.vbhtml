
@Imports Syncfusion.EJ2
@Imports Syncfusion.EJ2.Navigations


<script type="text/javascript">
    $(document).ready(function () {

        var treeObj2 = new ej.navigations.TreeView({ fields: { id: 'NodeId', text: 'NodeText', child: 'NodeChild', iconCss: 'Icon', Expanded: 'Expanded' }, nodeSelected: onNodeSelected });
        treeObj2.appendTo('#tree2');

        //$("#InvoiceID").on('change', function () {

        //    if (!RichEdit.InCallback())
        //        RichEdit.PerformCallback({ "InvoiceID": $("#InvoiceID").val() });
        //});
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
    <form >
        <div style="margin-bottom:20px;">
            @*@Html.EJS().ProgressButton("spinleft").Content("Spin Left").IsPrimary(True).Render()*@
            @*@Html.EJS().ProgressButton("progress").Content("Progress").EnableProgress(True).Progress("progress").End("end").CssClass("e-hide-spinner").Render()*@
        </div>


        @*@Html.DevExpress().LoadingPanel(Sub(settings)
                                            settings.Name = "lp"
                                            settings.Modal = True
                                        End Sub).GetHtml()*@

        <div id="document" style="display:block;height:800px"> @Html.Partial("../Policies/RichEditPartial")</div>
        @*<div id="document"></div>*@
    </form>
</div>



@*<script type="text/javascript">

    function OnLabelInit(s, e) {
        debugger;
        lp.Hide();
    }

    function OnRichEditEndCallback(s, e) {
         debugger;
        lp.Hide();
    }

    function OnRichEditBeginCallback(s, e) {
         debugger;
        lp.Show();
    }
</script>*@


@*<script>


    function end() {
        debugger;
        var progressBtn = ej.base.getComponent(document.querySelector("#progress"), "progress-btn");
        progressBtn.content = 'Download';
        progressBtn.iconCss = 'e-btn-sb-icon e-download';
        progressBtn.dataBind();
    }

    function created() {
        var progressBtn = ej.base.getComponent(document.querySelector("#progress"), "progress-btn");
        progressBtn.element.addEventListener('click', clickHandler);
    }

    function clickHandler() {
        var progressBtn = ej.base.getComponent(document.querySelector("#progress"), "progress-btn");
        if (progressBtn.content === 'Download') {
            progressBtn.content = 'Pause';
            progressBtn.iconCss = 'e-btn-sb-icon e-pause';
            progressBtn.dataBind();
        }
        // clicking on ProgressButton will stop the progress when the text content is 'Pause'
        else if (progressBtn.content === 'Pause') {
            progressBtn.content = 'Resume';
            progressBtn.iconCss = 'e-btn-sb-icon e-play';
            progressBtn.dataBind();
            progressBtn.stop();
        }
        // clicking on ProgressButton will start the progress when the text content is 'Resume'
        else if (progressBtn.content === 'Resume') {
            progressBtn.content = 'Pause';
            progressBtn.iconCss = 'e-btn-sb-icon e-pause';
            progressBtn.dataBind();
            progressBtn.start();
        }
    }
</script>*@



@*<script>
    function begin(args) {

        this.content = 'Progress ' + args.percent + '%';
        this.dataBind();
    }

    function progress(args) {
              this.content = 'Progress ' + args.percent + '%';
        this.dataBind();
        if (args.percent === 40) {
            args.percent = 90
        }
    }

    function end() {      
        this.content = 'Success';
        this.cssClass = 'e-hide-spinner e-success';
        this.dataBind();
        setTimeout(() => {
            this.content = 'Upload';
            this.cssClass = 'e-hide-spinner';
            this.dataBind();
        }, 500)
    }


    //function end(args) {
    //    debugger;


    //    this.content = 'Progress ' + args.percent + '%';
    //    this.dataBind();
    //}
</script>*@


<script type="text/javascript">
    var close = false;
    var iconFields


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
        debugger;
        //if (filePath != null)
        //    RichEdit.PerformCallback();
    }


    //function loadEfile(file, State, FacilityName) {
    //    $.ajax({
    //        type: "POST",
    //        cache: false,
    //        async: true,
    //        datatype: 'json',
    //        url: '../Policies/LoadDocument?FileName=' + file + '&State=' + State + '&SelectedFacilityName=' + FacilityName,
    //        contentType: 'application/json',
    //        success: function (response) {
    //            $('#document').html(response);
    //            return false;
    //        },
    //        error: function (xhr, status, message) {
    //        }
    //    });
    //    return false;
    //}


    function loadEfile2(State, FacilityName) {
        $.ajax({
            type: "POST",
            cache: false,
            async: true,
            datatype: 'json',
            url: '../Policies/LoadDocument4?State=' + State + '&SelectedFacilityName=' + FacilityName,
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

        loadEfile2(State, FacilityName);

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

                //var tree2 = ej.base.getComponent(document.querySelector('#tree2'), 'treeview');
                //tree2.fields.dataSource = Policies
                //iconFields = Policies
               
             //var progressBtn = ej.base.getComponent(document.querySelector("#progress"), "progress-btn");
             //   progressBtn.percent = 100
             //   progressBtn.stop();




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
       // var progressBtn = ej.base.getComponent(document.querySelector("#progress"), "progress-btn");
       // progressBtn.content = 'Pause';
       // progressBtn.iconCss = 'e-btn-sb-icon e-pause';
       // progressBtn.dataBind();
       // progressBtn.start();
       //progressBtn.percent = 20
        // progressBtn.element.addEventListener('click', clickHandler);

        //var ProgressBar = document.getElementById('progress').ej2_instances[0];
       
        let SelectedNode = iconFields2.find(g => g.NodeText === args.nodeData.text);
        SelectedFacility = SelectedNode

        
       // progressBtn.percent = 40


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

    function onNodeSelected(args) {
        var treeObj = document.getElementById('tree2').ej2_instances[0];
        var iconFields = treeObj.treeData[args.nodeData.parentID - 1]
        let SelectedNode = iconFields.NodeChild.find(g => g.NodeText === args.nodeData.text);

        var bkmInfos = RichEdit.document.activeSubDocument.bookmarksInfo;
        for (var bkmIndex = 0, bookmark; bookmark = bkmInfos[bkmIndex]; bkmIndex++)
            if (bookmark.name === SelectedNode.FullPath) {
                RichEdit.commands.goToBookmark.execute(bookmark.name);
            }


        // loadEfile(SelectedNode.FullPath, SelectedFacility.FullPath, SelectedFacility.NodeText);




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
    function GetBookMark() {
        initBookmark()
    }

    function initBookmark(rich, e) {
        __aspxRichEdit.Log.isEnabled = false;

        //RichEdit.commands.setFullscreen.execute(true);
        //RichEdit.commands.showTableGridLines.execute(true);
        //RichEdit.commands.showHiddenSymbols.execute(true);


        var makeInterval = function (start, length) {
            return { start: start, length: length };
        }

        var makeBookmarkInterval = function (bookmark) {
            return makeInterval(bookmark.start, bookmark.length);
        }

        //RichEdit.commands.insertText.execute("bookmark1, bookmark2, bookmark3");

        //for (var bkmIndex = 0; bkmIndex < 3; bkmIndex++)
        //    RichEdit.commands.insertBookmark.execute("bookmark" + (bkmIndex + 1).toString(), bkmIndex * 11, 9);

        var bkmInfos = RichEdit.document.activeSubDocument.bookmarksInfo;
        for (var bkmIndex = 0, bookmark; bookmark = bkmInfos[bkmIndex]; bkmIndex++)
            if (bookmark.name === "BookMark3") {

                RichEdit.commands.goToBookmark.execute(bookmark.name);
                // RichEdit.selection.intervals = [makeBookmarkInterval(bookmark)];
                // break;
            }
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


<style>
    @@font-face {
        font-family: 'btn-icon';
        src: url(data:application/x-font-ttf;charset=utf-8;base64,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) format('truetype');
        font-weight: normal;
        font-style: normal;
    }

    .e-btn-sb-icon {
        font-family: 'btn-icon' !important;
        speak: none;
        font-size: 55px;
        font-style: normal;
        font-weight: normal;
        font-variant: normal;
        text-transform: none;
        line-height: 1;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
    }

    .e-download::before {
        content: '\e706';
    }

    .e-play::before {
        content: '\e700';
    }

    .e-pause::before {
        content: '\e701';
    }
</style>
@Html.EJS().ScriptManager()
