using dashboard.Models;
using dashboard.DTOs;

namespace dashboard.Interfaces
{
    public interface ICarrerasService
    {
        Task Add(Carrera carrera);
        Task<IEnumerable<Carrera>> GetAll();
        Task<Carrera> GetById(int id);
        Task Update(int id, CarreraDTO carrera);
        Task Delete(int id);
    }
}