
 

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

   
			
	
        IBannerDal BannerDal { get; }

   
			
	
        IBannerTypeDal BannerTypeDal { get; }

   
			
	
        INewsDal NewsDal { get; }

   
			
	
        INewsTypeDal NewsTypeDal { get; }

   
			
	
        INoticeDal NoticeDal { get; }

   
			
	
        IQAInfosDal QAInfosDal { get; }

   
			
	
        IRoleDal RoleDal { get; }

   
			
	
        IRole_ActionDal Role_ActionDal { get; }

   
			
	
        ISinglePageDal SinglePageDal { get; }

   
			
	
        ISiteInfoDal SiteInfoDal { get; }

   
			
	
        IUserDal UserDal { get; }

   
			
	
        IUser_ActionDal User_ActionDal { get; }

   
			
	
        IUser_RoleDal User_RoleDal { get; }

   
			
	
        IUserInfoDal UserInfoDal { get; }

   
			
	
        IVideosDal VideosDal { get; }

   
		
		int SaveChanges();
	 }
}