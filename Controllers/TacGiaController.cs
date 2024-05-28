using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VoHuanDat_QLS.Models;

namespace VoHuanDat_QLS.Controllers
{
    public class TacGiaController : BaseController
    {
       
        
        // GET: TacGia
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult DanhMucTacGia()
        {
            var tacgia = data.TACGIAs.ToList();
            return View(tacgia);
        }

        // GET: TacGia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TacGia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenTG,DiaChi,TieuSu,DienThoai")] TACGIA tacgia)
        {
            if (ModelState.IsValid)
            {
                data.TACGIAs.InsertOnSubmit(tacgia);
                data.SubmitChanges();
                return RedirectToAction("DanhMucTacGia");
            }

            return View(tacgia);
        }

        // GET: TacGia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TACGIA tacgia = data.TACGIAs.SingleOrDefault(tg => tg.MaTG == id);
            if (tacgia == null)
            {
                return HttpNotFound();
            }
            return View(tacgia);
        }

        // POST: TacGia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "TenTG,DiaChi,TieuSu,DienThoai")] TACGIA tacgia)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var updateTacGia = data.TACGIAs.SingleOrDefault(tg => tg.MaTG == id);
            if (updateTacGia == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                updateTacGia.TenTG = tacgia.TenTG;
                
                updateTacGia.Diachi = tacgia.Diachi;
                updateTacGia.Tieusu = tacgia.Tieusu;
                updateTacGia.Dienthoai = tacgia.Dienthoai;
                data.SubmitChanges();
                return RedirectToAction("DanhMucTacGia");
            }
            return View(tacgia);
        }

        // GET: TacGia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TACGIA tacgia = data.TACGIAs.SingleOrDefault(tg => tg.MaTG == id);
            if (tacgia == null)
            {
                return HttpNotFound();
            }
            data.TACGIAs.DeleteOnSubmit(tacgia);
            data.SubmitChanges();
            return RedirectToAction("DanhMucTacGia");
        }
    }

}