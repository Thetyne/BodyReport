﻿using BodyReport.ViewModels.Framework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BodyReport.Framework.ViewComponents
{
    public class PagingComponent : ViewComponent
    {
        public PagingComponent()
        {
        }

        public Task<IViewComponentResult> InvokeAsync(string areaName, string controlerName, string actionName, int currentPage, int pageSize, int totalRecords, Dictionary<string, string> routeValues)
        {
            PagingViewModel pagin = new PagingViewModel();
            pagin.CurrentPage = currentPage;
            pagin.PageSize = pageSize;
            pagin.TotalRecords = totalRecords;
            pagin.AreaName = areaName;
            pagin.ControlerName = controlerName;
            pagin.ActionName = actionName;
            pagin.RouteValues = routeValues;
            return Task.FromResult<IViewComponentResult>(View(pagin));
        }
    }
}
