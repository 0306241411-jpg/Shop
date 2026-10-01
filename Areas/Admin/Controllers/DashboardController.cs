using Microsoft.AspNetCore.Mvc;

namespace Shop.Areas.Admin.Controllers
{
    [Area("Admin")] // Khai báo thuộc tính Area bắt buộc
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}