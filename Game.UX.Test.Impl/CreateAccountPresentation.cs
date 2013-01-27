using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.Presentation.TestImpl
{
    public class CreateAccountPresentation : ICreateAccountPresentation
    {

        private string _Username;
        private bool _UsernameVisible;
        private string _Password;
        private bool _PasswordVisible;
        private string _PasswordConfirmation;
        private bool _PasswordConfirmationVisible;
        private string _ResultMessage;
        private bool _ResultMessageVisible;
        private bool _CreationSucces;
        private bool _CreateButtonVisible;
        private bool _RedirectButtonVisible;

        public CreateAccountPresentation()
        {
            _UsernameVisible = true;
            _PasswordVisible = true;
            _PasswordConfirmationVisible = true;
            _CreateButtonVisible = true;
        }

        public string Username
        {
            set
            {
                _Username = value;
            }
        }

        public bool UsernameVisible
        {
            get
            {
                return _UsernameVisible;
            }
        }


        public string Password
        {
            set
            {
                _Password = value;
            }
        }

        public bool PasswordVisible
        {
            get
            {
                return _PasswordVisible;
            }
        }

        public string PasswordConfirmation
        {
            set
            {
                _PasswordConfirmation = value;
            }
        }

        public bool PasswordConfirmationVisible
        {
            get
            {
                return _PasswordConfirmationVisible;
            }
        }

        public string ResultMessage
        {
            get
            {
                return _ResultMessage;
            }
        }

        public bool ResultMessageVisible
        {
            get
            {
                return _ResultMessageVisible;
            }
        }

        public bool CreationSuccess
        {
            get
            {
                return _CreationSucces;
            }
        }

        public bool CreateButtonVisible
        {
            get
            {
                return _CreateButtonVisible;
            }
        }

        public bool RedirectButtonVisible
        {
            get
            {
                return _RedirectButtonVisible;
            }
        }

        public void CreateAccount()
        {
            if (_Username == "lotro")
            {
                _CreationSucces = false;
                _ResultMessage = "Error: ocurrio algun error";
                _ResultMessageVisible = true;
                _RedirectButtonVisible = false;
            }
            else
            {
                _UsernameVisible = false;
                _PasswordVisible = false;
                _PasswordConfirmationVisible = false;
                _CreationSucces = true;
                _ResultMessage = "Cuenta creada, ya puedes iniciar sesion";
                _ResultMessageVisible = true;
                _RedirectButtonVisible = true;
                _CreateButtonVisible = false;
            }
        }

    }
}