using CarDealership.Data;
using CarDealership.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealership.Controllers
{
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cars = _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.CarColor)
                .Include(c => c.Photos)
                .ToList();

            return View(cars);
        }

        // Add Car
        public IActionResult Create()
        {
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.CarColors = _context.CarColors.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Create(Car car, List<string> photos)
        {
            if (ModelState.IsValid)
            {
                // Save the car to the database
                _context.Cars.Add(car);
                _context.SaveChanges();

                // Check if photos are provided
                if (photos != null && photos.Any())
                {
                    foreach (var photoUrl in photos)
                    {
                        var photoModel = new Photo
                        {
                            CarId = car.CarId,
                            Url = photoUrl
                        };

                        // Save each photo to the database
                        _context.Photos.Add(photoModel);
                    }

                    _context.SaveChanges();
                }

                // Return a JSON result with success status and redirection URL
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Car") });
            }

            // Repopulate dropdowns or other data as needed
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.CarColors = _context.CarColors.ToList();

            // Return a JSON result with success status (false in this case)
            return Json(new { success = false });
        }




        [HttpGet]
        public IActionResult GetModels(int brandId)
        {
            var models = _context.Models
                .Where(m => m.BrandId == brandId)
                .Select(m => new { m.ModelId, m.Name })
                .ToList();

            return Json(models);
        }

        // Update Car
        public IActionResult Edit(int id)
        {
            var car = _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.CarColor)
                .Include(c => c.Photos)
                .FirstOrDefault(c => c.CarId == id);

            if (car == null)
            {
                return NotFound();
            }

            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.CarColors = _context.CarColors.ToList();

            return View(car);
        }

        [HttpPost]
        public IActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Update(car);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.CarColors = _context.CarColors.ToList();

            return View(car);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var car = _context.Cars.Find(id);

            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
