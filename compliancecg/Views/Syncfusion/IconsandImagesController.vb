Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports Syncfusion.EJ2.Navigations

Namespace EJ2MVCSampleBrowser.Models
    Public Class ImageIcons
        Public Property NodeText As String
        Public Property Icon As String
        Public Property NodeId As String
    End Class

    Public Class TreeviewImageIcons
        Public Property NodeText As String
        Public Property Icon As String
        Public Property NodeId As String
        Public Property Expanded As Boolean
        Public NodeChild As List(Of ImageIcons)

        Public Function getTreeviewImageIconsModel() As List(Of TreeviewImageIcons)
            Dim TreeviewImageIcons As List(Of TreeviewImageIcons) = New List(Of TreeviewImageIcons)()
            Dim ImageIcons As List(Of ImageIcons) = New List(Of ImageIcons)()

            TreeviewImageIcons.Add(New TreeviewImageIcons With {.NodeId = "01", .NodeText = "Music", .Icon = "folder", .NodeChild = ImageIcons})
            ImageIcons.Add(New ImageIcons With {.NodeId = "01-01", .NodeText = "Gouttes.mp3", .Icon = "audio"})

            Dim ImageIcons2 As List(Of ImageIcons) = New List(Of ImageIcons)()
            TreeviewImageIcons.Add(New TreeviewImageIcons With {.NodeId = "02", .NodeText = "Videos", .Icon = "folder", .NodeChild = ImageIcons2})
            ImageIcons2.Add(New ImageIcons With {.NodeId = "02-01", .NodeText = "Naturals.mp4", .Icon = "video"})
            ImageIcons2.Add(New ImageIcons With {.NodeId = "02-02", .NodeText = "Wild.mpeg", .Icon = "video"})

            Dim ImageIcons3 As List(Of ImageIcons) = New List(Of ImageIcons)()
            TreeviewImageIcons.Add(New TreeviewImageIcons With {.NodeId = "03", .NodeText = "Documents", .Icon = "folder", .Expanded = True, .NodeChild = ImageIcons3})
            ImageIcons3.Add(New ImageIcons With {.NodeId = "03-01", .NodeText = "Environment Pollution.docx", .Icon = "docx"})
            ImageIcons3.Add(New ImageIcons With {.NodeId = "03-02", .NodeText = "Global Water, Sanitation, & Hygiene.docx", .Icon = "docx"})
            ImageIcons3.Add(New ImageIcons With {.NodeId = "03-03", .NodeText = "Global Warming.ppt", .Icon = "ppt"})
            ImageIcons3.Add(New ImageIcons With {.NodeId = "03-04", .NodeText = "Social Network.pdf", .Icon = "pdf"})
            ImageIcons3.Add(New ImageIcons With {.NodeId = "03-05", .NodeText = "Youth Empowerment.pdf", .Icon = "pdf"})

            Dim ImageIcons4 As List(Of ImageIcons) = New List(Of ImageIcons)()
            TreeviewImageIcons.Add(New TreeviewImageIcons With {.NodeId = "04", .NodeText = "Pictures", .Icon = "folder", .NodeChild = ImageIcons4})
            ImageIcons4.Add(New ImageIcons With {.NodeId = "04-01", .NodeText = "Camera Roll", .Icon = "folder"})
            ImageIcons4.Add(New ImageIcons With {.NodeId = "04-02", .NodeText = "Wind.jpg", .Icon = "images"})
            ImageIcons4.Add(New ImageIcons With {.NodeId = "04-03", .NodeText = "Stone.jpg", .Icon = "images"})

            Dim ImageIcons5 As List(Of ImageIcons) = New List(Of ImageIcons)()
            TreeviewImageIcons.Add(New TreeviewImageIcons With {.NodeId = "05", .NodeText = "Downloads", .Icon = "folder", .NodeChild = ImageIcons5})
            ImageIcons5.Add(New ImageIcons With {.NodeId = "05-01", .NodeText = "UI-Guide.pdf", .Icon = "pdf"})
            ImageIcons5.Add(New ImageIcons With {.NodeId = "05-02", .NodeText = "Tutorials.zip", .Icon = "zip"})
            ImageIcons5.Add(New ImageIcons With {.NodeId = "05-03", .NodeText = "Game.exe", .Icon = "exe"})
            ImageIcons5.Add(New ImageIcons With {.NodeId = "05-04", .NodeText = "TypeScript.7z", .Icon = "zip"})

            Return TreeviewImageIcons
        End Function
    End Class
End Namespace
