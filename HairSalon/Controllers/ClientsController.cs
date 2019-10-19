using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace HairSalon.Controllers
{
    public class ClientsController : Controller
    {
        private readonly HairSalonContext _db;

        public ClientsController(HairSalonContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Client> clientlist = _db.Clients.Include(client => client.Stylist).ToList();
            return View(clientlist);
        }

        public ActionResult Create()
        {
            ViewBag.NoStylistErrorMessage = "";
            if (_db.Stylists.ToList().Count == 0)
            {
                ViewBag.NoStylistErrorMessage = "Please add a stylist first!";
            }
            ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Client client)
        {
            Console.WriteLine("StylistID: " + client.StylistId);
            _db.Clients.Add(client);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Show(int id)
        {
            Client thisClient = _db.Clients.FirstOrDefault(client => client.ClientId == id);
            var thisStylist = _db.Stylists.FirstOrDefault(Stylist => Stylist.StylistId == thisClient.StylistId);
            ViewBag.StylistName = thisStylist.Name;
            return View(thisClient);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name");
            var thisClient = _db.Clients.FirstOrDefault(client => client.ClientId == id);
            return View(thisClient);
        }

        [HttpPost]
        public ActionResult Edit(Client client)
        {
            _db.Entry(client).State = EntityState.Modified;
            _db.SaveChanges();
            
            return RedirectToAction("Show", "Clients", new {id = client.ClientId});
        }
        
        public ActionResult Delete(int id)
        {
            var thisClient = _db.Clients.FirstOrDefault(client => client.ClientId == id);
            _db.Clients.Remove(thisClient);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}