using Logic.DataTableObjects;
using Logic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace UserInterface
{
    public partial class OrderForm : Form
    {
        private readonly IAuthService _authService;
        private readonly IOrderService _orderService;
        private UserSessionService _userSession;

        public OrderForm(IServiceProvider serviceProvider, UserSessionService userSession)
        {
            _orderService = serviceProvider.GetRequiredService<IOrderService>();
            _authService = serviceProvider.GetRequiredService<IAuthService>();
            _userSession = userSession;

            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            _userSession.Basket.User.Name = tbUserName?.Text;
            _userSession.Basket.User.Address = tbAddress?.Text;
            _authService.UpdateUserData(_userSession.Basket.User);
            _orderService.CreateNewOrder(_userSession.MailNotification, _userSession.Basket);

            Close();
            MessageBox.Show("Ваш заказ принят");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
