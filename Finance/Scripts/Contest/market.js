$(document).ready(function () {


    // Search for shares 

    $(document).on('keyup', '#market  .col-lg  .block-lg .clean-content .input-text-special-wrap  input[type="text"]', function () {

        var searchString = $(this).val().toLowerCase();

        $('#market .col-lg .block-lg .clean-content table tbody tr').each(function () {

            var value = $(this).find('td:first-of-type').find('p').text().toLowerCase();

            $(this)[value.indexOf(searchString) !== -1 ? 'show' : 'hide']();

        });

        paginateTable();

    });


    
   
    var visibleRows = 8;
    var $rows;

    function paginateTable() {

        $('.table-pagination').empty();

        $rows = $('#market .col-lg .block-lg .clean-content table tbody tr:visible');
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


    // These calls are made to hide col-lg except first one, to make animation fancy,
    // and also to disable the otherwise enabled scroll-functionality, becase there's nothing to scroll

    $('#market .col-lg').not('#market .col-lg:first-of-type').css({
        'visibility': 'hidden',
        'opacity': '0.0',
        'left': '-30px'
    });

    $('.horizontal-scroll').mCustomScrollbar("disable");



    // Animate the columns containing share-data 
    var activeView;
    $(this).on('click', '#share-table tbody tr', function () {


        var $columns = $('#market .col-lg').not('#market .col-lg:first-of-type');
        var share = $(this).attr('data-id');



        if (share == activeView) {

            $columns.animate({ left: '-30px', opacity: 0.0 }, 300, 'easeOutQuint', function () {


                $columns.css({ 'visibility': 'hidden' });
                $('.horizontal-scroll').mCustomScrollbar("disable");

            });

            activeView = undefined;


        } else {


            $columns.css({ 'visibility': 'visible' }).animate({ left: '0px', opacity: 1.0 }, 300, 'easeOutQuint');
            activeView = share;
            $('.horizontal-scroll').mCustomScrollbar("update");
        }



    });



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
          ['20/5', 26]
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

        var chart = new google.visualization.LineChart($('.share-development')[0]);

        chart.draw(data, options);
    }


});