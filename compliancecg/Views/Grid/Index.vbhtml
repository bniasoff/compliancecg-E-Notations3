@Code
    ViewBag.Title = "Grid UrlAdaptor"

    Dim DeptList = New Syncfusion.EJ2.DropDowns.DropDownList() With
{
.DataSource = ViewBag.dropdowndata,
.Query = "new ej.data.Query()",
.Fields = New Syncfusion.EJ2.DropDowns.DropDownListFieldSettings() With {.Value = "value", .Text = "text"},
.AllowFiltering = False
}

End code
    <div>
        <B>Master Grid</B>

        @Html.EJS().Grid("Grid").DataSource(Function(DataManager) DataManager.Url("/Home/UrlDatasource").InsertUrl("/Home/Insert").UpdateUrl("/Home/Update").RemoveUrl("/Home/Delete").Adaptor("UrlAdaptor")).AllowPaging(True).Width("auto").Columns(Sub(col)
                                                                                                                                                                                                                                                          col.Field("OrderID").HeaderText("OrderID").IsPrimaryKey(True).Add()
                                                                                                                                                                                                                                                      End Sub).EditSettings(Sub(edit) edit.AllowAdding(True).AllowEditing(True).AllowDeleting(True).Mode(Syncfusion.EJ2.Grids.EditMode.Dialog)).Toolbar(New List(Of String)(New String() {"Add", "Edit", "Delete", "Update", "Cancel"})).Render()
    </div>

<script>
        function DisplayDescription(field, data, column) {

        var coldata = column.edit.params.dataSource;
        for (var i = 0; i < coldata.length; i++) {
            if (data.EmployeeID == coldata[i]['value'])
                return coldata[i]['text'];
        }
    }

</script>