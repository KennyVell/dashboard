using System.Text.Json.Serialization;

namespace dashboard.Models
{
    public class Profesor
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }

        [JsonIgnore]
        public List<Materia>? Materias { get; set; }
    }
}