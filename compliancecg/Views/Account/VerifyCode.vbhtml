@ModelType VerifyCodeViewModel
@Code
    ViewBag.Title = "Verify"
End Code



@Using Html.BeginForm("VerifyCode", "Account", New With {.ReturnUrl = Model.ReturnUrl}, FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
@Html.AntiForgeryToken()
@Html.Hidden("rememberMe", Model.RememberMe)
@<text>
    <div Class="loginContainer d-flex flex-column" style="margin-top: 20px;">
        <div Class="mainRow  align-self-center ">
            <h2>@ViewBag.Title</h2>

            <h6 style=" max-width: 300px;">A login verification code has been emailed to you.  Please check your email and enter the verification code below to authenticate your login.</h6>
            <hr />
            @Html.ValidationSummary("", New With {.class = "text-danger"})
            <div class="form-group">
                @Html.LabelFor(Function(m) m.Code, New With {.class = "col-md-2 control-label"})
                <div class="col-md-10">
                    @Html.TextBoxFor(Function(m) m.Code, New With {.class = "form-control"})
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <div class="col-md-offset-2 col-md-10">
                    <div class="checkbox">
                        @Html.CheckBoxFor(Function(m) m.RememberBrowser)
                        @Html.LabelFor(Function(m) m.RememberBrowser)
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <input type="submit" class="btn btn-success px-4" value="Submit" />
                </div>
            </div>
        </div>
  </div>
</text>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
