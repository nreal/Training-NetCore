using System.Threading.Tasks;

namespace Infrastructure.Data.Initializer
{
    public interface IDbInitializer
    {
            Task InitializeAsync();
    }
}
