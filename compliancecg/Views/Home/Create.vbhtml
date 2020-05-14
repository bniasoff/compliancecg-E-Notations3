@modeltype  CCGData.CCGData.BlogTable

@Code
    ViewBag.Title = "Create"
    'Layout = Nothing
End code

<script src="~/ckeditor/ckeditor.js"></script>


@Using Html.BeginForm("Create", "Home", FormMethod.Post, New With {.id = "CreateForm"})
    @<div Class="col-lg-12">
        Title
    </div>

    @*@<div Class="col-lg-12">
        @Html.TextBoxFor(Function(a) a.BlogTitle, New With {.class = "form-control "})
    </div>

    @<div Class="col-lg-12">
        Description
    </div>

    @<div Class="col-lg-12">
        @Html.TextBoxFor(Function(a) a.BlogDescription)
    </div>*@

    @<br />
    @<div Class="col-lg-12">
        <input type="submit" Class="btn btn-primary" value="Create" id="submit" />
    </div>
End Using




<script>
    $(document).ready(function () {
        //    alert('test');


        //debugger;
        //initialize CKEditor by givin id of textarea
        CKEDITOR.replace('BlogDescription');
        CKEDITOR.config.extraPlugins = 'justify';

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
