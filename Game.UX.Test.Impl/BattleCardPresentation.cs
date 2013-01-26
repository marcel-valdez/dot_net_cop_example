using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.UX.Test.Impl
{
    internal class BattleCardPresentation : IBattleCardPresentation
    {

        private string _CardName;
        private string _ImageUrl;
        private int _AttackPoints;
        private int _DefensePoints;
        private bool _Selected;
        private bool _Selectable;

        public BattleCardPresentation(string url, int attack, int defense)
        {
            _CardName = "card";
            _ImageUrl = url;
            _AttackPoints = attack;
            _DefensePoints = defense;
            _Selectable = true;
        }

        public string CardName
        {
            get
            {
                return _CardName;
            }
        }

        public string ImageUrl
        {
            get
            {
                return _ImageUrl;
            }
        }

        public int AttackPoints
        {
            get
            {
                return _AttackPoints;
            }
        }

        public int DefensePoints
        {
            get
            {
                return _DefensePoints;
            }
        }

        public bool Selected
        {
            set
            {
                _Selected = value;
            }
        }

        public bool Selectable
        {
            get
            {
                return _Selectable;
            }
        }

    }
}