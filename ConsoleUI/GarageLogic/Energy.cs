namespace GarageLogic
{
    public abstract class Energy
    {
        public enum eEnergyType
        {
            Regular = 1,
            Electric
        }

        public abstract float CurrentEneregy
        {
            get;
        }

        public abstract float MaxEnergy
        {
            get;
        }
    }
}
