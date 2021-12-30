using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public enum eVehicle
        {
            FuelMotorcycle = 1,
            ElectricMotorcycle,
            FuelCar,
            ElectricCar,
            FuelTruck
        }

        public Vehicle CreateVehicle(eVehicle i_VehicleChosen, List<string> i_VehicleParams)
        {
            Vehicle vehicle = null;

            switch (i_VehicleChosen)
            {
                case eVehicle.FuelMotorcycle:
                    vehicle = new FuelMotorcycle(
                        i_VehicleParams[0],
                        (FuelMotorcycle.eLicenseType)Enum.Parse(typeof(FuelMotorcycle.eLicenseType), i_VehicleParams[1]),
                        int.Parse(i_VehicleParams[2]));
                    break;

                case eVehicle.ElectricMotorcycle:
                    vehicle = new ElectricMotorcycle(
                        i_VehicleParams[0],
                        (ElectricMotorcycle.eLicenseType)Enum.Parse(typeof(ElectricMotorcycle.eLicenseType), i_VehicleParams[1]),
                        int.Parse(i_VehicleParams[2]));
                    break;

                case eVehicle.FuelCar:
                    vehicle = new FuelCar(
                        i_VehicleParams[0],
                        (FuelCar.eNumOfDoors)Enum.Parse(typeof(FuelCar.eNumOfDoors), i_VehicleParams[1]));
                    break;

                case eVehicle.ElectricCar:
                    vehicle = new ElectricCar(
                        i_VehicleParams[0],
                        (ElectricCar.eNumOfDoors)Enum.Parse(typeof(ElectricCar.eNumOfDoors), i_VehicleParams[1]));
                    break;

                case eVehicle.FuelTruck:
                    vehicle = new FuelTruck(i_VehicleParams[0], float.Parse(i_VehicleParams[1]));
                    break;

                default:
                    throw new ArgumentException("Invalid car type.");
            }

            return vehicle;
        }

        public List<string> ArgumentsNeedForCreation(eVehicle i_VehicleChosen)
        {
            List<string> ArgumentsNeeded = new List<string>();

            switch (i_VehicleChosen)
            {
                case eVehicle.FuelMotorcycle:
                case eVehicle.ElectricMotorcycle:

                    ArgumentsNeeded.Add(string.Format(
@"License Type:
A
A2
AA
B"));
                    ArgumentsNeeded.Add("Engine Volume");
                    break;

                case eVehicle.FuelCar:
                case eVehicle.ElectricCar:

                    ArgumentsNeeded.Add(string.Format(
@"Number of doors:
Two
Three
Four
Five"));
                    break;

                case eVehicle.FuelTruck:
                    ArgumentsNeeded.Add("Max Carrying weight");
                    break;

                default:
                    break;
            }

            return ArgumentsNeeded;
        }
    }
}
