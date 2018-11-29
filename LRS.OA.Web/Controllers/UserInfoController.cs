using LRS.OA.Model;
using LRS.OA.Model.EnumType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LRS.OA.Web.Controllers
{
    public class UserInfoController : BaseController
    {
        //
        // GET: /UserInfo/
        IBLL.IUserInfoService UserInfoService { get; set; }
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

        #region 批量删除
        public ActionResult DeleteUserInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            //将list集合存储的要删除的记录的编号传递到业务层
            if (UserInfoService.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
        }
        #endregion

        #region 添加用户
        public ActionResult AddUserInfo(UserInfo userInfo)
        {
            userInfo.DelFlag = 0;
            userInfo.ModifiedOn = DateTime.Now;
            userInfo.SubTime = DateTime.Now;
            UserInfoService.AddEntity(userInfo);
            return Content("ok");

        }
        #endregion
        #region 展示用户信息
        public ActionResult ShowEditInfo()
        {
            int id =Convert.ToInt32(Request["id"]) ;
            var userInfo = UserInfoService.LoadEntities(u => u.ID == id).FirstOrDefault();
            return Json(userInfo, JsonRequestBehavior.AllowGet);
            //return Json(new { data = userInfo });
        }
        #endregion
        #region 修改用户信息
        public ActionResult EditUserInfo(UserInfo userInfo)
        {
            userInfo.ModifiedOn = DateTime.Now;
            if (UserInfoService.EditEntity(userInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
        }
        #endregion
    }
}
