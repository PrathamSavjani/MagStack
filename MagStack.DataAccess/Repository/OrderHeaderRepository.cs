using MagStack.DataAccess.Repository.IRepository;
using MagStack.Models;
using MagStack.DataAccess.Repository;
using MagStack.DataAccess;
using MagStack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MagStack.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStripePaymentID(int id, string sessionId, string paymentItentId)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            orderFromDb.PaymentDate = DateTime.Now;
            orderFromDb.SessionId = sessionId;
            orderFromDb.PaymentIntentId = paymentItentId;
        }

        public IEnumerable<TotalOrderStatus> GetTotalOrderStatus()
        {
            var result = _db.OrderHeaders
                .GroupBy(o => o.OrderStatus)
                .Select(g => new TotalOrderStatus
                {
                    OrderStatus = g.Key,
                    Count = g.Count()
                });
            return result;
        }
        public IEnumerable<TotalPaymentStatus> GetTotalPaymentStatus()
        {
            var result = from o in _db.OrderHeaders
                        group o by o.PaymentStatus into g
                        select new TotalPaymentStatus
                        {
                            PaymentStatus = g.Key,
                            Count = g.Count()
                        };

            return result;
        }
    }
}