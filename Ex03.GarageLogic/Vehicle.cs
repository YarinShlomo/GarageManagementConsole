using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_LicensePlate;
        private readonly List<Wheel> r_Wheels;
        private string m_Brand;
        protected float m_EnergyLevel = 0;

        public Vehicle(string i_LicensePlate, int i_NumOfWheels, float i_MaxAirPressure)
        {
            r_LicensePlate = i_LicensePlate;
            r_Wheels = new List<Wheel>(i_NumOfWheels);
            for(int i = 0; i < i_NumOfWheels; i++)
            {
                r_Wheels.Add(new Wheel(i_MaxAirPressure));
            }
        }

        public string LicensePlate
        {
            get
            {
                return r_LicensePlate;
            }
        }

        public string Brand
        {
            get
            {
                return m_Brand;
            }

            set
            {
                m_Brand = value;
            }
        }

        private float EnergyLevel
        {
            get
            {
                return m_EnergyLevel;
            }

            set
            {
                m_EnergyLevel = value;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return r_Wheels;
            }
        }

        public void PumpAirToWheelsFull()
        {
            foreach (Wheel wheel in r_Wheels)
            {
                wheel.PumpAir(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            }
        }

        public virtual void SetUpCar()
        {
            m_EnergyLevel = 1;
            PumpAirToWheelsFull();
        }

        public override string ToString()
        {
            StringBuilder vehicleInformation = new StringBuilder().AppendFormat(
@"License Number: {0}
Brand: {1}
Energy Level: {2}{3}",
r_LicensePlate,
m_Brand,
m_EnergyLevel,
Environment.NewLine);
            int wheelNumber = 1;

            vehicleInformation.AppendLine("Wheels:");
            foreach(Wheel wheel in r_Wheels)
            {
                vehicleInformation.AppendFormat("{0}. {1}{2}", wheelNumber, wheel.ToString(), Environment.NewLine);
                wheelNumber++;
            }

            return vehicleInformation.ToString();
        }

        public override int GetHashCode()
        {
            return r_LicensePlate.GetHashCode();
        }
    }
}
