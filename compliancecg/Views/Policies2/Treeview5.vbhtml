@Code
    ViewData("Title") = "Treeview5"
End Code

<h2>Treeview5</h2>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Essential Studio for JavaScript : TreeView - Keyboard Navigation</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" charset="utf-8" />
    <link href="//cdn.syncfusion.com/14.2.0.26/js/web/flat-azure/ej.web.all.min.css" rel="stylesheet" />

    <script src="//cdn.syncfusion.com/js/assets/external/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="//cdn.syncfusion.com/js/assets/external/jquery.easing.1.3.min.js" type="text/javascript"></script>
    <script src="//cdn.syncfusion.com/14.2.0.26/js/web/ej.web.all.min.js" type="text/javascript"></script>
</head>
<body>
    <div style="width: 190px; height: 250px;border: 1px solid blue;">
        <ul id="tree1" tabindex="0">
            <li class="expanded">
                Cricket
                <ul>
                    <li>Princeton Club</li>
                    <li>Harvard Club</li>
                    <li>St.Columba's Club</li>
                    <li>Dartmouth Club</li>
                    <li>Middlebury cricket Club</li>
                </ul>
            </li>
            <li class="expanded">
                Football
                <ul>
                    <li>A.F.C. Blackpool</li>
                    <li>A.F.C. Emley</li>
                    <li>Bedford</li>
                    <li>Celtic Nation</li>
                    <li>Farsley</li>
                    <li>Gresley</li>
                    <li>London Colney</li>
                </ul>
            </li>
            <li>
                Basketball
                <ul>
                    <li>AGE Halkida </li>
                    <li>Iraklio </li>
                    <li>Sporting Athens</li>
                    <li>Pagrati Athens</li>
                </ul>
            </li>
            <li>
                Volleyball
                <ul>
                    <li>CV Elche</li>
                    <li>Jusan Canarias</li>
                    <li>Unicaja Arukasur</li>
                    <li>Voleibol Benidorm</li>
                </ul>
            </li>
            <li>
                Tennis
                <ul>
                    <li>Ryoma Echizen</li>
                    <li>Kaoru Kaidoh</li>
                    <li>Rokkaku</li>
                    <li>Higa</li>
                    <li>Yamabuki</li>
                </ul>
            </li>
        </ul>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#tree1").ejTreeView({
                showCheckbox: true,
                allowEditing: true,
                width: "100%",
                height: "100%",              
            });
            //Control focus key
            
        });
    </script>
</body>
</html>
