using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdwordsModuleApi.DbContext;
using AdwordsModuleApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdwordsModuleApiTest
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void GetProducts()
        {
            // Arrange
            ProductDbContext dbContext = new ProductDbContext();

            // Act
            ProductLo[] products = dbContext.Products.ToArray();

            // Assert
            Assert.AreNotEqual(products.Length, 0);
            Assert.AreEqual("Fur Bryghus øl, Porter", products[0].ProductName);
            Assert.AreEqual(24173, products[1].ProductNumber);
            Assert.AreEqual("fur-bryghus-oel-frokost.aspx", products[2].LogicName);
            Assert.AreEqual(3, products[2].Id);
        }
    }
}
