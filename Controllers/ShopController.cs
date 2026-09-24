using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers 
{
    public class ShopController : Controller
    {
        // Trang danh sách sản phẩm (SHOP)
        public IActionResult Index()
        {
            return View();
        }

        // Trang chi tiết sản phẩm (PRODUCT DETAILS)
        public IActionResult Details()
        {
            return View();
        }
    }
}