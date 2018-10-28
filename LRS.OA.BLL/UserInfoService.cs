using LRS.OA.DALFactiory;
using LRS.OA.IDAL;
using LRS.OA.IBLL;
using LRS.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRS.OA.BLL
{
    public class UserInfoService:BaseService<UserInfo>,IUserInfoService
    {

        public override void SetCurrentDal()
        {
            CurrentDal = this.currentDBSession.UserInfoDal;
        }

    }
}
