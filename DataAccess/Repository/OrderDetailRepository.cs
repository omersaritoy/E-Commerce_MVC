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

public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
{
    private ApplicationDbContext _context;
    public OrderDetailRepository(ApplicationDbContext context):base(context)
    {
        this._context = context;
    }

    public void Update(OrderDetail orderDetail)
    {
        _context.orderDetails.Update(orderDetail);
    }
}
