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

        if (products.Count == 0)
        {
            products = [
                new() {
                    Name = "Пiца",
                    Description = "Крута пiца.",
                    PhotoPath = "pizzaCool.jpg",
                    Price = 400,
                    SizeSm = 88
                },
                new() {
                    Name = "Пиво Закарпатське",
                    Description = "Оригінальне світле пиво для справжніх чоловіків. Воно було зроблено в у селі Богдан, де українській мужик Богдан в соло ебашить пиво для всій країні.",
                    PhotoPath = "pivo.jpg",
                    Price = 50.00m,
                    SizeSm = 500
                },
                new() {
                    Name = "Русь-чан",
                    Description = "Руська мала, така краса ти що.",
                    PhotoPath = "alya.jpg",
                    Price = 999999999.00m,
                    SizeSm = 4
                }
            ];

            _context.Products.AddRange(products);
            _context.SaveChanges();
        }


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
