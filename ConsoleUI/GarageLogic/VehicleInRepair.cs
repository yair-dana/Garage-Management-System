using System.Collections.Generic;

namespace GarageLogic
{
    public class VehicleInRepair
    {
        private readonly Vehicle r_TypeOfVehicle;
        private string m_Owner;
        private string m_PhoneNumberOfOwner;
        private eVehicleCondition m_VehicleCondition;

        public enum eVehicleCondition
        {
            inRepair,
            wasFixed,
            paidUp
        }

        internal VehicleInRepair(string i_OwnerName, string i_PhoneNumberOfOwner, List<string> i_DataMemory, SupportVehicle.eVehicleType i_VehicleType)
        {
            r_TypeOfVehicle = SupportVehicle.CreateVehicle(i_DataMemory, i_VehicleType);
            m_Owner = i_OwnerName;
            m_PhoneNumberOfOwner = i_PhoneNumberOfOwner;
            m_VehicleCondition = eVehicleCondition.inRepair;
        }

        public Vehicle Vehicle
        {
            get
            {
                return r_TypeOfVehicle;
            }
        }

        internal eVehicleCondition Condition
        {
            get
            {
                return m_VehicleCondition;
            }

            set
            {
                m_VehicleCondition = value;
            }
        }

        public override string ToString()
        {
            string vehicleInRepairDetails = "Owner name: " + m_Owner + System.Environment.NewLine;
            vehicleInRepairDetails += "Phone number: " + m_PhoneNumberOfOwner + System.Environment.NewLine;
            vehicleInRepairDetails += "Status: " + m_VehicleCondition.ToString() + System.Environment.NewLine;
            vehicleInRepairDetails += r_TypeOfVehicle.ToString();
            return vehicleInRepairDetails;
        }
    }
}
