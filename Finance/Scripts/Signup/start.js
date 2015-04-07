$(document).ready(function () {

    // Checkbox styling
    $('#terms-and-conditions').labelauty({
        development: false,
        class: "labelauty",
        label: true,
        separator: "|",
        minimum_width: 'calc(100% - 8px)',
        same_width: false
    });


    //Validaton-check if all fields are valid 
    function signUpValidation() {

        var isValid = true;

        $('#sign-up-form input').each(function () {

            if ($(this).is(':checkbox')) {

                // If false, (not checked) 
                if (!$(this).is(':checked')) {

                    isValid = false;
                }

            }
            // Validation-method uses html-attribute "validation" to see if element is good or not 
            // The value-assignment is handled by "validation.js"
            if ($(this).attr('validation') == 'false') {

                isValid = false;

            }


        });

        return isValid;
    }



    // SIGN UP COMMAND
    $(document).on('click', '#sign-up', function (e) {

        e.preventDefault();
        if (signUpValidation()) {

            $(this).find('p').hide();
            var spinner = new Spinner(smallSpinOptions('#FFF')).spin($(this)[0]);


            var model = {

                FirstName: $('#sign-up-first-name').val(),
                LastName: $('#sign-up-last-name').val(),
                City: $('#sign-up-city').val(),
                Email: $('#sign-up-email').val(),
                Password: $('#sign-up-password1').val(),
                ConfirmPassword: $('#sign-up-password2').val()

            };

            //Ajax sign-up-call
            $.ajax({
                url: '/Account/Register',
                type: 'POST',
                data: { 'model': model },
                success: function (data) {

                    if (data != 'Failure') {

                        window.location = '/Home/';

                    } else {

                        ajaxFailureResponse($('#sign-up'));

                    }

                },
                error: function (jqXHR, exception) {
                    alert('Error passing data to server.');
                },
                complete: function () {

                    spinner.stop();

                }


            });


        } else {
            
            ajaxFailureResponse($('#sign-up'));

        }

    });



    // LOG IN COMMAND
    $(document).on('click', '#sign-in', function (e) {

        e.preventDefault();

        if ($('#sign-in-email').val() != '' && $('#sign-in-password').val() != '') {


            $(this).find('p').hide();
            var spinner = new Spinner(smallSpinOptions('#FFF')).spin($(this)[0]);


            var model = {
                Email: $('#sign-in-email').val(),
                Password: $('#sign-in-password').val(),
                RememberMe: false
            };

            var returnUrl = $('.right-block').attr('value');

            //Ajax sign-in-call
            $.ajax({
                url: '/Account/Login',
                type: 'POST',
                data: { 'model': model, 'returnUrl': returnUrl },
                success: function(data) {

                    // Om ajax-resultatet som kommer tillbaka är returnURL:en, så är det success
                    if (data != 'Failure') {

                        window.location = data;

                    } else {

                        ajaxFailureResponse($('#sign-in'));

                    }

                },
                error: function(jqXHR, exception) {
                    alert('Ajax-call, or server-code has failed');
                },
                complete: function() {

                    spinner.stop();

                }


            });
        } else {

            ajaxFailureResponse($('#sign-in')); 

        }

    });

    // Animate the button that was clicked, to show error-response
    function ajaxFailureResponse($element) {

        $($element).find('p').show();
        $element.removeClass('-green');
        $element.addClass('-red');
        //$element.append('<p class="color-white -capitalize -mini"> Misslyckades </p>');

        $element.find('p').text('Misslyckades'); 

        $element.animate({ top: '0px' }, 2000, function () {

            $element.addClass('-green');
            $element.removeClass('-red');
            $element.find('p').text($element.find('p').attr('value'));

        });

    }

});