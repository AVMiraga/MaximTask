using MaximTask.Business.Services.Interfaces;
using MaximTask.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MaximTask.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceService _service;

        public HomeController(IServiceService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Service> services = await _service.GetAllAsync(); 

            return View(services);
        }
    }
}
