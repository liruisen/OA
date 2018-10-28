using LRS.OA.Model.EnumType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LRS.OA.Web.Controllers
{
    public class UserInfoController : Controller
    {
        //
        // GET: /UserInfo/
        IBLL.IUserInfoService UserInfoService = new BLL.UserInfoService();
        public ActionResult Index()
        {
            return View();
        }
        #region 获取用户列表数据

        public ActionResult GetUserInfoList()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;

            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;

            int totalCount;
            short delFlag=(short)DeleteEnumType.Normarl;
            var userInfoList = UserInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => c.DelFlag == delFlag, c => c.ID, true);
            var temp = from u in userInfoList
                       select new
                       {
                           Id=u.ID,
                           UName = u.UName,
                           UPwd = u.UPwd,
                           Remark = u.Remark,
                           SubTime=u.SubTime
                       };
            return Json(new { rows = temp, total = totalCount });
        } 
        #endregion
    }
}
