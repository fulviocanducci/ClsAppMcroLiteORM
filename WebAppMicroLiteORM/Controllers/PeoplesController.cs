using MicroLite;
using MicroLite.Builder;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAppMicroLiteORM.Models;

namespace WebAppMicroLiteORM.Controllers
{
    public class PeoplesController : Controller
    {
        private readonly IRepository<People> RepositoryPeople;
        public PeoplesController(IRepository<People> repositoryPeople)
        {
            RepositoryPeople = repositoryPeople;
        }
        
        public async Task<ActionResult> Index(int? page)
        {
            int total = 5;
            SqlQuery query = SqlBuilder
                .Select("*")
                .From(typeof(People))
                .OrderByAscending("Name")
                .ToSqlQuery();
            return View(await RepositoryPeople.PageAsync(query, page ?? 1, total));
        }
        
        public async Task<ActionResult> Details(int id)
        {
            return View(await RepositoryPeople.FindAsync(id));
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(People people)
        {
            try
            {
                await RepositoryPeople.InsertAsync(people);
                if (people.Id > 0)
                {
                    return RedirectToAction("Edit", new { Id = people.Id });
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public async Task<ActionResult> Edit(int id)
        {
            return View(await RepositoryPeople.FindAsync(id));
        }
        
        [HttpPost]
        public async Task<ActionResult> Edit(int id, People people)
        {
            try
            {
                await RepositoryPeople.UpdateAsync(people);
                return RedirectToAction("Edit", new { Id = people.Id });
            }
            catch
            {
                return View();
            }
        }
        
        public async Task<ActionResult> Delete(int id)
        {
            return View(await RepositoryPeople.FindAsync(id));
        }
       
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                await RepositoryPeople.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
