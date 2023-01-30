using music.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace music.ViewModel
{
    internal class AccountViewModel
    {
        public List<CLIENT> GetAllUser()
        {
            return DataProvider.Ins.DB.CLIENT.ToList();
        }
        public string ComputeSha256Hash( string rawData )
        {
            using ( SHA256 sha256Hash = SHA256.Create() )
            {
                byte [] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for ( int i = 0; i < bytes.Length; i++ )
                {
                    builder.Append(bytes [i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool LoginSuccessfully(string username, string password )
        {
            string hashPass = ComputeSha256Hash(password);
            if( DataProvider.Ins.DB.CLIENT.Where(user => user.userName == username && user.pass == hashPass).Count() > 0 )
            {
                if ((bool) DataProvider.Ins.DB.CLIENT.Where(user => user.userName == username).Select(user => user.isAdmin).First())
                {
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand($"INSERT INTO USER_LOGIN_CREDENTIAL VALUES ('{username}', 1)");
                }
                else
                {
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand($"INSERT INTO USER_LOGIN_CREDENTIAL VALUES ('{username}', 0)");
                }
                return true;
            }
            return false;
        }

        public CLIENT GetUserInfo(int id)
        {
            return DataProvider.Ins.DB.CLIENT.Where(user => user.id == id).First();
        }

        public USER_LOGIN_CREDENTIAL AccountData()
        {
            if ( DataProvider.Ins.DB.USER_LOGIN_CREDENTIAL.ToList().Count > 0 )
            {
                return DataProvider.Ins.DB.USER_LOGIN_CREDENTIAL.First();
            }
            return null;
        }

        public int Logout()
        {
            try
            {
                DataProvider.Ins.DB.Database.ExecuteSqlCommand($"DELETE FROM USER_LOGIN_CREDENTIAL");
            }
            catch
            {
                return 0;
            }
            return 1;
        }

        public int SignUp(string userName, string pass, string fullName, string email, string phone, int isAdmin, int vip)
        {
            try
            {
                DataProvider.Ins.DB.Database.ExecuteSqlCommand($"INSERT INTO CLIENT (userName, pass, fullName, email, phone, isAdmin, vip) VALUES ('{userName}', '{ComputeSha256Hash(pass)}', N'{fullName}', '{email}', '{phone}', {isAdmin}, {vip}) ");
            }
            catch
            {
                return 0;
            }
            return 1;
        }

        public int UpdateProfile(int id, string name, string email, string phone)
        {
            try
            {
                DataProvider.Ins.DB.Database.ExecuteSqlCommand($"UPDATE CLIENT SET fullName=N'{name}', email='{email}', phone='{phone}' WHERE id={id}");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return 0;
            }
            return 1;
        }
    }
}
