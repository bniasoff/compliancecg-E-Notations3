@ModelType ForgotPasswordViewModel
@Code
    ViewBag.Title = "Forgot your password?"
End Code
    <div class="loginContainer d-flex flex-column">
        <div class="mainRow  align-self-center ">
            <h2>@ViewBag.Title</h2>
            <section id="forgotForm">
                @Using Html.BeginForm("ForgotPassword", "Account", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
                    @Html.AntiForgeryToken()
                    @<text>

                        <h4>Enter your email.</h4>
                        <hr />
                        @Html.ValidationSummary("", New With {.class = "text-danger"})
                        <div class="form-group">
                            @Html.LabelFor(Function(m) m.Email, New With {.class = "col-md-2 control-label"})
                            <div class="col-md-10">
                                @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control"})
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <input type="submit" class="btn btn-success px-4" value="Email Link" />
                            </div>
                        </div>
                    </text>
                End Using
            </section>
        </div>
    </div>
        @section Scripts
            @Scripts.Render("~/bundles/jqueryval")
        End Section
