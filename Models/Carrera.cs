namespace dashboard.Models
{
    public class Carrera
    {
        public int Id_carrera { get; set; }
        public int Id_universidad { get; set; }
        public string? Nombre { get; set; }

        public virtual Universidad? Universidad { get; set; }
    }
}