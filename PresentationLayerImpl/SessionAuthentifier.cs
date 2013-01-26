namespace Game.Presentation.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Game.Logic;
    using Properties;
    using DependencyLocation;

    internal static class SessionHelper
    {
        public static IUserSession GetSession(string sessionId)
        {
            var accMgr = Dependency.Locator.GetSingleton<IAccountManager>();
            IOperationResult<IUserSession> operation = accMgr.GetUserSession(sessionId);
            if (operation.Result != ResultValue.Success)
            {
                throw new SecurityException(Resources.ErrorUserNotAuthentified);
            }

            return operation.ResultData;
        }

        public static IRoomUser GetRoomUser(IUserSession session)
        {
            var result = Dependency.Locator.GetSingleton<IRoomsManager>()
                               .GetRoomUser(session);
            if (result.Result != ResultValue.Success)
            {
                throw new NavigationException("Error: Aún no ha navegado dentro de una sala.");
            }

            return result.ResultData;
        }

        public static IRoomUser GetRoomUser(string sessionId)
        {
            IUserSession session = GetSession(sessionId);
            return GetRoomUser(session);
        }

        public static IPlayer GetBattlePlayer(string sessionId)
        {
            return GetBattlePlayer(GetSession(sessionId));
        }

        public static IPlayer GetBattlePlayer(IUserSession session)
        {
            var battle = Dependency.Locator.GetSingleton<IBattleManager>()
                                   .GetOngoingBattle(SessionHelper.GetRoomUser(session.SessionId));

            if (battle.Scenario.PlayerA.Username.Equals(session.Username))
            {
                return battle.Scenario.PlayerA;
            }
            else
            {
                return  battle.Scenario.PlayerB;
            }
        }
    }
}
