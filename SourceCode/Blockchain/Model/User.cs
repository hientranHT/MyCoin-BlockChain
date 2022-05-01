using System;
using System.Collections.Generic;
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

        public UserRegister(string inputusername, string inputpassword, string inputpasswordconfirm)
        {
            UserName = inputusername;
            PassWord = inputpassword;
            PassWordConfirm = inputpasswordconfirm;
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
                                                                        new XElement("PassWord", PassWord)));
            Database.UpdateDatabase();
        }
    }

    public static class UserLogged
    {
        public static string UserName { get; set; }
        public static string PassWord { get; set; }
    }

}
