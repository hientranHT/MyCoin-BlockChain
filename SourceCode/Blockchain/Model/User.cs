using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blockchain.Model
{
    public class UserLogin
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }

        public UserLogin( string inputusername, string inputpassword)
        {
            UserName = inputusername;
            PassWord = inputpassword;
        }

        public bool CheckUserName()
        {
            if (FindUserWithUserName())
            {
                return true;
            }
            return false;
        }

        public bool FindUserWithUserName()
        {
            var findUser = (from i in Database.Intance.Data.Root.Element("Users").Descendants("User")
                            where i.Element("UserName").Value == UserName
                            select i).Any();
            return findUser;
        }
    }

    public class UserRegister
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string PassWordConfirm { get; set; }
        public string PrivateKey { get; set; }
       

        public UserRegister(string inputusername, string inputpassword, string inputpasswordconfirm)
        {
            UserName = inputusername;
            PassWord = inputpassword;
            PassWordConfirm = inputpasswordconfirm;
            PrivateKey = CreatePrivateKey(inputusername);
        }

        private string CreatePrivateKey(string username)
        {
            var info = new FileInfo(username);
            return $"{Guid.NewGuid()}{info.Extension}";
        }

        public bool CheckPasswordandpasswordconfirm()
        {
            if(PassWord == PassWordConfirm)
            {
                return true;
            }
            return false;
         }

        public bool CheckUserName()
        {
            if(FindUserWithUserName())
            {
                return false;
            }
            CreateNewUser();
            return true;
        }

        public bool FindUserWithUserName()
        {
            var findUser = (from i in Database.Intance.Data.Root.Element("Users").Descendants("User")
                           where i.Element("UserName").Value == UserName
                           select i).Any();
            return findUser;
        }

        public void CreateNewUser()
        {
            Database.Intance.Data.Element("root").Element("Users").Add(new XElement("User",
                                                                        new XElement("UserName", UserName),
                                                                        new XElement("PassWord", PassWord),
                                                                        new XElement("PrivateKey", PassWord),
                                                                        new XElement("Money", PassWord)));
            Database.UpdateDatabase();
        }
    }

    public static class UserLogged
    {
        public static string UserName { get; set; }
        public static string PassWord { get; set; }
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
    public class Transaction
    {
        //public string TransactionId { get; set; }
        //public List<TxIn> txIns;
        //public List<TxOut> txOuts;
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public int Amount { get; set; }
        public string Miner { get; set; }

        public Transaction(string sender, string receiver, int amount, string miner)
        {
            Sender = sender;
            Receiver = receiver;
            Amount = amount;
            Miner = miner;
        }

        public Transaction()
        {
            Sender = "";
            Receiver = "";
            Amount = 0;
            Miner = "";
        }
    }

    public class InforUser
    {
        public string UserName { get; set; }
        public int Money { get; set; }

        public InforUser(string inputusername)
        {
            UserName = inputusername;
            Money = MyBlockChain.blockChain.HowManyMoney(inputusername);
        }
    }
}
