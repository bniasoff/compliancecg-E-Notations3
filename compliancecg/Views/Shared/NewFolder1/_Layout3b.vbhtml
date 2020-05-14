<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - CCG Compliance</title>


    @Scripts.Render("~/bundles/jquery")

    @Html.DevExpress().GetStyleSheets(
                                                                                                                                                             New StyleSheet With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
                                                                                                                                                             New StyleSheet With {.ExtensionSuite = ExtensionSuite.RichEdit},
                                                                                                                                                             New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
                                                                                                                                                             New StyleSheet With {.ExtensionSuite = ExtensionSuite.Scheduler},
                                                                                                                                                             New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
                                                                                                                                                             New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid},
                                                                                                                                                             New StyleSheet With {.ExtensionSuite = ExtensionSuite.HtmlEditor},
                                                                                                                                                             New StyleSheet With {.ExtensionSuite = ExtensionSuite.Chart},
                                                                                                                                                             New StyleSheet With {.ExtensionSuite = ExtensionSuite.Report},
                                                                                                                                                             New StyleSheet With {.ExtensionSuite = ExtensionSuite.TreeList})

    @Html.DevExpress().GetScripts(
                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.RichEdit},
                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.Editors},
                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.Scheduler},
                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.GridView},
                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid},
                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.HtmlEditor},
                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.Editors},
                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.Chart},
                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.Report},
                                                                                           New Script With {.ExtensionSuite = ExtensionSuite.TreeList})





    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("~/bundles/bootstrap")

    @Styles.Render("~/Content/ej2/dist/material.css")
    @Scripts.Render("~/Content/ej2/dist/ej2.min.js")




    <script src="~/Scripts/ej2/ej2.min.js"></script>
    <link rel="stylesheet" href="https://cdn.syncfusion.com/ej2/material.css" />
    @*<link href="~/Scripts/Bootstrap/bootsnav.css" rel="stylesheet" />*@
</head>


