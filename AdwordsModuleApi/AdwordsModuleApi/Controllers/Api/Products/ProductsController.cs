using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private readonly ProductDbContext _dbContext;

        public ProductsController()
        {
            _dbContext = new ProductDbContext();
        }

        public IHttpActionResult GetProducts()
        {
            var products = _dbContext.AdGroupHos.Include(p => p.ProductLos).ToList();

            foreach (var productGroup in products)
            {
                for (int i = 0; i < productGroup.ProductLos.Count; i++)
                {
                    productGroup.ProductLos.ElementAt(i).Description =
                        productGroup.ProductLos.ElementAt(i).Description.Replace("\r\n", "");
                    productGroup.ProductLos.ElementAt(i).DescriptionShort =
                        productGroup.ProductLos.ElementAt(i).DescriptionShort.Replace("\r\n", "");
                }
            }

            return Ok(products);
        }

        
    }
}
