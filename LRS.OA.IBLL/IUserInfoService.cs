using LRS.OA.IDAL;
using LRS.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LRS.OA.IBLL
{
    public interface IUserInfoService:IBaseService<UserInfo>
    {
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool DeleteEntities(List<int> list);
    }
}
