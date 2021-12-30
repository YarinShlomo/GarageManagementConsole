using System.Text;
using System;

namespace Ex03.GarageLogic
{
    public class ElectricCar : ElectricVehicle
    {
        private const int k_NumOfWheels = 4;
        private const int k_MaxAirPressure = 29;
        private const float k_MaxAmountOfBattery = 2.6f;
        private readonly eNumOfDoors r_NumberOfDoors;
        private eCarColor m_CarColor;

        public ElectricCar(string i_LicensePlate, eNumOfDoors i_NumberOfDoors)
            : base(i_LicensePlate, k_MaxAmountOfBattery, k_NumOfWheels, k_MaxAirPressure)
        {
            if (Enum.IsDefined(typeof(eNumOfDoors), i_NumberOfDoors))
            {
                r_NumberOfDoors = i_NumberOfDoors;
            }
            else
            {
                throw new FormatException(string.Format("invalid input: {0}. Number of doors does not contain the given value.", i_NumberOfDoors));
            }
        }

        public eCarColor CarColor
        {
            get
            {
                return CarColor;
            }

            set
            {
                if(Enum.IsDefined(typeof(eCarColor), value))
                {
                    m_CarColor = value;
                }
                else
                {
                    throw new FormatException(string.Format("Invalid Input: {0}, is not a viable color", value));
                }
            }
        }

        public eNumOfDoors NumberOfDoors
        {
            get
            {
                return r_NumberOfDoors;
            }
        }

        public enum eCarColor
        {
            Red = 1,
            White,
            Black,
            Blue
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
@"Car Color: {0}
Number Of Doors: {1}",
m_CarColor.ToString(),
r_NumberOfDoors.ToString());

            return carInfo.ToString();
        }
    }
}
