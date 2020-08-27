namespace MS.Test.Integration
{
	using AutoMapper;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System.Collections.Generic;

	[TestClass]
	public class MapperTests
	{
		public class TraderEntity
		{
			public string LegalName { get; set; }
			public string TradingName { get; set; }
			public virtual ICollection<AddressEntity> Addresses { get; set; }
		}
		public class AddressEntity
		{
			public string Address1 { get; set; }
			public string Address2 { get; set; }
			public string Post { get; set; }
			public string City { get; set; }
			public int TraderId { get; set; }
			public virtual TraderEntity Trader { get; set; }
		}
		public class TraderDto
		{
			public string LegalName { get; set; }
			public string TradingName { get; set; }
			public List<AddressDto> Addresses { get; set; }
		}
		public class AddressDto
		{
			public string Address1 { get; set; }
			public string Address2 { get; set; }
			public string Post { get; set; }
			public string City { get; set; }
			public int TraderId { get; set; }
			public TraderDto Trader { get; set; }
		}
		[TestMethod]
		public void TestMappers()
        {
			var config = new MapperConfiguration(cfg => {
				cfg.CreateMap<TraderEntity, TraderDto>();
				cfg.CreateMap<AddressEntity, AddressDto>();
			});
			config.AssertConfigurationIsValid();
			var source = new TraderEntity
			{
				LegalName = "legal name",
				TradingName = "trading name",
				Addresses = new List<AddressEntity> 
				{ 
					new AddressEntity
                    {
						Address1 = "address 1",
						Address2 = string.Empty,
						City = "city",
						Post = "12345",
						TraderId = 1,
						Trader = new TraderEntity
                        {
							LegalName = "legal name",
							TradingName = "trading name",
						}
                    }
				}
			};
			var source2 = new AddressEntity
			{
				Address1 = "address 1",
				Address2 = string.Empty,
				City = "city",
				Post = "12345",
				TraderId = 1,
				Trader = new TraderEntity
				{
					LegalName = "legal name",
					TradingName = "trading name",
				}
			};
			var mapper = config.CreateMapper();
			var dest = mapper.Map<TraderEntity, TraderDto>(source);
			Assert.AreEqual("legal name", dest.LegalName);
			Assert.IsNotNull(dest.Addresses);
			var dest2 = mapper.Map<AddressEntity, AddressDto>(source2);
			Assert.AreEqual("address 1", dest2.Address1);
			Assert.AreEqual("legal name", dest2.Trader.LegalName);
		}
	}
}