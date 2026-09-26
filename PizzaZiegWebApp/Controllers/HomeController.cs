using Microsoft.AspNetCore.Mvc;
using PizzaZiegWebApp.Domain;

namespace PizzaZiegWebApp.Controllers;

public class HomeController : Controller
{
    AppDbContext _context;

    public HomeController(AppDbContext context) => 
        _context = context;

    public IActionResult Index()
    {
        var products = _context.Products.ToList();

        return View(products);
    }

    public IActionResult AboutPizzaItem(int id)
    {
        var p = _context.Products.FirstOrDefault(x => x.Id == id);

        if(p == null)
            return NotFound();

        return View(p);
    }
}
