namespace Game.Presentation.Tests.Stubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Game.Logic;

    class ChallengeStub : IChallenge
    {

        public ChallengeStub()
        {
        }

        #region IChallenge Members
        public IRoomUser Challenger
        {
            get;
            set;
        }

        public IRoomUser Challengee
        {
            get;
            set;
        }

        public ChallengeState State
        {
            get;
            set;
        }
        #endregion
    }
}
