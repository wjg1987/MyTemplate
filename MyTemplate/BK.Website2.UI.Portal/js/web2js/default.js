$(function () {
  
 
    
    //获取报名验证码
    $(".yanzhengma").click(function () {
        if ($(this).text() != "获取验证码") {
            return false;
        }

        var phone = $("#dianhua").val();
        var chenhu = $("#chenghu").val();
        var reg = /^1[3|4|5|8][0-9]{9}$/;



        if (chenhu == "") {
            $(".chenghucuowu").css('visibility', 'visible');
            return;
        }
        else {
            $(".chenghucuowu").css('visibility', 'hidden');
        }

        if (!reg.test(phone)) {
            $(".phonecuowu").css('visibility', 'visible');
            return;
        }
        if (reg.test(phone)) {
            $(".phonecuowu").css('visibility', 'hidden');
        }


        $.ajax({
            url: "/API/ShortMsg/SendYuYueCode",
            type: "post",
            data: { "mobile": phone, "stype": "yuyue" },
            datatype: "text",
            success: function (data) {
                if (data == '0') {
                    changenum_main();
                }
            },
            error: function () {
                alert("未知错误，请重试");
            }
        });


    });
    
    //给立刻报名按钮注册事件
    $(".nanti").click(function () {

        var phone = $("#dianhua").val();
        var chenhu = $("#chenghu").val();
        var reg = /^1[3|4|5|8][0-9]{9}$/;



        if (chenhu == "") {
            $(".chenghucuowu").css('visibility', 'visible');
            return;
        }
        else {
            $(".chenghucuowu").css('visibility', 'hidden');
        }

        if (!reg.test(phone)) {
            $(".phonecuowu").css('visibility', 'visible');
            return;
        }
        if (reg.test(phone)) {
            $(".phonecuowu").css('visibility', 'hidden');
        }

        var el = $(".yanzhengma");
        var checkcode = $(".yanzhen").val();
      
        if (el.text() == '获取验证码') {
            $(".checkcuowu").css('visibility', 'visible').text("请先获取验证码");
            return;
        }
     
        if (checkcode.length == 0) {
            $(".checkcuowu").css('visibility', 'visible').text('请输入验证码');
            return;
        }
        else {
            $(".checkcuowu").css('visibility', 'hidden');
        }

        $.ajax({
            url: "/API/GetData/YuYue",
            type: "post",
            data: { "phone": phone, "name": chenhu, "checkcode": checkcode },
            datatype: "text",
            success: function (data) {
               
                if (data == '1') {
                    alert("恭喜您，预约成功");
                }
                else if (data == "0") {
                    alert("未知错误，请重试");
                } else if (data == "-1") {
                    $(".chenghucuowu").css('visibility', 'visible');
                }
                else  if(data == "-2"){
                    $(".phonecuowu").css('visibility', 'visible');
                }else if(data == "-3")
                {
                    $(".checkcuowu").css('visibility', 'visible').text('请输入验证码');
                }
                else if (data == "-4") {
                    $(".checkcuowu").css('visibility', 'visible').text("请先获取验证码");
                }
                else if (data == "-5")
                {
                    $(".checkcuowu").css('visibility', 'visible').text("验证码错误");
                }
                else if (data == "-6") {
                    alert("您已经预约过了");
                } else {
                    $(".checkcuowu").css('visibility', 'visible').text("预约失败，请重试");
                }
            },
            error: function () {
                alert("未知错误，请重试");
            }
        });

    });

    //验证码读秒
    var curnum = 60;
    var timerid;
    function changenum_main() {
        timerid = setInterval(changenum, 1000);
    }

    function changenum() {
        var el = $(".yanzhengma");

        if (curnum >= 1) {
            curnum--;
            el.text(curnum + '秒');
            el.css('background-color', 'rgb(125,125,125)');
            el.attr('disabled', false);
        } else {
            el.text('获取验证码');
            clearInterval(timerid);
            curnum = 60;
            el.css('background-color', 'rgb(206,29,65)');
            el.attr('disabled', true);
        }
    }

    //搜索
    $(".sousuo").click(function () {
        var searchWords = $("#loupan_name").val();
        if (searchWords.length != 0) {
            window.location.href = "/Community/Index?searchWords=" + searchWords;
        }
    });


    //搜索按回车按钮效果
    $('body').keydown(function () {
        if (event.keyCode == 13)//按Enter键的键值为13  
        {
            var searchWords = $("#loupan_name").val();
            if (searchWords.length != 0) {
                window.location.href = "/Community/Index?searchWords=" + searchWords;
            }
        }
    });


   
    //
   
  
    //切换未来家数据
    $(".housetupian").click(function () {
        var data = [
    {
        "SampleHouseId1": 31,
        "Pic1": "/UserUpload/images/6100f8ee-ec20-4f47-9a35-203a57122870.jpg",
        "SamHouseName1": "5#C 30㎡一室一厅",
        "DesHeadPic1": "/UserUpload/images/8a0e8e05-60e3-423d-a12f-862ebf8e9022.png ",
        "StyleRemark1": "L型的上下床设计，给予了两个人更加私密的空间。楼梯部分做成有收纳功能的抽屉，来满足多人居住的收纳需求。",
        "SampleHouseId2": 32,
        "Pic2": "/UserUpload/images/ba09c8b1-a5a0-4ea1-a74f-36e106cbf994.jpg",
        "SamHouseName2": "80㎡ 三房两厅 ",
        "DesHeadPic2": "/UserUpload/images/821ca886-6c81-429f-9947-5d36b8620c6b.png",
        "StyleRemark2": "整面电视柜的设计，不仅拥有强大的收纳功能，观赏性也是不容小觑，比起毫无用处的电视墙，现在的家装设计都更倾向于实用的电视柜。",
        "SampleHouseId3": 41,
        "Pic3": "/UserUpload/images/d40f7466-4457-42cb-8147-5a410a50c2a3.jpg",
        "SamHouseName3": "57㎡三房一厅",
        "DesHeadPic3": "/UserUpload/images/f6a6f055-0738-4c71-a71e-9e0e824f1fca.png",
        "StyleRemark3": "大面墙的衣柜，不仅使整个空间看起来更有整体感，也能满足两个人的收纳。考虑到女主人的需求，因此一个大梳妆台也是很有必要的。"
    },
    {
        "SampleHouseId1": 36,
        "Pic1": "/UserUpload/images/18b11760-315d-43d0-bea5-2276b829db88.jpg",
        "SamHouseName1": "89㎡复式户型",
        "DesHeadPic1": "/UserUpload/images/984f17ec-67c4-477e-a8cb-32caa9965aa0.png",
        "StyleRemark1": "是孩子们的起居室，也是孩子们的学习玩耍乐园，为了让孩子们能有更多的活动空间，我们将床底、衣柜、书柜都充分利用起来，加大收纳空间。",
        "SampleHouseId2": 36,
        "Pic2": "/UserUpload/images/0bb43c81-6022-49e0-a6d8-68877ce58d50.jpg",
        "SamHouseName2": "89㎡复式户型",
        "DesHeadPic2": "/UserUpload/images/b251ea10-1efb-4d74-84b5-5cd23c696f63.png",
        "StyleRemark2": "我在这里用了不占空间却收纳超强的转角衣柜，床底也是收纳抽屉。将床头柜与书桌融合起来，既节约了空间又不影响功能的实现。",
        "SampleHouseId3": 36,
        "Pic3": "/UserUpload/images/26f68846-96d2-4736-b0d9-4455e6eed250.jpg",
        "SamHouseName3": "89㎡复式户型",
        "DesHeadPic3": "/UserUpload/images/04cb8fa6-4239-4032-a643-ff26b515cd98.png",
        "StyleRemark3": "我喜欢整洁的、功能性分明的衣柜，这样可以将性质不同的衣物通通分开，让整个衣柜看起来干净又利落。"
    },
    {
        "SampleHouseId1": 37,
        "Pic1": "/UserUpload/images/dee705a4-afb3-4bba-a81a-dcfc6570affb.jpg",
        "SamHouseName1": "78㎡二室二厅",
        "DesHeadPic1": "/UserUpload/images/b251ea10-1efb-4d74-84b5-5cd23c696f63.png",
        "StyleRemark1": "超强收纳的电视柜和餐边柜，能够完全的容纳三口之家的杂物。布艺沙发搭配上淡黄的墙壁，给人一种温馨又有格调的感觉。",
        "SampleHouseId2": 37,
        "Pic2": "/UserUpload/images/82439150-c0be-4d14-921f-feafc9a32ad2.jpg",
        "SamHouseName2": "78㎡二室二厅",
        "DesHeadPic2": "/UserUpload/images/04cb8fa6-4239-4032-a643-ff26b515cd98.png",
        "StyleRemark2": "楼梯由一个个抽屉组成，充分考虑到两个孩子需要的收纳空间。抽屉里可以存放过季衣物和床上用品，为两人使用的衣柜节约更多空间。",
        "SampleHouseId3": 37,
        "Pic3": "/UserUpload/images/bc575b58-e1ff-476f-b43a-e6c91838e255.jpg",
        "SamHouseName3": "78㎡二室二厅",
        "DesHeadPic3": "/UserUpload/images/c7dd75c8-e284-440b-b0c4-42137dc0c6fc.png",
        "StyleRemark3": "高低床的设计，能够让整个空间看起来不那么压抑，这不仅仅是为了两个孩子的家庭设计，也能方便家里有带小孩的老人。"
    },
    {
        "SampleHouseId1": 12,
        "Pic1": "/UserUpload/images/314a4edd-4445-4ded-9ef4-4a3087ed035e.jpg",
        "SamHouseName1": "三房两厅两卫",
        "DesHeadPic1": "/UserUpload/images/c7dd75c8-e284-440b-b0c4-42137dc0c6fc.png",
        "StyleRemark1": "我很喜欢将飘窗设计成榻榻米，如果有朋友来的话也有地方可以坐，平时可以躺在榻榻米上看电影、看书。在有限的空间里也需要有小书桌，工作学习都更加方便。",
        "SampleHouseId2": 12,
        "Pic2": "/UserUpload/images/a8f44d89-7ea7-41a3-baad-c22faf8d8153.jpg",
        "SamHouseName2": "三房两厅两卫",
        "DesHeadPic2": "/UserUpload/images/38ef941a-d4e2-44fc-92fd-4656d719f59c.png",
        "StyleRemark2": "粉红色的房间是每个女孩子的dream place，我充分利用了床屏后的空间，加大收纳空间。将衣柜和书柜融为一体，增强空间的层次感。",
        "SampleHouseId3": 12,
        "Pic3": "/UserUpload/images/5a5c0d5f-b5a2-43c5-a0fa-37713b18653b.jpg",
        "SamHouseName3": "三房两厅两卫",
        "DesHeadPic3": "/UserUpload/images/b251ea10-1efb-4d74-84b5-5cd23c696f63.png",
        "StyleRemark3": "将沙发背景墙打造成一大面壁柜，既有装饰作用，又有收纳功能。电视柜和边几都带有收纳功能，能时时保证空间的整洁。"
    },
    {
        "SampleHouseId1": 43,
        "Pic1": "/UserUpload/images/be3065b9-e95c-4ae5-bbfe-469372926351.jpg",
        "SamHouseName1": "3栋B户型",
        "DesHeadPic1": "/UserUpload/images/b251ea10-1efb-4d74-84b5-5cd23c696f63.png",
        "StyleRemark1": "由于和悦居是公租房，面积较小，住的人却比较多，我们采用大面积壁柜设计，既是电视柜，又是书柜，还是鞋柜。后方的书桌平时拉出来就是一个可以容纳8个人的餐桌。",
        "SampleHouseId2": 44,
        "Pic2": "/UserUpload/images/cee7c638-33c0-488a-aff5-9fbe722d4599.jpg",
        "SamHouseName2": "3/4栋G户型",
        "DesHeadPic2": "/UserUpload/images/c7dd75c8-e284-440b-b0c4-42137dc0c6fc.png",
        "StyleRemark2": "单床的次卧设计，考虑到住两个人的可能性，我们还是着重强调了空间的收纳功能。吊柜和悬梁融为一体，增强了空间整体性。",
        "SampleHouseId3": 39,
        "Pic3": "/UserUpload/images/14dc66b2-3dee-4b16-b61b-c0c5f10c2fbd.jpg",
        "SamHouseName3": "1/2/4/5/6栋AB、3栋ACH、7/8栋CN",
        "DesHeadPic3": "/UserUpload/images/04cb8fa6-4239-4032-a643-ff26b515cd98.png",
        "StyleRemark3": "子母床的设计，是为了单人居住设计的，只是考虑到偶尔父母来居住，所以做成的了子母床，平时不影响一人的居住习惯，多人时也可以互不打扰。"
    }
        ];
       
        var jsondata = data;
        var oneagenttmp = $("#sphousetmp").html();
     
      //  var jsondata = $.parseJSON(data);
        var oneagentHtml = juicer(oneagenttmp, jsondata[$(this).data("Index")]);
       
        var $divs = $(oneagentHtml);
        $('.houses').empty();
        $divs.appendTo('.houses');

        fanImg($('.houses'));
        

    });
    

    //bananner效果
    jQuery(".bannerBox").slide({ mainCell: ".box_2 ul", titCell: ".banner_hd ul li", prevCell: ".prev_2", nextCell: ".next_2", effect: "leftLoop", autoPlay: true, delayTime: 600 });
    jQuery(".ydjl").slide({ mainCell: ".ydul", autoPlay: true, effect: "leftMarquee", interTime: 50, trigger: "click", vis: 4 });



   

    //报名点击二维码切换效果
    $('.erweima').click(function () {
        $('.yuyue').toggleClass('cur');
    });

    //设计师展示信息高度动态获取和划伤去效果
    $('.shejishi_list li a .text ').css('margin-top', function () {
        return -$(this).height() / 2 + 'px';
    });
    $('.shejishi_list li a').hover(function () {
        $(this).find('.jieshao,.text').toggle();
    });

    //商家翻牌效果
    flip();

    //首页楼盘选择效果;
    $('.toumin').hover(function () {
        $(this).toggleClass('cur02');
    }).click(function () {
        $(this).addClass('cur01').parent().parent().siblings().find('.toumin').removeClass('cur01')
    });


    //视频
    $('#Video1').click(function () {
        this.play();
        $(this).attr('controls', true);
    });

});

