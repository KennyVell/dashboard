using dashboard.Models;
using dashboard.DTOs;

namespace dashboard.Interfaces
{
    public interface IInscripcionesService
    {
        Task Add(InscripcionDTO inscripcion);
        Task<IEnumerable<Inscripcion>> GetAll();
        Task<Inscripcion> GetById(int id);
        Task Update(int id, InscripcionDTO inscripcion);
        Task Delete(int id);
    }
}