using dashboard.Models;

namespace dashboard.Interfaces
{
    public interface IDataProcessingService
    {
        public Task ProcessFileAsync(string filePath);
        //public Task InsertDataAsync(List<Estudiante> estudiantes, List<Profesor> profesores, List<Universidad> universidades, List<Carrera> carreras, List<Materia> materias, List<Inscripcion> inscripciones);
    }
}
