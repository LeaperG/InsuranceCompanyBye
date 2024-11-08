using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCompany.HellperClass
{
    internal class TempFile
    {
        public static Driver DriverSelect;
        public static bool DriverShow;
        public static DB.UserAccount user;
        public static DB.Clients client;
        public static DB.Agents agent;
        public static DB.Agents SelectAgent;
        public static DB.Clients SelectClient;
        public static DB.Vehicles SelectCar;
        public static DB.VehicleClientView SelectCars;
        public static DB.Policies SelectPolicy;
        public static DB.Claims SelectClaim;
        public static DB.Address SelectClientAddress;
        public static string InsuranseCost;
        public static int PageCTPVehicleDataBack;
        public static bool Auth;
        public static int PageCTPCalcBack;
        public static int PageDriversBack;
        public static int PageOwnerBack;
        public static bool BoolAddCar;
        public static bool BoolAddClaim;
        public static bool PageDriversChangeDriver;
        public static bool NewUser;
        public static bool Reg;
        public static bool Personal;
        public static bool carPoliciesContinue;
        public static bool claimPoliciesContinue;
        public static bool Skip;



        public static void Reset()
        {
            DriverSelect = null;
            DriverShow = false;
            user = null;
            client = null;
            agent = null;
            SelectAgent = null;
            SelectClient = null;
            SelectCar = null;
            SelectCars = null;
            SelectPolicy = null;
            SelectClaim = null;
            SelectClientAddress = null;
            InsuranseCost = null;
            PageCTPVehicleDataBack = 0;
            Auth = false;
            PageCTPCalcBack = 0;
            PageDriversBack = 0;
            PageOwnerBack = 0;
            BoolAddCar = false;
            BoolAddClaim = false;
            PageDriversChangeDriver = false;
            NewUser = false;
            Reg = false;
            Personal = false;
            Skip = false;
            carPoliciesContinue = false;
        }

    }


    internal class TempFileVehicleData
    { 
         //Стараница PageCTPVehicleData
         public static string RegNumber;
         public static string Country;
         public static string VIN;
         public static int VehicleCategory;
         public static int VehicleType;
         public static int TypeVehicleDocument;
         public static int CMBUsagePurpose;
         public static string UsagePurposeText;
         public static string Brand;
         public static int CMBBrand;
         public static string Model;
         public static int CMBModel;
         public static string Generation;
         public static int CMBGeneration;
         public static double Power;
         public static int Year;


        public static void Reset()
        {
            RegNumber = null;
            Country = null;
            VIN = null;
            VehicleCategory = 0;
            VehicleType = 0;
            TypeVehicleDocument = 0;
            CMBUsagePurpose = 0;
            UsagePurposeText = null;
            Brand = null;
            CMBBrand = 0;
            Model = null;
            CMBModel = 0;
            Generation = null;
            CMBGeneration = 0;
            Power = 0;
            Year = 0;
        }

    }


    internal class TempFileCalc
    {
        //Стараница PageCTPCalc
        public static DateTime DatePickerStart;
        public static int Duration;
        public static string DateEnd;
        public static int PeriodOfUse;
        public static string PeriodOfUseText;

        public static int TypeVehicle;
        public static string TypeVehicleText;
        public static DateTime DatePickerDriverLicense;
        public static string PassportSeries;
        public static string PassportNumber;



        public static void Reset()
        {
            DatePickerStart = DateTime.MinValue;
            Duration = 0;
            DateEnd = null;
            PeriodOfUse = 0;
            PeriodOfUseText = null;

            TypeVehicle = 0;
            TypeVehicleText = null;
            DatePickerDriverLicense = DateTime.MinValue;
            PassportSeries = null;
            PassportNumber = null;
        }


    }



    // Класс для представления водителя
    public class Driver
    {
        public int IdDriver { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string DriverLicenseSeries { get; set; }
        public string DriverLicenseNumber { get; set; }
        public DateTime DataDriverLicense { get; set; }
        public int IdGender { get; set; }

        public int Number { get; set; }
    }

    // Класс для управления коллекцией водителей
    public class DriverManager
    {
        public static List<Driver> Drivers { get; set; }

        public static void Reset()
        {
            if (Drivers != null)
            {
                Drivers.Clear(); // Очищает список, если он уже существует
            }
        }
            public void AddDriver(Driver driver)
        {
            Drivers.Add(driver);
        }

        public void RemoveDriver(Driver driver)
        {
            Drivers.Remove(driver);
        }
    }

    internal class TempFileInsurant
    {
        //Страница PageInsurant
        public static string Email;
        public static string Phone;
        public static string FirstName;
        public static string LastName;
        public static string Patronymic;
        public static int Gender;
        public static DateTime DatePickerBirthDate;
        public static string PassportSeries;
        public static string PassportNumber;
        public static string Region;
        public static string City;
        public static string Street;
        public static string House;
        public static string Apartment;

        public static void Reset()
        {
            Email = null;
            Phone = null;
            FirstName = null;
            LastName = null;
            Patronymic = null;
            Gender = 0;
            DatePickerBirthDate = DateTime.MinValue;
            PassportSeries = null;
            PassportNumber = null;
            Region = null;
            City = null;
            Street = null;
            House = null;
            Apartment = null;
        }
    }

    internal class TempFileOwner
    {
        public static string FirstName;
        public static string LastName;
        public static string Patronymic;
        public static int Gender;
        public static DateTime DatePickerBirthDate;
        public static string PassportSeries;
        public static string PassportNumber;

        public static string Region;
        public static string City;
        public static string Street;
        public static string House;
        public static string Apartment;

        public static void Reset()
        {
            FirstName = null;
            LastName = null;
            Patronymic = null;
            Gender = 0;
            DatePickerBirthDate = DateTime.MinValue;
            PassportSeries = null;
            PassportNumber = null;

            Region = null;
            City = null;
            Street = null;
            House = null;
            Apartment = null;
        }

    }
}
