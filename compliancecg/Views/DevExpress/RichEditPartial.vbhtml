@*@Html.DevExpress().GetStyleSheets(New StyleSheet With {.ExtensionSuite = ExtensionSuite.RichEdit})
@Html.DevExpress().GetScripts(New Script With {.ExtensionSuite = ExtensionSuite.RichEdit})*@

<script type="text/javascript">
    var filePath = null;
    function OnSelectedFileChanged(s, e) {
        debugger;
        if (e.file) {
            filePath = s.GetCurrentFolderPath() + "\\" + e.file.name;
            if (!RichEdit.InCallback())
                RichEdit.PerformCallback({ "filePath": filePath });
        }
    }
    function OnRichEditBeginCallback(s, e) {
        debugger;
        e.customArgs["filePath"] = filePath;
        filePath = null;
    }
    function OnRichEditEndCallback(s, e) {
        debugger;
        if (filePath != null)
            RichEdit.PerformCallback();
    }


    function onKeyDown(s, e) {
        var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
        var ctrlDown = e.htmlEvent.ctrlKey || e.htmlEvent.metaKey;
        if (ctrlDown && keyCode === 67) { e.handled = true; }
        if (ctrlDown && keyCode === 88) { e.handled = true; }
    }
</script>

@*<div>Test</div>*@
@Html.DevExpress().RichEdit(Sub(settings)
                                     settings.Name = "RichEdit"
                                     'settings.CustomActionRouteValues = New With {.Controller = "DevExpress", .Action = "OpenFile"}
                                     settings.CallbackRouteValues = New With {.Controller = "DevExpress", .Action = "RichEditPartial"}

                                     'settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "CustomDocumentManagementPartial"}
                                     'settings.ClientSideEvents.Init = "function(s, e) { s.SetFullscreenMode(true); }"
                                     ' settings.ClientSideEvents.BeginCallback = "function (s,e) { lp.Show(); }"
                                     ' settings.ClientSideEvents.EndCallback = "function (s,e) { lp.Hide(); }"



                                     'settings.ClientSideEvents.BeginCallback = "OnRichEditBeginCallback"
                                     'settings.ClientSideEvents.EndCallback = "OnRichEditEndCallback"

                                     'settings.ReadOnly = True
                                     'settings.ClientSideEvents.KeyDown = "onKeyDown"
                                     'settings.Settings.Behavior.Copy = False
                                     ' settings.Width = Unit.Pixel(700) 'Unit.Percentage(100)
                                     ''settings.Width = System.Web.UI.WebControls.Unit.Percentage(50)
                                     'settings.Height = Unit.Pixel(700)

                                     ''settings.ShowConfirmOnLosingChanges = False
                                     ''settings.ActiveTabIndex = 0

                                     'settings.RibbonMode = ASPxRichEdit.RichEditRibbonMode.None

                                     'settings.Saving = Sub(s, e)
                                     '                      Dim docBytes As Byte() = RichEditExtension.SaveCopy("RichEditName", DevExpress.XtraRichEdit.DocumentFormat.Doc)
                                     '                      compliancecg.Models.DataHelper.SaveDocument(docBytes)
                                     '                      e.Handled = True
                                     '                  End Sub

                                     'settings.Saving = Sub(s, e)
                                     '                      Dim docBytes As Byte() = RichEditExtension.SaveCopy("RichEditName", DevExpress.XtraRichEdit.DocumentFormat.Doc)
                                     '                      DXWebApplication1.Models.DataHelper.SaveDocument(docBytes)
                                     '                      e.Handled = True
                                     '                  End Sub
                                 End Sub).Open(Path.Combine("C:\CCG\temp\", "Test.docx")).GetHtml()



