using Logic.DataTableObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public class MailTemplateBuilder
    {
        public MailTemplate Stuffed { get; set; }
        public MailTemplate Delivered { get; set; }
        public MailTemplate Paid { get; set; }

        private readonly BasketDTO _basket;
        public MailTemplateBuilder(BasketDTO basket)
        {
            _basket = basket;
        }

        public void Build()
        {
            Stuffed = new MailTemplate()
            {
                Recepient = _basket.User.Email,
                Subject = "Ваш заказ скомплектован",
                Text = string.Format($"Ваш заказ состоящий из {0} скомплектован", _basket.Sushies)
            };
            Delivered = new MailTemplate()
            {
                Recepient = _basket.User.Email,
                Subject = "Ваш заказ доставлен",
                Text = "Ваш заказ состоящий из доставлен"
            };
            Paid = new MailTemplate()
            {
                Recepient = _basket.User.Email,
                Subject = "Ваш заказ оплачен",
                Text = string.Format($"Ваш заказ на сумму {0} б.р. оплачен", _basket.TotalPrice)
            };
        }
    }
}
