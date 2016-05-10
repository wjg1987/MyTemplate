/* PCAS (Province City Area Selector 省、市、地区联动选择JS封装类) Ver 2.01 完整版 *\

　制作时间:2005-12-30
　更新时间:2006-01-24
　数据修正:2006-08-17
　文档大小:18KB
　　应用说明:页面包含<script type="text/javascript" src="pcasunzip.js"></script>
 省市联动
   new PCAS("Province","City")
   new PCAS("Province","City","吉林省")
   new PCAS("Province","City","吉林省","吉林市")
 省市地区联动
   new PCAS("Province","City","Area")
   new PCAS("Province","City","Area","吉林省")
   new PCAS("Province","City","Area","吉林省","松原市")
   new PCAS("Province","City","Area","吉林省","松原市","宁江区")这里分别为select的名和默认值
 省、市、地区对象取得的值均为实际值。
 注：省、市、地区提示信息选项的值为""(空字符串)

　感谢
　　　网友418528#gmail.com对数据进行的核实工作 2006-08-07

\*** 程序制作/版权所有:崔永祥(333) 此为转载 转载请注明***/


SPT="--请选择省份--";
SCT="--请选择城市--";
SAT="--请选择地区--";
ShowT = 0;   //提示文字 0:不显示 1:显示
//PCAD = "上海市$市辖区,黄浦区,卢湾区,徐汇区,长宁区,静安区,普陀区,闸北区,虹口区,杨浦区,闵行区,宝山区,嘉定区,浦东新区,金山区,松江区,青浦区,南汇区,奉贤区|市辖县,崇明县#北京市$市辖区,东城区,西城区,崇文区,宣武区,朝阳区,丰台区,石景山区,海淀区,门头沟区,房山区,通州区,顺义区,昌平区,大兴区,怀柔区,平谷区|市辖县,密云县,延庆县";
if (ShowT) PCAD = SPT + "$" + SCT + "," + SAT + "#" + PCAD;
PCAArea = [];
PCAP = [];
PCAC = [];
PCAA = [];
PCAN = PCAD.split("#");//上海市$市辖区,黄浦区|市辖县,崇明县


for (i = 0; i < PCAN.length; i++){
    PCAA[i] = [];
    TArea = PCAN[i].split("$")[1].split("|");//获得市区
    for (j = 0; j < TArea.length; j++) {
        PCAA[i][j] = TArea[j].split(",");//设置市区
        if (PCAA[i][j].length == 1) PCAA[i][j][1] = SAT;
        TArea[j] = TArea[j].split(",")[0];
    }
    PCAArea[i] = PCAN[i].split("$")[0] + "," + TArea.join(",");
    PCAP[i] = PCAArea[i].split(",")[0];
    PCAC[i] = PCAArea[i].split(',');
}

function PCAS() {
    this.SelP = document.getElementsByName(arguments[0])[0];
    this.SelC = document.getElementsByName(arguments[1])[0];
    this.SelA = document.getElementsByName(arguments[2])[0];
    this.DefP = this.SelA ? arguments[3] : arguments[2];
    this.DefC = this.SelA ? arguments[4] : arguments[3];
    this.DefA = this.SelA ? arguments[5] : arguments[4];
    this.SelP.PCA = this;
    this.SelC.PCA = this;

    this.SelP.onchange = function() {
        PCAS.SetC(this.PCA);
    };

    if (this.SelA)
        this.SelC.onchange = function() {
             PCAS.SetA(this.PCA)
        };
    PCAS.SetP(this);
}

PCAS.SetP = function(PCA) {
     for (i = 0; i < PCAP.length; i++) {
         PCAPT = PCAPV = PCAP[i];
         if (PCAPT == SPT) 
             PCAPV = "";
         PCA.SelP.options.add(new Option(PCAPT, PCAPV));
         if (PCA.DefP == PCAPV)
             PCA.SelP[i].selected = true;
     }
    PCAS.SetC(PCA);
}

PCAS.SetC = function(PCA) {
    PI = PCA.SelP.selectedIndex;
    PCA.SelC.length = 0;
    for (i = 1; i < PCAC[PI].length; i++) {
        PCACT = PCACV = PCAC[PI][i];
        if (PCACT == SCT) PCACV = "";
        PCA.SelC.options.add(new Option(PCACT, PCACV));
        if (PCA.DefC == PCACV) PCA.SelC[i - 1].selected = true;
    }
    if (PCA.SelA) PCAS.SetA(PCA);
}

PCAS.SetA = function(PCA) {
    PI = PCA.SelP.selectedIndex;
    CI = PCA.SelC.selectedIndex;
    PCA.SelA.length = 0;
    for (i = 1; i < PCAA[PI][CI].length; i++) {
        PCAAT = PCAAV = PCAA[PI][CI][i];
        if (PCAAT == SAT) PCAAV = "";
        PCA.SelA.options.add(new Option(PCAAT, PCAAV));
        if (PCA.DefA == PCAAV) PCA.SelA[i - 1].selected = true;
    }
}
//--> 
