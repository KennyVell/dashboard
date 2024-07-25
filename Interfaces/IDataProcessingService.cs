using dashboard.Models;

namespace dashboard.Interfaces
{
    public interface IDataProcessingService
    {
        Task ProcessFileAsync(string filePath);
        void InsertDataAsync(List<Estudiante> estudiantes, List<Profesor> profesores, List<Universidad> universidades, List<Carrera> carreras, List<Materia> materias, List<Inscripcion> inscripciones);
    }
}
