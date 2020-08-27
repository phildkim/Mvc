using AutoMapper;
using MS.Model.Entity;
using MS.Model.View;
namespace app
{
    public class MSMapper
    {
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.CreateMap<Trader, TraderVM>();
                cfg.CreateMap<Address, AddressVM>();
                cfg.CreateMap<Address, EditVM>()
                    .ForMember(des => des.LegalName, opt => opt.MapFrom(src => src.Trader.LegalName))
                    .ForMember(des => des.TradingName, opt => opt.MapFrom(src => src.Trader.TradingName))
                    .ForMember(des => des.UpdatedBy, opt => opt.MapFrom(des => des.Trader.UpdatedBy))
                    .ReverseMap();
            });
        }
    }
}