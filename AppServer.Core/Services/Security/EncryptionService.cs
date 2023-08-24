using BC = BCrypt.Net.BCrypt;

namespace AppServer.Core.Services.Security
{
    public interface IEncryptionService
    {
        bool IsValidPassword(string pin, string pinHash);
    }

    public class EncryptionService : IEncryptionService
    {
        public bool IsValidPassword(string pin, string pinHash)
        {
            return BC.Verify(pin, pinHash);
        }
    }
}
