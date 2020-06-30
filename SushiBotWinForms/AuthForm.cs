using Logic.Services;
using Logic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using DataAccess;
using Logic.DataTableObjects;

namespace UserInterface
{
    public partial class AuthForm : Form
    {

        IAuthService _authService;
        BackgroundWorkerService _backgroundWorker;
        public UserDTO CurrentUser { get; set; }

        public AuthForm(IServiceProvider serviceProvider)
        {
            _backgroundWorker = serviceProvider.GetRequiredService<BackgroundWorkerService>();
            _authService = serviceProvider.GetRequiredService<IAuthService>();
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var state = _authService.Login(tbEmail.Text, tbPassword.Text);
            CurrentUser = _authService.CurrentUser;

            switch (state)
            {
                case AuthState.InvalidEmail:
                    MessageBox.Show("Пользователя с такой электронной почтой не существует");
                    break;
                case AuthState.InvalidPassword:
                    MessageBox.Show("Неверный пароль");
                    break;
                case AuthState.LoginSuccess:
                    _backgroundWorker.CreateNewUserSession(CurrentUser);
                    Hide();
                    break;
                default:
                    break;
            }
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            var state = _authService.Registration(tbEmail.Text, tbPassword.Text);
            CurrentUser = _authService.CurrentUser;

            switch (state)
            {
                case AuthState.AlreadyRegistred:
                    MessageBox.Show("Пользователь с такой электронной почтой уже существует");
                    break;
                case AuthState.RegistrationSuccess:
                    _backgroundWorker.CreateNewUserSession(CurrentUser);
                    Hide();
                    break;
                default:
                    break;
            }
        }
    }
}