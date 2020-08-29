namespace MS.Model.View
{
    using System.Collections.Generic;
    public class EditVM : BaseVM
    {
        public int TraderId { get; set; }
        public string LegalName { get; set; }
        public string TradingName { get; set; } = string.Empty;
        public string Address1 { get; set; }
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; }
        public string Post { get; set; }
        public static EditVM Create(EditVM editVM)
        {
            return new EditVM
            {
                LegalName = editVM.LegalName,
                TradingName = editVM.TradingName,
                Address1 = editVM.Address1,
                Address2 = editVM.Address2,
                City = editVM.City,
                Post = editVM.Post,
            };
        }
        public IEnumerable<AddressVM> Addresses { get; set; }
        public IEnumerable<TraderVM> Traders { get; set; }
    }
}