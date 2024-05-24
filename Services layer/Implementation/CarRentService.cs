using CarRentalApp.API.DAO.Abstract;
using CarRentalApp.API.DAO.Implementation;
using CarRentalApp.API.Data;
using CarRentalApp.API.Models;
using CarRentalApp.API.Services.Abstract;

namespace CarRentalApp.API.Services.Implementation
{
    public class CarRentService:ICarRentService
    {
        private readonly ApplicationDbContext _db;
        private readonly ICarRentRepository _carRentRepository;
        private readonly ICarCrudRepository _carCrudRepository;
        public CarRentService(ApplicationDbContext db, ICarRentRepository carRentRepository,ICarCrudRepository carCrudRepository)
        {
            _db = db;
            _carRentRepository = carRentRepository;
            _carCrudRepository = carCrudRepository;
        }
        public bool RentCar(RentalAgreement rentalAgreement)
        {
            //call the respository method
            _carRentRepository.RentCar(rentalAgreement);

            int rentalAgreementId=rentalAgreement.Id;

           //make the car unavailable
            int rentedCarId = rentalAgreement.CarId;
            Car car = _carCrudRepository.GetCar(rentedCarId);
            car.IsAvailable = "false";

            //add a new record in the user agreement table
            var obj = new UserRentalAgreement
            {
                UserId=rentalAgreement.UserId,
                RentalAgreementId=rentalAgreementId,
            };
            _db.UserRentalAgreements.Add(obj);
            _db.SaveChanges();

            return true; 

        }

        public List<RentalAgreement> GetUserRentalAgreementList(string userId)
        {
            //call the repository method
            List<RentalAgreement> rentalAgreements = _carRentRepository.GetUserRentalAgreementList(userId);  /*_db.RentalAgreements.Where(obj => obj.UserId == userId).ToList();*/
            return rentalAgreements;
        }

        public bool ReturnRequest(int agreementId)
        {
            //call the repo method
            return _carRentRepository.ReturnRequest(agreementId);

        }

        //for admin
        public List<RentalAgreement> GetRentalAgreements()
        {
            //call the repo method
            List<RentalAgreement> rentalAgreements = _carRentRepository.GetRentalAgreements(); /*_db.RentalAgreements.ToList();*/
            return rentalAgreements;
        }

        public bool DeleteRentalAgreement(int id)
        {
            //some checks
            if(id==0 || id == null)
            {
                return false;
            }
            //call the repo method
            return _carRentRepository.DeleteRentalAgreement(id);
        }

        //for admin
        public bool UpdateRentalAgreement(int rentalAgreementId, RentalAgreement rentalAgreement)
        {
            //some checks
            if (rentalAgreementId != rentalAgreement.Id || rentalAgreementId == 0 || rentalAgreementId == null)
            {
                return false;
            }
            //call repo method
            return _carRentRepository.UpdateRentalAgreement(rentalAgreementId, rentalAgreement);
        }


        public RentalAgreement GetRentalAgreementById(int id)
        {
            RentalAgreement rentalAgreement = _carRentRepository.GetRentalAgreementById(id);/*_db.RentalAgreements.FirstOrDefault(obj=>obj.Id == id);*/
            return rentalAgreement;
        }

        public bool MarkForInspection(CarInspection carInspectionObj)
        {
            
            //call the repo method
            bool res= _carRentRepository.MarkForInspection(carInspectionObj);
            return res;
        }

        public bool InspectionCompleted(int carId)
        {
            //some checks
            if (carId == 0 || carId == null)
            {
                return false;
            }
            //call the repo method
            bool res=_carRentRepository.InspectionCompleted(carId);
            return res;

        }

        //to list cars under inspection 
        public List<CarInspection>  GetCarInspections()
        {
            //call the repo method
           List<CarInspection> list= _carRentRepository.GetCarInspections();
           return list;
        }

    }
}
