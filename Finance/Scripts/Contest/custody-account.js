$(document).ready(function () {


    //PROTOTYPES

    if (typeof String.prototype.startsWith != 'function') {
        String.prototype.startsWith = function (str) {

            return this.slice(0, str.length) == str;
        };
    }

    if (typeof String.prototype.endsWith != 'function') {
        String.prototype.endsWith = function (str) {
            return this.slice(-str.length) == str;
        };
    }

    // Search for transactions 

    $(document).on('keyup', '#custody-account  .col-lg  .block-lg  .rectangular-block  .col-lg  .input-text-special-wrap  input[type="text"]', function () {

        var searchString = $(this).val().toLowerCase();


        $('#custody-account .col-lg  .block-lg .block-lg .rectangular-block').each(function () {

            var value = $(this).find('.block-s:nth-of-type(2)').find('p').text().toLowerCase();

            $(this)[value.indexOf(searchString) !== -1 ? 'show' : 'hide'](); 

        });

    });



    //Account Development chart

    google.setOnLoadCallback(drawChart);

    function drawChart() {

        var arr = [
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
          ['23/5', -5],
          ['24/5', -21],
          ['25/5', -44],
          ['26/5', -75]

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

    function toolTiper(date, value) {
        return +
            '<div class="account-development-tooltip">' +
                '<p>' + date + '</p>' +
                '<p>' + value + '%</p>' +
            '</div>';

    };


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




});