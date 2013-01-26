namespace Game.Presentation.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class UserDialogOptions
    {
        public string Title
        {
            get;
            set;
        }

        public string Subtitle
        {
            get;
            set;
        }

        public string DialogMessage
        {
            get;
            set;
        }

        public IEnumerable<string> ButtonsText
        {
            get;
            set;
        }

        public Action<int> ButtonHandler
        {
            get;
            set;
        }
    }
}
