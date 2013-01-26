namespace Game.Logic.Impl
{
    using System;
    /*
            Responsabilidades:
            Escojer carta a lanzar, escojer carta(s) a desechar, delegar cálculo de resultados de daños
            Conocer los IPlayer dentro de una batalla específica
        */
    internal class BattleDirector : IBattleDirector
    {
        public BattleDirector()
        {

        }
        public IBattleScenario Scenario
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public IOperationResult ChooseCards(IPlayer player, IBattleDeck deck)
        {
            throw new NotImplementedException();
        }
    }
}
