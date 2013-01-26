using System;
namespace Game.Logic
{
    /// <summary>
    /// Es un efecto que puede realizar una carta
    /// </summary>
    public interface IEffect
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        string Description
        {
            get;
        }

        /// <summary>
        /// Gets the card attack multiplier.
        /// </summary>
        double CardAttackMultiplier
        {
            get;
        }

        /// <summary>
        /// Gets the card attack change.
        /// </summary>
        int CardAttackChange
        {
            get;
        }

        /// <summary>
        /// Gets the card defense multiplier.
        /// </summary>
        double CardDefenseMultiplier
        {
            get;
        }

        /// <summary>
        /// Gets the card defense change.
        /// </summary>
        int CardDefenseChange
        {
            get;
        }

        /// <summary>
        /// Gets the life points change.
        /// </summary>
        double LifePointsChange
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether [disable opponent effect].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [disable opponent effect]; otherwise, <c>false</c>.
        /// </value>
        bool DisableOpponentEffect
        {
            get;
        }

        /// <summary>
        /// Gets the probability of effect actually happening.
        /// </summary>
        double ProbabilityOfEffect
        {
            get;
        }

        /// <summary>
        /// Gets the affected player.
        /// </summary>
        AffectedPlayer Affected
        {
            get;
        }

        /// <summary>
        /// Gets the effect timing.
        /// </summary>
        MomentOfEffect EffectTiming
        {
            get;
        }
    }

    /// <summary>
    /// Turno en el que puede hacer efecto un IEffect
    /// </summary>
    public enum MomentOfEffect
    {
        CurrentTurn = 0,
        NextTurn = 1,
        NextTurns = 2,
        AllTurns = 3
        
    }

    /// <summary>
    /// Jugador al que hace efecto
    /// </summary>
    public enum AffectedPlayer
    {
        /// <summary>
        /// Affects the holder of the card
        /// </summary>
        Holder = 0,

        /// <summary>
        /// Affects the opponent
        /// </summary>
        Opponent = 1,

        /// <summary>
        /// Affects both players
        /// </summary>
        Both = 2
    }
}
