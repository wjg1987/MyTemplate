
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.Template.EFDAL;
using My.Template.IDAL;

namespace My.Template.DALFactory
{
	public partial class DbSession:IDbSession
    {
   
	
	   private IActionDal _ActionDal;
        public IActionDal ActionDal
        {
            get
            {
                if(_ActionDal == null)
                {
                    _ActionDal = SimpleDalFactory.GetActionDal();
                }
                return _ActionDal;
            }
        }

	
	   private IActionTypeDal _ActionTypeDal;
        public IActionTypeDal ActionTypeDal
        {
            get
            {
                if(_ActionTypeDal == null)
                {
                    _ActionTypeDal = SimpleDalFactory.GetActionTypeDal();
                }
                return _ActionTypeDal;
            }
        }

	
	   private IAreaInfoDal _AreaInfoDal;
        public IAreaInfoDal AreaInfoDal
        {
            get
            {
                if(_AreaInfoDal == null)
                {
                    _AreaInfoDal = SimpleDalFactory.GetAreaInfoDal();
                }
                return _AreaInfoDal;
            }
        }

	
	   private IBannerDal _BannerDal;
        public IBannerDal BannerDal
        {
            get
            {
                if(_BannerDal == null)
                {
                    _BannerDal = SimpleDalFactory.GetBannerDal();
                }
                return _BannerDal;
            }
        }

	
	   private IBannerTypeDal _BannerTypeDal;
        public IBannerTypeDal BannerTypeDal
        {
            get
            {
                if(_BannerTypeDal == null)
                {
                    _BannerTypeDal = SimpleDalFactory.GetBannerTypeDal();
                }
                return _BannerTypeDal;
            }
        }

	
	   private ICityInfoDal _CityInfoDal;
        public ICityInfoDal CityInfoDal
        {
            get
            {
                if(_CityInfoDal == null)
                {
                    _CityInfoDal = SimpleDalFactory.GetCityInfoDal();
                }
                return _CityInfoDal;
            }
        }

	
	   private ICommunityDal _CommunityDal;
        public ICommunityDal CommunityDal
        {
            get
            {
                if(_CommunityDal == null)
                {
                    _CommunityDal = SimpleDalFactory.GetCommunityDal();
                }
                return _CommunityDal;
            }
        }

	
	   private IGoodsInfoDal _GoodsInfoDal;
        public IGoodsInfoDal GoodsInfoDal
        {
            get
            {
                if(_GoodsInfoDal == null)
                {
                    _GoodsInfoDal = SimpleDalFactory.GetGoodsInfoDal();
                }
                return _GoodsInfoDal;
            }
        }

	
	   private IGoodsPropertyDal _GoodsPropertyDal;
        public IGoodsPropertyDal GoodsPropertyDal
        {
            get
            {
                if(_GoodsPropertyDal == null)
                {
                    _GoodsPropertyDal = SimpleDalFactory.GetGoodsPropertyDal();
                }
                return _GoodsPropertyDal;
            }
        }

	
	   private IGoodsPropertyDetailDal _GoodsPropertyDetailDal;
        public IGoodsPropertyDetailDal GoodsPropertyDetailDal
        {
            get
            {
                if(_GoodsPropertyDetailDal == null)
                {
                    _GoodsPropertyDetailDal = SimpleDalFactory.GetGoodsPropertyDetailDal();
                }
                return _GoodsPropertyDetailDal;
            }
        }

	
	   private IGoodsTypeDal _GoodsTypeDal;
        public IGoodsTypeDal GoodsTypeDal
        {
            get
            {
                if(_GoodsTypeDal == null)
                {
                    _GoodsTypeDal = SimpleDalFactory.GetGoodsTypeDal();
                }
                return _GoodsTypeDal;
            }
        }

	
	   private IHouseAreaDal _HouseAreaDal;
        public IHouseAreaDal HouseAreaDal
        {
            get
            {
                if(_HouseAreaDal == null)
                {
                    _HouseAreaDal = SimpleDalFactory.GetHouseAreaDal();
                }
                return _HouseAreaDal;
            }
        }

	
	   private INewsDal _NewsDal;
        public INewsDal NewsDal
        {
            get
            {
                if(_NewsDal == null)
                {
                    _NewsDal = SimpleDalFactory.GetNewsDal();
                }
                return _NewsDal;
            }
        }

	
	   private INewsTypeDal _NewsTypeDal;
        public INewsTypeDal NewsTypeDal
        {
            get
            {
                if(_NewsTypeDal == null)
                {
                    _NewsTypeDal = SimpleDalFactory.GetNewsTypeDal();
                }
                return _NewsTypeDal;
            }
        }

	
	   private INoticeDal _NoticeDal;
        public INoticeDal NoticeDal
        {
            get
            {
                if(_NoticeDal == null)
                {
                    _NoticeDal = SimpleDalFactory.GetNoticeDal();
                }
                return _NoticeDal;
            }
        }

	
	   private IOrderDal _OrderDal;
        public IOrderDal OrderDal
        {
            get
            {
                if(_OrderDal == null)
                {
                    _OrderDal = SimpleDalFactory.GetOrderDal();
                }
                return _OrderDal;
            }
        }

	
	   private IOrderGoodsDal _OrderGoodsDal;
        public IOrderGoodsDal OrderGoodsDal
        {
            get
            {
                if(_OrderGoodsDal == null)
                {
                    _OrderGoodsDal = SimpleDalFactory.GetOrderGoodsDal();
                }
                return _OrderGoodsDal;
            }
        }

	
	   private IOrderStatusDal _OrderStatusDal;
        public IOrderStatusDal OrderStatusDal
        {
            get
            {
                if(_OrderStatusDal == null)
                {
                    _OrderStatusDal = SimpleDalFactory.GetOrderStatusDal();
                }
                return _OrderStatusDal;
            }
        }

	
	   private IQAInfosDal _QAInfosDal;
        public IQAInfosDal QAInfosDal
        {
            get
            {
                if(_QAInfosDal == null)
                {
                    _QAInfosDal = SimpleDalFactory.GetQAInfosDal();
                }
                return _QAInfosDal;
            }
        }

	
	   private IRecAddressDal _RecAddressDal;
        public IRecAddressDal RecAddressDal
        {
            get
            {
                if(_RecAddressDal == null)
                {
                    _RecAddressDal = SimpleDalFactory.GetRecAddressDal();
                }
                return _RecAddressDal;
            }
        }

	
	   private IRoleDal _RoleDal;
        public IRoleDal RoleDal
        {
            get
            {
                if(_RoleDal == null)
                {
                    _RoleDal = SimpleDalFactory.GetRoleDal();
                }
                return _RoleDal;
            }
        }

	
	   private IRole_ActionDal _Role_ActionDal;
        public IRole_ActionDal Role_ActionDal
        {
            get
            {
                if(_Role_ActionDal == null)
                {
                    _Role_ActionDal = SimpleDalFactory.GetRole_ActionDal();
                }
                return _Role_ActionDal;
            }
        }

	
	   private ISampleHouseDal _SampleHouseDal;
        public ISampleHouseDal SampleHouseDal
        {
            get
            {
                if(_SampleHouseDal == null)
                {
                    _SampleHouseDal = SimpleDalFactory.GetSampleHouseDal();
                }
                return _SampleHouseDal;
            }
        }

	
	   private ISinglePageDal _SinglePageDal;
        public ISinglePageDal SinglePageDal
        {
            get
            {
                if(_SinglePageDal == null)
                {
                    _SinglePageDal = SimpleDalFactory.GetSinglePageDal();
                }
                return _SinglePageDal;
            }
        }

	
	   private ISiteInfoDal _SiteInfoDal;
        public ISiteInfoDal SiteInfoDal
        {
            get
            {
                if(_SiteInfoDal == null)
                {
                    _SiteInfoDal = SimpleDalFactory.GetSiteInfoDal();
                }
                return _SiteInfoDal;
            }
        }

	
	   private ISpHouse_HseAreaDal _SpHouse_HseAreaDal;
        public ISpHouse_HseAreaDal SpHouse_HseAreaDal
        {
            get
            {
                if(_SpHouse_HseAreaDal == null)
                {
                    _SpHouse_HseAreaDal = SimpleDalFactory.GetSpHouse_HseAreaDal();
                }
                return _SpHouse_HseAreaDal;
            }
        }

	
	   private IUserDal _UserDal;
        public IUserDal UserDal
        {
            get
            {
                if(_UserDal == null)
                {
                    _UserDal = SimpleDalFactory.GetUserDal();
                }
                return _UserDal;
            }
        }

	
	   private IUser_ActionDal _User_ActionDal;
        public IUser_ActionDal User_ActionDal
        {
            get
            {
                if(_User_ActionDal == null)
                {
                    _User_ActionDal = SimpleDalFactory.GetUser_ActionDal();
                }
                return _User_ActionDal;
            }
        }

	
	   private IUser_RoleDal _User_RoleDal;
        public IUser_RoleDal User_RoleDal
        {
            get
            {
                if(_User_RoleDal == null)
                {
                    _User_RoleDal = SimpleDalFactory.GetUser_RoleDal();
                }
                return _User_RoleDal;
            }
        }

	
	   private IUser_ShoppingCartDal _User_ShoppingCartDal;
        public IUser_ShoppingCartDal User_ShoppingCartDal
        {
            get
            {
                if(_User_ShoppingCartDal == null)
                {
                    _User_ShoppingCartDal = SimpleDalFactory.GetUser_ShoppingCartDal();
                }
                return _User_ShoppingCartDal;
            }
        }

	
	   private IUserInfoDal _UserInfoDal;
        public IUserInfoDal UserInfoDal
        {
            get
            {
                if(_UserInfoDal == null)
                {
                    _UserInfoDal = SimpleDalFactory.GetUserInfoDal();
                }
                return _UserInfoDal;
            }
        }

	
	   private IVideosDal _VideosDal;
        public IVideosDal VideosDal
        {
            get
            {
                if(_VideosDal == null)
                {
                    _VideosDal = SimpleDalFactory.GetVideosDal();
                }
                return _VideosDal;
            }
        }


		public int SaveChanges()
        {
            return DbContextFactory.GetCurDbContext().SaveChanges();
        }
	}
	 
}