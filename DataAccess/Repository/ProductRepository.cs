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

public class ProductRepository : Repository<Product>, IProductRepository
{
    private ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        this._context = context;
    }

    public void Update(Product obj)
    {
        var objFromDb = _context.products.FirstOrDefault(u => u.Id == obj.Id);
        if (objFromDb != null)
        {
            objFromDb.Title = obj.Title;
            objFromDb.ISBN = obj.ISBN;
            objFromDb.Price = obj.Price;
            objFromDb.Price50 = obj.Price50;
            objFromDb.ListPrice = obj.ListPrice;
            objFromDb.Price100 = obj.Price100;
            objFromDb.Description = obj.Description;
            objFromDb.CategoryId = obj.CategoryId;
            objFromDb.Author = obj.Author;
            if (obj.ImageUrl != null)
            {
                objFromDb.ImageUrl = obj.ImageUrl;
            }
        }
    }
}
