using System;
namespace Game.Logic
{
    public interface IUserRanking
    {
        /* El nombre del usuario */
        string Username
        {
            get;
        }

        /* El ranking del dado usuario */
        int Ranking
        {
            get;
        }
    }
}