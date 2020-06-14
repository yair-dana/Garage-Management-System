using System;
using System.Collections.Generic;
using System.Linq;

namespace GarageLogic
{
    public class SupportVehicle
    {
        internal const int k_MotorcycleMaxAirPressure = 30;
        internal const int k_CarMaxAirPressure = 32;
        internal const int k_TruckMaxAirPressure = 28;
        internal const int k_RegualrCarFullTank = 60;
        internal const int k_RegularMotorcycleFullTank = 7;
        internal const int k_RegularTruckFullTank = 120;
        internal const float k_ElectricCarMaxBatteryHourTime = 2.1f;
        internal const float k_ElectricMotorcycleMaxBatteryHourTime = 1.5f;

        public enum eVehicleType
        {
            RegularCar = 1,
            ElectricCar,
            RegularMotorcycle,
            ElectricMotorcycle,
            Truck,
        }

        public static readonly string[] r_KnownTypes =
        {
            "Regular Car",
            "Electric Car",
            "Regular Motorcycle",
            "Electric Motorcycle",
            "Truck"
        };

        public enum eVehicleData
        {
            Model,
            LicencePlate,
            CurrentAirPressure,
            LicenceType,
            EngineCapacity,
            NumOfDoors,
            HavingHazardousMeterials,
            CargoVolume,
            Color,
            AmountOfFuelLeft,
            HoursLeftInBattery,
        }

        public static Vehicle CreateVehicle(List<string> i_Properties, eVehicleType i_Type)
        {
            Dictionary<eVehicleData, string> dataMemory = new Dictionary<eVehicleData, string>();
            Dictionary<Vehicle.eWheelData, string> wheelsData = new Dictionary<Vehicle.eWheelData, string>();
            int indexInProperties = 0;
            dataMemory.Add(eVehicleData.Model, i_Properties[indexInProperties++]);
            dataMemory.Add(eVehicleData.LicencePlate, i_Properties[indexInProperties++]);
            wheelsData.Add(Vehicle.eWheelData.CurrentAirPressure, i_Properties[indexInProperties++]);
            checkAlpabeatStrings(i_Properties[indexInProperties]); // check manufactuereName
            wheelsData.Add(Vehicle.eWheelData.ManufacturerName, i_Properties[indexInProperties++]);
            switch (i_Type)
            {
                case eVehicleType.RegularCar:
                case eVehicleType.RegularMotorcycle:
                case eVehicleType.Truck:
                    dataMemory.Add(eVehicleData.AmountOfFuelLeft, i_Properties[indexInProperties++]);
                    break;
                case eVehicleType.ElectricCar:
                case eVehicleType.ElectricMotorcycle:
                    dataMemory.Add(eVehicleData.HoursLeftInBattery, i_Properties[indexInProperties++]);
                    break;
            }

            switch (i_Type)
            {
                case eVehicleType.RegularCar:
                case eVehicleType.ElectricCar:
                    dataMemory.Add(eVehicleData.Color, i_Properties[indexInProperties++]);
                    dataMemory.Add(eVehicleData.NumOfDoors, i_Properties[indexInProperties++]);
                    wheelsData.Add(Vehicle.eWheelData.MaxAirPressure, k_CarMaxAirPressure.ToString());
                    break;
                case eVehicleType.RegularMotorcycle:
                case eVehicleType.ElectricMotorcycle:
                    dataMemory.Add(eVehicleData.LicenceType, i_Properties[indexInProperties++]);
                    dataMemory.Add(eVehicleData.EngineCapacity, i_Properties[indexInProperties++]);
                    wheelsData.Add(Vehicle.eWheelData.MaxAirPressure, k_MotorcycleMaxAirPressure.ToString());
                    break;
                case eVehicleType.Truck:
                    dataMemory.Add(eVehicleData.HavingHazardousMeterials, i_Properties[indexInProperties++]);
                    dataMemory.Add(eVehicleData.CargoVolume, i_Properties[indexInProperties++]);
                    wheelsData.Add(Vehicle.eWheelData.MaxAirPressure, k_TruckMaxAirPressure.ToString());
                    break;
            }

            return CreateVehicle(dataMemory, i_Type, wheelsData);
        }

        public static List<string> GetPropertiesByVehicleType(eVehicleType i_Type)
        {
            List<string> properties = new List<string>();
            properties.Add("model name:");
            properties.Add("license plate:");
            properties.Add("current air pressure in wheels:");
            properties.Add("manufacturer name:");
            switch (i_Type)
            {
                case eVehicleType.RegularCar:
                case eVehicleType.RegularMotorcycle:
                case eVehicleType.Truck:
                    properties.Add("amount of fuel left:");
                    break;
                case eVehicleType.ElectricCar:
                case eVehicleType.ElectricMotorcycle:
                    properties.Add("hours left for battery usage:");
                    break;
            }

            switch (i_Type)
            {
                case eVehicleType.RegularCar:
                case eVehicleType.ElectricCar:
                    properties.Add("color:");
                    properties.Add("number of doors:");
                    break;
                case eVehicleType.RegularMotorcycle:
                case eVehicleType.ElectricMotorcycle:
                    properties.Add("license type:");
                    properties.Add("engine capacity:");
                    break;
                case eVehicleType.Truck:
                    properties.Add("status about having hazardous materials: ");
                    properties.Add("cargo volume:");
                    break;
            }

            return properties;
        }

