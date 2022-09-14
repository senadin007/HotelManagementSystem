using HotelManagementSystem.Core.IConfiguration;
using HotelManagementSystem.Models;
using HotelManagementSystem.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MvcContrib.UI.InputBuilder.Views;

namespace HotelManagementSystem.Controllers
{
    public class HotelController : BaseController
    {
        private readonly ILogger<HotelController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HotelController(ILogger<HotelController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _unitOfWork.Hotel.GetAll();
            return View("Index", data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Hotel model)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Hotel.Add(model);
                await _unitOfWork.CompleteAsync();

                return RedirectToAction("Index");
            }
            return new JsonResult("Something went wrong.") { StatusCode = 500 };
        }
        [HttpGet]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var hotel = await _unitOfWork.Hotel.GetById(id);

            if(hotel == null)
                return NotFound();
            return Ok(hotel);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hotel = await _unitOfWork.Hotel.GetAll();
            return Ok(hotel);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateItem(Guid Id,Hotel model)
        {
            if (Id != model.Id)
                return BadRequest();
            await _unitOfWork.Hotel.Update(model);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
        public async Task<ActionResult> Delete(Guid Id)
        {
            var recordItem = await _unitOfWork.Hotel.GetById(Id);
            if (recordItem == null)
            {
                return NotFound();
            }
            await _unitOfWork.Hotel.Remove(Id);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var recordItem = await _unitOfWork.Hotel.GetById(Id);
            if (recordItem == null)
            {
                return NotFound();
            }
            return View("Edit", recordItem);
        }
        public async Task<IActionResult> Details(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var recordItem = await _unitOfWork.Hotel.GetById(Id);
            if (recordItem == null)
            {
                return NotFound();
            }
            return View("Details", recordItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Hotel model)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Hotel.Update(model);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> HotelSearch(double longitude, double latitude, int page, int size)
        {
            var command = new GetHotelsQuery(
                longitude: longitude,
                latitude: latitude,
                pageNumber: page,
                pageSize: size);

            var data = await Mediator.Send(command);
            return View(data);
        }
        
    }
}
