﻿using BodyReport.Crud.Module;
using BodyReport.Data;
using BodyReport.Models;
using BodyReport.Framework;
using BodyReport.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BodyReport.Manager
{
    public class UserInfoManager : BodyReportManager
    {
        UserInfoModule _userInfoModule = null;

        public UserInfoManager(ApplicationDbContext dbContext) : base(dbContext)
        {
            _userInfoModule = new UserInfoModule(DbContext);
        }

        internal UserInfo GetUserInfo(UserInfoKey key)
        {
            return _userInfoModule.Get(key);
        }

        public List<UserInfo> FindUserInfos(UserInfoCriteria userInfoCriteria = null)
        {
            return _userInfoModule.Find(userInfoCriteria);
        }

        internal void DeleteUserInfo(UserInfoKey key)
        {
            _userInfoModule.Delete(key);
        }

        internal UserInfo UpdateUserInfo(UserInfo userInfo)
        {
            return _userInfoModule.Update(userInfo);
        }
    }
}
