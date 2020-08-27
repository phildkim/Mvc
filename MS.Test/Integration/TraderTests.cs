namespace MS.Test.Integration
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MS.Model.View;
    using System.Threading.Tasks;
    [TestClass]
    public class TraderTests
    {
        #region Query
        [TestMethod]
        public void QueryTests()
        {
            var cmd = new Features.Query { };
            Assert.IsNotNull(cmd);
            var handler = new Features.RequestHandler(new Data.DataContextFactory());
            Task<EditVM> result = handler.Handle(cmd, System.Threading.CancellationToken.None);
            EditVM editVM = result.Result;
            Assert.IsNotNull(editVM);
        }
        #endregion

        #region Create
        [TestMethod]
        public void CreateTests()
        {
            var cmd = new Features.Create { EditVM = new EditVM { } };
            Assert.IsNotNull(cmd);
            cmd.EditVM.LegalName = "Legal Name";
            cmd.EditVM.TradingName = string.Empty;
            cmd.EditVM.Address1 = "123 W. Address st.";
            cmd.EditVM.Address2 = string.Empty;
            cmd.EditVM.City = "Glendale";
            cmd.EditVM.Post = "91203";
            var handler = new Features.RequestHandler(new Data.DataContextFactory());
            Task<bool> result = handler.Handle(cmd, System.Threading.CancellationToken.None);
            Assert.IsTrue(result.Result);
        }
        #endregion

        #region Update
        [TestMethod]
        public void UpdateTests()
        {
            var cmd = new Features.Update { Id = 1, EditVM = new EditVM { } };
            Assert.IsNotNull(cmd);
            
            cmd.EditVM.LegalName = "Legal name 2";
            cmd.EditVM.TradingName = "Trading name 2";
            cmd.EditVM.Address1 = "123 W. Address st.";
            cmd.EditVM.Address2 = "UPDATE 2";
            cmd.EditVM.City = "Glendale";
            cmd.EditVM.Post = "91203";
            var handler = new Features.RequestHandler(new Data.DataContextFactory());
            Task<bool> result = handler.Handle(cmd, System.Threading.CancellationToken.None);
            Assert.IsTrue(result.Result);
        }
        #endregion

        #region Delete
        [TestMethod]
        public void DeleteTests()
        {
            var cmd = new Features.Delete { Id = 3 };
            Assert.IsNotNull(cmd);
            
            var handler = new Features.RequestHandler(new Data.DataContextFactory());
            Task<bool> result = handler.Handle(cmd, System.Threading.CancellationToken.None);
            Assert.IsTrue(result.Result);
        }
        #endregion
    }
}
