using Data;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Project.Yad2.Services
{
    public class ProductService : IProductService
    {
        private MyContext Context;

        public ProductService(MyContext context)
        {
            this.Context = context;
        }

        public void Add(ProductModel model)
        {
            Context.Products.Add(model);
            Context.SaveChanges();
        }

        public void AddProductToCart(long id, UserModel user)
        {
            ProductModel product = Context.Products.Include(p => p.Owner).Include(p => p.User).Where(p => p.Id == id).FirstOrDefault();
            product.User = user;
            product.State = MyState.ShoppingCart;
            Context.Products.Update(product);
            Context.SaveChanges();
        }

        public void Update(ProductModel model)
        {
            if (model.Id <= 0)
                return;
            Context.Products.Update(model);
            Context.SaveChanges();
        }

        public ProductModel GetById(long id)
        {
            var item = Context.Products.Find(id);
            return item;
        }

        public IEnumerable<ProductModel> GetCart(UserModel user)
        => Context.Products.Include(p => p.Owner).Include(p => p.User).Where(p => p.User == user && p.State == MyState.ShoppingCart).ToList();

        public IEnumerable<ProductModel> GetCart(List<long> productsInAnnonymusCart)
        => Context.Products.Include(p => p.Owner).Include(p => p.User).Where(p => p.State == MyState.Available && productsInAnnonymusCart.Contains(p.Id)).ToList();

        public IEnumerable<ProductModel> GetAll()
        {
            var items = Context.Products;
            return items;
        }

        public void RemoveFromCart(long id)
        {
            ProductModel item = Context.Products.Find(id);
            item.State = MyState.Available;
            Context.Products.Update(item);
            Context.SaveChanges();
        }

        public void Buy(IEnumerable<ProductModel> ProductList)
        {
            foreach (var item in ProductList)
            {
                item.State = MyState.Sold;
                Context.Update(item);
            }
            Context.SaveChanges();
        }

    }
}
