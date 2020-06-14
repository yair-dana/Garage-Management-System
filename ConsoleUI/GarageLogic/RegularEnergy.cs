using System;

namespace GarageLogic
{
    public class RegularEnergy : Energy
    {
        public enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        internal readonly eFuelType r_FuleType;
        internal readonly float r_FullTank;
        internal float m_AmountOfLittersLeftInTank;

        internal static eFuelType parseFuelType(string i_FuelType)
        {
            i_FuelType = i_FuelType.Trim().ToUpper();
            eFuelType fuelType;
            switch (i_FuelType)
            {
                case "OCTAN98":
                    fuelType = eFuelType.Octan98;
                    break;
                case "OCTAN95":
                    fuelType = eFuelType.Octan95;
                    break;
                case "OCTAN96":
                    fuelType = eFuelType.Octan96;
                    break;
                case "SOLER":
                    fuelType = eFuelType.Soler;
                    break;
                default:
                    throw new FormatException("Unknown Fuel Type");
            }

            return fuelType;
        }

        internal RegularEnergy(eFuelType i_FuelType, float i_AmountOfLittersInTank, float i_FullTank)
        {
            r_FuleType = i_FuelType;
            if (i_AmountOfLittersInTank > i_FullTank)
            {
                throw new ValueOutOfRangeException(0, i_FullTank, "litters left in the tank is bigger than the tank itself");
            }

            m_AmountOfLittersLeftInTank = i_AmountOfLittersInTank;
            r_FullTank = i_FullTank;
        }

        public override float CurrentEneregy
        {
            get
            {
                return m_AmountOfLittersLeftInTank;
            }
        }

        public override float MaxEnergy
        {
            get
            {
                return r_FullTank;
            }
        }

        internal void fuel(float i_Litters)
        {
            if (m_AmountOfLittersLeftInTank + i_Litters > r_FullTank)
            {
                throw new ValueOutOfRangeException(0, r_FullTank - m_AmountOfLittersLeftInTank, "You can't fuel more litters then the tank can has");
            }
            else
            {
                m_AmountOfLittersLeftInTank += i_Litters;
            }
        }

        public override string ToString()
        {
            string RegularEnergyDetails = "Fuel type: " + r_FuleType.ToString() + System.Environment.NewLine;
            RegularEnergyDetails += "Full tank: " + r_FullTank.ToString() + System.Environment.NewLine;
            RegularEnergyDetails += "Litters left in tank: " + m_AmountOfLittersLeftInTank.ToString() + System.Environment.NewLine;
            return RegularEnergyDetails;
        }
    }
}
