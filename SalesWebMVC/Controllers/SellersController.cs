using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Controllers {
    public class SellersController : Controller {

        private readonly SellerService sellerService;

        public SellersController(SellerService sellerService) {
            this.sellerService = sellerService;
        }
        public IActionResult Index() {
            var list = sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) {
            sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}
