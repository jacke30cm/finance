$(document).ready(function () {



    

    //alert($('.market-search-share-list li').length); 

    // Search for shares 

    $(document).on('keyup', '.sub-section .col-full .input-text-special-wrap  input[type="text"]', function () {



        var searchString = $(this).val().toLowerCase();

        $('.sub-section .col-full .market-search-share-list li').each(function () {

            var value = $(this).find('.data').find('h4').text().toLowerCase();

            $(this)[value.indexOf(searchString) !== -1 ? 'show' : 'hide']();

        });

    });


    //$(document).on('keyup', '#market  .col-lg  .block-lg .clean-content .input-text-special-wrap  input[type="text"]', function () {

    //    var searchString = $(this).val().toLowerCase();

    //    $('#market .col-lg .block-lg .clean-content table tbody tr').each(function () {

    //        var value = $(this).find('td:first-of-type').find('p').text().toLowerCase();

    //        $(this)[value.indexOf(searchString) !== -1 ? 'show' : 'hide']();

    //    });

    //    paginateTable();

    //});




    // These calls are made to hide col-lg except first one, to make animation fancy,
    // and also to disable the otherwise enabled scroll-functionality, becase there's nothing to scroll

    $('#market .col-lg').not('#market .col-lg:first-of-type, #market .initial-views').css({
        'visibility': 'hidden',
        'opacity': '0.0',
        'left': '-30px',
        'width': '0'
    });


    // Animate the columns containing share-data 
    var activeView;
    $(this).on('click', '#share-table tbody tr', function () {

        $('#share-table tbody tr').removeClass('selected');
        var $columns = $('#market .col-lg').not('#market .col-lg:first-of-type, #market .initial-views');
        var share = $(this).attr('data-id');



        if (share == activeView) {



            $columns.animate({ left: '-30px', opacity: 0.0 }, 300, 'easeOutQuint', function () {


                $columns.css({ 'visibility': 'hidden', 'width': '0' });
                $('#market .initial-views').css({ 'visibility': 'visible', 'width': '', 'margin-left': '-10px' }).animate({ left: '0px', opacity: 1.0 }, 300, 'easeOutQuint');


            });

            activeView = undefined;


        } else {

            $(this).addClass('selected');
            $('#market .initial-views').animate({ left: '-30px', opacity: 0.0 }, 300, 'easeOutQuint', function () {

                $(this).css('width', '0');

                $columns.css({ 'visibility': 'visible', 'width': '' }).animate({ left: '0px', opacity: 1.0 }, 300, 'easeOutQuint');
                activeView = share;


            });



        }



    });




    var shareDevelopment = new google.visualization.LineChart($('.share-development')[0]);

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


    function drawChart($chart, baseData) {

        var data = google.visualization.arrayToDataTable(baseData);
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
            backgroundColor: 'transparent',
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

        $chart.draw(data, options);
    }


    google.setOnLoadCallback(drawChart(shareDevelopment, arr));





    var currentPosition = 1;
    var contentHeight = $('#market .col-lg #daily-winners .clean-content').height() + 100;
    var rollCount = $('#market .col-lg #daily-winners .clean-content').size();
    var rollSpeed = 8000;


    var runningRoll = setTimeout(blockRoll, rollSpeed);


    function blockRoll() {

        if (currentPosition != rollCount) {

            $('#market .col-lg #daily-winners .clean-content').stop(true, true).animate({ top: '-=' + contentHeight + 'px' }, 1500, 'easeOutExpo');
            currentPosition++;
            runningRoll = setTimeout(blockRoll, rollSpeed);

        } else {

            $('#market .col-lg #daily-winners .clean-content').css({ 'top': '-' + contentHeight + 'px' }).stop(true, true).animate({ top: '0' }, 1500, 'easeOutExpo');
            currentPosition = 1;
            runningRoll = setTimeout(blockRoll, rollSpeed);

        }


    }


    //var listColumns = parseInt($('.market-search-share-list li').length) / 10;

    //$('.market-search-share-list').easyListSplitter({

    //    colNumber: listColumns
    //});

    

    $(this).on('click', '.navigation-item', function () {

        $('.navigation-item').removeClass('active'); 
        var destination = $(this).attr('row-location');
        var rowHeight = $('.sub-section').height() + 72; // Big resolution height

        $(this).addClass('active');

        getShareView(destination, rowHeight); 


    });

    $(this).on('click', '.market-search-share-list li .data', function () {

        $('.navigation-item').removeClass('active');
        $('.market-search-share-list li').removeClass('active');
        var rowHeight = $('.sub-section').height() + 72; // Big resolution height

        $(this).parent().addClass('active');
        $('.navigation-item:last-of-type').addClass('active');

        getShareView('market-share', rowHeight);


    });


    function getShareView($destination, rowHeight) {

        var i = 0;
        $('.sub-section').each(function () {

            var distance = i * rowHeight;

            if ($(this).attr('row-location') == $destination) {



                $('.sub-section').stop().animate({ top: '-' + distance + 'px' }, 500, 'easeOutQuint');

            }

            i++;
        });

    }

});