$(document).ready(function () {

    // Style checkboxes
    $('#visible-portfolios').labelauty({
        development: false,
        class: 'labelauty',
        label: true,
        separator: '|',
        minimum_width: '100%',
        same_width: false,
        force_random_id: true
    });

    $('#visible-score-list').labelauty({
        development: false,
        class: 'labelauty',
        label: true,
        separator: '|',
        minimum_width: '100%',
        same_width: false,
        force_random_id: true
    });




    //Bring competition-control-panel
    $(this).on('click', '.user-panel .drop-down #create-competition', function (e) {

        e.preventDefault();
        showUp();
    });


    //Bring share-control-panel
    $(this).on('click', '.share-control-action', function (e) {

        e.preventDefault();
        showUp();
    });

    // Click, and button-actions
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


    // Bring control panel
    function showUp() {

        $('.control-panel-overlay').css('opacity', '0.0');
        $('.control-panel-overlay').css('display', 'initial');

        $('.control-panel-overlay').animate({ opacity: 0.7 }, 500);

        $('.control-panel').animate({ bottom: '0%' }, 500, 'easeOutQuint');

    }

    // Close control panel
    function closeDown() {

        $('.control-panel-overlay').animate({ opacity: 0.0 }, 500, function () {
            $(this).css('display', 'none');
        });

        $('.control-panel').animate({ bottom: '-50%' }, 500, 'easeOutQuint');
    }


    // Animate right border on click

    $(this).on('click', '.control-panel .column ul li', function () {


        $(this).parent().find('li').each(function () {

            $(this).css({ 'border': 'none', 'background': '' });

        });

        $(this).css({ 'border-right': '3px solid #FFF', 'background': '#666', 'width': '247px' });


    });



    $(this).on('mouseenter', '.control-panel ul li', function () {

        var description = $(this).attr('data-description');

        $('.control-panel .explanatory-column p').animate({ opacity: '0.0' }, 100, function () {

            $(this).text(description);

            $(this).animate({ opacity: '1.0' }, 100);

        });

    });

    //var initialText = $('.control-panel .explanatory-column p').text();

    //$(this).on('mouseleave', '.control-panel ul li', function () {


    //    $('.control-panel .explanatory-column p').animate({ opacity: '0.0' }, 100, function () {

    //        $(this).text(initialText);

    //        $(this).animate({ opacity: '1.0' }, 100);

    //    });

    //});

    // Bring correct column to show

    $(this).on('click', '.control-panel .column ul li', function () {

        var destination = $(this).attr('data-href');

        $('.control-panel .column').each(function () {


            var level = parseInt($(this).attr('value'));
            var distances = [];



            if (level == 2) {
                distances = [220, 250];
            }
            if (level == 3) {
                distances = [470, 500];
            }

            if ($(this).attr('data-target') == destination) {

                var correctTab = $(this);

                if (level == 2) {

                    $('.control-panel .level-3').stop().animate({ opacity: 0.0, left: '500px' }, 250, 'easeOutQuint');


                    $('.control-panel .level-2').each(function () {

                        $(this).find('ul li').css({ 'border': 'none', 'background': '' });

                    });

                }


                $('.control-panel .level-' + level).stop().animate({ opacity: 0.0, left: distances[0] }, 250, 'easeOutQuint', function () {

                    $(this).css({ 'display': 'none' });

                    var explanatoryColumn = distances[1] + 400;

                    correctTab.css({ 'display': 'initial', 'left': distances[0] + 'px' });
                    correctTab.stop().animate({ opacity: 1.0, left: distances[1] + 'px' }, 500, 'easeOutQuint');


                });

                return true;

            }

        });


    });



    // Create new constest-call

    $(document).on('click', '#create-new-contest', function () {

        // if all is valid
        
        $('#home-my-contests').prepend('<div class="block-m"><div class="competition"></div></div>');
        
        //var spinner = new Spinner(smallSpinOptions('#444')).spin($('#home-my-contests .block-m:first-of-type')[0]);
        closeDown();

        var image = new FormData();

        $.each($('#upload-contest-image')[0].files, function (i, file) {

            image.append('image', file);

        });
        

        var model = {
            ContestType: 'private',
            Name: 'Babbens livs',
            Length: '2015-05-05',
            InvestmentSize: 1000,
            VisiblePortfolios: false,
            VisibleScores: true
        };

        // Create-contest-call
        $.ajax({
            url: '/Master/CreateContest',
            type: 'POST',
            processData: false,
            data: { 'model': model, 'image': image },
            success: function () {

            },
            error: function (jqXHR, exception) {
                alert('Error passing data to server.');
            }
        });



        //$('#home-my-contests').animate({ top: 0 }, 2000, function () {

            
        //    $('#home-my-contests .block-m:first-of-type').find('.competition').addClass('-white').css({ 'top': '-30px', 'opacity': '0.0', 'margin-right' : '5px' }).animate({ top: '0', opacity: 1.0 }, 500, 'easeOutQuint', function() {
        //    });
            
        //    spinner.stop();
        //});


    });


});