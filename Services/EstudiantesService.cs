using dashboard.Data;
using dashboard.Interfaces;
using dashboard.Models;
using dashboard.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace dashboard.Services
{
    public class EstudiantesService : IEstudiantesService
    {
        private readonly BaseContext _context;
        private readonly IMapper _mapper;
        public EstudiantesService(BaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(EstudianteDTO estudiante)
        {
            if (await _context.Estudiantes.AnyAsync(e => e.Correo == estudiante.Correo))
            {
                throw new Exception("El correo electrónico ya está en uso.");
            }

            var nuevoEstudiante = _mapper.Map<Estudiante>(estudiante);
            await _context.Estudiantes.AddAsync(nuevoEstudiante);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                throw new Exception("El estudiante no existe.");
            }
            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Estudiante>> GetAll()
        {
            var estudiantes = await _context.Estudiantes.ToListAsync();
            if (estudiantes.Any()) return estudiantes;
            return Enumerable.Empty<Estudiante>();
        }

        public async Task<Estudiante> GetById(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                throw new Exception("El estudiante no existe.");
            }
            return estudiante;
        }

        public async Task Update(int id, EstudianteDTO estudiante)
        {
            var estudianteUpdate = await _context.Estudiantes.FindAsync(id);
            if (estudianteUpdate == null)
            {
                throw new Exception("El estudiante no existe.");
            }
            _mapper.Map(estudiante, estudianteUpdate);
            await _context.SaveChangesAsync();
        }
    }
}