$(document).ready(function() {

    // Check all empty fields
    $(this).on('focus', '.validation-empty', function () {


        $(this).removeClass('field-error');

    });


    $(this).on('focusout', '.validation-empty', function() {

        alert(); 
        $(this).addClass('field-error'); 

    });

    // Check all fields with email-requirements
    $(this).on('focus', '.validation-email', function () {

        $(this).removeClass('-red');

    });

    $(this).on('focusout', '.validation-email', function () {

        $(this).addClass('-red');

    });

    // Check all fields with number- limitations
    $(this).on('focus', '.validation-numbers', function () {

        $(this).removeClass('-red');

    });

    $(this).on('focusout', '.validation-empty', function () {

        $(this).addClass('-red');

    });
}); 