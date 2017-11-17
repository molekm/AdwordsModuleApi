using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using AdwordsModuleApi.Models;

namespace AdwordsModuleApi.DbContext
{
    public class ProductDbContext : System.Data.Entity.DbContext
    {

        public DbSet<Product> Products { get; set; }
    }
}