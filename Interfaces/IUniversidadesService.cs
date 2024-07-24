using dashboard.Models;
using dashboard.DTOs;

namespace dashboard.Interfaces
{
    public interface IUniversidadesService
    {
        Task Add(Universidad universidad);
        Task<IEnumerable<Universidad>> GetAll();
        Task<Universidad> GetById(int id);
        Task Update(int id, UniversidadDTO universidad);
        Task Delete(int id);
    }
}