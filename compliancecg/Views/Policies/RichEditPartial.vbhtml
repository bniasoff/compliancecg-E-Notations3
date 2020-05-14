@Imports CCGData.CCGData
@modeltype compliancecg.Models.AllRichEditSettings

@*<script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>*@

@code
' Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@*<script type="text/javascript">
        var filePath = null;
        function OnSelectedFileChanged(s, e) {
            debugger;
            if (e.file) {
                filePath = s.GetCurrentFolderPath() + "\\" + e.file.name;
                if (!RichEdit.InCallback())
                    RichEdit.PerformCallback({ "filePath": filePath });
            }
        }



        function onKeyDown(s, e) {
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            var ctrlDown = e.htmlEvent.ctrlKey || e.htmlEvent.metaKey;
            if (ctrlDown && keyCode === 67) { e.handled = true; }
            if (ctrlDown && keyCode === 88) { e.handled = true; }
        }


        function Insert(s, e) {
            var subDocumentId = richEdit.document.activeSubDocument.id;
            var insertPosition = 75;
            var interval = new ASPx.Interval(insertPosition, 0);
            richEdit.commands.insertText.execute("an example text", insertPosition, subDocumentId);
            richEdit.commands.insertText.execute("an example text", insertPosition);
            richEdit.commands.insertText.execute("an example text", interval, subDocumentId);
            richEdit.commands.insertText.execute("an example text", interval);
            richEdit.commands.insertText.execute("an example text");
        }


    </script>*@



@code
    Dim DataRepository As New CCGData.DataRepository
    Dim FacilityGroup As CCGData.CCGData.FacilityGroup = Nothing

    If Session("FacilityGroup") IsNot Nothing Then
        Dim FacilityGrouptmp As FacilityGroup = Session("FacilityGroup")

        FacilityGroup = DataRepository.GetFacilityGroup(FacilityGrouptmp.FacilityGroupID)
    End If



    If Model.EFile IsNot Nothing Then
        Dim fileName As String = If(Model.EFile.FriendlyName Is Nothing, Model.EFile.Name, Model.EFile.FriendlyName)
        Dim docFormat As DevExpress.XtraRichEdit.DocumentFormat

        Select Case (Model.EFile.Extension.ToLower())
            Case "rtf"
                docFormat = DevExpress.XtraRichEdit.DocumentFormat.Rtf
            Case "txt"
                docFormat = DevExpress.XtraRichEdit.DocumentFormat.PlainText
            Case "doc"
                docFormat = DevExpress.XtraRichEdit.DocumentFormat.Doc
            Case "docx"
                docFormat = DevExpress.XtraRichEdit.DocumentFormat.OpenXml
            Case Else
                docFormat = DevExpress.XtraRichEdit.DocumentFormat.OpenXml
        End Select
    End If

End Code


@*<div style="padding-bottom: 30px;">
        @Html.DevExpress().Ribbon(Sub(settings)
                                      settings.Name = "ExternalRibbon"
                                      settings.ShowFileTab = False
                                      settings.Tabs.Add(compliancecg.Controllers.PoliciesController.GetCustomRibbonTab(True))
                                  End Sub).GetHtml()
    </div>*@



