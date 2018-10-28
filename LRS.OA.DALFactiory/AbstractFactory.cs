using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using LRS.OA.IDAL;
using System.Reflection;

namespace LRS.OA.DALFactiory
{
    /// <summary>
    /// 抽象工厂创建类的实例
    /// 通过反射的形式创建类的实例
    /// </summary>
   public  class AbstractFactory
   {
       private static readonly string AssemblyPath = ConfigurationSettings.AppSettings["AssemblyPath"];
       private static readonly string NameSpace = ConfigurationSettings.AppSettings["NameSpace"];


       //private static readonly string AssemblyPaths = System.Configuration.ConfigurationManger
       //private static readonly string NameSpace = ConfigurationSettings.AppSettings["NameSpace"];

       public static IUserInfoDal createUserInfoDal()
       {
           string fullClassName = NameSpace + ".UserInfoDal";
           return createInstance(fullClassName) as IUserInfoDal;
       }
       private static object createInstance(string className)
       {
         var assembly=  Assembly.Load(AssemblyPath);
         return assembly.CreateInstance(className);
       }
   }
}
