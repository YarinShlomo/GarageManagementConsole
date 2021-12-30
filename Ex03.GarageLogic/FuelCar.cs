using System.Text;
using System;

namespace Ex03.GarageLogic
{
    public class FuelCar : FuelVehicle
    {
        private const int k_NumOfWheels = 4;
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private const int k_MaxAirPressure = 29;
        private const float k_MaxAmountOfFuel = 48;
        private readonly eNumOfDoors r_NumOfDoors;
        private eCarColor m_CarColor;

        public FuelCar(string i_LicensePlate, eNumOfDoors i_NumOfDoors)
        : base(i_LicensePlate, k_FuelType, k_MaxAmountOfFuel, k_NumOfWheels, k_MaxAirPressure)
        {
            if(Enum.IsDefined(typeof(eNumOfDoors), i_NumOfDoors))
            {
                r_NumOfDoors = i_NumOfDoors;
            }
            else
            {
                throw new FormatException(string.Format("invalid input: {0}. Number of doors does not contain the given value.", i_NumOfDoors));
            }
        }

        public enum eCarColor
        {
            Red = 1,
            White,
            Black,
            Blue
        }

        public eCarColor CarColor
        {
            get
            {
                return m_CarColor;
            }

            set
            {
                if (Enum.IsDefined(typeof(eCarColor), value))
                {
                    m_CarColor = value;
                }
                else
                {
                    throw new FormatException(string.Format("Invalid Input: {0}, is not a valid color", value));
                }
            }
        }

        public enum eNumOfDoors
        {
            Two = 2,
            Three,
            Four,
            Five
        }

        public override string ToString()
        {
            StringBuilder carInfo = new StringBuilder().AppendLine(base.ToString());

            carInfo.AppendFormat(
@"Number of Doors: {0}
Car Color: {1}",
r_NumOfDoors.ToString(),
m_CarColor.ToString());

            return carInfo.ToString();
        }
    }
}
