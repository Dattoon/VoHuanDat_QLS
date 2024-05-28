using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoHuanDat_QLS.Models;

namespace VoHuanDat_QLS.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base

        protected DataClasses1DataContext data = new DataClasses1DataContext("DATTOON\\DATER");

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            ViewBag.ChuDes = data.CHUDEs.ToList();
            ViewBag.NhaXuatBans = data.NHAXUATBANs.ToList();
            


        }
        public BaseController()
        {

            ViewBag.ChuDes = data.CHUDEs.ToList();
            ViewBag.NhaXuatBans = data.NHAXUATBANs.ToList();
        }

       

    }
}