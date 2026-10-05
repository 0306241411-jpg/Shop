using Microsoft.AspNetCore.Mvc;
using Shop.Models;

namespace Shop.Controllers
{
    public class ShopController : Controller
    {
        private const string CART_KEY = "MY_SHOP_CART";

        private List<CartItem> GetMockProducts()
        {
            return new List<CartItem>
            {
                new CartItem { ProductId = 1, ProductName = "Jeans midi cocktail dress 1", Price = 39.90m, ImageUrl = "/karl/img/product-img/product-1.jpg" },
                new CartItem { ProductId = 2, ProductName = "Jeans midi cocktail dress 2", Price = 45.00m, ImageUrl = "/karl/img/product-img/product-2.jpg" },
                new CartItem { ProductId = 3, ProductName = "Jeans midi cocktail dress 3", Price = 50.00m, ImageUrl = "/karl/img/product-img/product-3.jpg" },
                new CartItem { ProductId = 4, ProductName = "Jeans midi cocktail dress 4", Price = 29.90m, ImageUrl = "/karl/img/product-img/product-4.jpg" },
                new CartItem { ProductId = 5, ProductName = "Jeans midi cocktail dress 5", Price = 39.90m, ImageUrl = "/karl/img/product-img/product-5.jpg" },
                new CartItem { ProductId = 6, ProductName = "Jeans midi cocktail dress 6", Price = 39.90m, ImageUrl = "/karl/img/product-img/product-6.jpg" },
                new CartItem { ProductId = 7, ProductName = "Jeans midi cocktail dress 7", Price = 39.90m, ImageUrl = "/karl/img/product-img/product-7.jpg" },
                new CartItem { ProductId = 8, ProductName = "Jeans midi cocktail dress 8", Price = 39.90m, ImageUrl = "/karl/img/product-img/product-8.jpg" },
                new CartItem { ProductId = 9, ProductName = "Jeans midi cocktail dress 9", Price = 39.90m, ImageUrl = "/karl/img/product-img/product-9.jpg" }
            };
        }

        private List<CartItem> GetCartFromSession()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(CART_KEY);
            return cart ?? new List<CartItem>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = id;
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var cart = GetCartFromSession();
            var mockProducts = GetMockProducts();

            // Tìm chính xác sản phẩm theo ID
            var product = mockProducts.FirstOrDefault(p => p.ProductId == id);

            if (product != null)
            {
                var existingItem = cart.FirstOrDefault(c => c.ProductId == product.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity += quantity; // Cộng dồn số lượng nếu đã có trong giỏ
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Price = product.Price,
                        ImageUrl = product.ImageUrl,
                        Quantity = quantity
                    });
                }

                HttpContext.Session.SetObject(CART_KEY, cart);
            }

            return RedirectToAction("Cart");
        }

        public IActionResult Cart()
        {
            var cart = GetCartFromSession();
            return View(cart);
        }

        public IActionResult Checkout()
        {
            var cart = GetCartFromSession();
            return View(cart);
        }
    }
}