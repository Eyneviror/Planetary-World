namespace PlanetaryWorld.Events
{
    public class OnPickupAplayed : GlobalGameEvent
    {
        public OnPickupAplayed(PickupType currentType)
        {
            type = currentType;
        }

        public PickupType type;
    }
}