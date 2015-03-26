$(document).ready(function() {


    // Animate right border on click

    $(this).on('click', '.control-panel .column ul li', function() {


        $(this).parent().find('li').each(function() {

            $(this).css({ 'border': 'none', 'background' : '' }); 

        });

        $(this).css({ 'border-right' : '3px solid #FFF', 'background' : '#666' }); 


    });

    // Bring correct column to show

    $(this).on('click', '.control-panel .column ul li', function () {

        var destination = $(this).attr('data-href');
        var found = false; 
        
        

        $('.control-panel .column').each(function() {

            
            var level = parseInt($(this).attr('value'));
            var distances = [];

            

            if (level == 2) {
                distances = ['220px', '250px'];
            }
            if (level == 3) {
                distances = ['470px', '500px'];
            }

            if ($(this).attr('data-target') == destination) {

                found = true; 
                var correctTab = $(this); 
               
                if (level == 2) {

                    $('.control-panel .level-3').stop().animate({ opacity: 0.0, left: '500px' }, 250, 'easeOutQuint');
                    $('.control-panel .level-2').each(function() {

                        $(this).find('ul li').css({ 'border': 'none', 'background' : '' }); 

                    });

                }

                $('.control-panel .level-' + level).stop().animate({ opacity: 0.0, left: distances[0] }, 250, 'easeOutQuint', function () {

                    $(this).css({ 'display': 'none' });

                    

                    correctTab.css({ 'display': 'initial', 'left': distances[0] });
                    correctTab.stop().animate({ opacity: 1.0, left: distances[1] }, 500, 'easeOutQuint');

                    

                    
                });

                return true;
 
            }

        });

        if (!found) {
            


        }

    });

});