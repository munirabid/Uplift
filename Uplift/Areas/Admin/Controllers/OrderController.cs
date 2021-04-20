using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models.ViewModels;
using Uplift.Utility;

namespace Uplift.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            OrderViewModel orderViewModel = new OrderViewModel
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(id),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(filter:x=>x.OrderHeaderId == id)
            };

            if (orderViewModel == null)
            {
                return NotFound();
            }

            return View(orderViewModel);
        }

        public IActionResult Approve(int id)
        {
            var orderFromDb = _unitOfWork.OrderHeader.Get(id);

            if (orderFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.OrderHeader.ChangeOrderStatus(id, SD.StatusApproved);

            return View(nameof(Index));
        }

        public IActionResult Reject(int id)
        {
            var orderFromDb = _unitOfWork.OrderHeader.Get(id);

            if (orderFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.OrderHeader.ChangeOrderStatus(id, SD.StatusRejected);

            return View(nameof(Index));
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll()});
        }

        [HttpGet]
        public IActionResult GetAllPendingOrder()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter:o=>o.Status == SD.StatusSubmitted) });
        }

        [HttpGet]
        public IActionResult GetAllApprovedStatus()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: o => o.Status == SD.StatusApproved) });
        }


        #endregion
    }
}