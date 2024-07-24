using System.Text.Json.Serialization;

namespace dashboard.Models
{
    public class Profesor
    {
        public int Id_profesor { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }

        [JsonIgnore]
        public virtual List<Materia> Materias { get; set; } = new List<Materia>();
    }
}