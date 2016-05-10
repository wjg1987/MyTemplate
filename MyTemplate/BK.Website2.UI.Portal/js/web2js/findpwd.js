$(function () {
    $('#getpassword').css({
        'left': $(window).width() / 2 - 490 + 'px',
        'top': $(document).scrollTop() + $(window).height() / 2 - 250 + 'px',
        'display': 'block'
    });


    $(window).resize(function () {

        $('#getpassword').css({
            'left': $(window).width() / 2 - 490 + 'px',
            'top': $(document).scrollTop() + $(window).height() / 2 - 250 + 'px'
        });
    });

    $('#GetForm input').keydown(function (event) {
        if (event.keyCode == 32) {
            return false;
        }
    });

    $("#sub").click(function () {
        var phone = $("#phone").val();
        var reg = /^1[3|4|5|8][0-9]\d{4,8}$/;

        if (!reg.test(phone)) {
            $("#phoneError").css('visibility', 'visible').text("请输入正确的手机号码");;
            return;
        }
        else {
            $("#phoneError").css('visibility', 'hidden').text("");

        }
        var checkcode = $("#checkcode").val();
        if (checkcode.length == 0) {
            $("#checkcodeError").css('visibility', 'visible').text("请输入验证码");
            return;
        }
        else {

        }
        $("#GetForm").submit();
    });

    $("#checkImg").click(function () {
            var Mydate = new Date();
            $(this).attr("src", "/Login/GetCheckCodeImg?id=" + Mydate.getTime());
    }).attr("src", "/Login/GetCheckCodeImg");

});