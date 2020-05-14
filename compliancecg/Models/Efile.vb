

Imports System.ComponentModel.DataAnnotations
Imports System.Drawing
Imports DevExpress.Web
Imports DevExpress.Web.ASPxRichEdit

Namespace Models
    Public Class EFile
        Public Property FileGuid As String
        Public Property FilePath As String
        Public Property FileId As Integer
        Public Property ParentID As Integer?
        Public Property RelativeName As String
        Public Property FriendlyName As String
        Public Property Name As String
        Public Property Extension As String
        Public Property TempNamePrefix As String
        Public Property IsFolder As Boolean
        Public Property ThumbnailData As Byte()
        Public Property Data As Byte()
        Public Property Category As String
        Public Property FileSource As String
        Public Property ReferingPhysician As String
        Public Property LinkTo As String
        Public Property Description As String
    End Class


    Public Class RichEditSettingOptions
        Const SettingsDemoOptionsKey As String = "RichEditSettingOptions"

        Public Sub New()
            BehaviorSettings = New ASPxRichEditBehaviorSettings()
            DocumentCapabilitiesSettings = New ASPxRichEditDocumentCapabilitiesSettings()
        End Sub

        Public Shared Property Current As RichEditSettingOptions
            Get
                If HttpContext.Current.Session(SettingsDemoOptionsKey) Is Nothing Then HttpContext.Current.Session(SettingsDemoOptionsKey) = New RichEditSettingOptions()
                Return CType(HttpContext.Current.Session(SettingsDemoOptionsKey), RichEditSettingOptions)
            End Get
            Set(ByVal value As RichEditSettingOptions)
                HttpContext.Current.Session(SettingsDemoOptionsKey) = value
            End Set
        End Property

        <Display(Name:="Behavior")>
        Public Property BehaviorSettings As ASPxRichEditBehaviorSettings
        <Display(Name:="Document Capabilities")>
        Public Property DocumentCapabilitiesSettings As ASPxRichEditDocumentCapabilitiesSettings
    End Class



    Public Class DocumentProtectionOptions
        Public Sub New()
            User = "lawyer@somecompany.com"
            Color = Color.FromArgb(255, 254, 213)
            BracketsColor = Color.FromArgb(164, 160, 0)
            Visibility = True
        End Sub

        Const DocumentProtectionOptionsKey As String = "DocumentProtectionOptions"

        Public Shared Property Current As DocumentProtectionOptions
            Get
                If HttpContext.Current.Session(DocumentProtectionOptionsKey) Is Nothing Then HttpContext.Current.Session(DocumentProtectionOptionsKey) = New DocumentProtectionOptions()
                Return CType(HttpContext.Current.Session(DocumentProtectionOptionsKey), DocumentProtectionOptions)
            End Get
            Set(ByVal value As DocumentProtectionOptions)
                HttpContext.Current.Session(DocumentProtectionOptionsKey) = value
            End Set
        End Property

        <Display(Name:="User")>
        Public Property User As String
        <Display(Name:="Color")>
        Public Property Color As Color
        <Display(Name:="Brackets Color")>
        Public Property BracketsColor As Color
        <Display(Name:="Visibility")>
        Public Property Visibility As Boolean
    End Class

    Public Class ContextMenuCustomizationOptions
        Public Sub New()
            ShowContextMenu = True
            ShowDefaultItems = False
        End Sub

        Const ContextMenuCustomizationDemoOptionsKey As String = "ContextMenuCustomizationOptions"

        Public Shared Property Current As ContextMenuCustomizationOptions
            Get
                If HttpContext.Current.Session(ContextMenuCustomizationDemoOptionsKey) Is Nothing Then HttpContext.Current.Session(ContextMenuCustomizationDemoOptionsKey) = New ContextMenuCustomizationOptions()
                Return CType(HttpContext.Current.Session(ContextMenuCustomizationDemoOptionsKey), ContextMenuCustomizationOptions)
            End Get
            Set(ByVal value As ContextMenuCustomizationOptions)
                HttpContext.Current.Session(ContextMenuCustomizationDemoOptionsKey) = value
            End Set
        End Property

        <Display(Name:="ShowContextMenu")>
        Public Property ShowContextMenu As Boolean
        <Display(Name:="ShowDefaultItems")>
        Public Property ShowDefaultItems As Boolean
    End Class



    Public Class RichEditSettings
        Public RichEditRibbonMode As RichEditRibbonMode
        Public EditMode As Boolean
        Public [ReadOnly] As Boolean
    End Class

    Public Class AllRichEditSettings
        Public EFile As New EFile
        Public RichEditSettingOptions As New RichEditSettingOptions
        Public RichEditSettings As New RichEditSettings
        Public RichEditClientSideEvents As New RichEditClientSideEvents
        Public RibbonTabs As New RibbonTab
        Public AllowPrint As Boolean = False
    End Class
End Namespace






