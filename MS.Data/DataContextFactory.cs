namespace MS.Data
{
    public class DataContextFactory : IDataContextFactory
    {
        public DataContext Get()
        {
            return new DataContext();
        }
    }
    public interface IDataContextFactory
    {
        DataContext Get();
    }
}