using MagStack.DataAccess;
using MagStack.DataAccess.Repository.IRepository;
using MagStack.Models;
using MagStack.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MagStackWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid) //After hitting the custom error the model state will become invalid and it will directly jump to return statement
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type Added Successfully";
                return RedirectToAction("Index");
            }
            return View(obj); //It returns the obj if model is not valid and is used to tell validation tags to display invalidity
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            //var categoryFromDb = _db.Categories.SingleOrDefault(c => c.Id == id);
            if (coverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(coverTypeFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid) //After hitting the custom error the model state will become invalid and it will directly jump to return statement
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(obj); //It returns the obj if model is not valid and is used to tell validation tags to display invalidity
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var covertypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            //var categoryFromDb = _db.Categories.SingleOrDefault(c => c.Id == id);
            if (covertypeFromDb == null)
            {
                return NotFound();
            }
            return View(covertypeFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
