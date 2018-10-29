using LRS.OA.DALFactiory;
using LRS.OA.IDAL;
using LRS.OA.IBLL;
using LRS.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LRS.OA.Model.EnumType;

namespace LRS.OA.BLL
{
    public class UserInfoService:BaseService<UserInfo>,IUserInfoService
    {
        /// <summary>
        /// 批量删除多条用户记录
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteEntities(List<int> list)
        {
            var userInfoList = this.currentDBSession.UserInfoDal.LoadEntities(u => list.Contains(u.ID));
            foreach (var userInfo in userInfoList)
            {
                userInfo.DelFlag = (short)DeleteEnumType.LogicDelete;
                this.currentDBSession.UserInfoDal.EditEntity(userInfo);
            }
            return this.currentDBSession.saveChanges();
        }
        
        public override void SetCurrentDal()
        {
            CurrentDal = this.currentDBSession.UserInfoDal;
        }

    }
}
