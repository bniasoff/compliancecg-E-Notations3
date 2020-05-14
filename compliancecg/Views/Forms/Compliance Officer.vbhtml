@Code
    ViewData("Title") = "Compliance Officer"
    'Layout = Nothing
End Code

<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">


<div style="margin-top:100px;width:800px;">
    <div style="text-align: center; font-size: 20px;">
        <b>@Html.Label(ViewData("Title"))</b>
    </div>

    <ul>
        <div Class="divTableRow">
            <div Class="divTableCell">
                <b>
                    Name
                </b>
            </div>
            <div Class="divTableCell">
                <b>
                    Description
                </b>
            </div>
        </div>
    </ul>

    <ul>
        @For Each File In Model.Files
            Select Case File.Ext
                Case = ".pdf"
                    @<div Class="divTableRow">
                        <div Class="divTableCell">
                            <b>
                                <li class="fa fa-file-pdf-o" style="font-size:16px;color:red;margin-bottom:2px;">
                                    @Html.ActionLink(File.Name, "Download", "Training", New With {.FilePath = File.Path, .FileName = File.Name}, Nothing)
                                </li>
                            </b>
                        </div>

                        <div Class="divTableCell"><b>@Html.Label(File.description)</b></div>
                    </div>
            End Select
        Next
    </ul>
</div>


        <!-- DivTable.com -->


        <Style>
            .divTable {
                display: table;
                width: 1000px;
            }

            .divTableRow {
                display: table-row;
                height: 10px;
                max-height: 80px;
            }

            .divTableHeading {
                background - color: #EEE;
                display: table-header-group;
            }

            .divTableCell, .divTableHead {
                border: 1px solid #999999;
                display: table-cell;
                padding: 2px 2px;
                height: 10px;
                width: 500px;
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
        </Style>

        <Style>
            .FilePDF {
                font-size: 14px;
                color: red
            }
        </Style>

