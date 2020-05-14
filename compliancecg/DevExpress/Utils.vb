'Imports Microsoft.VisualBasic
'Imports DevExpress.Web
'Imports DevExpress.Web.Demos
'Imports DevExpress.Web.Office
'Imports DevExpress.XtraRichEdit
'Imports System
'Imports System.IO
'Imports System.Web.UI.WebControls

'Partial Public Class RichEditDemoSettings
'    Inherits OfficeDemoPage
'    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
'        If (Not IsPostBack) Then
'            DemoRichEdit.Open(Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, "AynRand.docx"))
'            PrepareEditors()
'        End If
'    End Sub

'    Protected Sub btnApply_Click(ByVal sender As Object, ByVal e As EventArgs)
'        If (Not String.IsNullOrEmpty(DemoRichEdit.DocumentId)) Then
'            DocumentManager.FindDocument(DemoRichEdit.DocumentId).Close()
'        End If
'        DemoRichEdit.Settings.Behavior.Copy = CType(cmbCopy.Value, DocumentCapability)
'        DemoRichEdit.Settings.Behavior.CreateNew = CType(cmbCreateNew.Value, DocumentCapability)
'        DemoRichEdit.Settings.Behavior.Cut = CType(cmbCut.Value, DocumentCapability)
'        DemoRichEdit.Settings.Behavior.Drag = CType(cmbDrag.Value, DocumentCapability)
'        DemoRichEdit.Settings.Behavior.Drop = CType(cmbDrop.Value, DocumentCapability)
'        DemoRichEdit.Settings.Behavior.FullScreen = CType(cmbFullScreen.Value, DocumentCapability)
'        DemoRichEdit.Settings.Behavior.Open = CType(cmbOpen.Value, DocumentCapability)
'        DemoRichEdit.Settings.Behavior.PageBreakInsertMode = CType(cmbPageBreakInsertMode.Value, PageBreakInsertMode)

'        DemoRichEdit.Settings.Behavior.Paste = CType(cmbPaste.Value, DocumentCapability)
'        DemoRichEdit.Settings.Behavior.Printing = CType(cmbPrinting.Value, DocumentCapability)

'        DemoRichEdit.Settings.Behavior.Save = CType(cmbSave.Value, DocumentCapability)
'        DemoRichEdit.Settings.Behavior.SaveAs = CType(cmbSaveAs.Value, DocumentCapability)
'        DemoRichEdit.Settings.Behavior.TabMarker = CStr(tbTabMarker.Value)

'        DemoRichEdit.Settings.DocumentCapabilities.CharacterFormatting = CType(cmbCharacterFormatting.Value, DocumentCapability)
'        DemoRichEdit.Settings.DocumentCapabilities.CharacterStyle = CType(cmbCharacterStyle.Value, DocumentCapability)
'        DemoRichEdit.Settings.DocumentCapabilities.Fields = CType(cmbFields.Value, DocumentCapability)
'        DemoRichEdit.Settings.DocumentCapabilities.Hyperlinks = CType(cmbHyperlinks.Value, DocumentCapability)
'        DemoRichEdit.Settings.DocumentCapabilities.InlinePictures = CType(cmbInlinePictures.Value, DocumentCapability)
'        DemoRichEdit.Settings.DocumentCapabilities.Numbering.Bulleted = CType(cmbNumbering.Value, DocumentCapability)
'        DemoRichEdit.Settings.DocumentCapabilities.Numbering.MultiLevel = CType(cmbNumbering.Value, DocumentCapability)
'        DemoRichEdit.Settings.DocumentCapabilities.Numbering.Simple = CType(cmbNumbering.Value, DocumentCapability)

'        DemoRichEdit.Settings.DocumentCapabilities.ParagraphFormatting = CType(cmbParagraphFormatting.Value, DocumentCapability)
'        DemoRichEdit.Settings.DocumentCapabilities.Paragraphs = CType(cmbParagraphs.Value, DocumentCapability)
'        DemoRichEdit.Settings.DocumentCapabilities.ParagraphStyle = CType(cmbParagraphStyle.Value, DocumentCapability)
'        DemoRichEdit.Settings.DocumentCapabilities.ParagraphTabs = CType(cmbParagraphTabs.Value, DocumentCapability)
'        DemoRichEdit.Settings.DocumentCapabilities.Sections = CType(cmbSections.Value, DocumentCapability)
'        DemoRichEdit.Settings.DocumentCapabilities.TabSymbol = CType(cmbTabSymbol.Value, DocumentCapability)
'        DemoRichEdit.Settings.DocumentCapabilities.Undo = CType(cmbUndo.Value, DocumentCapability)
'        DemoRichEdit.Settings.Behavior.TabMarker = CStr(tbTabMarker.Value)
'        DemoRichEdit.Open(Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, "AynRand.docx"))
'    End Sub

