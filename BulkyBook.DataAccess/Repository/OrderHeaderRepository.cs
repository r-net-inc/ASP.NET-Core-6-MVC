using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
	{
        private ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader header)
        {
            _db.OrderHeaders.Update(header);
        }

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
            var headerFromDb = _db.OrderHeaders.FirstOrDefault(h => h.Id == id);
            if (headerFromDb != null)
            {
                headerFromDb.OrderStatus = orderStatus;

                if (paymentStatus != null)
                {
                    headerFromDb.PaymentStatus = paymentStatus;
                }
            }
		}

		public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
		{
			var headerFromDb = _db.OrderHeaders.FirstOrDefault(h => h.Id == id);
			
            headerFromDb.PaymentDate = DateTime.Now;
            headerFromDb.SessionId = sessionId;
            headerFromDb.PaymentIntentId = paymentIntentId;
		}
	}
}
