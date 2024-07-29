using AutoMapper;
using dashboard.DTOs;
using dashboard.Models;

namespace dashboard.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<EstudianteDTO, Estudiante>();
            CreateMap<Estudiante, EstudianteDTO>().ReverseMap();

            CreateMap<ProfesorDTO, Profesor>();
            CreateMap<Profesor, ProfesorDTO>().ReverseMap();

            CreateMap<MateriaDTO, Materia>();
            CreateMap<Materia, MateriaDTO>().ReverseMap();

            CreateMap<CarreraDTO, Carrera>();
            CreateMap<Carrera, CarreraDTO>().ReverseMap();

            CreateMap<UniversidadDTO, Universidad>();
            CreateMap<Universidad, UniversidadDTO>().ReverseMap();

            CreateMap<InscripcionDTO, Inscripcion>();
            CreateMap<Inscripcion, InscripcionDTO>().ReverseMap();
        }
    }
}