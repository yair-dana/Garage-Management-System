namespace GarageLogic
{
    public class ElectricEnergy : Energy
    {
        internal readonly float r_FullBatteryInHours;
        internal float m_HoursLeftForEndingBattery;

        internal ElectricEnergy(float i_HoursLeftInBattery, float i_FullBatteryInHours)
        {
            if (i_HoursLeftInBattery > i_FullBatteryInHours)
            {
                throw new ValueOutOfRangeException(0, i_FullBatteryInHours, "Hours left in battery are bigger then maximum hours in battery");
            }
            else
            {
                m_HoursLeftForEndingBattery = i_HoursLeftInBattery;
                r_FullBatteryInHours = i_FullBatteryInHours;
            }
        }

        public override float CurrentEneregy
        {
            get
            {
                return m_HoursLeftForEndingBattery;
            }
        }

        public override float MaxEnergy
        {
            get
            {
                return r_FullBatteryInHours;
            }
        }

        internal void batteryCharging(float i_HoursToCharge)
        {
            if (i_HoursToCharge + m_HoursLeftForEndingBattery > r_FullBatteryInHours)
            {
                throw new ValueOutOfRangeException(0, r_FullBatteryInHours - m_HoursLeftForEndingBattery, "You cannot charge your battery more than its maxumim");
            }
            else
            {
                m_HoursLeftForEndingBattery += i_HoursToCharge;
            }
        }

        public override string ToString()
        {
            string ElectricEnergyDetails = "Full battery in hours: " + r_FullBatteryInHours.ToString() + System.Environment.NewLine;
            ElectricEnergyDetails += "Hours left in battery: " + m_HoursLeftForEndingBattery.ToString() + System.Environment.NewLine;
            return ElectricEnergyDetails;
        }
    }
}
