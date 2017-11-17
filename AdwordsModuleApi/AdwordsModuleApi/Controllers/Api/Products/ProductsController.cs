﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;
using System.Web.Http;
using AdwordsModuleApi.DbContext;
using AdwordsModuleApi.Models;

namespace AdwordsModuleApi.Controllers.Api.Products
{
    [Route("api/products")]
    public class ProductsController : ApiController
    {
        public ProductDbContext _dbContext;

        public ProductsController()
        {
            _dbContext = new ProductDbContext();
        }

        public IHttpActionResult GetProducts()
        {
            IEnumerable<Product> products = _dbContext.Products.ToList();

            return Ok(products);
        }

        
    }
}