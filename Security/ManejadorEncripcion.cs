using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace kit_api.Security
{
    public class ManejadorEncripcion : iManejadorEncripcion
    {
        public IConfiguration Configuration { get; set; }

        public ManejadorEncripcion(IConfiguration configuration) {
            Configuration = configuration;
        }

        public string Encriptar(string texto)
        {
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(texto);
            var passwordBytes = Encoding.UTF8.GetBytes(Configuration.GetSection("Encrypt:pwd").Get<string>() ?? string.Empty);
            passwordBytes = SHA512.Create().ComputeHash(passwordBytes);

            byte[]? encryptedBytes = null;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return Convert.ToBase64String(encryptedBytes);
        }
    }
}
