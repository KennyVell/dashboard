using dashboard.Data;
using dashboard.Interfaces;
using dashboard.Models;
using dashboard.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace dashboard.Services
{
    public class InscripcionesService : IInscripcionesService
    {
        private readonly BaseContext _context;
        private readonly IMapper _mapper;
        public InscripcionesService(BaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(InscripcionDTO inscripcion)
        {
            var nuevoInscripcion = _mapper.Map<Inscripcion>(inscripcion);
            await _context.Inscripciones.AddAsync(nuevoInscripcion);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(id);
            if (inscripcion == null)
            {
                throw new Exception("El inscripcion no existe.");
            }
            _context.Inscripciones.Remove(inscripcion);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Inscripcion>> GetAll()
        {
            var inscripciones = await _context.Inscripciones.ToListAsync();
            if (inscripciones.Any()) return inscripciones;
            return Enumerable.Empty<Inscripcion>();
        }

        public async Task<Inscripcion> GetById(int id)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(id);
            if (inscripcion == null)
            {
                throw new Exception("El inscripcion no existe.");
            }
            return inscripcion;
        }

        public async Task Update(int id, InscripcionDTO inscripcion)
        {
            var inscripcionUpdate = await _context.Inscripciones.FindAsync(id);
            if (inscripcionUpdate == null)
            {
                throw new Exception("El inscripcion no existe.");
            }
            _mapper.Map(inscripcion, inscripcionUpdate);
            await _context.SaveChangesAsync();
        }
    }
}