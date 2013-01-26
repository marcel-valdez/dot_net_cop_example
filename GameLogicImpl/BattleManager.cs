namespace Game.Logic.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DynamicModel;
    using Model;

    internal class BattleManager : IBattleManager
    {
        CachedList<BattleDirector> battles;

        public BattleManager()
        {
            battles = new CachedList<BattleDirector>(TimeSpan.FromMinutes(10));
        }
        /// <summary>
        /// Gets the ongoing battle of a given user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The battle director of an ongoing battle</returns>
        public IBattleDirector GetOngoingBattle(IRoomUser user)
        {
            return this.battles.FirstOrDefault(
                battle => battle.Scenario.State != BattleState.Concluded &&
                    battle.Scenario.State != BattleState.Aborted &&
                    (battle.Scenario.PlayerA.Username == user.Username ||
                     battle.Scenario.PlayerB.Username == user.Username));
        }

        internal void AddBattle(IRoomUser A, IRoomUser B)
        {
            // TODO: Agregar una batalla a la lista de batallas, crear Players en base a IRoomUser
            /*BattleDirector director = new BattleDirector();

            
            director.Scenario = new BattleScenario()
            {
                PlayerA = A,
                PlayerB = B,
                Result = BattleResult.None,
                State = BattleState.WaitingForCardElection
            };*/
        }
    }
}
