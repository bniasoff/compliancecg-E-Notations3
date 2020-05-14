@Imports Microsoft.AspNet.Identity
@imports  System.Security.Claims
@Imports compliancecg.Membership

@code
    Dim ClientIDClaim As Claim = Nothing

    Dim Principal As ClaimsPrincipal = TryCast(HttpContext.Current.User, ClaimsPrincipal)
    Dim UserID As String = Nothing
    Dim UserName As String = Nothing
    Dim LastName As String = Nothing
    Dim FirstName As String = Nothing

    If Principal IsNot Nothing Then
        If Principal.Claims IsNot Nothing Then
            Dim Claims = Principal.Claims
            For Each claim As Claim In Principal.Claims
                If claim.Type = "UserID" Then UserID = claim.Value
                If claim.Type = "UserName" Then UserName = claim.Value
                If claim.Type = "LastName" Then LastName = claim.Value
                If claim.Type = "FirstName" Then FirstName = claim.Value
            Next
        End If
    End If


    Dim FullName As String = Nothing
    If FirstName IsNot Nothing Then FullName = FirstName
    If LastName IsNot Nothing Then FullName = FullName + " " + LastName

    If FullName Is Nothing Then If UserName IsNot Nothing Then FullName = UserName
End Code


<script>
  //  function ShowSuccessMsg() {
  //      alert('Succes');

  //  };
  //  function ShowFailure(name) {
 //       toastr.error("Addition Failed.");
 //   };
</script>


@If Request.IsAuthenticated And Session("Acknowledgement") = True Then
    @Using Html.BeginForm("LogOff", "Account", FormMethod.Post, New With {.id = "logoutForm", .class = "navbar-right"})
        @Html.AntiForgeryToken()
    End Using
ElseIf Request.IsAuthenticated And Session("Acknowledgement") = False Then
    @Using Html.BeginForm("LogOff", "Account", FormMethod.Post, New With {.id = "logoutForm", .class = "navbar-right"})
        @Html.AntiForgeryToken()
    End Using
    @<div class="col align-self-center ml-4">
         <a href="javascript:document.getElementById('logoutForm').submit()">Log out</a>
    </div>
Else
    @<div class="col align-self-center ml-2">
        @Html.ActionLink("Login", "Login", "Account")
    </div>
    @<div Class="pt-2 pr-2 registerDiv">
        Don't have an account?   <div class="btn btn-light btn-sm px-3">@Html.ActionLink("Get Started", "Register")</div>
    </div>
End If


