namespace Game.Presentation
{
    using System.Diagnostics.Contracts;

    // Es la interfaz del modelo de presentación de una carta durante
    // una batalla
    [ContractClass(typeof(IBattleCardPresentationCodeContract))]
    public interface IBattleCardPresentation
    {
        // El nombre de la carta
        string CardName
        {
            get;
        }

        // Url de la imagen de la carta
        string ImageUrl
        {
            get;
        }

        // Puntos de ataque de la carta
        int AttackPoints
        {
            get;
        }

        // Puntos de defensa de la carta
        int DefensePoints
        {
            get;
        }

        // Determina si la carta ha sido seleccionada
        bool Selected
        {
            set;
        }

        // Determina si la carta puede ser seleccionada o no
        bool Selectable
        {
            get;
        }
    }
}