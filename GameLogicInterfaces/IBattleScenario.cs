using System;
namespace Game.Logic
{
    public interface IBattleScenario
    {
        /* Uno de los dos jugadores en la batalla */
        IPlayer PlayerA
        {
            get;
        }

        /* Uno de los dos jugadores en la batalla */
        IPlayer PlayerB
        {
            get;
        }

        /*
            El estado (ciclo de vida) de la batalla:
            { WaitingForCardElection | WaitingForCardRemoval | CalculatingResult | Concluded | Aborted }
        */
        BattleState State
        {
            get;
        }

        /*
            El resultado de la batalla:
            { PlayerAWon | PlayerBWon | Draw | None }
            None se usa cuando la batalla fue abortada.
        */
        BattleResult Result
        {
            get;
        }
    }

    public enum BattleState
    {
        WaitingForCardElection = 0,
        WaitingForCardRemoval = 1,
        CalculatingResult = 2,
        Concluded = 3,
        Aborted = 4
    }

    public enum BattleResult
    {
        PlayerAWon = 0,
        PlayerBWon = 1,
        Draw = 2,
        None = 3
    }
}