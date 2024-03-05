using System.Security.Cryptography;

namespace MvcCoreCryptography.Helpers
{
    public class HelperCryptography
    {
        public static byte[] EncryptPassword(string password, string salt)
        {
            string contenido = password + salt;
            SHA512 sHA512 = SHA512.Create();
            //CONVERTIMOS CONTENIDO A BYTES[]
            byte[] salida = System.Text.Encoding.UTF8.GetBytes(contenido);
            //CREAMOS LA ITERACIONES QUE NOS APETEZCA
            for (int i = 1; i<=114; i ++)
            {
                salida = sHA512.ComputeHash(salida);
            }
            sHA512.Clear();
            return salida;
        }
    }
}
