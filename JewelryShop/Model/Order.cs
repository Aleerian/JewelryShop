using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryShop.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int PickupPoint {  get; set; }
        public DateTime OrederDate { get; set; }
        public string Code { get; set; }
        public int UserId { get; set; }
    }
}
