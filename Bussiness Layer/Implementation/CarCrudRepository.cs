using CarRentalApp.API.DAO.Abstract;
using CarRentalApp.API.Data;
using CarRentalApp.API.Models;

namespace CarRentalApp.API.DAO.Implementation
{
    public class CarCrudRepository:ICarCrudRepository
    {
        private readonly ApplicationDbContext _db;
        public CarCrudRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public Car AddCar(Car carObj)
        {
            _db.Cars.Add(carObj);
            _db.SaveChanges();
            return carObj;
        }

        public bool UpdateCar(int id, Car carObj)
        {
            _db.Cars.Update(carObj);
            _db.SaveChanges();
            return true;
        }
        public bool DeleteCar(int carId)
        {
            //fetch the car obj based on id
            Car car = _db.Cars.FirstOrDefault(obj => obj.Id == carId);
            if (car != null)
            {
                _db.Cars.Remove(car);
                _db.SaveChanges();
                return true;
            }
            //if car==null return badrequest
            return false;
        }

        public Car GetCar(int id)
        {
            //fetch the car based on id
            Car car = _db.Cars.FirstOrDefault(obj => obj.Id == id);
            if (car != null)
            {
                return car;
            }
            return car;
        }

        public List<Car> GetCars()
        {
            //fetch all the cars
            return _db.Cars.ToList();
        }
    }
}
