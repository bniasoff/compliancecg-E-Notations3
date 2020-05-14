Imports System.Web.Mvc
Imports Syncfusion.EJ2.Navigations
Imports NLog

'Imports System
'Imports System.Collections.Generic
'Imports System.Linq
'Imports System.Web
'Imports System.Web.Mvc
'Imports Syncfusion.EJ2.Navigations



Namespace Controllers
    Public Class SyncfusionController
        Inherits Controller
        Private Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Private tabItems As List(Of TabTabItem) = New List(Of TabTabItem)()

        ' GET: Syncfusion
        Function Index() As ActionResult
            Return View()
        End Function

        Function Card() As ActionResult
            Return View()
        End Function

        Function ProgressBar() As ActionResult
            Return View()
        End Function

        Public Function ResponsivePanel() As ActionResult
            Try

                Dim parentitem As List(Of Parentitem) = New List(Of Parentitem)()
                Dim childitem As List(Of childItems) = New List(Of childItems)()
                Dim localFields As TreeViewFieldsSettings = New TreeViewFieldsSettings()

                parentitem.Add(New Parentitem With {.nodeId = "01", .nodeText = "Installation2", .iconCss = "icon-microchip icon"})
                parentitem.Add(New Parentitem With {.nodeId = "02", .nodeText = "Deployment", .iconCss = "icon-thumbs-up-alt icon"})
                parentitem.Add(New Parentitem With {.nodeId = "03", .nodeText = "Quick Start", .iconCss = "icon-docs icon"})

                Dim childitem4 As List(Of childItems) = New List(Of childItems)()
                parentitem.Add(New Parentitem With {.nodeId = "04", .nodeText = "Components", .iconCss = "icon-th icon", .child = childitem4})
                childitem4.Add(New childItems With {.nodeId = "04-01", .nodeText = "Calendar", .iconCss = "icon-circle-thin icon"})
                childitem4.Add(New childItems With {.nodeId = "04-02", .nodeText = "DatePicker", .iconCss = "icon-circle-thin icon"})
                childitem4.Add(New childItems With {.nodeId = "04-03", .nodeText = "DateTimePicker", .iconCss = "icon-circle-thin icon"})
                childitem4.Add(New childItems With {.nodeId = "04-04", .nodeText = "DateRangePicker", .iconCss = "icon-circle-thin icon"})
                childitem4.Add(New childItems With {.nodeId = "04-05", .nodeText = "TimePicker", .iconCss = "icon-circle-thin icon"})
                childitem4.Add(New childItems With {.nodeId = "04-06", .nodeText = "SideBar", .iconCss = "icon-circle-thin icon"})

                Dim childitem5 As List(Of childItems) = New List(Of childItems)()
                parentitem.Add(New Parentitem With {.nodeId = "05", .nodeText = "API Reference", .iconCss = "icon-code icon", .child = childitem4})
                childitem5.Add(New childItems With {.nodeId = "05-01", .nodeText = "Calendar", .iconCss = "icon-circle-thin icon"})
                childitem5.Add(New childItems With {.nodeId = "05-02", .nodeText = "DatePicker", .iconCss = "icon-circle-thin icon"})
                childitem5.Add(New childItems With {.nodeId = "05-03", .nodeText = "DateTimePicker", .iconCss = "icon-circle-thin icon"})
                childitem5.Add(New childItems With {.nodeId = "05-04", .nodeText = "DateRangePicker", .iconCss = "icon-circle-thin icon"})
                childitem5.Add(New childItems With {.nodeId = "05-05", .nodeText = "TimePicker", .iconCss = "icon-circle-thin icon"})
                childitem5.Add(New childItems With {.nodeId = "05-06", .nodeText = "SideBar", .iconCss = "icon-circle-thin icon"})

                parentitem.Add(New Parentitem With {.nodeId = "06", .nodeText = "Browser Compatibility", .iconCss = "icon-chrome icon"})
                parentitem.Add(New Parentitem With {.nodeId = "07", .nodeText = "Upgrade Packages", .iconCss = "icon-up-hand icon"})
                parentitem.Add(New Parentitem With {.nodeId = "08", .nodeText = "Release Notes", .iconCss = "icon-bookmark-empty icon"})
                parentitem.Add(New Parentitem With {.nodeId = "09", .nodeText = "FAQ", .iconCss = "icon-help-circled icon"})
                parentitem.Add(New Parentitem With {.nodeId = "10", .nodeText = "License", .iconCss = "icon-doc-text icon"})

                localFields.DataSource = parentitem
                localFields.Id = "nodeId"
                localFields.Child = "child"
                localFields.Text = "nodeText"
                localFields.IconCss = "iconCss"
                ViewBag.fields = localFields
                Return View()


          Catch ex As Exception
logger.Error(ex)

            End Try
        End Function

        Function TabControl() As ActionResult
            tabItems.Add(New TabTabItem With {
                    .Header = New TabHeader With {
                        .Text = "Twitter",
                        .IconCss = "e-twitter"
                    },
                    .Content = "Twitter is an online social networking service that enables users to send and read short 140-character messages called 'tweets'.Registered users can read and post tweets, but those who are unregistered can only read them.Users access Twitter through the website interface, SMS or mobile device app Twitter Inc. is based in San Francisco and has more than 25 offices around the world.Twitter was created in March 2006 by Jack Dorsey, Evan Williams, Biz Stone, and Noah Glass and launched in July 2006. The service rapidly gained worldwide popularity, with more than 100 million users posting 340 million tweets a day in 2012.The service also handled 1.6 billion search queries per day."
                })
            tabItems.Add(New TabTabItem With {
                    .Header = New TabHeader With {
                        .Text = "Facebook",
                        .IconCss = "e-facebook"
                    },
                    .Content = "Facebook is an online social networking service headquartered in Menlo Park, California. Its website was launched on February 4, 2004, by Mark Zuckerberg with his Harvard College roommates and fellow students Eduardo Saverin, Andrew McCollum, Dustin Moskovitz and Chris Hughes.The founders had initially limited the website's membership to Harvard students, but later expanded it to colleges in the Boston area, the Ivy League, and Stanford University.It gradually added support for students at various other universities and later to high - school students."
                })
            tabItems.Add(New TabTabItem With {
                    .Header = New TabHeader With {
                        .Text = "WhatsApp",
                        .IconCss = "e-whatsapp"
                    },
                    .Content = "WhatsApp Messenger is a proprietary cross-platform instant messaging client for smartphones that operates under a subscription business model.It uses the Internet to send text messages, images, video, user location and audio media messages to other users using standard cellular mobile numbers. As of February 2016, WhatsApp had a user base of up to one billion,[10] making it the most globally popular messaging application.WhatsApp Inc., based in Mountain View, California, was acquired by Facebook Inc.on February 19, 2014, for approximately US$19.3 billion."
                })
            ViewBag.items = tabItems
            Return View()
        End Function

    End Class



    Public Class Parentitem
        Public nodeId As String
        Public nodeText As String
        Public icon As String
        Public expanded As Boolean
        Public selected As Boolean
        Public iconCss As String
        Public child As List(Of childItems)
    End Class

    Public Class childItems
        Public nodeId As String
        Public nodeText As String
        Public icon As String
        Public expanded As Boolean
        Public selected As Boolean
        Public iconCss As String
    End Class


End Namespace