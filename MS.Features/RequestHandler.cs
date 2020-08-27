namespace MS.Features
{
    using AutoMapper;
    using MediatR;
    using MS.Data;
    using MS.Model.Entity;
    using MS.Model.View;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading;
    using System.Threading.Tasks;

    #region QueryRequest
    public class Query : IRequest<EditVM> { }
    #endregion QueryRequest

    #region CreateRequest
    public class Create : IRequest<bool> 
    { 
        public virtual EditVM EditVM { get; set; }
    }
    #endregion CreateRequest

    #region UpdateRequest
    public class Update : IRequest<bool> 
    {
        public int? Id { get; set; }
        public EditVM EditVM { get; set; }
    }
    #endregion UpdateRequest

    #region DeleteRequest
    public class Delete : IRequest<bool> 
    { 
        public virtual int? Id { get; set; }
    }
    #endregion DeleteRequest

    public class RequestHandler : 
        IRequestHandler<Query, EditVM>,
        IRequestHandler<Create, bool>,
        IRequestHandler<Update, bool>,
        IRequestHandler<Delete, bool>
    {
        #region ContextFactory
        private readonly IDataContextFactory factory;
        public RequestHandler(IDataContextFactory factory)
        {
            this.factory = factory;
        }
        #endregion ContextFactory

        #region QueryHandler
        public async Task<EditVM> Handle(Query request, CancellationToken cancellationToken)
        {
            DataContext db = factory.Get();
            List<Address> context = await db.Addresses.Include(t => t.Trader.Addresses).ToListAsync();
            var tradersDto = new EditVM { Addresses = Mapper.Map<List<AddressVM>>(context) };
            return await Task.FromResult(tradersDto);
        }
        #endregion QueryHandler

        #region CreateHandler
        public async Task<bool> Handle(Create request, CancellationToken cancellationToken)
        {
            DataContext db = factory.Get();
            bool created = false;
            var traderDto = EditVM.Create(request.EditVM);
            var addressEntity = Mapper.Map<EditVM, Address>(traderDto);
            if (addressEntity != null)
            {
                db.Entry(Mapper.Map<Address>(addressEntity)).State = EntityState.Added;
                created = await db.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(created);
        }
        #endregion CreateHandler

        #region UpdateHandler
        public async Task<bool> Handle(Update request, CancellationToken cancellationToken)
        {
            DataContext db = factory.Get();
            bool updated = false;
            var traderDto = EditVM.Create(request.EditVM);
            var addressDto = Mapper.Map<Address>(await db.Addresses.FirstOrDefaultAsync(a => a.Id == request.Id));
            if (addressDto != null)
            {
                addressDto.Trader.LegalName = traderDto.LegalName;
                addressDto.Trader.TradingName = traderDto.TradingName;
                addressDto.Address1 = traderDto.Address1;
                addressDto.Address2 = traderDto.Address2;
                addressDto.City = traderDto.City;
                addressDto.Post = traderDto.Post;
                db.Addresses.Attach(addressDto);
                db.Entry(addressDto).State = EntityState.Modified;
                updated = await db.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(updated);
        }        
        #endregion UpdateHandler

        #region DeleteHandler
        public async Task<bool> Handle(Delete request, CancellationToken cancellationToken)
        {
            DataContext db = factory.Get();
            bool deleted = false;
            var addressDto = Mapper.Map<Address>(await db.Addresses.FirstOrDefaultAsync(a => a.Id == request.Id));
            if (addressDto != null)
            {
                db.Entry(addressDto.Trader).State = EntityState.Deleted;
                db.Entry(addressDto).State = EntityState.Deleted;
                deleted = await db.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(deleted);
        }
        #endregion DeleteHandler
    }
}