using AutoMapper;
using DataAccess;
using DataAccess.Entities;
using Logic.DataTableObjects;
using Logic.Services;
using Microsoft.Extensions.Options;
using NETCore.MailKit.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace UserInterface
{
    public class BackgroundWorkerService
    {
        const int SECOND_FACTOR = 1000;

        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;
        private readonly IEmailService _emailService;
        private readonly TaskFactory _taskFactory;
        private readonly IOptions<OrderSettings> _orderSettings;

        public BackgroundWorkerService(IServiceProvider serviceProvider,
            IUnitOfWork uow, 
            IEmailService emailService,
            IOptions<OrderSettings> orderSettings,
            IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _db = uow;
            _emailService = emailService;
            _orderSettings = orderSettings;
            _mapper = mapper;

            _taskFactory = new TaskFactory();
        }

        public void MailNotification(int basketId)
        {
            var basket = _db.BasketRepository.Get(basketId);
            var basketDto = _mapper.Map<Basket, BasketDTO>(basket);
            
            var mail = new MailTemplateBuilder(basketDto);
            mail.Build();

            Task.Delay(_orderSettings.Value.OrderStuffed * SECOND_FACTOR);
            _emailService.SendAsync(mail.Stuffed.Recepient, mail.Stuffed.Subject, mail.Stuffed.Text);

            Task.Delay(_orderSettings.Value.OrderDelivered * SECOND_FACTOR);
            _emailService.SendAsync(mail.Delivered.Recepient, mail.Delivered.Subject, mail.Delivered.Text);

            Task.Delay(_orderSettings.Value.OrderPaid * SECOND_FACTOR);
            _emailService.SendAsync(mail.Paid.Recepient, mail.Paid.Subject, mail.Paid.Text);
        }

        public void CreateNewUserSession(UserDTO user)
        {
            var task = _taskFactory.StartNew(() =>
            {
                var session = new UserSessionService(_serviceProvider, user, MailNotification);
                session.OpenCatalogForm();
            });

            SessionStateDTO sessionStateDto = new SessionStateDTO()
            {
                Id = task.Id,
                User = user,
                Basket = new BasketDTO()
            };
            var sessionState = _mapper.Map<SessionState>(sessionStateDto);
            _db.SessionStateRepository.Create(sessionState);
            
        }
    }
}