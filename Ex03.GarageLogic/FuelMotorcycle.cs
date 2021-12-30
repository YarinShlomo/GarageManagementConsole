using System.Text;
using System;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : FuelVehicle
    {
        private const int k_NumOfWheels = 2;
        private const eFuelType k_FuelType = eFuelType.Octan98;
        private const int k_MaxAirPressure = 30;
        private const float k_MaxAmountOfFuel = 5.8f;
        private readonly eLicenseType r_LicenseType;
        private readonly int r_EngineVolume;

        public FuelMotorcycle(string i_LicensePlate, eLicenseType i_LicenseType, int i_EngineVol)
            : base(i_LicensePlate, k_FuelType, k_MaxAmountOfFuel, k_NumOfWheels, k_MaxAirPressure)
        {
            if (Enum.IsDefined(typeof(eLicenseType), i_LicenseType))
            {
                r_LicenseType = i_LicenseType;
            }
            else
            {
                throw new FormatException(string.Format("Invalid Input: {0}, is not a valid license Type", i_LicenseType));
            }

            r_EngineVolume = i_EngineVol;
        }

        public enum eLicenseType
        {
            A = 1,
            A2,
            AA,
            B
        }

        public int EngineVolume
        {
            get
            {
                return r_EngineVolume;
            }
        }

        public eLicenseType LicenseType
        {
            get
            {
                return r_LicenseType;
            }
        }

        public override string ToString()
        {
            StringBuilder motorcycleInfo = new StringBuilder().AppendLine(base.ToString());

            motorcycleInfo.AppendFormat(
@"License type: {0}
Engine Vol. : {1}",
r_LicenseType,
r_EngineVolume);

            return motorcycleInfo.ToString();
        }
    }
}
