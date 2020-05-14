@modeltype  compliancecg.Controllers.DocFiles

@Code
    ViewData("Title") = "Index"
End Code

@*<h2>Forms</h2>*@

<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">


<div class="mt-2">
<ul id="limheight">
    @For Each File In Model.Files

        Select Case File.Ext
            Case = ".pdf"
                @<li class="fa fa-file-pdf-o" style="font-size:16px;color:red;margin-bottom:10px;">
                    @Html.ActionLink(File.Name, "Download", "Forms", New With {.FilePath = File.Path, .FileName = File.Name}, Nothing)
                </li>
                                    Case = ".docx"
                                        @<li class="fa fa-file-word-o" style="font-size:16px;color:blue;margin-bottom:10px;">
                                            @Html.ActionLink(File.Name, "Download", "Forms", New With {.FilePath = File.Path, .FileName = File.Name}, Nothing)
                                        </li>
                                End Select
                            Next
</ul>
</div>

<style>
    .FilePDF {
        font-size: 14px;
        color: red
    }

    #limheight3 {
        list-style-type: disc;
        -webkit-columns: 3;
        -moz-columns: 3;
        columns: 3;
        list-style-position: inside; /*//this is important addition*/
    }


    #limheight2 {
        /*height: 900px;*/ /*your fixed height;*/
        -webkit-column-count: 2;
        -moz-column-count: 2;
        column-count: 2; /*3 in those rules is just placeholder -- can be anything*/
        list-style-type: none;
    }

    #limheight li {
        float: left;
        width: 50%; /*helps to determine number of columns, for instance 33.3% displays 3 columns*/
    }

    #limheight {
        list-style-type: disc;
    }
</style>


