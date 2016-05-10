$(function() {
    
    $('#zhuce').css({
        'left': $(window).width() / 2 - 490 + 'px',
        'top': $(document).scrollTop() + $(window).height() / 2 - 250 + 'px',
        'display': 'block'
    });



    $(window).resize(function () {

        $('#zhuce').css({
            'left': $(window).width() / 2 - 490 + 'px',
            'top': $(document).scrollTop() + $(window).height() / 2 - 250 + 'px'
        });


    });


    $('#zhuce .zhuce_right .for_zhuce input').keydown(function (event) {
        if (event.keyCode == 32) {
            return false;
        }
    });


    $('#passwordfirst').keyup(function () {
        checkcuowu(this, '#passwordagain');

    });

    $('#passwordagain').keyup(function () {

        checkcuowu('#passwordfirst', this);

    });
    //给获取验证码按钮加事件
    $("#huoqu").click(function () {
        var phone = $("#phone").val();
        var reg = /^1[3|4|5|8][0-9]{9}$/;

        if (!reg.test(phone)) {
            $(".phoneError").css('visibility', 'visible');
            return;
        } 
        if (reg.test(phone)) {
            $(".phoneError").css('visibility', 'hidden');
        }
        var passwordfirst = $('#passwordfirst').val();
        var passwordagain = $('#passwordagain').val();
        if (passwordfirst.length == 0) {
            $("#Pwdcuowu").css('visibility', 'visible');
            return;
        }
        else if (passwordfirst.length > 0) {
            $("#Pwdcuowu").css('visibility', 'hidden');
        }

        if (passwordagain.length == 0) {
            $("#aginPwdcuowu").css('visibility', 'visible').text("请输入确认密码");
            return;
        }
        else if (passwordagain.length > 0) {
            $("#aginPwdcuowu").css('visibility', 'hidden');
        }


        if (passwordfirst != passwordagain) {
            $("#aginPwdcuowu").css('visibility', 'visible').text(" 两次输入的密码不一致，请重新输入");
            return;
        }
        else if (passwordfirst == passwordagain) {
            $("#aginPwdcuowu").css('visibility', 'hidden');
        }


        $(this).attr('disabled', 'disabled');
        $(this).css('background-color', '#cecece');
        changenum_main();

        $.ajax({
            url: '/API/ShortMsg/GetValidateCode',
            data: { mobile: phone, type: 1 },
            type: 'get',
            dataType: 'text',
            success: function (data) {
                if (data === '0') {
                    alert('验证码已发送.');
                } else {
                    alert('错误' + data);
                }
            },
            error: function () { alert('异步出错.') }
        })
    });

    //给注册按钮注册事件
    $("#submit").click(
        function () {
            var phone = $("#phone").val();
            var reg = /^1[3|4|5|8][0-9]{9}$/;

            if (!reg.test(phone)) {
                $(".phoneError").css('visibility', 'visible');
                return;
            }
            if (reg.test(phone)) {
                $(".phoneError").css('visibility', 'hidden');
            }
            var passwordfirst = $('#passwordfirst').val();
            var passwordagain = $('#passwordagain').val();
            if (passwordfirst.length == 0) {
                $("#Pwdcuowu").css('visibility', 'visible');
                return;
            }
            else if (passwordfirst.length > 0) {
                $("#Pwdcuowu").css('visibility', 'hidden');
            }
            if (passwordagain.length == 0) {
                $("#aginPwdcuowu").css('visibility', 'visible').text("请输入确认密码");
                return;
            }
            else if (passwordagain.length > 0) {
                $("#aginPwdcuowu").css('visibility', 'hidden');
            }
            if (passwordfirst != passwordagain) {
                $("#aginPwdcuowu").css('visibility', 'visible').text(" 两次输入的密码不一致，请重新输入");
                return;
            }
            else if (passwordfirst == passwordagain) {
                $("#aginPwdcuowu").css('visibility', 'hidden');
            }

            var checkcode = $("#checkcode").val();
            if (checkcode.length == 0) {
                $("#checkcodeError").css('visibility', 'visible').text("请输入验证码");
                return;
            }
            else {
                $("#checkcodeError").css('visibility', 'hidden');
            }

            $("#zhuceFrom").submit();
        });
})




var curnum = 60;
var timerid;
function changenum_main() {
     timerid = setInterval(changenum, 1000);
}

function changenum() {
    var el = $("#huoqu");
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

function checkcuowu(s1, s2) {
    var passwordfirst = $(s1).val();
    var passwordagain = $(s2).val();
    if (passwordfirst.length != 0 && passwordagain.length != 0) {

        if (passwordfirst != passwordagain) {
            $('#cuowu').addClass('cur');
        }
        else {
            $('#cuowu').removeClass('cur');
        }

    }

}