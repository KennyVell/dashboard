namespace dashboard.DTOs
{
    public class MateriaDTO
    {
        public string? Nombre { get; set; }
        public string? Semestre { get; set; }
        public int Año { get; set; }
        public int CarreraId { get; set; }
        public int ProfesorId { get; set; }
    }
}