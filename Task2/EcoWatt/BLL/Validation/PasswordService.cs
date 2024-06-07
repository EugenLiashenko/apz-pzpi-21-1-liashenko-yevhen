using System.Security.Cryptography;
using System.Text;

namespace BLL.Validation;

public class PasswordService: IPasswordService
{
    public (string hash, string salt) HashPassword(string passwordToHash)
    {
        var saltBytes = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }

        var combinedBytes = Encoding.UTF8.GetBytes(passwordToHash).Concat(saltBytes).ToArray();
        using (var sha256 = new SHA256Managed())
        {
            var hashedBytes = sha256.ComputeHash(combinedBytes);
            var hash = Convert.ToBase64String(hashedBytes);

            return (hash, Convert.ToBase64String(saltBytes));
        }
    }

    public bool IsValid(string inputPassword, string passwordInDb, string salt)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(inputPassword);
        byte[] saltBytes = Convert.FromBase64String(salt);
        byte[] saltedPasswordBytes = new byte[inputBytes.Length + saltBytes.Length];

        Array.Copy(inputBytes, saltedPasswordBytes, inputBytes.Length);
        Array.Copy(saltBytes, 0, saltedPasswordBytes, inputBytes.Length, saltBytes.Length);

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(saltedPasswordBytes);
            string enteredHash = Convert.ToBase64String(hashedBytes);

            return string.Equals(enteredHash, passwordInDb);
        }
    }
}