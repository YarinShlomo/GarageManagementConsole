using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public static class ConsoleUI
    {
        private static readonly VehicleFactory sr_vehicleFactory = new VehicleFactory();
        private static readonly Garage sr_myGarage = new Garage();

        public static void GarageProgram()
        {
            bool isRunning = true;
            int userChoice;

            while(isRunning)
            {
                Console.Clear();
                try
                {
                    userChoice = PrintMainMenuAndGetUserChoice();
                    ExecuteUserChoice(userChoice, ref isRunning);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();
                }
            }
        }

        public static int PrintMainMenuAndGetUserChoice()
        {
            int userChoice = -1;
            bool isValidChoice = false;
            string mainMenu = string.Format(
@"-------------------------------------------------
--------------Garage Menu------------------------
-------------------------------------------------
Please Choose what whould you like to do (Enter the number):
1. Enter a new vehicle to the garage(if already in garage will be changed to 'inFix').
2. Show vehicles in garage lisence plates.
3. Change vehicle garage status.
4. Inflate vehicle wheels to maximum pressure.
5. Refuel vehicle.
6. Recharge vehicle.
7. Show vehicle's data.
8. Exit.");

            Console.WriteLine(mainMenu);
            while(!isValidChoice)
            {
                Console.Write("=> ");
                isValidChoice = int.TryParse(Console.ReadLine(), out userChoice);
                if(isValidChoice)
                {
                    if(userChoice < 1 || userChoice > 8)
                    {
                        isValidChoice = false;
                        Console.WriteLine("Invalid Input: pick out of bounds. Please pick a number between 1-8. Try again:");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input: Pick was not a number. Please enter a number between 1 to 8. Try again:");
                }
            }

            return userChoice;
        }

        public static void ExecuteUserChoice(int i_UserChoice, ref bool io_IsRunning)
        {
            Console.Clear();

            switch (i_UserChoice)
            {
                case 1:
                    Console.WriteLine("======================");
                    Console.WriteLine("=====New Car Menu=====");
                    Console.WriteLine("======================");
                    enterVehicle();
                    break;

                case 2:
                    Console.WriteLine("=============================");
                    Console.WriteLine("=====Garage Vehicle View=====");
                    Console.WriteLine("=============================");
                    showVehicleInGarageByStatus();
                    break;

                case 3:
                    Console.WriteLine("===============================");
                    Console.WriteLine("=====Change Vehicle Status=====");
                    Console.WriteLine("===============================");
                    changeVehicleStatus();
                    break;

                case 4:
                    Console.WriteLine("================================");
                    Console.WriteLine("=====Inflate Vehicle Wheels=====");
                    Console.WriteLine("================================");
                    inflateVehicleWheels();
                    break;

                case 5:
                    Console.WriteLine("========================");
                    Console.WriteLine("=====Refuel Vehicle=====");
                    Console.WriteLine("========================");
                    reFuelVehicle();
                    break;

                case 6:
                    Console.WriteLine("==================================");
                    Console.WriteLine("=====Recharge Vehicle Battery=====");
                    Console.WriteLine("==================================");
                    reChargeVehicle();
                    break;

                case 7:
                    Console.WriteLine("========================");
                    Console.WriteLine("=====Vehicle's Data=====");
                    Console.WriteLine("========================");
                    showVehicleData();
                    break;

                case 8:
                    io_IsRunning = false;
                    break;

                default:
                    throw new ValueOutOfRangeException(8, 0, "Given action number is out of range");
            }

            ThankYouMsg();
        }

        private static void enterVehicle()
        {
            string vehicleLisencePlate;
            GarageItem garageItem;
            bool invalidInput = true;
            Vehicle vehicleToEnter = null;
            VehicleFactory.eVehicle typeChosen;
            List<string> vehicleParams = new List<string>();
            string ownerName;
            string ownerPhone;

            vehicleLisencePlate = getLisencePlate();

            bool isCarExists = sr_myGarage.IsVehicleInGarage(vehicleLisencePlate);

            if (!isCarExists)
            {
                ownerName = getOwnerName();
                ownerPhone = getOwnerPhone();
                typeChosen = getWantedVehicleType();
                vehicleParams.Add(vehicleLisencePlate);

                while (invalidInput)
                {
                    try
                    {
                        vehicleParams.AddRange(getWantedVehicleMustHaveParams(typeChosen));
                        vehicleToEnter = sr_vehicleFactory.CreateVehicle(typeChosen, vehicleParams);
                        invalidInput = false;
                    }
                    catch (Exception exeption)
                    {
                        Console.WriteLine(exeption.Message);
                        vehicleParams.Clear();
                        vehicleParams.Add(vehicleLisencePlate);
                    }
                }

                garageItem = new GarageItem(ownerName, ownerPhone, vehicleToEnter);
                sr_myGarage.AddVehicleToGarage(garageItem);
                updateVehicleProperties(vehicleLisencePlate, typeChosen);
            }
            else
            {
                Console.WriteLine("The Wanted Vehicle is already in the Garage.");
                sr_myGarage.GetGarageItem(vehicleLisencePlate).UpdateVehicleStatus(GarageItem.eGarageStatus.InFix);
            }
        }

        private static void setAllWheelsBrand(List<Wheel> i_Wheels)
        {
            Console.WriteLine("----------------");
            Console.WriteLine("What is the wheels brand?: ");
            Console.Write("=> ");
            string userInput = Console.ReadLine();
            Console.WriteLine("----------------");

            foreach (Wheel wheel in i_Wheels)
            {
                wheel.Brand = userInput;
            }
        }

        private static string getVehicleBrand()
        {
            Console.WriteLine("----------------");
            Console.WriteLine("What is the vehicle brand?:");
            Console.Write("=> ");
            return Console.ReadLine();
        }

        private static VehicleFactory.eVehicle getWantedVehicleType()
        {
            int index = 1;

            Console.WriteLine(string.Format("Choose vehicle type by number:{0}==============================", Environment.NewLine));
            foreach (string currentType in Enum.GetNames(typeof(VehicleFactory.eVehicle)))
            {
                Console.WriteLine(string.Format("{0}.{1}", index, currentType.ToString()));
                index++;
            }

            Console.Write("=> ");

            string typeChosenNumber = Console.ReadLine();

            VehicleFactory.eVehicle vehicleType;
            bool isValid = Enum.TryParse<VehicleFactory.eVehicle>(typeChosenNumber, out vehicleType);

            while (!isValid || !Enum.IsDefined(typeof(VehicleFactory.eVehicle), vehicleType))
            {
                Console.WriteLine("Invalid choice. please try again.");
                Console.Write("=> ");
                typeChosenNumber = Console.ReadLine();
                isValid = Enum.TryParse<VehicleFactory.eVehicle>(typeChosenNumber, out vehicleType);
            }
        
            return vehicleType;
        }

        private static List<string> getWantedVehicleMustHaveParams(VehicleFactory.eVehicle i_VehicleType)
        {
            List<string> vehicleArguments = new List<string>();
            List<string> vehicleParams = new List<string>();

            vehicleArguments = sr_vehicleFactory.ArgumentsNeedForCreation(i_VehicleType);
            foreach (string toAdd in vehicleArguments)
            {
                Console.WriteLine("----------------");
                Console.WriteLine("Choose the wanted option: ");
                Console.WriteLine(toAdd);
                Console.Write("=> ");
                vehicleParams.Add(Console.ReadLine());
            }

            return vehicleParams;
        }

        private static void updateVehicleProperties(string i_LisencePlate, VehicleFactory.eVehicle i_VehicleType)
        {
            GarageItem garageItem = sr_myGarage.GetGarageItem(i_LisencePlate);
            Vehicle vehicleToUpdate = garageItem.Vehicle;
            float amountOfWheelsAirPressure;
            bool isValid = false;

            vehicleToUpdate.Brand = getVehicleBrand();
            setAllWheelsBrand(vehicleToUpdate.Wheels);
            while (!isValid)
            {
                Console.WriteLine("What is the amount of wheels air pressure?: ");
                amountOfWheelsAirPressure = getAmountToAdd();
                try
                {
                    sr_myGarage.PumpAirToWheelsByAmount(i_LisencePlate, amountOfWheelsAirPressure);
                    isValid = true;
                }
                catch (Exception exeption)
                {
                    Console.WriteLine(exeption.Message);
                    Console.WriteLine("Try again.");
                }
            }

            switch(i_VehicleType)
            {
                case VehicleFactory.eVehicle.ElectricCar:
                    (vehicleToUpdate as ElectricCar).CarColor = (ElectricCar.eCarColor)Enum.Parse(typeof(ElectricCar.eCarColor), getCarColor());
                    break;

                case VehicleFactory.eVehicle.FuelCar:
                    (vehicleToUpdate as FuelCar).CarColor = (FuelCar.eCarColor)Enum.Parse(typeof(FuelCar.eCarColor), getCarColor());
                    break;

                case VehicleFactory.eVehicle.FuelTruck:
                    (vehicleToUpdate as FuelTruck).IsCooled = getIsTruckCooling();
                    break;
            }
        }

        private static string getOwnerName()
        {
            Console.WriteLine("----------------");
            Console.WriteLine("Enter Owner's name: ");
            Console.Write("=> ");

            return Console.ReadLine();
        }

        private static string getOwnerPhone()
        {
            Console.WriteLine("----------------");
            Console.WriteLine("Enter Owner's Phone: ");
            Console.Write("=> ");

            return Console.ReadLine();
        }

        private static bool getIsTruckCooling()
        {
            bool isCooling;

            Console.WriteLine("Is the truck cooling? ");
            isCooling = getYNUserInput();

            return isCooling;
        }

        private static string getCarColor()
        {
            int index = 1;

            Console.WriteLine("Please Pick Car Color by number");
            foreach (string color in sr_myGarage.GetCarColors())
            {
                Console.WriteLine("{0}- {1}", index, color);
                index++;
            }

            Console.Write("=> ");
            string colorChose = Console.ReadLine();

            FuelCar.eCarColor vehicleColor;
            bool isValid = Enum.TryParse(colorChose, out vehicleColor);

            while (!isValid || !Enum.IsDefined(typeof(FuelCar.eCarColor), vehicleColor))
            {
                Console.WriteLine("Invalid choice. please try again.");
                Console.Write("=> ");
                colorChose = Console.ReadLine();
                isValid = Enum.TryParse(colorChose, out vehicleColor);
            }

            return colorChose;
        }

        private static void showVehicleInGarageByStatus()
        {
            List<string> lisencePlates;
            GarageItem.eGarageStatus statusChosen;

            Console.Clear();
            Console.Write("Do you want to filter by vehicle status? ");
            if(getYNUserInput())
            {
                statusChosen = getUserGarageStatus();
                lisencePlates = sr_myGarage.CreateListByVehicleStatus(statusChosen);
            }
            else
            {
                lisencePlates = sr_myGarage.CreateListByVehicleStatus(null);
            }

            Console.WriteLine("=======================");

            if (lisencePlates.Count == 0)
            {
                Console.WriteLine("There are no vehicles in the wanted status inside the garage.");
            }
            else
            {
                int index = 1;

                foreach (string lisencePlate in lisencePlates)
                {
                    Console.WriteLine(index + ". " + lisencePlate);
                    index++;
                }
            }
        }

        private static bool getYNUserInput()
        {
            Console.WriteLine("Y/N: ");
            Console.Write("=> ");
            string userInput = Console.ReadLine().ToUpper();

            while(userInput != "Y" && userInput != "N")
            {
                Console.WriteLine("Invalid input please try again. Choose Y or N");
                Console.Write("=> ");
                userInput = Console.ReadLine().ToUpper();
            }

            return userInput == "Y" ? true : false;
        }

        private static GarageItem.eGarageStatus getUserGarageStatus()
        {            
            Console.WriteLine("Please Pick wanted garage status by number:");
            int index = 1;

            foreach (string status in Enum.GetNames(typeof(GarageItem.eGarageStatus)))
            {
                Console.WriteLine("{0}- {1}", index, status);
                index++;
            }
            
            Console.Write("=> ");

            string userChoice = Console.ReadLine();
            GarageItem.eGarageStatus garageStatus;
            bool isValid = Enum.TryParse(userChoice, out garageStatus);

            while (!isValid || !Enum.IsDefined(typeof(GarageItem.eGarageStatus), garageStatus))
            {
                Console.WriteLine("Invalid choice. please try again.");
                Console.Write("=> ");
                userChoice = Console.ReadLine();
                isValid = Enum.TryParse(userChoice, out garageStatus);
            }

            return garageStatus;
        }

        private static void changeVehicleStatus()
        {
            string vehicleLisencePlate;
            GarageItem.eGarageStatus newGarageStatus;

            vehicleLisencePlate = getLisencePlate();
            newGarageStatus = getUserGarageStatus();

            try
            {
                sr_myGarage.ChangeVehicleGarageStatus(vehicleLisencePlate, newGarageStatus);
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static void inflateVehicleWheels()
        {
            string vehicleLisencePlate = getLisencePlate();

            try
            {
                sr_myGarage.InflateAllWheels(vehicleLisencePlate);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static FuelVehicle.eFuelType GetEFuelType()
        {
            int index = 1;

            Console.WriteLine("The available fuel types are:");
            foreach (string eFuelType in sr_myGarage.GetFuelTypes())
            {
                Console.WriteLine("{0}. {1}", index, eFuelType);
                index++;
            }

            Console.WriteLine("which one would you like to add(pick the number)");
            Console.Write("=> ");

            string userFuelTypeChose = Console.ReadLine();
            FuelVehicle.eFuelType fuelType;
            bool isValid = Enum.TryParse(userFuelTypeChose, out fuelType);

            while(!isValid || !Enum.IsDefined(typeof(FuelVehicle.eFuelType), fuelType))
            {
                Console.WriteLine("Invalid choice. please try again.");
                Console.Write("=> ");
                userFuelTypeChose = Console.ReadLine();
                isValid = Enum.TryParse(userFuelTypeChose, out fuelType);
            }

            return fuelType;
        }

        private static string getLisencePlate()
        {
            Console.WriteLine("Please Enter the vehicle's License plate number: ");
            Console.Write("=> ");
            string lisencePlate = Console.ReadLine();

            lisencePlate = lisencePlate.Replace(" ", string.Empty);
            while(string.IsNullOrEmpty(lisencePlate))
            {
                Console.WriteLine("Invalid input, Please try again:");
                Console.Write("=> ");
                lisencePlate = Console.ReadLine();
                lisencePlate = lisencePlate.Replace(" ", string.Empty);
            }

            return lisencePlate;
        }

        private static void reFuelVehicle()
        {
            FuelVehicle.eFuelType fuelType;
            float amountOfFuelToAdd;
            string vehicleLisencePlate = getLisencePlate();

            if (sr_myGarage.IsVehicleInGarage(vehicleLisencePlate))
            {
                if (sr_myGarage.GetGarageItem(vehicleLisencePlate).Vehicle is FuelVehicle)
                {
                    fuelType = GetEFuelType();
                    Console.WriteLine("What is the amount of Fuel you would like to add?: ");
                    amountOfFuelToAdd = getAmountToAdd();
                    try
                    {
                        sr_myGarage.RefuelVehicle(vehicleLisencePlate, fuelType, amountOfFuelToAdd);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Cannot fuel an None-Fuel vehicle.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Lisence number was entered: <{0}>", vehicleLisencePlate);
            }
        }

        private static float getAmountToAdd()
        {
            string userInput;

            Console.Write("=> ");
            userInput = Console.ReadLine();
            Console.WriteLine("----------------");

            float amountToAdd = -1;
            bool isvalid = false;

            while (!isvalid)
            {
                isvalid = float.TryParse(userInput, out amountToAdd);
                if (!isvalid)
                {
                    Console.WriteLine("Did not enter a number. please try again: ");
                    Console.Write("=> ");
                    userInput = Console.ReadLine();
                }
            }

            return amountToAdd;
        }
    
        private static void reChargeVehicle()
        {
            string vehicleLisencePlate = getLisencePlate();

            if (sr_myGarage.IsVehicleInGarage(vehicleLisencePlate))
            {
                if (sr_myGarage.GetGarageItem(vehicleLisencePlate).Vehicle is ElectricVehicle)
                {
                    Console.WriteLine("What is the amount of Charge time like to add?: ");
                    float amountOfChargeToAdd = getAmountToAdd();

                    try
                    {
                        sr_myGarage.ReChargeVehicle(vehicleLisencePlate, amountOfChargeToAdd);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Cannot recharge a Non-electric type car.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Lisence number was entered: <{0}>", vehicleLisencePlate);
            }
        }

        private static void showVehicleData()
        {
            string vehicleLisencePlate = getLisencePlate();

            if(sr_myGarage.IsVehicleInGarage(vehicleLisencePlate))
            {
                string vehicleData = sr_myGarage.GetVehicleData(vehicleLisencePlate);

                Console.WriteLine(vehicleData);
            }
            else
            {
                Console.WriteLine("Invalid Lisence number was entered: <{0}>", vehicleLisencePlate);
            }
        }

        private static void ThankYouMsg()
        {
            Console.WriteLine("=======================");
            Console.WriteLine("Task done succesfuly, Thank you!");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
