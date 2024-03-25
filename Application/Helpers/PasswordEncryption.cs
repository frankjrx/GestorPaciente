using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;


namespace GestorPacientes.Core.Application.Helpers
{
    public class PasswordEncryption
    {
        public static string ComputeSha256Hash(string password) 
        {
            //CREAR SHA256
            using (SHA256 sha256Hash = SHA256.Create()) 
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                //Convertir de bytes a string
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++) 
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
