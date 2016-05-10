// JavaScript Document
$(document).ready(function () {
	var map = new BMap.Map("map");//在container容器中创建一个地图,参数container为div的id属性;  
	var point = new BMap.Point(114.110152, 22.541352);//定位
	
		
	map.centerAndZoom(point,15);                //将point移到浏览器中心，并且地图大小调整为15;
	
	map.addControl(new BMap.NavigationControl());               // 添加平移缩放控件
	map.addControl(new BMap.ScaleControl());                    // 添加比例尺控件
	map.addControl(new BMap.OverviewMapControl());              //添加缩略地图控件
	map.enableScrollWheelZoom();                            //启用滚轮放大缩小
	map.addControl(new BMap.MapTypeControl()); 
		
	//标注  
	var marker = new BMap.Marker(point);  
	map.addOverlay(marker);  
	marker.addEventListener("click",function(){ //点击标注时出发事件  
		this.openInfoWindow(infoWindow);  
	});  
		
	//创建信息窗口  
	var opts = {    
		width : 150,     // 信息窗口宽度    
		height: 15,     // 信息窗口高度    
		title : "深圳市很有蜂格网络科技有限公司"  // 信息窗口标题    
	}
	 
	var infoWindow = new BMap.InfoWindow("深圳市福田区滨河大道2001号T-Park深港影视创意园609-610", opts);  // 创建信息窗口对象    
	map.openInfoWindow(infoWindow, map.getCenter());      // 打开信息窗口   
});
