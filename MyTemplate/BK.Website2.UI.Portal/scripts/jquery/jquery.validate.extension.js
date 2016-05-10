//2009-11-12
//by cyt
//增加验证

/******************************* 增加 验证 开始 *******************************/
//*** 身份证验证 开始 ***
 var Errors=new Array( 
"验证通过!", 
"身份证号码位数不对!", 
"身份证号码出生日期超出范围或含有非法字符!", 
"身份证号码校验错误!", 
"身份证地区非法!" 
); 


function checkIdcard(idcard) {
    var area = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江", 31: "上海", 32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏", 61: "陕西", 62: "甘肃", 63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外" }

    var idcard, Y, JYM;
    var S, M;
    var idcard_array = new Array();
    idcard_array = idcard.split("");
    //地区检验
    if (area[parseInt(idcard.substr(0, 2))] == null) return 4;
    //身份号码位数及格式检验
    switch (idcard.length) {
        case 15:
            if (idcard == '111111111111111')
                return 2;
            if ((parseInt(idcard.substr(6, 2)) + 1900) % 4 == 0 || ((parseInt(idcard.substr(6, 2)) + 1900) % 100 == 0 && (parseInt(idcard.substr(6, 2)) + 1900) % 4 == 0)) {
                ereg = /^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}$/; //测试出生日期的合法性
            } else {
                ereg = /^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}$/; //测试出生日期的合法性
            }
            if (ereg.test(idcard)) return 0;
            else return 2;
            break;
        case 18:
            //18位身份号码检测
			//小于1900年为生日错			
			if(parseInt(idcard.substr(6, 4))<1900)
				return 2;


            //出生日期的合法性检查 
            //闰年月日:((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))
            //平年月日:((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))
            if (parseInt(idcard.substr(6, 4)) % 4 == 0 || (parseInt(idcard.substr(6, 4)) % 100 == 0 && parseInt(idcard.substr(6, 4)) % 4 == 0)) {
                ereg = /^[1-9][0-9]{5}[1-9][0-9][0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}[0-9Xx]$/; //闰年出生日期的合法性正则表达式
            } else {
                ereg = /^[1-9][0-9]{5}[1-9][0-9][0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}[0-9Xx]$/; //平年出生日期的合法性正则表达式
            }
            if (ereg.test(idcard)) {//测试出生日期的合法性
                //计算校验位
                S = (parseInt(idcard_array[0]) + parseInt(idcard_array[10])) * 7
+ (parseInt(idcard_array[1]) + parseInt(idcard_array[11])) * 9
+ (parseInt(idcard_array[2]) + parseInt(idcard_array[12])) * 10
+ (parseInt(idcard_array[3]) + parseInt(idcard_array[13])) * 5
+ (parseInt(idcard_array[4]) + parseInt(idcard_array[14])) * 8
+ (parseInt(idcard_array[5]) + parseInt(idcard_array[15])) * 4
+ (parseInt(idcard_array[6]) + parseInt(idcard_array[16])) * 2
+ parseInt(idcard_array[7]) * 1
+ parseInt(idcard_array[8]) * 6
+ parseInt(idcard_array[9]) * 3;
                Y = S % 11;
                M = "F";
                JYM = "10X98765432";
                M = JYM.substr(Y, 1); //判断校验位

                if (M == idcard_array[17]) return 0; //检测ID的校验位
                else return 3;
            }
            else return 2;
            break;
        default:
            return 1;
            break;
    }
}

// 身份证号码验证       
$.validator.addMethod("isIDCardNo", function (value, element) {
    return this.optional(element) || checkIdcard(value) == 0;
}, "请正确输入您的身份证号码");

//*** 身份证验证 结束 ***

//*** IP验证
var isIPAddress = new Object();
isIPAddress = /^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/;
// IP验证
$.validator.addMethod("isIP", function (value, element) {
    return this.optional(element) || isIPAddress.test(value.replace(/(^\s*)|(\s*$)/g, ""));
}, "请正确输入IP地址");

//*** 正则表达式
$.validator.addMethod(
        "regex",
        function (value, element, regexp) {
            var check = false;
            var re = new RegExp(regexp);
            return this.optional(element) || re.test(value);
        },
        "Please check your input."
);

        //*** 反正则表达式
$.validator.addMethod(
"notRegex",
function (value, element, regexp) {
    var check = false;
    var re = new RegExp(regexp);
    return this.optional(element) || !re.test(value);
},
"Please check your input."
);
         


//*** 全部为中文
function allChinese(str) {
    var reg = /[^\u4E00-\u9FA5]/g;
    return !reg.test(str);

}

$.validator.addMethod("allChinese", function (value, element) {
    return this.optional(element) || allChinese(value);
}, "必须全部是中文");


//*** 不能包含Html代码验证
var htmlLabel = new Object();
htmlLabel = /<|>/;
$.validator.addMethod("noHtml", function (value, element) {
    return this.optional(element) || !htmlLabel.test(value);
}, "不能包含Html符号");


//*** 不能包含中文或全角字符验证
function isChn(str) {
    var reg = /^[\x00-\xff]+$/;
    if (reg.test(str)) {
        return false;
    }
    return true;
}

$.validator.addMethod("noChinese", function (value, element) {
    return this.optional(element) || !isChn(value);
}, "不能包含中文");

//*** 按CharCode检测 ***//
function CharCodeLength(s) {
    var n = 0;
    for (var i = 0; i < s.length; i++) {
        //charCodeAt()可以返回指定位置的unicode编码,这个返回值是0-65535之间的整数   
        if (s.charCodeAt(i) < 128) {
            n++;
        }
        else {
            n += 2;
        }
    }
    return n;
}

$.validator.addMethod("charCodeLength", function (value, element, pars) {
    return this.optional(element) || (CharCodeLength(value) <= pars);
}, "长度错误");


//*** 标准字符检测 ***//
//是否包含全部是标准字符 true 是 false 包含不标准字符
function IsLegalXmlString(str) {

    var isOk = true;

    for (var i = 0; i < str.length; i++) {
        if (!IsLegalXmlChar(str.charCodeAt(i))) {
            isOk = false;
            break;
        }
    }
    return isOk;
}

function IsLegalXmlChar(character) {
    return (
				character == 0x9 /* == '\t' == 9   */ ||
                 character == 0xA /* == '\n' == 10  */ ||
                 character == 0xD /* == '\r' == 13  */ ||
                (character >= 0x20 && character <= 0xD7FF) ||
                (character >= 0xE000 && character <= 0xFFFD) ||
                (character >= 0x10000 && character <= 0x10FFFF)
		);
}

$.validator.addMethod("IsLegalXmlString", function (value, element) {
    return IsLegalXmlString(value);
}, "包含不标准字符");

$.validator.addMethod("notequalTo", function (value, element, param) {
     var target = $(param).unbind(".validate-notequalTo").bind("blur.validate-notequalTo", function () {
                    $(element).valid();
                });
    return value != target.val();
}, "不能相同");
/******************************* 增加 验证 结束 *******************************/

//by sww
function keydown(evt, target, len) {
    if (evt.keyCode == 8 || evt.keyCode == 46) return;
    var elmt = evt.srcElement ? evt.srcElement : evt.target;
    if (elmt.value.length >= len && target != null) {
        target.focus();
    }
}

