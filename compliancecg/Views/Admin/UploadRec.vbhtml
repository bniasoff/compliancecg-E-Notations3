@Code
    ViewData("Title") = "Upload"
    Layout = Nothing
End Code


<!DOCTYPE html>
<html lang="en">
<head>
    <title>Essential JS 2 Uploader</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Essential JS 2 Uploader Component">
    <meta name="author" content="Syncfusion">
    <link href="index.css" rel="stylesheet">
    <link href="//cdn.syncfusion.com/ej2/ej2-base/styles/material.css" rel="stylesheet">
    <link href="//cdn.syncfusion.com/ej2/ej2-inputs/styles/material.css" rel="stylesheet">
    <link href="//cdn.syncfusion.com/ej2/ej2-popups/styles/material.css" rel="stylesheet">
    <link href="//cdn.syncfusion.com/ej2/ej2-buttons/styles/material.css" rel="stylesheet">


    <script src="https://cdn.syncfusion.com/ej2/dist/ej2.min.js" type="text/javascript"></script>
</head>


<body>

    @*<h2>Upload Files</h2>*@

   <div class="jumbotron">
            <h3>Recording Upload</h3>
   </div>

        @*<div class="row">
            <div class="col-md-4">
                @Using Html.BeginForm("Upload", "Policies", FormMethod.Post, New With {.enctype = "multipart/form-data"})
                    @<input type="file" name="files" />
                    @<input type="submit" value="OK" />
                End Using
            </div>
        </div>*@

    @*<div class="col-lg-8 control-section">
            <div class="control_wrapper">
                    @Html.EJS().Uploader("UploadFiles").AsyncSettings(New With Syncfusion.EJ2.Inputs.UploaderAsyncSettings { .SaveUrl = "http://localhost:64433/Home/Save", .RemoveUrl = "http://localhost:64433/Home/Remove" }).AutoUpload(False).Render()
            </div>
        </div>*@




    <div class="stackblitz-container material">
        <div class="col-lg-8 control-section">
            <div class="control_wrapper">
                <input type="file" id="fileupload" name="UploadFiles">
            </div>
        </div> 
    </div>

</body>
</html>

<script type="text/javascript">
    ej.base.enableRipple(true);

    var dropElement = document.getElementsByClassName('control-fluid')[0];
    var uploadObj = new ej.inputs.Uploader({
        asyncSettings: {
            saveUrl: '/Admin/SaveRec',
            removeUrl: '/Admin/remove'
        },
        autoUpload: false,
        removing: onFileRemove,
        dropArea: dropElement,
        upLoading: onFileUpload,
        success: onUploadSuccess,
        failure: onUploadFailure
    });
    uploadObj.appendTo('#fileupload');

    function onFileRemove(args) {
        args.postRawFile = false;
    }

    function onFileUpload(args) {
        debugger;
        if (args.operation === 'upload') {
            console.log('File uploaded successfully');
        }
    }

    function onUploadSuccess(args) {

        if (args.operation === 'upload') {
            console.log('File uploaded successfully');
        }
    }
    function onUploadFailure(args) {

        console.log('File failed to upload');
    }
</script>

<style>
    .control_wrapper {
        max-width: 400px;
        margin: auto;
    }

    .e-upload {
        width: 100%;
        position: relative;
        margin-top: 15px;
        float: none;
    }

        .e-upload .e-upload-actions {
            float: none;
            text-align: right;
        }

    .control_wrapper .e-upload .e-upload-drag-hover {
        margin: 0;
    }
</style>

