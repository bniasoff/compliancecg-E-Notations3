@Code
    ViewBag.Title = "Reset password confirmation"
End Code
    <div class="d-flex flex-column">
        <div class="mainRow  align-self-center mt-3">
            <hgroup class="title">
                <h1>@ViewBag.Title</h1>
            </hgroup>
            <div>
                <p>
                    Your password has been reset. Please @Html.ActionLink("click here to log in", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {Key .id = "loginLink"})
                </p>
            </div>
        </div>
     </div>
