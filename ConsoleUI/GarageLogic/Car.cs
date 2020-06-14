using System;
using System.Collections.Generic;

namespace GarageLogic
{
    public class Car : Vehicle
    {
        public enum eColorType
        {
            Red,
            White,
            Black,
            Silver
        }

        private const int k_MinNumOfDoors = 2;
        private const int k_MaxNumOfDoors = 5;
        private eColorType m_Color;
        private int m_NumOfDoors;

        internal Car(eColorType i_Color, int i_NumOfDoors, string i_Model, string i_LicensePlate, Dictionary<Vehicle.eWheelData, string> i_Wheel, Energy i_EnergyType) : base(i_Model, i_LicensePlate, 4, i_Wheel, i_EnergyType)
        {
            m_Color = i_Color;
            m_NumOfDoors = i_NumOfDoors;
        }

        internal static int parseNumOfDoors(string i_NumOfDoors)
        {
            int numOfDoors;
            bool res = int.TryParse(i_NumOfDoors, out numOfDoors);
            if (!res)
            {
                throw new FormatException("Not A Valid Number");
            }

            if (numOfDoors < k_MinNumOfDoors || numOfDoors > k_MaxNumOfDoors)
            {
                string errorMsg = string.Format("Wrong number of doors. only 2 to 5 numbers of doors are allowed");
                throw new ValueOutOfRangeException(k_MinNumOfDoors, k_MaxNumOfDoors, errorMsg);
            }

            return numOfDoors;
        }

        internal static eColorType parseColor(string i_Color)
        {
            i_Color = i_Color.Trim().ToUpper();
            eColorType color;
            switch (i_Color)
            {
                case "RED":
                    color = eColorType.Red;
                    break;
                case "WHITE":
                    color = eColorType.White;
                    break;
                case "BLACK":
                    color = eColorType.Black;
                    break;
                case "SILVER":
                    color = eColorType.Silver;
                    break;
                default:
                    throw new FormatException("Wrong Known Color");
            }

            return color;
        }

        public override string ToString()
        {
            string carDetails = base.ToString();
            carDetails += "Car color: " + m_Color.ToString() + System.Environment.NewLine;
            carDetails += "Number of doors: " + m_NumOfDoors.ToString() + System.Environment.NewLine;
            return carDetails;
        }
    }
}