window.onload = function () {

  
    //第一次加载首页未来家数据
    loadsphouse();

    function loadsphouse() {
        var data = [
     {
         "SampleHouseId1": 31,
         "Pic1": "/UserUpload/images/6100f8ee-ec20-4f47-9a35-203a57122870.jpg",
         "SamHouseName1": "5#C 30㎡一室一厅",
         "DesHeadPic1": "/UserUpload/images/8a0e8e05-60e3-423d-a12f-862ebf8e9022.png ",
         "StyleRemark1": "L型的上下床设计，给予了两个人更加私密的空间。楼梯部分做成有收纳功能的抽屉，来满足多人居住的收纳需求。",
         "SampleHouseId2": 32,
         "Pic2": "/UserUpload/images/ba09c8b1-a5a0-4ea1-a74f-36e106cbf994.jpg",
         "SamHouseName2": "80㎡ 三房两厅 ",
         "DesHeadPic2": "/UserUpload/images/821ca886-6c81-429f-9947-5d36b8620c6b.png",
         "StyleRemark2": "整面电视柜的设计，不仅拥有强大的收纳功能，观赏性也是不容小觑，比起毫无用处的电视墙，现在的家装设计都更倾向于实用的电视柜。",
         "SampleHouseId3": 41,
         "Pic3": "/UserUpload/images/d40f7466-4457-42cb-8147-5a410a50c2a3.jpg",
         "SamHouseName3": "57㎡三房一厅",
         "DesHeadPic3": "/UserUpload/images/f6a6f055-0738-4c71-a71e-9e0e824f1fca.png",
         "StyleRemark3": "大面墙的衣柜，不仅使整个空间看起来更有整体感，也能满足两个人的收纳。考虑到女主人的需求，因此一个大梳妆台也是很有必要的。"
     },
     {
         "SampleHouseId1": 36,
         "Pic1": "/UserUpload/images/18b11760-315d-43d0-bea5-2276b829db88.jpg",
         "SamHouseName1": "89㎡复式户型",
         "DesHeadPic1": "/UserUpload/images/984f17ec-67c4-477e-a8cb-32caa9965aa0.png",
         "StyleRemark1": "是孩子们的起居室，也是孩子们的学习玩耍乐园，为了让孩子们能有更多的活动空间，我们将床底、衣柜、书柜都充分利用起来，加大收纳空间。",
         "SampleHouseId2": 36,
         "Pic2": "/UserUpload/images/0bb43c81-6022-49e0-a6d8-68877ce58d50.jpg",
         "SamHouseName2": "89㎡复式户型",
         "DesHeadPic2": "/UserUpload/images/b251ea10-1efb-4d74-84b5-5cd23c696f63.png",
         "StyleRemark2": "我在这里用了不占空间却收纳超强的转角衣柜，床底也是收纳抽屉。将床头柜与书桌融合起来，既节约了空间又不影响功能的实现。",
         "SampleHouseId3": 36,
         "Pic3": "/UserUpload/images/26f68846-96d2-4736-b0d9-4455e6eed250.jpg",
         "SamHouseName3": "89㎡复式户型",
         "DesHeadPic3": "/UserUpload/images/04cb8fa6-4239-4032-a643-ff26b515cd98.png",
         "StyleRemark3": "我喜欢整洁的、功能性分明的衣柜，这样可以将性质不同的衣物通通分开，让整个衣柜看起来干净又利落。"
     },
     {
         "SampleHouseId1": 37,
         "Pic1": "/UserUpload/images/dee705a4-afb3-4bba-a81a-dcfc6570affb.jpg",
         "SamHouseName1": "78㎡二室二厅",
         "DesHeadPic1": "/UserUpload/images/b251ea10-1efb-4d74-84b5-5cd23c696f63.png",
         "StyleRemark1": "超强收纳的电视柜和餐边柜，能够完全的容纳三口之家的杂物。布艺沙发搭配上淡黄的墙壁，给人一种温馨又有格调的感觉。",
         "SampleHouseId2": 37,
         "Pic2": "/UserUpload/images/82439150-c0be-4d14-921f-feafc9a32ad2.jpg",
         "SamHouseName2": "78㎡二室二厅",
         "DesHeadPic2": "/UserUpload/images/04cb8fa6-4239-4032-a643-ff26b515cd98.png",
         "StyleRemark2": "楼梯由一个个抽屉组成，充分考虑到两个孩子需要的收纳空间。抽屉里可以存放过季衣物和床上用品，为两人使用的衣柜节约更多空间。",
         "SampleHouseId3": 37,
         "Pic3": "/UserUpload/images/bc575b58-e1ff-476f-b43a-e6c91838e255.jpg",
         "SamHouseName3": "78㎡二室二厅",
         "DesHeadPic3": "/UserUpload/images/c7dd75c8-e284-440b-b0c4-42137dc0c6fc.png",
         "StyleRemark3": "高低床的设计，能够让整个空间看起来不那么压抑，这不仅仅是为了两个孩子的家庭设计，也能方便家里有带小孩的老人。"
     },
     {
         "SampleHouseId1": 12,
         "Pic1": "/UserUpload/images/314a4edd-4445-4ded-9ef4-4a3087ed035e.jpg",
         "SamHouseName1": "三房两厅两卫",
         "DesHeadPic1": "/UserUpload/images/c7dd75c8-e284-440b-b0c4-42137dc0c6fc.png",
         "StyleRemark1": "我很喜欢将飘窗设计成榻榻米，如果有朋友来的话也有地方可以坐，平时可以躺在榻榻米上看电影、看书。在有限的空间里也需要有小书桌，工作学习都更加方便。",
         "SampleHouseId2": 12,
         "Pic2": "/UserUpload/images/a8f44d89-7ea7-41a3-baad-c22faf8d8153.jpg",
         "SamHouseName2": "三房两厅两卫",
         "DesHeadPic2": "/UserUpload/images/38ef941a-d4e2-44fc-92fd-4656d719f59c.png",
         "StyleRemark2": "粉红色的房间是每个女孩子的dream place，我充分利用了床屏后的空间，加大收纳空间。将衣柜和书柜融为一体，增强空间的层次感。",
         "SampleHouseId3": 12,
         "Pic3": "/UserUpload/images/5a5c0d5f-b5a2-43c5-a0fa-37713b18653b.jpg",
         "SamHouseName3": "三房两厅两卫",
         "DesHeadPic3": "/UserUpload/images/b251ea10-1efb-4d74-84b5-5cd23c696f63.png",
         "StyleRemark3": "将沙发背景墙打造成一大面壁柜，既有装饰作用，又有收纳功能。电视柜和边几都带有收纳功能，能时时保证空间的整洁。"
     },
     {
         "SampleHouseId1": 43,
         "Pic1": "/UserUpload/images/be3065b9-e95c-4ae5-bbfe-469372926351.jpg",
         "SamHouseName1": "3栋B户型",
         "DesHeadPic1": "/UserUpload/images/b251ea10-1efb-4d74-84b5-5cd23c696f63.png",
         "StyleRemark1": "由于和悦居是公租房，面积较小，住的人却比较多，我们采用大面积壁柜设计，既是电视柜，又是书柜，还是鞋柜。后方的书桌平时拉出来就是一个可以容纳8个人的餐桌。",
         "SampleHouseId2": 44,
         "Pic2": "/UserUpload/images/cee7c638-33c0-488a-aff5-9fbe722d4599.jpg",
         "SamHouseName2": "3/4栋G户型",
         "DesHeadPic2": "/UserUpload/images/c7dd75c8-e284-440b-b0c4-42137dc0c6fc.png",
         "StyleRemark2": "单床的次卧设计，考虑到住两个人的可能性，我们还是着重强调了空间的收纳功能。吊柜和悬梁融为一体，增强了空间整体性。",
         "SampleHouseId3": 39,
         "Pic3": "/UserUpload/images/14dc66b2-3dee-4b16-b61b-c0c5f10c2fbd.jpg",
         "SamHouseName3": "1/2/4/5/6栋AB、3栋ACH、7/8栋CN",
         "DesHeadPic3": "/UserUpload/images/04cb8fa6-4239-4032-a643-ff26b515cd98.png",
         "StyleRemark3": "子母床的设计，是为了单人居住设计的，只是考虑到偶尔父母来居住，所以做成的了子母床，平时不影响一人的居住习惯，多人时也可以互不打扰。"
     }
        ];

        var jsondata = data;
        var oneagenttmp = $("#sphousetmp").html();

        var oneagentHtml = juicer(oneagenttmp, jsondata[0]);

        var $divs = $(oneagentHtml);
        $('.houses').empty();
        $divs.appendTo('.houses');
        fanImg($('.houses'));


    }

};

  
  
   
 


    //主页end






