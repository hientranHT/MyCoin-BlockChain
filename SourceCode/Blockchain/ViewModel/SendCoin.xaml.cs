using Blockchain.Model;
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

namespace Blockchain.ViewModel
{
    /// <summary>
    /// Interaction logic for SendCoin.xaml
    /// </summary>
    public partial class SendCoin : Page
    {
        public SendCoin()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string Receiver = WalletTo.Text;
            string InputAmount = Amount.Text;
            string Miner = "Miner";
            if (!CheckInputReceiver(Receiver))
            {
                MessageBox.Show("Receiver don't exist");
            }
            else
            {
                if (CheckInputAmount(InputAmount))
                {
                    Transaction transaction = new Transaction(UserLogged.UserName, Receiver, int.Parse(InputAmount), Miner);
                    MyBlockChain.blockChain.AddBlock(new Model.Block(DateTime.Now, null, transaction));
                    MessageBox.Show("Send money successfully");
                }
                else
                {
                    MessageBox.Show("Money is a number");
                }
            }
            
        }

        private bool CheckInputReceiver(string receiver)
        {
            return FindUserWithUserName(receiver);
        }

        private bool FindUserWithUserName(string username)
        {
            var findUser = (from i in Database.Intance.Data.Root.Element("Users").Descendants("User")
                            where i.Element("UserName").Value == username
                            select i).Any();
            return findUser;
        }

        private bool CheckInputAmount(string inputamount)
        {
            if(inputamount == "")
            {
                return false;
            }
            return true;
        }
    }
}
