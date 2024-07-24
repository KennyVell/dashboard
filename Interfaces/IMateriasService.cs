using dashboard.Models;
using dashboard.DTOs;

namespace dashboard.Interfaces
{
    public interface IMateriasService
    {
        Task Add(Materia materia);
        Task<IEnumerable<Materia>> GetAll();
        Task<Materia> GetById(int id);
        Task Update(int id, MateriaDTO materia);
        Task Delete(int id);
    }
}