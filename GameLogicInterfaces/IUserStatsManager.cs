namespace Game.Logic
{
    using System;

    public interface IUserStatsManager
    {
        /*
            Obtiene las estad�sticas de un usuarios con el nombre de usuario username
        */
        IOperationResult<IUserStats> GetUserStats(string username);
    }
}