using DataAccess.Entities;

namespace DataAccess.Repositories
{
    class UserRepository : GenericRepository<User>
    {
        public UserRepository(DataContext db)
            : base(db)
        {
        }
    }
}