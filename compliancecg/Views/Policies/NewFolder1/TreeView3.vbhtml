﻿<!DOCTYPE html>
<html lang="en">
<head>
    <script>
    (function (w, d, s, l, i) {
        w[l] = w[l] || []; w[l].push({
            'gtm.start':
                new Date().getTime(), event: 'gtm.js'
        }); var f = d.getElementsByTagName(s)[0],
            j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
                'https://www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
        })(window, document, 'script', 'dataLayer', 'GTM-WLQL39J');</script>
    <title>TreeView &#xB7; Local Data &#xB7; Essential JS 2 for Javascript (ES5) &#xB7; Syncfusion</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="description" content="This demo demonstrates the binding of local data to the JavaScript tree view. The local data structure can be hierarchical data or list data.">
    <meta name="author" content="Syncfusion">
    <link rel="shortcut icon" href="https://ej2.syncfusion.com/home/favicon.ico">
    <script type="text/javascript">
    var themeName = location.hash || 'material';
        themeName = themeName.replace('#', '');
        window.ripple = (themeName === "material")
        document.write('<link href="../../styles/' + themeName + '.css" rel="stylesheet">');</script>
    <script type="text/javascript">
    if (/MSIE \d|Trident.*rv:/.test(navigator.userAgent)) {
            document.write("<script src=\'https://cdnjs.cloudflare.com/ajax/libs/bluebird/3.3.5/bluebird.min.js\'>");
        }</script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet">
    <link rel="canonical" href="https://ej2.syncfusion.com/demos/treeview/local-data">

    <link href="~/Syncfusion/TreeView/local-data/index.css" rel="stylesheet" />
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
<body class="ej2-new">

    <noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-WLQL39J" height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
    <div hidden id="sync-analytics" data-queue="EJ2 - Javascript - Demos"></div>

    <div class="sample-browser">
        <div id="sample-header" class="sb-header" role="banner">
            <div class="sb-header-left sb-left sb-table">
                <div class="sb-header-item sb-table-cell">
                    <div class="header-logo">
                        <a href="#" target rel="noopener noreferrer">
                        </a><a href="https://ej2.syncfusion.com/home" target rel="noopener noreferrer">
                            <div class="footer-logo"> </div>
                        </a>
                    </div>

                </div>
                <div class="sb-header-splitter sb-download-splitter"></div>
                <div class="sb-header-item sb-table-cell">
                    <div id="sb-header-text" class="e-sb-header-text">

                        <span class="sb-header-text-left">Essential JS 2 for Javascript</span>
                    </div>
                </div>
            </div>
            <div class="sb-header-right sb-right sb-table">
                <div class="sb-header-item sb-table-cell">
                    <div class="product-style">
                        <div><a href="https://www.syncfusion.com/javascript-ui-controls/treeview">PRODUCT DETAILS</a></div>
                    </div>
                </div>
                <div class="sb-header-item sb-table-cell">
                    <div class="sb-header-item sb-table-cell sb-download-wrapper">
                        <a href="https://www.syncfusion.com/downloads/essential-js2" target="_blank">
                            <button id="download-now" class="sb-download-btn">
                                <span class="sb-download-text">Download Now</span>
                            </button>
                        </a>
                    </div>
                </div>
            </div>

        </div>
        <div class="content e-view">
            <div class="sample-content">
                <div id="sample-bread-crumb" class="sb-bread-crumb">
                    <div class="sb-custom-item sb-sample-navigation sb-right sb-header-right sb-table-cell">
                        <div id="prev-sample" class="sb-navigation-prev e-control e-tooltip " aria-label="previous sample" title="Previous Sample">
                            <a href="https://ej2.syncfusion.com/demos/treeview/default/" class=""><span class="sb-icons sb-icon-Previous"></span></a>

                        </div>
                        <div id="next-sample" class="sb-navigation-next e-control e-tooltip" aria-label="next sample" title="Next Sample">
                            <a href="https://ej2.syncfusion.com/demos/treeview/remote-data/" class=""><span class="sb-icons sb-icon-Next"></span></a>

                        </div>
                    </div>
                    <h1 class="sb-bread-crumb-text">
                        <div class="category-allcontrols"><a href="https://ej2.syncfusion.com/demos"><span>All Controls</span></a></div>
                        <div class="category-seperator sb-icons"> / </div>
                        <div class="category-text bread-ctext"><a href="https://ej2.syncfusion.com/demos/#/material/treeview/default.html"><span>TreeView</span></a></div>
                        <div class="category-seperator sb-icons"> / </div>
                        <div class="crumb-sample">Local Data</div>
                    </h1>
                </div>
                <div class="control-content">
                    <div id="action-description">
                        <p>
                        </p><div class="layout" id="actionDes">
                            <p>This sample demonstrates the binding of local data to the TreeView. Click on node to select it, and click on icon or double click on node to expand/collapse it.</p>
                        </div>
                        <a id="details" class="layout" onclick="desDetails()"> More Details...</a>
                        <script>
                            function desDetails() {
                                var element = document.getElementById('description-section');
                                if (element) {
                                    element.scrollIntoView();
                                }
                            }</script>
                        <p></p>
                    </div>
                    <div class="container-fluid">
                        <div class="control-section">
                            <div class="col-lg-12 control-section">
                                <div class="col-lg-6 nested-data">
                                    <div class="content">
                                        <h4>Hierarchical Data</h4>
                                        <div id="tree"></div>
                                    </div>
                                </div>
                                <div class="col-lg-6 list-data">
                                    <div class="content">
                                        <h4>List Data</h4>
                                        <div id="listtree"></div>
                                    </div>
                                </div>
                            </div>
                            <!-- custom code start -->
                            <style>

                                .nested-data, .list-data {
                                    padding: 15px;
                                    margin-bottom: 25px;
                                }

                                .content {
                                    margin: 0 auto;
                                    border: 1px solid #dddddd;
                                    border-radius: 3px;
                                }

                                    .content h4 {
                                        padding: 0 10px;
                                    }
                            </style>
                            <!-- custom code end -->



                        </div>
                    </div>
                    <div id="description-section">Description</div>
                    <div id="description">
                        <p>
                            The TreeView component loads the data through the dataSource property, where the data can be either local data or remote data. In case of local data, the data structure can be hierarchical data or list data (with self-referential format i.e., mapped with the id and parentID fields).
                            In this demo, the first TreeView is bound with the hierarchical data that contains array of nested objects. And the second TreeView is bound with the list type data where the parent-child relation is referred by the id and parentID mapping fields.
                            For more information, you can refer to the Data Binding section from the documentation.
                        </p>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <script src="~/Syncfusion/TreeView/local-data/index.min.js"></script>
    <script src="~/Syncfusion/TreeView/navigations.min.js"></script>





</body>
</html>