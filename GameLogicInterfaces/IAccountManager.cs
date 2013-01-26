using System;
namespace Game.Logic
{
    using System.Diagnostics.Contracts;
    [ContractClass(typeof(IAccountManagerCodeContract))]
    public interface IAccountManager
    {
        /// <summary>
        /// Autentifica las credenciales de un usuario
        /// </summary>
        /// <param name="username">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="sessionId">The session id.</param>
        /// <returns>El resultado de la operación con una sesión de usuario autentificada</returns>
        IOperationResult<IUserSession> Autentificar(string username, string password, string sessionId);


        /// <summary>
        /// Logouts the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>el resultado de la operación</returns>
        IOperationResult Logout(IUserSession session);

        /// <summary>
        /// Crea una cuenta nueva, regresando el resultado de la operación
        /// </summary>
        /// <param name="username">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>The operation result</returns>
        IOperationResult CreateAccount(string username, string password);

        /// <summary>
        /// Obtiene el objeto de sesión de usuario con el identificador
        /// sessionId, si no existe tal sesión, entonces se crea.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        /// <returns></returns>
        IOperationResult<IUserSession> GetUserSession(string sessionId);

        /// <summary>
        /// Obtiene la cuenta de jugador correspondiente al usuario autentificado
        /// en la sesión 'session'
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>The corresponding player account</returns>
        IOperationResult<IPlayerAccount> GetPlayerAccount(IUserSession session);

        /// <summary>
        /// Determines whether [is already taken] [the specified username].
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>
        ///   <c>true</c> if [is already taken] [the specified username]; otherwise, <c>false</c>.
        /// </returns>
        bool IsAlreadyTaken(string username);
    }
}