@Html.DevExpress().RichEdit(Sub(settings)
                                     settings.Name = "RichEdit"
                                     'settings.CustomActionRouteValues = New With {.Controller = "Policies", .Action = "OpenFile"}
                                     settings.CallbackRouteValues = New With {.Controller = "Policies", .Action = "DocFileViewerCallback"}
                                     settings.Height = WebControls.Unit.Pixel(800)
                                     'settings.Width = WebControls.Unit.Pixel(850)

                                     'settings.ClientSideEvents.BeginCallback = "OnRichEditBeginCallback"
                                     'settings.ClientSideEvents.EndCallback = "OnRichEditEndCallback"

                                     settings.Settings.Behavior.Assign(Model.RichEditSettingOptions.BehaviorSettings)
                                     settings.Settings.DocumentCapabilities.Assign(Model.RichEditSettingOptions.DocumentCapabilitiesSettings)
                                     'settings.RibbonMode = Model.RichEditSettings.RichEditRibbonMode
                                     settings.ReadOnly = Model.RichEditSettings.ReadOnly
                                     settings.ViewMergedData = True


                                     If Model.AllowPrint Then
                                         settings.Settings.Behavior.Printing = DevExpress.XtraRichEdit.DocumentCapability.Enabled
                                         settings.RibbonTabs.Add(compliancecg.Controllers.PoliciesController.GetCustomRibbonTab(True, Session("FacilityGroup")))
                                     Else
                                         settings.RibbonMode = Model.RichEditSettings.RichEditRibbonMode
                                     End If

                                     'settings.Settings.Behavior.Printing = DevExpress.XtraRichEdit.DocumentCapability.Enabled

                                     'If FacilityGroup IsNot Nothing Then
                                     '    'FacilityGroup = Session("FacilityGroup")
                                     '    If FacilityGroup.AllowPolicyPrint Then

                                     '        settings.Settings.Behavior.Printing = DevExpress.XtraRichEdit.DocumentCapability.Enabled
                                     '        settings.RibbonTabs.Add(compliancecg.Controllers.PoliciesController.GetCustomRibbonTab(True, Session("FacilityGroup")))
                                     '    Else
                                     '        settings.RibbonMode = Model.RichEditSettings.RichEditRibbonMode
                                     '    End If
                                     'Else
                                     '    settings.RibbonMode = Model.RichEditSettings.RichEditRibbonMode
                                     'End If



                                     'settings.Settings.Behavior.Printing = DocumentCapability.Enabled
                                     'settings.Settings.Behavior.PrintingAllowed = True

                                     ' settings.ClientSideEvents.Init = "function(s, e) { RichEdit.commands.openFindPanel.execute(); }"
                                     settings.ClientSideEvents.Init = "onRichInit"


                                     'settings.Settings.DocumentCapabilities.
                                     'settings.RichEditSettingOptions.BehaviorSettings. = DocumentCapability.Enabled
                                     ' settings.Tabs.Add(RibbonCustomizationDemoHelper.GetCustomRibbonTab(True));

                                     'settings.Settings.RangePermissions.HighlightColor = System.Drawing.Color.DarkGreen
                                     'settings.Settings.RangePermissions.HighlightBracketsColor = System.Drawing.Color.DarkBlue
                                     'settings.Settings.RangePermissions.Visibility = True


                                     '  settings.Settings.RangePermissions.Visibility = Model.Visibility ? RichEditRangePermissionVisibility.Visible : RichEditRangePermissionVisibility.Hidden

                                     '  settings.ClientSideEvents.KeyDown = "onKeyDown"

                                     'settings.ClientSideEvents.PopupMenuShowing = "function(s, e) { RichEditPopupMenuShowing(s, e); }"
                                     'settings.ClientSideEvents.CustomCommandExecuted = "function(s, e) { RichEditCustomCommandExecuted(s, e); }"


                                     ' settings.WorkDirectory = DirectoryManagmentUtils.DocumentBrowsingFolderPath
                                     'settings.ShowConfirmOnLosingChanges = False

                                     'settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "CustomDocumentManagementPartial"}
                                     'settings.ClientSideEvents.Init = "function(s, e) { s.SetFullscreenMode(true); }"
                                     ' settings.ClientSideEvents.BeginCallback = "function (s,e) { lp.Show(); }"
                                     ' settings.ClientSideEvents.EndCallback = "function (s,e) { lp.Hide(); }"

                                     'settings.Width = Unit.Percentage(100)
                                     'settings.Width = System.Web.UI.WebControls.Unit.Percentage(50)
                                     'settings.ShowConfirmOnLosingChanges = False
                                     'settings.ActiveTabIndex = 0

                                     'settings.ClientSideEvents.ActiveSubDocumentChanged = "function(s, e) { DXEventMonitor.Trace(s, e, 'ActiveSubDocumentChanged') }"
                                     'settings.ClientSideEvents.BeginSynchronization = "function(s, e) { DXEventMonitor.Trace(s, e, 'BeginSynchronization') }"
                                     'settings.ClientSideEvents.CharacterPropertiesChanged = "function(s, e) { DXEventMonitor.Trace(s, e, 'CharacterPropertiesChanged') }"
                                     'settings.ClientSideEvents.DocumentChanged = "function(s, e) { DXEventMonitor.Trace(s, e, 'DocumentChanged') }"
                                     'settings.ClientSideEvents.DocumentLoaded = "function(s, e) { DXEventMonitor.Trace(s, e, 'DocumentLoaded') }"
                                     'settings.ClientSideEvents.EndSynchronization = "function(s, e) { DXEventMonitor.Trace(s, e, 'EndSynchronization') }"
                                     'settings.ClientSideEvents.HyperlinkClick = "function(s, e) { DXEventMonitor.Trace(s, e, 'HyperlinkClick') }"
                                     'settings.ClientSideEvents.Init = "function(s, e) { DXEventMonitor.Trace(s, e, 'Init') }"
                                     'settings.ClientSideEvents.KeyDown = "function(s, e) { DXEventMonitor.Trace(s, e, 'KeyDown') }"
                                     'settings.ClientSideEvents.KeyUp = "function(s, e) { DXEventMonitor.Trace(s, e, 'KeyUp') }"
                                     'settings.ClientSideEvents.PointerDown = "function(s, e) { DXEventMonitor.Trace(s, e, 'PointerDown') }"
                                     'settings.ClientSideEvents.PointerUp = "function(s, e) { DXEventMonitor.Trace(s, e, 'PointerUp') }"
                                     'settings.ClientSideEvents.LostFocus = "function(s, e) { DXEventMonitor.Trace(s, e, 'LostFocus') }"
                                     'settings.ClientSideEvents.GotFocus = "function(s, e) { DXEventMonitor.Trace(s, e, 'GotFocus') }"
                                     'settings.ClientSideEvents.ParagraphPropertiesChanged = "function(s, e) { DXEventMonitor.Trace(s, e, 'ParagraphPropertiesChanged') }"
                                     'settings.ClientSideEvents.PopupMenuShowing = "function(s, e) { DXEventMonitor.Trace(s, e, 'PopupMenuShowing') }"
                                     'settings.ClientSideEvents.SelectionChanged = "function(s, e) { DXEventMonitor.Trace(s, e, 'SelectionChanged') }"
                                     'settings.ClientSideEvents.ContentInserted = "function(s, e) { DXEventMonitor.Trace(s, e, 'ContentInserted') }"
                                     'settings.ClientSideEvents.ContentRemoved = "function(s, e) { DXEventMonitor.Trace(s, e, 'ContentRemoved') }"




                                     settings.ShowConfirmOnLosingChanges = False
                                     'settings.ActiveTabIndex = 4

                                     settings.CalculateDocumentVariable = Sub(s, e)
                                                                              Select Case (e.VariableName)
                                                                                  Case "test2"
                                                                                      e.Value = "Benyomin Niasoff2"
                                                                                      e.Handled = True
                                                                                  Case "CLIENT"
                                                                                      e.Value = "Client-Benyomin Niasoff"
                                                                                      e.Handled = True
                                                                              End Select
                                                                          End Sub

                                     settings.PreRender = Sub(s, e)
                                                              Dim RichEdit As MVCxRichEdit = CType(s, MVCxRichEdit)
                                                              ' RichEditDemoUtils.HideFileTab(RichEdit)
                                                              RichEdit.Focus()
                                                          End Sub

                                     'settings.Saving = Sub(s, e)
                                     '                      Dim docBytes As Byte() = RichEditExtension.SaveCopy("RichEditName", DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
                                     '                      DXWebApplication1.Models.DataHelper.SaveDocument(docBytes)
                                     '                      e.Handled = True
                                     '                  End Sub
                                     'End Sub).Open(Server.MapPath("~/App_Data/Policies/Blank2.docx")).GetHtml()
                                 End Sub).Open(Model.EFile.FriendlyName, DevExpress.XtraRichEdit.DocumentFormat.OpenXml, Function() Model.EFile.Data).GetHtml()

