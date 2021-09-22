using AjaxSample.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AjaxSample.Core.Controllers
{
    public class HomeController : Controller
    {
        readonly BookModel context;

        public HomeController(BookModel context) => this.context = context;

        public IActionResult Index() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Books(string searchText)
        {
            var books = await context.Books
                                      .Include(book => book.Publisher)
                                      .Include(book => book.Authors)
                                      .Where(book => book.Title.Contains(searchText))
                                      .OrderBy(book => book.Code)
                                      .ToArrayAsync();
            var bookViews = books.Select(book => book.ToViewModel());
            return PartialView("BooksView", bookViews);
        }
    }
}
