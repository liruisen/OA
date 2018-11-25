using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using LRS.OA.Web.Models;
using Spring.Web.Mvc;
namespace LRS.OA.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication :SpringMvcApplication
    {
        protected void Application_Start()
        {
            //log4net配置
            log4net.Config.XmlConfigurator.Configure();                 //读取配置文件中，关于log4net的配置信息

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //开启一个线程，扫描异常信息队列
            string filePath = Server.MapPath("/Log/");
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    if (MyExceptionAttribute.ExceptionQueue.Count>0)
                    {
                        //判断队列中是否有数据
                        Exception ex = MyExceptionAttribute.ExceptionQueue.Dequeue();
                        if (ex!=null)
                        {
                            //string fileName = DateTime.Now.ToString("yyyyMMddHHmm");
                            //File.AppendAllText(filePath + fileName + ".txt",ex.ToString(),System.Text.Encoding.UTF8);
                            ILog logger = LogManager.GetLogger("errorMsg");
                            logger.Error(ex.ToString());
                        }
                        else
                        {
                            Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        //如果队列中没有数据，就把该线程休息3秒
                        Thread.Sleep(3000);
                    }
                }
            },filePath);
        }
    }
}