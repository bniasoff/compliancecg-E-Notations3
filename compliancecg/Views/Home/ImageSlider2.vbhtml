@Code
    ViewData("Title") = "Home Page"
    'Layout = Nothing
End Code

@*<script src="~/Scripts/jquery-3.4.1.js"></script>*@



<script src="~/Scripts/jssor/jssor.slider.min.js"></script>

<div id="jssor_1" style="padding-top:40px;left:0px;width:1900px;height:500px;overflow:hidden;">
    <div data-u="slides" style="top:60px;left:0px;width:1900px;height:500px;overflow:hidden;">
        <div><img data-u="image" src="~/Content/Images/slide1-1024x683-1024x683.jpg" /></div>
        <div><img data-u="image" src="~/Content/Images/slide2-1024x683-1024x683.jpg" /></div>
        <div><img data-u="image" src="~/Content/Images/slide3-1024x683-1024x683.jpg" /></div>
    </div>
</div>


@*<div id="jssor_1" style="position:relative;top:0px;left:0px;width:1900px;height:500px;overflow:hidden;">
    <div data-u="slides" style="position:absolute;top:0px;left:0px;width:1900px;height:500px;overflow:hidden;">
        <div><img data-u="image" src="~/Content/Images/slide1-1024x683-1024x683.jpg" /></div>
        <div><img data-u="image" src="~/Content/Images/slide2-1024x683-1024x683.jpg" /></div>
        <div><img data-u="image" src="~/Content/Images/slide3-1024x683-1024x683.jpg" /></div>
    </div>
</div>*@



@*<div id="wrapper">
        <img src="http://lorempixel.com/200/200" />
        <img  src="~/Content/Images/slide1-1024x683-1024x683.jpg" />

    </div>*@


@*<style>

    #jssor_3, img{
        width:100%;
        height: 500px;
    }
    </style>*@





