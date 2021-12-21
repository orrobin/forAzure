using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Project.Yad2.Services
{
    public interface IProductService
    {
        void Add(ProductModel model);

        void AddProductToCart(long id, UserModel user);

        void Update(ProductModel model);

        ProductModel GetById(long id);

        IEnumerable<ProductModel> GetCart(UserModel user);

        IEnumerable<ProductModel> GetCart(List<long> productIDs);

        IEnumerable<ProductModel> GetAll();

        void RemoveFromCart(long id);

        void Buy(IEnumerable<ProductModel> ProductList);
    }
}
