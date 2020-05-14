@Code
    ViewData("Title") = "About"
End Code



    <div id="stage">
        <a href="1.jpg" title="Picture1"><img src="~/Content/Images/slide1-1024x683-1024x683.jpg" width="1024" height="683" ></a>
        <a href="2.jpg" title="Picture2"><img src="~/Content/Images/slide2-1024x683-1024x683.jpg" width="1024" height="683" ></a>
        <a href="3.jpg" title="Picture3"><img src="~/Content/Images/slide3-1024x683-1024x683.jpg" width="1024" height="683" ></a>

        @*<a href="1.jpg" title="Picture1"><img src="~/Content/Images/slide1-1024x683-1024x683.jpg"  width="360" height="270" title="Picture1"></a>
        <a href="2.jpg" title="Picture2"><img src="~/Content/Images/slide2-1024x683-1024x683.jpg"  width="360" height="270" title="Picture2"></a>
        <a href="3.jpg" title="Picture3"><img src="~/Content/Images/slide3-1024x683-1024x683.jpg"  width="360" height="270" title="Picture3"></a>*@
    </div>




<style type="text/css">

  #stage {
    margin: 1em auto;
    width: 382px;
    height: 292px;
  }

  #stage a {
    position: absolute;
  }
  #stage a img {
    padding: 10px;
    border: 1px solid #ccc;
    background: #fff;
  }

  #stage a:nth-of-type(1) {
    animation-name: fader;
    animation-delay: 4s;
    animation-duration: 1s;
    z-index: 20;
  }
  #stage a:nth-of-type(2) {
    z-index: 10;
  }
  #stage a:nth-of-type(n+3) {
    display: none;
  }

  #stage a::after {
    position: absolute;
    left: 11px;
    bottom: 11px;
    padding: 2px 0;
    width: calc(100% - 22px);
    background: rgba(0,0,0,0.5);
    text-align: center;
    content: attr(title);
    font-size: 1.1em;
    color: #fff;
  }

@@keyframes fader {
    from { opacity: 1.0; }
    to   { opacity: 0.0; }
  }
</style>

<script type="text/javascript">

    // Original JavaScript code by Chirp Internet: www.chirp.com.au
    // Please acknowledge use of this code by including this header.

    window.addEventListener("DOMContentLoaded", function (e) {

        var stage = document.getElementById("stage");
        var fadeComplete = function (e) { stage.appendChild(arr[0]); };
        var arr = stage.getElementsByTagName("a");
        for (var i = 0; i < arr.length; i++) {
            arr[i].addEventListener("animationend", fadeComplete, false);
        }

    }, false);

</script>


@*<style type="text/css">

  #stage {
    margin: 1em auto;
    width: 360px;
    height: 270px;
    border: 10px solid #000;
    overflow: hidden;
  }
  #stage a {
    position: relative;
    display: inline-block;
  }
  #stage a::after {
    position: absolute;
    top: 50%;
    left: 0;
    transform: translateY(-50%);
    width: 100%;
    text-align: center;
    content: attr(title);
    font-weight: bold;
    font-size: 2em;
    color: #fff;
    text-shadow: -1px -1px 0 #333, 1px -1px 0 #333, -1px 1px 0 #333, 2px 2px 0 #333;
  }

  #stage a:nth-of-type(2) {
    left: 360px;
    top: -50%;
    animation-name: slider;
    animation-delay: 4s;
    animation-duration: 1s;
    animation-timing-function: cubic-bezier(0,1.5,0.5,1);
  }
  #stage a:nth-of-type(n+3) {
    display: none;
  }

  @@keyframes slider {
    from { transform: translateY(-50%) rotate(30deg); left: 360px; }
    to   { transform: translateY(-50%); left: 0px; }
  }

</style>


<script type="text/javascript">

    // Original JavaScript code by Chirp Internet: www.chirp.com.au
    // Please acknowledge use of this code by including this header.

    window.addEventListener("DOMContentLoaded", function (e) {

        var stage = document.getElementById("stage");
        var slideComplete = function (e) { stage.appendChild(arr[0]); };
        var arr = stage.getElementsByTagName("a");
        for (var i = 0; i < arr.length; i++) {
            arr[i].addEventListener("animationend", slideComplete, false);
        }

    }, false);

</script>*@


@*<script type="text/javascript">

    // Original JavaScript code by Chirp Internet: www.chirp.com.au
    // Please acknowledge use of this code by including this header.

    window.addEventListener("DOMContentLoaded", function (e) {

        var maxW = 0;
        var maxH = 0;

        var stage = document.getElementById("stage");
        var fadeComplete = function (e) { stage.appendChild(arr[0]); };
        var arr = stage.getElementsByTagName("img");
        for (var i = 0; i < arr.length; i++) {
            if (arr[i].width > maxW) maxW = arr[i].width;
            if (arr[i].height > maxH) maxH = arr[i].height;
        }
        for (var i = 0; i < arr.length; i++) {
            if (arr[i].width < maxW) {
                arr[i].style.paddingLeft = 10 + (maxW - arr[i].width) / 2 + "px";
                arr[i].style.paddingRight = 10 + (maxW - arr[i].width) / 2 + "px";
            }
            if (arr[i].height < maxH) {
                arr[i].style.paddingTop = 10 + (maxH - arr[i].height) / 2 + "px";
                arr[i].style.paddingBottom = 10 + (maxH - arr[i].height) / 2 + "px";
            }
            arr[i].addEventListener("animationend", fadeComplete, false);
        }

    }, false);

</script>*@