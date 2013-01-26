using System;
namespace Game.Logic
{
    public interface IBattleSearcher
    {
        /*
            Busca una batalla con un oponente aleatorio (con ranking más similar posible),
            si el usuario ya había enviado una petición de búsqueda de batalla, la cuál sigue
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