using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoHuanDat_QLS.Models;
using PagedList;
using PagedList.Mvc;
using System.Web.UI;


namespace VoHuanDat_QLS.Controllers
{
    public class SachController : BaseController
    {
        private List<SACH> Laysachmoi(int count)
        {
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }

        public ActionResult Index(int? page)
        {
            var sach = data.SACHes.ToList();
            var chude = data.CHUDEs.ToList();
            int pageSize = 3;
            int pageNum = (page ?? 1);

            var sachmoi = Laysachmoi(15);
            return View(sachmoi.ToPagedList(pageNum, pageSize));
        }
        

        public ActionResult Details(int id)
        {
            var sach = data.SACHes.SingleOrDefault(s => s.Masach == id);
            if (sach == null)
            {
                return HttpNotFound();
            }

            var vietsach = data.VIETSACHes.SingleOrDefault(vs => vs.Masach == id);
            if (vietsach != null)
            {
                var tacgia = data.TACGIAs.SingleOrDefault(tg => tg.MaTG == vietsach.MaTG);
                ViewBag.TacGia = tacgia;
            }

            return View(sach);
        }
        public ActionResult TheoChuDe(int id)
        {
            var sachTheoChuDe = data.SACHes.Where(s => s.MaCD == id)
                                                .OrderBy(s => s.Tensach) // Sắp xếp theo tên sách
                                                .ToList();
            var chuDe = data.CHUDEs.SingleOrDefault(cd => cd.MaCD == id);
            if (chuDe != null)
            {
                ViewBag.ChosenTopic = chuDe.TenChuDe;
            }
            return View(sachTheoChuDe);
        }

        public ActionResult TheoNhaXuatBan(int id)
        {
            var sachTheoNXB = data.SACHes.Where(s => s.MaNXB == id)
                                              .OrderBy(s => s.Tensach) // Sắp xếp theo tên sách
                                              .ToList();
            var nhaXuatBan = data.NHAXUATBANs.SingleOrDefault(nxb => nxb.MaNXB == id);
            if (nhaXuatBan != null)
            {
                ViewBag.ChosenPublisher = nhaXuatBan.TenNXB;
            }
            return View(sachTheoNXB);
        }





    }

}