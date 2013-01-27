using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.Presentation.TestImpl
{
    public class StatisticsPresentation : IStatisticsPresentation
    {

        private string _Username;
        private string _PlayedGamesLabelText = "Partidas jugadas: ";
        private int _PlayedGamesCount;
        private string _WonGamesLabeltext = "Partidas ganadas: ";
        private int _WonGamesCount;
        private string _LostGamesLabelText = "Partidas perdidas: ";
        private int _LostGamesCount;

        public StatisticsPresentation(string un, int pg, int wg)
        {
            _Username = un;
            _PlayedGamesCount = pg;
            _WonGamesCount = wg;
            _LostGamesCount = pg - wg;
        }

        public string Username
        {
            get
            {
                return _Username;
            }
        }

        public string PlayedGamesLabelText
        {
            get
            {
                return _PlayedGamesLabelText;
            }
        }

        public int PlayedGamesCount
        {
            get
            {
                return _PlayedGamesCount;
            }
        }

        public string WonGamesLabelText
        {
            get
            {
                return _WonGamesLabeltext;
            }
        }

        public int WonGamesCount
        {
            get
            {
                return _WonGamesCount;
            }
        }

        public string LostGamesLabelText
        {
            get
            {
                return _LostGamesLabelText;
            }
        }

        public int LostGamesCount
        {
            get
            {
                return _LostGamesCount;
            }
        }


        #region IStatisticsPresentation Members


        public string RankingLabelText
        {
          get
          {
            throw new NotImplementedException();
          }
        }

        public int Ranking
        {
          get
          {
            throw new NotImplementedException();
          }
        }

        #endregion
    }
}