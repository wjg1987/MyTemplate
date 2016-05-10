﻿$(function () {
    $('#newpassword').keydown(function (event) {
        if (event.keyCode == 32) {
            return false;
        }
    });

    $('#newpassword').keyup(function (event) {

        var strongRegex = new RegExp("^(?=.{8,})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).*|(?=.{8,})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])|(?=.{8,})(?=.*[a-z])(?=.*[0-9])(?=.*\\W).*$", "g");
        var mediumRegex = new RegExp("^(?=.{7,})(((?=.*[A-Z])(?=.*[a-z]))|((?=.*[A-Z])(?=.*[0-9]))|((?=.*[a-z])(?=.*[0-9]))).*$", "g");
        var enoughRegex = new RegExp("(?=.{1,6}).*", "g");
        if ($(this).val().length <= 0) {
            $('#level').removeClass('pw-weak');
            $('#level').removeClass('pw-medium');
            $('#level').removeClass('pw-strong');
            $('#level').addClass(' pw-defule');
        } else if (false == enoughRegex.test($(this).val())) {

            //密码小于六位的时候，密码强度图片都为灰色 
            $('#level').removeClass('pw-weak');
            $('#level').removeClass('pw-medium');
            $('#level').removeClass('pw-strong');
            $('#level').addClass('pw-weak');
        }
        else if (strongRegex.test($(this).val())) {
            $('#level').removeClass('pw-weak');
            $('#level').removeClass('pw-medium');
            $('#level').removeClass('pw-strong');
            $('#level').addClass(' pw-strong');
            //密码为八位及以上并且字母数字特殊字符三项都包括,强度最强 
        }
        else if (mediumRegex.test($(this).val())) {
            $('#level').removeClass('pw-weak');
            $('#level').removeClass('pw-medium');
            $('#level').removeClass('pw-strong');
            $('#level').addClass(' pw-medium');
            //密码为七位及以上并且字母、数字、特殊字符三项中有两项，强度是中等 
        }
        else {
            $('#level').removeClass('pw-weak');
            $('#level').removeClass('pw-medium');
            $('#level').removeClass('pw-strong');
            $('#level').addClass('pw-weak');


            //如果密码为6为及以下，就算字母、数字、特殊字符三项都包括，强度也是弱的 
        }
        return true;
    });


    $('#againpassword').keydown(function (event) {
        if (event.keyCode == 32) {
            return false;
        }
    });

    $('#againpassword').keyup(function () {
        var ka = $(this).val();
        var kb = $('#newpassword').val();
        if (ka === kb) {
            $('.get03 .p2').css('visibility', 'hidden');
        }
        else {
            $('.get03 .p2').css('visibility', 'visible').text("两次输入密码不一致");

        }

    });





    $('#set_password').css({
        'left': $(window).width() / 2 - 490 + 'px',
        'top': $(document).scrollTop() + $(window).height() / 2 - 250 + 'px',
        'display': 'block'
    });


    $(window).resize(function () {

        $('#set_password').css({
            'left': $(window).width() / 2 - 490 + 'px',
            'top': $(document).scrollTop() + $(window).height() / 2 - 250 + 'px'
        });
    });


});
