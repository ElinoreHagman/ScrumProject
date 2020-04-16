using System.Linq;
using System.Web.Mvc;
using ScrumProject.Models;

namespace ScrumProject.Controllers
{
    public class CategoryController : Controller
    {
        // GET: ExampleArrayCategory
        public ActionResult Index()
        {
            var ctx = new BlogDbContext();
            var viewModel = new CategoryIndexViewModel
            {
                Categories = ctx.Categories.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult addNewCategory(Category model)
        {
            var ctx = new BlogDbContext();
            ctx.Categories.Add(model);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult displayCategoryThatExists()
        {
            var ctx = new BlogDbContext();
            {
                var categories = ctx.Categories.ToList();
                var viewModel = new CategoryIndexViewModel { Categories = categories };
                return View(viewModel);
            }
        }
    }
}