using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class ElectricVehicle : Vehicle, IChargable
    {
        private readonly float r_MaxBatteryTime;
        private float m_BatteryTimeLeft = 0;

        public ElectricVehicle(string i_LicensePlate, float i_MaxBattery, int i_NumOfWheels, float i_MaxAirPressure)
            : base(i_LicensePlate, i_NumOfWheels, i_MaxAirPressure)
        {
            r_MaxBatteryTime = i_MaxBattery;
        }

        public float BatteryTimeLeft
        {
            get
            {
                return m_BatteryTimeLeft;
            }
        }

        float IChargable.BatteryTimeLeft()
        {
            return m_BatteryTimeLeft;
        }

        float IChargable.MaxBatteryTime()
        {
            return r_MaxBatteryTime;
        }

        void IChargable.ChargeBattery(float i_AmountToCharge)
        {
            if (i_AmountToCharge + m_BatteryTimeLeft > r_MaxBatteryTime || i_AmountToCharge < 0)
            {
                throw new ValueOutOfRangeException(r_MaxBatteryTime, 0, string.Format("Error: Hours to charge value must be between 0-{0}", r_MaxBatteryTime - m_BatteryTimeLeft));
            }

            m_BatteryTimeLeft += i_AmountToCharge;
            m_EnergyLevel = r_MaxBatteryTime / m_BatteryTimeLeft;
        }

        public void ChargeBatteryToFull()
        {
            m_BatteryTimeLeft = r_MaxBatteryTime;
        }

        public override void SetUpCar()
        {
            base.SetUpCar();
            ChargeBatteryToFull();
        }

        public override string ToString()
        {
            StringBuilder vehicleInfo = new StringBuilder().Append(base.ToString());

            vehicleInfo.AppendFormat(
@"Max Battery Time: {0}
Battery Left: {1}",
r_MaxBatteryTime,
m_BatteryTimeLeft);

            return vehicleInfo.ToString();
        }
    }
}
