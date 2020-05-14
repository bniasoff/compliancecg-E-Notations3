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


<link href="~/Scripts/Bootstrap/bootsnav.css" rel="stylesheet" />
<style>
         body {
        padding: 0px;
    }


    :root {
        --bg_color: #073a78;
        --text_color: white;
    }

    nav.navbar.bootsnav {
        background-color: var(--bg_color);
        font-family: 'Fira Sans', sans-serif;
        padding: 8px;
        margin-bottom: 75px;
        border: none;
    }

        nav.navbar.bootsnav ul.nav > li {
            margin-right: 20px;
        }

            nav.navbar.bootsnav ul.nav > li > a {
                color: var(--text_color);
                background-color: transparent;
                font-size: 15px;
                font-weight: 600;
                text-transform: uppercase;
                padding: 8px 15px;
                border-radius: 10px;
                overflow: hidden;
                position: relative;
                z-index: 1;
                transition: all .5s ease;
            }

            nav.navbar.bootsnav ul.nav > li.dropdown > a {
                padding: 8px 30px 8px 15px;
            }

            nav.navbar.bootsnav ul.nav > li.active > a,
            nav.navbar.bootsnav ul.nav > li.active > a:hover,
            nav.navbar.bootsnav ul.nav > li > a:hover,
            nav.navbar.bootsnav ul.nav > li.on > a {
                color: var(--text_color);
                background: transparent !important;
                box-shadow: 0 0 5px var(--text_color) inset;
                border-color: transparent;
            }

        nav.navbar.bootsnav li.dropdown ul.dropdown-menu.megamenu-content li a:hover,
        nav.navbar.bootsnav li.dropdown ul.dropdown-menu li a:hover,
        nav.navbar.bootsnav li.dropdown ul.dropdown-menu li a.dropdown-toggle:active,
        nav.navbar ul.nav li.dropdown.on ul.dropdown-menu li.dropdown.on > a {
            color: var(--text_color) !important;
            /*background-color: #000 !important;*/
        }

        nav.navbar.bootsnav ul.nav > li.dropdown > a.dropdown-toggle:after {
            color: var(--text_color);
            position: absolute;
            top: 8px;
            right: 10px;
            margin: 0 0 0 7px;
        }

        nav.navbar.bootsnav ul.nav > li.dropdown > ul {
            opacity: 0;
            visibility: hidden;
            transform: perspective(600px) rotateY(-30deg) scaleX(0);
            transform-origin: top center;
            transition: all 0.5s ease-in-out 0s;
        }

        nav.navbar.bootsnav ul.nav > li.dropdown.on > ul {
            opacity: 1 !important;
            visibility: visible !important;
            transform: perspective(600px) rotateY(0deg) scale(1);
        }

    .dropdown-menu.multi-dropdown {
        position: absolute;
        left: -100% !important;
    }

    nav.navbar.bootsnav li.dropdown ul.dropdown-menu {
        background-color: var(--bg_color);
        border: none;
        border-radius: 0 0 10px 10px;
        top: 124%;
    }

        nav.navbar.bootsnav li.dropdown ul.dropdown-menu.megamenu-content {
            top: 100%;
        }

            nav.navbar.bootsnav li.dropdown ul.dropdown-menu.megamenu-content li {
                font-size: 14px;
            }

            nav.navbar.bootsnav li.dropdown ul.dropdown-menu.megamenu-content .menu-col li a {
                padding-left: 10px;
            }

            nav.navbar.bootsnav li.dropdown ul.dropdown-menu.megamenu-content .title {
                color: var(--text_color);
                font-size: 16px;
                font-weight: bold;
            }

    @@media only screen and (max-width:990px) {
        .dropdown-menu.multi-dropdown {
            left: 0 !important;
        }

        nav.navbar.bootsnav .navbar-toggle {
            color: var(--text_color);
            background: transparent !important;
        }

        nav.navbar.bootsnav ul.nav > li {
            margin: 5px auto 15px;
        }

        nav.navbar.bootsnav.navbar-mobile .navbar-collapse {
            background-color: var(--bg_color);
        }

        nav.navbar.bootsnav.navbar-mobile ul.nav > li > a {
            text-align: center;
            padding: 10px 15px;
            border: none;
        }

        nav.navbar.bootsnav ul.nav > li.dropdown > a {
            padding: 10px 10px 10px 17px;
        }

            nav.navbar.bootsnav ul.nav > li.dropdown > a.dropdown-toggle:before {
                color: var(--text_color);
            }

        nav.navbar.bootsnav ul.nav li.dropdown ul.dropdown-menu > li > a {
            color: var(--text_color);
            padding-left: 10px;
            border-bottom-color: none;
        }

        nav.navbar.bootsnav ul.nav > li.dropdown > ul {
            top: 100%;
        }

        nav.navbar.bootsnav li.dropdown ul.dropdown-menu.megamenu-content .title {
            font-size: 14px;
            font-weight: normal;
            color: var(--text_color);
        }

        nav.navbar.bootsnav li.dropdown ul.dropdown-menu.megamenu-content .col-menu li a {
            color: var(--text_color);
        }
    }
