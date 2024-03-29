﻿
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
    If Model.EFile IsNot Nothing And Model.EFile.Name IsNot Nothing Then
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


@Html.DevExpress().RichEdit(Sub(settings)
                                     settings.Name = "RichEdit"
                                     'settings.CustomActionRouteValues = New With {.Controller = "Policies", .Action = "OpenFile"}
                                     settings.CallbackRouteValues = New With {.Controller = "Policies", .Action = "DocFileViewerCallback"}
                                     settings.Height = WebControls.Unit.Pixel(800)
                                     settings.Width = WebControls.Unit.Pixel(1000)

                                     'settings.ClientSideEvents.BeginCallback = "OnRichEditBeginCallback"
                                     'settings.ClientSideEvents.EndCallback = "OnRichEditEndCallback"


                                     settings.Settings.Behavior.Assign(Model.RichEditSettingOptions.BehaviorSettings)
                                     settings.Settings.DocumentCapabilities.Assign(Model.RichEditSettingOptions.DocumentCapabilitiesSettings)
                                     settings.RibbonMode = Model.RichEditSettings.RichEditRibbonMode
                                     settings.ClientSideEvents.DocumentLoaded = Model.RichEditClientSideEvents.DocumentLoaded

                                     settings.ReadOnly = Model.RichEditSettings.ReadOnly
                                     settings.ViewMergedData = True

                                     settings.ClientSideEvents.Init = "function(s, e) {RichEdit.commands.openFindPanel.execute(); }"
                                     settings.ClientSideEvents.DocumentLoaded = "function(s, e) { DXEventMonitor.Trace(s, e, 'DocumentLoaded') }"





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


                                     'settings.ClientSideEvents.HyperlinkClick = "function(s, e) { Dim RichEdit As MVCxRichEdit = CType(s, MVCxRichEdit) }"


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




                                     'settings.ShowConfirmOnLosingChanges = False
                                     'settings.ActiveTabIndex = 4

                                     'settings.CalculateDocumentVariable = Sub(s, e)
                                     '                                         Select Case (e.VariableName)
                                     '                                             Case "test2"
                                     '                                                 e.Value = "Benyomin Niasoff2"
                                     '                                                 e.Handled = True
                                     '                                             Case "CLIENT"
                                     '                                                 e.Value = "Client-Benyomin Niasoff"
                                     '                                                 e.Handled = True
                                     '                                         End Select
                                     '                                     End Sub

                                     settings.PreRender = Sub(s, e)
                                                              Dim RichEdit As MVCxRichEdit = CType(s, MVCxRichEdit)
                                                              'RichEdit.commands.openHyperlink.execute()
                                                              ' RichEditDemoUtils.HideFileTab(RichEdit)
                                                              RichEdit.Focus()
                                                          End Sub

                                     'settings.Init = Sub(s, e)
                                     '                    Dim RichEdit As MVCxRichEdit = CType(s, MVCxRichEdit)

                                     '                    ' RichEditDemoUtils.HideFileTab(RichEdit)
                                     '                    RichEdit.Focus()
                                     '                End Sub


                                     'settings.ClientSideEvents.HyperlinkClick = Sub(s, e)
                                     '                                               Dim RichEdit As MVCxRichEdit = CType(s, MVCxRichEdit)
                                     '                                               RichEdit.commands.openHyperlink.execute()

                                     '                                           End Sub






                                     '{
                                     '    var rich = s as MVCxRichEdit;  
                                     '    rich.CreateDefaultRibbonTabs(True);  
                                     '    var Tab = rich.RibbonTabs[0];  

                                     '    var customItem = New RibbonButtonItem() {Text = "Schließen", Name = "Schließen", Size = RibbonItemSize.Large};  
                                     '    customItem.LargeImage.IconID = DevExpress.Web.ASPxThemes.IconID.ActionsClose32x32gray;  
                                     '    Tab.Groups[0].Items.Insert(0, customItem);  

                                     '};  


                                     'settings.Saving = Sub(s, e)
                                     '                      Dim docBytes As Byte() = RichEditExtension.SaveCopy("RichEditName", DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
                                     '                      DXWebApplication1.Models.DataHelper.SaveDocument(docBytes)
                                     '                      e.Handled = True
                                     '                  End Sub
                                     'End Sub).Open(Server.MapPath("~/App_Data/Test.docx")).GetHtml()
                                     'End Sub).GetHtml()
                                 End Sub).Open(Model.EFile.FriendlyName, DevExpress.XtraRichEdit.DocumentFormat.OpenXml, Function() Model.EFile.Data).GetHtml()





@*<script type="text/javascript">
        function ClickMe() {
            debugger;
            var bookmarks = RichEdit.core.model.subDocuments[0].bookmarks;
            for (var i = 0; i < bookmarks.length; i++) {
                if (bookmarks[i].name === "bookmark1") {
                    var start = bookmarks[i].start.value;
                    var end = bookmarks[i].end.value;

                    rich.core.selection.setSelection(start, end, false, -1, 0);
                }
            }
        }
    </script>*@

@*RectangleF bounds = this.richEditControl1.GetBoundsFromPositionF(this.richEditControl1.Document.CaretPosition);
    float y = bounds.Top;
    float x = bounds.Left;*@





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