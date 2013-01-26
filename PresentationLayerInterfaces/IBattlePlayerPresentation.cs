namespace Game.Presentation
{
    // Es la interfaz del modelo de presentación de un jugador
    // durante una batalla
    [System.Diagnostics.Contracts.ContractClass(typeof(IBattlePlayerPresentationCodeContract))]
    public interface IBattlePlayerPresentation
    {
        // El nombre de usuario
        string Username
        {
            get;
        }

        // Puntos de vida restantes
        int LifePoints
        {
            get;
        }

        // Es el texto del label de puntos de vida "HP: "
        string LifePointsTxt
        {
            get;
        }
    }
}