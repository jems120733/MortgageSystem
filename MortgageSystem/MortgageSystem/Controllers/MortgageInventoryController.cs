using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MortgageSystem.Controllers
{
    public class MortgageInventoryController : Controller
    {
        // GET: MortgageInventory
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "MortgageInventory";
            return View();
        }

        //Get Create
        public ActionResult Create()
        {

            return View();
        }
    }
}