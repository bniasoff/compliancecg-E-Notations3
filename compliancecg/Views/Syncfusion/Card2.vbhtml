﻿<div class="col-lg-12 control-section card-control-section basic_card_layout">
    <div class="e-card-resize-container">
        <div class='row'>
            <div class="row card-layout">
                <div class="col-xs-6 col-sm-6 col-lg-6 col-md-6">
                    <!-- Basic Card Layout  -->
                    <div tabindex="0" class="e-card" id="basic_card">
                        <div class="e-card-header">
                            <div class="e-card-header-caption">
                                <div class="e-card-header-title">Debunking Five Data Science Myths</div>
                                <div class="e-card-sub-title">By John Doe | Jan 20, 2018 </div>
                            </div>
                        </div>
                        <div class="e-card-content">
                            Tech evangelists are currently pounding their pulpits about all things AI, machine learning, analytics—anything that sounds
                            like the future and probably involves lots of numbers. Many of these topics can be grouped under
                            the intimidating term data science.
                        </div>
                        <div class="e-card-actions">
                            <button class="e-btn e-outline e-primary">
                                Read More
                            </button>
                        </div>
                    </div>
                </div>


                @*<div class="col-xs-6 col-sm-6 col-lg-6 col-md-6">
                     Weather Card Layout  
                    <div tabindex="0" class="e-card" id="weather_card">
                        <div class="e-card-header">
                            <div class="e-card-header-caption">
                                <div class="e-card-header-title">Today</div>
                                <div class="e-card-sub-title"> New York - Scattered Showers.</div>
                            </div>
                        </div>
                        <div class="e-card-header weather_report">
                            <div class="e-card-header-image"></div>
                            <div class="e-card-header-caption">
                                <div class="e-card-header-title">1&#186; / -4&#186;</div>
                                <div class="e-card-sub-title">Chance for snow: 100%</div>
                            </div>
                        </div>
                    </div>
                </div>*@
            </div>
        </div>
    </div>
</div>





<style>
    /* Weather Card Layout Customization */

    .card-control-section.basic_card_layout #weather_card.e-card {
        background-image: url('./src/card/images/weather.png');
    }

    .card-control-section.basic_card_layout #weather_card.e-card .e-card-header-caption .e-card-header-title,
    .card-control-section.basic_card_layout #weather_card.e-card .e-card-header-caption .e-card-sub-title {
        color: white;
    }

    .highcontrast .card-control-section.basic_card_layout #weather_card.e-card .e-card-header.weather_report .e-card-header-image {
        border: none;
    }

    .card-control-section.basic_card_layout #weather_card.e-card .weather_report .e-card-header-caption {
        text-align: right;
    }

    .card-control-section.basic_card_layout #weather_card.e-card .e-card-header.weather_report .e-card-header-image {
        background-image: url('./src/card/images/rainy.svg');
    }

    .card-control-section.basic_card_layout .col-xs-6.col-sm-6.col-lg-6.col-md-6 {
        width: 100%;
        padding: 10px;
    }

    .card-control-section.basic_card_layout .card-layout {
        margin: auto;
        max-width: 400px;
    }

    @@media (min-width: 870px) {
        .card-control-section.basic_card_layout .col-xs-6.col-sm-6.col-lg-6.col-md-6 {
            width: 50%;
        }

        .card-control-section.basic_card_layout .card-layout {
            max-width: 870px;
        }
    }
</style>