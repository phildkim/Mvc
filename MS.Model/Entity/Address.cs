namespace MS.Model.Entity
{
    public class Address : BaseEntity
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Post { get; set; }
        public string City { get; set; }
        public int TraderId { get; set; }
        public virtual Trader Trader { get; set; }
    }
}