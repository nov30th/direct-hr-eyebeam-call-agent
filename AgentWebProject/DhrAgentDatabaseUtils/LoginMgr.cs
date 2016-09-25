using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DhrAgentDatabaseUtils
{
    public class LoginMgr
    {
        DBConn db = new DBConn();

        public static string MD5(string hashString)
        {
            //create the MD5CryptoServiceProvider object we will use to encrypt the password
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            //create an array of bytes we will use to store the encrypted password
            Byte[] hashedBytes;
            //Create a UTF8Encoding object we will use to convert our password string to a byte array
            UTF8Encoding encoder = new UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(hashString));
            var key = Convert.ToBase64String(hashedBytes);
            return key;
        }

        public bool Login(string username, string password, string ipAddress)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;
            var account = db.LoginUsers.SingleOrDefault(u => u.Username.ToUpper() == username.ToUpper());
            if (account == null)
                return false;
            if (account.Password != MD5(password))
                return false;
            account.LastLogin = DateTime.Now;
            account.LastLoginIP = ipAddress;
            db.SubmitChanges();
            return true;
        }

        public bool ChangePassword(string username, string password, string newpassword, string ipAddress)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(newpassword))
                return false;
            var account = db.LoginUsers.SingleOrDefault(u => u.Username.ToUpper() == username.ToUpper());
            if (account == null)
                return false;
            if (account.Password != MD5(password))
                return false;
            account.Password = MD5(newpassword);
            account.LastLogin = DateTime.Now;
            account.LastLoginIP = ipAddress;
            db.SubmitChanges();
            return true;
        }

        public bool CreateUser(string username, string password, string ipAddress)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(ipAddress))
                return false;
            var account = db.LoginUsers.SingleOrDefault(u => u.Username.ToUpper() == username.ToUpper());
            if (account != null)
                return false;
            var newaccount = new LoginUsers();
            newaccount.LastLogin = DateTime.Now;
            newaccount.LastLoginIP = ipAddress;
            newaccount.Password = MD5(password);
            newaccount.Pri = 0;
            newaccount.Username = username.ToUpper();
            db.LoginUsers.InsertOnSubmit(newaccount);
            db.SubmitChanges();
            return true;
        }


    }
}
