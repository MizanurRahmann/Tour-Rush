using Microsoft.EntityFrameworkCore;
using AssignmentAsp.Net.Data;
using AssignmentAsp.Net.Models;

namespace AssignmentAsp.Net.Services
{
    public class TourService : ITourService
    {
        private ApplicationDbContext _db;

        public TourService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(TourViewModel model)
        {
            await _db.Tours.AddAsync(model);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(TourViewModel model)
        {
            _db.Tours.Remove(model);
            await _db.SaveChangesAsync();
        }

        public async Task Edit(TourViewModel model)
        {
            _db.Tours.Update(model);
            await _db.SaveChangesAsync();
        }

        public async Task<List<TourViewModel>> GetAll(string searchStr = "", string sortType = "")
        {
            var data = await _db.Tours.ToListAsync();
            if(searchStr != null)
            {
                data = data.Where(x => x.Name.ToLower().Contains(searchStr.ToLower())).ToList();
            }
            return data;
        }

        public async Task<TourViewModel> GetById(int id)
        {
            var data = await _db.Tours.FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }
    }
}
