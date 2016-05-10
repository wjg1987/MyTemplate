$(function () {


    $('#LoginForm input').keydown(function (event) {
        if (event.keyCode == 32) {
            return false;
        }
    });

    $('#denglu').css({
        'left': $(window).width() / 2 - 490 + 'px',
        'top': $(document).scrollTop() + $(window).height() / 2 - 250 + 'px',
        'display':'block'
    });


    $(window).resize(function () {

        $('#denglu').css({
            'left': $(window).width() / 2 - 490 + 'px',
            'top': $(document).scrollTop() + $(window).height() / 2 - 250 + 'px'
        });
    });

    $('#login_btn').click(function () {
        var pwd = $('#Pwd').val();
        var acc = $('#Account').val();
        var phone = $("#Account").val();
        var reg = /^1[3|4|5|8][0-9]{9}$/;

        if (!reg.test(phone)) {
            $(".p1").css('visibility', 'visible');
            return false;
        }
        if (reg.test(phone)) {
            $(".p1").css('visibility', 'hidden');
        }
        if (pwd.length <= 0 || acc.length <= 0) {
            $('#ErrorMsg').css('visibility', 'visible');
            $('#ErrorMsg').css('color', 'red');
            $('#ErrorMsg').text('用户名或者密码不能为空.');
            return false;
        } else {
            $('#LoginForm').submit();
        }
    });


    $('body').keydown(function () {    
        if (event.keyCode == 13)//按Enter键的键值为13 
        {

            $('#login_btn').click();
        }
    })
});
