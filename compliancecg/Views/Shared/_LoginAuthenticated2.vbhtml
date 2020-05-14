﻿<div class="row">
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
                        <li id="acknolwedgmentforms"><a href="javascript:myFunc('acknolwedgmentforms')">Acknolwedgment Forms</a></li>
                        <li id="complianceofficer"><a href="javascript:myFunc('complianceofficer')">Compliance Officer</a></li>
                        <li id="generalforms"><a href="javascript:myFunc('generalforms')">General Forms</a></li>
                        <li id="misc"><a href="javascript:myFunc('misc')">Misc</a></li>
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
                @Html.Partial("_LoginPartial")
            </div>
        </nav>
    </div>
</div>




<script>
    function SetActive(arg) {
        jQuery("#home").removeClass("active");
        jQuery("#facilities").removeClass("active");
        jQuery("#forms").removeClass("active");
        jQuery("#search").removeClass("active");
        jQuery("#policies").removeClass("active");
        jQuery("#about").removeClass("active");
        jQuery("#contact").removeClass("active");
        var current = jQuery("#" + arg).addClass("active");

        if (arg == 'facilities') {

            var ajax = new ej.base.Ajax("/Facilities/Main", 'POST', true);
            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });
        };


        if (arg == 'search') {
            var ajax = new ej.base.Ajax("/Facilities/Search", 'POST', true);
            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });
        };

        //if (arg == 'forms') {
        //    var ajax = new ej.base.Ajax("/Forms/Index", 'POST', true);
        //    ajax.send().then((data) => {
        //        $("#PartialView").html(data);
        //    });
        //};

        if (arg == 'policies') {
            var ajax = new ej.base.Ajax("/Policies/TreeView", 'POST', true);
            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });
        };

    }

    function myFunc(arg) {
         //url: '../Policies/LoadDocument?FileName=' + file + '&State=' + State + '&SelectedFacilityName=' + FacilityName,

        if (arg != null) {    
            var ajax = new ej.base.Ajax('/Forms/Index?Folder='  + arg , 'POST', true);
            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });
        };



        //jQuery("#generalforms").addClass("active");
        //jQuery("#acknolwedgmentforms").addClass("active");
        //jQuery("#complianceofficer").addClass("active");
        //jQuery("#posters").addClass("active");
        //jQuery("#statespecific").addClass("active");
        //jQuery("#training").addClass("active");
    }
</script>




<style> 

    body {
        /*padding-top: 40px;*/      
        padding: 0px
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
            color: red;
            /*color: var(--text_color) !important;*/
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
        background-color: dodgerblue;
        /*background-color: var(--bg_color);*/
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