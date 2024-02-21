using CarsMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsMVC.Controllers
{
    public class CarController : Controller
    {
        private carDBContext _context;

        public CarController(carDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allCar = await _context.Cars.ToListAsync();
            return View(allCar);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarViewModel request)
        {
            Car car = new Car()
            {
                CarId = Guid.NewGuid(),
                Brand = request.Brand,
                Type = request.Type,
                RegistrationNumber = request.RegistrationNumber,
                ProductionDate= request.ProductionDate
            };

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var selectedCar = await _context.Cars.FirstOrDefaultAsync(x => x.CarId == id);

            return View(selectedCar);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Car car)
        {
            var carToUpdate = await _context.Cars.FindAsync(car.CarId);

            carToUpdate.Brand = car.Brand;
            carToUpdate.Type = car.Type;
            carToUpdate.RegistrationNumber = car.RegistrationNumber;
            carToUpdate.ProductionDate = car.ProductionDate;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var selectedCar = _context.Cars.FirstOrDefault(x => x.CarId == id);

            return Delete(selectedCar);
        }

        [HttpPost]
        public IActionResult Delete(Car car)
        {
            Car carToDelete = _context.Cars.Find(car.CarId);

            _context.Cars.Remove(carToDelete);

            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
