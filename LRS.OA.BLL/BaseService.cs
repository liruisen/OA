using LRS.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using LRS.OA.IDAL;
using LRS.OA.DALFactiory;
using LRS.OA.DALFactory;

namespace LRS.OA.BLL
{

    public abstract class BaseService<T> where T:class ,new()
    {
        /// <summary>
        /// 在业务基类中完成DBSession的调用，然后将业务层中公共的方法定义在基类中，但是这些方法不知道通过DBSession来获取哪个数据操作类的实例。所以将该业务基类定义成抽象类，加上一个抽象方法，加上一个IBaseDal属性，并让基类的构造方法调用抽象方法，目的是在表现层new具体的业务子类时，父类的构造方法被调用，这时执行抽象方法，但是执行的是子类中的具体的实现。业务之类知道通过DBsession获取哪个数据操作类的实例
        /// </summary>
        public IDBSession currentDBSession
        {
            get
            {
                //return new DBSession();//暂时先New
                return DBSessionFactory.creatDBSession();
            }
        }

        public IDAL.IBaseDal<T> CurrentDal { get; set; }
        public abstract void SetCurrentDal();
        public BaseService()
        {
            SetCurrentDal();//子类一定要实现抽象方法
        }

        #region 查询
        /// <summary>
        /// 查询过滤
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.LoadEntities(whereLambda);
        } 
        #endregion

        #region 分页过滤
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="s">排序条件的类型</typeparam>
        /// <param name="pageIndex">当前页码数</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="totalCount">总数据条数</param>
        /// <param name="whereLambda">查询过滤条件</param>
        /// <param name="orderByLambda">排序条件</param>
        /// <param name="isAsc">是否为升序，否则为倒序</param>
        /// <returns>排序结果集</returns>
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderByLambda, bool isAsc)
        {
            return CurrentDal.LoadPageEntities<s>(pageIndex, pageSize, out totalCount, whereLambda, orderByLambda, isAsc);
        } 
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">需要删除的对象</param>
        /// <returns>bool值</returns>
        public bool DeleteEntity(T entity)
        {
            CurrentDal.DeleteEntity(entity);
            return this.currentDBSession.saveChanges();
        } 
        #endregion

        #region 更新数据
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">需要更新的对象</param>
        /// <returns>bool结果</returns>
        public bool EditEntity(T entity)
        {
            CurrentDal.EditEntity(entity);
            return this.currentDBSession.saveChanges();


        }
        #endregion

        #region 添加数据
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">需要添加的对象</param>
        /// <returns>添加成功后的对象</returns>
        public T AddEntity(T entity)
        {
            CurrentDal.AddEntity(entity);
            if (this.currentDBSession.saveChanges())
            {
                return entity;
            }
            else
            {
                return null;
            }
        } 
        #endregion

       
    }
}
