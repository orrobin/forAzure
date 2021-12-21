using Asp.Net.Project.Yad2.Models;
using Asp.Net.Project.Yad2.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Project.Yad2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService ProductService;

        public HomeController(ILogger<HomeController> logger, IProductService service)
        {
            this.ProductService = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<ProductModel> ProductList = ProductService.GetAll();
            if(TempData["sortType"] !=null)
            {
                if(TempData["sortType"].Equals("Date"))
                {
                  ProductList =  ProductList.OrderBy(p => p.Date);
                }
                else if(TempData["sortType"].Equals("Name"))
                {
                  ProductList = ProductList.OrderBy(p => p.Title);
                }
            }
            return View(ProductList.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var model = ProductService.GetById(id);
            if (model == null)
                return NotFound();
            return Ok(model);
        }

        [HttpPost("{id}")]
        public IActionResult Save(ProductModel model)
        {
            ProductService.Update(model);
            return Ok();
        }

        [HttpGet("new")]
        public IActionResult Add()
        {
            var item = new ProductModel();
            return View(item);
        }

        [HttpPost("new")]
        public IActionResult Add(ProductModel model)
        {
            ProductService.Add(model);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Sorting(string sort)
        {
            TempData["sortType"] = sort;
            return RedirectToAction("Index", "Home");
        }
    }
}
