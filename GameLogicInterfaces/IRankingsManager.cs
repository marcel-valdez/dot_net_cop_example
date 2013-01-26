using System;
namespace Game.Logic
{
    public interface IRankingsManager
    {
        /*
        * Obtiene el objeto UserRanking correspondiente al usuario con el
        * nombre de usuario username
        * Nota: Los usuarios son rankeados en base a un algoritmo ELO no públicamente visible
        */
        IUserRanking GetUserRanking(string username);
    }
}