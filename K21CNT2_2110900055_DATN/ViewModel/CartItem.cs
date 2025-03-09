using K21CNT2_2110900055_DATN.Models;

namespace K21CNT2_2110900055_DATN.ViewModel
{
    public class CartItem
    {
        public Product product { get; set; }
        public int amount { get; set; }
        public double TotalMoney => amount * product.Price.Value;
    }
}
