﻿using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using System.Globalization;
using Microsoft.Extensions.Localization;
using BodyReport.Framework;

namespace BodyReport.Areas.Site.Controllers
{
    [Area("Site")]
    public class HomeController : Controller
    {
        private IStringLocalizer _htmlLocalizer;

        public HomeController()
        {
            StringLocalizerFactory sl = new StringLocalizerFactory();
            _htmlLocalizer = sl.Create("Translation", "Resources");
        }

        //
        // GET: /Site/Home/Index
        public IActionResult Index()
        {
            return View();
        }

        //
        // GET: /Site/Home/About
        public IActionResult About()
        {
            //ViewData["Message"] = "My Sport Report WebSite.";

            ViewData["Message"] = _htmlLocalizer["HELLO"];
            ViewData["CurrentCulture"] = CultureInfo.CurrentCulture.ToString();
            ViewData["CurrentUICulture"] = CultureInfo.CurrentUICulture.ToString();

            return View();
        }

        //
        // GET: /Site/Home/Contact
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        //
        // GET: /Site/Home/Error
        public IActionResult Error()
        {
            return View();
        }
        
        //
        // GET: /Site/Home/Json
        public IActionResult Json()
        {
            List<string> dataList = new List<string>();
            for(int i=0; i < 50; i++)
            {
                dataList.Add("test " + i);
            }
            return Json(dataList);
        }
    }
}