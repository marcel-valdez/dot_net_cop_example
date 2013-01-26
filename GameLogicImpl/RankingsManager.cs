namespace Game.Logic.Impl
{
    using System.Linq;
    using Model;
    using Game;
    using DynamicModel;
    using Game.Core;
    internal class RankingsManager : IRankingsManager
    {
        public RankingsManager()
        {

        }
        public IUserRanking GetUserRanking(string username)
        {
            return RequestContext.Model<Entities>().UserRanks.FirstOrDefault(rank => rank.Username == username);
        }
    }
}
