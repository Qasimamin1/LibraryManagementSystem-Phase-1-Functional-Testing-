using LibraryManagementSystemTesting.Data;
using LibraryManagementSystemTesting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LibraryManagementSystemTesting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LibraryDbContext _context;  // Add the context here

        // Update the constructor to receive the LibraryDbContext
        public HomeController(ILogger<HomeController> logger, LibraryDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Update the Index method to fetch books from the database
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.ToListAsync(); // Retrieve all books
            return View(books); // Pass the list of books to the view
        }

        // ... Other actions (Privacy, Error) remain the same
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
