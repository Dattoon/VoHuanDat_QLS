using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VoHuanDat_QLS.Models;

namespace VoHuanDat_QLS.Controllers
{
    public class ChuDeController : BaseController
    {
        // GET: ChuDe
       
        public ActionResult Index()
        {
            
            return View(  );
        }
        public ActionResult DanhMucChuDe()
        {
            var chude = data.CHUDEs.ToList();
            return View(chude);
        }


        // GET: CHUDE/Create
        public ActionResult Create()
        {
            return View("CreateChuDe");
        }

        // POST: CHUDE/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenChuDe")] CHUDE chude)
        {
            if (ModelState.IsValid)
            {
                data.CHUDEs.InsertOnSubmit(chude);
                data.SubmitChanges();
                return RedirectToAction("DanhMucChuDe");
            }

            return View("DanhMucChuDe");
        }

        // GET: CHUDE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUDE chude = data.CHUDEs.SingleOrDefault(c => c.MaCD == id);
            if (chude == null)
            {
                return HttpNotFound();
            }
            return View(chude);
        }

        // POST: CHUDE/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "TenChuDe")] CHUDE chude)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var updateChuDe = data.CHUDEs.SingleOrDefault(c => c.MaCD == id);
            if (updateChuDe == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                updateChuDe.TenChuDe = chude.TenChuDe;
                data.SubmitChanges();
                return RedirectToAction("DanhMucChuDe");
            }
            return View(chude);
        }

        // GET: CHUDE/Delete/5
        public ActionResult Delete(int id)
        {
            CHUDE chude = data.CHUDEs.SingleOrDefault(c => c.MaCD == id);
            if (chude == null)
            {
                return HttpNotFound();
            }

            data.CHUDEs.DeleteOnSubmit(chude);
            data.SubmitChanges();
            return RedirectToAction("DanhMucChuDe");
        }


    }
}