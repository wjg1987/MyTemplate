/*!
 * js模拟html元素滚动条 v1.0
 * http://www.cnblogs.com/typeof/
 *
 * 由于主流浏览器对html元素滚动条样式控制不一致，
 * 有的浏览器可以通过简单的样式来控制滚动条的显示效果比如说ie系列，
 * 而有的浏览器则不允许比如firefox。所以如果前端开发人员，
 * 想要对html页面一部分元素的滚动条通过css来实现简单的“美化、处理”，
 * 对于系统默认给html元素提供滚动条是不可实现的。
 *
 * 该脚本通过标准html元素模拟实现了滚动条的主要功能，开发人员可以自定义滚动条元素样式。
 * 解决了跨浏览器情况下不能为滚动元素添加css，渲染不一致的问题。
 * 高级浏览器下比如：ie9+、firefox、chrome等等的最新版本滚动条操作、效果和系统默认提供的没有太多差异。
 * ie6下不能点击滚动条的上方或者下方区域来触发滚动条的滚动效果，
 * 也不能通过键盘上下键来触发滚动条的滚动，其他浏览器下不会有这个问题。
 * 该版本不支持横向滚动条
 *
 * 如果想使用该脚本来实现自定义滚动条，可以在需要的元素上添加自定义属性data-scroll="true"，同时需要
 * 通过自定义属性data-width=""、data-height=""来指定元素显示的宽度和高度
 *
 * 该脚本实现的滚动条部分样式来自google webstore https://chrome.google.com/webstore
 * ie6, 7, 8 下为实现滚动条能达到css3的圆角效果引入了PIE.htc http://html-tricks.com/css/css3-alternatives-for-ie/ 
 *
 * 日期：2012-12-12 10:12
 */
