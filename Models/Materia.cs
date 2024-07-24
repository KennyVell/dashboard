using System.Text.Json.Serialization;

namespace dashboard.Models
{
    public class Materia
    {
        public int Id_materia { get; set; }
        public string? Nombre { get; set; }
        public string? Semestre { get; set; }
        public int Año { get; set; }
        public int Id_carrera { get; set; }
        public int Id_profesor { get; set; }

        public virtual Carrera? Carrera { get; set; }
        public virtual Profesor? Profesor { get; set; }

        [JsonIgnore]
        public virtual List<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}