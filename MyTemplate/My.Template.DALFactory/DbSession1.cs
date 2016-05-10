
 

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