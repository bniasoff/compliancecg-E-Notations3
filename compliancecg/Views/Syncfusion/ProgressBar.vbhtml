﻿@*@Html.EJS().ProgressButton("progress").Content("Progress").EnableProgress(True).Duration(15000).Begin("begin").Progress("progress").End("end").CssClass("e-hide-spinner").Render()

    <script>
        function begin(args) {
            debugger;
            this.content = 'Progress ' + args.percent + '%';
            this.dataBind();
        }

        function progress(args) {
            this.content = 'Progress ' + args.percent + '%';
            this.dataBind();
            if (args.percent === 40) {
                args.percent = 90
            }
        }

        function end(args) {
            this.content = 'Progress ' + args.percent + '%';
            this.dataBind();
        }
    </script>*@


@*@Html.EJS().ProgressButton("download").Content("Download").EnableProgress(true).IconCss("e-btn-sb-icon e-download").Duration(4000).Created("created").End("end").CssClass("e-hide-spinner").Render()

    <script>
        function end() {
               debugger;
            var progressBtn = ej.base.getComponent(document.querySelector("#download"), "progress-btn");
            progressBtn.content = 'Download';
            progressBtn.iconCss = 'e-btn-sb-icon e-download';
            progressBtn.dataBind();
        }

        function created() {
               debugger;
            var progressBtn = ej.base.getComponent(document.querySelector("#download"), "progress-btn");
            progressBtn.element.addEventListener('click', clickHandler);
        }

        function clickHandler() {
            debugger;
            var progressBtn = ej.base.getComponent(document.querySelector("#download"), "progress-btn");
            if (progressBtn.content === 'Download') {
                progressBtn.content = 'Pause';
                progressBtn.iconCss = 'e-btn-sb-icon e-pause';
                progressBtn.dataBind();
            }
            // clicking on ProgressButton will stop the progress when the text content is 'Pause'
            else if (progressBtn.content === 'Pause') {
                   debugger;
                progressBtn.content = 'Resume';
                progressBtn.iconCss = 'e-btn-sb-icon e-play';
                progressBtn.dataBind();
                progressBtn.stop();
            }
            // clicking on ProgressButton will start the progress when the text content is 'Resume'
            else if (progressBtn.content === 'Resume') {
                   debugger;
                progressBtn.content = 'Pause';
                progressBtn.iconCss = 'e-btn-sb-icon e-pause';
                progressBtn.dataBind();
                progressBtn.start();
            }
        }
    </script>
    <style>
        @@font-face {
            font-family: 'btn-icon';
            src: url(data:application/x-font-ttf;charset=utf-8;base64,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) format('truetype');
            font-weight: normal;
            font-style: normal;
        }

        .e-btn-sb-icon {
            font-family: 'btn-icon' !important;
            speak: none;
            font-size: 55px;
            font-style: normal;
            font-weight: normal;
            font-variant: normal;
            text-transform: none;
            line-height: 1;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }

        .e-download::before {
            content: '\e706';
        }

        .e-play::before {
            content: '\e700';
        }

        .e-pause::before {
            content: '\e701';
        }
    </style>*@


@Html.EJS().ProgressButton("progress").Content("Progress").EnableProgress(True).Duration(1000).Begin("begin").Progress("progress").End("end").CssClass("e-hide-spinner").Render()

<script>
    function begin(args) {
        this.content = 'Progress ' + args.percent + '%';
        this.dataBind();
    }

    function progress(args) {
        this.content = 'Progress ' + args.percent + '%';
        this.dataBind();
        if (args.percent === 40) {
            args.percent = 90
        }
    }

    function end(args) {
        debugger;
        this.content = 'Progress ' + args.percent + '%';
        this.dataBind();
    }
</script>