using System;
using System.Collections.Generic;

namespace GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleInRepair> m_VehiclesInGarage;

        public Garage()
        {
            m_VehiclesInGarage = new Dictionary<string, VehicleInRepair>();
        }

        public Dictionary<string, VehicleInRepair> VehiclesInRepair
        {
            get
            {
                return m_VehiclesInGarage;
            }
        }

        public VehicleInRepair GetVehicleByLicenseNumber(string i_LicenseNumber)
        {
            return m_VehiclesInGarage[i_LicenseNumber];
        }

        public bool TryAddingNewVehicleToGarage(string i_OwnerName, string i_PhoneNumberOfOwner, List<string> i_DataMemory, SupportVehicle.eVehicleType i_VehicleType)
        {
            bool res = true;
            if (!IsLicenseNumberAlrdyExists(i_DataMemory[(int)SupportVehicle.eVehicleData.LicencePlate]))
            {
                m_VehiclesInGarage.Add(i_DataMemory[(int)SupportVehicle.eVehicleData.LicencePlate], new VehicleInRepair(i_OwnerName, i_PhoneNumberOfOwner, i_DataMemory, i_VehicleType));
            }
            else
            {
                changeVehicleConditionBackToInRepair(m_VehiclesInGarage[i_DataMemory[(int)SupportVehicle.eVehicleData.LicencePlate]].Vehicle.LicensePlate);
                res = false;
            }

            return res;
        }

        public bool IsLicenseNumberAlrdyExists(string i_LicenseNumber)
        {
            return m_VehiclesInGarage.ContainsKey(i_LicenseNumber);
        }

        public List<string> LicenseNumbersByConditions(VehicleInRepair.eVehicleCondition i_Condition)
        {
            List<string> licenseNumbers = new List<string>();
            foreach (KeyValuePair<string, VehicleInRepair> vehicle in m_VehiclesInGarage)
            {
                if (vehicle.Value.Condition == i_Condition)
                {
                    licenseNumbers.Add(vehicle.Key);
                }
            }

            return licenseNumbers;
        }

        public void ChangesVehicleCondition(string licenseNumber, VehicleInRepair.eVehicleCondition i_Condition)
        {
            if (m_VehiclesInGarage[licenseNumber].Condition == i_Condition)
            {
                throw new ArgumentException("Vehicle is allready in " + i_Condition.ToString() + " condition.");
            }
            else
            {
                m_VehiclesInGarage[licenseNumber].Condition = i_Condition;
            }
        }

        public void FillAirInWheels(string i_LicenseNumber)
        {
            bool res = m_VehiclesInGarage[i_LicenseNumber].Vehicle.fillAirInWheels();
            if (!res)
            {
                throw new ArgumentException("The Wheels Already fill to the Maximum");
            }
        }

        public void FuelRegularVehicle(string i_LicenseNumber, string i_FuelType, string i_AmountToFuel)
        {
            RegularEnergy vehicleEnergy = m_VehiclesInGarage[i_LicenseNumber].Vehicle.Energy as RegularEnergy;
            RegularEnergy.eFuelType fuelType = RegularEnergy.parseFuelType(i_FuelType);
            float amountToFuel;
            bool res = float.TryParse(i_AmountToFuel, out amountToFuel);
            if (vehicleEnergy == null)
            {
                throw new ArgumentException("The vehicle isn't using Fuel");
            }

            if (res && vehicleEnergy.r_FuleType == fuelType)
            {
                vehicleEnergy.fuel(amountToFuel);
                m_VehiclesInGarage[i_LicenseNumber].Vehicle.updatePecentageOfEnergy();
            }
            else
            {
                throw new ArgumentException("Wrong type of fuel for fueling");
            }
        }

        public void ChargeElectricVehicle(string i_LicenseNumber, string i_MinutesToCharge)
        {
            ElectricEnergy electricType = m_VehiclesInGarage[i_LicenseNumber].Vehicle.Energy as ElectricEnergy;
            float minToRechare = float.Parse(i_MinutesToCharge);
            if (electricType != null)
            {
                electricType.batteryCharging(minutesToHours(minToRechare));
                m_VehiclesInGarage[i_LicenseNumber].Vehicle.updatePecentageOfEnergy();
            }
            else
            {
                throw new ArgumentException("The Vehicle number " + i_LicenseNumber + "doesn't have electric Energy");
            }
        }

        private float minutesToHours(float i_Minutes)
        {
            float hours = i_Minutes / 60;
            if (i_Minutes > 60)
            {
                hours += (i_Minutes / 60) % 60;
            }

            return hours;
        }

        private void changeVehicleConditionBackToInRepair(string i_LicenseNumber)
        {
            m_VehiclesInGarage[i_LicenseNumber].Condition = VehicleInRepair.eVehicleCondition.inRepair;
        }
    }
}
