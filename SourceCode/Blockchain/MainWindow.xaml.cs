using Blockchain.Model;
using Blockchain.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blockchain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Login LoginScreen = (new Login());
            LoginScreen.Show();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(HomeSingleton.Instance);
        }

        private void CreateWallet_Clik(object sender, RoutedEventArgs e)
        {
            Login LoginScreen = (new Login());
            LoginScreen.Show();
        }

        private void AccountInfor_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(AccountInforSingleton.Instance);
        }

        private void SendCoin_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(SendCoinSingleton.Instance);
        }

        private void HistoryExchange_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(HistoryExchangeSingleton.Instance);
        }
    }
}
