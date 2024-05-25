using CarRentalApp.API.Models;

namespace CarRentalApp.API.Services.Abstract
{
    public interface ICarCrudService
    {
         Car AddCar(Car carObj);
         bool UpdateCar(int id,Car carObj);
        //public Car GetCarById(int carId);
         bool DeleteCar(int carId);
         List<Car> GetCars();    
         Car GetCar(int id);

    }
}
