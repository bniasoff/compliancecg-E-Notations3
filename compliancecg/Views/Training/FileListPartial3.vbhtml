
@*<div style="margin-left:2px; width:800px">
    <ul id="limheight">
        @For Each File In Model.Files
            If File.name.StartsWith("Basics") Then
                Select Case File.Ext
                    Case = ".pdf"
                        @<li class="fa fa-file-pdf-o" style="font-size:16px;color:red;margin-bottom:10px;">
                            @Html.ActionLink(File.Name, "Download", "Training", New With {.FilePath = File.Path, .FileName = File.Name}, Nothing)
                        </li>
                End Select
            End If
        Next
    </ul>
</div>*@


<div style="margin-left:2px; width:800px">
    <ul id="limheight">
        <li class="fa fa-file-pdf-o" style="font-size:16px;color:red;margin-bottom:10px;">
            @Html.ActionLink("Basics of the Compliance Program Training and Acknowledgement", "DownloadFiles", "Training", New With {.fileName = "Basics of the Compliance Program Training and Acknowledgement"}, New With {.target = "_blank"})
        </li>
        <li class="fa fa-file-pdf-o" style="font-size:16px;color:red;margin-bottom:10px;">
            @Html.ActionLink("Basics of the Compliance Program Department Head Sign-in Sheet", "DownloadFiles", "Training", New With {.fileName = "Basics of the Compliance Program Department Head Sign-in Sheet"}, New With {.target = "_blank"})
        </li>
        <li class="fa fa-file-pdf-o" style="font-size:16px;color:red;margin-bottom:10px;">
            @Html.ActionLink("Basics of the Compliance Program Staff Sign-in Sheet.pdf", "DownloadFiles", "Training", New With {.fileName = "Basics of the Compliance Program Staff Sign-in Sheet"}, New With {.target = "_blank"})
        </li>
    </ul>
</div>


<Style>
    .FilePDF {
        font-size: 14px;
        color: red
    }

    #limheight3 {
        list-style-type disc;
        -webkit-columns: 3;
        -moz-columns: 3;
        columns: 3;
        list-style-position: inside; /*//this Is important addition*/
    }


    #limheight2 {
        /*height 900px;*/ /*your fixed height;*/
        -webkit-column-count: 2;
        -moz-column-count: 2;
        column-count: 2; /*3 in those rules Is just placeholder -- can be anything*/
        list-style-type: none;
    }

    #limheight li {
        /*float left;*/        width: 100%; /*helps to determine number of columns, for instance 33.3% displays 3 columns*/
    }

    #limheight {
        list-style-type disc;
    }
</Style>

