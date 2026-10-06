using Microsoft.AspNetCore.Mvc;
using Shop.Models;

namespace Shop.Controllers
{
    public class ShopController : Controller
    {
        private const string CART_KEY = "MY_SHOP_CART";
        private static int currentCartQty = 1;

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

        // Action hiển thị trang chi tiết sản phẩm
        public IActionResult Details(int id = 1)
        {
            string[] prices = { "$39.90", "$45.00", "$50.00", "$35.00", "$49.90", "$60.00" };
            int index = (id >= 1 && id <= prices.Length) ? id - 1 : 0;

            ViewBag.ProductId = id;
            ViewBag.ProductName = $"Jeans midi cocktail dress {id}";
            ViewBag.ProductPrice = prices[index];
            ViewBag.ProductImage = $"/karl/img/product-img/product-{id}.jpg";

            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var cart = GetCartFromSession();
            var mockProducts = GetMockProducts();

            var product = mockProducts.FirstOrDefault(p => p.ProductId == id);

            if (product != null)
            {
                var existingItem = cart.FirstOrDefault(c => c.ProductId == product.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
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
                currentCartQty = cart.Sum(c => c.Quantity);
            }

            return RedirectToAction("Cart");
        }

        // Hiển thị giỏ hàng
        public IActionResult Cart()
        {
            ViewBag.Quantity = currentCartQty;
            ViewBag.CartCount = currentCartQty;
            return View();
        }

        // Cập nhật số lượng (+ / -)
        public IActionResult UpdateCart(int id, int quantity)
        {
            // GIỮ NGUYÊN TỐI THIỂU LÀ 1: Nếu bấm trừ khi đang là 1 thì vẫn giữ là 1, không cho về 0
            if (quantity < 1)
            {
                quantity = 1;
            }

            currentCartQty = quantity;

            // Đồng bộ lại dữ liệu trong Session nếu có
            var cart = GetCartFromSession();
            var item = cart.FirstOrDefault(c => c.ProductId == id);
            if (item != null)
            {
                item.Quantity = quantity;
                HttpContext.Session.SetObject(CART_KEY, cart);
            }

            return RedirectToAction("Cart");
        }

        // CHỈ XÓA SẢN PHẨM KHI BẤM NÚT THÙNG RÁC
        public IActionResult RemoveFromCart(int id)
        {
            currentCartQty = 0;
            HttpContext.Session.Remove(CART_KEY); // Xóa sạch giỏ hàng khi bấm xóa
            return RedirectToAction("Cart");
        }

        public IActionResult Checkout()
        {
            var cart = GetCartFromSession();
            return View(cart);
        }
    }
}