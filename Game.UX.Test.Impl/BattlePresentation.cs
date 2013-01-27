using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.Presentation.TestImpl
{
  public class BattlePresentation : IBattlePresentation
  {

    private string _MiddleMsgTitle;
    private string _HarmInflictedTxt;
    private int _HarmInflictedPts;
    private string _HarmReceivedTxt;
    private int _HarmReceivedPts;
    private string _MiddleMsgFooter;
    private bool _ConfirmBtnEnabled;
    private IBattlePlayerPresentation _MyInfo;
    private IEnumerable<IBattleCardPresentation> _MyCards;
    private IBattlePlayerPresentation _OppInfo;
    private IEnumerable<IBattleCardPresentation> _OppCards;
    private bool _HasBattleEnded;
    private bool _IsWaitingForOpponent;

    public BattlePresentation()
    {
      BattlePlayerPresentation player = new BattlePlayerPresentation("admin", 1000, "HP: ");
      _MyInfo = (IBattlePlayerPresentation)player;
      BattlePlayerPresentation opp = new BattlePlayerPresentation("opponent", 1000, "HP: ");
      _OppInfo = (IBattlePlayerPresentation)opp;
      IBattleCardPresentation[] cardsP = new IBattleCardPresentation[4];
      IBattleCardPresentation[] cardsO = new IBattleCardPresentation[4];
      for (int i = 0; i < 4; i++)
      {
        BattleCardPresentation card = new BattleCardPresentation("Images/01squareN1.png", 100, 50);
        IBattleCardPresentation ibcp = (IBattleCardPresentation)card;
        cardsP[i] = ibcp;
        cardsO[i] = ibcp;
      }
      _MyCards = cardsP;
      _OppCards = cardsO;
      _MiddleMsgTitle = "Seleccione la carta a utilizar";
      _MiddleMsgFooter = "";
      _HarmInflictedTxt = "Daño producido: ";
      _HarmInflictedPts = 0;
      _HarmReceivedTxt = "Daño recibido: ";
      _HarmReceivedPts = 0;
      _ConfirmBtnEnabled = true;
      _HasBattleEnded = false;
      _IsWaitingForOpponent = false;
    }

    public string MiddleMsgTitle
    {
      get
      {
        return _MiddleMsgTitle;
      }
    }

    public string HarmInflictedTxt
    {
      get
      {
        return _HarmInflictedTxt;
      }
    }

    public int HarmInflictedPts
    {
      get
      {
        return _HarmInflictedPts;
      }
    }

    public string HarmReceivedTxt
    {
      get
      {
        return _HarmReceivedTxt;
      }
    }

    public int HarmReceivedPts
    {
      get
      {
        return _HarmReceivedPts;
      }
    }

    public string MiddleMsgFooter
    {
      get
      {
        return _MiddleMsgFooter;
      }
    }

    public bool ConfirmBtnEnabled
    {
      get
      {
        return _ConfirmBtnEnabled;
      }
    }

    public IBattlePlayerPresentation MyInfo
    {
      get
      {
        return _MyInfo;
      }
    }

    public IEnumerable<IBattleCardPresentation> MyCards
    {
      get
      {
        return _MyCards;
      }
    }

    public IBattlePlayerPresentation OppInfo
    {
      get
      {
        return _OppInfo;
      }
    }

    public IEnumerable<IBattleCardPresentation> OppCards
    {
      get
      {
        return _OppCards;
      }
    }

    public bool HasBattleEnded
    {
      get
      {
        return _HasBattleEnded;
      }
    }

    public bool IsWaitingForOpponent
    {
      get
      {
        return _IsWaitingForOpponent;
      }
    }

    public void ConfirmButtonPressed()
    {
      _HarmInflictedPts = 30;
      _HarmReceivedPts = 0;
      _MiddleMsgTitle = "Resultados";
      _MiddleMsgFooter = "Seleccione las cartas a desechar";
    }


    #region IBattlePresentation Members

    public string Title
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public bool HarmResultsVisible
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public ILogoutButtonPresentation LogoutButton
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    #endregion
  }
}