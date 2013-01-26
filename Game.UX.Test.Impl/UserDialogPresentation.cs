using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Game.UX.Test;

namespace Game.UX.Test.Impl
{
    public class UserDialogPresentation : IUserDialogPresentation
    {

        private string _Title;
        private string _Subtitle;
        private string _DialogMessage;
        private IEnumerable<string> _ButtonsText;
        private int _PressedButtonIndex;

        public UserDialogPresentation(string titl, string[] buttons, string dialogmsg)
        {
            _Title = titl;
            _ButtonsText = buttons;
            _DialogMessage = dialogmsg;
        }

        public string Title
        {
            get
            {
                return _Title;
            }
        }

        public string Subtitle
        {
            get
            {
                return _Subtitle;
            }
        }

        public string DialogMessage
        {
            get
            {
                return _DialogMessage;
            }
        }

        public IEnumerable<string> ButtonsText
        {
            get
            {
                return _ButtonsText;
            }
        }

        public int PressedButtonIndex
        {
            set
            {
                _PressedButtonIndex = value;
            }
        }

        public void ButtonClicked()
        {
        }

    }
}