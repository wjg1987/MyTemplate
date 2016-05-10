
 

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
	
		public static IBannerDal GetBannerDal()
        {
            return new BannerDal();
        }
	
		public static IBannerTypeDal GetBannerTypeDal()
        {
            return new BannerTypeDal();
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
	
		public static IQAInfosDal GetQAInfosDal()
        {
            return new QAInfosDal();
        }
	
		public static IRoleDal GetRoleDal()
        {
            return new RoleDal();
        }
	
		public static IRole_ActionDal GetRole_ActionDal()
        {
            return new Role_ActionDal();
        }
	
		public static ISinglePageDal GetSinglePageDal()
        {
            return new SinglePageDal();
        }
	
		public static ISiteInfoDal GetSiteInfoDal()
        {
            return new SiteInfoDal();
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