<script>

    var jssor_1_SlideshowTransitions = [
        { $Duration: 1400, x: 0.25, $Zoom: 1.5, $Easing: { $Left: $Jease$.$InWave, $Zoom: $Jease$.$InSine }, $Opacity: 2, $ZIndex: -10, $Brother: { $Duration: 1400, x: -0.25, $Zoom: 1.5, $Easing: { $Left: $Jease$.$InWave, $Zoom: $Jease$.$InSine }, $Opacity: 2, $ZIndex: -10 } },
        { $Duration: 1500, x: 0.5, $Cols: 2, $ChessMode: { $Column: 3 }, $Easing: { $Left: $Jease$.$InOutCubic }, $Opacity: 2, $Brother: { $Duration: 1500, $Opacity: 2 } },
        { $Duration: 1500, x: 0.3, $During: { $Left: [0.6, 0.4] }, $Easing: { $Left: $Jease$.$InQuad, $Opacity: $Jease$.$Linear }, $Opacity: 2, $Outside: true, $Brother: { $Duration: 1000, x: -0.3, $Easing: { $Left: $Jease$.$InQuad, $Opacity: $Jease$.$Linear }, $Opacity: 2 } },
        { $Duration: 1200, x: 0.25, y: 0.5, $Rotate: -0.1, $Easing: { $Left: $Jease$.$InQuad, $Top: $Jease$.$InQuad, $Opacity: $Jease$.$Linear, $Rotate: $Jease$.$InQuad }, $Opacity: 2, $Brother: { $Duration: 1200, x: -0.1, y: -0.7, $Rotate: 0.1, $Easing: { $Left: $Jease$.$InQuad, $Top: $Jease$.$InQuad, $Opacity: $Jease$.$Linear, $Rotate: $Jease$.$InQuad }, $Opacity: 2 } },
        {
            $Duration: 1600, x: 1, $Rows: 2, $ChessMode: { $Row: 3 }
            , $Easing: {
                $Left: $Jease$.$InOutQuart, $Opacity: $Jease$.$Linear
            }, $Opacity: 2, $Brother: { $Duration: 1600, x: -1, $Rows: 2, $ChessMode: { $Row: 3 }, $Easing: { $Left: $Jease$.$InOutQuart, $Opacity: $Jease$.$Linear }, $Opacity: 2 }
        },
        { $Duration: 1600, y: -1, $Cols: 2, $ChessMode: { $Column: 12 }, $Easing: { $Top: $Jease$.$InOutQuart, $Opacity: $Jease$.$Linear }, $Opacity: 2, $Brother: { $Duration: 1600, y: 1, $Cols: 2, $ChessMode: { $Column: 12 }, $Easing: { $Top: $Jease$.$InOutQuart, $Opacity: $Jease$.$Linear }, $Opacity: 2 } },
        { $Duration: 1200, y: 1, $Easing: { $Top: $Jease$.$InOutQuart, $Opacity: $Jease$.$Linear }, $Opacity: 2, $Brother: { $Duration: 1200, y: -1, $Easing: { $Top: $Jease$.$InOutQuart, $Opacity: $Jease$.$Linear }, $Opacity: 2 } },
        { $Duration: 1500, x: -0.1, y: -0.7, $Rotate: 0.1, $During: { $Left: [0.6, 0.4], $Top: [0.6, 0.4], $Rotate: [0.6, 0.4] }, $Easing: { $Left: $Jease$.$InQuad, $Top: $Jease$.$InQuad, $Opacity: $Jease$.$Linear, $Rotate: $Jease$.$InQuad }, $Opacity: 2, $Brother: { $Duration: 1000, x: 0.2, y: 0.5, $Rotate: -0.1, $Easing: { $Left: $Jease$.$InQuad, $Top: $Jease$.$InQuad, $Opacity: $Jease$.$Linear, $Rotate: $Jease$.$InQuad }, $Opacity: 2 } },
        { $Duration: 1600, x: -0.2, $Delay: 40, $Cols: 12, $During: { $Left: [0.4, 0.6] }, $SlideOut: true, $Formation: $JssorSlideshowFormations$.$FormationStraight, $Assembly: 260, $Easing: { $Left: $Jease$.$InOutExpo, $Opacity: $Jease$.$InOutQuad }, $Opacity: 2, $Outside: true, $Round: { $Top: 0.5 }, $Brother: { $Duration: 1000, x: 0.2, $Delay: 40, $Cols: 12, $Formation: $JssorSlideshowFormations$.$FormationStraight, $Assembly: 1028, $Easing: { $Left: $Jease$.$InOutExpo, $Opacity: $Jease$.$InOutQuad }, $Opacity: 2, $Round: { $Top: 0.5 } } },
        { $Duration: 700, $Opacity: 2, $Brother: { $Duration: 1000, $Opacity: 2 } },
        { $Duration: 1200, x: 1, $Easing: { $Left: $Jease$.$InOutQuart, $Opacity: $Jease$.$Linear }, $Opacity: 2, $Brother: { $Duration: 1200, x: -1, $Easing: { $Left: $Jease$.$InOutQuart, $Opacity: $Jease$.$Linear }, $Opacity: 2 } }
    ];

    //var jssor_1_SlideshowTransitions = [
    //    { $Duration: 800, $Delay: 20, $Clip: 12, $SlideOut: true, $Assembly: 260, $Easing: { $Clip: $Jease$.$OutCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
    //    { $Duration: 800, x: 0.3, $During: { $Left: [0.3, 0.7] }, $Easing: { $Left: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
    //    { $Duration: 800, x: 0.3, $Rows: 2, $During: { $Left: [0.3, 0.7] }, $ChessMode: { $Row: 3 }, $Easing: { $Left: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2, $Outside: true },
    //    { $Duration: 1500, x: 0.2, y: -0.1, $Delay: 80, $Cols: 10, $Rows: 5, $Clip: 15, $During: { $Left: [0.2, 0.8], $Top: [0.2, 0.8] }, $SlideOut: true, $Easing: { $Left: $Jease$.$InWave, $Top: $Jease$.$InWave, $Clip: $Jease$.$Linear }, $Opacity: 2, $Round: { $Left: 0.8, $Top: 2.5 } },
    //    {$Duration:1500,x:-1,y:0.5,$Delay:100,$Cols:10,$Rows:5,$SlideOut:true,$Formation:$JssorSlideshowFormations$.$FormationSwirl,$Easing:{$Left:$Jease$.$Linear,$Top:$Jease$.$OutJump},$Opacity:2,$Assembly:260,$Round:{$Top:1.5}}

    //];

    var jssor_1_options = {
        $AutoPlay: 1,
        $FillMode: 2,
        $SlideshowOptions: {
            $Class: $JssorSlideshowRunner$,
            $Transitions: jssor_1_SlideshowTransitions,
            $TransitionsOrder: 1
        },
        $ArrowNavigatorOptions: {
            $Class: $JssorArrowNavigator$
        },
        $BulletNavigatorOptions: {
            $Class: $JssorBulletNavigator$
        }
    };
    var jssor_1_slider = new $JssorSlider$("jssor_1", jssor_1_options);
    //var options = { $AutoPlay: 1 };

    //var options =  { $AutoPlay: 1,$Duration:800,x:0.3,$During:{$Left:[0.3,0.7]},$Easing:{$Left:$Jease$.$InCubic,$Opacity:$Jease$.$Linear},$Opacity:2};
    //var jssor_1_slider = new $JssorSlider$("jssor_1", options);
</script>