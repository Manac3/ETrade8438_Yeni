using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers
{
    public class ProductsController : Controller
    {
        //root neydi hatırlayalım controller/action/id?

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index() // /Products/Index dicez gelecek bu default zaten 
        {
            var model = _productService.Query().ToList();
            return View(model);
        }
    }
}
