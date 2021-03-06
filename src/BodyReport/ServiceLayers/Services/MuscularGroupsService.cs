﻿using BodyReport.Data;
using BodyReport.Framework;
using BodyReport.Manager;
using BodyReport.Message;
using BodyReport.ServiceLayers.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BodyReport.ServiceLayers.Services
{
    public class MuscularGroupsService : BodyExercisesService, IMuscularGroupsService
    {
        private const string _cacheName = "MuscularGroupsCache";
        /// <summary>
        /// Logger
        /// </summary>
        private static ILogger _logger = WebAppConfiguration.CreateLogger(typeof(MuscularGroupsService));

        public MuscularGroupsService(ApplicationDbContext dbContext, ICachesService cacheService) : base(dbContext, cacheService)
        {
        }

        public MuscularGroup CreateMuscularGroup(MuscularGroup muscularGroup)
        {
            MuscularGroup result = null;
            BeginTransaction();
            try
            {
                result = GetMuscularGroupManager().CreateMuscularGroup(muscularGroup);
                CommitTransaction();
                //invalidate cache
                InvalidateCache(_cacheName);
            }
            catch (Exception exception)
            {
                _logger.LogCritical("Unable to create muscular group", exception);
                RollbackTransaction();
                throw exception;
            }
            finally
            {
                EndTransaction();
            }
            return result;
        }

        public MuscularGroup GetMuscularGroup(MuscularGroupKey key)
        {
            MuscularGroup muscularGroup = null;
            string cacheKey = key == null ? "MuscularGroupKey_null" : key.GetCacheKey();
            if (key != null && !TryGetCacheData(cacheKey, out muscularGroup))
            {
                muscularGroup = GetMuscularGroupManager().GetMuscularGroup(key);
                SetCacheData(_cacheName, cacheKey, muscularGroup);
            }
            return muscularGroup;
        }

        public List<MuscularGroup> FindMuscularGroups()
        {
            List<MuscularGroup> muscularGroupList = null;
            //string cacheKey = criteria == null ? "MuscularGroupCriteria_null" : criteria.GetCacheKey();
            string cacheKey = "MuscularGroupCriteria_null";
            if (!TryGetCacheData(cacheKey, out muscularGroupList))
            {
                muscularGroupList = GetMuscularGroupManager().FindMuscularGroups();
                SetCacheData(_cacheName, cacheKey, muscularGroupList);
            }
            return muscularGroupList;
        }

        public void DeleteMuscularGroup(MuscularGroupKey key)
        {
            BeginTransaction();
            try
            {
                GetMuscularGroupManager().DeleteMuscularGroup(key);
                CommitTransaction();
                //invalidate cache
                InvalidateCache(_cacheName);
            }
            catch (Exception exception)
            {
                _logger.LogCritical("Unable to delete muscular group", exception);
                RollbackTransaction();
                throw exception;
            }
            finally
            {
                EndTransaction();
            }
        }

        public MuscularGroup UpdateMuscularGroup(MuscularGroup muscularGroup)
        {
            MuscularGroup result = null;
            BeginTransaction();
            try
            {
                result = GetMuscularGroupManager().UpdateMuscularGroup(muscularGroup);
                CommitTransaction();
                //invalidate cache
                InvalidateCache(_cacheName);
            }
            catch (Exception exception)
            {
                _logger.LogCritical("Unable to update muscular group", exception);
                RollbackTransaction();
                throw exception;
            }
            finally
            {
                EndTransaction();
            }
            return result;
        }

        public List<MuscularGroup> UpdateMuscularGroupList(List<MuscularGroup> muscularGroups)
        {
            List<MuscularGroup> result = null;
            BeginTransaction();
            try
            {
                if (muscularGroups != null && muscularGroups.Count > 0)
                {
                    result = new List<MuscularGroup>();
                    foreach (var muscularGroup in muscularGroups)
                    {
                        result.Add(GetMuscularGroupManager().UpdateMuscularGroup(muscularGroup));
                    }
                    //invalidate cache
                    InvalidateCache(_cacheName);
                }

                CommitTransaction();
            }
            catch (Exception exception)
            {
                _logger.LogCritical("Unable to update muscular group list", exception);
                RollbackTransaction();
                throw exception;
            }
            finally
            {
                EndTransaction();
            }
            return result;
        }
    }
}
