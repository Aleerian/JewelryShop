using JewelryShop.Model;
using JewelryShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JewelryShop.ViewModel
{
    public class AddProductViewModel
    {
        public List<ProductCategory> GetAllProductCategory()
        {
            var productCategorys = DbCommandSender.GetAllProductCategory();
            return productCategorys;
        }

        public List<Manufacture> GetAllManufacture()
        {
            var manufacture = DbCommandSender.GetAllManufacture();
            return manufacture;
        }

        public List<Provider> GetAllProvider() 
        {
            var provider = DbCommandSender.GetAllProvider();
            return provider;
        }

        public void AddProduct(Product product) 
        {
            if (product == null) 
            {
                MessageBox.Show("Ошибка при добавлени");
                return;
            }

            DbCommandSender.AddProduct(product);
        }
    }
}
