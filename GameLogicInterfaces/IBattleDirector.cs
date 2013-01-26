using System;
namespace Game.Logic
{
    public interface IBattleDirector
    {
        /*
            Es la batalla en ejecución
        */
        IBattleScenario Scenario
        {
            get;
        }

        /*
            Permite a un jugador escojer las cartas para el turno correspondiente,
            donde envía el IBattleDeck con las modificaciones (selección) de cartas
        */
        IOperationResult ChooseCards(IPlayer player, IBattleDeck deck);
    }
}