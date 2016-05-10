using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.Template.DALFactory;
using My.Template.EFDAL;
using My.Template.IBLL;
using My.Template.IDAL;
using My.Template.Model;

namespace My.Template.BLL
{
    public partial class UserServices:BaseServices<User>,IUserServices
    {

        //todo：测试这里赋值语句    this.CurrentDal = userDal;  是否有问题 ，先执行的是父类构造函数 那么如果直接赋值userDal的话 很大的可能是 为NULL。需要 测试 确认。。。。。
        //public int AddUser(User user,UserInfo userInfo,ShoppingCart shoppingCart)
        //{
        //   var dal  = CurrentDal as UserDal;
        //    return  dal.AddUser( user, userInfo, shoppingCart);
        //}
    }
}
