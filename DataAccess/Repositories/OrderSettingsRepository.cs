using DataAccess.Entities;

namespace DataAccess.Repositories
{
    class OrderSettingsRepository : GenericRepository<OrderSettings>
    {
        public OrderSettingsRepository(DataContext db)
            : base(db)
        {
        }
    }
}