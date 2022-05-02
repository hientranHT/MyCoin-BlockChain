using Blockchain.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blockchain.Model
{
    class MainModel
    {
    }

    class HomeSingleton
    {
        private static Home _instance = null;
        public static Home Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Home();
                }
                return _instance;
            }
        }
    }

    class AccountInforSingleton
    {
        private static AccountInfor _instance = null;
        public static AccountInfor Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AccountInfor();
                }
                return _instance;
            }
        }
    }

    class SendCoinSingleton
    {
        private static SendCoin _instance = null;
        public static SendCoin Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SendCoin();
                }
                return _instance;
            }
        }
    }

    class HistoryExchangeSingleton
    {
        private static HistoryExchange _instance = null;
        public static HistoryExchange Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HistoryExchange();
                }
                return _instance;
            }
        }
    }

    public class Database
    {
        private static Database _instance = null;
        public XDocument Data { get; set; }

        public static Database Intance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }

        public static void UpdateDatabase()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            path += @"\data\Database.xml";

            File.WriteAllText(path, Database.Intance.Data.ToString());
        }

        private Database()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string XMLpath = path + @"\data\Database.xml";

            string XMLtext = File.ReadAllText(XMLpath);
            this.Data = new XDocument(XDocument.Parse(XMLtext));
        }
    }

}
