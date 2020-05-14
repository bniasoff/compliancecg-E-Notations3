﻿
   
      
<div class="row menuRow p-1">
    <ul class="nav text-white mx-3">
        <li id="policies" Class="nav-item p-2 m-2"><a href="javascript:SetActive('policies')" data-hover="policies">Policies <span data-hover="policies"></span></a></li>

        <li id="resources" class="dropdown nav-item p-2  m-2">
            <a href="javascript:SetActive('resources')" class="dropdown-toggle " data-toggle="dropdown" >Resources</a>
            <ul class="dropdown-menu">
                <li class="dropdown-item" id="acknowledgmentforms"><a href="javascript:myFunc3('acknowledgmentforms')">Acknowledgment Forms</a></li>
                <li class="dropdown-item" id="complianceofficer"><a href="javascript:myFunc3('complianceofficer')">Compliance Officer</a></li>
                <li class="dropdown-item" id="exclusionlist"><a href="javascript:myFunc3('exclusionlist')">Exclusion List</a></li>
                <li class="dropdown-item" id="generalformsinformation"><a href="javascript:myFunc3('generalformsinformation')">General Forms/Information</a></li>
                <li class="dropdown-item" id="hipaa"><a href="javascript:myFunc3('hipaa')">HIPAA</a></li>
                <li class="dropdown-item" id="humanresources"><a href="javascript:myFunc3('humanresources')">Human Resources</a></li>
                <li class="dropdown-item" id="requiredposters"><a href="javascript:myFunc3('requiredposters')">Required Posters</a></li>
            </ul>
        </li>


        <li id="facilities" Class="nav-item p-2  m-2"> <a href="javascript:SetActive('facilities')" data-hover="facilities">Facilities <span data-hover="Facilities"></span></a></li>
        <li id="search" style="display:none" class="nav-item p-2 m-2"> <a href="javascript:SetActive('search')" data-hover="Search">Search <span data-hover="Search"></span></a></li>
        <li id="training" Class="nav-item p-2  m-2"> <a href="javascript:SetActive('training')" data-hover="training">Yearly Training <span data-hover="training"></span></a></li>
    </ul>
    <div style="margin-left: auto;align-self: center;" class="pr-3  "><a href="javascript:document.getElementById('logoutForm').submit()">Log out</a></div>
            
    @Html.Partial("_LoginPartial")
</div>


        






<script>

  
   
    $('#search').hide();
    //$('#search').show();

    $.post('/Home/GetLoginName')
        .done(function (data) {

            if (data.length > 2) {
                if (data == 'info@compliancecg.com') { $('#search').show(); }

                if (data.includes("staff") == true) {
                    $('#facilities').hide();
                    $('#resources').hide();
                    $('#training').hide();
                    $('#forms').hide();
                }
            }

            //error: function () {
            //    alert("error");
            //}
        });


    function SetActive(arg) {
        $("#page-content-wrapper").removeClass();
        $("#page-content-wrapper").addClass("page_" + arg);


        jQuery("#home").removeClass("active");
        jQuery("#facilities").removeClass("active");
        jQuery("#forms").removeClass("active");
        jQuery("#search").removeClass("active");
        jQuery("#policies").removeClass("active");
        jQuery("#about").removeClass("active");
        jQuery("#contact").removeClass("active");
        jQuery("#whatsnew").removeClass("active");
        jQuery("#audits").removeClass("active");
        jQuery("#hotline").removeClass("active");
        jQuery("#resources").removeClass("active");
        jQuery("#training").removeClass("active");





        var current = jQuery("#" + arg).addClass("active");

        if (arg == 'about') {
            //var ajax = new ej.base.Ajax("/Facilities/Main", 'POST', true);
            //ajax.send().then((data) => {
            //    $("#PartialView").html(data);
            //});
        };


        if (arg == 'training') {
            var ajax = new ej.base.Ajax('/Training/Index?Folder=' + arg, 'POST', true);

            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });
        };

        if (arg == 'facilities') {

            var ajax = new ej.base.Ajax("/Facilities/Main", 'POST', true);
            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });
        };

        if (arg == 'policies') {
            var ajax = new ej.base.Ajax("/Policies/TreeView", 'POST', true);
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



    }

    function myFunc(arg) {

        //url: '../Policies/LoadDocument?FileName=' + file + '&State=' + State + '&SelectedFacilityName=' + FacilityName,

        if (arg != null) {
            var ajax = new ej.base.Ajax('/Forms/Index?Folder=' + arg, 'POST', true);
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


    function myFunc2(arg) {
        if (arg != null) {
            var ajax = new ej.base.Ajax('/Forms/Index?Folder=' + arg, 'POST', true);
            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });
        };
    };

    function myFunc3(arg) {
        if (arg != null) {

         $("#page-content-wrapper").removeClass();
            $("#page-content-wrapper").addClass("page_resources"); 

        jQuery(".nav-item").removeClass("active");
        var current = jQuery("#resources" ).addClass("active");

            var ajax = new ej.base.Ajax('/Forms/Resources?File=' + arg, 'POST', true);
            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });
        };
    };
</script>




<style>

 



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


        nav.navbar.bootsnav ul.nav {
            height: 100px;
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