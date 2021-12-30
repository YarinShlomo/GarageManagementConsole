using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue = 0;
        private readonly float r_MinValue = 0;

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue, string i_ErrorMessage) : base(i_ErrorMessage)
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
        }

        public float MaxValue
        {
            get
            {
                return r_MaxValue;
            }
        }

        public float MinValue
        {
            get
            {
                return r_MinValue;
            }
        }

        public override string ToString()
        {
            return string.Format("The requested value can only be between {0} and {1}", r_MinValue, r_MaxValue);
        }
    }
}
