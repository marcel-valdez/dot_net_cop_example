namespace Game.Logic.Impl
{
    using System.Linq;
    using System.Data;
    using System.Data.EntityClient;
    using System;
    using Game.Logic.DynamicModel;
    using Game.Logic.Model;
    using System.Diagnostics.Contracts;
    using System.Text.RegularExpressions;
    using Game.Core;
    /*
            Responsabilidades:
            Conocer las estadísticas de los usuarios existentes
        */
    internal class UserStatsManager : IUserStatsManager
    {
        public UserStatsManager()
        {

        }

        public IOperationResult<IUserStats> GetUserStats(string username)
        {
            var account = RequestContext.Model<Entities>().Accounts.FirstOrDefault(acc => acc.Username == username);
            if (account == default(Account))
            {
                return new OperationResult<IUserStats>(ResultValue.Fail, 
                           username + " no existe en los registros.", 
                           default(IUserStats));
            }

            return new OperationResult<IUserStats>(ResultValue.Success,
                           "",
                           new UserStats(account));
        }
    }
}
