$(document).ready(function () {




    // Search for transactions 

    $(document).on('keyup', '#portfolio  .col-lg  .block-lg  .rectangular-block  .col-lg  .input-text-special-wrap  input[type="text"]', function () {

        var searchString = $(this).val().toLowerCase();


        $('#portfolio .col-lg  .block-lg .block-lg .rectangular-block').each(function () {

            var value = $(this).find('.block-s:nth-of-type(2)').find('p').text().toLowerCase();

            $(this)[value.indexOf(searchString) !== -1 ? 'show' : 'hide']();

        });

    });


    //Account Development chart

    google.setOnLoadCallback(drawChart);

    function drawChart() {

        var arr = [
            ['Datum', 'Min utveckling', 'Tävlingsgenomsnitt'],
            ['1/5', 5, -5],
            ['2/5', 4, -4],
            ['3/5', 7, -3],
            ['4/5', 13, -8],
            ['5/5', 9, -10],
            ['6/5', 10, -5],
            ['7/5', 9, -1],
            ['8/5', 14, 3],
            ['9/5', 16, 4],
            ['10/5', 15, 3],
            ['11/5', 14, 6],
            ['12/5', 16, 3],
            ['13/5', 19, 8],
            ['14/5', 20, 5],
            ['15/5', 22, 6],
            ['16/5', 21, 5],
            ['17/5', 20, 8],
            ['18/5', 23, 9],
            ['19/5', 21, 5],
            ['20/5', 26, 6],
            ['21/5', 15, 7],
            ['22/5', 7, 8],
            ['23/5', -5, 9],
            ['24/5', -21, 18],
            ['25/5', -44, 28],
            ['26/5', -34, 55]
        ];

        var data = google.visualization.arrayToDataTable(arr);

        var color = '#90c3c1';

        if (arr[arr.length - 1][1] < 0) {

            color = '#dd7f7f';
        }

        var options = {
            curveType: 'function',
            height: 615,
            width: 615,
            legend: { position: 'none' },
            fontName: 'Open Sans',
            fontSize: 12,
            animation: { startup: true, duration: 500, easing: 'out' },
            series: {
                0: {
                    color: color
                },
                1: {
                    color: '#CCCCCC'
                }
            },
            hAxis: {
                format: 'dd-MM',
                showTextEvery: parseInt(data.getNumberOfRows() / 4),
            },
            vAxis: {
                format: '#\'%',
                baselineColor: '#EBEBEB',
                gridlines: {
                    color: '#EBEBEB'
                }

            },
            toolTip: { isHtml: true }
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
                    innerRadius: 0.66,
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
        options.lineWidth = 8;

    } else {

        options.size = 150;
        options.lineWidth = 6;
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
        } else if (percentage >= -66 && percentage < -33) {

            return '#c390b8';
        } else if (percentage >= -33 && percentage <= -1) {

            return '#c3a690';
        } else if (percentage >= 0 && percentage < 33) {

            return '#9095c3';
        } else if (percentage >= 33 && percentage < 66) {

            return '#90c3c1';
        } else if (percentage >= 66) {

            return '#90c394';
        }

    };


    var visibleRows = 5;
    var initialPagination = true;
    var $rows;

    function paginateTable() {

        $('.table-pagination').empty();

        if (initialPagination) {

            $rows = $('#portfolio .col-lg .block-lg .clean-content table tbody tr');
            initialPagination = false;

        } else {

            $rows = $('#portfolio .col-lg .block-lg .clean-content table tbody tr:visible');

        }

        var totalRows = $rows.length;
        var numberOfPages = totalRows / visibleRows;

        for (var x = 0; x < numberOfPages; x++) {

            var pageNumber = x + 1;
            $('.table-pagination').append('<div class="pagination-item" pagination="' + x + '"><p>' + pageNumber + '</p></div>');

        }

        $rows.hide();
        $rows.slice(0, visibleRows).show();
        $('.table-pagination .pagination-item:first-of-type').find('p').addClass('color-green');
    }

    paginateTable();

    $(document).on('click', '.table-pagination .pagination-item', function () {

        $('.table-pagination .pagination-item').find('p').removeClass('color-green');
        $(this).find('p').addClass('color-green');


        var thisPage = $(this).find('p').text();
        thisPage = parseInt(thisPage) - 1;

        var startItem = thisPage * visibleRows;
        var endItem = startItem + visibleRows;

        $rows.css('opacity', '0.0').hide().slice(startItem, endItem).css('display', 'table-row').animate({ opacity: 1.0 }, 0);

    });

});