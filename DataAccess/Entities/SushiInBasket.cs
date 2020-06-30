using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class SushiInBasket : Entity
    {
        public Sushi Sushi { get; set; }
        public int Count { get; set; }
    }
}
