using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaZiegWebApp.Domain;


namespace PizzaZiegWebApp.Controllers.Admin;


[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    private readonly IWebHostEnvironment _hostingEnviroment;

    private readonly AppDbContext _context;

    public AdminController(IWebHostEnvironment hostingEnviroment, AppDbContext context)
    {
        _hostingEnviroment = hostingEnviroment;
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.ToListAsync();
        return View(products);
    }


}
