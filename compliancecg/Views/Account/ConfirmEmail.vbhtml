@Code
    ViewBag.Title = "Confirm Email"
End Code
<div class="d-flex flex-column">
    <div class="mainRow  align-self-center mt-3">
        <h2>@ViewBag.Title</h2>
        <div>
            <p  style="font-size: 18px;">
                Thank you for confirming your email. Please @Html.ActionLink("Click here to Log in", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {Key .id = "loginLink"})
            </p>
        </div>
    </div>
</div>
