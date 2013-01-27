namespace Game.Logic.Impl
{
  using System.Linq;
  using Game.Logic.Model;
  /*
          Conocer las estadísticas de los usuarios
      */
  internal class UserStats : IUserStats
  {
    public UserStats(Account acc)
    {      
      this.Sync(acc);
    }

    private void Sync(Account account)
    {
      this.Username = account.Username;
      int totalBattles = account.Battles.Count();
      int wonABattles = account.BattlesA.Where(battle => battle.Result == 1).Count();
      int wonBBattles = account.BattlesB.Where(battle => battle.Result == 2).Count();
      int tiedBattles = account.Battles.Where(battle => battle.Result == 0).Count();

      this.BattlesWon = wonBBattles + wonABattles;
      this.BattlesTied = tiedBattles;
      this.BattlesLost = totalBattles - BattlesWon - tiedBattles;
      this.Ranking = (int)Game.Core.RequestContext.Model<Entities>()
                                   .UserRanks.FirstOrDefault(u => u.Username == this.Username)
                                   .Rank;
    }

    public string Username
    {
      get;
      private set;
    }
    public int BattlesWon
    {
      get;
      private set;
    }
    public int BattlesLost
    {
      get;
      private set;
    }
    public int BattlesTied
    {
      get;
      private set;
    }
    public int Ranking
    {
      get;
      private set;
    }
  }
}
