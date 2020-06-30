using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.DataTableObjects
{
    public class SessionStateDTO
    {
        public int Id { get; set; }
        public UserDTO User {get;set;}
        public BasketDTO Basket { get; set; }
    }
}