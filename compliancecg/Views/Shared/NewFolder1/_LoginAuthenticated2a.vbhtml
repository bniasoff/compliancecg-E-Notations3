@Imports Syncfusion.EJ2
@Imports Syncfusion.EJ2.Navigations
@Imports compliancecg.controllers

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



@Code





    Dim parentitem As List(Of Parentitem) = New List(Of Parentitem)()
    Dim childitem As List(Of childItems) = New List(Of childItems)()
    Dim localFields As TreeViewFieldsSettings = New TreeViewFieldsSettings()

    parentitem.Add(New Parentitem With {.nodeId = "01", .nodeText = "Facilities", .iconCss = "icon-docs icon"})
    If UserName IsNot Nothing Then
        If UserName = "info@compliancecg.com" Then
            parentitem.Add(New Parentitem With {.nodeId = "04", .nodeText = "Search", .iconCss = "icon-docs icon"})
            parentitem.Add(New Parentitem With {.nodeId = "02", .nodeText = "Forms", .iconCss = "icon-docs icon"})
        End If
    End If




    parentitem.Add(New Parentitem With {.nodeId = "03", .nodeText = "Policies", .iconCss = "icon-docs icon"})
    'parentitem.Add(New Parentitem With {.nodeId = "03", .nodeText = "Quick Start", .iconCss = "icon-docs icon"})

    'Dim childitem4 As List(Of childItems) = New List(Of childItems)()
    'parentitem.Add(New Parentitem With {.nodeId = "04", .nodeText = "Components", .iconCss = "icon-th icon", .child = childitem4})
    'childitem4.Add(New childItems With {.nodeId = "04-01", .nodeText = "Calendar", .iconCss = "icon-circle-thin icon"})
    'childitem4.Add(New childItems With {.nodeId = "04-02", .nodeText = "DatePicker", .iconCss = "icon-circle-thin icon"})
    'childitem4.Add(New childItems With {.nodeId = "04-03", .nodeText = "DateTimePicker", .iconCss = "icon-circle-thin icon"})
    'childitem4.Add(New childItems With {.nodeId = "04-04", .nodeText = "DateRangePicker", .iconCss = "icon-circle-thin icon"})
    'childitem4.Add(New childItems With {.nodeId = "04-05", .nodeText = "TimePicker", .iconCss = "icon-circle-thin icon"})
    'childitem4.Add(New childItems With {.nodeId = "04-06", .nodeText = "SideBar", .iconCss = "icon-circle-thin icon"})

    'Dim childitem5 As List(Of childItems) = New List(Of childItems)()
    'parentitem.Add(New Parentitem With {.nodeId = "05", .nodeText = "API Reference", .iconCss = "icon-code icon", .child = childitem4})
    'childitem5.Add(New childItems With {.nodeId = "05-01", .nodeText = "Calendar", .iconCss = "icon-circle-thin icon"})
    'childitem5.Add(New childItems With {.nodeId = "05-02", .nodeText = "DatePicker", .iconCss = "icon-circle-thin icon"})
    'childitem5.Add(New childItems With {.nodeId = "05-03", .nodeText = "DateTimePicker", .iconCss = "icon-circle-thin icon"})
    'childitem5.Add(New childItems With {.nodeId = "05-04", .nodeText = "DateRangePicker", .iconCss = "icon-circle-thin icon"})
    'childitem5.Add(New childItems With {.nodeId = "05-05", .nodeText = "TimePicker", .iconCss = "icon-circle-thin icon"})
    'childitem5.Add(New childItems With {.nodeId = "05-06", .nodeText = "SideBar", .iconCss = "icon-circle-thin icon"})

    'parentitem.Add(New Parentitem With {.nodeId = "06", .nodeText = "Browser Compatibility", .iconCss = "icon-chrome icon"})
    'parentitem.Add(New Parentitem With {.nodeId = "07", .nodeText = "Upgrade Packages", .iconCss = "icon-up-hand icon"})
    'parentitem.Add(New Parentitem With {.nodeId = "08", .nodeText = "Release Notes", .iconCss = "icon-bookmark-empty icon"})
    'parentitem.Add(New Parentitem With {.nodeId = "09", .nodeText = "FAQ", .iconCss = "icon-help-circled icon"})
    'parentitem.Add(New Parentitem With {.nodeId = "10", .nodeText = "License", .iconCss = "icon-doc-text icon"})

    localFields.DataSource = parentitem
    localFields.Id = "nodeId"
    localFields.Child = "child"
    localFields.Text = "nodeText"
    localFields.IconCss = "iconCss"
    ViewBag.fields = localFields
