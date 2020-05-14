@ModelType CCGData.CCGData.Facility
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>Facility</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Active)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Active)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Name)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Name)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.FullAddress)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.FullAddress)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Address)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Address)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.City)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.City)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.State)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.State)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Zip)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Zip)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Beds)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Beds)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.ShiftChange)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ShiftChange)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Phone1)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Phone1)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Phone2)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Phone2)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Fax)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Fax)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.FacilityType)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.FacilityType)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BeginDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BeginDate)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.ComplianceOfficer)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ComplianceOfficer)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Logo)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Logo)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.LogoPath)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.LogoPath)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DateCreated)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DateCreated)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DateModified)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DateModified)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.UserEditor)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UserEditor)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.FacilityGroup.GroupName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.FacilityGroup.GroupName)
        </dd>

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
