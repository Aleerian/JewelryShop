using JewelryShop.Model;
using JewelryShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryShop.ViewModel
{
    public class ProductViewModel
    {
        public List<ProductOutput> GetAllProducts()
        {
            var products = DbCommandSender.GetAllProduct();
            List<ProductOutput> productsOutput = new List<ProductOutput>();
            foreach (var product in products)
            {
                if (product == null)
                {
                    return null;
                }
                var manufacture = DbCommandSender.GetManufactureById(product.Manufacture);

                Model.ProductOutput productOutput = new Model.ProductOutput
                {
                    ArticleNumber = product.ArticleNumber,
                    Name = product.Name,
                    Description = product.Description,
                    Category = product.Category,
                    Manufacture = manufacture,
                    Provider = product.Provider,
                    Cost = product.Cost,
                    DiscountAmount = product.DiscountAmount,
                    MaxDiscountAmount = product.MaxDiscountAmount,
                    QuantityInStock = product.QuantityInStock,
                    Unit = product.Unit
                };

                productsOutput.Add(productOutput);
            }

            return productsOutput;
        }

        public List<Manufacture> GetAllManufacture()
        {
            var manufactures = DbCommandSender.GetAllManufacture();
            return manufactures;
        }

        public bool DeleteProduct(string id)
        {
            var isProductDeleted = DbCommandSender.DeleteProduct(id);
            return isProductDeleted;
        }
    }
}
