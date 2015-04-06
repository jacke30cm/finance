$(document).ready(function () {

    // If input classes requires validation
    if ($('input').hasClass('validation')) {


        // Trigger validation when user leaves focus 
        $(document).on('focusout', '.validation', function () {

            var object = $(this);



            var validationTypes = object.attr('class').split(' ');

            $.each(validationTypes, function (i, className) {


                if (className == '-empty') {

                    return isEmpty(object);
                }

                if (className == '-email') {

                    return isEmail(object);

                }

                if (className == '-email-availability') {

                    return isAvailableEmail(object);

                }

                if (className == '-numbers') {

                    return isNumber(object);
                }

                if (className == '-password-relation') {

                    return isPassword(object);

                }

            });


        });


        function isEmpty($element) {

            if ($element.val().length == 0) {

                errorStyle($element, 'Fältet kan inte lämnas tomt');
                bringToolTip($element);
                return false;
            }

            return true;
        };

        function isEmail($element) {

            var result = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

            if (!result.test($element.val())) {

                errorStyle($element, 'En korrekt email måste anges');
                bringToolTip($element);
                return false;
            }

            return true;
        };

        function isAvailableEmail($element) {

            var result = false;

            $.ajax({
                url: '/Master/EmailAvailability',
                type: 'GET',
                processData: false,
                async: false,
                data: $element.val(),
                success: function (data) {


                    if (data == 'true') {

                        result = true;

                    } else {

                        result = false;

                    }
                },
                error: function (jqXHR, exception) {
                    alert('Ajax-request failed, or server caused internal error.');
                }
            });

            return result;
        };

        function isNumber($element) {

            var result = /^\d+$/; 

            if (!result.test($element.val())) {

                errorStyle($element, 'Fältet får bara innehålla siffror');
                bringToolTip($element);
                return false;
            }

            return true;
        };

        function isPassword($element) {

            if ($('.-password-relation:first-of-type').val() != $element.val()) {

                errorStyle($element, 'Lösenorden stämmer inte överrens');
                bringToolTip($element);
                return false;

            }

            return true;
        }


      
        // ERROR-HANDLING-METHODS
        // -----------------------------------------------------------------------------

        // Error-style
        function errorStyle($element, error) {

            // The input box itself
            $element.css({
                'border': '1px solid #e6abab',
                'background': '#f4cece'

            });

            var toolTipStyle = $element.attr('tooltip-style');
            writeToolTip($element, error);

            if (toolTipStyle == 'light') {

                // The tooltip
                $element.parent().find('.validation-tool-tip').css({
                    '-webkit-box-shadow': '0px 2px 15px #CCC'
                });
            } else {

                // The tooltip
                $element.parent().find('.validation-tool-tip').css({
                    '-webkit-box-shadow': '0px 2px 15px #444'
                });

                // If dark theme is selected, no need for border, looks bad
                $element.css({
                    'border': ''
                });

            }


            // Set the attribute to fail, so it can be checked later
            $element.attr('validation', 'false');
        }

        function initialState($element) {

            $element.css({
                'border': '',
                'background': ''
            });

            hideToolTip($element);
            $element.attr('validation', 'true');
        }

        function bringToolTip($element) {

            var height = $element.parent().find('.validation-tool-tip').height() + 30;

            $element.parent().find('.validation-tool-tip').stop().animate({ top: '-' + height + 'px', opacity: '1.0' }, 500, 'easeOutQuint');
        }

        function hideToolTip($element) {

            $element.parent().find('.validation-tool-tip').stop().animate({ top: '-30px', opacity: '0.0' }, 500, 'easeOutQuint', function () {

                $(this).remove();

            });

        }

        function writeToolTip($element, error) {

            $element.parent().prepend(
                '<div class="validation-tool-tip">' +
                '<div class="arrow"></div>' +
                '<p class="-mini-compressed color-red">' + error + '</p>' +
                '</div'
            );

        }

        // Clear all errors on focus
        $(document).on('focus', '.validation', function () {


            initialState($(this));

        });

    }





});