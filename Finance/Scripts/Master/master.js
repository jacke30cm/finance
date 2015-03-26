$(document).ready(function() {


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

        $(this).find('.competition-ghost').stop().animate({ opacity: 0.9 }, 700, 'easeOutQuint'); 

    });

    $(this).on('mouseleave', '.competition', function () {

        $(this).find('.competition-ghost').stop().animate({ opacity: 0.0 }, 700, 'easeOutQuint');

    });

    //Bring controlpanel

    $(this).on('click', '.user-panel .drop-down #create-competition', function () {

        $('.control-panel-overlay').css('opacity', '0.0');
        $('.control-panel-overlay').css('display', 'initial');

        $('.control-panel-overlay').animate({ opacity: 0.7 }, 500);

        $('.control-panel').animate({ bottom: '0px' }, 500, 'easeOutQuint');

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

        $('.control-panel').animate({ bottom: '-40%' }, 500, 'easeOutQuint');
    }
   

}); 