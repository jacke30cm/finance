(function ($) {
    $(window).load(function () {



        $('.horizontal-scroll').mCustomScrollbar({
            axis: "x",
            theme: "light",
            autoDraggerLength: true,
            autoHideScrollbar: true,
            scrollbarPosition: "outside",
            alwaysShowScrollbar: 0,
            advanced: { autoExpandHorizontalScroll: true },
            mouseWheel: { scrollAmount: 500 },
            keyboard: { scrollAmount: 15 },
            scrollInertia: 700,
            callbacks: {
                //onTotalScroll: function() {

                //    $('.right-ghost').animate({ opacity: 0.0 }, 1000); 

                //},
                onTotalScrollBack: function () {

                    $('.market-navigator').stop().animate({ opacity: '1.0' }, 500);

                },
                onScrollStart: function () {

                    $('.market-navigator').stop().animate({ opacity: '0.0' }, 500);
                    //$('.right-ghost').animate({ opacity: 1.0 }, 200);
                }

            }
        });


        $('.vertical-scroll').mCustomScrollbar({
            axis: "y",
            theme: "light",
            autoDraggerLength: true,
            autoHideScrollbar: true,
            scrollbarPosition: "outside",
            alwaysShowScrollbar: 0,
            advanced: { autoExpandHorizontalScroll: true },
            mouseWheel: { scrollAmount: 150 },
            keyboard: { scrollAmount: 15 },
            scrollInertia: 400,
        });

    });
})(jQuery);

$(document).ready(function () {

    //Moment language must be set
    moment.locale('sv');

    //Input- focus- actions

    $('input[type="text"]').on('keyup', function (e) {

        if (e.keyCode == 27) {

            $(this).val('');

        }

    });


    // Fancier checkboxes
    $('.fancy-checkbox').labelauty({
        development: false,
        class: "labelauty",
        label: false,
        separator: "|",
        checked_label: "Checked",
        unchecked_label: "Unchecked",
        minimum_width: false,
        same_width: true
    });

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


    // User being able to sign out
    $(this).on('click', '#sign-out', function (e) {

        e.preventDefault();

        //Ajax sign-in-call
        $.ajax({
            url: '/Account/LogOff',
            type: 'POST',
            success: function () {

                window.location = '/Home/';
            },
            error: function (jqXHR, exception) {
                alert('Error passing data to server.');
            }

        });



    });


    // Drop-down-listeners
    $(document).on('focus', '.input-wrap input', function () {

        $(this).siblings('.drop-down').css({

            'display': 'initial',

        }).stop().animate({opacity: 1.0}, 500, 'easeOutQuint');

    });


    $(document).on('focusout', '.input-wrap input', function () {

        $(this).siblings('.drop-down').stop().animate({ opacity: 0.0 }, 500, 'easeOutQuint', function() {

            $(this).css('display', 'none'); 

        });

    });

    //Disable real input and force user to choose from dropdown alternatives
    $(document).on('keydown', '.input-wrap .no-typing', function (e) {

        e.preventDefault();
        return false; 

    });

    $(document).on('click', '.input-wrap .drop-down .item', function (e) {

        var value = $(this).find('p').text();
        $(this).parent().siblings('input').val(value); 

    });




    //Sections in market and details
    $(this).on('click', '.navigation-item', function () {

        $('.navigation-item').removeClass('active');
        var destination = $(this).attr('row-location');
        var rowHeight = $(this).parent().parent().find('.sub-section').height() + 72; // Big resolution height

        $(this).addClass('active');

        getSectionView($(this), destination, rowHeight);


    });

    function getSectionView($section, $destination, rowHeight) {

        var i = 0;
        $section.closest('.anchor-section').find('.sub-section').each(function () {

            var distance = i * rowHeight;

            if ($(this).attr('row-location') == $destination) {

                $section.closest('.anchor-section').find('.sub-section').stop().animate({ top: '-' + distance + 'px' }, 500, 'easeOutQuint');

                if ($destination == 'market-search') {

                    showShareFilter(); 
                } else {

                    hideShareFilter(); 
                }

            }

            i++;
        });

    }

    // Bring search-area
    function showShareFilter() {

        $('#search-control').animate({ bottom: '0%' }, 500, 'easeOutQuint');

    }

    // Hide search-area
    function hideShareFilter() {

        $('#search-control').animate({ bottom: '-50%' }, 500, 'easeOutQuint');

    }
});