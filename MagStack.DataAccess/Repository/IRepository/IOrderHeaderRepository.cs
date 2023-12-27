using MagStack.Models;
using MagStack.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagStack.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader obj);
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
        void UpdateStripePaymentID(int id, string sessionId, string paymentItentId);
        public IEnumerable<TotalOrderStatus> GetTotalOrderStatus();
        public IEnumerable<TotalPaymentStatus> GetTotalPaymentStatus();
    }
}