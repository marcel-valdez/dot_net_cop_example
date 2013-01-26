namespace Game.Presentation.Impl
{
    using System.Text;
    using Game.Logic;

    internal class BattleCardPresentation : IBattleCardPresentation
    {
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
            internal get;
            set;
        }

        public bool Selectable
        {
            get;
            internal set;
        }
        #endregion
    }
}
