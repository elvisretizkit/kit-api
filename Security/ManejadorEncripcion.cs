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
            byte[] encrypted;
            var passwordBytes = Encoding.UTF8.GetBytes(Configuration.GetSection("Encrypt:pwd").Get<string>() ?? string.Empty);
            passwordBytes = SHA256.HashData(passwordBytes);
            var vectorEncoding = Encoding.ASCII.GetBytes(Configuration.GetSection("Encrypt:vector").Get<string>());

            using (MemoryStream ms = new MemoryStream())
            {
                using Aes AES = Aes.Create();
                AES.Key = passwordBytes;
                AES.IV = vectorEncoding;

                ICryptoTransform encryptor = AES.CreateEncryptor();

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(texto);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }
    }
}
