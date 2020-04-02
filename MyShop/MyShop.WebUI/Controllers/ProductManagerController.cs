﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        ProductCategoryRepository productCategoryRepo;
        public ProductManagerController()
        {
            context = new ProductRepository();
            productCategoryRepo = new ProductCategoryRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> productList = context.Collection().ToList();
            return View(productList);
        }

        public ActionResult Create()
        {
            //Product product = new Product();
            ProductManagerViewModel productManagerViewModel = new ProductManagerViewModel();
            productManagerViewModel.Product = new Product();
            productManagerViewModel.ProductCategoryList = productCategoryRepo.Collection();
            return View(productManagerViewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            ProductManagerViewModel productManagerViewModel = new ProductManagerViewModel();
            productManagerViewModel.Product = product;
            productManagerViewModel.ProductCategoryList = productCategoryRepo.Collection();
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productManagerViewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product,string Id)
        {
            Product productToEdit = context.Find(Id);
            if(productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if(productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if(productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}