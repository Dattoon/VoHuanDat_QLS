using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoHuanDat_QLS.Models;

namespace VoHuanDat_QLS.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View(data.SACHes.ToList());
        }

        public ActionResult Sach(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(data.SACHes.ToList().OrderBy(n => n.Masach).ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]

        public ActionResult Login()
        { return View(); }


        public ActionResult Login(FormCollection collection)
        {
            string tendn = collection["username"];
            string matkhau = collection["password"];
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                Admin ad = data.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }


        [HttpGet]
        public ActionResult ThemMoiSach()
        {
            // Đưa dữ liệu vào dropdownList
            // Lấy ds từ bảng chủ đề, sắp xếp tăng dần theo tên chủ đề, chọn lấy giá trị MaCD, hiển thị là TenChuDe
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");

            // Lấy ds từ bảng nhà xuất bản, sắp xếp tăng dần theo tên nhà xuất bản, chọn lấy giá trị MaNXB, hiển thị là TenNXB
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            return View();
        }

        [HttpPost]

        public ActionResult ThemHoiSach(SACH sach, HttpPostedFileBase file)
        {
            // Giả sử 'file' là tệp đã tải lên
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/NghienCuuKhoaHoc"), fileName);

                // Kiểm tra xem đường dẫn có tồn tại trên máy chủ không
                if (!System.IO.File.Exists(path))
                {
                    // Lưu tệp tại đây bằng 'file.SaveAs(path)'
                    ViewBag.ThongBao = "Tải lên thành công";
                }
                else
                {
                    ViewBag.ThongBao = "Tệp đã tồn tại";
                }
            }
            else
            {
                ViewBag.ThongBao = "Không có tệp nào được chọn";
            }

            // Các logic liên quan đến 'sach' có thể đi vào đây

            return View();
        }
        public ActionResult ChiTietsach(int id)
        {
            // Lấy ra đối tượng sách theo mã
            var sach = data.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;

            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(sach);
        }
        public ActionResult Xoasach(int id)
        {
            // Lấy ra đối tượng sách cần xóa theo mã
            SACH sach = data.SACHes.SingleOrDefault(n => n.Masach == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return HttpNotFound("Sách không tồn tại.");
            }
            return View(sach);
        }


        [HttpPost, ActionName("Xoasach")]
        public ActionResult Xacnhanxoa(int id)
        {
            // Lấy ra đối tượng sách cần xóa theo mã
            SACH sach = data.SACHes.SingleOrDefault(n => n.Masach == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return HttpNotFound("Sách không tồn tại.");
            }

            try
            {
                data.SACHes.DeleteOnSubmit(sach);
                data.SubmitChanges();
                TempData["Message"] = "Xóa sách thành công.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi xảy ra khi xóa sách: " + ex.Message;
            }

            return RedirectToAction("Sach");
        }

        public ActionResult Suasach(int id)
        {
            // Lấy ra đối tượng sách cần xóa theo mã
            SACH sach = data.SACHes.SingleOrDefault(n => n.Masach == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", sach.MaCD);
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suasach(SACH sach, HttpPostedFileBase fileUpload)
        {
            // Dua du lieu vao dropdown
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            // Kiem tra duong dan file
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui long chon anh bia";
                return View();
            }

            // Them vao CSDL
            else
            {
                var fileName = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/AnhBia/"), fileName);

                // Kiem tra hinh anh ton tai chua?
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Thongbao += "Hinh anh da ton tai";
                }
                else
                {
                    // Luu hinh anh vao duong dan
                    fileUpload.SaveAs(path);
                    sach.Anhbia = fileName;

                    // Luu vao CSDL
                    UpdateModel(sach);
                    data.SubmitChanges();
                }
            }

            return RedirectToAction("Sach");
        }






    }
}