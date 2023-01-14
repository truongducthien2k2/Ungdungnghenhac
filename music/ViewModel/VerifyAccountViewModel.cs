using music.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace music.ViewModel
{
    internal class VerifyAccountViewModel
    {
        public bool IsExistedEmailInSysten(string email)
        {
            return DataProvider.Ins.DB.CLIENT.SqlQuery($"SELECT * FROM CLIENT WHERE email='{email}'").Count() > 0;
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

        public int SettingNewPassword(string email, string pass)
        {
            try
            {
                DataProvider.Ins.DB.Database.ExecuteSqlCommand($"UPDATE CLIENT SET pass='{ComputeSha256Hash(pass)}' WHERE email='{email}'");
            }
            catch
            {
                return 0;
            }
            return 1;
        }
    }
}
