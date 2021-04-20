using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Extensions;
using Uplift.Models;
using Uplift.Models.ViewModels;
using Uplift.Utility;

namespace Uplift.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public CartViewModel CartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CartVM = new CartViewModel()
            {
                OrderHeader = new Models.OrderHeader(),
                ServicesList = new List<Models.Service>(),
            };
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> cartItemsList = new List<int>();
                cartItemsList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);

                foreach (var service in cartItemsList)
                {
                    CartVM.ServicesList.Add(_unitOfWork.Service.GetFirstOrDefault(filter:x=>x.Id == service, includeProperties: "Category,Frequency"));
                }
            }

            return View(CartVM);
        }
        [HttpGet]
        public IActionResult Summary()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> cartItemsList = new List<int>();
                cartItemsList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);

                foreach (var service in cartItemsList)
                {
                    CartVM.ServicesList.Add(_unitOfWork.Service.GetFirstOrDefault(filter: x => x.Id == service, includeProperties: "Category,Frequency"));
                }
            }

            return View(CartVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public IActionResult SummaryPOST()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> cartItemsList = new List<int>();
                cartItemsList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);

                CartVM.ServicesList = new List<Service>();

                foreach (var service in cartItemsList)
                {
                    CartVM.ServicesList.Add(_unitOfWork.Service.Get(service));
                }
            }

            if (!ModelState.IsValid)
            {
                return View(CartVM);
            }

            CartVM.OrderHeader.OrderDate = DateTime.Now;
            CartVM.OrderHeader.Status = SD.StatusSubmitted;
            CartVM.OrderHeader.ServiceCount = CartVM.ServicesList.Count();

            _unitOfWork.OrderHeader.Add(CartVM.OrderHeader);

            _unitOfWork.Save();

            foreach (var service in CartVM.ServicesList)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    ServiceId = service.Id,
                    OrderHeaderId = CartVM.OrderHeader.Id,
                    ServiceName = service.Name,
                    Price = service.Price
                };

                _unitOfWork.OrderDetails.Add(orderDetail);

            }

            _unitOfWork.Save();

            HttpContext.Session.SetObject(SD.SessionCart, new List<int>());

            return RedirectToAction("OrderConfirmation", "Cart", new { id = CartVM.OrderHeader.Id});
        }

        public IActionResult OrderConfirmation(int Id)
        {
            return View(Id);
        }

        public IActionResult Remove(int serviceId)
        {
            List<int> cartItemsList = new List<int>();

            cartItemsList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);

            cartItemsList.Remove(serviceId);

            HttpContext.Session.SetObject(SD.SessionCart, cartItemsList);

            return RedirectToAction(nameof(Index));
        }
    }
}