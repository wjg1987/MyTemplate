$(function () {

    //-----------登录 注册 图片滑动效果开始-----------
    //背景滚动效果
    var index_div_pro = { sx: 0, sy: 0, mw: 3, mh: 1, bx: 8.4, by: 10.4, rx: -0.6 };
    var ePageX = null;
    var ePageY = null;
    function getMousePos(expression) {
        if (ePageX == null || ePageY == null) return null;
        var _x = $(expression).position().left;
        _x += ePageX - $(expression).parent().position().left;
        var _y = $(expression).position().top;
        _y += ePageY - $(expression).parent().position().top;
        return {
            x: _x,
            y: _y
        }
    };
    var index_xh = setInterval(function () {
        var mousepos = getMousePos("#indexg0");
        if (mousepos != null) {
            var left = parseInt($("#indexg0").css("left"));
            var l = left + (index_div_pro.sx + index_div_pro.mw - (mousepos.x - 2000) / index_div_pro.bx * index_div_pro.rx - left) * 0.02;
            var top = parseInt($("#indexg0").css("top"));
            var t = top + (index_div_pro.sy + index_div_pro.mh - (mousepos.y + 100) / index_div_pro.by - top) * 0.01;
            $("#indexg0").css({
                left: l,
                top: t
            });
        }
    },
    15);

    $("body").mousemove(function (event) {
        event = event || window.event;
        ePageX = event.pageX;
        ePageY = event.pageY;
    });
    //--------------------登录 注册 图片滑动效果结束---------------

})