using Microsoft.EntityFrameworkCore;
using dashboard.Models;

namespace dashboard.Data;

public class BaseContext : DbContext
{
    public BaseContext(DbContextOptions<BaseContext> options) : base(options) { }
    
    //conexicón con modelos
    public DbSet<Estudiante> Estudiantes { get; set; }
    public DbSet<Universidad> Universidades { get; set; }
    public DbSet<Carrera> Carreras { get; set; }
    public DbSet<Materia> Materias { get; set; }
    public DbSet<Profesor> Profesores { get; set; }
    public DbSet<Inscripcion> Inscripciones { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<SocialLogin> SocialLogins { get; set; }
} 