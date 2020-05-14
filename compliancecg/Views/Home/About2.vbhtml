@Code
    ViewData("Title") = "About"

    ''https://www.the-art-of-web.com/css/infinite-photo-carousel/
End Code

<div id="stage">
    <div id="rotator" style="-webkit-animation-name: rotator;">
        @*<a href="1.jpg" title="Picture1"><img src="~/Content/Images/slide1-1024x683-1024x683.jpg" width="1024" height="683" ></a>*@
            @*<a href="2.jpg" title="Picture2"><img src="~/Content/Images/slide2-1024x683-1024x683.jpg" width="1024" height="683" ></a>
            <a href="3.jpg" title="Picture3"><img src="~/Content/Images/slide3-1024x683-1024x683.jpg" width="1024" height="683" ></a>*@


        @*<a href="1.jpg" title="Picture1"><img src="~/Content/Images/slide1-1024x683-1024x683.jpg" width="1024"></a>
            <a href="2.jpg" title="Picture2"><img src="~/Content/Images/slide2-1024x683-1024x683.jpg" width="1024"></a>
            <a href="3.jpg" title="Picture3"><img src="~/Content/Images/slide3-1024x683-1024x683.jpg" width="1024"></a>
            <a href="4.jpg" title="Picture1"><img src="~/Content/Images/slide1-1024x683-1024x683.jpg" width="1024"></a>
            <a href="5.jpg" title="Picture2"><img src="~/Content/Images/slide2-1024x683-1024x683.jpg" width="1024"></a>
            <a href="6.jpg" title="Picture3"><img src="~/Content/Images/slide3-1024x683-1024x683.jpg" width="1024"></a>
            <a href="7.jpg" title="Picture1"><img src="~/Content/Images/slide1-1024x683-1024x683.jpg" width="1024"></a>
            <a href="8.jpg" title="Picture2"><img src="~/Content/Images/slide2-1024x683-1024x683.jpg" width="1024"></a>
            <a href="9.jpg" title="Picture3"><img src="~/Content/Images/slide3-1024x683-1024x683.jpg" width="1024"></a>*@



        <a href="1.jpg" title="Picture1"><img src="~/Content/Images/slide1-1024x683-1024x683.jpg" width="140"></a>
        <a href="2.jpg" title="Picture2"><img src="~/Content/Images/slide2-1024x683-1024x683.jpg" width="140"></a>
        <a href="3.jpg" title="Picture3"><img src="~/Content/Images/slide3-1024x683-1024x683.jpg" width="140"></a>
        <a href="4.jpg" title="Picture1"><img src="~/Content/Images/slide1-1024x683-1024x683.jpg" width="140"></a>
        <a href="5.jpg" title="Picture2"><img src="~/Content/Images/slide2-1024x683-1024x683.jpg" width="140"></a>
        <a href="6.jpg" title="Picture3"><img src="~/Content/Images/slide3-1024x683-1024x683.jpg" width="140"></a>
        <a href="7.jpg" title="Picture1"><img src="~/Content/Images/slide1-1024x683-1024x683.jpg" width="140"></a>
        <a href="8.jpg" title="Picture2"><img src="~/Content/Images/slide2-1024x683-1024x683.jpg" width="140"></a>
        <a href="9.jpg" title="Picture3"><img src="~/Content/Images/slide3-1024x683-1024x683.jpg" width="140"></a>
    </div>
</div>


<style type="text/css">

    #stage {
        margin: 2em auto 1em 50%;
        height: 140px;
        -webkit-perspective: 1200px;
        -webkit-perspective-origin: 0 50%;
    }

    #rotator a {
        position: absolute;
        left: -81px;
    }

        #rotator a img {
            padding: 10px;
            border: 1px solid #ccc;
            background: #fff;
            -webkit-backface-visibility: hidden;
        }

        #rotator a:nth-of-type(1) img {
            -webkit-transform: rotateY(-90deg) translateZ(300px);
        }

        #rotator a:nth-of-type(2) img {
            -webkit-transform: rotateY(-60deg) translateZ(300px);
        }

        #rotator a:nth-of-type(3) img {
            -webkit-transform: rotateY(-30deg) translateZ(300px);
        }

        #rotator a:nth-of-type(4) img {
            -webkit-transform: translateZ(300px);
            background: #000;
        }

        #rotator a:nth-of-type(5) img {
            -webkit-transform: rotateY(30deg) translateZ(300px);
        }

        #rotator a:nth-of-type(6) img {
            -webkit-transform: rotateY(60deg) translateZ(300px);
        }

        #rotator a:nth-of-type(n+7) {
            display: none;
        }
</style>


<style type="text/css">

    @@-webkit-keyframes rotator {
        from {
            -webkit-transform: rotateY(0deg);
        }

        to {
            -webkit-transform: rotateY(30deg);
        }
    }

    #rotator {
        -webkit-transform-origin: 0 0;
        -webkit-transform-style: preserve-3d;
        -webkit-animation-timing-function: cubic-bezier(1, 0.2, 0.2, 1);
        -webkit-animation-duration: 1s;
        -webkit-animation-delay: 1s;
    }

        #rotator:hover {
            -webkit-animation-play-state: paused;
        }
</style>



<script type="text/javascript">

    document.addEventListener("DOMContentLoaded", function () {

        var rotateComplete = function () {
            target.style.webkitAnimationName = "";
            target.insertBefore(arr[arr.length - 1], arr[0]);
            setTimeout(function (el) {
                el.style.webkitAnimationName = "rotator";
            }, 0, target);
        };

        var target = document.getElementById("rotator");
        var arr = target.getElementsByTagName("a");

        target.addEventListener("webkitAnimationEnd", rotateComplete, false);

    }, false);

</script>


<script type="text/javascript">

    // Original JavaScript code by Chirp Internet: www.chirp.com.au
    // Please acknowledge use of this code by including this header.

    var rotateComplete = function () {
        with (target.style) {
            webkitAnimationName = MozAnimationName = msAnimationName = "";
        }
        target.insertBefore(arr[arr.length - 1], arr[0]);
        setTimeout(function (el) {
            with (el.style) {
                webkitAnimationName = MozAnimationName = msAnimationName = "rotator";
            }
        }, 0, target);
    };

    var target = document.getElementById("rotator");
    var arr = target.getElementsByTagName("a");

    target.addEventListener("webkitAnimationEnd", rotateComplete, false);
    target.addEventListener("animationend", rotateComplete, false);
    target.addEventListener("MSAnimationEnd", rotateComplete, false);

</script>






