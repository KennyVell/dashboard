using dashboard.Models;
using dashboard.DTOs;

namespace dashboard.Interfaces
{
    public interface IEstudiantesService
    {
        Task Add(EstudianteDTO estudiante);
        Task<IEnumerable<Estudiante>> GetAll();
        Task<Estudiante> GetById(int id);
        Task Update(int id, EstudianteDTO estudiante);
        Task Delete(int id);
    }
}