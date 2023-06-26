using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
            
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        //GET
        // This method returns the form where we can create a category
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                // Here if we change the key to CustomError or any other name other than the property name, then without summary of asp validation, we won't be able to display that error to the user
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name");
            }
            if(ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                // Here since we are in the same controller it will be redirected to the view of the Index method present in the same controller
                // If you want to redirect to a method inside a different controller, then you have to specify the controller name after the method name
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        // This method returns the form where we can create a category
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
/*            var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
*/            
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            
            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // Here if we change the key to CustomError or any other name other than the property name, then without summary of asp validation, we won't be able to display that error to the user
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";

                // Here since we are in the same controller it will be redirected to the view of the Index method present in the same controller
                // If you want to redirect to a method inside a different controller, then you have to specify the controller name after the method name
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //GET
        // This method returns the form where we can create a category
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            /*            var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
                        var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            */
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        // Here if you want this method to be triggered when our asp-action is used with a different name in the link, you have to use the actionname attrib
        // [Httppost,ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // Here if we change the key to CustomError or any other name other than the property name, then without summary of asp validation, we won't be able to display that error to the user
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                // TempData only stays in memory for a single redirect
                TempData["success"] = "Category deleted successfully";

                // Here since we are in the same controller it will be redirected to the view of the Index method present in the same controller
                // If you want to redirect to a method inside a different controller, then you have to specify the controller name after the method name
                return RedirectToAction("Index");
            }
            return View(obj);
        }



    }
}
