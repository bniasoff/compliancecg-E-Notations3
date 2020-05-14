@Imports Syncfusion.EJ2
@imports Syncfusion.EJ2.Grids
@imports  Syncfusion.EJ2.Popups


<div id='container' style="height:400px;" class="control">
    @Html.EJS().Button("targetButton").Content("Open Dialog").Render()

    <div id="DivDisplay" style="display:none;">
        @Html.EJS().Dialog("dialog").Visible(False).IsModal(True).Header("Outer Dialog").ShowCloseIcon(True).Target("#container").Height("300px").CloseOnEscape(False).Width("400px").AnimationSettings(New DialogAnimationSettings() With {.Effect = DialogEffect.None}).Created("onCreated").ContentTemplate(
                                @@<div>
                                    {@Html.EJS().Grid("Grid4").Height("300").Width("1220").Columns(Sub(col)
                                                                                                       col.Field("UserID").HeaderText("UserID").IsPrimaryKey(True).Visible(True).Add()
                                                                                                   End Sub).EditSettings(Sub(edit) edit.AllowAdding(True).AllowEditing(True).AllowDeleting(True).Mode(Syncfusion.EJ2.Grids.EditMode.Dialog)).Toolbar(New List(Of String)(New String() {"Add", "Edit", "Delete", "Update", "Cancel"})).Render()}


                                </div>).Render()
        @*@@<Button Class="e-control e-btn" id="innerButton" role="button">Open InnerDialog</Button>).Render()*@
    </div>
</div>



<script>

    document.getElementById("targetButton").addEventListener('click', function () {
        $('#DivDisplay').show();

        var dialog = document.getElementById("dialog").ej2_instances[0];
        //var innerDialog = document.getElementById("innerDialog").ej2_instances[0];
        dialog.show();
        //innerDialog.show();
    });

    //window.onload = function () {
    //    document.getElementById('targetButton').onclick = function () {
    //        var dialog = document.getElementById("dialog").ej2_instances[0];
    //        var innerDialog = document.getElementById("innerDialog").ej2_instances[0];
    //        dialog.show();
    //        innerDialog.show();
    //    }
    //}
    function onCreated() {
        document.getElementById("innerButton").addEventListener("click", function () {
            var innerDialog = document.getElementById("innerDialog").ej2_instances[0];
            innerDialog.show();
        })
    }

</script>

<style>
    .e-grid .e-dialog .gridform .e-rowcell {
        padding: .5em;
    }
</style>


<style>


    body {

        height: 100% !important;
    }
</style>
<style>


    .e-edit-dialog .e-gridform .e-table {
        border-collapse: separate;
        border-spacing: 11px;
        width: 100%;
    }

    .e-edit-dialog .e-dlg-content {
        position: relative;
    }
</style>
@*<div id='container' style="height:400px;">
        @Html.EJS().Button("targetButton").Content("Open Dialog").Render()
        @Html.EJS().Dialog("dialog").Header("Outer Dialog").ShowCloseIcon(True).Target("#container").Height("300px").CloseOnEscape(False).Width("400px").AnimationSettings(New DialogAnimationSettings() With {.Effect = DialogEffect.None}).ContentTemplate(@@<button class="e-control e-btn" id="innerButton" role="button">Open InnerDialog</button>).Render()
        @Html.EJS().Dialog("innerDialog").ShowCloseIcon(True).Header("Inner Dialog").CloseOnEscape(False).Target("#targetButton").Height("150px").AnimationSettings(New DialogAnimationSettings() With {.Effect = DialogEffect.None}).Width("250px").ContentTemplate(@@<button class="e-control e-ctn" id="innerButton" role="button">Open InnerDialog</button>).Render()


        <div id="DivGrid" style="display:none">
            @Html.EJS().Grid("Grid4").Height("300").Width("1220").Columns(Sub(col)
                col.Field("UserID").HeaderText("UserID").IsPrimaryKey(True).Visible(False).Add()
            End Sub).EditSettings(Sub(edit) edit.AllowAdding(True).AllowEditing(True).AllowDeleting(True).Mode(Syncfusion.EJ2.Grids.EditMode.Dialog)).Toolbar(New List(Of String)(New String() {"Add", "Edit", "Delete", "Update", "Cancel"})).Render()

        </div>
    </div>*@

@*<script>
        document.getElementById("targetButton").addEventListener('click', function () {

            var dialog = document.getElementById("innerDialog").ej2_instances[0];
            dialog.show();

            //var toggleBtn = document.getElementById("togglebtn").ej2_instances[0];
            //       if (toggleBtn.element.classList.contains('e-active')) {
            //           toggleBtn.content = 'Pause';
            //           toggleBtn.iconCss = 'e-btn-sb-icons e-pause-icon';
            //       } else {
            //           toggleBtn.content = 'Play';
            //           toggleBtn.iconCss = 'e-btn-sb-icons e-play-icon';
            //       }
        });
    </script>*@




@*<script>

        document.getElementById('targetButton').addEventListener('click', function () {



            $('#DivGrid').show();

        }

        //window.onload = function () {
        //         document.getElementById('targetButton').onclick = function () {
        //        var dialog = document.getElementById("dialog").ej2_instances[0];
        //        var innerDialog = document.getElementById("innerDialog").ej2_instances[0];
        //        dialog.show();
        //        innerDialog.show();
        //    }
        //}

        //function onCreated() {

        //   document.getElementById('targetButton').onclick = function () {
        //    $('#DivGrid').show();
        //    })
        //}



        //function onCreated() {
        //    debugger;
        //    document.getElementById("innerButton").addEventListener("click", function () {
        //        var innerDialog = document.getElementById("innerDialog").ej2_instances[0];
        //        innerDialog.show();
        //    })
        //}

    </script>*@








@*@Html.EJS().Dialog("dialog").Visible(True).AnimationSettings(New DialogAnimationSettings() With {.Effect = DialogEffect.Zoom}).Header("Facility Users").Open("dialogOpen").Close("dialogClose").Position(Function(obj) obj.X("center").Y("center")).IsModal(True).AllowDragging(True).ShowCloseIcon(True).CloseOnEscape(False).Width("1270").Height("600").Target("#Grid1").Buttons(Sub(btn) btn.Click("dlgButtonClick").ButtonModel(ViewBag.button1).Add()).ContentTemplate(@@<div class='dialog-content'>

        {@Html.EJS().Grid("Grid4").Height("300").Width("1220").Columns(Sub(col)
                                                                          col.Field("UserID").HeaderText("UserID").IsPrimaryKey(True).Visible(False).Add()
                                                                      End Sub).EditSettings(Sub(edit) edit.AllowAdding(True).AllowEditing(True).AllowDeleting(True).Mode(Syncfusion.EJ2.Grids.EditMode.Dialog)).Toolbar(New List(Of String)(New String() {"Add", "Edit", "Delete", "Update", "Cancel"})).Render()}


    </div>).Render()




    <script>
        function dialogClose() {
            //document.getElementById('normalbtn').style.display = 'inline-block';
        }
        function dialogOpen() {
            //document.getElementById('normalbtn').style.display = 'none';
        }

        function dlgButtonClick() {
            //window.open('https://www.syncfusion.com/company/about-us');
        }
    </script>*@



@*@Html.EJS().ScriptManager()*@