namespace Game.Presentation
{
    //EL DTO para los datos de usuario a mostrar en UX; DTO: Data Transfer Object - Se utiliza para transferir datos de una capa a otra, sin exponer los objetos/interfaces de una capa que debe esconderse: en este caso, para esconder el objeto IUser de la capa de lógica de juego
    [System.Diagnostics.Contracts.ContractClass(typeof(IRoomUserDTOCodeContract))]
    public interface IRoomUserDTO
    {
        // El nombre de usuario
        string Username
        {
            get;
        }

        // El estado de usuario en la sala: 
        // En Sala, Jugando, Esperando
        string RoomUserState
        {
            get;
        }
    }
}