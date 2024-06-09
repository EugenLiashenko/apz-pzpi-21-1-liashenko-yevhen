namespace BLL.Validation;

public interface IPasswordService
{
    (string hash, string salt) HashPassword(string passwordToHash);

    bool IsValid(string inputPassword, string passwordInDb, string salt);
}