$(function() {

    //顶部导航 我的 
    $('#my').hover(function () {
        $(this).find('.navwrap').slideToggle();
    });
    $('.twonav dd').hover(function () {
        $(this).find('a').toggleClass('cur');
    });


    //上下滑动 顶部导航和 中间出现的导航变化
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.common_top .top_inner .logo').addClass('none');
            $('.common_top .top_inner .nav_div').addClass('cur');
            $('.common_top').addClass('cur').removeClass('cur2');
            $('.common_top .top_inner a').removeClass('cur');
        }
        else {
            $('.common_top .top_inner .logo').removeClass('none');
            $('.common_top .top_inner .nav_div').removeClass('cur');
            $('.common_top').removeClass('cur').addClass('cur2');
            $('.common_top .top_inner a').addClass('cur');
        }

    });

    //我的 左侧导航
    $('.my_list .listtop').click(function () {
        $(this).find('.pics .zhankai').toggleClass('cur').end().find('.pics .shousuo').toggleClass('cur').end().parent().find('dl').slideToggle();
    });

})