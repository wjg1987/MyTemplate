
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.Template.EFDAL;
using My.Template.IDAL;

namespace My.Template.DALFactory
{
    public partial class SimpleDalFactory
    {
	
		public static IActionDal GetActionDal()
        {
            return new ActionDal();
        }
	
		public static IActionTypeDal GetActionTypeDal()
        {
            return new ActionTypeDal();
        }
	
		public static IAreaInfoDal GetAreaInfoDal()
        {
            return new AreaInfoDal();
        }
	
		public static IBannerDal GetBannerDal()
        {
            return new BannerDal();
        }
	
		public static IBannerTypeDal GetBannerTypeDal()
        {
            return new BannerTypeDal();
        }
	
		public static ICityInfoDal GetCityInfoDal()
        {
            return new CityInfoDal();
        }
	
		public static ICommunityDal GetCommunityDal()
        {
            return new CommunityDal();
        }
	
		public static IGoodsInfoDal GetGoodsInfoDal()
        {
            return new GoodsInfoDal();
        }
	
		public static IGoodsPropertyDal GetGoodsPropertyDal()
        {
            return new GoodsPropertyDal();
        }
	
		public static IGoodsPropertyDetailDal GetGoodsPropertyDetailDal()
        {
            return new GoodsPropertyDetailDal();
        }
	
		public static IGoodsTypeDal GetGoodsTypeDal()
        {
            return new GoodsTypeDal();
        }
	
		public static IHouseAreaDal GetHouseAreaDal()
        {
            return new HouseAreaDal();
        }
	
		public static IKeywordsRankDal GetKeywordsRankDal()
        {
            return new KeywordsRankDal();
        }
	
		public static IKeywordsRecordsDal GetKeywordsRecordsDal()
        {
            return new KeywordsRecordsDal();
        }
	
		public static INewsDal GetNewsDal()
        {
            return new NewsDal();
        }
	
		public static INewsTypeDal GetNewsTypeDal()
        {
            return new NewsTypeDal();
        }
	
		public static INoticeDal GetNoticeDal()
        {
            return new NoticeDal();
        }
	
		public static IOrderDal GetOrderDal()
        {
            return new OrderDal();
        }
	
		public static IOrderGoodsDal GetOrderGoodsDal()
        {
            return new OrderGoodsDal();
        }
	
		public static IOrderStatusDal GetOrderStatusDal()
        {
            return new OrderStatusDal();
        }
	
		public static IQAInfosDal GetQAInfosDal()
        {
            return new QAInfosDal();
        }
	
		public static IRecAddressDal GetRecAddressDal()
        {
            return new RecAddressDal();
        }
	
		public static IRoleDal GetRoleDal()
        {
            return new RoleDal();
        }
	
		public static IRole_ActionDal GetRole_ActionDal()
        {
            return new Role_ActionDal();
        }
	
		public static ISampleHouseDal GetSampleHouseDal()
        {
            return new SampleHouseDal();
        }
	
		public static ISinglePageDal GetSinglePageDal()
        {
            return new SinglePageDal();
        }
	
		public static ISiteInfoDal GetSiteInfoDal()
        {
            return new SiteInfoDal();
        }
	
		public static ISpHouse_HseAreaDal GetSpHouse_HseAreaDal()
        {
            return new SpHouse_HseAreaDal();
        }
	
		public static IUserDal GetUserDal()
        {
            return new UserDal();
        }
	
		public static IUser_ActionDal GetUser_ActionDal()
        {
            return new User_ActionDal();
        }
	
		public static IUser_RoleDal GetUser_RoleDal()
        {
            return new User_RoleDal();
        }
	
		public static IUser_ShoppingCartDal GetUser_ShoppingCartDal()
        {
            return new User_ShoppingCartDal();
        }
	
		public static IUserInfoDal GetUserInfoDal()
        {
            return new UserInfoDal();
        }
	
		public static IVideosDal GetVideosDal()
        {
            return new VideosDal();
        }
	}
}