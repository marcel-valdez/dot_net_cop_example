namespace Game.Presentation
{
    [System.Diagnostics.Contracts.ContractClass(typeof(IStatisticsPresentationCodeContract))]
    public interface IStatisticsPresentation
    {
        // Es el nombre del usuario a qui�n pertenecen las estad�sticas
        string Username
        {
            get;
        }

        // El texto label de "Partidas Jugadas"
        string PlayedGamesLabelText
        {
            get;
        }

        // El n�mero de partidas jugadas
        int PlayedGamesCount
        {
            get;
        }

        // El texto del label de "Partidas Ganadas"
        string WonGamesLabelText
        {
            get;
        }

        // El n�mero de partidas ganadas
        int WonGamesCount
        {
            get;
        }

        // El texto del label de "Partidas Perdidas"
        string LostGamesLabelText
        {
            get;
        }

        // El n�mero de partidas perdidas
        int LostGamesCount
        {
            get;
        }

        string RankingLabelText
        {
            get;
        }

        // El ranking global del jugador
        int Ranking
        {
            get;
        }
    }
}