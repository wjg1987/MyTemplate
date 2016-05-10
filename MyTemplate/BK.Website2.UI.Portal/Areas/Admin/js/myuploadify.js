$(function () {
    //初始化所有上传的容器，注意：必须指定id 
    $(".upload_btn").uploadify({
        height: uploadheight,
        swf: '/Scripts/uploadify3.2.1/uploadify.swf',
        uploader: '/API/Upload/UploadImg',
        width: uploadwhidth,
        buttonImage: uploadbtn, //上传按钮可以重新设计
        wmode: 'transparent', //隐藏上传的flash
        fileTypeExts: '*.gif; *.jpg; *.png', // 允许的上传文件
        onUploadStart: upload_start_function,
        onUploadProgress: upload_progress_function,
        onUploadError: upload_error_function,
        onUploadSuccess: upload_success_function,
        onUploadComplete: upload_complete_function
    });
});

//处理上传相关事件
//开始上传
function upload_start_function(file) {
    var o_id = file.id; //上传组件的id
    var splitIndex = o_id.lastIndexOf("_"); //最后一个下划线位置
    var o_el = o_id.substr(0, splitIndex); //得到object的id
    var container_el = $("#" + o_el).parent().parent(); //得到最外层容器
    container_el.children(".loading").addClass("on");
}


//上传处理过程中的函数
function upload_progress_function() {

}


//上传出错处理函数
function upload_error_function(e) {
    console.log(e);
}

//上传图片成功
function upload_success_function(file, data, response) {

    var o_id = file.id; //上传组件的id
    var splitIndex = o_id.lastIndexOf("_"); //最后一个下划线位置
    var o_el = o_id.substr(0, splitIndex); //得到object的id
    var o_parent = $("#" + o_el).parent(); //得到object的父容器
    var container_el = o_parent.parent(); //得到最外层容器

    var jd = JSON.parse(data);

    if (jd.Success) {

        container_el.empty().html("<img src='" + jd["SavePath"] + "' />");
        container_el.append("<a class='del_img' href='javascript:;' onclick=\"del_uploadimg(this,'" + jd["SaveName"] + "','" + o_parent.attr("id") + "')\"><img src='/images/morelist_del.png' /></a>");
        container_el.children(".loading").removeClass("on");
        container_el.append("<input name='" + o_parent.attr("id") + "' type='hidden' value='" + jd["SavePath"] + "' />");


    } else {
        container_el.children(".loading").removeClass("on");
        alert(jd.Message);

    }
}

//上传完成
function upload_complete_function() { }


//删除上传的图片
function del_uploadimg(e, saveName, eid) {
    var el = $(e);
    var p_el = el.parent();
    p_el.empty();
    p_el.html('<div id="' + eid + '" class="upload_btn"></div><div class="loading off"><img src="/images/uploadloading.gif" /></div>');
    p_el.children(".upload_btn").uploadify({
        height: uploadheight,
        swf: '/Scripts/uploadify3.2.1/uploadify.swf',
        uploader: '/API/Upload/UploadImg',
        width: uploadwhidth,
        buttonImage: uploadbtn, //上传按钮图片
        wmode: 'transparent',
        fileTypeExts: '*.gif; *.jpg; *.png', // 允许的上传文件
        upload_start_handler: upload_start_function,
        upload_progress_handler: upload_progress_function,
        upload_error_handler: upload_error_function,
        upload_success_handler: upload_success_function,
        upload_complete_handler: upload_complete_function

    });

    //此处删除图片的话 只是前端去掉图片样式 只有点击更新的时候才会更新掉图片为空
    //$.ajax({
    //    type: "post",
    //    url: "/API/Upload/Delete",
    //    dataType: 'json',
    //    data: { fileName: saveName },
    //    success: function (data, textStatus) {
    //        console.log(data["Message"]);
    //    },
    //    error: function () {
    //        //请求出错处理
    //    }
    //})
}