/************************************************************************************
* Copyright (c) 2018Microsoft all Rights Reserved
* CLR版本: 4.0.30319.42000
*机器名称: LIRUISEN
*命名空间: LRS.OA.Web.Models
*文件名: MyExceptionAttribute
*版本号: V1.0.0.0
*唯一标识: 4bbbf586-5f9a-4708-a963-03edaefd6262
*当前用户域: LIRUISEN
*创建人: Administrator
*创建时间: 2018/11/25 14:36:20
*
*描述:
*
*
*
*====================================================================
*修改标记
*修改时间: 2018/11/25 14:36:20
*修改人: Administrator
*版本号: V1.0.0.0
*描述：
*
*
************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LRS.OA.Web.Models
{
    public class MyExceptionAttribute:HandleErrorAttribute
    {
        public static Queue<Exception> ExceptionQueue = new Queue<Exception>();
        public override void OnException(ExceptionContext filterContext)
        {
            
            Exception ex = filterContext.Exception;
            //写到队列
            ExceptionQueue.Enqueue(ex);
            //跳转到错误页
            filterContext.HttpContext.Response.Redirect("error.html");
            base.OnException(filterContext);
        }
    }
}