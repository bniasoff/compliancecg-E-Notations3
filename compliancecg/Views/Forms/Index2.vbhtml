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


    <style>
        .e-card {
            -webkit-tap-highlight-color: transparent;
            background-color: #fff;
            border: 1px #000;
            box-shadow: 3px 2px 3px 3px rgba(0, 0, 0, 0.16);
            color: rgba(0, 0, 0, 0.87);
            outline: none;
        }

        .e-card {
            border-radius: 10px;
            box-sizing: border-box;
            display: -ms-flexbox;
            display: flex;
            -ms-flex-direction: column;
            flex-direction: column;
            font-family: "Roboto", "Segoe UI", "GeezaPro", "DejaVu Serif", "sans-serif", "-apple-system", "BlinkMacSystemFont";
            font-size: 15px;
            -ms-flex-pack: center;
            justify-content: center;
            line-height: 36px;
            min-height: 36px;
            overflow: hidden;
            position: relative;
            text-overflow: ellipsis;
            vertical-align: middle;
            width: 100%;
        }

            .e-card .e-card-header .e-card-header-caption {
                -ms-flex-item-align: center;
                align-self: center;
                display: -ms-flexbox;
                display: flex;
                -ms-flex: 1;
                flex: 1;
                -ms-flex-direction: column;
                flex-direction: column;
                overflow: hidden;
                padding: 0 0 0 12px;
            }

            .e-card .e-card-header {
                box-sizing: border-box;
                display: -ms-flexbox;
                display: flex;
                -ms-flex-direction: row;
                flex-direction: row;
                -ms-flex-pack: center;
                justify-content: center;
                line-height: normal;
                min-height: 22.5px;
                padding: 10px 10px 2px 10px;
                width: inherit;
            }

            .e-card .e-card-content {
                font-size: 14px;
                line-height: normal;
                padding: 10px 10px 5px 10px;
            }

            .e-card .e-card-header .e-card-header-caption .e-card-header-title, .e-card .e-card-header .e-card-header-caption .e-card-sub-title {
                white-space: normal;
            }
    </style>
