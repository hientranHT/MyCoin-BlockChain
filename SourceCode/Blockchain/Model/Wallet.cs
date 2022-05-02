using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blockchain.Model
{
    public class Wallet
    {
        public string Address { get; set; }
        public string PrivateKey { get; set; }
        public string UserName { get; set; }
        public int Money { get; set; }

        public Wallet(string username,  string address)
        {
            Address = address;
            PrivateKey = CreatePrivateKey(username+address);
            UserName = username;
            Money = 0;
        }

        private string CreatePrivateKey(string username)
        {
            var info = new FileInfo(username);
            return $"{Guid.NewGuid()}{info.Extension}";
        }

        public bool CheckWalletValid(string address)
        {
            if (FindWallet(address))
            {
                return true;
            }
            CreateNewUser();
            return false;
        }

        private bool FindWallet(string address)
        {
            var findWallet = (from i in Database.Intance.Data.Root.Element("Wallets").Descendants("Wallet")
                            where i.Element("Address").Value == address
                              select i).Any();
            return findWallet;
        }

        public void CreateNewUser()
        {
            Database.Intance.Data.Element("root").Element("Wallets").Add(new XElement("Wallet",
                                                                        new XElement("UserName", UserName),
                                                                        new XElement("Address", Address),
                                                                        new XElement("Money", 0)));
            Database.UpdateDatabase();
        }
    }

    //class TxOut
    //{
    //    public string Address { get; set; }
    //    public int Amount { get; set; }

    //    public TxOut(string address, int amount)
    //    {
    //        Address = address;
    //        Amount = amount;
    //    }
    //}

    //class TxIn
    //{
    //    public string TxOutId { get; set; }
    //    public int TxOutIndex { get; set; }
    //    public string Signature { get; set; }
    //}

    //class Transaction
    //{
    //    //public string TransactionId { get; set; }
    //    //public List<TxIn> txIns;
    //    //public List<TxOut> txOuts;
    //    public string AdressFrom { get; set; }
    //    public string AdressTo { get; set; }
    //    public int Amount { get; set; }
    //    public Transaction (string addfressfrom, string addressto, int  amount)
    //    {
    //        AdressFrom = addfressfrom;
    //        AdressTo = addressto;
    //        Amount = amount;
    //    }
    //}
}
