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

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    private ApplicationDbContext _context;
    public CompanyRepository(ApplicationDbContext context):base(context)
    {
        this._context = context;
    }

    public void Update(Company Company)
    {
        _context.companies.Update(Company);
    }
}
