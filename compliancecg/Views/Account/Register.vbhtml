@ModelType RegisterViewModel
@Code
    ViewBag.Title = "Register"
End Code



@Using Html.BeginForm("Register", "Account", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})

    @Html.AntiForgeryToken()

    @<text>
        <div class="loginContainer d-flex flex-column">
            <div class="mainRow  align-self-center ">
                <h2 style="font-size: 2rem;">Create a new account</h2>
              
                @Html.ValidationSummary("", New With {.class = "text-danger"})
                <div class="form-group">
                    @Html.LabelFor(Function(m) m.Email, New With {.class = "mb-0"})
                    <div >
                        @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control"})
                    </div>
                </div>
                <div class="form-group">
                    @Html.LabelFor(Function(m) m.Password, New With {.class = "mb-0"})
                    <div >
                        @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control"})
                    </div>
                </div>
                <div class="form-group">
                    @Html.LabelFor(Function(m) m.ConfirmPassword, New With {.class = "mb-0"})
                    <div >
                        @Html.PasswordFor(Function(m) m.ConfirmPassword, New With {.class = "form-control"})
                    </div>
                </div>

                <div class="form-group">
                    @Html.LabelFor(Function(m) m.LastName, New With {.class = "mb-0"})
                    <div >
                        @Html.TextBoxFor(Function(m) m.LastName, New With {.class = "form-control"})
                    </div>
                </div>

                <div class="form-group">
                    @Html.LabelFor(Function(m) m.FirstName, New With {.class = "cmb-0"})
                    <div >
                        @Html.TextBoxFor(Function(m) m.FirstName, New With {.class = "form-control"})
                    </div>
                </div>


                <div class="form-group text-center">
                    <input type="submit" class="btn btn-success px-4" value="Register" />
                </div>
            </div>
            </div>
        </text>
End Using

        @section Scripts
            @Scripts.Render("~/bundles/jqueryval")
        End Section
