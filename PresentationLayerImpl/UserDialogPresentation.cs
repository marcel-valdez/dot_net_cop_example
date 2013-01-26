namespace Game.Presentation.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class UserDialogPresentation : IUserDialogPresentation
    {

        Action<int> mButtonHandler;

        public UserDialogPresentation(UserDialogOptions options)
        {
            this.Title = options.Title;
            this.Subtitle = options.Subtitle;
            this.DialogMessage = options.DialogMessage;
            this.ButtonsText = options.ButtonsText;
            this.mButtonHandler = options.ButtonHandler;
        }

        #region IUserDialogPresentation Members
        public virtual string Title
        {
            get;
            private set;
        }

        public virtual string Subtitle
        {
            get;
            private set;
        }

        public virtual string DialogMessage
        {
            get;
            private set;
        }

        public virtual IEnumerable<string> ButtonsText
        {
            get;
            private set;
        }

        public virtual int PressedButtonIndex
        {
            private get;
            set;
        }

        public virtual void ButtonClicked()
        {
            if (this.mButtonHandler != null)
            {
                this.mButtonHandler(this.PressedButtonIndex);
            }
        }
        #endregion
    }
}
