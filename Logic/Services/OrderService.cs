using AutoMapper;
using DataAccess;
using Logic.DataTableObjects;
using Logic.Services.Interfaces;
using DataAccess.Entities;
using NETCore.MailKit.Core;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using System;

namespace Logic.Services
{
    public class OrderService : IOrderService
    {
        const int SECOND_FACTOR = 1000;

        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IOptions<OrderSettings> _orderSettings;
        private event Action<int> MailHandler;

        public OrderService(IUnitOfWork uow, 
            IMapper mapper, 
            IEmailService emailService, 
            IOptions<OrderSettings> orderSettings)
        {
            _db = uow;
            _emailService = emailService;
            _orderSettings = orderSettings;
            _mapper = mapper;
        }

        public void CreateNewOrder(Action<int> mailNotification, BasketDTO basketDto)
        {
            var basket = _mapper.Map<Basket>(basketDto);
            _db.BasketRepository.Create(basket);
            //Task.Run(() => MailNotification(basketDto)); 
            _db.BasketRepository.Delete(basket.Id);
            _db.SaveChanges();
            MailHandler += mailNotification;
            MailHandler(basket.Id);
        }
    }
}