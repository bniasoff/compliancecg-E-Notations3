@Imports System.Data.Linq


<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
<div Class="row formsRow">
    @If Model.Page = "memos" Then

        @<div Class="col-lg-5 mainRow col-md-12">
            <h2>Covid-19 Memos</h2>
            <div style="margin-top:30px;">
                <ul class="covidList">
                    @For Each CovidMemo In Model.CovidMemos
                        Dim sclass = ""
                        If CovidMemo.MemoID = Model.SelectedMemo.MemoID Then
                            sclass = "class=selectedLI"
                        End If
                        @<li id="memo_@CovidMemo.MemoID" @sclass><i class="fa fa-file-text-o"></i>@CovidMemo.MemoName</li>
                    Next
                </ul>
            </div>
        </div>
        @<div class="col-lg-7 col-md-12 documentView">
            <div ID="DocumentView" Class="col-12 mainRow ">
                @For Each CovidTool In Model.CovidTools
                    If CovidTool.MemoID = Model.SelectedMemo.MemoID Then
                        @<div class="downloadBtn"><Span Class="fa fa-file-pdf-o" style="font-size:16px;margin-right:6px;"> </Span><a href="@CovidTool.ToolPath" download>@CovidTool.ToolName</a></div>
                    End If
                Next
                <div id="document">
                    <embed src="@Model.SelectedMemo.MemoPath" type="application/pdf" style="width: 100%;height: 800px;">
                </div>
            </div>
        </div>
    ElseIf Model.Page = "tools" Then
        @<div Class="col-12">
            <h2>Covid-19 Tools</h2>
            <div Class="col-lg-12 control-section card-control-section basic_card_layout">
                @For Each CovidTool In Model.CovidTools
                    Dim MemoName = ""
                    For Each covidMemo In Model.CovidMemos
                        If covidMemo.MemoID = CovidTool.MemoID Then
                            MemoName = covidMemo.MemoName
                        End If
                    Next
                    @<div style="margin-bottom:20px">
                        <div tabindex="0" class="e-card" id="basic_card">
                            <div class="e-card-header">
                                <div class="e-card-header-caption">
                                    <div class="e-card-header-title"><span class="fa fa-file-pdf-o" style="font-size:16px;color:red;margin-right:10px;"> </span><a href="@CovidTool.ToolPath" download>@CovidTool.ToolName</a><span style="float:right">refer to memo <span class="memospan" id="memo_@CovidTool.MemoID">@MemoName</span></span></div>
                                </div>
                            </div>
                        </div>
                    </div>
                Next
            </div>
        </div>
    End If
</div>

<script>
    jQuery(document).ready(function () {
        jQuery(".covidList li, span.memospan").click(function () {
            MemoID = jQuery(this)[0].id.split("_")[1];
            var ajax = new ej.base.Ajax('/Forms/CovidResources?Page=memos&SMemoID=' + MemoID, 'POST', true);
            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });
        })
    })

</script>
<style>
    span.memospan{
        cursor:pointer;
        text-decoration:underline;
    }
    .selectedLI {
        background: #173978;
        padding: 5px;
        color: white;
    }

    .downloadBtn {
        background-color:  #3da84a;
        padding: 8px 13px;
        border-radius:  20px;
        color: white;
        float: right;
        margin-bottom:  10px;
        margin-right:  10px;
    }

        .downloadBtn a {
            color: white;
        }

    .covidList {
        list-style-type: none;
        font-size:  16px;
    }

        .covidList li {
            margin: 10px 0;
            cursor: pointer;
        }

            .covidList li i {
                margin-right:  15px;
            }

    .FilePDF {
        font-size:  14px;
        color: red
    }
    .e-card-header-title{
        font-size:15px !important;
    }
</style>


