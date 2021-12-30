using System.Text;
using System;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : ElectricVehicle
    {
        private const int k_NumOfWheels = 2;
        private const int k_MaxAirPressure = 30;
        private const float k_MaxAmountOfBattery = 2.3f;
        private readonly eLicenseType r_LicenseType;
        private readonly int r_EngineVolume;

        public ElectricMotorcycle(string i_LicensePlate, eLicenseType i_LicenseType, int i_EngineVolume)
            : base(i_LicensePlate, k_MaxAmountOfBattery, k_NumOfWheels, k_MaxAirPressure)
        {
            r_EngineVolume = i_EngineVolume;
            if(Enum.IsDefined(typeof(eLicenseType), i_LicenseType))
            {
                r_LicenseType = i_LicenseType;
            }
            else
            {
                throw new FormatException(string.Format("Invalid Input: {0}, is not a valid licenseType", i_LicenseType));
            }
        }

        public eLicenseType LicenseType
        {
            get
            {
                return r_LicenseType;
            }
        }

        public int EngineVolume
        {
            get
            {
                return r_EngineVolume;
            }
        }

        public enum eLicenseType
        {
            A = 1,
            A2,
            AA,
            B
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
