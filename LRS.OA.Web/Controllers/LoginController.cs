using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LRS.OA.Common;
using LRS.OA.Model.EnumType;

namespace LRS.OA.Web.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        IBLL.IUserInfoService userInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 完成用户登录
        public ActionResult UserLogin()
        {
            string validateCode = Session["validateCode"] != null ? Session["validateCode"].ToString() : string.Empty;
            if (string.IsNullOrEmpty(validateCode))
            {
                return Content("no:验证码错误");
            }
            Session["validateCode"] = null;
            string txtCode = Request["VCode"];
            if (!validateCode.Equals(txtCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("no:验证码错误");
            }

            string LoginCode = Request["LoginCode"];
            string LoginPwd = Request["LoginPwd"];
            //根据用户名和密码找用户
            var userInfo = userInfoService.LoadEntities(u => u.UName == LoginCode && u.UPwd == LoginPwd && u.DelFlag == (short)DeleteEnumType.Normarl).FirstOrDefault();
            if (userInfo!=null)
            {
                Session["userInfo"] = userInfo;
                return Content("ok:登录成功");
            }
            else
            {
                return Content("no:登录失败");

            }

        }
        #endregion

        #region 显示验证码
        public ActionResult ShowValidateCode()
        {
            Common.ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["validateCode"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        } 
        #endregion

    }
}
