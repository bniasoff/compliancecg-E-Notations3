
    <div class="row" id="body-row">
        <div id="sidebar-container" class="">
            <ul class="list-group">

                <a href="#submenu2" data-toggle="collapse" aria-expanded="false" class="bg-dark list-group-item list-group-item-action flex-column align-items-start">
                    <div class="d-flex w-100 justify-content-start align-items-center">
                        <span class="fa fa-user fa-fw mr-3"></span>
                        <span class="menu-collapsed">Importing</span>
                        <span class="submenu-icon ml-auto"></span>
                    </div>
                </a>

                <div id='submenu2' class="collapse sidebar-submenu">
                    <a href="@Url.Action("CustomerMain", "Import")" class="list-group-item list-group-item-action bg-dark text-white">
                        <span class="menu-collapsed">Sync Customers</span>
                    </a>
                    <a href="@Url.Action("Invoice", "Import")" class="list-group-item list-group-item-action bg-dark text-white">
                        <span class="menu-collapsed">Sync Orders</span>
                    </a>

                    <a href="#" class="list-group-item list-group-item-action bg-dark text-white">
                        <span class="menu-collapsed">Sync Products</span>
                    </a>
                    <a href="#" class="list-group-item list-group-item-action bg-dark text-white">
                        <span class="menu-collapsed">Sync Vendors</span>
                    </a>
                    <a href="#" class="list-group-item list-group-item-action bg-dark text-white">
                        <span class="menu-collapsed">Sync Classes</span>
                    </a>
                </div>

                <a href="#submenu1" data-toggle="collapse" aria-expanded="false" class="bg-dark list-group-item list-group-item-action flex-column align-items-start">
                    <div class="d-flex w-100 justify-content-start align-items-center">
                        <span class="fa fa-dashboard fa-fw mr-3"></span>
                        <span class="menu-collapsed">Settings</span>
                        <span class="submenu-icon ml-auto"></span>
                    </div>
                </a>

                <div id='submenu1' class="collapse sidebar-submenu">
                    <a href="@Url.Action("QBAccounts", "QBAccounts")" class="list-group-item list-group-item-action bg-dark text-white">
                        <span class="menu-collapsed">QB Accounts</span>
                    </a>
                    <a href="@Url.Action("TreeView5", "QBAccounts")" class="list-group-item list-group-item-action bg-dark text-white">
                        <span class="menu-collapsed">QB Account Companies</span>
                    </a>
                    <a href="@Url.Action("AccountMappingsMain", "Settings")" class="list-group-item list-group-item-action bg-dark text-white">
                        <span class="menu-collapsed">Account Mappings</span>
                    </a>
                    <a href="@Url.Action("CustomerMappings", "Settings")" class="list-group-item list-group-item-action bg-dark text-white">
                        <span class="menu-collapsed">Customer Mappings</span>
                    </a>
                    <a href="@Url.Action("OrderMappings", "Settings")" class="list-group-item list-group-item-action bg-dark text-white">
                        <span class="menu-collapsed">Order Setting</span>
                    </a>

                </div>


                <li class="list-group-item sidebar-separator menu-collapsed"></li>

                <a href="#" class="bg-dark list-group-item list-group-item-action">
                    <div class="d-flex w-100 justify-content-start align-items-center">
                        <span class="fa fa-question fa-fw mr-3"></span>
                        <span class="menu-collapsed">Help</span>
                    </div>
                </a>
                <a href="#" data-toggle="sidebar-colapse" class="bg-dark list-group-item list-group-item-action d-flex align-items-center">
                    <div class="d-flex w-100 justify-content-start align-items-center">
                        <span id="collapse-icon" class="fa fa-2x mr-3"></span>
                        <span id="collapse-text" class="menu-collapsed">Collapse</span>
                    </div>
                </a>
            </ul>
        </div>

      