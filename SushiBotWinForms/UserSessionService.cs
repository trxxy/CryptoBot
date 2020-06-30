using Logic.DataTableObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInterface
{
    public class UserSessionService : IDisposable
    {
        public BasketDTO Basket { get; set; }
        public UserDTO CurrentUser { get; set; }
        public Action<int> MailNotification { get; set; }

        public int SessionId { get; set; }

        private readonly IServiceProvider _serviceProvider;
        private bool _disposed;

        public UserSessionService(IServiceProvider serviceProvider, UserDTO user, Action<int> mailNotification)
        {
            _serviceProvider = serviceProvider;
            CurrentUser = user;
            MailNotification = mailNotification;
        }

        public void OpenAuthForm()
        {
            Application.Run(new AuthForm(_serviceProvider));
        }

        public void OpenCatalogForm()
        {
            Application.Run(new CatalogForm(_serviceProvider, this));
        }

        public void OpenOrderForm()
        {
            var orderForm = new OrderForm(_serviceProvider, this);
            orderForm.ShowDialog();
        }
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            OpenAuthForm();
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}