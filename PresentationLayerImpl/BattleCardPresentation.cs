namespace Game.Presentation.Impl
{
  using System.Text;
  using Game.Logic;

  internal class BattleCardPresentation : IBattleCardPresentation
  {
    private bool mSelectable;
    private bool mSelected;
    ICard mCard;
    public BattleCardPresentation(ICard card)
    {
      this.mCard = card;
    }
    #region IBattleCardPresentation Members
    public string CardName
    {
      get
      {
        return mCard.Name;
      }
    }

    public string ImageUrl
    {
      get
      {
        return mCard.ImageUrl;
      }
    }

    public int AttackPoints
    {
      get
      {
        return mCard.AttackPoints;
      }
    }

    public int DefensePoints
    {
      get
      {
        return mCard.DefensePoints;
      }
    }

    public bool Selected
    {
      internal get
      {
        return mSelected;
      }

      set
      {
        this.mSelected = value;        
      }
    }

    public bool Selectable
    {
      get
      {
        return mSelectable && !this.Selected;
      }

      internal set
      {
        mSelectable = value;
      }
    }
    #endregion
  }
}
