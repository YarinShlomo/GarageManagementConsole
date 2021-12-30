using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, GarageItem> r_MyGarage = new Dictionary<string, GarageItem>();

        public Garage()
        {
            r_MyGarage = new Dictionary<string, GarageItem>();
        }

        public GarageItem GetGarageItem(string i_LicensePlate)
        {
            GarageItem garageItem = null;
            bool isValid = r_MyGarage.TryGetValue(i_LicensePlate, out garageItem);

            if (isValid)
            {
                return garageItem;
            }
            else
            {
                throw new ArgumentException(string.Format("LicensePlate not found: <{0}>", i_LicensePlate));
            }
        }

        public bool IsVehicleInGarage(string i_LicensePlate)
        {
            return r_MyGarage.ContainsKey(i_LicensePlate);
        }

        public void AddVehicleToGarage(GarageItem i_NewGarageItem)
        {
            if (r_MyGarage.ContainsKey(i_NewGarageItem.Vehicle.LicensePlate))
            {
                i_NewGarageItem.CurrentStatus = GarageItem.eGarageStatus.InFix;
            }
            else
            {
                r_MyGarage.Add(i_NewGarageItem.Vehicle.LicensePlate, i_NewGarageItem);
            }
        }

        public List<string> CreateListByVehicleStatus(GarageItem.eGarageStatus? i_FilterStatus)
        {
            List<string> o_FilteredVehicles = new List<string>();

            if (i_FilterStatus != null)
            {
                foreach (KeyValuePair<string, GarageItem> garageItem in r_MyGarage)
                {
                    if (garageItem.Value.CurrentStatus == i_FilterStatus)
                    {
                        o_FilteredVehicles.Add(garageItem.Value.Vehicle.LicensePlate);
                    }
                }
            }
            else
            {
                o_FilteredVehicles.AddRange(r_MyGarage.Keys.ToList());
            }

            return o_FilteredVehicles;
        }

        public void ChangeVehicleGarageStatus(string i_LicensePlate, GarageItem.eGarageStatus i_NewStatus)
        {
            GarageItem garageItem = null;
            bool isValid = r_MyGarage.TryGetValue(i_LicensePlate, out garageItem);

            if(isValid)
            {
                garageItem.UpdateVehicleStatus(i_NewStatus);
            }
            else
            {
                throw new ArgumentException(string.Format("LicensePlate not found: <{0}>", i_LicensePlate));
            }
        }

        public void InflateAllWheels(string i_LicensePlate)
        {
            GarageItem garageItem = null;
            bool isValid = r_MyGarage.TryGetValue(i_LicensePlate, out garageItem);

            if (isValid)
            {
                garageItem.Vehicle.PumpAirToWheelsFull();
            }
            else
            {
                throw new ArgumentException(string.Format("LicensePlate not found: <{0}>", i_LicensePlate));
            }
        }

        public void PumpAirToWheelsByAmount(string i_LicensePlate, float i_amountToAdd)
        {
            GarageItem garageItem;
            bool isValid = r_MyGarage.TryGetValue(i_LicensePlate, out garageItem);
            List<Wheel> vehicleWheels;

            if (isValid)
            {
                if (i_amountToAdd < 0)
                {
                    throw new ValueOutOfRangeException(garageItem.Vehicle.Wheels[0].MaxAirPressure, 0, "Cannot fill negative number");
                }

                vehicleWheels = garageItem.Vehicle.Wheels;
                foreach (Wheel wheel in vehicleWheels)
                {
                    wheel.CurrentAirPressure = i_amountToAdd;
                }
            }
            else
            {
                throw new ArgumentException(string.Format("LicensePlate not found: <{0}>", i_LicensePlate));
            }
        }

        public void RefuelVehicle(string i_LicensePlate, FuelVehicle.eFuelType i_FuelType, float i_AmountToAdd)
        {
            GarageItem garageItem = GetGarageItem(i_LicensePlate);
            
            if(garageItem.Vehicle is IFuelable)
            {
                (garageItem.Vehicle as IFuelable).AddFuel(i_AmountToAdd, i_FuelType);
            }
            else
            {
                throw new ArgumentException("Cannot fuel non-fuel type vehicle");
            }
        }

        public void ReChargeVehicle(string i_LicensePlate, float i_AmountToAdd)
        {
            GarageItem garageItem = GetGarageItem(i_LicensePlate);
            if(garageItem.Vehicle is IChargable)
            {
                (garageItem.Vehicle as IChargable).ChargeBattery(i_AmountToAdd);
            }
            else
            {
                throw new ArgumentException("Cannot charge non-electric vehicle");
            }
        }
        
        public string GetVehicleData(string i_LicensePlate)
        {
            GarageItem garageItem = GetGarageItem(i_LicensePlate);

            return garageItem.ToString();
        }

        public List<string> GetFuelTypes()
        {
            List<string> fuelTypes = new List<string>();

            fuelTypes.AddRange(Enum.GetNames(typeof(FuelVehicle.eFuelType)));

            return fuelTypes;
        }

        public List<string> GetCarColors()
        {
            List<string> carColors = new List<string>();

            carColors.AddRange(Enum.GetNames(typeof(ElectricCar.eCarColor)));

            return carColors;
        }
    }
}
