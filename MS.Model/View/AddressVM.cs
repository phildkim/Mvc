namespace MS.Model.View
{
    public class AddressVM : BaseVM
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Post { get; set; }
        public int TraderId { get; set; }
        public virtual TraderVM Trader { get; set; }
    }
}