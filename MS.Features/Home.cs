namespace MS.Features
{
    using MediatR;
    using MS.Data;
    using MS.Model.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MS.Model.View;

    public class Home
    {
        public class Query : IRequest<HomeVM> { }
        public class QueryHandler : IRequestHandler<Query, HomeVM>
        {
            private readonly IDataContextFactory factory;
            public QueryHandler(IDataContextFactory factory)
            {
                this.factory = factory;
            }
            public Task<HomeVM> Handle(Query request, CancellationToken cancellationToken)
            {
                DataContext db = factory.Get();
                List<Trader> allTraders = db.Traders.Include(t => t.Addresses).ToList();
                HomeVM result = new HomeVM { Traders = Mapper.Map<List<TraderVM>>(allTraders) };
                return Task.FromResult(result);
            }
        }
    }
}