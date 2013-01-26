using System;
namespace Game.Logic
{
    public interface IBattleSearcher
    {
        /*
            Busca una batalla con un oponente aleatorio (con ranking m�s similar posible),
            si el usuario ya hab�a enviado una petici�n de b�squeda de batalla, la cu�l sigue
            pendiente, entonces se le entrega el mismo objeto
        */
        IBattleRequest FindBattle(IRoomUser user);

        /*
            Obtiene la batalla peticionada por el usuario IRoomUser, si el usuario no
            ha enviado ninguna batalla, entonces se regresa NULL
        */
        IBattleRequest GetRequestedBattle(IRoomUser user);
    }
}