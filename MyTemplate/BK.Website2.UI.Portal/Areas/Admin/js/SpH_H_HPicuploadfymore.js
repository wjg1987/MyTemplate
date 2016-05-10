
$(function () {
    //初始化所有上传的容器，注意：必须指定id 
    $(".uploadMore_btn").uploadify({
        height: uploadMoreheight,
        swf: '/Scripts/uploadify3.2.1/uploadify.swf',
        uploader: '/API/Upload/UploadHPicImg?sid=' + $(".uploadMore_btn").data('sid'),
        width: uploadMorewidth,
        buttonImage: uploadMorebtn,//上传按钮可以重新设计
        wmode: 'transparent',//隐藏上传的flash
        multi: true,
        fileTypeExts: '*.gif; *.jpg; *.png',// 允许的上传文件
        onUploadStart: uploadMore_start_function,
        onUploadProgress: uploadMore_progress_function,
        onUploadError: uploadMore_error_function,
        onUploadSuccess: uploadMore_success_function,
        onUploadComplete: uploadMore_complete_function
    });
});

//处理上传相关事件
//开始上传
function uploadMore_start_function(file) {
    var o_id = file.id; //上传组件的id
    var splitIndex = o_id.lastIndexOf("_"); //最后一个下划线位置
    var o_el = o_id.substr(0, splitIndex); //得到object的id
    var container_el = $("#" + o_el).parent().parent();//得到最外层容器
    var processbarDiv = $("<div class='processbarDiv' data-id='" + file.id + "' data-isFinish='false'><div class='picLoding' ><div>上传中..<span class='spPercent'>20%</span></div> </div></div>");//进度条
    processbarDiv.insertBefore(container_el);
}



//上传处理过程中的函数
function uploadMore_progress_function(file, bytesUploaded, bytesTotal, totalBytesUploaded, totalBytesTotal) {
    var divs = $('.processbarDiv[data-isFinish="false"]');//取得所有正在上传的div
    for (var i = 0; i < divs.length; i++) {
        var id = $(divs[i]).attr('data-id');
        if (id == file.id) {
            var percent = parseInt(bytesUploaded / bytesTotal);
            if (percent < 1) {//没有上传完毕
                $(divs[i]).children('.picLoding').css('height', percent * 100 + '%');
                $(divs[i]).children('div').children('div').children('span').text(percent * 100 + '%');
            } else {
                $(divs[i]).children('.picLoding').css('height', '100%');
                $(divs[i]).children('div').children('div').children('span').text('100%');
                $(divs[i]).attr('data-isFinish', 'true');
            }
        }
    }
}


//上传出错处理函数
function uploadMore_error_function(e) {
    console.log(e);
}

//上传图片成功
function uploadMore_success_function(file, data, response) {

    var o_id = file.id; //上传组件的id
    var splitIndex = o_id.lastIndexOf("_"); //最后一个下划线位置
    var o_el = o_id.substr(0, splitIndex); //得到object的id
    var o_parent = $("#" + o_el).parent();//得到object的父容器
    var container_el = o_parent.parent();//得到最外层容器

    var jd = JSON.parse(data);


    var divs = $('.processbarDiv[data-isFinish="true"]');//取得上传成功的div
    for (var i = 0; i < divs.length; i++) {
        var id = $(divs[i]).attr('data-id');
        if (id == file.id) {
            if (jd.Success) {
               
                $(divs[i]).empty().html("<img src='" + jd["SavePath"] + "' />");
                $(divs[i]).append("<a class='delMore_img' data-picid='" + jd["Pid"] + "' data-hiddenName='" + o_parent.attr("id") + "' href='javascript:;' onclick=\"delMore_uploadimg(this,'" + jd["SaveName"] + "','" + o_parent.attr("id") + "')\"><img src='/images/morelist_del.png' /></a>");
                $(divs[i]).append(" <a href='/admin/HouseScene/SetGoodsRelevance?pid=" + jd["Pid"] + "' target='_blank' class='btn-shezhi'>商品</a>");
                $(divs[i]).append(" <a href='/admin/HouseScene/SetBrand?sphousepicid=" + jd["Pid"] + "' target='_blank' class='btn-shezhi'>品牌</a>");
                $(divs[i]).append("  <a href='/admin/HouseScene/SetDesigner?id=" +jd["Pid"] + "' target='_blank' class='btn-shezhi'>设计师</a>");
                var hiddenVal = $(divs[i]).siblings('.upload_container').children('input[type=hidden]');
                hiddenVal.val(hiddenVal.val() + '|' + jd["SavePath"]);
            } else {

            }
        }
    }


}

//上传完成
function uploadMore_complete_function() { }


//删除上传的图片
function delMore_uploadimg(e, saveName, eid) {
    var el = $(e);
    
    var p_el = el.parent();

    var hiddenName = el.attr("data-hiddenname");
    var picSrc = el.siblings('img').attr('src');
    //var tempValue = $('input[name=' + hiddenName + ']').val();
    //tempValue = tempValue.replace("|" + picSrc, "");
    //$('input[name=' + hiddenName + ']').val(tempValue)
    var node = $(el);
  
    p_el.remove();
   
    $.ajax({
        url: "/API/Upload/DeleteHPic",
        type: "post",      
        dataType: 'json',
        data: { fileName: saveName, pid: el.data('picid') },
        success: function (data, textStatus) {
            console.log(data["Message"]);
            if (data["Res"] == "-1") {
                alert(data["Message"]);
            } else if (data["Res"] == "-2") {
                alert(data["Message"]);
            }
        },
        error: function () {
            //请求出错处理
        }
    })


}
