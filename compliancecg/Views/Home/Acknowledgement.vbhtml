
<div class=" d-flex flex-column loginContainer row">
    <div class="  align-self-center mainRow ackRow  text-center">
        @*<div style="float:left;padding-top:0px;margin-right:100px"> @Html.Partial("../Home/Acknowledgement/Client Portal User Agreement")</div>*@
        @*<div style="float:left;padding-top:10px;"> @Html.Partial("../Home/Acknowledgement/Provisions - CCG Retainer")</div>*@


        @*<div style="border: 1px solid black;padding:5px;margin-bottom:5px">
            @Html.Partial("../Home/Acknowledgement/Client Portal User Agreement")
        </div>
        <div style="border: 1px solid black;padding:5px">
            @Html.Partial("../Home/Acknowledgement/Provisions - CCG Retainer")
        </div>*@
        <h4 class="font-weight-bold">Compliance Consulting Group, LLC</h4>
        <div>Client Portal User Agreement</div>
        <div class="pb-3">Terms of Use</div>
        <div class="ackText">
            <div >
                @Html.Partial("../Home/Acknowledgement/Client Portal User Agreement")
            </div>
            <div>
                @Html.Partial("../Home/Acknowledgement/Provisions - CCG Retainer")
            </div>
            @* This Client Portal User Agreement (the "Agreement") applies to CCG’s clients’ employees’ and other authorized users’ ("You" or "Your") use of and access to Compliance Consulting Group’s ("CCG") policies, trainings, information, and other materials (the "Materials") available on CCG’s online client portal (the "Portal"). By clicking the box indicating your acceptance of this Agreement, you acknowledge that you have read, understood, and agree to be bound by the terms and conditions of this Agreement.*@
        </div>
        @*@Html.EJS().CheckBox("checked").Change("onChange").Checked(False).Label("I Acknowledge").Render()
        I Acknowledge
        <input type="checkbox" id="myCheck" onclick="onChange2()">*@
        <input type="button" class="btn btn-success px-4" value="I Agree" onclick="onChange2()" />
        <a  style="display:block;" class="pt-2" href="javascript:document.getElementById('logoutForm').submit()">Log out</a>
    </div>

</div>


<script>



    function onChange2() {

      //  var checkBox = document.getElementById("myCheck");

    //    if (checkBox.checked == true) {
        var request = true;// JSON.stringify(checkBox.checked);
            $.ajax({
                type: "POST",
                async: false,
                data: jQuery.param({ Checked: request }),
                url: '/Home/SetAcknowledgementSessionValue',
                success: function (data) {
                    if (data.length = 'True') {
                    };
                }
            });
            window.location.href = 'Index';

    //    } else {

   //     }
    }
</script>



