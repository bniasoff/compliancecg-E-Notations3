<!DOCTYPE html>
<html lang="en">

<head>
    <title>Sidebar · Responsive Panel · Essential JS 2 for Javascript  · Syncfusion</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="description" content="This example demonstrates how to render TreeView for navigation purpose inside the Javascript Sidebar" />
    <meta name="author" content="Syncfusion" />
    <link rel="shortcut icon" href="https://ej2.syncfusion.com/home/favicon.ico" />
    <!-- Google Tag Manager -->
    <script>
        (function (w, d, s, l, i) {
            w[l] = w[l] || []; w[l].push({
                'gtm.start':
                    new Date().getTime(), event: 'gtm.js'
            }); var f = d.getElementsByTagName(s)[0],
                j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
                    'https://www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
        })(window, document, 'script', 'dataLayer', 'GTM-WLQL39J');</script>
    <!-- End Google Tag Manager -->
    <script type="text/javascript">
        var themeName = location.hash || 'material';
        themeName = themeName.replace('#', '');
        window.ripple = (themeName === "material")
        document.write('<link href="../../dist/' + themeName + '.css" rel="stylesheet">');
    </script>
    <script type="text/javascript">
        if (/MSIE \d|Trident.*rv:/.test(navigator.userAgent)) {
            document.write("<script src=\'https://cdnjs.cloudflare.com/ajax/libs/bluebird/3.3.5/bluebird.min.js\'>");
        }</script>


    @*<script src="../../dist/ej2.min.js" type="text/javascript"></script>*@
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="canonical" href="https://ej2.syncfusion.com/javascript/demos/sidebar/responsive-panel">
    @*<link href="index.css" rel="stylesheet">*@
<script src="~/Scripts/SideBar/ej2.min.js"></script>
    <link href="~/Scripts/SideBar/index.css" rel="stylesheet" />
 
    <style>

        #loader {
            color: #008cff;
            height: 40px;
            left: 45%;
            position: absolute;
            top: 45%;
            width: 30%;
        }

        body {
            touch-action: none;
        }
    </style>
</head>

