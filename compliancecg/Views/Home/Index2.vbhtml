@Imports Syncfusion.EJ2
@imports Syncfusion.EJ2.Grids
@imports  Syncfusion.EJ2.Popups


@Code
    Dim commands As List(Of Object) = New List(Of Object)()
    commands.Add(New With {.Type = "Edit", .buttonOption = New With {.iconCss = "e-icons e-edit", .cssClass = "e-flat"}})
    commands.Add(New With {.Type = "Delete", .buttonOption = New With {.iconCss = "e-icons e-delete", .cssClass = "e-flat"}})
    commands.Add(New With {.Type = "Save", .buttonOption = New With {.iconCss = "e-icons e-update", .cssClass = "e-flat"}})
    commands.Add(New With {.Type = "Cancel", .buttonOption = New With {.iconCss = "e-icons e-cancel-icon", .cssClass = "e-flat"}})

    Dim commands2 As List(Of Object) = New List(Of Object)()
    commands2.Add(New With {.type = "userstatus", .buttonOption = New With {.content = "Users", .cssClass = "e-flat"}})
End code




    <div id='container' style="height:1000px;" class="control">
        @Html.EJS().Grid("Grid").DataSource(Function(DataManager) DataManager.Url("/Facilities/UrlDatasource").InsertUrl("/Facilities/Insert").UpdateUrl("/Facilities/Update").RemoveUrl("/Facilities/Delete").Adaptor("UrlAdaptor")).Height("300").Width("1300").Columns(Sub(col)
                                                                                                                                                                                                                                                                              col.HeaderText("Users").Width("80").Commands(commands2).Add()
                                                                                                                                                                                                                                                                              col.Field("FacilityID").HeaderText("FacilityID").IsPrimaryKey(True).Visible(True).Add()
                                                                                                                                                                                                                                                                              col.Field("Name").HeaderText("Name").Width("200").ValidationRules(New With {.required = True, .minLength = 3}).Add()
                                                                                                                                                                                                                                                                          End Sub).Load("load").EditSettings(Sub(edit) edit.AllowAdding(True).AllowEditing(True).AllowDeleting(True).Mode(Syncfusion.EJ2.Grids.EditMode.Dialog)).Toolbar(New List(Of String)(New String() {"Add", "Edit", "Delete", "Update", "Cancel"})).Render()



        <div id='container2' style="height:1000px;" class="control">
            @Html.EJS().Dialog("dialog").Visible(False).IsModal(True).Header("Grid Dialog").ShowCloseIcon(True).Target("#container2").Height("300px").CloseOnEscape(False).Width("400px").AnimationSettings(New DialogAnimationSettings() With {.Effect = DialogEffect.None}).ContentTemplate(
                                                                                @@<div>
                                                                                    {@Html.EJS().Grid("Grid2").Height("300").Width("1220").Columns(Sub(col)
                                                                                                                                                       col.Field("UserID").HeaderText("UserID").IsPrimaryKey(True).Visible(True).Add()
                                                                                                                                                       col.Field("Name").HeaderText("Name").Width("200").Add()
                                                                                                                                                   End Sub).EditSettings(Sub(edit) edit.AllowAdding(True).AllowEditing(True).AllowDeleting(True).Mode(Syncfusion.EJ2.Grids.EditMode.Dialog)).Toolbar(New List(Of String)(New String() {"Add", "Edit", "Delete", "Update", "Cancel"})).Render()}


                                                                                </div>).Render()

        </div>
        </div>


        <script>
            function load() {
                this.columns[0].commands[0].buttonOption.click = function (args) {
                    var dialog = document.getElementById("dialog").ej2_instances[0];
                    dialog.show();
                };
            };
        </script>





        @*<div id='container2' style="height:400px;" class="control">
            @Html.EJS().Button("targetButton").Content("Open Dialog").Render()

            <div id="DivDisplay" style="display:none;">
                @Html.EJS().Dialog("dialog2").Visible(False).IsModal(True).Header("Outer Dialog").ShowCloseIcon(True).Target("#container2").Height("300px").CloseOnEscape(False).Width("400px").AnimationSettings(New DialogAnimationSettings() With {.Effect = DialogEffect.None}).Created("onCreated").ContentTemplate(
                                                                @@<div>
                                                                    {@Html.EJS().Grid("Grid3").Height("300").Width("1220").Columns(Sub(col)
                                                                                                                                       col.Field("UserID").HeaderText("UserID").IsPrimaryKey(True).Visible(True).Add()
                                                                                                                                   End Sub).EditSettings(Sub(edit) edit.AllowAdding(True).AllowEditing(True).AllowDeleting(True).Mode(Syncfusion.EJ2.Grids.EditMode.Dialog)).Toolbar(New List(Of String)(New String() {"Add", "Edit", "Delete", "Update", "Cancel"})).Render()}


                                                                </div>).Render()
                @@<Button Class="e-control e-btn" id="innerButton" role="button">Open InnerDialog</Button>).Render()
            </div>
        </div>



        <script>
            document.getElementById("targetButton").addEventListener('click', function () {
                $('#DivDisplay').show();
                var dialog = document.getElementById("dialog2").ej2_instances[0];
                dialog.show();
            });
        </script>*@
        @Html.EJS().ScriptManager()
