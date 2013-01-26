using System;
namespace Game.Logic
{
    using System.Collections.Generic;
    public interface IRoom
    {
        /// <summary>
        /// Gets the room name.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets the users in the room.
        /// </summary>
        IEnumerable<IRoomUser> Users
        {
            get;
        }

        /// <summary>
        /// Gets the battles manager of this room.
        /// </summary>
        IBattleManager BattlesManager
        {
            get;
        }

        /// <summary>
        /// Finds a random battle for the requestor.
        /// </summary>
        /// <param name="requestor">The requestor.</param>
        /// <returns>A battle request</returns>
        IBattleRequest FindBattle(IRoomUser requestor);

        /// <summary>
        /// Gets the challenges manager for this room.
        /// </summary>
        IChallengeManager ChallengesManager
        {
            get;
        }
    }
}