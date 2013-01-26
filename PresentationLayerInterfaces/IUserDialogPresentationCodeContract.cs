namespace Game.Presentation
{
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Collections.Generic;

    [ContractClassFor(typeof(IUserDialogPresentation))]
    internal abstract class IUserDialogPresentationCodeContract : IUserDialogPresentation
    {
        #region IUserDialogPresentation Members
        public void ButtonClicked()
        {
            Contract.Requires(this.ButtonsText.Count() > 0);
        }

        public string Title
        {
            get
            {
                /* Siempre se requiere un título de diálogo */
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        public string Subtitle
        {
            get
            {
                /* Puede no tener subtítulo */
                return default(string);
            }
        }

        public string DialogMessage
        {
            get
            {
                /* Siempre se requiere un mensaje de diálogo */
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        public IEnumerable<string> ButtonsText
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<string>>().Count() > 0);
                Contract.Ensures(Contract.ForAll<string>(Contract.Result<IEnumerable<string>>(), value => !string.IsNullOrEmpty(value)));
                return default(IEnumerable<string>);
            }
        }

        public int PressedButtonIndex
        {
            set
            {
                Contract.Requires(value >= 0 && value < this.ButtonsText.Count());
            }
        }

        #endregion
    }
}
