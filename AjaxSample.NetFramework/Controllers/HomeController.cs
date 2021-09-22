using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AjaxSample.NetFramework.Controllers
{
    using Models;

    public class HomeController : Controller
    {
        public ActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Books(string searchText)
        {
            var model = new BookModel();
            var books = await model.Books
                                   .Where(book => book.Title.Contains(searchText))
                                   .OrderBy(book => book.Code)
                                   .ToArrayAsync();
            var bookViews = books.Select(book => book.ToViewModel());
            return PartialView("BooksView", bookViews);
        }
    }
}