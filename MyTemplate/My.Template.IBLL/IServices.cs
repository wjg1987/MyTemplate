
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.Template.Model;

namespace My.Template.IBLL
{
   
	
	 public partial interface IActionServices:IBaseServices<My.Template.Model.Action>
    {
    }
	
	 public partial interface IActionTypeServices:IBaseServices<My.Template.Model.ActionType>
    {
    }
	
	 public partial interface IBannerServices:IBaseServices<My.Template.Model.Banner>
    {
    }
	
	 public partial interface IBannerTypeServices:IBaseServices<My.Template.Model.BannerType>
    {
    }
	
	 public partial interface INewsServices:IBaseServices<My.Template.Model.News>
    {
    }
	
	 public partial interface INewsTypeServices:IBaseServices<My.Template.Model.NewsType>
    {
    }
	
	 public partial interface INoticeServices:IBaseServices<My.Template.Model.Notice>
    {
    }
	
	 public partial interface IQAInfosServices:IBaseServices<My.Template.Model.QAInfos>
    {
    }
	
	 public partial interface IRoleServices:IBaseServices<My.Template.Model.Role>
    {
    }
	
	 public partial interface IRole_ActionServices:IBaseServices<My.Template.Model.Role_Action>
    {
    }
	
	 public partial interface ISinglePageServices:IBaseServices<My.Template.Model.SinglePage>
    {
    }
	
	 public partial interface ISiteInfoServices:IBaseServices<My.Template.Model.SiteInfo>
    {
    }
	
	 public partial interface IUserServices:IBaseServices<My.Template.Model.User>
    {
    }
	
	 public partial interface IUser_ActionServices:IBaseServices<My.Template.Model.User_Action>
    {
    }
	
	 public partial interface IUser_RoleServices:IBaseServices<My.Template.Model.User_Role>
    {
    }
	
	 public partial interface IUserInfoServices:IBaseServices<My.Template.Model.UserInfo>
    {
    }
	
	 public partial interface IVideosServices:IBaseServices<My.Template.Model.Videos>
    {
    }
	
}