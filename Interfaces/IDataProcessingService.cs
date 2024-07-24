using dashboard.Models;

namespace dashboard.Interfaces
{
    public interface IDataProcessingService
    {
        void ProcessFile(string filePath);
        void InsertData(List<Estudiante> estudiantes, List<Profesor> profesores, List<Universidad> universidades, List<Carrera> carreras, List<Materia> materias, List<Inscripcion> inscripciones);
    }
}