!function(squid) {

    "use strict";

    //JScroll类
    function JScroll(selector, context) {
        this.init(selector, context)
    }

    JScroll.prototype = {

        constructor: JScroll,

        init: function(selector, context) {
            var elem;

            selector = selector || 'data-scroll'

            if(selector.nodeType) {
                elem = [selector]
            }

            if(typeof selector === 'string') {
                elem = squid.getElementsByAttribute(selector, context)
            }

            this.initView(elem)
        },

        initView: function(elems) {
            var i = 0,
                length = elems.length,
                elem,
                height,
                has;

            for(; i < length; i++) {
                elem = elems[i]
                height = elem['data-height'] || elem.getAttribute('data-height')
                if(elem.scrollHeight > height) {
                    this.create(elem, height)
                    has = true
                }
            }
             
            if(has) {
                this.initEvent()
                
                //resize height redraw the scroll bar
                this.refresh() 
            }
        },
        
        //创建滚动条元素整体结构
        create: function(elem, height) {
            var wrapper,
                list,
                //滚动条元素
                s,
                //带滚动条元素显示的宽度
                width = elem['data-width'] || elem.getAttribute('data-width'),
                //滚动条显示高度
                value,
                //滚动条可以滚动的最大值
                max;

            //wrapper
            wrapper = document.createElement('div')
            wrapper.className = 'jscroll-wrapper'
            squid.css(wrapper, {
                height: height + 'px',
                width: width + 'px'
            })

            squid.addClass(elem, 'jscroll-body')
            //overwrite the user define style
            squid.css(elem, {
                overflow: 'visible',
                position: 'absolute',
                height: 'auto',
                width: (width-20) + 'px'
            })

            //list
            list = document.createElement('div')
            list.className = 'jscroll-list unselectable'
            list.unselectable = 'on'
            squid.css(list, {
                height: (height - 5) + 'px'
            })

            //滚动条
            s = document.createElement('div')
            s.className = 'jscroll-drag unselectable'
            s.unselectable = 'on'
            s.setAttribute('tabindex', '1')
            s.setAttribute('hidefocus', true)

            list.appendChild(s)
            wrapper.appendChild(list)
            //把需要出现滚动条的元素包裹起来
            elem.parentNode.replaceChild(wrapper, elem)
            wrapper.insertBefore(elem, list) 
            
            //滚动条高度
            value = this.scrollbarHeight(elem, height)
            squid.css(s, {
                height: value + 'px',
                top: '0px'
            })

            //add event
            this.regEvent(wrapper)

            //record the elem current height
            this.data[elem] = {
                height: elem.offsetHeight
            }

            //push the local stack
            this.localStack.push(elem)

            max = list.offsetHeight - s.offsetHeight
            //变量缓存min, max
            this.limits[s] = {
                min: 0,
                max: max
            }
        },
        
        //计算滚动条的高度 
        scrollbarHeight: function(elem, height) {
            var value = elem.scrollHeight;
                
            return (height / value) * height 
        },
        
        initEvent: function() {
            var that = this,
                _default,
                elem,
                top,
                min,
                max,
                prev,
                parent,
                sbody,
                unit;

            //滚动条滚动
            squid.on(document, 'mousemove', function(event) {
                elem = that.scrolling.elem
                if(elem !== null) {
                    squid.addClass(elem, 'scrolling')
                    top = event.clientY - that.scrolling.diffy 
					parent = that.ie6 ? elem.parentNode.parentNode : elem.parentNode
					min = that.limits[elem].min
                    max = that.limits[elem].max 
                    prev = parent.previousSibling
                    sbody = prev.tagName.toLowerCase() === 'div' ? prev : prev.previousSibling
                    _default = parseInt(sbody['data-height'] || sbody.getAttribute('data-height'), 10)
                    unit = (sbody.scrollHeight - _default) / max
                    
                    squid.addClass(sbody.parentNode, 'unselectable')
                    if(top < min) {
                        top = min 
                    }else if(top > max) {
                        top = max
                    }

                    elem.style.top = top + 'px' 
                    that.doScroll(sbody, top * unit) 
                }
            })

            //滚动结束
            squid.on(document, 'mouseup', function(event) {
                elem = that.scrolling.elem
                if(elem) {
                    prev = that.ie6 ? elem.parentNode.parentNode.previousSibling : elem.parentNode.previousSibling
                    sbody = prev.tagName.toLowerCase() === 'div' ? prev : prev.previousSibling
                    squid.removeClass(sbody.parentNode, 'unselectable')
                }

                that.scrolling.elem = null
                that.scrolling.diffy = 0
            }) 
        },

        //添加滚动条事件 
        regEvent: function(elem) {
            var that = this,
                sbody = elem.firstChild,
                list = sbody.nextSibling,
                //滚动条元素
                s = list.firstChild,
                //滚动条滚动最小值
                min,
                //滚动条滚动最大值
                max,
                _default = parseInt(sbody['data-height'] || sbody.getAttribute('data-height'), 10),
                //比例单位
                unit,
                //firefox浏览器
                firefox = 'MozBinding' in document.documentElement.style,
                //鼠标滚轮事件
                mousewheel = firefox ? 'DOMMouseScroll' : 'mousewheel',
                //opera浏览器
                opera = window.oprea && navigator.userAgent.indexOf('MSIE') === -1,
                //is firing mousedown event
                firing = false,
                //鼠标点击，定时器执行时间
                interval,
                //滚动条距离容器高度
                top,
                //滚动条当前top值
                cur,
                //每次滚动多少像素
                speed = 18;

            
            
            //scroll事件 鼠标滑动滚轮移动滚动条
            squid.on(elem, mousewheel, function(event) {
                var delta;
                 
                min = that.limits[s].min
                max = that.limits[s].max
                unit = (sbody.scrollHeight - _default) / max
                if(event.wheelDelta) {
                    delta = opera ? -event.wheelDelta / 120 : event.wheelDelta / 120
                }else{
                    delta = -event.detail / 3 
                }

                cur = parseInt(s.style.top || 0, 10)
                //向上滚动
                if(delta > 0) {
                    top = cur - speed 
                    if(top < min) {
                        top = min
                    }
                }else{//向下滚动
                    top = cur + speed 
                    if(top > max) {
                        top = max
                    }
                }

                s.style.top = top + 'px'
                that.doScroll(sbody, top * unit)
                
                //阻止body元素滚动条滚动
                event.preventDefault()
            })
            
            //ie6, 7, 8下，如果鼠标连续点击两次且时间间隔太短，则第二次事件不会触发
            //拖动滚动条，点击滚动条可到达区域
            squid.on(list, 'mousedown', function(event) {
                var target = event.target,
                    y = event.clientY; 
                
                target = event.target 
                if(target.tagName.toLowerCase() === 'shape')
                    target = s
                  
                //鼠标点击元素是滚动条
                if(target === s) {
                    //invoke elem setCapture
                    s.setCapture && s.setCapture() 
                     
                    that.scrolling.diffy = y - s.offsetTop
                    //鼠标移动过程中判断是否正在拖动滚动条
                    that.scrolling.elem = s 
                }else if(target.className.match('jscroll-list')){
                    firing = true
                    interval = setInterval(function() {
                        if(firing) {
                            that.mouseHandle(list, y, _default)
                        }
                    }, 80)
                }
            })

            //鼠标松开滚动条停止滚动
            squid.on(list, 'mouseup', function() {
                //invoke elem releaseCapture
                s.releaseCapture && s.releaseCapture()
                
                firing = false 
                clearInterval(interval)
            })

            //滚动条元素获取焦点，可以触发keyup事件
            squid.on(s, 'click', function() {
                this.focus() 
            })

            //滚动条获取焦点后，触发键盘上下键，滚动条滚动
            squid.on(s, 'keydown', function(event) {
                var keyCode = event.keyCode,
                    state = false;

                min = that.limits[s].min
                max = that.limits[s].max
                unit = (sbody.scrollHeight - _default) / max
                cur = parseInt(s.style.top || 0, 10)
                switch(keyCode) {
                    case 38:
                        top = cur - speed 
                        if(top < min) {
                            top = min
                        }
                        state = true
                        break
                    case 40:
                        top = cur + speed 
                        if(top > max) {
                            top = max
                        }
                        state = true
                        break
                    default:
                        break
                }
                if(state) {
                    s.style.top = top + 'px'
                    that.doScroll(sbody, top * unit) 
                }
                 
                event.preventDefault()
            })
        },
        
        //鼠标点击滚动条可到达区域上面或者下面时，滚动条滚动
        mouseHandle: function(elem, place, _default) {
            var prev = elem.previousSibling,
                //包含滚动条显示内容元素
                a = prev.tagName.toLowerCase() === 'div' ? prev : prev.previousSibling,
                //
                n = elem.firstChild,
                //滚动条元素
                s = this.ie6 ? n.lastChild : n.tagName.toLowerCase() === 'div' ? n : n.nextSibling,
                //滚动条高度
                height,
                //list元素距body的top值
                value,
                //滚动条距离容器高度
                top,
                //滚动条距body的top值
                sTop,
                //滚动条滚动最小值
                min,
                //滚动条滚动最大值
                max,
                //
                unit,
                //每点击滚动条可到达区域，滚动条向下或向上移动10px
                step = 10,
                //鼠标点击滚动条可到达区域距离最顶端或者最底端小于distance时，滚动条能够自动移动到最顶端或者最低端
                distance = 20;

            min = this.limits[s].min
            max = this.limits[s].max
            unit = (a.scrollHeight - _default) / max
            height = s.offsetHeight
            top = parseInt(s.style.top || 0, 10)
            value = squid.getOffset(elem).top
            sTop = squid.getOffset(s).top
            //鼠标点击滚动条下方区域，滚动条向下滚动
            if(place > sTop) {
                if(value + elem.offsetHeight - place < distance && (elem.offsetHeight - height - top) < distance) {
                    top = max 
                }else{
                    if((sTop + height + step) <= place) {
                        top += step
                    }else{
                        top = place - value - height 
                    } 
                }
            }else{
                //鼠标点击区域距滚动条顶端小于滚动条长度时，滚动条自动滚动到最顶端
                if(place - value < distance && top < distance) {
                    top = min 
                }else{
                    //滚动条距页面顶部高度减去鼠标clientY值大于step
                    if(sTop - place >= step) {
                        top -= step 
                    }else{
                        top = place - value 
                    } 
                }
            }
            if(top < min) {
                top = min 
            }else if(top > max) {
                top = max
            } 

            s.style.top = top + 'px' 
            this.doScroll(a, top * unit)
        },

        //recalculate the height of the scroll bar
        update: function(elem) {
            var list = elem.parentNode.lastChild,
                s = this.ie6 ? list.firstChild.lastChild : list.lastChild,
                height = elem['data-height'] || elem.getAttribute('data-height'),
                value;
             
            //has or not scroll bar
            if(elem.offsetHeight > height) {
                value = (height / elem.offsetHeight) * height
                s.style.height = value + 'px' 
                this.limits[s].max = list.offsetHeight - s.offsetHeight
                list.style.display = 'block'
            }else{
                list.style.display = 'none'
            }
        },

        //rerending scroll bar
        refresh: function() {
            var i = 0,
                elem,
                elems = this.localStack,
                length = elems.length;

            for(; i< length; i++) {
                elem = elems[i]
                if(this.data[elem].height !== elem.offsetHeight) {
                    this.update(elem)
                    this.data[elem].height = elem.offsetHeight
                }
            }
             
            setTimeout(squid.proxy(this.refresh, this), 1)
        },

        //滚动内容
        doScroll: function(elem, top) {
            elem.style.top = -top + 'px'
        },

        //current do scroll element
        scrolling: {
            elem: null,
            diffy: 0
        },

        //ie6不支持XMLHttpRequest对象
        ie6: !window.XMLHttpRequest,

        //滚动条能滚动到最上方，最下方的极限值
        limits: {},

        //jscroll element storage
        localStack: [],

        //last height
        data: {}
    }

    //外部调用方法
    squid.swing.jscroll = function(selector, context) {
        return new JScroll(selector, context)
    }
}(squid);
