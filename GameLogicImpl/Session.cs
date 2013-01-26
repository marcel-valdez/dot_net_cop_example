namespace Game.Logic
{
    using System;
    /*
            Responsabilidades:
            Conocer el identificador de sesión
            
            Respresenta:
            Una sesión genérica, no necesariamente autentificada
        */
    internal class Session : ISession
    {
        public Session(string id)
        {
            this.SessionId = id;
        }

        public string SessionId
        {
            get;
            private set;
        }
    }
}
