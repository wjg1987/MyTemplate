
  
function loadmap(data)
    {
        // 百度地图API功能	
        map = new BMap.Map("map");
        
      
       
        map.addControl(new BMap.NavigationControl());               // 添加平移缩放控件
        map.addControl(new BMap.ScaleControl());                    // 添加比例尺控件
        map.addControl(new BMap.OverviewMapControl());              //添加缩略地图控件
        map.enableScrollWheelZoom();                            //启用滚轮放大缩小
        map.addControl(new BMap.MapTypeControl());
       
        
        var data_info =$.parseJSON(data);
    
        if (data_info.length <= 0) {
            map.centerAndZoom(new BMap.Point(114.109833, 22.541155), 15);
        }
        else {
            map.centerAndZoom(new BMap.Point(data_info[0][0], data_info[0][1]), 15);
        }
       
       
               //将point移到浏览器中心，并且地图大小调整为15;
       
       
        var opts = {
            width: 250,     // 信息窗口宽度
            height: 80,     // 信息窗口高度
            title: "商家地址", // 信息窗口标题
            enableMessage: true//设置允许信息窗发送短息
        };
       
        for (var i = 0; i < data_info.length; i++) {
           
            var marker = new BMap.Marker(new BMap.Point(data_info[i][0], data_info[i][1]));  // 创建标注
            var content = data_info[i][2];

            map.addOverlay(marker);               // 将标注添加到地图中
            addClickHandler(content, marker);
        }
     
        function addClickHandler(content, marker) {

            marker.addEventListener("click", function (e) {

                openInfo(content, e)
            }
            );
        }
        function openInfo(content, e) {
            var p = e.target;

            var point = new BMap.Point(p.getPosition().lng, p.getPosition().lat);
            var infoWindow = new BMap.InfoWindow(content, opts);  // 创建信息窗口对象 
            map.openInfoWindow(infoWindow, point); //开启信息窗口
        }


    }
	