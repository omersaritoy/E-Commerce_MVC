using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repository;

public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
{
    private ApplicationDbContext _context;
    public OrderHeaderRepository(ApplicationDbContext context):base(context)
    {
        this._context = context;
    }

    public void Update(OrderHeader orderHeader)
    {
        _context.orderHeader.Update(orderHeader);
    }
    public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
    {
        var orderFromDb = _context.orderHeader.FirstOrDefault(u => u.Id == id);
        if (orderFromDb != null)
        {
            orderFromDb.OrderStatus = orderStatus;
            if (!string.IsNullOrEmpty(paymentStatus))
            {
                orderFromDb.PaymentStatus = paymentStatus;
            }
        }
    }
    public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
    {
        var orderFromDb = _context.orderHeader  .FirstOrDefault(u => u.Id == id);
        if (!string.IsNullOrEmpty(sessionId))
        {
            orderFromDb.SessionId = sessionId;
        }
        if (!string.IsNullOrEmpty(paymentIntentId))
        {
            orderFromDb.PaymentIntentId = paymentIntentId;
            orderFromDb.PaymentDate = DateTime.Now;
        }
    }
}