        public static Vehicle CreateVehicle(Dictionary<eVehicleData, string> i_DataMemory, eVehicleType i_VehicleType, Dictionary<Vehicle.eWheelData, string> i_Wheel)
        {
            string licensePlate = i_DataMemory[eVehicleData.LicencePlate];
            string model = i_DataMemory[eVehicleData.Model];
            checkNumericStrings(licensePlate, i_Wheel[Vehicle.eWheelData.CurrentAirPressure]);
            Vehicle vehicle = null;
            switch (i_VehicleType)
            {
                case eVehicleType.RegularMotorcycle:
                    vehicle = CreateMotorCycle(licensePlate, model, i_DataMemory, i_Wheel, Energy.eEnergyType.Regular);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    vehicle = CreateMotorCycle(licensePlate, model, i_DataMemory, i_Wheel, Energy.eEnergyType.Electric);
                    break;
                case eVehicleType.RegularCar:
                    vehicle = CreateCar(licensePlate, model, i_DataMemory, i_Wheel, Energy.eEnergyType.Regular);
                    break;
                case eVehicleType.ElectricCar:
                    vehicle = CreateCar(licensePlate, model, i_DataMemory, i_Wheel, Energy.eEnergyType.Electric);
                    break;
                case eVehicleType.Truck:
                    vehicle = CreateTruck(licensePlate, model, i_DataMemory, i_Wheel, Energy.eEnergyType.Electric);
                    break;
            }

            return vehicle;
        }

        public static Car CreateCar(string i_LicensePlate, string i_Model, Dictionary<eVehicleData, string> i_DataMemory, Dictionary<Vehicle.eWheelData, string> i_Wheel, Energy.eEnergyType i_EnergyType)
        {
            checkNumericStrings(i_DataMemory[eVehicleData.NumOfDoors]);
            Car.eColorType color = Car.parseColor(i_DataMemory[eVehicleData.Color]);
            int numOfDoors = Car.parseNumOfDoors(i_DataMemory[eVehicleData.NumOfDoors]);
            Energy energyType = null;

            if (i_EnergyType == Energy.eEnergyType.Regular)
            {
                checkNumericStrings(i_DataMemory[eVehicleData.AmountOfFuelLeft]);
                energyType = new RegularEnergy(RegularEnergy.eFuelType.Octan96, float.Parse(i_DataMemory[eVehicleData.AmountOfFuelLeft]), k_RegualrCarFullTank);
            }
            else if (i_EnergyType == Energy.eEnergyType.Electric)
            {
                checkNumericStrings(i_DataMemory[eVehicleData.HoursLeftInBattery]);
                energyType = new ElectricEnergy(float.Parse(i_DataMemory[eVehicleData.HoursLeftInBattery]), k_ElectricCarMaxBatteryHourTime);
            }

            return new Car(color, numOfDoors, i_Model, i_LicensePlate, i_Wheel, energyType);
        }

        public static Truck CreateTruck(string i_LicensePlate, string i_Model, Dictionary<eVehicleData, string> i_DataMemory, Dictionary<Vehicle.eWheelData, string> i_Wheel, Energy.eEnergyType i_EnergyType)
        {
            checkNumericStrings(i_DataMemory[eVehicleData.CargoVolume], i_DataMemory[eVehicleData.AmountOfFuelLeft]);
            bool isHavingHazardousMeterials = Truck.parseHazardousMeterials(i_DataMemory[eVehicleData.HavingHazardousMeterials]);
            float cargoVolume = float.Parse(i_DataMemory[eVehicleData.CargoVolume]);

            Energy energyType = new RegularEnergy(RegularEnergy.eFuelType.Soler, float.Parse(i_DataMemory[eVehicleData.AmountOfFuelLeft]), k_RegularTruckFullTank);
            return new Truck(isHavingHazardousMeterials, cargoVolume, i_Model, i_LicensePlate, i_Wheel, energyType);
        }

        public static Motorcycle CreateMotorCycle(string i_LicensePlate, string i_Model, Dictionary<eVehicleData, string> i_DataMemory, Dictionary<Vehicle.eWheelData, string> i_Wheel, Energy.eEnergyType i_EnergyType)
        {
            checkNumericStrings(i_DataMemory[eVehicleData.EngineCapacity]);
            Motorcycle.eLicenseType licenceType = Motorcycle.parseLicenseType(i_DataMemory[eVehicleData.LicenceType]);
            int engineCapacity = int.Parse(i_DataMemory[eVehicleData.EngineCapacity]);
            Energy energyType = null;

            if (i_EnergyType == Energy.eEnergyType.Regular)
            {
                checkNumericStrings(i_DataMemory[eVehicleData.AmountOfFuelLeft]);
                energyType = new RegularEnergy(RegularEnergy.eFuelType.Octan95, float.Parse(i_DataMemory[eVehicleData.AmountOfFuelLeft]), k_RegularMotorcycleFullTank);
            }
            else if (i_EnergyType == Energy.eEnergyType.Electric)
            {
                checkNumericStrings(i_DataMemory[eVehicleData.HoursLeftInBattery]);
                energyType = new ElectricEnergy(float.Parse(i_DataMemory[eVehicleData.HoursLeftInBattery]), k_ElectricMotorcycleMaxBatteryHourTime);
            }

            return new Motorcycle(licenceType, engineCapacity, i_Model, i_LicensePlate, i_Wheel, energyType);
        }

        private static void checkNumericStrings(params string[] i_ListStrings)
        {
            foreach (string strToCheck in i_ListStrings)
            {
                if (!strToCheck.All(char.IsDigit))
                {
                    string failedMsg = string.Format("your input: {0}, should be Only Numeric input", strToCheck);
                    throw new FormatException(failedMsg);
                }
            }
        }

        private static void checkAlpabeatStrings(params string[] i_ListStrings)
        {
            foreach (string strToCheck in i_ListStrings)
            {
                if (!strToCheck.All(char.IsLetter))
                {
                    string failedMsg = string.Format("your input: {0}, should be Only Alpabeat input", strToCheck);
                    throw new FormatException(failedMsg);
                }
            }
        }
    }
}
