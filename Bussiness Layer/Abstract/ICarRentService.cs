using CarRentalApp.API.Models;

namespace CarRentalApp.API.Services.Abstract
{
    public interface ICarRentService
    {
        bool RentCar(RentalAgreement rentalAgreement);
        List<RentalAgreement> GetUserRentalAgreementList(string userId);
        bool ReturnRequest(int agreementId);
        List<RentalAgreement> GetRentalAgreements();
        bool DeleteRentalAgreement(int id);
        bool UpdateRentalAgreement(int renalAgreementId, RentalAgreement rentalAgreement);
        RentalAgreement GetRentalAgreementById(int id);
        bool MarkForInspection(CarInspection carInspectionObj);

        bool InspectionCompleted(int carId);
        List<CarInspection> GetCarInspections();
    }
}
