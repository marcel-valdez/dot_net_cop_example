namespace Game.Logic.Impl
{
    using System.Linq;
    using System.Data;
    using Game.Core;
    using System.Data.EntityClient;
    using System;
    using Game.Logic.DynamicModel;
    using Game.Logic.Model;
    using System.Diagnostics.Contracts;
    using System.Text.RegularExpressions;
    /*
            Se encarga de gestionar cuentas y sesiones de usuario
        */
    internal class AccountManager : IAccountManager
    {
        private static Regex userRegx = new Regex(
            "[a-zA-Z0-9_-]{4,16}",
            RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex passRegx = new Regex(
            "[a-zA-Z0-9_-]{8,32}",
            RegexOptions.Singleline | RegexOptions.Compiled);

        public AccountManager()
        {

        }
        /// <summary>
        /// Autentifica las credenciales de un usuario
        /// </summary>
        /// <param name="username">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="sessionId">The session id.</param>
        /// <returns>El resultado de la operación con una sesión de usuario autentificada</returns>
        public IOperationResult<IUserSession> Autentificar(string username, string password, string sessionId)
        {
            IUserSession uSession = Model.Cache.Retrieve<IUserSession>(u => u.SessionId == sessionId);
            if (uSession != null)
            {
                return new OperationResult<IUserSession>(ResultValue.Success, "", uSession);
            }

            var account = RequestContext.Model<Entities>().Accounts
                            .FirstOrDefault(acc => acc.Username == username && acc.Password == password);
            if (account != null)
            {
                uSession = new UserSession(sessionId, account.Username);
                Model.Cache.Add(null, uSession);
                return new OperationResult<IUserSession>(ResultValue.Success, "", uSession);
            }

            return new OperationResult<IUserSession>(ResultValue.Fail, "Credenciales de usuario incorrectas", uSession);
        }

        /// <summary>
        /// Logouts the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>el resultado de la operación</returns>
        public IOperationResult Logout(IUserSession session)
        {
            if (Model.Cache.Retrieve()
                           .Remove(session))
            {
                return new OperationResult(ResultValue.Success, "Ha salido del sistema.");
            }

            return new OperationResult(ResultValue.Fail, "Ha salido del sistema.");
        }

        /// <summary>
        /// Crea una cuenta nueva, regresando el resultado de la operación
        /// </summary>
        /// <param name="username">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>The operation result</returns>
        public IOperationResult CreateAccount(string username, string password)
        {
            if (!userRegx.IsMatch(username))
            {
                return new OperationResult(ResultValue.Fail, "El nombre de usuario solamente puede contener letras, números y _\ny ser de 4 a 16 caracteres");
            }

            if (!passRegx.IsMatch(password))
            {
                return new OperationResult(ResultValue.Fail, "El password solamente puede contener letras, números y _\ny ser de 8 a 32 caracteres");
            }

            if (IsAlreadyTaken(username))
            {
                return new OperationResult(ResultValue.Fail, "El nombre de usuario " + username + " ya está registrado");
            }

            /**
             * Se tiene un conjunto de cartas por default con las que inician los jugadores.
             **/
            var acc = new Account
                {
                    Username = username,
                    Password = password,
                };

            var model = RequestContext.Model<Entities>();
            foreach (var card in model.Cards.Take(8))
            {
                acc.Deck.Add(card);
            }

            acc.Deck.Add(new Card
            {
                AttackPoints = 10,
                DefensePoints = 20,
                Effect = new CardEffect
                {
                    ProbabilityOfEffect = 0.1,
                    Name = "name",
                    LifePointsChange = 0,
                    EffectTiming = 0,
                    DisableOpponentEffect = false,
                    Description = "as",
                    CardDefenseMultiplier = 0.9,
                    CardDefenseChange = 10,
                    CardAttackMultiplier = 1,
                    Affected = 0
                },
                ImageUrl = "",
                Name = "name"
            });

            model.AddToAccounts(acc);

            model.SaveChanges();

            return new OperationResult(ResultValue.Success, "Cuenta creada exitósamente");
        }
        /// <summary>
        /// Obtiene el objeto de sesión de usuario con el identificador
        /// sessionId, si no existe tal sesión, entonces se crea.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        /// <returns></returns>
        public IOperationResult<IUserSession> GetUserSession(string sessionId)
        {
            IUserSession uSession = Model.Cache.Retrieve<IUserSession>(u => u.SessionId == sessionId);
            if (uSession != null)
            {
                return new OperationResult<IUserSession>(ResultValue.Success, "", uSession);
            }

            return new OperationResult<IUserSession>(ResultValue.Fail, "", uSession);
        }
        /// <summary>
        /// Obtiene la cuenta de jugador correspondiente al usuario autentificado
        /// en la sesión 'session'
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>The corresponding player account</returns>
        public IOperationResult<IPlayerAccount> GetPlayerAccount(IUserSession session)
        {
            Account account = RequestContext.Model<Entities>().Accounts.FirstOrDefault(acc => acc.Username == session.Username);
            if (account == default(Account))
            {
                return new OperationResult<IPlayerAccount>(
                    ResultValue.Error, 
                    "Su sesión está incorrectamente autentificada, hack?", 
                    default(IPlayerAccount));
            }

            return new OperationResult<IPlayerAccount>(
                    ResultValue.Success,
                    "",
                    account);
        }
        /// <summary>
        /// Determines whether [is already taken] [the specified username].
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>
        /// <c>true</c> if [is already taken] [the specified username]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAlreadyTaken(string username)
        {
            Account account = RequestContext.Model<Entities>().Accounts.FirstOrDefault(acc => acc.Username == username);
            return account != default(Account);
        }
    }
}
