﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.UX.Test
{
    public interface ILoginPresentation
    {
        
        // Es el valor de Username que el usuario haya escrito
	    string Username 
	    {
		    set;
	    }
	
	    // Es el valor de password que el usuario haya introducido
	    string Password
	    {
		    set;
	    }
	
	    // Determina si el usuario está autentificado
	    bool IsAuthenticated
	    {
		    get;
	    }
	
	    // Muestra un mensaje al usuario
	    string Message 
	    {
		    get;
	    }
	
	    // Determina si hay un mensaje visible para el usuario
	    bool MessageVisible
	    {
		    get;
	    }
	
	    void LoginClicked();
    }
}
