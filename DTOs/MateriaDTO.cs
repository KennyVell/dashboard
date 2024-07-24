namespace dashboard.DTOs
{
    public class MateriaDTO
    {
        public string? Nombre { get; set; }
        public string? Semestre { get; set; }
        public int Año { get; set; }
        public int Id_carrera { get; set; }
        public int Id_profesor { get; set; }
    }
}