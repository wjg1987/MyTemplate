/*
   二级联动数据格式。。 
    var jsondata = [{ lev1text: '深圳', lev1val: '1', lev2: [{ lev2text: '福田', lev2val: '2' }] }, { lev1text: '广州', lev1val: '3', lev2: [{ lev2text: '东莞', lev2val: '3' }] }];

    1.需要配合 jquery 使用
    2.页面加载完毕后 调用方法 setSelectData(lev1ID, lev2ID)    参数1 是第1个select的id  参数2 是联动的2级select的id
    3.后台传递数json数据 到前台 JSON.Parse()
    
 */

//var jsondata = [{ lev1text: '深圳', lev1val: '1',isselect:0, lev2: [{ lev2text: '福田', lev2val: '2',isselect:0 }] }, { lev1text: '广州', lev1val: '3',isselect:0, lev2: [{ lev2text: '东莞', lev2val: '3',isselect:0 }] }];

function setSelectData(lev1ID, lev2ID) {
    for (var i = 0; i < jsondata.length; i++) {
        var lev1Text = jsondata[i].lev1text;
        var lev1Val = jsondata[i].lev1val;
        if (jsondata[i].isselect === 0) {
            var $option = $("<option value='" + lev1Val + "'>" + lev1Text + "</option>");
            $option.appendTo("#" + lev1ID);
        } else {
            var $option = $("<option selected='true'  value='" + lev1Val + "'>" + lev1Text + "</option>");
            $option.appendTo("#" + lev1ID);
        }

        for (var j = 0; j < jsondata[i].lev2.length; j++) {
            if (i == 0) {
                var lev2Text = jsondata[i].lev2[j].lev2text;
                var lev2Val = jsondata[i].lev2[j].lev2val;
                if (jsondata[i].lev2[j].isselect === 0) {
                    var $option2 = $("<option value='" + lev2Val + "'>" + lev2Text + "</option>");
                    $option2.appendTo("#" + lev2ID);
                } else {
                    var $option2 = $("<option selected='true' value='" + lev2Val + "'>" + lev2Text + "</option>");
                    $option2.appendTo("#" + lev2ID);
                }
            }
        }

        $("#" + lev1ID).change(function() {
            $("#" + lev2ID).empty();
            var lev1IDchose = $(this).val();
            for (var k = 0; k < jsondata.length; k++) {
                if (jsondata[k].lev1val == lev1IDchose) {
                    for (var m = 0; m < jsondata[k].lev2.length; m++) {
                        var lev2Text = jsondata[k].lev2[m].lev2text;
                        var lev2Val = jsondata[k].lev2[m].lev2val;
                        var $option2 = $("<option value='" + lev2Val + "'>" + lev2Text + "</option>");
                        $option2.appendTo("#" + lev2ID);
                    }
                }
            }
        })
    }
}

