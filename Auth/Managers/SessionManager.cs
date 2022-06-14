using Serilog;
using System;
using System.Collections.Generic;

namespace Auth
{
    public class SessionManager : ISessionManager
    {
        private List<Session> _sessions { get; set; }
        //public SessionManager(IFBServices fbService)
        public SessionManager()
        {
            _sessions = new List<Session>()
            {
                new Session() { UserName = "grupoB", Password = "123", Role = "Admin" }
            };
        }
        public Session ValidateCredentials(string userName, string password)
        {
            Log.Information("Authorization Layer: Se realiza busqueda de los credenciales");
            return _sessions.Find(session => session.UserName == userName && session.Password == password);
        }
    }
}
