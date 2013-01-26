namespace Game.Logic
{
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(IAccountManager))]
    internal abstract class IAccountManagerCodeContract : IAccountManager
    {
        public bool IsAlreadyTaken(string username)
        {
            Contract.Requires(!System.String.IsNullOrEmpty(username), "username is null or empty.");
            return default(bool);
        }

        public IOperationResult<IPlayerAccount> GetPlayerAccount(IUserSession session)
        {
            Contract.Requires(session != null, "session is null.");
            Contract.Ensures(Contract.Result<IOperationResult<IPlayerAccount>>() != null);
            return default(IOperationResult<IPlayerAccount>);
        }

        public IOperationResult<IUserSession> GetUserSession(string sessionId)
        {
            Contract.Requires(!System.String.IsNullOrEmpty(sessionId), "sessionId is null or empty.");
            Contract.Ensures(Contract.Result<IOperationResult<IUserSession>>() != null);
            return default(IOperationResult<IUserSession>);
        }

        public IOperationResult CreateAccount(string username, string password)
        {
            Contract.Requires(!System.String.IsNullOrEmpty(password), "password is null or empty.");
            Contract.Requires(!System.String.IsNullOrEmpty(username), "username is null or empty.");
            Contract.Ensures(Contract.Result<IOperationResult>() != null);
            return default(IOperationResult);
        }

        public IOperationResult<IUserSession> Autentificar(string username, string password, string sessionId)
        {
            Contract.Requires(!System.String.IsNullOrEmpty(sessionId), "sessionId is null or empty.");
            Contract.Requires(!System.String.IsNullOrEmpty(password), "password is null or empty.");
            Contract.Requires(!System.String.IsNullOrEmpty(username), "username is null or empty.");
            Contract.Ensures(Contract.Result<IOperationResult<IUserSession>>() != null);
            return default(IOperationResult<IUserSession>);
        }

        public IOperationResult Logout(IUserSession session)
        {
            Contract.Requires(session != null, "session is null");
            Contract.Requires(session.Username != null, "username is null");
            Contract.Requires(this.GetUserSession(session.Username).Result.Equals(ResultValue.Success),
                "session must be previously logged in");

            return default(IOperationResult);
        }
    }
}
