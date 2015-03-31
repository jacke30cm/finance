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
            mouseWheel: { scrollAmount: 300 },
            keyboard: { scrollAmount: 15 },
            scrollInertia: 800,
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


    //Input- focus- actions

    $('input[type="text"]').on('keyup', function(e) {

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

    
});