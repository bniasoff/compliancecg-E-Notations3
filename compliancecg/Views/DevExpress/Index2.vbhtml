@*@Using Html.BeginForm("UploadControlForm", "DevExpress", FormMethod.Post)
        @Html.DevExpress().UploadControl(
                         Sub(settings)
                             settings.Name = "UploadControl"
                             settings.CallbackRouteValues = New With {.Controller = "DevExpress", .Action = "UploadControlUpload"}
                             settings.ShowUploadButton = True
                             settings.ShowProgressPanel = True

                             settings.UploadMode = UploadControlUploadMode.Standard
                             settings.ValidationSettings.Assign(compliancecg.CustomUploadControlValidationSettings.Settings)
                             settings.ClientSideEvents.FilesUploadComplete = "function(s, e) { CallbackPanel.PerformCallback( { path: e.callbackData }); }"
                         End Sub).GetHtml()
    End Using
    <br />*@
@*@Html.Action("CallbackPanelPartial")*@

@Html.DevExpress().GetStyleSheets(New StyleSheet With {.ExtensionSuite = ExtensionSuite.RichEdit})
@Html.DevExpress().GetScripts(New Script With {.ExtensionSuite = ExtensionSuite.RichEdit})













@Using (Html.BeginForm())
    @Html.Action("RichEditPartial")
End Using









