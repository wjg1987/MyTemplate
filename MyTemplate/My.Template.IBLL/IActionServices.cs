using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace My.Template.IBLL
{
    public  partial interface IActionServices
    {
        
         int GetLoginAdminAllActionCount(int loginAdminId, int actionInfoID);
    }
}
