using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Initializer
{
    public class UpdateByMigrationsDbInitializer : IDbInitializer
    {
        private readonly MachineContext _paymentPortalDatabaseContext;

        public UpdateByMigrationsDbInitializer(MachineContext paymentPortalDatabaseContext)
        {
            _paymentPortalDatabaseContext = paymentPortalDatabaseContext;
        }

        public async Task InitializeAsync()
        {
            await _paymentPortalDatabaseContext.Database.MigrateAsync();
        }
    }
}
