namespace DataAccess.Entities
{
    public class SessionState : Entity
    {
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}