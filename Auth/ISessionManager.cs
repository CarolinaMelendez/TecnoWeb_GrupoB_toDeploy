namespace Auth
{
    public class ISessionManager
    {
        public Session ValidateCredentials(string userName, string password);
    }
}
