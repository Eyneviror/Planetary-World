namespace PlanetaryWorld.Events
{
    public class OnEndGame : GlobalGameEvent
    {
        public OnEndGame(bool state)
        {
            ObjectsCanMove = state;
        }

        public bool ObjectsCanMove;
    }
}