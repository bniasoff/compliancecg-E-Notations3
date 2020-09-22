﻿
<div style="margin-left:2px; ">
    <ul id="limheight">
        @For Each File In Model.Files
            If File.name.StartsWith("CCG 00201") Then
                Select Case File.Ext
                    Case = ".pdf"
                        @<li>
                            <i class="fa fa-file-pdf-o" style="font-size:16px;color:red;"></i>
                            @Html.ActionLink(File.Name, "Download", "Training", New With {.FilePath = File.Path, .FileName = File.Name}, Nothing)
                        </li>
                End Select
            End If
        Next
    </ul>
</div>




@*<div style="width:800px">
    <ul id="limheight">
        @For Each File In Model.Files
            @<div><span>Test</span></div>
        Next
    </ul>
</div>*@

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
        /*float left;*/
        width: 100%; /*helps to determine number of columns, for instance 33.3% displays 3 columns*/
    }

    #limheight {
        list-style: none;
    }
</Style>

