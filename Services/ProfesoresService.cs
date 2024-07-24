using dashboard.Data;
using dashboard.Interfaces;
using dashboard.Models;
using dashboard.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace dashboard.Services
{
    public class ProfesoresService : IProfesoresService
    {
        private readonly BaseContext _context;
        private readonly IMapper _mapper;
        public ProfesoresService(BaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(Profesor profesor)
        {
            if (await _context.Profesores.AnyAsync(e => e.Correo == profesor.Correo))
            {
                throw new Exception("El correo electrónico ya está en uso.");
            }

            var nuevoProfesor = _mapper.Map<Profesor>(profesor);
            await _context.Profesores.AddAsync(nuevoProfesor);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null)
            {
                throw new Exception("El profesor no existe.");
            }
            _context.Profesores.Remove(profesor);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Profesor>> GetAll()
        {
            var profesores = await _context.Profesores.ToListAsync();
            if (profesores.Any()) return profesores;
            return Enumerable.Empty<Profesor>();
        }

        public async Task<Profesor> GetById(int id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null)
            {
                throw new Exception("El profesor no existe.");
            }
            return profesor;
        }

        public async Task Update(int id, ProfesorDTO profesor)
        {
            var profesorUpdate = await _context.Profesores.FindAsync(id);
            if (profesorUpdate == null)
            {
                throw new Exception("El profesor no existe.");
            }
            _mapper.Map(profesor, profesorUpdate);
            await _context.SaveChangesAsync();
        }
    }
}