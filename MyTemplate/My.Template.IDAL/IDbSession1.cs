
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.Template.IDAL
{
   	public partial interface IDbSession
    {
			
	
        IActionDal ActionDal { get; }

   
			
	
        IActionTypeDal ActionTypeDal { get; }

   
			
	
        IAreaInfoDal AreaInfoDal { get; }

   
			
	
        IBannerDal BannerDal { get; }

   
			
	
        IBannerTypeDal BannerTypeDal { get; }

   
			
	
        ICityInfoDal CityInfoDal { get; }

   
			
	
        ICommunityDal CommunityDal { get; }

   
			
	
        IGoodsInfoDal GoodsInfoDal { get; }

   
			
	
        IGoodsPropertyDal GoodsPropertyDal { get; }

   
			
	
        IGoodsPropertyDetailDal GoodsPropertyDetailDal { get; }

   
			
	
        IGoodsTypeDal GoodsTypeDal { get; }

   
			
	
        IHouseAreaDal HouseAreaDal { get; }

   
			
	
        IKeywordsRankDal KeywordsRankDal { get; }

   
			
	
        IKeywordsRecordsDal KeywordsRecordsDal { get; }

   
			
	
        INewsDal NewsDal { get; }

   
			
	
        INewsTypeDal NewsTypeDal { get; }

   
			
	
        INoticeDal NoticeDal { get; }

   
			
	
        IOrderDal OrderDal { get; }

   
			
	
        IOrderGoodsDal OrderGoodsDal { get; }

   
			
	
        IOrderStatusDal OrderStatusDal { get; }

   
			
	
        IQAInfosDal QAInfosDal { get; }

   
			
	
        IRecAddressDal RecAddressDal { get; }

   
			
	
        IRoleDal RoleDal { get; }

   
			
	
        IRole_ActionDal Role_ActionDal { get; }

   
			
	
        ISampleHouseDal SampleHouseDal { get; }

   
			
	
        ISinglePageDal SinglePageDal { get; }

   
			
	
        ISiteInfoDal SiteInfoDal { get; }

   
			
	
        ISpHouse_HseAreaDal SpHouse_HseAreaDal { get; }

   
			
	
        IUserDal UserDal { get; }

   
			
	
        IUser_ActionDal User_ActionDal { get; }

   
			
	
        IUser_RoleDal User_RoleDal { get; }

   
			
	
        IUser_ShoppingCartDal User_ShoppingCartDal { get; }

   
			
	
        IUserInfoDal UserInfoDal { get; }

   
			
	
        IVideosDal VideosDal { get; }

   
		
		int SaveChanges();
	 }
}