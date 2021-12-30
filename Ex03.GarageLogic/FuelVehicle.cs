using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class FuelVehicle : Vehicle, IFuelable
    {
        private readonly eFuelType r_FuelType;
        private readonly float r_MaxAmountOfGas;
        private float m_CurrentAmountOfGas = 0;

        public FuelVehicle(string i_LicensePlate, eFuelType i_FuelType, float i_MaxAmountOfGas, int i_NumOfWheels, float i_MaxAirPressure)
            : base(i_LicensePlate, i_NumOfWheels, i_MaxAirPressure)
        {
            if(Enum.IsDefined(typeof(eFuelType), i_FuelType))
            {
                r_FuelType = i_FuelType;
            }
            else
            {
                throw new FormatException(string.Format("Invalid Input: {0}, is not a valid fuel type", i_FuelType));
            }

            r_MaxAmountOfGas = i_MaxAmountOfGas;
        }

        public float CurrentAmountOfGas
        {
            get
            {
                return m_CurrentAmountOfGas;
            }
        }

        public float MaxAmountOfGas
        {
            get
            {
                return r_MaxAmountOfGas;
            }
        }

        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }
        
        float IFuelable.CurrentGasTank()
        {
            return m_CurrentAmountOfGas;
        }

        float IFuelable.MaxFuelTank()
        {
            return r_MaxAmountOfGas;
        }

        void IFuelable.AddFuel(float i_AmountToAdd, eFuelType i_FuelType)
        {
            if (!Enum.Equals(r_FuelType, i_FuelType))
            {
                throw new ArgumentException(string.Format("Error:This vehicle can only use {0} fuel", r_FuelType));
            }

            if (i_AmountToAdd + m_CurrentAmountOfGas > r_MaxAmountOfGas || i_AmountToAdd <= 0)
            {
                throw new ValueOutOfRangeException(
                    r_MaxAmountOfGas,
                    0,
                    string.Format("Tried to add {0} liters of fuel but can only add more {1}", i_AmountToAdd, r_MaxAmountOfGas - m_CurrentAmountOfGas));
            } 
            
            m_CurrentAmountOfGas += i_AmountToAdd;
            m_EnergyLevel = m_CurrentAmountOfGas / r_MaxAmountOfGas;
        }

        public void PumpFuelToFull()
        {
            m_CurrentAmountOfGas = r_MaxAmountOfGas;
        }

        public override void SetUpCar()
        {
            base.SetUpCar();
            PumpFuelToFull();
        }

        public enum eFuelType
        {
            Octan98 = 1,
            Octan96,
            Octan95,
            Soler
        }

        public override string ToString()
        {
            StringBuilder fuelVehicleInfo = new StringBuilder().Append(base.ToString());

            fuelVehicleInfo.AppendFormat(
@"Fuel type: {0}
Max amount of fuel: {1}
Current amount of fuel: {2}",
r_FuelType.ToString(),
r_MaxAmountOfGas,
m_CurrentAmountOfGas);

            return fuelVehicleInfo.ToString();
        }
    }
}
