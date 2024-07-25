using OfficeOpenXml;
using dashboard.Models;
using dashboard.Interfaces;
using dashboard.Data;
using Microsoft.EntityFrameworkCore;

namespace dashboard.Services
{
    public class DataProcessingService : IDataProcessingService
    {
        private readonly BaseContext _context;

        public DataProcessingService(BaseContext context)
        {
            _context = context;
        }

        public async Task ProcessFileAsync(string filePath)
        {
            var estudiantes = new List<Estudiante>();
            var profesores = new List<Profesor>();
            var universidades = new List<Universidad>();
            var carreras = new List<Carrera>();
            var materias = new List<Materia>();
            var inscripciones = new List<Inscripcion>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var sheet = package.Workbook.Worksheets[0];
                int rowCount = sheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    int id = int.Parse(sheet.Cells[row, 1].Value.ToString()!);

                    // Verificar y agregar estudiante si el correo no existe
                    string correoEstudiante = sheet.Cells[row, 4].Value.ToString()!;
                    if (!estudiantes.Any(e => e.Correo == correoEstudiante))
                    {
                        var estudiante = new Estudiante
                        {
                            Nombre = sheet.Cells[row, 2].Value.ToString(),
                            Apellido = sheet.Cells[row, 3].Value.ToString(),
                            Correo = correoEstudiante,
                            Telefono = sheet.Cells[row, 5].Value.ToString()
                        };
                        estudiantes.Add(estudiante);
                    }
                    
                    // Verificar y agregar profesor si el correo no existe
                    string correoProfesor = sheet.Cells[row, 11].Value.ToString()!;
                    if (!profesores.Any(p => p.Correo == correoProfesor))
                    {
                        var profesor = new Profesor
                        {
                            Nombre = sheet.Cells[row, 9].Value.ToString(),
                            Apellido = sheet.Cells[row, 10].Value.ToString(),
                            Correo = correoProfesor,
                            Telefono = sheet.Cells[row, 12].Value.ToString()
                        };
                        profesores.Add(profesor);
                    }

                    // Agregar universidad
                    var universidad = new Universidad
                    {
                        Nombre = sheet.Cells[row, 15].Value.ToString(),
                        Decano = sheet.Cells[row, 13].Value.ToString()
                    };
                    universidades.Add(universidad);

                    // Agregar carrera
                    var carrera = new Carrera
                    {
                        Nombre = sheet.Cells[row, 14].Value.ToString(),
                        UniversidadId = universidades.Last().Id,
                    };
                    carreras.Add(carrera);

                    // Agregar materia
                    var materia = new Materia
                    {
                        Nombre = sheet.Cells[row, 6].Value.ToString(),
                        Semestre = sheet.Cells[row, 7].Value.ToString(),
                        Año = int.Parse(sheet.Cells[row, 8].Value.ToString()!),
                        CarreraId = carreras.Last().Id,
                        ProfesorId = profesores.Last().Id
                    };
                    materias.Add(materia);

                    // Agregar inscripcion
                    var inscripcion = new Inscripcion
                    {
                        Id = id,
                        Estado = sheet.Cells[row, 16].Value.ToString(),
                        EstudianteId = estudiantes.Last().Id,
                        MateriaId = materias.Last().Id
                    };
                    inscripciones.Add(inscripcion);
                }
            }

            await InsertDataAsync(estudiantes, profesores, universidades, carreras, materias, inscripciones);
        }

        public async Task InsertDataAsync(List<Estudiante> estudiantes, List<Profesor> profesores, List<Universidad> universidades, List<Carrera> carreras, List<Materia> materias, List<Inscripcion> inscripciones)
        {
            await _context.Estudiantes.AddRangeAsync(estudiantes);
            await _context.Profesores.AddRangeAsync(profesores);
            await _context.Universidades.AddRangeAsync(universidades);
            await _context.Carreras.AddRangeAsync(carreras);
            await _context.Materias.AddRangeAsync(materias);
            await _context.Inscripciones.AddRangeAsync(inscripciones);

            await _context.SaveChangesAsync();
        }
    }
}
