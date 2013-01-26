namespace Game.Logic
{
    using System;

    public interface IUserSession : IDisposable, ISession
    {

        /*
            El nombre de usuario en la sesión
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
            Obtiene algún valor guardado en la sesión de usuario
        */
        IOperationResult<T> GetValue<T>(object key = null);
    }

    public enum BattleHistory
    {
        PreviousHp = 0,
        PreviousThrownCards = 1
    }
}