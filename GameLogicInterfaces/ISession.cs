using System;
namespace Game.Logic
{
    public interface ISession
    {
        /*
            Identificador de la sesión
        */
        string SessionId
        {
            get;
        }
    }
}