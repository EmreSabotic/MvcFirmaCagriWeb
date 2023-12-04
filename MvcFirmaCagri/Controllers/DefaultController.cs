﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFirmaCagri.Models.Entity;

namespace MvcFirmaCagri.Controllers
{
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
            var cagrilar = db.TblCagri.Where(x=>x.Durum==true).ToList();
            return View(cagrilar);
        }
        public ActionResult PasifCagrilar()
        {
            var cagrilar = db.TblCagri.Where(x => x.Durum == false).ToList();
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
            p.Durum= true;
            p.Tarih=DateTime.Now;
            p.CagriFirma = 4;
            db.TblCagri.Add(p);
            db.SaveChanges();
            return RedirectToAction("AktifCagrilar");
        }
    }
}