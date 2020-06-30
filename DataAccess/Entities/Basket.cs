using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Basket : Entity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int SushiInBasketId { get; set; }
        public List<SushiInBasket> Sushies { get; set; }
        public decimal TotalPrice { get; set; }
    }
}