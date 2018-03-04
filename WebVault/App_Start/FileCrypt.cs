using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using ECDHAES256s;
using System.Threading.Tasks;

namespace WebVault.App_Start
{
    public class FileCrypt
    {
        public static bool EncryptFile(AES aes, byte[] input, string filename, byte[] key)
        {
            try
            {
                
                    Task.Factory.StartNew(() =>
                    {
                        ECDHAES256s.CNG c = new ECDHAES256s.CNG();
                        c.Key = key;
                        c.PlaintextBytes = input;
                        c = aes.Encrypt(c);
                        var file=HttpContext.Current.Server.MapPath(filename);
                        File.WriteAllBytes(file + "." + c.Iv, c.EncryptedBytes);
                        c.PlaintextBytes = null;
                       
                    });
                
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}