@modeltype  IEnumerable(Of compliancecg.JQueryImageSlider.Models.ImageModel)


@code
    ViewBag.Title = "Home Page"
End Code

@Code
    Dim index As Integer = 0
End Code

<div id="page" class="row">
    <section>
        <div id="viewport">
            <div id="box">
                @*Bind images here*@
                @For Each item As compliancecg.JQueryImageSlider.Models.ImageModel In Model
                    @<figure Class="slide">
                        <img src=@item.ImagePath Class="img-responsive">
                        @*<img src="~/img/finding-the-key.jpg" />*@
                        <figcaption> Static Caption</figcaption>
                    </figure>

                Next

            </div>
        </div>
        <footer>
            <nav class="slider-controls">
                <a class="increment-control" href="#" id="prev" title="go to the next slide">&laquo; Prev</a>
                <a class="increment-control" href="#" id="next" title="go to the next slide">Next &raquo;</a>
                <ul id="controls">

                    @*<a href="2.jpg" title="Picture2"><img src="~/Content/Images/slide2-1024x683-1024x683.jpg" width="1024"></a>*@

                    @For Each item As compliancecg.JQueryImageSlider.Models.ImageModel In Model
                        Dim cssClass As String = If(index.Equals(0), "goto-slide current", "goto-slide")
                        @*@<a href = "2.jpg" title="Picture2"><img src="~/Content2/Images/slide2-1024x683-1024x683.jpg" width="1024"></a>*@

                        @*@<a href = "2.jpg" title="Picture2"><img src="~/Content/Images/slide2-1024x683-1024x683.jpg" width="1024"></a>*@

                        @<li> <a Class="goto-slide current" href=" #" data-slideindex=0></a></li>
                        '<li> <a Class="@cssClass" href=" #" data-slideindex="@index"></a></li>
                        index = index + 1
                    Next
                </ul>
            </nav>
        </footer>
    </section>
    <aside id="effect-switcher">
        <h2>Select effect</h2>
        <ul id="effect-list">
            <li><a href="#" class="effect current" data-fx="scrollVert3d">Vertical 3D scroll</a></li>
            <li> <a href="#" Class="effect" data-fx="scrollHorz3d">Horizontal 3D scroll</a></li>
            <li> <a href="#" Class="effect" data-fx="tile3d">3D tiles<span Class="new-effect">new!</span></a></li>
            <li> <a href="#" Class="effect" data-fx="tile">2D tiles<span Class="new-effect">new!</span></a></li>
            <li> <a href="#" Class="effect" data-fx="scrollVert">Vertical scroll</a></li>
            <li> <a href="#" Class="effect" data-fx="scrollHorz">Horizontal scroll</a></li>
            <li> <a href="#" Class="effect" data-fx="blindLeft">Blind left</a></li>
            <li> <a href="#" Class="effect" data-fx="blindDown">Blind down</a></li>
            <li> <a href="#" Class="effect" data-fx="fade">Fade</a></li>
        </ul>
    </aside>
</div>



<script type="text/javascript">
    $(function () {
        debugger;
        // This function runs before the slide transition starts
        var switchIndicator = function ($c, $n, currIndex, nextIndex) {
            // kills the timeline by setting it's width to zero
            $timeIndicator.stop().css('width', 0);
            // Highlights the next slide pagination control
            $indicators.removeClass('current').eq(nextIndex).addClass('current');
        };

        // This function runs after the slide transition finishes
        var startTimeIndicator = function () {
            debugger;
            // start the timeline animation
            $timeIndicator.animate({ width: '100%' }, slideInterval);
        };

        var $box = $('#box')
            , $indicators = $('.goto-slide')
            , $effects = $('.effect')
            , $timeIndicator = $('#time-indicator')
            , slideInterval = 5000
            , defaultOptions = {
                speed: 1200
                , autoScroll: true
                , timeout: slideInterval
                , next: '#next'
                , prev: '#prev'
                , pause: '#pause'
                , onbefore: switchIndicator
                , onafter: startTimeIndicator
            }
            , effectOptions = {
                'blindLeft': { blindCount: 15 }
                , 'blindDown': { blindCount: 15 }
                , 'tile3d': { tileRows: 6, rowOffset: 80 }
                , 'tile': { tileRows: 6, rowOffset: 80 }
            };

        // initialize the plugin with the desired settings
        $box.boxSlider(defaultOptions);
        // start the time line for the first slide
        startTimeIndicator();

        // Paginate the slides using the indicator controls
        $('#controls').on('click', '.goto-slide', function (ev) {
            debugger;
            $box.boxSlider('showSlide', $(this).data('slideindex'));
            ev.preventDefault();
        });

        // This is for demo purposes only, kills the plugin and resets it with
        // the newly selected effect
        $('#effect-list').on('click', '.effect', function (ev) {
            debugger;
            var $effect = $(this)
                , fx = $effect.data('fx')
                , extraOptions = effectOptions[fx];

            $effects.removeClass('current');
            $effect.addClass('current');
            switchIndicator(null, null, 0, 0);
            $box
                .boxSlider('destroy')
                .boxSlider($.extend({ effect: fx }, defaultOptions, extraOptions));
            startTimeIndicator();

            ev.preventDefault();
        });
    });</script>

