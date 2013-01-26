using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Logic
{
    /// <summary>
    /// Responsabilidad:
    /// Administrar los aspectos globales de las batallas, aquellos que aplican aunque
    /// sea una batalla iniciada aleatoriamente o por reto
    /// </summary>
    public interface IBattleManager
    {
        /// <summary>
        /// Gets the ongoing battle of a given user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The battle director of an ongoing battle</returns>
        IBattleDirector GetOngoingBattle(IRoomUser user);
    }
}
