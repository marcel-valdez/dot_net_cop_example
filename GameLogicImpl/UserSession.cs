namespace Game.Logic.Impl
{
    using System.Collections.Generic;
    using System;
    /*
            Responsabilidades:
            Gestionar los recursos de sesión de usuario
            Conocer el usuario al que corresponda la sesión
            Saber si la sesión ha sido autentificada
        */
    internal class UserSession : ISession, IUserSession
    {
        private ISession mSession;
        private Dictionary<object, object> values = new Dictionary<object, object>();
        private string mId = null;
        private string mUsername = null;

        
        public UserSession(ISession session, string username)
        {
            this.mSession = session;
            this.mUsername = username;
        }

        public UserSession(string id, string username)
        {
            this.mId = id;
            this.mUsername = username;
        }

        public string Username
        {
            get
            {
                return mUsername;
            }
        }
        public void Dispose()
        {
            this.values.Clear();
        }
        public IOperationResult<T> GetValue<T>(object key = null)
        {
            object value;
            if (values.TryGetValue(key ?? this, out value))
            {
                return new OperationResult<T>(ResultValue.Success, "", (T)value);
            }

            return new OperationResult<T>(ResultValue.Fail, "", default(T));
        }

        #region ISession Members

        public string SessionId
        {
            get
            {
                return mId ?? this.mSession.SessionId;
            }
        }

        #endregion
    }
}
