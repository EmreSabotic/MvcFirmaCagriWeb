using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFirmaCagri.Models.Entity;

namespace MvcFirmaCagri.Controllers
{
    [Authorize]

    public class DefaultController : Controller
    {

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        DbİsTakipEntities db = new DbİsTakipEntities();
       
        public ActionResult AktifCagrilar()
        {
            var mail = (string)Session["Mail"];
            var id=db.TblFirmalar.Where(x=>x.Mail==mail).Select(y=>y.ID).FirstOrDefault();

            var cagrilar = db.TblCagri.Where(x=>x.Durum==true && x.CagriFirma==id).ToList();
            return View(cagrilar);
        }
        public ActionResult PasifCagrilar()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var cagrilar = db.TblCagri.Where(x => x.Durum == false && x.CagriFirma==id).ToList();
            return View(cagrilar);
        }
        [HttpGet]
        public  ActionResult YeniCagri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniCagri(TblCagri p)
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            p.Durum= true;
            p.Tarih=DateTime.Now;
            p.CagriFirma = id;
            db.TblCagri.Add(p);
            db.SaveChanges();
            return RedirectToAction("AktifCagrilar");
        }
        public ActionResult CagriDetay(int id)
        {
            var cagri = db.TblCagriDetay.Where(x=>x.cagri==id).ToList();
            return View(cagri);
        }
        public ActionResult CagriGetir(int id)
        {
            var cagri = db.TblCagri.Find(id);
            return View("CagriGetir",cagri);

        }
        public ActionResult CagriDuzenle(TblCagri p)
        {
            var cagri = db.TblCagri.Find(p.ID);
            cagri.Konu = p.Konu;
            cagri.Aciklama = p.Aciklama;
            db.SaveChanges();
            return RedirectToAction("AktifCagrilar");

                }
        [HttpGet]
        public ActionResult ProfilDuzenle()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var profil = db.TblFirmalar.Where(x=>x.ID==id).FirstOrDefault();
            return View("ProfilDuzenle",profil);
        }

        public ActionResult AnaSayfa()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var toplamcagri=db.TblCagri.Where(x=>x.CagriFirma==id).Count();
            var aktifcagri=db.TblCagri.Where(x=>x.CagriFirma==id && x.Durum==true).Count();
            var pasifcagri=db.TblCagri.Where(x=>x.CagriFirma==id && x.Durum==false).Count();
            var yetkili = db.TblFirmalar.Where(x=>x.ID==id).Select(y => y.Yetkili).FirstOrDefault();
            var sektor = db.TblFirmalar.Where(x=>x.ID==id).Select(y => y.Sektor).FirstOrDefault();
          
            ViewBag.c1 = toplamcagri;
            ViewBag.c2 = aktifcagri;
            ViewBag.c3 = pasifcagri;
            ViewBag.c4 = yetkili;
            ViewBag.c5 = sektor;

            return View();
        }
        public PartialViewResult Partial1()
        {
            var mail = (string)Session["Mail"];
            var mesajlar = db.tblMesajlar.Where(x => x.Alici == mail && x.Durum==true).ToList();
            var mesajsayisi = db.tblMesajlar.Where(x=>x.Alici == mail && x.Durum==true).Count();
            ViewBag.m1 = mesajsayisi;
            return PartialView(mesajlar);
        }
    }
}