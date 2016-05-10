
(function () {
    $.fn.smint = function (options) {
        $(this).addClass('smint');
        var settings = $.extend({
            'scrollSpeed ': 500
        }, options);
        return $('.smint a').each(function () {
            if (settings.scrollSpeed) {
                var scrollSpeed = settings.scrollSpeed
            }

            var stickyTop = $('.smint').offset().top; //取的$('.smint')到顶部的距离
            var stickyMenu = function () {
                var scrollTop = $(window).scrollTop();   //取到当前滚动条的距离
                if (scrollTop > stickyTop) {    //如果滚动条的距离大于$('.smint')的距离
                    $('.smint').css({ 'position': 'fixed', 'top': 0 });
                } else {
                    $('.smint').css({ 'position': 'absolute', 'top': stickyTop });
                }
            };
            stickyMenu();
            $(window).scroll(function () {
                stickyMenu();
            });
            $(this).on('click', function (e) {
                var selectorHeight = $('.smint').height();
                e.preventDefault();
                var id = $(this).attr('id');
                var goTo = $('div.' + id).offset().top - selectorHeight;
                $("html, body").animate({ scrollTop: goTo }, scrollSpeed);

            });
        });

    }

})();

$(function () {

    var ks = window.location.href;
    var ix = ks.indexOf('#')

    if (ix > -1) {
        var id = ks.slice(ix + 1);
        $('#'+id).addClass('cur').siblings().removeClass('cur');
        var goTo = $('div.' + id).offset().top - 55 + 'px';
        $("html, body").animate({ scrollTop: goTo }, 500);
    }
  

    //页面锚点滚动效果
    $('.subMenu').smint({
        'scrollSpeed': 500
    });
    //点击锚点文字变色效果
    $('.subMenu a').not('.subNav').click(function () {
        $(this).addClass('cur').siblings().removeClass('cur');

    });

    $('.subMenu a.subNav').click(function () {
        $('#s1').addClass('cur').siblings().removeClass('cur');

    });


});
