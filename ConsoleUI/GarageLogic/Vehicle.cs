using System.Collections.Generic;

namespace GarageLogic
{
    public abstract class Vehicle
    {
        public enum eWheelData
        {
            ManufacturerName,
            CurrentAirPressure,
            MaxAirPressure
        }

        internal class Wheel
        {
            private readonly string r_ManufacturerName;
            private readonly float r_MaxAirPressure;
            private float m_CurrentAirPressure;

            internal void fillAirInWheel(ref bool io_Res)
            {
                if (m_CurrentAirPressure != r_MaxAirPressure)
                {
                    m_CurrentAirPressure = r_MaxAirPressure;
                    io_Res = true;
                }
            }

            public Wheel(string i_ManurfacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
            {
                r_ManufacturerName = i_ManurfacturerName;
                if (i_CurrentAirPressure > i_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(0, i_MaxAirPressure, "Current air pressure is more then the maximum of the air pressure of the wheel can has");
                }

                m_CurrentAirPressure = i_CurrentAirPressure;
                r_MaxAirPressure = i_MaxAirPressure;
            }

            public override string ToString()
            {
                string wheelDetails = "Manufacturer of wheels: " + r_ManufacturerName + System.Environment.NewLine;
                wheelDetails += "Max air pressure in wheels: " + r_MaxAirPressure.ToString() + System.Environment.NewLine;
                wheelDetails += "Current air pressure  in wheels: " + m_CurrentAirPressure.ToString() + System.Environment.NewLine;
                return wheelDetails;
            }
        }

        private readonly string r_Model;
        private readonly string r_LicensePlate;
        private float m_PercentageOfEnergyRemaining;
        private Wheel[] m_Wheels;
        private readonly int r_NumOfWheels;
        private readonly Energy r_EnergyType;

        internal Vehicle(string i_Model, string i_LicensePlate, int i_NumOfWheels, Dictionary<eWheelData, string> i_Wheel, Energy i_EnergyType)
        {
            r_Model = i_Model;
            r_LicensePlate = i_LicensePlate;
            r_NumOfWheels = i_NumOfWheels;
            m_Wheels = Wheels(i_Wheel, i_NumOfWheels);
            r_EnergyType = i_EnergyType;
            updatePecentageOfEnergy();
        }

        private Wheel[] Wheels(Dictionary<eWheelData, string> i_Wheel, int i_NumOfWheels)
        {
            Wheel[] Wheels = new Wheel[i_NumOfWheels];
            string manufacturer = i_Wheel[eWheelData.ManufacturerName];
            float maxAirPressure = float.Parse(i_Wheel[eWheelData.MaxAirPressure]);
            float currentAirPressure = float.Parse(i_Wheel[eWheelData.CurrentAirPressure]);
            for (int i = 0; i < i_NumOfWheels; i++)
            {
                Wheels[i] = new Wheel(manufacturer, currentAirPressure, maxAirPressure);
            }

            return Wheels;
        }

        public string LicensePlate
        {
            get
            {
                return r_LicensePlate;
            }
        }

        public Energy Energy
        {
            get
            {
                return r_EnergyType;
            }
        }

        internal bool fillAirInWheels()
        {
            bool succeessFillAir = false;
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.fillAirInWheel(ref succeessFillAir);
            }

            return succeessFillAir;
        }

        internal void updatePecentageOfEnergy()
        {
            m_PercentageOfEnergyRemaining = r_EnergyType.CurrentEneregy * 100 / r_EnergyType.MaxEnergy;
        }

        public override string ToString()
        {
            string vehicleDetails = "Model: " + r_Model + System.Environment.NewLine;
            vehicleDetails += "License Plate: " + r_LicensePlate + System.Environment.NewLine;
            vehicleDetails += "Percentage left fuel: " + m_PercentageOfEnergyRemaining.ToString("0.00") + "%" + System.Environment.NewLine;
            vehicleDetails += "Numbers Of Wheels: " + r_NumOfWheels + System.Environment.NewLine;
            vehicleDetails += m_Wheels[0].ToString();
            vehicleDetails += r_EnergyType.ToString();
            return vehicleDetails;
        }
    }
}
