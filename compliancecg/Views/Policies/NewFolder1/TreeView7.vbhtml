
@Imports Syncfusion.EJ2
@Imports Syncfusion.EJ2.Navigations


    <html>
    <head>
        <script src="https://ej2.syncfusion.com/javascript/demos/treeview/default/datasource.js" type="text/javascript"></script>
        <script src="https://cdn.syncfusion.com/ej2/dist/ej2.min.js" type="text/javascript"></script>
        <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet">
        <link href="https://cdn.syncfusion.com/ej2/material.css" rel="stylesheet">
        <style>
            body {
                touch-action: none;
            }
        </style>
    </head>
    <body>
        <div class="stackblitz-container material">
            <div class="col-lg-12 control-section">
                <div class="control_wrapper">
                    <!-- Initialize TreeView -->
                    <div id="tree"></div>
                </div>
                <button id="normalbtn">Change TreeView datasource</button>
            </div>
            <style>
                .control_wrapper {
                    max-width: 500px;
                    margin: auto;
                    border: 1px solid #dddddd;
                    border-radius: 3px;
                }
            </style>



        </div>
    </body>
    </html>


<script type="text/javascript">
ej.base.enableRipple(true);

// Render the TreeView by mapping its fields property with data source properties
var hierarchicalData = [
  {
    id: '01', name: 'Local Disk (C:)', expanded: true,
    subChild: [
      {
        id: '01-01', name: 'Program Files',
        subChild: [
          { id: '01-01-01', name: 'Windows NT' },
          { id: '01-01-02', name: 'Windows Mail' },
          { id: '01-01-03', name: 'Windows Photo Viewer' },
        ]
      }
    ]
  }];
var treeObj = new ej.navigations.TreeView({
  fields: { dataSource: hierarchicalData, id: 'id', text: 'name', child: 'subChild' }
});
treeObj.appendTo('#tree');

//Render button object
var buttonObj = new ej.buttons.Button();
buttonObj.appendTo('#normalbtn');

//Function for modifying TreeView content
function changeDataSource() {
  // Hierarchical data source for TreeView component
  var treeInstance = ej.base.getComponent(document.querySelector('#tree'), 'treeview');
  var treeData = [
    {
      id: '1', name: 'Local Disk (E:)', expanded: true,
      subChild: [
        {
          id: '03-01', name: 'Pictures',
          subChild: [
            { id: '03-01-01', name: 'Wind.jpg' },
            { id: '03-01-02', name: 'Stone.jpg' },
            { id: '03-01-03', name: 'Home.jpg' },
          ]
        }]
    }
  ];
  treeInstance.fields = { dataSource: treeData, id: 'id', text: 'name', child: 'subChild' };
}

var btn = document.getElementById("normalbtn");

//Binding click event to button for changing TreeView datasource
btn.addEventListener("click", (e) => changeDataSource());



</script>


@Html.EJS().ScriptManager()
