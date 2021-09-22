using AjaxSample.NetFramework.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AjaxSample.NetFramework.Controllers
{
    public class HomeApiController : ApiController
    {
        // GET: api/HomeApi/5
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> GetBooks(string id)
        {
            var model = new BookModel();
            var books = await model.Books
                                   .Where(book => book.Title.Contains(id))
                                   .OrderBy(book => book.Code)
                                   .ToArrayAsync();
            var bookViews = books.Select(book => book.ToViewModel());
            return Ok(bookViews);
        }
    }
}