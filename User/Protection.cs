using System.Security.Cryptography;
using System.Text;

namespace User
{
    public class Protection
    {
        public static string hash(string password)
        {
            SHA1 alg = SHA1.Create();
            byte[] arrayByte = null;
            arrayByte = alg.ComputeHash(Encoding.Default.GetBytes(password));
            string hashPass = "";
            for (int x = 0; x < arrayByte.Length; x++)
            {
                hashPass += arrayByte[x].ToString("x2");
            }
            return hashPass;
        }
    }
}
