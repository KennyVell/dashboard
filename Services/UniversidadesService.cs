using dashboard.Data;
using dashboard.Interfaces;
using dashboard.Models;
using dashboard.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace dashboard.Services
{
    public class UniversidadesService : IUniversidadesService
    {
        private readonly BaseContext _context;
        private readonly IMapper _mapper;
        public UniversidadesService(BaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(UniversidadDTO universidad)
        {
            var nuevoUniversidad = _mapper.Map<Universidad>(universidad);
            await _context.Universidades.AddAsync(nuevoUniversidad);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var universidad = await _context.Universidades.FindAsync(id);
            if (universidad == null)
            {
                throw new Exception("El universidad no existe.");
            }
            _context.Universidades.Remove(universidad);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Universidad>> GetAll()
        {
            var universidades = await _context.Universidades.ToListAsync();
            if (universidades.Any()) return universidades;
            return Enumerable.Empty<Universidad>();
        }

        public async Task<Universidad> GetById(int id)
        {
            var universidad = await _context.Universidades.FindAsync(id);
            if (universidad == null)
            {
                throw new Exception("El universidad no existe.");
            }
            return universidad;
        }

        public async Task Update(int id, UniversidadDTO universidad)
        {
            var universidadUpdate = await _context.Universidades.FindAsync(id);
            if (universidadUpdate == null)
            {
                throw new Exception("El universidad no existe.");
            }
            _mapper.Map(universidad, universidadUpdate);
            await _context.SaveChangesAsync();
        }
    }
}