<script type="text/javascript">
    function onRichInit(s) {

        RichEdit.commands.openFindPanel.execute();
        if (ASPx.Browser.Chrome && ASPx.Browser.Version >= 77) {
            var createHelperFrame = s.createHelperFrame;
            s.createHelperFrame = function () {
                var helperFrame = createHelperFrame.call(this);
                helperFrame.frameElement.addEventListener("load", function () {
                    if (helperFrame.frameElement.contentDocument.contentType === "application/pdf")
                        helperFrame.frameElement.contentWindow.print();
                });
                return helperFrame;
            }
        }
    }

    //function onRichInit(s,e) {

    //    debugger;
    //    function(s, e) { RichEdit.commands.openFindPanel.execute(); }
    //    RichEdit.commands.openFindPanel.execute();

    //    if (ASPx.Browser.Chrome && ASPx.Browser.Version >= 77) {

    //        var createHelperFrame = s.createHelperFrame;
    //        s.createHelperFrame = function () {
    //            var helperFrame = createHelperFrame.call(this);
    //            helperFrame.frameElement.addEventListener("load", function () {
    //                if (helperFrame.frameElement.contentDocument.contentType === "application/pdf")
    //                    helperFrame.frameElement.contentWindow.print();
    //            });
    //            return helperFrame;
    //        }
    //    }
    //}
</script>

@*<div>
    @FacilityGroup.AllowPolicyPrint.Value.ToString
</div>*@


@*RectangleF bounds = this.richEditControl1.GetBoundsFromPositionF(this.richEditControl1.Document.CaretPosition);
    float y = bounds.Top;
    float x = bounds.Left;*@

@*function Print() {
        rich.core.commandManager.getCommand(__aspxRichEdit.RichEditClientCommand.FilePrint).execute();
    }*@
@*richEdit.commands.filePrint.execute();*@
@*ribbon.onShortCutCommand(ASPxClientSpreadsheet.ServerCommands.getCommandIDByName('Print').id);*@

@*<script type="text/javascript">
        function onKeyDown(s, e) {
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            var ctrlDown = e.htmlEvent.ctrlKey || e.htmlEvent.metaKey;
            if (ctrlDown && keyCode === 67) { e.handled = true; }
            if (ctrlDown && keyCode === 88) { e.handled = true; }
        }
    </script>


    <script src="~/Scripts/DevExpress-RichEdit-ContextMenuCustomization.js"></script>
    <script src="~/Scripts/DevExpress-RichEdit-CustomToolBar.js"></script>*@
