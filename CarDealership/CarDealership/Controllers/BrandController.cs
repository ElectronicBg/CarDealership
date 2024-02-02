using CarDealership.Data;
using CarDealership.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
public class BrandController : Controller
{
    private readonly ApplicationDbContext _context;

    public BrandController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Brand/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Brand/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Brand brand)
    {
        if (ModelState.IsValid)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(brand);
    }

    // GET: Brand/Edit/5
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var brand = _context.Brands.Find(id);
        if (brand == null)
        {
            return NotFound();
        }

        return View(brand);
    }

    // POST: Brand/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Brand brand)
    {
        if (id != brand.BrandId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Entry(brand).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(brand.BrandId))
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
        return View(brand);
    }

    private bool BrandExists(int id)
    {
        return _context.Brands.Any(e => e.BrandId == id);
    }

    public IActionResult Index()
    {
        var brands = _context.Brands.ToList();
        return View(brands);
    }
}
