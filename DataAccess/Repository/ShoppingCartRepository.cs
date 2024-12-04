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

public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    private ApplicationDbContext _context;
    public ShoppingCartRepository(ApplicationDbContext context):base(context)
    {
        this._context = context;
    }

    public void Update(ShoppingCart shoppingCart)
    {
        _context.ShoppingCarts.Update(shoppingCart);
    }
}
