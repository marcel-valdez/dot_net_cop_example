using System;

namespace Game.Logic.Model
{
  /*
    Responsabilidades: Conocer los atributos y nombre de una carta de juego
  */
  internal partial class CardEffect : IEffect
  {
    #region IEffect Members


    AffectedPlayer IEffect.Affected
    {
      get
      {
        return (AffectedPlayer)this.Affected;
      }
    }

    MomentOfEffect IEffect.EffectTiming
    {
      get
      {
        return (MomentOfEffect)this.EffectTiming;
      }
    }

    #endregion
  }
}
