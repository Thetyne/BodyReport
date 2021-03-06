﻿using BodyReport.Crud.Module;
using BodyReport.Data;
using BodyReport.Models;
using BodyReport.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BodyReport.ServiceLayers.Interfaces;
using BodyReport.Framework;
using BodyReport.ServiceLayers;
using Microsoft.Extensions.DependencyInjection;

namespace BodyReport.Manager
{
    /// <summary>
    /// Manage Users
    /// </summary>
    public class UserManager : BodyReportManager
    {
        UserModule _userModule = null;
        IUserRolesService _usersRoleService;
        IRolesService _rolesService;

        public UserManager(ApplicationDbContext dbContext) : base(dbContext)
        {
            _userModule = new UserModule(DbContext);

            _usersRoleService = WebAppConfiguration.ServiceProvider.GetService<IUserRolesService>();
            ((BodyReportService)_usersRoleService).SetDbContext(DbContext); // for use same transaction

            _rolesService = WebAppConfiguration.ServiceProvider.GetService<IRolesService>();
            ((BodyReportService)_rolesService).SetDbContext(DbContext); // for use same transaction
        }

        private void CompleteUserRole(User user)
        {
            var userRoleCriteria = new UserRoleCriteria();
            userRoleCriteria.UserId = new StringCriteria() { Equal = user.Id };
            var userRoleList = _usersRoleService.FindUserRole(userRoleCriteria);
            if (userRoleList != null)
            {
                foreach (var userRole in userRoleList)
                {
                    user.Role = _rolesService.GetRole(new RoleKey() { Id = userRole.RoleId });
                }
            }
        }

        internal User GetUser(UserKey key, bool manageRole = true)
        {
            User user = _userModule.Get(key);

            if(user != null && manageRole)
                CompleteUserRole(user);

            return user;
        }

        public List<User> FindUsers(out int totalRecords, UserCriteria userCriteria = null, bool manageRole = true, int currentRecordIndex = 0, int maxRecord = 0)
        {
            var userList = _userModule.Find(out totalRecords, userCriteria, currentRecordIndex, maxRecord);

            if(userList != null && manageRole)
            {
                foreach(var user in userList)
                {
                    if(user != null && manageRole)
                        CompleteUserRole(user);
                }
            }

            return userList;
        }
        
        internal void DeleteUser(UserKey key)
        {
            _userModule.Delete(key);
        }

        internal User UpdateUser(User user)
        {
            var result = _userModule.Update(user);

            if (user.Role != null && result != null)
            {
                var userRole = new UserRole()
                {
                    UserId = user.Id,
                    RoleId = user.Role.Id
                };
                _usersRoleService.UpdateUserRole(userRole);
                CompleteUserRole(result);
            }

            return user;
        }
    }
}
