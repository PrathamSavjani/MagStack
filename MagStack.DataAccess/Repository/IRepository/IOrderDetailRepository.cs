using MagStack.DataAccess.Repository.IRepository;
using MagStack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagStack.DataAccess.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail obj);
        public IEnumerable<ProductSaleSummary> GetProductSaleSummaries();
    }
}