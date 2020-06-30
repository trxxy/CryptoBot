using Logic.DataTableObjects;
using System;

namespace Logic.Services.Interfaces
{
    public interface IOrderService
    {
        public void CreateNewOrder(Action<int> mailNotification, BasketDTO sushies);
    }
}