using AjaxSample.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjaxSample.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        readonly BookModel context;

        public HomeApiController(BookModel context) => this.context = context;

        // GET: api/HomeApi/5
        [HttpGet("{searchText}")]
        public async Task<ActionResult<IEnumerable<BookViewModel>>> GetBooks(string searchText)
        {
            var books = await context.Books
                                      .Include(book => book.Publisher)
                                      .Include(book => book.Authors)
                                      .Where(book => book.Title.Contains(searchText))
                                      .OrderBy(book => book.Code)
                                      .ToArrayAsync();
            var bookViews = books.Select(book => book.ToViewModel()).ToArray();
            return bookViews;
        }
    }
}
