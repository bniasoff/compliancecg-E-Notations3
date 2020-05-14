
   
    <header class="row  justify-content-between align-items-top py-2">
        @*flex-nowrap*@
        <div class="col-md-6 col-sm-12 ">
            <a href="#"> <img id="brandImage" src="~/Content/Images/Logos/complianceLogo.jpg" /></a>
        </div>
        <div class="col-md-6 col-sm-12 d-flex justify-content-end ">
            <a class="pr-2 text-dark " target="_blank" href="https://www.compliancecg.com/">About Us</a>&#124;
            <a class="pl-2 text-dark " target="_blank" href="https://www.compliancecg.com/#contact">Contact Us</a>
        </div>
    </header>
    

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

        }
    </script>


