using Blockchain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Blockchain.ViewModel
{
    /// <summary>
    /// Interaction logic for AccountInfor.xaml
    /// </summary>
    public partial class AccountInfor : Page
    {
        public AccountInfor()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InforUser inforUser = new InforUser(UserLogged.UserName);
            UserName.Text = inforUser.UserName;
            Money.Text = inforUser.Money.ToString();

            BindingList<Model.Block> BlockList = new BindingList<Model.Block>();
            MyBlockChain.blockChain.CopyToBlockBindingListOfYourWallet(BlockList);
            ListBlock.ItemsSource = BlockList;
        }
    }
}
