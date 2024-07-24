using dashboard.Data;
using dashboard.Interfaces;
using dashboard.Models;
using dashboard.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace dashboard.Services
{
    public class CarrerasService : ICarrerasService
    {
        private readonly BaseContext _context;
        private readonly IMapper _mapper;
        public CarrerasService(BaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(CarreraDTO carrera)
        {
            var nuevoCarrera = _mapper.Map<Carrera>(carrera);
            await _context.Carreras.AddAsync(nuevoCarrera);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            if (carrera == null)
            {
                throw new Exception("La carrera no existe.");
            }
            _context.Carreras.Remove(carrera);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Carrera>> GetAll()
        {
            var carreras = await _context.Carreras.ToListAsync();
            if (carreras.Any()) return carreras;
            return Enumerable.Empty<Carrera>();
        }

        public async Task<Carrera> GetById(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            if (carrera == null)
            {
                throw new Exception("La carrera no existe.");
            }
            return carrera;
        }

        public async Task Update(int id, CarreraDTO carrera)
        {
            var carreraUpdate = await _context.Carreras.FindAsync(id);
            if (carreraUpdate == null)
            {
                throw new Exception("La carrera no existe.");
            }
            _mapper.Map(carrera, carreraUpdate);
            await _context.SaveChangesAsync();
        }
    }
}