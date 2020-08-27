namespace MS.Test.Integration
{
    using System;
    using System.Threading.Tasks;
    using app;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MS.Model.View;

    [TestClass]
    public class HomeTests
    {

        [TestInitialize]
        public void Init()
        {
            MSMapper.Config();
        }

        [TestMethod]
        public void IndexTests()
        {
            var cmd = new MS.Features.Home.Query { };
            var handler = new MS.Features.Home.QueryHandler(new MS.Data.DataContextFactory());
            Task<HomeVM> result = handler.Handle(cmd, System.Threading.CancellationToken.None);
            HomeVM homeVM = result.Result;
            Assert.IsNotNull(homeVM);
        }
    }
}