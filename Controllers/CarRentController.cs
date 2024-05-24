using CarRentalApp.API.Models;
using CarRentalApp.API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentController : ControllerBase
    {
        private readonly ICarRentService _carRentService;
        public CarRentController(ICarRentService carRentService)
        {
            _carRentService = carRentService;
        }


        [HttpPost("rent")]
        public IActionResult RentCar( RentalAgreement rentalAgreement)
        {
            if(ModelState.IsValid)
            {
                //call the service method
                bool res=_carRentService.RentCar(rentalAgreement);
                if (res)
                {
                    var successResponse = new { message = "Agreement Generated" };
                    return Ok(successResponse);
                }
                else
                {
                    var failedResponse = new { message = "Cannot rent" };
                    return BadRequest(failedResponse);
                }
            }
            ModelState.AddModelError("", "invalid");
            return BadRequest(ModelState);
        }

        [HttpGet("RentalAgreements/{username}")]
        public IActionResult GetUserRentalAgreements(string username)
        {
            //call the service method
            List<RentalAgreement> userRentalAgreements=_carRentService.GetUserRentalAgreementList(username);
            return Ok(userRentalAgreements);
        }

        [HttpGet("Return/{rentalAgreementId}")]
        public IActionResult ReturnRequest( int rentalAgreementId)
        {
            //call the service method
            bool res=_carRentService.ReturnRequest(rentalAgreementId);
            if (res)
            {
                var successResponse = new { message = "Return Initiated" };
                return Ok(successResponse);
            }
            var error = new { message = "Invalid" };
            return Ok(error);
        }

        [HttpGet("RentalAgreements")]
        //will be used by admin
        public IActionResult GetRentalAgreements()
        {
            //call the service method
            List<RentalAgreement> rentalAgreements = _carRentService.GetRentalAgreements();
            return Ok(rentalAgreements);
        }

        [HttpGet("GetRentalAgreement/{id}")]
        public IActionResult GetRentalAgreementById(int id)
        {
            // some checks
            if(id==0 || id == null)
            {
                var error = new { message = "Invalid Id" };
                return BadRequest(error);
            }
            //call service method
            RentalAgreement rentalAgreement=_carRentService.GetRentalAgreementById(id);
            if (rentalAgreement == null)
            {
                var error = new { message = "Invalid Id" };
                return BadRequest(error);
            }
            //if rental agreement exist
            return Ok(rentalAgreement);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteRentalAgreement( int id)
        {
            //call the service method
            bool res = _carRentService.DeleteRentalAgreement(id);
            if (res)
            {
                var successResponse = new { message = "Deleted Successfully" };
                return Ok(successResponse);
            }
            var error = new { message = "Invalid Id" };
            return Ok(error);
        }

        [HttpPut("RentalAgreement/{rentalAgreementId}")]
        //for admin
        public IActionResult UpdateRentalAgreement( int rentalAgreementId, RentalAgreement rentalAgreement)
        {
            if (ModelState.IsValid)
            {
                //call the service method
                bool isUpdated = _carRentService.UpdateRentalAgreement(rentalAgreementId, rentalAgreement);
                if (isUpdated)
                {
                    var successResponse = new { message = "Rental agreement updated successfully" };

                    return Ok(successResponse);
                }
                else
                {
                    var error = new { message = "Invalid Id" };
                    return BadRequest(error);
                }

            }
            ModelState.AddModelError("", "invalid");
            return BadRequest(ModelState);
        }

        [HttpPost("MarkForInspection")]
        public IActionResult MarkForInspection( CarInspection carInspectionObj)
        {
            bool res=_carRentService.MarkForInspection(carInspectionObj);

           return Ok(carInspectionObj);   

        }
        [HttpGet("InspectionCompleted/{carId}")]
        //for admin
        public IActionResult InspectionCompleted( int carId)
        {
            //call the service method
            bool res=_carRentService.InspectionCompleted(carId);
            if (res == true)
            {
                var response = new { message = "Success" };
                return Ok(response);
            }
            var error = new { message = "Invalid Id" };
            return BadRequest(error);
        }

        //to list cars under inspection
        [HttpGet("GetCarInspections")]
        public IActionResult GetCarInspections()
        {
            //cal the service method
            List<CarInspection> list = _carRentService.GetCarInspections();  /*_db.CarInspections.ToList();*/

            return Ok(list);
        }

        
    }

}
