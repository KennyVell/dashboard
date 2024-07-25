using System.Text.Json.Serialization;

namespace dashboard.Models
{
    public class Universidad
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Decano { get; set; }

        [JsonIgnore]
        public List<Carrera>? Carreras { get; set; }
    }
}