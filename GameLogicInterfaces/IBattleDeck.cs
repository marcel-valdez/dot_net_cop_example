using System;
namespace Game.Logic
{
    using System.Collections.Generic;
    public interface IBattleDeck
    {
        /*
            Cartas disponibles en el deck de batalla
        */
        IEnumerable<ICard> Cards
        {
            get;
        }

        /*
            Número máximo de cartas a elegir
        */
        int MaxToChoose
        {
            get;
        }

        /*
            Permite elegir cartas del deck de batalla,
            no puede ser mayor al número de cartas determinadas en
            MaxToChoose, de lo contrario, se enviara un mensaje
            de error en IOperationResult
        */
        IOperationResult Choose(IEnumerable<ICard> chosen);

        /*
            Determina si ya se han elegido cartas
        */
        bool HasChosen
        {
            get;
        }
    }
}