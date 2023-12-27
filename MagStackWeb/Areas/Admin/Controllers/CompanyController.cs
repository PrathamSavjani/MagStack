using MagStack.DataAccess;
using MagStack.DataAccess.Repository.IRepository;
using MagStack.Models;
using MagStack.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MagStack.Models.ViewModels;
using MagStack.Utility;
using Microsoft.AspNetCore.Authorization;

namespace MagStackWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        //GET
        public IActionResult Upsert(int? id)
        {
            Company company = new();
            
            if (id == null || id == 0)
            {
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                //create
                return View(company);
            }
            else
            {
                company = _unitOfWork.Company.GetFirstOrDefault(u=>u.Id==id);
                return View(company);
            }
            //var categoryFromDb = _db.Categories.Find(id);
            //var coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            //var categoryFromDb = _db.Categories.SingleOrDefault(c => c.Id == id);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid) //After hitting the custom error the model state will become invalid and it will directly jump to return statement
            {
                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Company created Successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    TempData["success"] = "Company updated Successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj); //It returns the obj if model is not valid and is used to tell validation tags to display invalidity
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Company.GetAll();
            return Json(new {data = companyList});
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}