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


        // Write a new div, which will be populated with all new data on the end
        $('#home-my-contests').prepend('<div class="block-m"></div>');


        // Start spin, and close command-control
        var spinner = new Spinner(smallSpinOptions('#444')).spin($('#home-my-contests .block-m:first-of-type')[0]);
        closeDown();

       
        var image = new FormData($('#upload-contest-image')[0]);

        var model = {
            ContestType: 'private',
            Name: 'Babbens livs',
            Description: 'En beskrivning',
            StartDate: '2015-05-05',
            EndDate: '2015-06-05',
            CashLimit: 10000,
            VisiblePortfolios: false,
            VisibleScores: true,

        };

        // Create-contest-call
        // First - try to upload the image that was chosen, because the ajax can't handle both object and image at same time ...
        $.ajax({
            url: '/Master/ContestImage',
            type: 'POST',
            processData: false,
            contentType: false,
            cache: false,
            data: image,
            success: function (data) {

                // If the uploading-process on server was successful, continue with creation of contest-data
                if (data == 'Success') {

                    $.ajax({
                        url: '/Master/CreateContest',
                        type: 'POST',
                        cache: false,
                        data: model,
                        success: function (data) {

                            //If the creation was successful, server return html-string containing markup for new contest    
                            $('#home-my-contests').animate({ top: 0 }, 2000, function () {

                                $(this).find('.block-m:first-of-type').html(data); 
                                $('#home-my-contests .block-m:first-of-type').find('.competition').addClass('-white').css({ 'top': '-30px', 'opacity': '0.0', 'margin-right': '5px' }).animate({ top: '0', opacity: 1.0 }, 500, 'easeOutQuint', function () {
                                });

                                spinner.stop();
                            });



                        },
                        error: function (jqXHR, exception) {
                            alert('Error passing data to server.');
                        }
                    });

                }


            },
            error: function (jqXHR, exception) {
                alert('Error passing data to server.');
            }
        });

    });


    // File upload modification 
    $(document).on('change', '#upload-contest-image :file', function () {

       
        if (this.files[0] != undefined) {

            var fileName = this.files[0].name;
            $(this).parent().siblings('p').text(fileName).removeClass('color-grey');

        } else {

            $(this).parent().siblings('p').text('Klicka för att välja en omslagsbild').addClass('color-grey');

        }




    });


});