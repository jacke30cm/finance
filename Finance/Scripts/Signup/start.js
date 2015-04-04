$(document).ready(function() {

    $('#terms-and-conditions').labelauty({
        development: false,
        class: "labelauty",
        label: true,
        separator: "|",
        checked_label: "Checked",
        unchecked_label: "Unchecked",
        minimum_width: 'calc(100% - 8px)',
        same_width: false
    });


    $(document).on('change', '#terms-and-conditions', function() {

        if (this.checked) {

            $('#sign-up').removeClass('disabled');
  

        } else {
            
            $('#sign-up').addClass('disabled');
            

        }
    }); 


    $(document).on('click', '#sign-up', function () {


        if ($(this).hasClass('disabled')) {

            return false;

        } else {

            $(this).find('p').remove();
            var spinner = new Spinner(smallSpinOptions()).spin($(this)[0]);


            var model = {

                FirstName: $('#sign-up-first-name').val(),
                LastName: $('#sign-up-last-name').val(),
                City: $('#sign-up-city').val(),
                Email: $('#sign-up-email').val(),
                Password: $('#sign-up-password1').val(),
                ConfirmPassword: $('#sign-up-password2').val()

            }; 

            //Ajax sign-in-call
            $.ajax({
                url: '/Account/Register',
                type: 'POST',
                data: { 'model': model },
                success: function (data) {

                    if (data == 'Success') {

                        window.location = '/Home/'; 

                    } else {
                        
                        ajaxFailureResponse($('#sign-up'));

                    }

                },
                error: function (jqXHR, exception) {
                    alert('Error passing data to server.');
                }

            });


        }

    });

    $(document).on('click', '#sign-in', function (e) {

        e.preventDefault();

        $(this).find('p').remove();
        var spinner = new Spinner(smallSpinOptions()).spin($(this)[0]);


        var model = {

            Email : $('#sign-in-email').val(),
            Password : $('#sign-in-password').val(),
            RememberMe: false
        };

        var returnUrl = $('.right-block').attr('value');

        //Ajax sign-in-call
        $.ajax({
            url: '/Account/Login',
            type: 'POST',
            data: { 'model': model, 'returnUrl' : returnUrl },
            success: function (data) {

                if (data == 'Success') {

                    window.location = '/Home/'; 

                } else {

                    spinner.stop();
                    ajaxFailureResponse($('#sign-in')); 

                }

            },
            error: function (jqXHR, exception) {
                alert('Ajax-call, or server-code has failed');
            }

        });
    });


    function ajaxFailureResponse($element) {
        
        $($element).removeClass('-green');
        $($element).addClass('-red');
        $($element).append('<p class="color-white -capitalize -mini"> Misslyckades </p>');

        $($element).animate({ top: '0px' }, 2000, function () {

            $($element).addClass('-green');
            $($element).removeClass('-red');
            $($element).find('p').text('Logga in');

        });

    }

}); 