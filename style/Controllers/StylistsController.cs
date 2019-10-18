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
        
        public ActionResult Show(int id)
        {
            Stylist stylist = _db.Stylists.Include(s => s.Clients).FirstOrDefault(s => s.StylistId == id);
            return View(stylist);        
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

        public ActionResult Delete(int id)
        {
            var thisStylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == id);
            _db.Stylists.Remove(thisStylist);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}