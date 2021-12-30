namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaxAirPressure;
        private string m_Brand = null;
        private float m_CurrentAirPressure = 0;

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                if(m_CurrentAirPressure == 0)
                {
                    PumpAir(value);
                }
                else
                {
                    PumpAir(value - m_CurrentAirPressure);
                }
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
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
                if (m_Brand == null)
                {
                    m_Brand = value;
                }
            }
        }

        public void PumpAir(float i_AmountToFill)
        {
            if (m_CurrentAirPressure + i_AmountToFill > r_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(
                    r_MaxAirPressure,
                    0,
                    string.Format("invalid input. Tried to add {0} to wheel air-pressure but can only add more {1}", i_AmountToFill, r_MaxAirPressure - m_CurrentAirPressure));
            }

            m_CurrentAirPressure += i_AmountToFill;
        }

        public override string ToString()
        {
            string wheelInfo = string.Format("Brand: {0}; got {1} air pressure out of {2}", Brand, CurrentAirPressure, r_MaxAirPressure);
            
            return wheelInfo;
        }
    }
}
