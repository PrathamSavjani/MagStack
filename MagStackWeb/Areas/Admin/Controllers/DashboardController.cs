using MagStack.DataAccess;
using MagStack.DataAccess.Repository.IRepository;
using MagStack.Models;
using MagStack.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MagStackWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public DashboardController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IConfiguration configuration, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        public IActionResult Index()
        {
            int totalUsers = _userManager.Users.Count();
            ViewBag.TotalUsers = totalUsers;

            double totalRevenue = 0;
            int totalProduct = 0;
            int totalCompany = 0;
            using (var connection = new SqlConnection(_configuration.GetConnectionString("magStackDB")))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT SUM([dbo].[OrderDetails].[Count] * [dbo].[OrderDetails].[Price]) FROM [dbo].[OrderDetails]", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            totalRevenue = reader.IsDBNull(0) ? 0 : reader.GetDouble(0);
                        }
                    }
                }
                using (var command1 = new SqlCommand("SELECT COUNT([dbo].[Products].[Id]) FROM [dbo].[Products]", connection))
                {
                    using (var reader1 = command1.ExecuteReader())
                    {
                        while (reader1.Read())
                        {
                            totalProduct = reader1.IsDBNull(0) ? 0 : reader1.GetInt32(0);
                        }
                    }
                }
                using (var command2 = new SqlCommand("SELECT COUNT([dbo].[Companies].[Id]) FROM [dbo].[Companies]", connection))
                {
                    using (var reader2 = command2.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            totalCompany = reader2.IsDBNull(0) ? 0 : reader2.GetInt32(0);
                        }
                    }
                }
            }
            ViewBag.TotalRevenue = totalRevenue;
            ViewBag.TotalProduct = totalProduct;
            ViewBag.TotalCompany = totalCompany;
            return View();
        }

        #region API CALLS
        public IActionResult GetProductRevenue()
        {
            var result = from orderDetail in _context.OrderDetails
                         join product in _context.Products
                         on orderDetail.ProductId equals product.Id
                         group orderDetail by product.Title into grouped
                         select new ProductRevenue
                         {
                             ProductName = grouped.Key,
                             Total = Math.Round(grouped.Sum(od => od.Count * od.Price), 2)
                         };

            return Json(result.ToList());
        }

        public IActionResult GetSalesByProduct()
        {
            var salesList = _unitOfWork.OrderDetail.GetProductSaleSummaries();
            return Json(new { data = salesList });
        }
        public IActionResult GetTotalOrderStatus()
        {
            var orderStatus = _unitOfWork.OrderHeader.GetTotalOrderStatus();
            return Json(new { data = orderStatus });
        }
        public IActionResult GetTotalPaymentStatus()
        {
            var paymentStatus = _unitOfWork.OrderHeader.GetTotalPaymentStatus();
            return Json(new { data = paymentStatus });
        }
        #endregion
    }
}