function flip() {
    var timer = null;
    var i = 0;
    var aFlip = $(".card");
    var licout = $(".card").length;
    //存储logo
    var arrimgs = $("#agentjson").val();
    //记录当前数组显示到哪个数据
    var curindex = 3;
    //记录批次--
    var pici = 1;
    function flipFn() {
        aFlip.eq(i).toggleClass('card-flipped');
        curindex++;
        if (curindex == arrimgs.length) {
            curindex = 0;
        }
        //一开始 奇数批次时 front被换到后面，所以要替换front
        if (pici % 2 != 0) {
            var $img = aFlip.eq(i).find('.front img');
            var $id = aFlip.eq(i).find('.front a');

        } else {
            var $img = aFlip.eq(i).find('.back img');
            var $id = aFlip.eq(i).find('.back a');
        }

        $img.attr('src', arrimgs[curindex].src);
        $id.attr('href', arrimgs[curindex].id);


        i++;
        if (i == licout) {
            i = 0;
            pici++;
        }

    }
    timer = setInterval(flipFn, 2000);
}


function fanImg(obj) {
    var $box = obj,
        $lis = $box.children(),
        boxWidth = $box.width(),
        boxHeight = $box.height(),
        boxOffLeft = $box.offset().left,
        boxOffTop = $box.offset().top;
    $lis.mouseover(function () {
        if (!$lis.is(":animated")) {
            if (!$(this).hasClass("bigPic")) {
                var $bigPic = $(this).siblings(".bigPic"),
                    bigOffLeft = $bigPic.offset().left - boxOffLeft,
                    bigOffTop = $bigPic.offset().top - boxOffTop,
                    bigWidth = $bigPic.width(),
                    bigHeight = $bigPic.height(),
                    bigOffRight = boxWidth - bigOffLeft - bigWidth,
                    bigOffBottom = boxHeight - bigOffTop - bigHeight;
                $bigPic.height("100%");
                var offLeft = $(this).offset().left - boxOffLeft,
                    offTop = $(this).offset().top - boxOffTop,
                    width = $(this).width(),
                    height = $(this).height(),
                    offRight = boxWidth - offLeft - width,
                    offBottom = boxHeight - offTop - height;

                var $another = $(this).siblings().not(".bigPic");
                $bigPic.removeClass("bigPic");
                var during = 400;
                if (offLeft >= offRight && offTop >= offBottom) {
                    $bigPic.css({
                        "left": bigOffLeft,
                        "top": offTop,
                    }).animate({ "width": width, "height": height }, during, function () {

                    });
                    $another.css({
                        "left": offLeft,
                        "top": bigOffTop,
                        "bottom": "auto"
                    }).animate({
                        "left": bigOffLeft,
                    }, during);
                    $(this).css({
                        "left": "auto",
                        "top": "auto",
                        "right": offRight,
                        "bottom": offBottom
                    }).animate({ "width": bigWidth, "height": bigHeight }, during, function () {
                        $(this).addClass("bigPic").siblings().removeClass("bigPic");
                    });
                } else if (offLeft >= offRight && offTop < offBottom) {
                    $bigPic.css({
                        "left": bigOffLeft,
                        "top": bigOffTop,
                        "bottom": "auto"
                    }).animate({ "width": width, "height": height }, during, function () {

                    });
                    $another.css({
                        "left": offLeft,
                        "bottom": bigOffBottom,
                    }).animate({
                        "left": bigOffLeft,
                    }, during);
                    $(this).css({
                        "left": "auto",
                        "top": offTop,
                        "right": offRight,
                        "bottom": "auto"
                    }).animate({ "width": bigWidth, "height": bigHeight }, during, function () {
                        $(this).addClass("bigPic").siblings().removeClass("bigPic");
                    });
                } else if (offLeft < offRight && offTop >= offBottom) {
                    $bigPic.css({
                        "left": "auto",
                        "right": bigOffRight,
                        "bottom": bigOffBottom,
                        "top": "auto"
                    }).animate({ "width": width, "height": height }, during, function () {

                    });
                    $another.css({
                        "left": "auto",
                        "right": offRight,
                        "bottom": "auto"
                    }).animate({
                        "right": bigOffRight
                    }, during);
                    $(this).css({
                        "left": offLeft,
                        "bottom": offBottom,
                        "top": "auto"
                    }).animate({ "width": bigWidth, "height": bigHeight }, during, function () {
                        $(this).addClass("bigPic").siblings().removeClass("bigPic");
                    });
                } else if (offLeft < offRight && offTop < offBottom) {
                    $bigPic.css({
                        "top": bigOffTop,
                        "right": bigOffRight
                    }).animate({ "width": width, "height": height }, during, function () {

                    });
                    $another.css({
                        "left": "auto",
                        "right": offRight
                    }).animate({
                        "right": bigOffRight
                    }, during);
                    $(this).css({
                        "left": offLeft,
                        "top": offTop
                    }).animate({ "width": bigWidth, "height": bigHeight }, during, function () {
                        $(this).addClass("bigPic").siblings().removeClass("bigPic");

                    });
                }
            }
        }
    });
}


