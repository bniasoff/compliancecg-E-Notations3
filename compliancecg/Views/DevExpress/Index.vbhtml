

@Html.DevExpress().GetStyleSheets(New StyleSheet With {.ExtensionSuite = ExtensionSuite.RichEdit})
@Html.DevExpress().GetScripts(New Script With {.ExtensionSuite = ExtensionSuite.RichEdit})

<script type="text/javascript">
    $(document).ready(function () {

    });

    function loadEfile() { 
        $.ajax({
            type: "POST",
            cache: false,
            async: true,
            datatype: 'json',
            contentType: 'application/json',
            url: '../DevExpress/LoadDocument',
            success: function (response) {
                $('#document').html(response);
                return false;
            },
            error: function (xhr, status, message) {

            }

        });
        return false;
    }
</script>
<a href="#" onclick="loadEfile()">load file</a>
<a href="javascript:void()" onclick="loadEfile()">load file</a>

<form>
    <div id="document">
    </div>
</form>