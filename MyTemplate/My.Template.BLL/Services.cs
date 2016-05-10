
 

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
	
	 public partial class QAInfosServices:BaseServices<My.Template.Model.QAInfos>,IQAInfosServices
    {
        public override void SetCurrentDal()
        {
            this.CurrentDal = dbSession.QAInfosDal;
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