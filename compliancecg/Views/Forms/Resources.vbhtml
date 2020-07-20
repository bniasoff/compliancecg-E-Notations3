@Code
    ViewData("Title") = Model.Title
    'Layout = Nothing
End Code

<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
<div class="row formsRow">
    <div class="col-12">
        <h2>@Html.Label(ViewData("Title"))</h2>

        <div class="col-lg-12 control-section card-control-section basic_card_layout">
            <div class="e-card-resize-container">
                <div class='row'>
                    <div class="row card-layout">
                        <div class="col-12" >

                            @For Each File In Model.Files
                                Select Case File.Ext
                                    Case = ".pdf"

                                        @<div style="margin-bottom:20px">
                                            <div tabindex="0" class="e-card" id="basic_card">
                                                <div class="e-card-header">
                                                    <div class="e-card-header-caption">
                                                        <div class="e-card-header-title"><b><span class="fa fa-file-pdf-o" style="font-size:16px;color:red;margin-right:10px;"> </span>@Html.ActionLink(File.Name, "Download", "Training", New With {.FilePath = File.Path, .FileName = File.Name}, Nothing)</b></div>
                                                    </div>
                                                </div>
                                                <div class="e-card-content">
                                                    <div>
                                                        @if File.description IsNot Nothing Then
                                                            @<div><b>@Html.Label(File.description)</b></div>
                                                        End If
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                End Select
                            Next

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>



    <!-- DivTable.com -->
    @*<Style>
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
        </Style>*@


  