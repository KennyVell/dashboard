using dashboard.Data;
using dashboard.Interfaces;
using dashboard.Models;
using dashboard.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace dashboard.Services
{
    public class MateriasService : IMateriasService
    {
        private readonly BaseContext _context;
        private readonly IMapper _mapper;
        public MateriasService(BaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(Materia Materia)
        {
            var nuevoMateria = _mapper.Map<Materia>(Materia);
            await _context.Materias.AddAsync(nuevoMateria);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var Materia = await _context.Materias.FindAsync(id);
            if (Materia == null)
            {
                throw new Exception("La Materia no existe.");
            }
            _context.Materias.Remove(Materia);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Materia>> GetAll()
        {
            var Materias = await _context.Materias.ToListAsync();
            if (Materias.Any()) return Materias;
            return Enumerable.Empty<Materia>();
        }

        public async Task<Materia> GetById(int id)
        {
            var Materia = await _context.Materias.FindAsync(id);
            if (Materia == null)
            {
                throw new Exception("La Materia no existe.");
            }
            return Materia;
        }

        public async Task Update(int id, MateriaDTO Materia)
        {
            var MateriaUpdate = await _context.Materias.FindAsync(id);
            if (MateriaUpdate == null)
            {
                throw new Exception("La Materia no existe.");
            }
            _mapper.Map(Materia, MateriaUpdate);
            await _context.SaveChangesAsync();
        }
    }
}