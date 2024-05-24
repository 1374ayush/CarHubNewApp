using CarRentalApp.API.Models;
using CarRentalApp.API.Services.Abstract;
using CarRentalApp.API.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarCrudService _carCrudService;

        public CarController(ICarCrudService carCrudService)
        {
            _carCrudService = carCrudService;
        }

        [HttpPost("AddCar")] 
        public IActionResult AddCar(Car carObj)
        {
            if(ModelState.IsValid)
            {
                //call service method
                Car car = _carCrudService.AddCar(carObj);
                return Ok(car);

            }
            ModelState.AddModelError("", "invalid data");
            return BadRequest(ModelState);
        }


        [HttpGet("GetCars")]
        public IActionResult GetCars()
        {  
            //call service method
            List<Car> cars= _carCrudService.GetCars();
            return Ok(cars);
        }

        [HttpDelete("DeleteCar/{id}")]
        public IActionResult DeleteCar( int id)
        {
            var error = new { message = "Invalid Id" };
            if (id == 0 || id == null)
            {
                
                return BadRequest(error);
            }
            //call service method
            bool isDeleted= _carCrudService.DeleteCar(id);
            if(isDeleted)
            {
                var successResponse = new { message = "Car deleted successfully" };
                return Ok(successResponse);
            }
            
            return BadRequest(error);

        }

        [HttpPut("UpdateCar/{id}")]
        public IActionResult UpdateCar(int id, Car carObj)
        {
            if (ModelState.IsValid)
            {
                //call service method
                bool isUpdated=_carCrudService.UpdateCar(id, carObj);
                if (isUpdated)
                {
                    var successResponse = new { message = "Car updated successfully" };

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

        [HttpGet("GetCar/{id}")]
        public IActionResult GetCar( int id)
        {
            var error = new { message = "Invalid Id" };
            if (id==0 || id == null)
            {
                
                return BadRequest(error);
            }
            //call service method
            Car car=_carCrudService.GetCar(id);
            if (car != null)
            {
                return Ok(car);
            }
            return BadRequest(error);
        }
        

    }
}
