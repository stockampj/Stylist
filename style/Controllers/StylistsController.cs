using Microsoft.AspNetCore.Mvc;
using Style.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Style.Controllers
{
    public class StylistsController : Controller
    {
        private readonly StyleContext _db;

        public StylistsController(StyleContext db)
        {
            _db = db;
        }


        public ActionResult Index()
        {
            List<Stylist> model = _db.Stylists.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();        
        }

        [HttpPost]
        public ActionResult Create(Stylist stylist)
        {
            if (stylist.Name != null)
            {
                _db.Stylists.Add(stylist);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        public ActionResult Details(int id)
        {
            Stylist et = _db.Stylists.Include(e => e.Clients).FirstOrDefault(e => e.StylistId == id);
            return View(et);        
        }

        public ActionResult Edit(int id)
        {
            var thisET = _db.Stylists.FirstOrDefault(e => e.StylistId == id);
            return View(thisET);
        }
        [HttpPost]
        public ActionResult Edit(Stylist ET)
        {
            _db.Entry(ET).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}