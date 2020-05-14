@*@Html.DevExpress().FileManager(Sub(settings)

                                            settings.Name = "fileManager"
                                            settings.CallbackRouteValues = New With {.Controller = "DocumentManagement", .Action = "CustomDocumentManagementFileManagerPartial"}
                                            settings.DownloadRouteValues = New With {.Controller = "DocumentManagement", .Action = "DownloadFile"}

                                            settings.Height = 220
                                            settings.Settings.AllowedFileExtensions = New String() {".docx", ".doc", ".rtf", ".txt"}
                                            settings.ClientSideEvents.SelectedFileChanged = "OnSelectedFileChanged"

                                            '  settings.SettingsUpload.Enabled = !Utils.IsSiteMode
                                            settings.SettingsEditing.AllowDownload = True

                                            settings.PreRender = Sub(s, e)
                                                                     Dim fileManager As MVCxFileManager = CType(s, MVCxFileManager)
                                                                     Dim File As FileManagerFile = fileManager.SelectedFolder.GetFiles().FirstOrDefault()
                                                                     If (File Is Nothing) Then
                                                                         fileManager.SelectedFile = File
                                                                     End If
                                                                 End Sub


                                            settings.FileUploading = Sub(s, e)
                                                                         e.Cancel = Utils.IsSiteMode
                                                                         e.ErrorText = Utils.GetReadOnlyMessageText()
                                                                     End Sub

    ).BindToFolder(DirectoryManagmentUtils.DocumentBrowsingFolderPath).GetHtml()

    <script type="text/javascript">
        var postponedCallbackRequired = false;
        function OnSelectedFileChanged(s, e) {
            if (e.file) {
                if (!DemoRichEdit.InCallback())
                    DemoRichEdit.PerformCallback();
                else
                    postponedCallbackRequired = true;
            }
        }
        function OnRichEditEndCallback(s, e) {
            if (postponedCallbackRequired) {
                DemoRichEdit.PerformCallback();
                postponedCallbackRequired = false;
            }
        }
        function OnExceptionOccurred(s, e) {
            e.handled = true;
            alert(e.message);
            window.location.reload();
        }
    </script>*@


<script type="text/javascript">
    function uploadStarted(s, e) {
        var fileNames = e.fileName.split(", ");
        var existsFiles = fileManager.GetItems();

        var founded = [];

        for (var i = 0; i < existsFiles.length; i++) {
            var ef = existsFiles[i];
            for (var j = 0; j < fileNames.length; j++) {
                if (ef.name.toLowerCase() == fileNames[j].toLowerCase())
                    founded.push(ef.name);
            }
        }
        if (founded.length > 0) {
            if (confirm("Overwrite " + founded.length + " files?")) {
                document.getElementById('overwrite').value = true;
                return
            }
            e.cancel = true;
        }
        document.getElementById('overwrite').value = false;
    }
</script>

@Using (Html.BeginForm("FileManagerForm", "UploadControl", FormMethod.Post))
    @Html.Hidden("overwrite")
    @Html.Action("FileManagerPartial")
End Using

