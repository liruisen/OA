using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LRS.OA.IDAL;
using LRS.OA.Model;
using LRS.OA.DAL;
using System.Data.Entity;

namespace LRS.OA.DALFactiory
{
    /// <summary>
    ///  数据会话层：就是一个工厂类，负责完成所有数据操作类实例的创建，然后业务层通过数据会话层来获取要 操作数据类的实例。所以数据会话层将业务与数据层分离解耦
    ///  
    ///  在数据会话层中提供一个方法：完成所有数据的保存（同一事件对数据库需要进行多次操作，在此只连接一次数据库）
    /// </summary>
    public class DBSession:IDBSession
    {
        //public IDAL.IUserInfoDal creatUserInfoDal()
        //{
        //    return new DAL.UserInfoDal();
        //}

        //OAEntities Db = new OAEntities();
        //DbContext Db = DAL.DbContextFactory.createDbContext();//创建线程内唯一的EF操作对象

        /// <summary>
        /// 创建线程内唯一的EF操作对象
        /// </summary>
        public DbContext Db
        {
            get
            {
                return DAL.DbContextFactory.createDbContext();//创建线程内唯一的EF操作对象
            }
           
        }

        /// <summary>
        /// 封装类的实例的创建
        /// </summary>
        private IUserInfoDal _UserInfoDal;
        public IUserInfoDal UserInfoDal
        {
            get
            {
                if (_UserInfoDal==null)
                {
                    //_userInfoDal = new UserInfoDal();
                    _UserInfoDal = AbstractFactory.createUserInfoDal();//通过抽象工厂封装了类的实例的创建
                }
                return _UserInfoDal;
            }
            set
            {
                _UserInfoDal = value;
            }
        }

        /// <summary>
        /// 一个业务中，经常涉及到对多张表的操作，我们希望连接一次数据库，完成对多张表数据的操作。提高性能。工作单元模式
        /// </summary>
        /// <returns>保存成功与否的bool值</returns>
        public bool saveChanges()
        {
            return Db.SaveChanges() > 0;
        }
    }
}