'    Private Sub PrepareEditors()
'        PrepateCombobox(cmbCharacterFormatting, GetType(DocumentCapability), DemoRichEdit.Settings.DocumentCapabilities.CharacterFormatting)
'        PrepateCombobox(cmbCharacterStyle, GetType(DocumentCapability), DemoRichEdit.Settings.DocumentCapabilities.CharacterStyle)
'        PrepateCombobox(cmbCopy, GetType(DocumentCapability), DemoRichEdit.Settings.Behavior.Copy)
'        PrepateCombobox(cmbCreateNew, GetType(DocumentCapability), DemoRichEdit.Settings.Behavior.CreateNew)
'        PrepateCombobox(cmbCut, GetType(DocumentCapability), DemoRichEdit.Settings.Behavior.Cut)
'        PrepateCombobox(cmbDrag, GetType(DocumentCapability), DemoRichEdit.Settings.Behavior.Drag)
'        PrepateCombobox(cmbDrop, GetType(DocumentCapability), DemoRichEdit.Settings.Behavior.Drop)
'        PrepateCombobox(cmbFields, GetType(DocumentCapability), DemoRichEdit.Settings.DocumentCapabilities.Fields)
'        PrepateCombobox(cmbFullScreen, GetType(DocumentCapability), DemoRichEdit.Settings.Behavior.FullScreen)
'        PrepateCombobox(cmbHyperlinks, GetType(DocumentCapability), DemoRichEdit.Settings.DocumentCapabilities.Hyperlinks)
'        PrepateCombobox(cmbInlinePictures, GetType(DocumentCapability), DemoRichEdit.Settings.DocumentCapabilities.InlinePictures)
'        PrepateCombobox(cmbNumbering, GetType(DocumentCapability), DemoRichEdit.Settings.DocumentCapabilities.Numbering.Simple)
'        PrepateCombobox(cmbOpen, GetType(DocumentCapability), DemoRichEdit.Settings.Behavior.Open)
'        PrepateCombobox(cmbPageBreakInsertMode, GetType(PageBreakInsertMode), DemoRichEdit.Settings.Behavior.PageBreakInsertMode)
'        PrepateCombobox(cmbParagraphFormatting, GetType(DocumentCapability), DemoRichEdit.Settings.DocumentCapabilities.ParagraphFormatting)
'        PrepateCombobox(cmbParagraphs, GetType(DocumentCapability), DemoRichEdit.Settings.DocumentCapabilities.Paragraphs)
'        PrepateCombobox(cmbParagraphStyle, GetType(DocumentCapability), DemoRichEdit.Settings.DocumentCapabilities.ParagraphStyle)
'        PrepateCombobox(cmbParagraphTabs, GetType(DocumentCapability), DemoRichEdit.Settings.DocumentCapabilities.ParagraphTabs)
'        PrepateCombobox(cmbPaste, GetType(DocumentCapability), DemoRichEdit.Settings.Behavior.Paste)
'        PrepateCombobox(cmbPrinting, GetType(DocumentCapability), DemoRichEdit.Settings.Behavior.Printing)
'        PrepateCombobox(cmbSave, GetType(DocumentCapability), DemoRichEdit.Settings.Behavior.Save)
'        PrepateCombobox(cmbSaveAs, GetType(DocumentCapability), DemoRichEdit.Settings.Behavior.SaveAs)
'        PrepateCombobox(cmbSections, GetType(DocumentCapability), DemoRichEdit.Settings.DocumentCapabilities.Sections)
'        PrepateCombobox(cmbTabSymbol, GetType(DocumentCapability), DemoRichEdit.Settings.DocumentCapabilities.TabSymbol)
'        PrepateCombobox(cmbUndo, GetType(DocumentCapability), DemoRichEdit.Settings.DocumentCapabilities.Undo)
'        tbTabMarker.Value = DemoRichEdit.Settings.Behavior.TabMarker
'    End Sub
'    Private Sub PrepateCombobox(ByVal cmb As ASPxComboBox, ByVal enumType As Type, ByVal value As Object)
'        For Each enumValue In System.Enum.GetValues(enumType)
'            cmb.Items.Add(System.Enum.GetName(enumType, enumValue), CInt(Fix(enumValue)))
'        Next enumValue
'        cmb.Value = CInt(Fix(value))
'    End Sub
'    Protected Sub DemoRichEdit_PreRender(ByVal sender As Object, ByVal e As EventArgs)
'        DemoRichEdit.Focus()
'    End Sub
'    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
'        DemoHelper.Instance.ControlAreaMaxWidth = Unit.Percentage(100)
'        DemoHelper.Instance.PrepareControlOptions(OptionsFormLayout, New ControlOptionsSettings With {.ColumnMinWidth = 350, .ColumnCountMode = RecalculateColumnCountMode.ChildGroups})
'    End Sub
'End Class