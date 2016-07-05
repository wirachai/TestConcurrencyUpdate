using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestConcurrencyUpdate.ViewModels;
using TestConcurrencyUpdate.Models;

namespace TestConcurrencyUpdate.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Edit(int id)
        {
            var model = new ProductViewModel();
            using (var db = new ProductEntities())
            {
                var product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                model.ProductId = product.ProductId;
                model.ProductName = product.ProductName;
                model.RowVersion = product.RowVersion;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            using (var db = new ProductEntities())
            {
                var product = db.Products.Find(model.ProductId);
                if (product == null)
                {
                    ModelState.AddModelError("", "The record you are trying to update was deleted by another user.");
                    return View(model);
                }
                if (Convert.ToBase64String(product.RowVersion) != Convert.ToBase64String(model.RowVersion))
                {
                    ModelState.AddModelError("", "The record you want to edit has been updated by another user.");
                    return View(model);
                }
                product.ProductName = model.ProductName;
                db.SaveChanges();
            }

            return RedirectToAction("Edit", model.ProductId);
        }
    }
}