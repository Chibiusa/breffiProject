using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestTask.ModelsFromDB;

namespace TestTask.Controllers
{
    public class BrandController:Controller
    {
        CSVQuery cq = new CSVQuery(@"C:\Users\Asus\Desktop\BrandCSV.csv");
        public ActionResult Index()
        {
            return View(cq.Get());
        }
    }
}