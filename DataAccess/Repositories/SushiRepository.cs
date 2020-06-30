using DataAccess.Entities;

namespace DataAccess.Repositories
{
    class SushiRepository : GenericRepository<Sushi>
    {
        public SushiRepository(DataContext db)
            : base(db)
        {
        }
    }
}