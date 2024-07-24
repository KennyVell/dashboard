using dashboard.Models;
using dashboard.DTOs;

namespace dashboard.Interfaces
{
    public interface IProfesoresService
    {
        Task Add(ProfesorDTO profesor);
        Task<IEnumerable<Profesor>> GetAll();
        Task<Profesor> GetById(int id);
        Task Update(int id, ProfesorDTO profesor);
        Task Delete(int id);
    }
}