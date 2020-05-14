@Code
    ViewData("Title") = "Training"
    'Layout = Nothing
End Code

<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

@*<div class="divTableRow">
        <div class="divTableCell2"><b>Defecit Reduction Act</b><br />Policy & Procedure: CCG 00201 and Acknowledgement</div>
        <div class="divTableCell3"><b>&#10004;</b></div>
        <div class="divTableCell3"><b>&#10004;</b></div>
        <div class="divTableCell3"><b>&#10004;</b></div>
        <div class="divTableCell3"><b>&#10004;</b></div>
        <div class="divTableCell3"><b>&#10004;</b></div>
    </div>*@

 @*var JobTitlesDropDownList =@Html.Raw(Json.Encode(ViewBag.JobTitlesDropDownList));*@
<div class="row formsRow">
    <h2>Training</h2>
    <div>
        <div class="divTable" style="float:left;">
            <div class="divTableBody">
                <div class="divTableRow">
                    <div class="divTableCell"></div>
                    <div class="divTableCell"><b>Signature Required</b></div>
                    <div class="divTableCell"><b>Employees</b></div>
                    <div class="divTableCell"><b>Independent Contractors</b></div>
                    <div class="divTableCell"><b>Vendors</b></div>
                    <div class="divTableCell"><b>Volunteers</b></div>
                    <div class="divTableCell"><b>Forms</b></div>

                </div>
                <div class="divTableRow">
                    <div class="divTableCell"><b>Code Of Conduct and Compliance</b><br />Policy & Procedure: CCG 00101 & CCG 00102 and Acknowledgement</div>
                    <div class="divTableCell3"><b>&#10004;</b></div>
                    <div class="divTableCell3"><b>&#10004;</b></div>
                    <div class="divTableCell3"><b>&#10004;</b></div>
                    <div class="divTableCell3"><b>&#10004;</b></div>
                    <div class="divTableCell3"><b>&#10004;</b></div>
                    <div class="divTableCell4">
                        @Html.Raw(Html.Partial("~/Views/Training/FileListPartial1.vbhtml"))
                    </div>
                </div>
                <div Class="divTableRow">
                    <div Class="divTableCell2"><b>Deficit Reduction Act</b><br />Policy & Procedure: CCG 00201 And Acknowledgement</div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div class="divTableCell4">
                        @Html.Raw(Html.Partial("~/Views/Training/FileListPartial2.vbhtml"))
                    </div>
                </div>
                <div Class="divTableRow">
                    <div Class="divTableCell"><b>Basics of the Compliance Program<br /> Training & Sign-In Sheet</b><br />Forms/</div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div class="divTableCell4">
                        @Html.Raw(Html.Partial("~/Views/Training/FileListPartial3.vbhtml"))
                    </div>
                </div>
                <div Class="divTableRow">
                    <div Class="divTableCell"><b>Elder Justice Act</b><br />Policy & Procedure: CCG 00304 And Acknowledgement</div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3">&nbsp;</div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div class="divTableCell4">
                        @Html.Raw(Html.Partial("~/Views/Training/FileListPartial4.vbhtml"))
                    </div>
                </div>
                <div Class="divTableRow">
                    <div Class="divTableCell"><b>Fraud Waste & Abuse<br />Training & Sign-In Sheet</b><br />Forms/</div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div class="divTableCell4">
                        @Html.Raw(Html.Partial("~/Views/Training/FileListPartial5.vbhtml"))
                    </div>
                </div>
                <div Class="divTableRow">
                    <div Class="divTableCell"><b>HIPAA<br />Training & Sign-In Sheet</b><br />Forms/</div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div Class="divTableCell3"><b>&#10004;</b></div>
                    <div class="divTableCell4">
                        @Html.Raw(Html.Partial("~/Views/Training/FileListPartial6.vbhtml"))
                    </div>
                </div>
            </div>
        </div>

        @*<div style = "float:left;margin-left:20px;width:700px" >

                        <ul id = "limheight" >
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

                    </div>*@
    </div>
</div>
    <!-- DivTable.com -->


    <Style>
        /* DivTable.com */
        .divTable {
            display: table;
            width: 100px;
        }

        .divTableRow {
            display: table-row;
            height: 80px;
            max-height: 80px;
        }

        .divTableHeading {
            background - color: #EEE;
            display: table-header-group;
        }

        .divTableCell, .divTableHead {
            border: 1px solid #999999;
            display: table-cell;
            padding: 3px 10px;
            height: 80px;
        }

        .divTableHeading {
            background - color: #EEE;
            display: table-header-group;
            font-weight: bold;
        }

        .divTableFoot {
            background - color: #EEE;
            display: table-footer-group;
            font-weight: bold;
        }

        .divTableBody {
            display: table-row-group;
        }


        .divTableCell2, .divTableHead {
            border: 1px solid #999999;
            /*display: table-cell;*/
            padding: 3px 10px;
            width: 300px;
            height: 80px;
        }

        .divTableCell3, .divTableHead {
            border: 1px solid #999999;
            display: table-cell;
            padding: 3px 10px;
            text-align: center;
        }

        .divTableCell4, .divTableHead {
            border: 1px solid #999999;
            display: table-cell;
        }
    </Style>


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
            list-style-type disc;
        }
    </Style>

