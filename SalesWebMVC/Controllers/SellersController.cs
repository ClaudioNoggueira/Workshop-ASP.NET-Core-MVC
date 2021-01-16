using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Controllers {
    public class SellersController : Controller {

        private readonly SellerService sellerService;
        private readonly DepartmentService departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService) {
            this.sellerService = sellerService;
            this.departmentService = departmentService;
        }
        public IActionResult Index() {
            var list = sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create() {
            var departments = departmentService.FindAll();
            var viewModel = new SellerFormViewModel {
                Departments = departments
        };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) {
            sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}
