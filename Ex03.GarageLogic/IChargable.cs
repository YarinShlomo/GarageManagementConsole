namespace Ex03.GarageLogic
{
    public interface IChargable
    {
        void ChargeBattery(float i_AmountToCharge);

        float BatteryTimeLeft();

        float MaxBatteryTime();
    }
}
