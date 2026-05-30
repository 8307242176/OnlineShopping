namespace OnlineShopping.ViewModel
{
    public class CartVM
    {
        public int CartItemId { get; set; }
        public int PdID { get; set; }
        public string PdName { get; set; }
        public string ImgURL { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int SubTotal => Price * Quantity;
    }
}
