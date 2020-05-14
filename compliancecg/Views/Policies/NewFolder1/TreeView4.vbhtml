
@modeltype CCGData.CCGData.BlogTable
@Imports Syncfusion.EJ2
@Imports Syncfusion.EJ2.Navigations


<script src="~/ckeditor/ckeditor.js"></script>

<input type="hidden" id="iconFieldsjson" data-value="@ViewBag.iconFieldsjson" />
@*<div class="control_wrapper">*@
<div style="width: 500px; float:left;    margin-right:10px">
    @Html.EJS().TreeView("tree").Fields(ViewBag.iconFields).ExpandOn(Syncfusion.EJ2.Navigations.ExpandOnSettings.DblClick).NodeSelected("onNodeSelected").NodeClicked("onNodeClicked").NodeExpanding("onNodeExpanded").Render()
</div>

<div style="width: 1000px; float:left;">
    @Using Html.BeginForm("Create", "Home", FormMethod.Post, New With {.id = "CreateForm"})
        @*@<div Class="col-lg-12">
            Title
        </div>

        @<div Class="col-lg-12">
            @Html.TextBoxFor(Function(a) a.BlogTitle, New With {.class = "form-control "})
        </div>*@

        @*@<div Class="col-lg-12">
            Description
        </div>*@

        @<div Class="col-lg-12">
            @Html.TextBoxFor(Function(a) a.BlogDescription)
        </div>

        @<br />
        @<div Class="col-lg-12">
            <input type="submit" Class="btn btn-primary" value="Create" id="submit" />
        </div>
    End Using
</div>

<script>
    $(document).ready(function () {
        //    alert('test');


        //debugger;
        //initialize CKEditor by givin id of textarea
        CKEDITOR.replace('BlogDescription');
      //  CKEDITOR.config.extraPlugins = 'justify';

        //call on form submit
        $('#CreateForm').on('submit', function () {

            //get each instance of ckeditor
            for (instance in CKEDITOR.instances) {
                //update element
                CKEDITOR.instances[instance].updateElement();
            }

            //set updated value in textarea
            $('#BlogDescription').val(CKEDITOR.instances["BlogDescription"].getData());
        });
    })
</script>


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

    function onNodeSelected(args) {


        let SelectedNode = iconFields.find(g => g.NodeText === args.nodeData.text);

        $.post('/Policies/OpenFile', SelectedNode.FullPath)
            .done(function (data) {
                if (data.length = 'True') {
                }
            });


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
@Html.EJS().ScriptManager()
