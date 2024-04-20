using JewelryShop.Middleware;
using JewelryShop.Repository;
using JewelryShop.ViewModel;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JewelryShop.View
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private CaptchaManager captchaManager;
        private Authorization authorization;
        private bool isRequireCaptcha;
        private string captchaCode;
        public AuthorizationWindow()
        {
            InitializeComponent();
            DbConnector.Connect("localhost", "5432", "postgres", "1234", "exam");
            captchaManager = new CaptchaManager();
            authorization = new Authorization();
        }

        private void onSingIn(object sender, RoutedEventArgs e)
        {
            if (isRequireCaptcha)
            {
                MessageBox.Show("Введите Captcha");
                return;
            }

            string login = tbLogin.Text.Trim();
            string password = tbPassword.Password.Trim();
            if (login.Length < 1 || password.Length < 1)
            {
                MessageBox.Show("Необходимо ввести логин и пароль");
                return;
            }

            var user = authorization.SignIn(login, password);
            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль");
                isRequireCaptcha = true;
                spCaptcha.Visibility = Visibility.Visible;
                captchaCode = captchaManager.GenerateCaptcha(canvas);
                return;
            }

            ProductWindow productWindow = new ProductWindow(user);
            switch (user.Role)
            {
                case 4: // Администратор
                    productWindow.Show();
                    Hide();
                    break;
                case 5: // Менеджер
                    productWindow.Show();
                    Hide();
                    break;
                case 6: // Клиент
                    productWindow.Show();
                    Hide();
                    break;
            }
        }

        private void onCaptcha(object sender, RoutedEventArgs e)
        {
            if (isRequireCaptcha && captchaCode.ToLower() == tbCaptcha.Text.Trim().ToLower())
            {
                MessageBox.Show("Все правильно");
                isRequireCaptcha = false;
                spCaptcha.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Неверно");
                return;
            }
        }

        private void onGenerateCaptcha(object sender, RoutedEventArgs e)
        {
            captchaCode = captchaManager.GenerateCaptcha(canvas);
        }
    }
}
