@ModelType LoginViewModel
@Code
    ViewBag.Title = "Sign in"
End Code



<script src="~/Scripts/jquery-3.3.1.min.js"></script>
<div class="loginContainer d-flex flex-column">
    <div class="clearfix"></div>
    <div class="mainRow  align-self-center ">
        <h2>@ViewBag.Title</h2>
        <section id="loginForm">
            @Using Html.BeginForm("Login", "Account", New With {.ReturnUrl = ViewBag.ReturnUrl}, FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
                @Html.AntiForgeryToken()
                @<text>
                    <div class="pb-4">Enter your details below</div>

                    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                    <div class="form-group">
                        @Html.LabelFor(Function(m) m.Email, New With {.class = "mb-0"})
                        <div class="">
                            @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control"})
                            @Html.ValidationMessageFor(Function(m) m.Email, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                    <div class="form-group">
                        @Html.LabelFor(Function(m) m.Password, New With {.class = "mb-0"})
                        <p class="float-right mb-0 pPass">
                            @Html.ActionLink("Forgot password?", "ForgotPassword")
                        </p>
                        <div class="">
                            @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control"})
                            @Html.ValidationMessageFor(Function(m) m.Password, "", New With {.class = "text-danger"})
                        </div>
                    </div>


                    <div class="form-group  text-center">
                        <div class="checkbox">
                            @Html.CheckBoxFor(Function(m) m.RememberMe)
                            @Html.LabelFor(Function(m) m.RememberMe)
                        </div>
                    </div>
                    <div class="form-group text-center mb-0">
                        <input type="submit" value="Sign in" class="btn btn-success px-4" />
                    </div>

                    @*Enable this once you have account confirmation enabled for password reset functionality*@

                </text>
            End Using
        </section>

    </div>
</div>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
