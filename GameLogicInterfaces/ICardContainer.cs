using System;
namespace Game.Logic
{
    public interface ICardContainer
    {
        /*
            Obtiene la carta identificada con la llave id
        */
        ICard GetCard(int id);

        /*
            Modifica o crea una nueva carta en la persistencia de datos
            Nota:
                Se pide la sesión de usuario IUserSession para verificar que el 
                usuario tiene los accesos de seguridad para grabar una carta nueva
        */
        ICard SaveCard(IUserSession session, ICard newCard);
    }
}