using System;

namespace Game.Logic.Model
{

    internal partial class Card : ICard
    {

        #region ICard Members

        string ICard.Name
        {
            get
            {
                return this.Name;
            }
        }

        int ICard.AttackPoints
        {
            get
            {
                return this.AttackPoints;
            }
        }

        int ICard.DefensePoints
        {
            get
            {
                return this.DefensePoints;
            }
        }

        IEffect ICard.Effect
        {
            get
            {
                return this.Effect;
            }
        }

        string ICard.ImageUrl
        {
            get
            {
                return this.ImageUrl;
            }
        }

        #endregion
    }
}
