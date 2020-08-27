using System.Collections.Generic;
namespace MS.Model.Entity
{
    public class Trader : BaseEntity
    {
        public string LegalName { get; set; }
        public string TradingName { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}