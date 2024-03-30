using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarDealership.Data;
using CarDealership.Models;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
public class ModelController : Controller
{
    private readonly ApplicationDbContext _context;

    public ModelController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Model/Index
    public IActionResult Index()
    {
        var modelsByBrand = _context.Models
            .Include(m => m.Brand)
            .OrderBy(m => m.Brand.Name)
            .ToList()
            .GroupBy(m => m.Brand.Name); // Group models by brand name

        return View(modelsByBrand);
    }

    // GET: Model/Create
    public IActionResult Create()
    {
        ViewBag.Brands = _context.Brands.ToList();
        return View();
    }

    // POST: Model/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Model model)
    {
        if (ModelState.IsValid)
        {
            _context.Models.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Brands = _context.Brands.ToList();
        return View(model);
    }

    // GET: Model/Edit/5
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var model = _context.Models
            .Include(m => m.Brand)
            .FirstOrDefault(m => m.ModelId == id);

        if (model == null)
        {
            return NotFound();
        }

        ViewBag.Brands = _context.Brands.ToList();
        return View(model);
    }

    // POST: Model/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Model model)
    {
        if (id != model.ModelId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(model.ModelId))
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

        ViewBag.Brands = _context.Brands.ToList();
        return View(model);
    }

    private bool ModelExists(int id)
    {
        return _context.Models.Any(e => e.ModelId == id);
    }
}