End Code


<a href="javascript:SetActive('facilities')" data-hover="Search">Facilities <span data-hover="facilities"></span></a>



<div Class="main-header" id="header-section">
    <ul Class="header-list"></ul>
</div>



<script>
    function SetActive(arg) {
        if (arg == 'facilities') {
            var ajax = new ej.base.Ajax("/Facilities/Main", 'POST', true);
            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });
        };
    }
</script>




<div Class="main-header" id="header-section">
    <ul Class="header-list"></ul>
</div>
<!-- sidebar element declaration-->
@Html.EJS().Sidebar("sidebar-treeview").Width("290px").Target(".main-content").ContentTemplate(@@<div>
    <!-- normal state element declaration -->
    <div class="main-menu">
        <div class="table-content">
            <input type="text" placeholder="Search..." class="search-icon">
            <p class="main-menu-header">TABLE OF CONTENTS</p>
        </div>
        <div>
            <!-- Treeview element declaration-->
            {@Html.EJS().TreeView("main-treeview").Fields(ViewBag.fields).ExpandOn(Syncfusion.EJ2.Navigations.ExpandOnSettings.Click).NodeSelected("onNodeSelected").NodeClicked("onNodeClicked").NodeExpanding("onNodeExpanded").Render()}
        </div>
    </div>
    <!-- end of normal state element declaration -->
</div>).Render()




<script type="text/javascript">
    function onNodeCollapsed(args) {
        debugger;

    }

    function onNodeExpanded(args) {
        debugger;

    }

    function onNodeSelected(args) {


        //if (args.nodeData.text !== 'Facilities') {
        //          $("#PartialView").html("");
        //}

        if (args.nodeData.text == 'Facilities') {
            var ajax = new ej.base.Ajax("/Facilities/Main", 'POST', true);
            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });
        };


        if (args.nodeData.text == 'Search') {
            var ajax = new ej.base.Ajax("/Facilities/Search", 'POST', true);
            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });
        };

        if (args.nodeData.text == 'Forms') {
            var ajax = new ej.base.Ajax("/Forms/Index", 'POST', true);
            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });
        };



        if (args.nodeData.text == 'Users') {
            var ajax = new ej.base.Ajax("/FacilityUsers/Main", 'POST', true);
            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });
        };

@*var data2
    if (args.nodeData.text == 'Policies') {

      //  $('#PartialView').load('@Url.Action("index", "devexpress")');

        //var jqxhr = $.post("/DevExpress/index", function (data) {
        //          $("#PartialView").html(data2);
        //})
        //    .done(function () {


        //    })
        //    .fail(function () {
        //      //  alert("error");
        //    })
        //    .always(function () {
        //      //  alert("finished");
        //    });




        //var ajax = new ej.base.Ajax("/FacilityUsers/Main", 'POST', true);
        //ajax.send().then((data) => {
        //    $("#PartialView").html(data);
        //});
    };*@




          if (args.nodeData.text == 'Policies') {
            var ajax = new ej.base.Ajax("/Policies/TreeView", 'POST', true);
            ajax.send().then((data) => {
                $("#PartialView").html(data);
            });
        };

       // if (args.nodeData.text == 'Policies') {

//            $.post( "/DevExpress/index", function( data ) {
// $("#PartialView").html( data );
//});

            //var ajax = new ej.base.Ajax("/DevExpress/index", 'POST', true);
            //ajax.send().then((data) => {


@*@Using (Html.BeginForm())
        @Html.Action("Index", "DevExpress")
    End Using*@

               // $("#PartialView").html(data);
        //    });
        //};



@*{ window.location.href = '@Url.Action("Tab", "Facilities")/'; }*@
         //{  $.post('/Facilities/Tab');}

    }

    function onNodeClicked(args) {


    }
</script>
@*<script>
        document.addEventListener('DOMContentLoaded', function () {

            sidebarInstance = document.getElementById("sidebar-treeview").ej2_instances[0];
            // Expand the Sidebar
            document.getElementById('hamburger').addEventListener('click', function () {

                sidebarInstance.toggle();
            });
        });
    </script>*@

<style>
   
     body {
        padding: 0px;
    }


</style>


