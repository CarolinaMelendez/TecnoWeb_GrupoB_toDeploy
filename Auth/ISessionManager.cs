namespace Auth
{
    public interface ISessionManager
    {
        public Session ValidateCredentials(string userName, string password);
    }
}
