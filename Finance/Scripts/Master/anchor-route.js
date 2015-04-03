$(document).ready(function() {



    //Style link-elements
    $(this).on('click', '.menu ul li', function() {

        $('.menu ul li').removeClass('active-link');
        $(this).addClass('active-link'); 


    });


    // Scan the posted url for anchor-link
    var initialUrl = window.location.hash.slice(2);

    

    // If anchor-link is not found on page, fallback to this
    var fallBack = 'Overview';

    //Check if any section matches the requested anchor
    function isValidAnchor(url) {

        var isValid = false; 

        $('.anchor-section').each(function () {

            var anchor = $(this).attr('anchor-location');

            if (anchor == url) {
                isValid = true;
            }


        });

        return isValid; 
    };
    

    // If the anchor is matched with a action -> Bring that section, else, fall back to default-view
    if (isValidAnchor(initialUrl)) {
        
        getSection(initialUrl);

    } else {

        window.location.hash = '#/' + fallBack; 
    }



    //Listen for when hash changes, either from click or url
    $(window).bind('hashchange', function () {

        var hash = window.location.hash.slice(2);


        // If the anchor is matched with a action -> Bring that section, else, fall back to default-view
        if (isValidAnchor(hash)) {

            
            getSection(hash);

        } else {

            window.location.hash = '#/' + fallBack;
        }

        // Because scrollbar in some cases are being disabled, when user leaves tab width disabled scrollbar, it must be updated
        $('.horizontal-scroll').mCustomScrollbar("update");

    });

    //Listen for click to change section
    $(document).on('click', 'a[href^="/#/"]', function(e) {

        e.preventDefault(); 
        window.location.hash = $(this).attr('href');
    });




    function getSection(request) {

        $('.anchor-section').css({ 'display': 'none', 'opacity': '', 'top': '' });

        $('.anchor-section').each(function() {

            var anchor = $(this).attr('anchor-location'); 

            if (anchor == request) {

                $(this).css('display', 'initial');
                $(this).animate({ opacity: 1.0, top: '0px' }, 500);

                pageTitle($(this));
                return false;
            }


        });
    };

    //Untested
    function pageTitle($element) {

        var title = $element.attr('anchor-title');
        document.title = document.title.split('-')[0]; 
        document.title += ' - ' + title; 
    }


});