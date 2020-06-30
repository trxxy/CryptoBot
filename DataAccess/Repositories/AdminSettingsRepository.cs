using DataAccess.Entities;

namespace DataAccess.Repositories
{
    class AdminSettingsRepository : GenericRepository<AdminSettings>
    {
        public AdminSettingsRepository(DataContext db)
            : base(db)
        {
        }
    }
}
