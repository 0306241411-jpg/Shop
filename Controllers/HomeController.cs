using Microsoft.AspNetCore.Mvc;
using Shop.Models;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Truyền 9 sản phẩm mẫu sang View Trang chủ
            var products = new List<CartItem>
            {
                new CartItem { ProductId = 1, ProductName = "Jeans midi cocktail dress 1", Price = 39.90m, ImageUrl = "/karl/img/product-img/product-1.jpg" },
                new CartItem { ProductId = 2, ProductName = "Jeans midi cocktail dress 2", Price = 45.00m, ImageUrl = "/karl/img/product-img/product-2.jpg" },
                new CartItem { ProductId = 3, ProductName = "Jeans midi cocktail dress 3", Price = 50.00m, ImageUrl = "/karl/img/product-img/product-3.jpg" },
                new CartItem { ProductId = 4, ProductName = "Jeans midi cocktail dress 4", Price = 29.90m, ImageUrl = "/karl/img/product-img/product-4.jpg" }
            };

            return View(products);
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}