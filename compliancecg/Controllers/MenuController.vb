Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc


Partial Public Class MenuController
        Inherits Controller

    Public Function MenuFeatures() As ActionResult

        Dim menuItems As List(Of Object) = New List(Of Object)()
        menuItems.Add(New With {.text = "File", .iconCss = "em-icons e-file",
                      .items = New List(Of Object)() From {
                      New With {.text = "Open", .iconCss = "em-icons e-open"},
                New With {.text = "Save", .iconCss = "e-icons e-save"},
                New With {.separator = True},
                New With {.text = "Exit"}
            }
        })

        menuItems.Add(New With {.text = "Edit",
            .iconCss = "em-icons e-edit",
            .items =
            New List(Of Object)() From {New With {.text = "Cut", .iconCss = "em-icons e-cut"},
                New With {.text = "Copy", .iconCss = "em-icons e-copy"},
                New With {.text = "Paste", .iconCss = "em-icons e-paste"}
            }
        })

        menuItems.Add(New With {.text = "View", .items = New List(Of Object)() From {
                      New With {.text = "Toolbars", .items = New List(Of Object)() From {New With {.text = "Menu Bar"},
                        New With {.text = "Bookmarks Toolbar"},
                        New With {.text = "Customize"}
                    }
                },
                New With {.text = "Zoom",
                    .items = New List(Of Object)() From {
                    New With {.text = "Zoom In"},
                        New With {.text = "Zoom Out"},
                        New With {.text = "Reset"}
                    }
                },
                New With {.text = "Full Screen"}
            }
        })

        menuItems.Add(New With {.text = "Tools", .items = New List(Of Object)() From {
                New With {.text = "Spelling & Grammar"},
                New With {.text = "Customize"},
                New With {.separator = True},
                New With {.text = "Options"}
            }
        })

        menuItems.Add(New With {.text = "Help"})

        ViewBag.menuItems = menuItems
        Return View()
    End Function
End Class
