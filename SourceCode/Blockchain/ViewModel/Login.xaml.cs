using Blockchain.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Blockchain.ViewModel
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Hide();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UserName.Text;
            string password = PassWord.Password;
            UserLogin userLogin = new UserLogin(username, password);

            if(userLogin.CheckUserName())
            {
                MessageBox.Show("Login succeed");
                UserLogged.UserName = username;
                UserLogged.PassWord = password;
                this.Close();
                App.Current.MainWindow.Show();
            }
            else
            {
                MessageBox.Show("Username or password is wrong");
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string inputusername = RegisterUserName.Text;
            string inputpassword = RegisterPassWord.Password;
            string inputpasswordconfirm = RegisterConfirmPassWord.Password;

            UserRegister userRegister = new UserRegister(inputusername, inputpassword, inputpasswordconfirm);

            if(userRegister.CheckPasswordandpasswordconfirm() == true)
            {
                if(userRegister.CheckUserName())
                {
                    MessageBox.Show("Register succeed");
                }
                else
                {
                    MessageBox.Show("Username is availabe");
                }
            }
            else
            {
                MessageBox.Show("Register failed because password differ password confirm");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            App.Current.Shutdown();
        }
    }
}
