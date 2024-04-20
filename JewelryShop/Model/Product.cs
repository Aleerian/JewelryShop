using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryShop.Model
{
    public class Product
    {
        public string ArticleNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        public byte Photo { get; set; }
        public int Manufacture { get; set; }
        public int Provider { get; set; }
        public double Cost { get; set; }
        public int DiscountAmount { get; set; }
        public int MaxDiscountAmount { get; set;}
        public int QuantityInStock { get; set;}
        public Unit Unit { get; set; }
    }

    public class ProductOutput
    {
        public string ArticleNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        public byte Photo { get; set; }
        public Model.Manufacture Manufacture { get; set; }
        public int Provider { get; set; }
        public double Cost { get; set; }
        public int DiscountAmount { get; set; }
        public int MaxDiscountAmount { get; set; }
        public int QuantityInStock { get; set; }
        public Unit Unit { get; set; }
    }

    public enum Unit
    {
        [Description("шт.")]
        Шт = 1,
    }
}
