using CarsMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsMVC.Controllers
{
    public class OwnerController : Controller
    {
        private carDBContext _context;

        public OwnerController(carDBContext context)
        {
            _context = context;
        }

        public async Task <IActionResult> Index()
        {
            var allOwner = await _context.Owners.ToListAsync();
            return View(allOwner);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOwnerViewModel request)
        {
            Owner owner = new Owner()
            {
                OwnerId = Guid.NewGuid(),
                Surname = request.Surname,
                Firstname = request.Firstname,
                BirthDate = request.BirthDate
            };

            await _context.Owners.AddAsync(owner);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var selectedOwner = await _context.Owners.FirstOrDefaultAsync(x => x.OwnerId == id);

            return View(selectedOwner);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Owner owner)
        {
            var ownerToUpdate = await _context.Owners.FindAsync(owner.OwnerId);

            ownerToUpdate.Surname = owner.Surname;
            ownerToUpdate.Firstname= owner.Firstname;
            ownerToUpdate.BirthDate = owner.BirthDate;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var selectedOwner =  _context.Owners.FirstOrDefault(x => x.OwnerId == id);

            return Delete(selectedOwner);
        }

        [HttpPost]
        public IActionResult Delete(Owner owner)
        {
            Owner ownerToDelete = _context.Owners.Find(owner.OwnerId);

            _context.Owners.Remove(ownerToDelete);

            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }




        [HttpGet]
        public async Task<IActionResult> Ownership(Guid id)
        {
            var carsofOwner = await _context.Cars.FromSqlInterpolated($"SelectCarsofOwner @id = {id}").ToListAsync();
            OwnershipViewModel ownership = new OwnershipViewModel()
            {
                OwnerId = id,
                OwnerName = _context.Owners.Where(x => x.OwnerId == id).Select(x => x.Surname).SingleOrDefault()
                + " " + _context.Owners.Where(x => x.OwnerId == id).Select(x => x.Firstname).SingleOrDefault(),
                CarList = carsofOwner,
            };

            return View(ownership);
        }
    }
}
