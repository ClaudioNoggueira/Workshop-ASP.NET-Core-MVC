using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (!ModelState.IsValid) {
                var departments = departmentService.FindAll();
                var viewModel = new SellerFormViewModel {
                    Seller = seller,
                    Departments = departments
                };
                return View(viewModel);
            }
            sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = sellerService.FindById(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {
            sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = sellerService.FindById(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        public IActionResult Edit(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = sellerService.FindById(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            List<Department> departments = departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel {
                Seller = obj,
                Departments = departments
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller) {
            if (!ModelState.IsValid) {
                var departments = departmentService.FindAll();
                var viewModel = new SellerFormViewModel {
                    Seller = seller,
                    Departments = departments
                };
                return View(viewModel);
            }
            if (id != seller.Id) {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try {
                sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e) {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e) {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(String message) {
            var viewModel = new ErrorViewModel {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

    }
}
