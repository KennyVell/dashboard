namespace dashboard.Models
{
    public class Inscripcion
    {
        public int Id_inscripcion { get; set; }
        public int Id_estudiante { get; set; }
        public int Id_materia { get; set; }
        public string? Estado { get; set; }

        public virtual Estudiante? Estudiante { get; set; }
        public virtual Materia? Materia { get; set; }    
    }
}