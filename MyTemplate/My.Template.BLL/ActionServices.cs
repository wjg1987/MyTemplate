using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using My.Template.EFDAL;

namespace My.Template.BLL
{
    public  partial class ActionServices
    {
        //public void GetChildActions(int actionID, List<SelectListItem> items)
        //{
        //    var list = this.dbSession.ActionDal.LoadEntitys(
        //        (u) => u.ParentID == actionID && u.IsDelete == false && u.IsFrozen == false
        //        ).ToList();

        //    if (list.Count > 0)
        //    {
        //        foreach (var item in list)
        //        {
        //            items.Add(new SelectListItem() { Selected = false, Text = item.AName, Value = item.ID.ToString() });
        //            GetChildActions(item.ID, items);
        //        }
        //    }
        //} 


        public int GetLoginAdminAllActionCount(int loginAdminId,int actionInfoID)
        {
            var dal = CurrentDal as ActionDal;
            return dal.GetLoginAdminAllActionCount(loginAdminId, actionInfoID);
        }
    }
}
