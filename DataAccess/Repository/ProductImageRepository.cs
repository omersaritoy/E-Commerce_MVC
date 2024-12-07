﻿using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repository;

public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
{
    private ApplicationDbContext _db;
    public ProductImageRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }



    public void Update(ProductImage obj)
    {
        _db.productImages.Update(obj);
    }
}
