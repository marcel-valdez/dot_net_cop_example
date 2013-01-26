using System;
namespace Game.Logic
{
    using System.Collections.Generic;
    public interface IPlayerAccount
    {
        /*
            El nombre de usuario
        */
        string Username
        {
            get;
        }

        /*
            Las cartas que le pertenecen al usuario
        */
        IEnumerable<ICard> PlayerDeck
        {
            get;
        }
    }
}