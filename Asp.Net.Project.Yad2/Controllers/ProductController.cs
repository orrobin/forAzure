using Asp.Net.Project.Yad2.Services;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Project.Yad2.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService ProductService;
        private readonly IUserService UserService;

        public ProductController(IProductService productService, IUserService userService)
        {
            ProductService = productService;
            UserService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewProduct(ProductModel product)
        {
            return View(product);
        }

        [HttpPost]
        public IActionResult AddProduct(ProductModel product, List<IFormFile> Images)
        {
            if (!ModelState.IsValid)
                return View("New_Product", product);

            for (int i = 0; i < Images.Count; i++)
            {
                if (Images[i].Length > 0)
                {
                    using MemoryStream ms = new MemoryStream();
                    Images[i].CopyTo(ms);
                    product.GetType().GetProperty($"Picture{i + 1}").SetValue(product, ms.ToArray());
                }
            }
            product.State = MyState.Available;
            string[] UsernamePassword = HttpContext.Request.Cookies["AspProjectCookie"].Split(',');
            product.Owner = product.User = UserService.GetUser(UsernamePassword[0], UsernamePassword[1]);
            product.Date = DateTime.Now;
            ProductService.Add(product);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult MoreDetails(long Id)
        {
            return View(ProductService.GetById(Id));
        }

        public IActionResult AddToCart(long id)
        {
            if (HttpContext.Request.Cookies.ContainsKey("AspProjectCookie"))
            {
                string[] UsernamePassword = HttpContext.Request.Cookies["AspProjectCookie"].Split(',');
                ProductService.AddProductToCart(id, UserService.GetUser(UsernamePassword[0], UsernamePassword[1]));
            }
            else if (HttpContext.Request.Cookies.ContainsKey("AspProjectGuestCart"))
                HttpContext.Response.Cookies.Append("AspProjectGuestCart", HttpContext.Request.Cookies["AspProjectGuestCart"] + $",{id}", new CookieOptions() { Expires = DateTime.Now.AddDays(7) });
            else
                HttpContext.Response.Cookies.Append("AspProjectGuestCart", $"{id}", new CookieOptions() { Expires = DateTime.Now.AddDays(7) });
            return RedirectToAction("Index", "Home");

        }

        public IActionResult RemoveFromCart(long id)
        {
            ProductService.RemoveFromCart(id);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Cart()
        {
            if (HttpContext.Request.Cookies.ContainsKey("AspProjectCookie"))
            {
                string[] UsernamePassword = HttpContext.Request.Cookies["AspProjectCookie"].Split(',');
                return View(ProductService.GetCart(UserService.GetUser(UsernamePassword[0], UsernamePassword[1])));
            }
            else
            {
                if (HttpContext.Request.Cookies.ContainsKey("AspProjectGuestCart"))
                {
                    List<long> ProductIDs = new List<long>();
                    HttpContext.Request.Cookies["AspProjectGuestCart"].Split(',').ToList().ForEach(idstring => ProductIDs.Add(long.Parse(idstring))); ;
                    return View(ProductService.GetCart(ProductIDs));
                }
                else
                {
                    return View(Enumerable.Empty<ProductModel>());
                }
            }
        }
        public IActionResult Buy()
        {
            if (HttpContext.Request.Cookies.ContainsKey("AspProjectCookie"))
            {
                string[] UsernamePassword = HttpContext.Request.Cookies["AspProjectCookie"].Split(',');
                ProductService.Buy((ProductService.GetCart(UserService.GetUser(UsernamePassword[0], UsernamePassword[1]))));
            }
            else
            {
                if (HttpContext.Request.Cookies.ContainsKey("AspProjectGuestCart"))
                {
                    List<long> ProductIDs = new List<long>();
                    HttpContext.Request.Cookies["AspProjectGuestCart"].Split(',').ToList().ForEach(idstring => ProductIDs.Add(long.Parse(idstring))); ;
                    ProductService.Buy((ProductService.GetCart(ProductIDs)));
                }
            }
            return RedirectToAction("Index", "Home");

        }

    }
}
