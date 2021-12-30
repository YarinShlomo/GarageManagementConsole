using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageItem
    {
        private readonly Vehicle r_Vehicle = null;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eGarageStatus m_CurrentStatus;
        
        public GarageItem(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            r_Vehicle = i_Vehicle;
            m_CurrentStatus = eGarageStatus.InFix;
        }

        public enum eGarageStatus
        {
            InFix = 1,
            Fixed,
            Paid,
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }

            set
            {
                m_OwnerName = value;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return m_OwnerPhoneNumber;
            }

            set
            {
                this.m_OwnerPhoneNumber = value;
            }
        }

        public eGarageStatus CurrentStatus
        {
            get
            {
                return m_CurrentStatus;
            }

            set
            {
                this.m_CurrentStatus = value;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return r_Vehicle;
            }
        }

        public void UpdateVehicleStatus(eGarageStatus i_NewStatus)
        {
            m_CurrentStatus = i_NewStatus;
        }

        public override string ToString()
        {
            StringBuilder GarageItemInformation = new StringBuilder().AppendFormat(
@"________________The_Requested_Vehicle________________

Vehicle Owner Name: {0}  
Owner Phone Number: {1}
Vehicle Status: {2}
",
m_OwnerName,
m_OwnerPhoneNumber,
m_CurrentStatus);

            GarageItemInformation.AppendLine(Vehicle.ToString());
            GarageItemInformation.AppendLine("---------------------");

            return GarageItemInformation.ToString();
        }
    }
}
