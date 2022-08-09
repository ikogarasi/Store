using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStoreWeb.Areas.user.Controllers
{
    [Area("user")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<ProductModel> listOfProducts = _unitOfWork.Products.GetAll(includeProperties:"CategoryModel,CoverTypeModel");
            return View(listOfProducts);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            ShoppingCartVM cartObj = new()
            { 
                Product = _unitOfWork.Products.GetFirstOrDefault(i => i.Id == id, includeProperties: "CategoryModel,CoverTypeModel"),
                Quantity = 1,
            };

            return View(cartObj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}