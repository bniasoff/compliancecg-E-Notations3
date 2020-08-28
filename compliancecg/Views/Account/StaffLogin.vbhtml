@ModelType LoginViewModel
@Code
    ViewBag.Title = "Authenticate your login"
End Code


@Using Html.BeginForm("StaffLogin", "Account", New With { .ReturnUrl = ViewBag.ReturnUrl }, FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
@*Html.AntiForgeryToken()*@

@<text>
    <div class="loginContainer d-flex flex-column">
        <div class="mainRow  align-self-center ">
            <h2>@ViewBag.Title</h2>

            @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
            <p class="text-info">
                Please have an administrator login to authenticate this login request.
            </p>
            <div class="form-group">
                @Html.LabelFor(Function(m) m.Email, New With {.class = "mb-0"})
                <div class="">
                    @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control", .Value = "" })
                    @Html.ValidationMessageFor(Function(m) m.Email, "", New With {.class = "text-danger"})
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(Function(m) m.Password, New With {.class = "mb-0"})
                <div class="">
                    @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control"})
                    @Html.ValidationMessageFor(Function(m) m.Password, "", New With {.class = "text-danger"})
                </div>
            </div>
            <div class="form-group">
                <div class="form-group mb-0">
                    <input type="submit" class="btn  btn-success px-4" value="Login" />
                </div>
            </div>
        </div>
    </div>
</text>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
