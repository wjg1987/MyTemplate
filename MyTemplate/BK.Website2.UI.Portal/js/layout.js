

$(function () {





    //我的页面的左边的下拉效果
    $('.my_list .listtop').click(function () {
        $(this).find('.pics .zhankai').toggleClass('cur').end().find('.pics .shousuo').toggleClass('cur').end().parent().find('dl').slideToggle();
    });




})