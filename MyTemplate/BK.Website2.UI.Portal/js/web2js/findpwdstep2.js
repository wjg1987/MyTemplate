//新js
$(function () {
    $('#password').css({
        'left': $(window).width() / 2 - 490 + 'px',
        'top': $(document).scrollTop() + $(window).height() / 2 - 250 + 'px',
        'display': 'block'
    });


    $(window).resize(function () {

        $('#password').css({
            'left': $(window).width() / 2 - 490 + 'px',
            'top': $(document).scrollTop() + $(window).height() / 2 - 250 + 'px'
        });
    });

    $('#checkForm input').keydown(function (event) {
        if (event.keyCode == 32) {
            return false;
        }
    });

    $("#getcheckcode").click(function () {


        $(this).attr('disabled', 'disabled');
        $(this).css('background-color', '#cecece');
        changenum_main();
        var phone = $("#phone").val();

        $.ajax({
            url: "/API/ShortMsg/SendShortMsg4Findpwd",
            data: { mobile: phone },
            type: "post",
            datatype: "text",
            success: function (result) {
                if (result == "0") {

                }
                else {
                    alert('错误' + result);
                }
            },
            error: function () {
                alert("异步失败");
            }
        });
    });
    $("#sub").click(function () {
        var checkcode = $("#checkcode").val();
        var getcheckcodeBtn = $("#getcheckcode").val();
        if (getcheckcodeBtn == "获取验证码") {
            $(".p4").css("visibility", "visible").text("请先获取验证码");
            return;
        }
        else {
            $(".p4").css("visibility", "hidden");
        }
        if (checkcode.length == 0) {
            $(".p4").css("visibility", "visible").text("请输入验证码");
            return;
        }
        $("#checkForm").submit();
    });


});

var curnum = 60;
function changenum_main() {
    var timerid = setInterval(changenum, 1000);
}
function changenum() {
    var el = $("#getcheckcode");
    if (curnum >= 1) {
        curnum--;
        el.val(curnum + '秒');
    } else {

        el.val('获取验证码');
        clearInterval(timerid);
        curnum = 60;
        el.css('background-color', '#ffe198');
        el.attr('disabled', false);

    }
}
