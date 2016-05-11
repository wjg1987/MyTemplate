
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.Template.IBLL;
using My.Template.Model;

namespace My.Template.BLL
{
   
	
	 public partial class ActionServices:BaseServices<My.Template.Model.Action>,IActionServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.ActionDal;
        }

    }
	
	 public partial class ActionTypeServices:BaseServices<My.Template.Model.ActionType>,IActionTypeServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.ActionTypeDal;
        }

    }
	
	 public partial class AreaInfoServices:BaseServices<My.Template.Model.AreaInfo>,IAreaInfoServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.AreaInfoDal;
        }

    }
	
	 public partial class BannerServices:BaseServices<My.Template.Model.Banner>,IBannerServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.BannerDal;
        }

    }
	
	 public partial class BannerTypeServices:BaseServices<My.Template.Model.BannerType>,IBannerTypeServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.BannerTypeDal;
        }

    }
	
	 public partial class CityInfoServices:BaseServices<My.Template.Model.CityInfo>,ICityInfoServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.CityInfoDal;
        }

    }
	
	 public partial class CommunityServices:BaseServices<My.Template.Model.Community>,ICommunityServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.CommunityDal;
        }

    }
	
	 public partial class GoodsInfoServices:BaseServices<My.Template.Model.GoodsInfo>,IGoodsInfoServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.GoodsInfoDal;
        }

    }
	
	 public partial class GoodsPropertyServices:BaseServices<My.Template.Model.GoodsProperty>,IGoodsPropertyServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.GoodsPropertyDal;
        }

    }
	
	 public partial class GoodsPropertyDetailServices:BaseServices<My.Template.Model.GoodsPropertyDetail>,IGoodsPropertyDetailServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.GoodsPropertyDetailDal;
        }

    }
	
	 public partial class GoodsTypeServices:BaseServices<My.Template.Model.GoodsType>,IGoodsTypeServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.GoodsTypeDal;
        }

    }
	
	 public partial class HouseAreaServices:BaseServices<My.Template.Model.HouseArea>,IHouseAreaServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.HouseAreaDal;
        }

    }
	
	 public partial class NewsServices:BaseServices<My.Template.Model.News>,INewsServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.NewsDal;
        }

    }
	
	 public partial class NewsTypeServices:BaseServices<My.Template.Model.NewsType>,INewsTypeServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.NewsTypeDal;
        }

    }
	
	 public partial class NoticeServices:BaseServices<My.Template.Model.Notice>,INoticeServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.NoticeDal;
        }

    }
	
	 public partial class OrderServices:BaseServices<My.Template.Model.Order>,IOrderServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.OrderDal;
        }

    }
	
	 public partial class OrderGoodsServices:BaseServices<My.Template.Model.OrderGoods>,IOrderGoodsServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.OrderGoodsDal;
        }

    }
	
	 public partial class OrderStatusServices:BaseServices<My.Template.Model.OrderStatus>,IOrderStatusServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.OrderStatusDal;
        }

    }
	
	 public partial class QAInfosServices:BaseServices<My.Template.Model.QAInfos>,IQAInfosServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.QAInfosDal;
        }

    }
	
	 public partial class RecAddressServices:BaseServices<My.Template.Model.RecAddress>,IRecAddressServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.RecAddressDal;
        }

    }
	
	 public partial class RoleServices:BaseServices<My.Template.Model.Role>,IRoleServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.RoleDal;
        }

    }
	
	 public partial class Role_ActionServices:BaseServices<My.Template.Model.Role_Action>,IRole_ActionServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.Role_ActionDal;
        }

    }
	
	 public partial class SampleHouseServices:BaseServices<My.Template.Model.SampleHouse>,ISampleHouseServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.SampleHouseDal;
        }

    }
	
	 public partial class SinglePageServices:BaseServices<My.Template.Model.SinglePage>,ISinglePageServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.SinglePageDal;
        }

    }
	
	 public partial class SiteInfoServices:BaseServices<My.Template.Model.SiteInfo>,ISiteInfoServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.SiteInfoDal;
        }

    }
	
	 public partial class SpHouse_HseAreaServices:BaseServices<My.Template.Model.SpHouse_HseArea>,ISpHouse_HseAreaServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.SpHouse_HseAreaDal;
        }

    }
	
	 public partial class UserServices:BaseServices<My.Template.Model.User>,IUserServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.UserDal;
        }

    }
	
	 public partial class User_ActionServices:BaseServices<My.Template.Model.User_Action>,IUser_ActionServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.User_ActionDal;
        }

    }
	
	 public partial class User_RoleServices:BaseServices<My.Template.Model.User_Role>,IUser_RoleServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.User_RoleDal;
        }

    }
	
	 public partial class User_ShoppingCartServices:BaseServices<My.Template.Model.User_ShoppingCart>,IUser_ShoppingCartServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.User_ShoppingCartDal;
        }

    }
	
	 public partial class UserInfoServices:BaseServices<My.Template.Model.UserInfo>,IUserInfoServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.UserInfoDal;
        }

    }
	
	 public partial class VideosServices:BaseServices<My.Template.Model.Videos>,IVideosServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.VideosDal;
        }

    }
	
}