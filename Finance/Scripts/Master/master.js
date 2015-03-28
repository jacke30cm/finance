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
            keyboard: {  scrollAmount: 15},
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
                    innerRadius: 0.62,
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

        size: 200,
        lineWidth: 15,
        barColor: '#a4c8be',
        lineCap: 'square',
        scaleColor: false,
        animate: 2500,
        easing: 'easeOutCubic',
        trackColor: '#EBEBEB'
    };

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


    setInterval(function() {

        $('.pie-chart').data('easyPieChart').update(Math.floor(100 * Math.random()));

    }, 3000); 
    



    // Bring user-options

    $(this).on('click', '.user-panel', function() {


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