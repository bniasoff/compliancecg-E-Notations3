@Code
    ViewData("Title") = "Home Page"
    If Request.IsAuthenticated = False Then
        'RedirectToAction("Login", "Account")
        Response.Redirect("/Account/Login")
    End If
End Code

<script>
    $(document).ready(function () {
       
        isAuthenticated = $("#isAuthenticated").val();
        //isAcknowledged = $("#Acknowledgement").val();

        if (isAuthenticated == "True" )//&& isAcknowledged == "True"
            SetActive('policies');

     });
</script>