using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.UX.Test.Impl
{
    public class LoginPresentation : ILoginPresentation
    {

        private string _Username;
        private string _Password;
        private bool _IsAuthenticated;
        private string _Message;
        private bool _MessageVisible;

        public string Username
        {
            set
            {
                _Username = value;
            }
        }

        public string Password
        {
            set
            {
                _Password = value;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return _IsAuthenticated;
            }
        }

        public string Message
        {
            get
            {
                return _Message;
            }
        }

        public bool MessageVisible
        {
            get
            {
                return _MessageVisible;
            }
        }

        public void LoginClicked()
        {
            if (_Username == "admin" && _Password == "pass")
            {
                _IsAuthenticated = true;
                _Message = "";
                _MessageVisible = false;
            }
            else
            {
                _IsAuthenticated = false;
                _Message = "Usuario o contraseña incorrecta";
                _MessageVisible = true;
            }
        }

    }
}