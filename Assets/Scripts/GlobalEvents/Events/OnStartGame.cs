namespace PlanetaryWorld.Events
{
    public class OnStartGame : GlobalGameEvent
    {
        public OnStartGame(bool state)
        {
            ObjectsCanMove = state;
        }

        public bool ObjectsCanMove;
    }
}