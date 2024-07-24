using System.Text.Json.Serialization;

namespace dashboard.Models
{
    public class Carrera
    {
        public int Id { get; set; }
        public int UniversidadId { get; set; }
        public string? Nombre { get; set; }

        public Universidad? Universidad { get; set; }
        [JsonIgnore]
        public List<Materia> Materias { get; set; } = new List<Materia>();
    }
}