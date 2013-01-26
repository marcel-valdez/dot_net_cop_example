namespace Game.Logic
{
    using System;

    public interface IUserSession : IDisposable, ISession
    {

        /*
            El nombre de usuario en la sesi�n
        */
        string Username
        {
            get;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        void Dispose();

        /*
            Obtiene alg�n valor guardado en la sesi�n de usuario
        */
        IOperationResult<T> GetValue<T>(object key = null);
    }

    public enum BattleHistory
    {
        PreviousHp = 0,
        PreviousThrownCards = 1
    }
}