</style>

<div style="position:relative;margin-top:2px; width:100%; background-color:#000">
    <div style="float:left; margin-left:200px; width:200px">
        <div class="demo">
            <div class="container" style="width:880px">
                <div class="row">
                    <div class="col-md-12">
                        <nav class="navbar navbar-default navbar-mobile bootsnav on">
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbar-menu">
                                    <i class="fa fa-bars"></i>
                                </button>
                            </div>

                            @*@<ul class="nav navbar-nav navbar-right">
                                <li>@Html.ActionLink("Register", "Register", "Account", routeValues:=Nothing, htmlAttributes:=New With {.id = "registerLink"})</li>
                                <li>@Html.ActionLink("Log in", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {.id = "loginLink"})</li>
                            </ul>

                            <div class="collapse navbar-collapse" id="navbar-menu">
                                <ul class="nav navbar-nav" data-in="fadeInDown" data-out="fadeOutUp">
                                    <li id="home" class="active"><a href="javascript:SetActive('home')" data-hover="Home">Login <span data-hover="Home"></span></a></li>
                                    <li id="facilities"><a href="javascript:SetActive('facilities')" data-hover="Facilities">Register <span data-hover="Facilities"></span></a></li>
                                </ul>
                            </div>*@


                            <div class="collapse navbar-collapse" id="navbar-menu">
                                <ul class="nav navbar-nav navbar-right"  data-in="fadeInDown" data-out="fadeOutUp">
                                    <li>@Html.ActionLink("Register", "Register", "Account", routeValues:=Nothing, htmlAttributes:=New With {.id = "registerLink"})</li>
                                    <li>@Html.ActionLink("Log in", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {.id = "loginLink"})</li>
                                </ul>
                            </div>
                        </nav>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

@*@If Request.IsAuthenticated Then
    @Using Html.BeginForm("LogOff", "Account", FormMethod.Post, New With {.id = "logoutForm", .class = "navbar-right"})
        @Html.AntiForgeryToken()
        @<ul class="nav navbar-nav navbar-right">
             <li>
                 @Html.ActionLink("Hello " + FullName + "!", "Index", "Manage", routeValues:=Nothing, htmlAttributes:=New With {.title = "Manage"})
            
             </li>
            <li><a href="javascript:document.getElementById('logoutForm').submit()">Log off</a></li>
        </ul>   End Using
Else
    @<ul class="nav navbar-nav navbar-right">
        <li>@Html.ActionLink("Register", "Register", "Account", routeValues:=Nothing, htmlAttributes:=New With {.id = "registerLink"})</li>
        <li>@Html.ActionLink("Log in", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {.id = "loginLink"})</li>
    </ul>
End If*@
