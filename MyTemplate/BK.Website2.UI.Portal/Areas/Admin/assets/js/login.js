$(function () {
    
    $('.list-inline li > a').click(function () {
        var activeForm = $(this).attr('href') + ' > form';
        //console.log(activeForm);
        $(activeForm).addClass('magictime swap');
        //set timer to 1 seconds, after that, unload the magic animation
        setTimeout(function () {
            $(activeForm).removeClass('magictime swap');
        }, 1000);
    });

   
    //点击 链接 换验证码
    $('#aVCode').click(function () {
        $('#validateImg').attr('src', $('#validateImg').attr('src') + 1);
    });

    //点击图标 换验证码
    $("#validateImg").click(function() {
        $(this).attr('src', $(this).attr('src') + 1);
    });
});