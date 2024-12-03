using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{

    private ApplicationDbContext _context;
    public ICategoryRepository Category { get; private set; }
    public IProductRepository Product { get; private set; }

    public ICompanyRepository Company { get; set; }

    public UnitOfWork(ApplicationDbContext context) 
    {
        this._context = context;
        Category=new CategoryRepository(_context);
        Product=new ProductRepository(_context);
        Company=new CompanyRepository(_context);
    }
   

    public void Save()
    {
        _context.SaveChanges();
    }
}