<body>

    <div class="navbar navbar-fixed-top">
        <div style="position:relative;margin-top:2px; width:100%;">
            <div class="container" style=" width:100%;">



                <div class="row">


                    @*<div style="float:left;">
                            <img src="~/Content/Images/Logos/CCG_LOGO_COLOR.png" height="100" />
                        </div>*@



                    <div style="float:left;" class="col-md-12">
                        <nav class="navbar navbar-default navbar-mobile bootsnav on">
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbar-menu">
                                    <i class="fa fa-bars"></i>
                                </button>
                            </div>


                            @*<a class="navbar-brand" href="#">
                                    <img  id="brandImage" src="~/Content/Images/Logos/CCG_LOGO_COLOR.png" height="100" />
                                    <img id="brandImage" src="~/Content/Images/Logos/CCG_LOGO_BW.jpg" height="100"  />
                                </a>*@
                            <ul style="margin-left:200px" class="nav navbar-nav" data-in="fadeInDown" data-out="fadeOutUp">
                                <li id="home" class="active"><a href="javascript:SetActive('home')" data-hover="Home">Home <span data-hover="Home"></span></a></li>
                                <li id="about"><a href="javascript:SetActive('about')" data-hover="About">About <span data-hover="About"></span></a></li>

                                <li id="forms" class="dropdown">
                                    <a href="javascript:SetActive('forms')" class="dropdown-toggle" data-toggle="dropdown" data-hover="Forms">Forms<span data-hover="Shortcodes"></span></a>
                                    <ul class="dropdown-menu animated">
                                        <li id="generalforms"><a href="javascript:myFunc('generalforms')">General Forms</a></li>
                                        <li id="acknolwedgmentforms"><a href="javascript:myFunc('acknolwedgmentforms')">Acknolwedgment Forms</a></li>
                                        <li id="complianceofficer"><a href="javascript:myFunc('complianceofficer')">Compliance Officer</a></li>
                                        <li id="posters"><a href="javascript:myFunc('posters')">Posters</a></li>
                                        <li id="statespecific"><a href="javascript:myFunc('statespecific')">State Specific</a></li>
                                        <li id="training"><a href="javascript:myFunc('training')">Training</a></li>

                                        @*<li><a href="#">Custom Menu</a></li>
                                            <li class="dropdown">
                                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">Sub Menu</a>
                                                <ul class="dropdown-menu animated">
                                                    <li><a href="#">Custom Menu</a></li>
                                                    <li><a href="#">Custom Menu</a></li>
                                                    <li class="dropdown">
                                                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">Sub Menu</a>
                                                        <ul class="dropdown-menu multi-dropdown animated">
                                                            <li><a href="#">Custom Menu</a></li>
                                                            <li><a href="#">Custom Menu</a></li>
                                                            <li><a href="#">Custom Menu</a></li>
                                                            <li><a href="#">Custom Menu</a></li>
                                                        </ul>
                                                    </li>
                                                    <li><a href="#">Custom Menu</a></li>
                                                </ul>
                                            </li>*@

                                    </ul>
                                </li>


                                <li id="facilities"> <a href="javascript:SetActive('facilities')" data-hover="Search">Facilities <span data-hover="Facilities"></span></a></li>
                                <li id="search" style="display:none"> <a href="javascript:SetActive('search')" data-hover="Search">Search <span data-hover="Search"></span></a></li>

                                @*<ul class="dropdown-menu animated">
                                        <li><a href="#">Custom Menu</a></li>
                                        <li><a href="#">Custom Menu</a></li>
                                        <li class="dropdown">
                                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Sub Menu</a>
                                            <ul class="dropdown-menu animated">
                                                <li><a href="#">Custom Menu</a></li>
                                                <li><a href="#">Custom Menu</a></li>
                                                <li class="dropdown">
                                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Sub Menu</a>
                                                    <ul class="dropdown-menu multi-dropdown animated">
                                                        <li><a href="#">Custom Menu</a></li>
                                                        <li><a href="#">Custom Menu</a></li>
                                                        <li><a href="#">Custom Menu</a></li>
                                                        <li><a href="#">Custom Menu</a></li>
                                                    </ul>
                                                </li>
                                                <li><a href="#">Custom Menu</a></li>
                                            </ul>
                                        </li>
                                        <li><a href="#">Custom Menu</a></li>
                                        <li><a href="#">Custom Menu</a></li>
                                        <li><a href="#">Custom Menu</a></li>
                                        <li><a href="#">Custom Menu</a></li>
                                    </ul>

                                    </li>*@
                                <li id="policies"><a href="javascript:SetActive('policies')" data-hover="policies">Policies <span data-hover="policies"></span></a></li>
                                <li id="contact"> <a href="javascript:SetActive('contact')" data-hover="contact">Contact<span data-hover="contact"></span></a></li>



                            </ul>


                            <div class="navbar-collapse collapse">
                                @Html.Partial("_LoginPartial3")
                            </div>
                        </nav>
                    </div>
                </div>
            </div>
        </div>
    </div>

    @If Request.IsAuthenticated = True Then
        @<div id="wrapper">
            <div class="col-lg-12 col-sm-12 col-md-12">
                <div Class="main-content" id="main-text">
                    @Using (Html.BeginForm())
                        @<div style="margin-top:20px;margin-left:10px;">
                            <div class="watermark">
                                <div class="watermark-image" style="background-repeat: no-repeat;"></div>
                                <div id="PartialView"></div>
                            </div>
                        </div>
                    End Using
                    @RenderBody()
                </div>
            </div>
        </div>
    End If

    @If Request.IsAuthenticated = False Then
        @<div id="page-content-wrapper">
            <div class="container-fluid">
                @Using (Html.BeginForm())
                    @<div style="margin-top:20px;margin-left:20px;">
                        <div class="watermark">
                            <div class="watermark-image" style="background-repeat: no-repeat;"></div>
                            @*<div id="PartialView"></div>*@
                            @RenderBody()
                        </div>
                    </div>
                End Using
            </div>
        </div>
    End If


    @RenderSection("scripts", required:=False)
    @Html.EJS().ScriptManager()

    <script type="text/javascript">
        MVCxClientGlobalEvents.AddControlsInitializedEventHandler(onControlsInitialized);
        ASPxClientControl.GetControlCollection().BrowserWindowResized.AddHandler(onBrowserWindowResized);
    </script>








    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.0.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" integrity="sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS" crossorigin="anonymous"></script>
    <script src="~/Scripts/Bootstrap/bootsnav.js"></script>





    @RenderSection("scripts", required:=False)
    @Html.EJS().ScriptManager()

    <script type="text/javascript">
        MVCxClientGlobalEvents.AddControlsInitializedEventHandler(onControlsInitialized);
        ASPxClientControl.GetControlCollection().BrowserWindowResized.AddHandler(onBrowserWindowResized);
    </script>
</body>
</html>



<script>
    function SetActive(arg) {
        debugger;
        if (arg == 'facilities') {
            var ajax = new ej.base.Ajax("/Facilities/Main", 'POST', true);
            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });

</script>





<style>

    #brandImage {
        width: 100px;
        margin-top: -20px;
    }


    body {
        padding-top: 40px;
    }


    :root {
        --bg_color: #073a78;
        --text_color: white;
    }

    nav.navbar.bootsnav {
        background-color: var(--bg_color);
        font-family: 'Fira Sans', sans-serif;
        padding: 8px;
        /*margin-bottom: 75px;*/
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