using DataAccess.Entities;

namespace DataAccess.Repositories
{
    class BasketRepository : GenericRepository<Basket>
    {
        public BasketRepository(DataContext db)
            : base(db)
        {
        }
    }
}