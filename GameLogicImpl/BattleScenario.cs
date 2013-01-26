namespace Game.Logic.Impl
{
    using System;

    internal class BattleScenario : IBattleScenario
    {
        public BattleScenario()
        {

        }

        public IPlayer PlayerA
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IPlayer PlayerB
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public BattleState State
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public BattleResult Result
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
