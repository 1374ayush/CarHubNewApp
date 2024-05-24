using CarRentalApp.API.DAO.Abstract;
using CarRentalApp.API.Data;
using CarRentalApp.API.Models;

namespace CarRentalApp.API.DAO.Implementation
{
    public class CarRentRepository:ICarRentRepository
    {
        private readonly ApplicationDbContext _db;
        public CarRentRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool RentCar(RentalAgreement rentalAgreement)
        {
            // add a new record in the rental agreements table
            _db.RentalAgreements.Add(rentalAgreement);
            _db.SaveChanges();
            return true;
        }

        public List<RentalAgreement> GetUserRentalAgreementList(string userId)
        {
            //fetch the table based on userId and return the list
            return _db.RentalAgreements.Where(obj => obj.UserId == userId).ToList();
        }

        public bool ReturnRequest(int agreementId)
        {
            //fetch the rental agreement by id
            var agreement = _db.RentalAgreements.FirstOrDefault(obj => obj.Id == agreementId);
            if (agreement == null)
            {
                return false;
            }
            //mark the return status column value true
            agreement.ReturnStatus = true;
            _db.SaveChanges();
            return true;
        }
        // for admin
        public List<RentalAgreement> GetRentalAgreements()
        {
            //query the table and return the list of rental agreements
            return _db.RentalAgreements.ToList(); 
        }

        public bool DeleteRentalAgreement(int id)
        {
            //find the agreement from tbl
            RentalAgreement rentalAgreement = _db.RentalAgreements.FirstOrDefault(obj => obj.Id == id);
            if (rentalAgreement == null)
            {
                return false;
            }
            //if agreement exist
            //delete the rental agreement
            _db.RentalAgreements.Remove(rentalAgreement);
            _db.SaveChanges();

            //make the car availabe again
            int carId = rentalAgreement.CarId;
            Car car = _db.Cars.FirstOrDefault(obj => obj.Id == carId);
            car.IsAvailable = "true";
            _db.SaveChanges();

            return true;
        }


        //for admin
        public bool UpdateRentalAgreement(int renalAgreementId, RentalAgreement rentalAgreement)
        {
            _db.RentalAgreements.Update(rentalAgreement);
            _db.SaveChanges();
            return true;
        }

        public RentalAgreement GetRentalAgreementById(int id)
        {
            //fetch the rentalagreement obj based on id and return 
            return _db.RentalAgreements.FirstOrDefault(obj => obj.Id == id);
        }

        public bool MarkForInspection(CarInspection carInspectionObj)
        {
            //add the record in the carInspections table
            _db.CarInspections.Add(carInspectionObj);
            _db.SaveChanges();
            return true;
        }

        public bool InspectionCompleted(int carId)
        {
            //find the obj from carinspections table based on id
            CarInspection obj = _db.CarInspections.FirstOrDefault(obj => obj.CarId == carId);
            if (obj != null)
            {
                //remove the record from tbl
                _db.CarInspections.Remove(obj);
                _db.SaveChanges();
                return true;
            }
            //if object not exist
            return false;
        }

        //to list cars under inspection
        public List<CarInspection> GetCarInspections()
        {
            return _db.CarInspections.ToList();
        }
    }
}
