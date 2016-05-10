using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using My.Template.BLL;
using My.Template.IBLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using My.Template.Model;
using My.Template.UI.Portal.CommClass;
using System.Text;
using System.Text.RegularExpressions;

namespace My.Template.UI.Portal.Areas.mobile.Controllers
{
    public class GetDataController : Controller
    {
        #region PC
        //public JsonResult GetCityComm(int cityid)
        //{
        //    ICityInfoServices cityInfoServices = new CityInfoServices();
        //    int totalCitys = 0;
        //    var cityList = cityInfoServices.LoadPageEntitys(1, 5, out totalCitys,
        //        (u) => u.IsDelete == false && u.IsView == true,
        //        (o) => o.Sequence, true,
        //        (o) => o.ID, true).ToList();

        //    var city = cityInfoServices.LoadEntitys(u => u.ID == cityid).FirstOrDefault();

        //    JObject json = new JObject();

        //    json.Add(new JProperty("curcityid", city.ID));
        //    json.Add(new JProperty("curcityName", city.CityName));
        //    JArray jarraycomm = new JArray();
        //    var coms = city.Community.OrderBy(u => u.Sequence).ToList();
        //    foreach (var comm in coms)
        //    {
        //        if (comm.IsDelete == false)
        //        {
        //            JObject o = new JObject();

        //            ISampleHouseServices sam = new SampleHouseServices();
        //            var samp = sam.LoadEntitys(u => u.CommunityID == comm.ID && u.IsDelete == false && u.IsView).FirstOrDefault();
        //            o.Add(new JProperty("detailLink", samp == null ? "javascript:alert('当前区域还未添加蜂巢空间。');" : "/SpHse/index/" + samp.ID));
        //            o.Add(new JProperty("img", comm.HPreViewCircleImg));
        //            o.Add(new JProperty("sname", comm.HName));
        //            o.Add(new JProperty("sdescription", comm.Description));


        //            JArray array2 = new JArray();

        //            comm.SampleHouse = comm.SampleHouse.OrderBy(u => u.Sequence).ToList();
        //            foreach (var smp in comm.SampleHouse)
        //            {
        //                if (smp.IsDelete == false)
        //                {
        //                    JObject p = new JObject();
        //                    p.Add(new JProperty("id", smp.ID));
        //                    p.Add(new JProperty("shname", smp.SHName));
        //                    array2.Add(p);
        //                }
        //            }
        //            o.Add(new JProperty("sphouses", array2));
        //            jarraycomm.Add(o);
        //        }
        //    }
        //    json.Add(new JProperty("sampofcitylist", jarraycomm));


        //    JArray jarrayCity = new JArray();
        //    foreach (var item in cityList)
        //    {
        //        JObject o = new JObject();
        //        o.Add(new JProperty("cityid", item.ID));
        //        o.Add(new JProperty("cityname", item.CityName));
        //        o.Add(new JProperty("CityImgCur", item.CityImgCur));
        //        o.Add(new JProperty("CityImg", item.CityImg));
        //        jarrayCity.Add(o);
        //    }
        //    json.Add(new JProperty("citylist", jarrayCity));

        //    return this.Json(JsonConvert.SerializeObject(json));
        //}
        #endregion

      

    }
}
