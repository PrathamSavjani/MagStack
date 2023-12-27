using MagStack.DataAccess.Repository.IRepository;
using MagStack.DataAccess.Repository;
using MagStack.DataAccess;
using MagStack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagStack.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private ApplicationDbContext _db;

        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }

        public IEnumerable<ProductSaleSummary> GetProductSaleSummaries()
        {
            return _db.OrderDetails
                .Join(_db.Products,
                    od => od.ProductId,
                    p => p.Id,
                    (od, p) => new { od, p }
                )
                .GroupBy(x => x.p.Title)
                .Select(g => new ProductSaleSummary
                {
                    Title = g.Key,
                    Count = g.Sum(x => x.od.Count)
                });
        }
    }
}