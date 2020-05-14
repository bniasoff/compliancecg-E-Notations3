@modeltype   Models.EmailModel
@Code
    ViewBag.Title = "Contact"
End Code

<h2>@ViewBag.Title.</h2>

@Using Html.BeginForm()

    @Html.AntiForgeryToken()
    @<h4>Send your comments.</h4>
    @<hr />

    @<div Class="form-group">
        @Html.LabelFor(Function(m) m.FromName, New With {.class = "col-md-2 control-label"})
        <div Class="col-md-10">
            @Html.TextBoxFor(Function(m) m.FromName, New With {.class = "form-control"})
            @Html.ValidationMessageFor(Function(m) m.FromName)
        </div>
    </div>

    @<div Class="form-group">
        @Html.LabelFor(Function(m) m.FromEmail, New With {.class = "col-md-2 control-label"})
        <div Class="col-md-10">
            @Html.TextBoxFor(Function(m) m.FromEmail, New With {.class = "form-control"})
            @Html.ValidationMessageFor(Function(m) m.FromEmail)
        </div>
    </div>

    @<div Class="form-group">
        @Html.LabelFor(Function(m) m.Message, New With {.class = "col-md-2 control-label"})
        <div Class="col-md-10">
            @Html.TextAreaFor(Function(m) m.Message, New With {.class = "form-control"})
            @Html.ValidationMessageFor(Function(m) m.Message)
        </div>
    </div>

    @<div Class="form-group">
        <div Class="col-md-offset-2 col-md-10">
            <input type="submit" Class="btn btn-default" value="Send" />
        </div>
    </div>

End Using


@section Scripts
    @Scripts.Render("~/bundles/jqueryval")
end section