namespace Game.Logic
{
    using System;
    using Game.Core;
    using DependencyLocation;

    internal enum RoomAction
    {
        Join = 0,
        Leave = 1,
    }

    /**
    *  Responsabilidades:
    *  Conocer el estado de un usuario en una sala y la sala en la que esta
    **/
    internal class RoomUser : IRoomUser
    {
        private IUserSession uSession;

        public RoomUser(IUserSession user, IRoom room)
            : this(user, room, InRoomState.Idle)
        {
        }

        public RoomUser(IUserSession user, IRoom room, InRoomState state)
        {
            IMessaging messaging = Dependency.Locator.GetSingleton<IMessaging>();

            /** 
             * Registrarse para recibir cambios de estado de usuario en el cuarto 
             * Estos cambios son disparados por el mismo 
             **/
            Action<object, Tuple<IUserSession, InRoomState>> 
                stateChangeHandler =
                    (key, tuple) =>
                    {
                        this.State = tuple.Item2;
                    };

            /**
             * Quitar su registro para recibir cambios de estado en el cuarto, al recibir
             * la acción RoomAction.Leave, la cuál es disparada por la capa de presentación
             * al llamar el método RoomsManager.Leave(), al salirse el usuario del cuarto
             **/
            Action<object, Tuple<IRoomUser, RoomAction>> leaveHandler = null;
            leaveHandler =
                (key, tuple) =>
                {
                    if (tuple.Item2 == RoomAction.Leave)
                    {
                        messaging.Unsubscribe(room, stateChangeHandler);
                        messaging.Unsubscribe(room, leaveHandler);
                    }
                };

            /**
             * Se subscribe el evento manejo de cambios de estado de usuario en la sala (InRoomState)
             **/
            messaging.Subscribe<Tuple<IUserSession, InRoomState>>(
                room,
                stateChangeHandler,
                tuple => tuple.Item1.Username == this.Username);

            /**
             * Se subscribe al evento de acciones hechas en la sala (RoomAction)
             **/
            messaging.Subscribe<Tuple<IRoomUser, RoomAction>>(
                room,
                leaveHandler,
                tuple => tuple.Item1.Username == this.Username);
        }

        public string Username
        {
            get
            {
                return uSession.Username;
            }
        }

        public InRoomState State
        {
            get;
            private set;
        }

        public IRoom Room
        {
            get;
            private set;
        }
    }
}
