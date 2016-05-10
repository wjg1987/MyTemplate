
 

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using My.Template.IDAL;
using My.Template.Model;

namespace My.Template.EFDAL
{
   
	
	public partial class ActionDal:BaseDal<My.Template.Model.Action>,IActionDal
    {

    }
	
	public partial class ActionTypeDal:BaseDal<My.Template.Model.ActionType>,IActionTypeDal
    {

    }
	
	public partial class BannerDal:BaseDal<My.Template.Model.Banner>,IBannerDal
    {

    }
	
	public partial class BannerTypeDal:BaseDal<My.Template.Model.BannerType>,IBannerTypeDal
    {

    }
	
	public partial class NewsDal:BaseDal<My.Template.Model.News>,INewsDal
    {

    }
	
	public partial class NewsTypeDal:BaseDal<My.Template.Model.NewsType>,INewsTypeDal
    {

    }
	
	public partial class NoticeDal:BaseDal<My.Template.Model.Notice>,INoticeDal
    {

    }
	
	public partial class QAInfosDal:BaseDal<My.Template.Model.QAInfos>,IQAInfosDal
    {

    }
	
	public partial class RoleDal:BaseDal<My.Template.Model.Role>,IRoleDal
    {

    }
	
	public partial class Role_ActionDal:BaseDal<My.Template.Model.Role_Action>,IRole_ActionDal
    {

    }
	
	public partial class SinglePageDal:BaseDal<My.Template.Model.SinglePage>,ISinglePageDal
    {

    }
	
	public partial class SiteInfoDal:BaseDal<My.Template.Model.SiteInfo>,ISiteInfoDal
    {

    }
	
	public partial class UserDal:BaseDal<My.Template.Model.User>,IUserDal
    {

    }
	
	public partial class User_ActionDal:BaseDal<My.Template.Model.User_Action>,IUser_ActionDal
    {

    }
	
	public partial class User_RoleDal:BaseDal<My.Template.Model.User_Role>,IUser_RoleDal
    {

    }
	
	public partial class UserInfoDal:BaseDal<My.Template.Model.UserInfo>,IUserInfoDal
    {

    }
	
	public partial class VideosDal:BaseDal<My.Template.Model.Videos>,IVideosDal
    {

    }
	
}