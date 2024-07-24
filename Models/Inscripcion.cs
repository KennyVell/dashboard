namespace dashboard.Models
{
    public class Inscripcion
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public int MateriaId { get; set; }
        public string? Estado { get; set; }

        public Estudiante? Estudiante { get; set; }
        public Materia? Materia { get; set; }    
    }
}