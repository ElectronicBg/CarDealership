using CarDealership.Data;
using CarDealership.Models;
using CarDealership.ViewModel;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        public IActionResult Index(int? pageSize, int? pageNumber)
        {
            var cars = _context.Cars
                .Include(c => c.Brand)
                .Include(c=>c.Model)
                .Include(c => c.CarColor)
                .Include(c => c.Photos)
                .AsQueryable(); 

            // Pagination
            pageSize = pageSize ?? 6; // Default page size is 6
            pageNumber = pageNumber ?? 1; // Default page number is 1

            ViewBag.PageSize = pageSize.Value;
            ViewBag.CurrentPage = pageNumber.Value;
            ViewBag.TotalPages = (int)Math.Ceiling((double)cars.Count() / pageSize.Value);

            var paginatedCars = cars
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .ToList();

            // Pass the paginated cars to the view
            return View(paginatedCars);
        }

        // Add Car
        [Authorize(Roles = "Admin")]
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

            // Repopulate dropdowns 
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.CarColors = _context.CarColors.ToList();

            // Return a JSON result with success status (false in this case)
            return Json(new { success = false});
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

        //Search Cars
        [HttpGet]
        [Route("Car/Search")]
        public IActionResult Search()
        {
            var model = new SearchViewModel();
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.CarColors = _context.CarColors.ToList();
            return View(model);
        }

        //Search Cars
        [HttpPost]
        [Route("Car/Search")]
        public IActionResult Search(SearchViewModel search, int? pageNumber)
        {
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.CarColors = _context.CarColors.ToList();

            // Query to get cars based on selected filters
            var query = _context.Cars
                        .Include(c => c.Brand)
                        .Include(c => c.CarColor)
                        .Include(c => c.Model)
                        .Include(c => c.Photos).ToList()
                        .AsQueryable();


            // Filter by brand
            if (search.BrandId.HasValue)
            {
                query = query.Where(c => c.BrandId == search.BrandId);
            }

            int? selectedModelId = null;  // Variable to store the selected model ID
            string selectedModelName = null;  // Variable to store the selected model name

            // Filter by model
            if (search.ModelId.HasValue)
            {
                query = query.Where(c => c.ModelId == search.ModelId);
                selectedModelId = search.ModelId;  // Capture the selected model ID

                // Query the database to get the model name based on the selected model ID
                var selectedModel = _context.Models.FirstOrDefault(m => m.ModelId == search.ModelId);
                if (selectedModel != null)
                {
                    selectedModelName = selectedModel.Name;
                }
            }

            // Filter by engine type
            if (search.EngineType.HasValue)
            {
                query = query.Where(c => c.EngineType == search.EngineType);
            }

            // Filter by transmission type
            if (search.TransmissionType.HasValue)
            {
                query = query.Where(c => c.TransmissionType == search.TransmissionType);
            }

            // Filter by color
            if (search.CarColorId.HasValue)
            {
                query = query.Where(c => c.CarColorId == search.CarColorId);
            }

            // Filter by region
            if (search.Region.HasValue)
            {
                query = query.Where(c => c.Region == search.Region);
            }

            // Filter by year range
            if (search.MinYear.HasValue)
            {
                query = query.Where(c => c.Year >= search.MinYear);
            }

            if (search.MaxYear.HasValue)
            {
                query = query.Where(c => c.Year <= search.MaxYear);
            }

            // Filter by car type
            if (search.CarType.HasValue)
            {
                query = query.Where(c => c.CarType == search.CarType);
            }

            // Filter by condition
            if (search.Condition.HasValue)
            {
                query = query.Where(c => c.Condition == search.Condition);
            }

            // Filter by price range
            if (search.MinPrice.HasValue)
            {
                query = query.Where(c => c.Price >= search.MinPrice);
            }

            if (search.MaxPrice.HasValue)
            {
                query = query.Where(c => c.Price <= search.MaxPrice);
            }

            // Apply sorting based on the orderBy parameter
            switch (search.OrderBy)
            {
                case "PriceAsc":
                    query = query.OrderBy(c => c.Price);
                    break;

                case "PriceDesc":
                    query = query.OrderByDescending(c => c.Price);
                    break;

                case "MileageAsc":
                    query = query.OrderBy(c => c.Mileage);
                    break;

                case "MileageDesc":
                    query = query.OrderByDescending(c => c.Mileage);
                    break;

                default:
                    // Default sorting (you can choose a default based on your requirements)
                    query = query.OrderBy(c => c.Price);
                    break;
            }

            // Materialize the query into a list before pagination
            var results = query.ToList();

            // Pagination
            if (search.PageSize == 0)
            {
                search.PageSize = 6;
            }
            else
            {
                search.PageSize = search.PageSize; //?? 6; // Default page size is 3
            } 
            
            pageNumber = pageNumber ?? 1;

            var paginatedResults = results
                .Skip((pageNumber.Value - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToList();

            // Set pagination-related ViewBag values
            ViewBag.SearchResults = paginatedResults;
            ViewBag.PageSize = search.PageSize;
            ViewBag.CurrentPage = pageNumber.Value;
            ViewBag.TotalPages = (int)Math.Ceiling((double)results.Count() / search.PageSize);

            ViewBag.SelectedModelName = selectedModelName;

            return View(search);
        }

        //Car Details
        [HttpGet]
        [Route("Car/Details/{id}")]
        public IActionResult Details(int id)
        {
            // Retrieve the car details based on the 'id' parameter
            var car = _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Model)
                .Include(c => c.CarColor)
                .Include(c => c.Photos)
                .FirstOrDefault(c => c.CarId == id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [Authorize(Roles = "Admin")]
        // Update Car
        public IActionResult Edit(int id)
        {
            var car = _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Model)
                .Include(c => c.CarColor)
                .Include(c => c.Photos)
                .FirstOrDefault(c => c.CarId == id);

            if (car == null)
            {
                return NotFound();
            }

            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.CarColors = _context.CarColors.ToList();

            // Populate the Model dropdown based on the selected BrandId
            ViewBag.Models = _context.Models
                .Where(m => m.BrandId == car.BrandId)
                .ToList();

            return View(car);
        }

        [HttpPost]
        public IActionResult Edit(int id, Car editedCar)
        {
            if (id != editedCar.CarId)
            {
                return NotFound();
            }
            ModelState.Remove("Photos");
            if (ModelState.IsValid)
            {
                // Retrieve the existing car from the database
                var existingCar = _context.Cars
                    .Include(c => c.Brand)
                    .Include(c => c.Model)
                    .Include(c => c.CarColor)
                    .Include(c => c.Photos)
                    .FirstOrDefault(c => c.CarId == id);

                if (existingCar == null)
                {
                    return NotFound();
                }

                _context.Entry(existingCar).CurrentValues.SetValues(editedCar);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.CarColors = _context.CarColors.ToList();

            // Repopulate the Model dropdown based on the selected BrandId
            ViewBag.Models = _context.Models
                .Where(m => m.BrandId == editedCar.BrandId)
                .ToList();

            var car = _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Model)
                .Include(c => c.CarColor)
                .Include(c => c.Photos)
                .FirstOrDefault(c => c.CarId == id);

            return View("Edit",car);
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
