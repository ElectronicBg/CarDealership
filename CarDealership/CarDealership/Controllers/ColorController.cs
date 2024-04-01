using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarDealership.Data; 
using CarDealership.Models;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
public class ColorController : Controller
{
    private readonly ApplicationDbContext _context;

    public ColorController(ApplicationDbContext context)
    {
        _context = context;
    }
    // GET: Color/Index
    [HttpGet]
    public IActionResult Index()
    {
        var colors = _context.CarColors.ToList();
        return View(colors);
    }

    // GET: Color/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Color/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CarColor carColor)
    {
        if (ModelState.IsValid)
        {
            _context.CarColors.Add(carColor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(carColor);
    }

    // GET: Color/Edit/5
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var carColor = _context.CarColors.Find(id);

        if (carColor == null)
        {
            return NotFound();
        }

        return View(carColor);
    }

    // POST: Color/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, CarColor carColor)
    {
        if (id != carColor.CarColorId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Entry(carColor).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorExists(carColor.CarColorId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        return View(carColor);
    }

    private bool ColorExists(int id)
    {
        return _context.CarColors.Any(e => e.CarColorId == id);
    }
}
