@ModelType IEnumerable(Of CCGData.CCGData.Facility)
@Code
ViewData("Title") = "Index"
End Code

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.Active)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Name)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.FullAddress)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Address)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.City)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.State)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Zip)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Beds)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.ShiftChange)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Phone1)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Phone2)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Fax)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.FacilityType)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.BeginDate)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.ComplianceOfficer)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Logo)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.LogoPath)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.DateCreated)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.DateModified)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.UserEditor)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.FacilityGroup.GroupName)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Active)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Name)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.FullAddress)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Address)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.City)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.State)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Zip)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Beds)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.ShiftChange)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Phone1)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Phone2)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Fax)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.FacilityType)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.BeginDate)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.ComplianceOfficer)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Logo)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.LogoPath)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.DateCreated)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.DateModified)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.UserEditor)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.FacilityGroup.GroupName)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.FacilityID }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.FacilityID }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.FacilityID })
        </td>
    </tr>
Next

</table>
