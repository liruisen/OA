using LRS.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRS.OA.IBLL
{
    public interface IBaseService<T> where T : class,new()
    {

        IDBSession currentDBSession { get; }
        IDAL.IBaseDal<T> CurrentDal { get; set; }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderByLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderByLambda, bool isAsc);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool DeleteEntity(T entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool EditEntity(T entity);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T AddEntity(T entity);
    }
}
