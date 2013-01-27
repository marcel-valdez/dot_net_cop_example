using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.Presentation.TestImpl
{
    public class BattlePlayerPresentation : IBattlePlayerPresentation
    {

        private string _Username;
        private int _LifePoints;
        private string _LifePointsTxt;

        public BattlePlayerPresentation(string un, int lf, string lbtxt)
        {
            _Username = un;
            _LifePoints = lf;
            _LifePointsTxt = lbtxt;
        }

        public string Username
        {
            get
            {
                return _Username;
            }
        }

        public int LifePoints
        {
            get
            {
                return _LifePoints;
            }
        }

        public string LifePointsTxt
        {
            get
            {
                return _LifePointsTxt;
            }
        }

    }
}