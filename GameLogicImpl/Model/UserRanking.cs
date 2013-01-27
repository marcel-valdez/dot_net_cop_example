namespace Game.Logic.Model
{
  using System;
  /*
    Responsabilidades:
    Conoce el ranking de un usuario
  */
  internal partial class UserRank : IUserRanking
  {
    #region IUserRanking Members

    string IUserRanking.Username
    {
      get
      {
        return this.Username;
      }
    }

    int IUserRanking.Ranking
    {
      get
      {
        return (int)this.Rank;
      }
    }

    #endregion
  }
}
