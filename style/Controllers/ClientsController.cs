using Microsoft.AspNetCore.Mvc;
using Style.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Style.Controllers
{
    public class ClientsController : Controller
    {
        private readonly StyleContext _db;

        public ClientsController(StyleContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Client> clientlist = _db.Clients.Include(a => a.Stylist).ToList();
            return View(clientlist);
        }

        public ActionResult Create()
        {
            ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Client client)
        {
            _db.Clients.Add(client);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}