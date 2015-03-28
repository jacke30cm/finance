(function ($) {
    $(window).load(function () {

        $('.horizontal-scroll').mCustomScrollbar({
            axis: "x",
            theme: "light",
            autoDraggerLength: true,
            autoHideScrollbar: true,
            scrollbarPosition: "inside",
            alwaysShowScrollbar: 0,
            advanced: { autoExpandHorizontalScroll: true },
            mouseWheel: { scrollAmount: 300 },
            keyboard: { scrollAmount: 15 },
            scrollInertia: 400,
        });


        $('.vertical-scroll').mCustomScrollbar({
            axis: "y",
            theme: "light",
            autoDraggerLength: true,
            autoHideScrollbar: true,
            scrollbarPosition: "inside",
            alwaysShowScrollbar: 0,
            advanced: { autoExpandHorizontalScroll: true },
            mouseWheel: { scrollAmount: 150 },
            keyboard: { scrollAmount: 15 },
            scrollInertia: 400,
        });

    });
})(jQuery);

$(document).ready(function () {

    //Account Development chart

    google.setOnLoadCallback(drawChart);

    function drawChart() {
        var data = google.visualization.arrayToDataTable([
          ['Datum', 'Utveckling'],
          ['1/5', 5],
          ['2/5', 4],
          ['3/5', 7],
          ['4/5', 13],
          ['5/5', 9],
          ['6/5', 10],
          ['7/5', 9],
          ['8/5', 14],
          ['9/5', 16],
          ['10/5', 15],
          ['11/5', 14],
          ['12/5', 16],
          ['13/5', 19],
          ['14/5', 20],
          ['15/5', 22],
          ['16/5', 21],
          ['17/5', 20],
          ['18/5', 23],
          ['19/5', 21],
          ['20/5', 26],
          ['21/5', 15],
          ['22/5', 7],
          ['23/5', 6],
          ['24/5', 6],
          ['25/5', 6], 
          ['26/5', -5]
        ]);

        var options = {
            curveType: 'function',
            legend: { position: 'none' },
            fontName: 'Open Sans',
            animation: { startup: true, duration: 500, easing: 'out' },
            series: {
                0: {
                    color: '#90c3c1'
                }

            },
            hAxis: {
                format: 'dd-MM',
            },
            vAxis: {
                format: '#\'%',
                baselineColor: '#EBEBEB',
                gridlines: {
                    color: '#EBEBEB'
                }

            }
        };

        var chart = new google.visualization.LineChart($('.account-development')[0]);

        chart.draw(data, options);
    }




    //Donut chart
    if ($('.donut-chart').length > 0) {
        var data = [
			{ label: "Aktier", data: 65 },
			{ label: "Fonder", data: 20 },
            { label: "Kapital", data: 20 }
        ];

        $.plot('.donut-chart', data, {
            series: {
                pie: {
                    show: true,
                    //innerRadius: .4,
                    innerRadius: 0.64,
                    stroke: {
                        width: 2,
                        color: "#F9F9F9"
                    },
                    label: {
                        show: true,
                        radius: 3 / 4,
                        formatter: donutLabelFormatter,
                        background: {
                            opacity: 0.5,
                            color: '#FFF'
                        }
                    }
                },
            },
            legend: {
                show: false,

            },
            grid: {
                hoverable: true
            },
            colors: ["#90c3c1", "#9095c3", "#EBEBEB"],
        });
    }

    function donutLabelFormatter(label, series) {
        return "<div class=\"donut-label\"><p class=\"-capitalize\">" + label + "<br/>" + Math.round(series.percent) + "%</div>";
    }


    // Easy pie chart
    var options = {


        barColor: '#a4c8be',
        lineCap: 'square',
        scaleColor: false,
        animate: 2500,
        easing: 'easeOutCubic',
        trackColor: '#EBEBEB'
    };

    if ($(window).width() >= 1366) {

        options.size = 180;
        options.lineWidth = 12;

    } else {

        options.size = 150;
        options.lineWidth = 10;
    }

    $('.pie-chart').each(function () {

        var percent = Math.round($(this).attr('data-percent'));
        options.barColor = chartColor(percent);
        $(this).easyPieChart(options);
    });

    //Define chart
    function chartColor(percentage) {

        if (percentage > -100 && percentage < -66) {

            return '#dd7f7f';
        }
        else if (percentage >= -66 && percentage < -33) {

            return '#c390b8';
        }
        else if (percentage >= -33 && percentage <= -1) {

            return '#c3a690';
        }
        else if (percentage >= 0 && percentage < 33) {

            return '#9095c3';
        }
        else if (percentage >= 33 && percentage < 66) {

            return '#90c3c1';
        }
        else if (percentage >= 66) {

            return '#90c394';
        }

    };


    setInterval(function () {

        $('.pie-chart').data('easyPieChart').update(Math.floor(100 * Math.random()));

    }, 3000);




    // Bring user-options

    $(this).on('click', '.user-panel', function () {


        if ($(this).find('.drop-down').css('display') == 'none') {

            $(this).find('.drop-down').css('display', 'initial');

        } else {

            $(this).find('.drop-down').css('display', 'none');
        }


    });

    // Animate mouse-over and leave

    $(this).on('mouseenter', '.competition', function () {

        $(this).find('.competition-ghost').stop().animate({ opacity: 0.6 }, 700, 'easeOutQuint');

    });

    $(this).on('mouseleave', '.competition', function () {

        $(this).find('.competition-ghost').stop().animate({ opacity: 0.0 }, 700, 'easeOutQuint');

    });

    //Bring controlpanel

    $(this).on('click', '.user-panel .drop-down #create-competition', function () {

        $('.control-panel-overlay').css('opacity', '0.0');
        $('.control-panel-overlay').css('display', 'initial');

        $('.control-panel-overlay').animate({ opacity: 0.7 }, 500);

        $('.control-panel').animate({ bottom: '0%' }, 500, 'easeOutQuint');

    });


    $(document).on('click', '.control-panel .button-close', function () {

        closeDown();
    });

    $(document).on('click', '.control-panel-overlay', function () {

        closeDown();
    });

    $(document).on('keyup', function (e) {

        if (e.keyCode == 27) {

            closeDown();
        }
    });

    function closeDown() {

        $('.control-panel-overlay').animate({ opacity: 0.0 }, 500, function () {
            $(this).css('display', 'none');
        });

        $('.control-panel').animate({ bottom: '-50%' }, 500, 'easeOutQuint');
    }


});