using System.Text;
using System;

namespace Ex03.GarageLogic
{
    public class FuelTruck : FuelVehicle
    {
        private const int k_NumOfWheels = 16;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private const int k_MaxAirPressure = 25;
        private const float k_MaxAmountOfFuel = 130;
        private readonly float r_StorageSpace;
        private bool m_IsCooled;

        public FuelTruck(string i_LicensePlate, float i_StorageSpace)
            : base(i_LicensePlate, k_FuelType, k_MaxAmountOfFuel, k_NumOfWheels, k_MaxAirPressure)
        {
            if(i_StorageSpace < 0)
            {
                throw new ArgumentException("Truck storage cannot be negative number");
            }

            r_StorageSpace = i_StorageSpace;
        }

        public bool IsCooled
        {
            get
            {
                return m_IsCooled;
            }

            set
            {
                m_IsCooled = value;
            }
        }

        public float StorageSpace
        {
            get
            {
                return r_StorageSpace;
            }
        }

        public override string ToString()
        {
            StringBuilder truckInfo = new StringBuilder().AppendLine(base.ToString());

            truckInfo.AppendFormat(
@"IsCooled?: {0}
Storage space: {1}",
m_IsCooled ? "Yes" : "No",
r_StorageSpace);
            return truckInfo.ToString();
        }
    }
}
