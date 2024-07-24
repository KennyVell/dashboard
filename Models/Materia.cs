using System.Text.Json.Serialization;

namespace dashboard.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Semestre { get; set; }
        public int Año { get; set; }
        public int CateriaId { get; set; }
        public int ProfesorId { get; set; }

        public Carrera? Carrera { get; set; }
        public Profesor? Profesor { get; set; }

        [JsonIgnore]
        public List<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}