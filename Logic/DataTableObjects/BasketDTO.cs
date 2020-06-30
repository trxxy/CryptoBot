using DataAccess.Entities;
using System.Collections.Generic;

namespace Logic.DataTableObjects
{
    public class BasketDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public int SushiId { get; set; }
        public Dictionary<SushiDTO, int> Sushies { get; set; }
        public decimal TotalPrice { get; set; }
    }
}