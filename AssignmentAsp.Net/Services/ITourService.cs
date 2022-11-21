using AssignmentAsp.Net.Models;

namespace AssignmentAsp.Net.Services
{
    public interface ITourService
    {
        Task Create(TourViewModel model);
        Task Edit(TourViewModel model);
        Task Delete(TourViewModel model);
        Task<TourViewModel> GetById(int id);
        Task<List<TourViewModel>> GetAll(string search);
    }
}
