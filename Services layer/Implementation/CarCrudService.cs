using CarRentalApp.API.DAO.Abstract;
using CarRentalApp.API.Data;
using CarRentalApp.API.Models;
using CarRentalApp.API.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.API.Services.Implementation
{
    public class CarCrudService : ICarCrudService
    {
        private readonly ApplicationDbContext _db;
        private readonly ICarCrudRepository _carCrudRepository;
        public CarCrudService(ApplicationDbContext db,ICarCrudRepository carCrudRepository)
        {
            _db = db;
            _carCrudRepository = carCrudRepository;
        }
        
        public List<Car> GetCars()
        {
            //call the repository method
            List<Car> cars = _carCrudRepository.GetCars();
            return cars;
        }
        public Car AddCar(Car car)
        {
            //call the repository method
            return _carCrudRepository.AddCar(car);
        }

        public bool DeleteCar(int carId)
        {
            if (carId == 0 || carId == null)
            {
                return false;
            }
            //call the repository method
            return _carCrudRepository.DeleteCar(carId);

        }

        public bool UpdateCar(int id,Car car)
        {
            //if the id of car to be deleted and the updated carObj id, are not same, return badrequest
            if(id!=car.Id || id==0 || id == null)
            {
                return false;
            }
            //call the repository method
            return _carCrudRepository.UpdateCar(id,car);

        }

        public Car GetCar(int id)
        {
            //call the repository method
            return _carCrudRepository.GetCar(id);
        }
       
    }
}
