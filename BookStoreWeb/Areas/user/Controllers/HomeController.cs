using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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
        public IActionResult Details(int productId)
        {
            ShoppingCart cart = new()
            { 
                Product = _unitOfWork.Products.GetFirstOrDefault(i => i.Id == productId, includeProperties: "CategoryModel,CoverTypeModel"),
                Quantity = 1,
                ProductId = productId
            };

            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart obj)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            obj.UserId = claim.Value;

            var cartFromDb = _unitOfWork.ShoppingCarts
                .GetFirstOrDefault(i => i.UserId == claim.Value && i.Id == obj.ProductId);

            if (cartFromDb == null)
            {
                _unitOfWork.ShoppingCarts.Add(obj);
                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCarts.GetAll(u => u.UserId == claim.Value).ToList().Count);
                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.ShoppingCarts.IncrementQuantity(cartFromDb, obj.Quantity);
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
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