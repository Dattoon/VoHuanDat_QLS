using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoHuanDat_QLS.Models;

namespace VoHuanDat_QLS.Controllers
{
    public class HomeController : BaseController
    {

        

        public ActionResult Index()
        {
            
            var sach = data.SACHes.ToList();
            var chude = data.CHUDEs.ToList();
            var nxb = data.NHAXUATBANs.ToList();

            return View(sach);
        }




        
    }
}