using System;
namespace Game.Logic
{
    public interface IBattleDirector
    {
        /*
            Es la batalla en ejecuci�n
        */
        IBattleScenario Scenario
        {
            get;
        }

        /*
            Permite a un jugador escojer las cartas para el turno correspondiente,
            donde env�a el IBattleDeck con las modificaciones (selecci�n) de cartas
        */
        IOperationResult ChooseCards(IPlayer player, IBattleDeck deck);
    }
}