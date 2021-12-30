namespace Ex03.GarageLogic
{
    public interface IFuelable
    {
        void AddFuel(float i_AmountToAdd, FuelVehicle.eFuelType i_FuelType);

        float CurrentGasTank();

        float MaxFuelTank();
    }
}
