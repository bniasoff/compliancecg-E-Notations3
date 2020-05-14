@*<div><span>Login3</span></div>*@

@Code
    ViewData("Title") = "Menu"
    'Layout = Nothings
End Code

@*https://bestjquery.com/tutorial/menu/demo62/*@

<link href="~/js/bootsnav.css" rel="stylesheet" />
<style>
    :root {
        --bg_color: #dfe6e9;
        --text_color: #555;
    }

    nav.navbar.bootsnav {
        background-color: var(--bg_color);
        font-family: 'Fira Sans', sans-serif;
        padding: 8px;
        margin-bottom: 150px;
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
            background-color: #fff !important;
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



<div class="demo">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <nav class="navbar navbar-default navbar-mobile bootsnav on">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbar-menu">
                            <i class="fa fa-bars"></i>
                        </button>
                    </div>

                    <div class="collapse navbar-collapse" id="navbar-menu">
                        <ul class="nav navbar-nav" data-in="fadeInDown" data-out="fadeOutUp">

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



                            <li id="search"> <a href="javascript:SetActive('search')" data-hover="Search">Search <span data-hover="Search"></span></a></li>

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
                    </div>
                </nav>
            </div>
        </div>
    </div>
</div>

<script>
    function SetActive(arg) {
        jQuery("#home").removeClass("active");
        jQuery("#about").removeClass("active");
        jQuery("#forms").removeClass("active");
        jQuery("#search").removeClass("active");
        jQuery("#policies").removeClass("active");
        jQuery("#contact").removeClass("active");
        var current=jQuery("#" + arg).addClass("active");
    }

    function myFunc(arg) {
        //jQuery("#generalforms").addClass("active");
        //jQuery("#acknolwedgmentforms").addClass("active");
        //jQuery("#complianceofficer").addClass("active");
        //jQuery("#posters").addClass("active");
        //jQuery("#statespecific").addClass("active");
        //jQuery("#training").addClass("active");
    }
</script>

<script type="text/javascript" src="https://code.jquery.com/jquery-1.12.0.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" integrity="sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS" crossorigin="anonymous"></script>
<script src="~/js/bootsnav.js"></script>