<body class='e-view ej2-new' aria-busy="true">
    <!-- Google Tag Manager (noscript) -->
    <noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-WLQL39J" height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
    <!-- End Google Tag Manager (noscript) -->
    <div hidden id="sync-analytics" data-queue="EJ2 - Javascript (ES5) - Demos"></div>
    <div class="sample-browser">
        <div id='sample-header' class="sb-header" role="banner">
            <div class='sb-header-left sb-left sb-table'>
                <div class='sb-header-item sb-table-cell'>
                    <div class="header-logo">
                        <a href="https://ej2.syncfusion.com/home/javascript.html" target="" rel="noopener noreferrer">
                            <div class="footer-logo"> </div>
                        </a>
                    </div>

                </div>
                <div class="sb-header-splitter sb-download-splitter"></div>
                <div class='sb-header-item sb-table-cell'>
                    <div id='sb-header-text' class='e-sb-header-text'>

                        <span class='sb-header-text-left'>Essential JS 2 for JavaScript (ES5)</span>
                    </div>
                </div>
            </div>
            <div class='sb-header-right sb-right sb-table'>
                <div class='sb-header-item sb-table-cell'>
                    <div class='product-style'>
                        <div>
                            <a href="https://www.syncfusion.com/javascript-ui-controls/sidebar">PRODUCT DETAILS</a>
                        </div>
                    </div>
                </div>
                <div class='sb-header-item sb-table-cell'>
                    <div class="sb-header-item sb-table-cell sb-download-wrapper">
                        <a href="https://www.syncfusion.com/downloads/essential-js2" target="_blank">
                            <button id="download-now" class="sb-download-btn">
                                <span class="sb-download-text">DOWNLOAD</span>
                            </button>
                        </a>
                    </div>
                </div>
            </div>

        </div>
        <div id='sample-header' class="sb-headers" role="banner">
            <div class='sb-header-left sb-left sb-table'>
                <div class='sb-header-item sb-table-cell'>
                    <div class="syncfusion-logo">
                        <a href="https://ej2.syncfusion.com/home/javascript.html" target="" rel="noopener noreferrer">
                            <div class="sync-logo"></div>
                        </a>
                    </div>
                </div>
                <div class='sb-header-item sb-table-cell'>
                    <div id='sb-header-text' class='e-sb-header-text'>
                        <span class='sb-header-text-left'>Essential JS 2 for JavaScript (ES5)</span>
                    </div>
                </div>
            </div>
            <div class='sb-header-right sb-right sb-table'>
                <div class='sb-header-item sb-table-cell'>
                    <div class="product">
                        <a href="https://www.syncfusion.com/javascript-ui-controls/sidebar">
                            <div class="sb-icon-notification sb-icons"></div>
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div class='content e-view'>
            <div class='sample-content'>
                <div id="sample-bread-crumb" class="sb-bread-crumb">
                    <div class="sb-custom-item sb-sample-navigation sb-right sb-header-right sb-table-cell">
                        <div id="prev-sample" class="sb-navigation-prev e-control e-tooltip " aria-label="previous sample" title="Previous Sample">
                            <a href="https://ej2.syncfusion.com/javascript/demos/sidebar/sidebar-menu/" class=''><span class="sb-icons sb-icon-Previous"></span></a>

                        </div>
                        <div id="next-sample" class="sb-navigation-next e-control e-tooltip" aria-label="next sample" title="Next Sample">
                            <a href="https://ej2.syncfusion.com/javascript/demos/sidebar/sidebar-list/" class=''><span class="sb-icons sb-icon-Next"></span></a>

                        </div>
                    </div>
                    <h1 class="sb-bread-crumb-text">
                        <div class="category-allcontrols">
                            <a href="https://ej2.syncfusion.com/javascript/demos/">
                                <span>All Controls</span>
                            </a>
                        </div>
                        <div class="category-seperator sb-icons"> / </div>
                        <div class="category-text bread-ctext">
                            <a href="https://ej2.syncfusion.com/javascript/demos/#/material/sidebar/default.html">
                                <span>Sidebar</span>
                            </a>
                        </div>
                        <div class="category-seperator sb-icons"> / </div>
                        <div class="crumb-sample">Responsive Panel</div>
                    </h1>
                </div>
                <div class="control-content">
                    <div id="action-description">
                        <p>
                        <p>
                            <div class="layout" id="actionDes">
                        <p>
                            Click/Touch the button to view the Sidebar sample in new tab.
                        </p>
                    </div>
                    <a id="details" class="layout" onclick="desDetails()"> More Details...</a>
                    <script>
                        function desDetails() {
                            var element = document.getElementById('description-section');
                            if (element) {
                                element.scrollIntoView();
                            }
                        }
                    </script>
                    </p>
                    </p>
                </div>
                <div class="container-fluid">
                    <div class="control-section">
                        <div class="control-section">
                            <!-- sample level element  -->
                            <div class="col-lg-12 col-sm-12 col-md-12 center">
                                Click/Touch the button to view the sample
                            </div>
                            <div class="col-lg-12 col-sm-12 col-md-12 center">
                                <a class="e-btn" id="newTab" target="_blank">Open in new tab</a>
                            </div>
                            <!-- sample level element  -->
                            <div id="wrapper">
                                <!--header-section  declaration -->
                                <div class="main-header" id="header-section">
                                    <ul class="header-list">
                                        <li class="float-left header-style icon-menu" id="hamburger"></li>
                                        <li class="float-left header-style nav-pane"><b>Navigation Pane</b></li>
                                        <li class="header-style float-right support border-left"><b>Support</b></li>
                                    </ul>
                                </div>
                                <!-- end of header-section -->
                                <!-- sidebar element declaration -->
                                <aside id="sidebar-treeview">
                                    <div class="main-menu">
                                        <div class="table-content">
                                            <input type="text" placeholder="Search..." class="search-icon">
                                            <p class="main-menu-header">TABLE OF CONTENTS</p>
                                        </div>
                                        <div>
                                            <ul id="main-treeview"></ul>
                                        </div>
                                    </div>
                                </aside>
                                <!-- end of sidebar element -->
                                <!-- main-content declaration -->
                                <div class="main-content" id="main-text">
                                    <div class="sidebar-content">
                                        <h2 class="sidebar-heading"> Responsive Sidebar With Treeview</h2>
                                        <p class="paragraph-content"> This is a graphical aid for visualising and categorising the site, in the style of an expandable and collapsable treeview component. It auto-expands to display the node(s), if any, corresponding to the currently viewed title, highlighting that node(s) and its ancestors. Load-on-demand when expanding nodes is available where supported (most graphical browsers), falling back to a full-page reload. MediaWiki-supported caching, aside from squid, has been considered so that unnecessary re-downloads of content are avoided where possible. The complete expanded/collapsed state of the treeview persists across page views in most situations</p>
                                        <p class="paragraph-content">
                                            Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do
                                            eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis
                                            nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure
                                            dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
                                            Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim
                                            id est laborum.
                                        </p>
                                        <div class="line"></div>
                                        <h2 class="sidebar-heading">Lorem Ipsum Dolor</h2>
                                        <p class="paragraph-content">
                                            Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do
                                            eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis
                                            nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure
                                            dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
                                        </p>
                                        <div class="line"></div>
                                        <h2 class="sidebar-heading"> Lorem Ipsum Dolor</h2>
                                        <p class="paragraph-content">
                                            Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod
                                            tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud
                                            exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in
                                            reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint
                                            occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                                        </p>
                                        <div class="line"></div>
                                        <h2 class="sidebar-heading"> Lorem Ipsum Dolor</h2>
                                        <p class="paragraph-content">
                                            Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod
                                            tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud
                                            exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in
                                            reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint
                                            occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                                        </p>
                                    </div>
                                </div>
                                <!-- end of main-content -->
                            </div>


                        </div>
                        <style>
    /* header-section styles */
    #header-section.main-header {
        border-bottom: 1px solid #d2d6de;
        height: 55px;
        min-height: 55px;
        max-height: 55px;
        background: #1c86c8;
        color: #fff;
    }

    #header-section .header-style {
        line-height: 40px;
        height: 55px;
        padding: 8px;
        list-style: none;
        cursor: pointer;
        text-align: center;
        font-size: 18px;
    }

    #header-section .border-left {
        border-left: 1px solid #d2d6de;
        width: 10em;
    }

    #header-section .float-left {
        float: left;
		padding-right: 0px;
    }

    #header-section .icon-menu {
        width: 40px;
    }

     .sb-content-tab #wrapper {
        display: none;
    }
    /* custom code start */
    .center {
        text-align: center;
        display: none;
        font-size: 13px;
        font-weight: 400;
        margin-top: 20px;
    }

    .sb-content-tab .center {
        display: block;
    }
    /* custom code end */
    #header-section .float-right,
    #sidebar-treeview .e-treeview .e-icon-collapsible,
    #sidebar-treeview .e-treeview .e-icon-expandable {
        float: right;
    }

    #header-section .header-list,
    #sidebar-treeview .e-treeview,
    #sidebar-treeview .e-treeview .e-ul {
        padding: 0px;
        margin: 0px;
    }
    /* custom code start */
  
    /* custom code end */
    /*end of header-section styles */

    /*main-menu-header  styles */
    #sidebar-treeview .main-menu .main-menu-header {
        color: #656a70;
    padding: 15px;
    font-size: 14px;
    width: 13em;
    margin: 0;
    }

    /*end of main-menu-header styles */

    /*text input styles */
    #sidebar-treeview .main-menu .search-icon {
        text-indent: 10px;
        height: 30px;
        width: 19em;
    }

    /*end of text input styles */

    /* table of content area styles */
    #sidebar-treeview .table-content {
        padding: 20px;
    height: 8em;
    }

    /* end of table ofcontent area styles */

    /* content area styles */
    #main-text.main-content {
        overflow: hidden;
    }

    #main-text .sidebar-content .line {
        width: 100%;
        height: 1px;
        border-bottom: 1px dashed #ddd;
        margin: 40px 0;
    }

    #main-text .sidebar-content {
        padding: 15px;
    }

    #main-text .sidebar-heading {
        color: #1c86c8;
        margin: 40px 0;
        padding: 2px;
    }

    #main-text .paragraph-content {
    font-family: 'Poppins', sans-serif;
    padding: 5px;
    font-weight: 300;
    color: grey;
    }


    /* end of content area styles */
    /* custom code start */
    /* body and html styles */
    body {
        margin: 0;
        font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
        -webkit-tap-highlight-color: transparent;
    }
    /* custom code end */
    /* end of body and html styles */

    /* newTab support styles */

    .ej2-new .sb-header,
    .ej2-new .sb-bread-crumb,
    .ej2-new #action-description,
    .ej2-new #description-section,
    .ej2-new #description {
        display: none
    }

    .ej2-new .container-fluid,
    .ej2-new .container-fluid .control-section,
    #sidebar-section {
        padding: 0px;
    }

    .ej2-new .sample-browser>.content.e-view {
        top: 0px;
        padding: 0px;
		height: 100%;
        text-align: initial
    }

    /* end of newTab support styles */

    /* icon styles */
  
    #sidebar-treeview #main-treeview .icon {
        font-family: 'fontello';
        font-size: 16px;
    }

    #header-section.main-header .icon-menu::before {
        content: '\e801';
        font-family: 'fontello';
        font-size: 27px;
    }

    #sidebar-treeview #main-treeview .icon-microchip::before {
        content: '\e806';
    }

    #sidebar-treeview #main-treeview .icon-thumbs-up-alt::before {
        content: '\e805';
    }

    #sidebar-treeview #main-treeview .icon-docs::before {
        content: '\e802';
    }

    #sidebar-treeview #main-treeview .icon-th::before {
        content: '\e803';
    }

    #sidebar-treeview #main-treeview .icon-code::before {
        content: '\e804';
    }

    #sidebar-treeview #main-treeview .icon-chrome::before {
        content: '\e807';
    }

    #sidebar-treeview #main-treeview .icon-up-hand::before {
        content: '\e810';
    }

    #sidebar-treeview #main-treeview .icon-bookmark-empty::before {
        content: '\e811';
    }

    #sidebar-treeview #main-treeview .icon-help-circled::before {
        content: '\e813';
    }

    #sidebar-treeview #main-treeview .icon-doc-text::before {
        content: '\e812';
    }

    #sidebar-treeview #main-treeview .icon-circle-thin::before {
        content: '\e808';
    }

    /* end of icon styles */
                        </style>
                    </div>
                </div>
                <div id="description-section">Description</div>
                <div id="description">
                    <p>

                        This sample demonstrates how to use the TreeView component inside the Sidebar for navigation purposes. The Sidebar expands when the hamburger icon at the top-left corner of the header section is clicked, and TreeView expands and collapses when the TreeView expand/collapse icon is clicked.

                    </p>
                </div>
            </div>
        </div>

    </div>
    </div>

 
    <script src="~/Scripts/SideBar/index.js"></script>
</body>

</html>
