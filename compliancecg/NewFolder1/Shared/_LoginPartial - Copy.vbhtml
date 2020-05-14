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


    ' Dim UserAccount As UserAccount = Context.Session("UserAccount")

    'If UserAccount Is Nothing Then
    '    UserAccount = AuthorizationService.GetUser(User.Identity.GetUserId)
    '    If UserAccount IsNot Nothing Then Context.Session("UserAccount") = UserAccount
    'End If

    'If UserAccount IsNot Nothing Then
    '    FullName = UserAccount.FullName
    'End If

End Code




    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <ul class="navbar-nav navbar-right mr-auto">
            <li>@Html.ActionLink("Register", "Register", "Account", routeValues:=Nothing, htmlAttributes:=New With {.id = "registerLink", .class = "nav-link"})</li>
            <li>@Html.ActionLink("Log in", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {.id = "loginLink", .class = "nav-link"})</li>
        </ul>
    </nav>



        @*<nav class="navbar navbar-expand-lg navbar-light bg-light">
                <a class="navbar-brand" href="#">CCG</a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>

                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav mr-auto">
                        <li class="nav-item active">
                            <a class="nav-link" href="#">Home <span class="sr-only">(current)</span></a>
                        </li>

                        <li class="nav-item">
                            <a class="nav-link" href="#">Link</a>
                        </li>
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                Dropdown
                            </a>
                            <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                                <a class="dropdown-item" href="#">Action</a>
                                <a class="dropdown-item" href="#">Another action</a>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item" href="#">Something else here</a>
                            </div>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link disabled" href="#">Disabled</a>
                        </li>
                    </ul>
                    <form class="form-inline my-2 my-lg-0">
                        <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
                        <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
                    </form>
                </div>
            </nav>*@

