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

        /* public async Task ProcessFileAsync(string filePath)
        {
            var estudiantes = new List<Estudiante>();
            var profesores = new List<Profesor>();
            var universidades = new List<Universidad>();
            var carreras = new List<Carrera>();
            var materias = new List<Materia>();
            var inscripciones = new List<Inscripcion>();

            var data = new List<Dictionary<string, object>>();

            // Cargar datos del archivo Excel en memoria
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var sheet = package.Workbook.Worksheets[0];
                int rowCount = sheet.Dimension.Rows;
                int colCount = sheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++) // Comenzamos desde 2 para omitir los encabezados
                {
                    var rowData = new Dictionary<string, object>();
                    for (int col = 1; col <= colCount; col++)
                    {
                        var cellValue = sheet.Cells[row, col].Value;
                        var header = sheet.Cells[1, col].Value.ToString();
                        rowData[header!] = cellValue;
                    }
                    data.Add(rowData);
                }
            }

            // Procesar los datos en memoria
            foreach (var rowData in data)
            {
                // Obtener valores desde el diccionario
                var correoEstudiante = rowData["CorreoEstudiante"].ToString();
                var correoProfesor = rowData["CorreoProfesor"].ToString();
                var universidadNombre = rowData["NombreUniversidad"].ToString();
                var carreraNombre = rowData["NombreCarrera"].ToString();
                var materiaNombre = rowData["NombreMateria"].ToString();
                var estadoInscripcion = rowData["EstadoInscripcion"].ToString();
                var idInscripcion = int.Parse(rowData["IdInscripcion"].ToString()!);

                // Agregar estudiante
                if (!_context.Estudiantes.Any(e => e.Correo == correoEstudiante))
                {
                    estudiantes.Add(new Estudiante
                    {
                        Nombre = rowData["NombreEstudiante"].ToString(),
                        Apellido = rowData["ApellidoEstudiante"].ToString(),
                        Correo = correoEstudiante,
                        Telefono = rowData["TelefonoEstudiante"].ToString()
                    });
                }

                // Agregar profesor
                if (!_context.Profesores.Any(p => p.Correo == correoProfesor))
                {
                    profesores.Add(new Profesor
                    {
                        Nombre = rowData["NombreProfesor"].ToString(),
                        Apellido = rowData["ApellidoProfesor"].ToString(),
                        Correo = correoProfesor,
                        Telefono = rowData["TelefonoProfesor"].ToString()
                    });
                }

                // Agregar universidad
                if (!universidades.Any(u => u.Nombre == universidadNombre))
                {
                    universidades.Add(new Universidad
                    {
                        Nombre = universidadNombre,
                        Decano = rowData["DecanoUniversidad"].ToString()
                    });
                }

                // Agregar carrera
                carreras.Add(new Carrera
                {
                    Nombre = carreraNombre,
                    UniversidadId = universidades.First(u => u.Nombre == universidadNombre).Id
                });

                // Agregar materia
                materias.Add(new Materia
                {
                    Nombre = materiaNombre,
                    Semestre = rowData["SemestreMateria"].ToString(),
                    Año = int.Parse(rowData["AnioMateria"].ToString()!),
                    CarreraId = carreras.First(c => c.Nombre == carreraNombre).Id,
                    ProfesorId = _context.Profesores.First(p => p.Correo == correoProfesor).Id
                });

                // Agregar inscripción
                inscripciones.Add(new Inscripcion
                {
                    Id = idInscripcion,
                    Estado = estadoInscripcion,
                    EstudianteId = _context.Estudiantes.First(e => e.Correo == correoEstudiante).Id,
                    MateriaId = materias.First(m => m.Nombre == materiaNombre).Id
                });
            }

            // Guardar todos los datos en la base de datos
            await InsertDataAsync(estudiantes, profesores, universidades, carreras, materias, inscripciones);
        }

        public async Task InsertDataAsync(List<Estudiante> estudiantes, List<Profesor> profesores, List<Universidad> universidades, List<Carrera> carreras, List<Materia> materias, List<Inscripcion> inscripciones)
        {
            if (estudiantes.Any())
                await _context.Estudiantes.AddRangeAsync(estudiantes);
            if (profesores.Any())
                await _context.Profesores.AddRangeAsync(profesores);
            if (universidades.Any())
                await _context.Universidades.AddRangeAsync(universidades);
            if (carreras.Any())
                await _context.Carreras.AddRangeAsync(carreras);
            if (materias.Any())
                await _context.Materias.AddRangeAsync(materias);
            if (inscripciones.Any())
                await _context.Inscripciones.AddRangeAsync(inscripciones);

            if (estudiantes.Any() || profesores.Any() || universidades.Any() || carreras.Any() || materias.Any() || inscripciones.Any())
                await _context.SaveChangesAsync();
        } */


        /* public async Task ProcessFileAsync(string filePath)
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
                    // Verificar y agregar estudiante si el correo no existe
                    string correoEstudiante = sheet.Cells[row, 4].Value.ToString()!;
                    if (!_context.Estudiantes.Any(e => e.Correo == correoEstudiante))
                    {
                        var estudiante = new Estudiante
                        {
                            Nombre = sheet.Cells[row, 2].Value.ToString(),
                            Apellido = sheet.Cells[row, 3].Value.ToString(),
                            Correo = correoEstudiante,
                            Telefono = sheet.Cells[row, 5].Value.ToString()
                        };
                        estudiantes.Add(estudiante);
                        // _context.Estudiantes.Add(estudiante);
                    }

                    // Verificar y agregar profesor si el correo no existe
                    string correoProfesor = sheet.Cells[row, 11].Value.ToString()!;
                    if (!_context.Profesores.Any(p => p.Correo == correoProfesor))
                    {
                        var profesor = new Profesor
                        {
                            Nombre = sheet.Cells[row, 9].Value.ToString(),
                            Apellido = sheet.Cells[row, 10].Value.ToString(),
                            Correo = correoProfesor,
                            Telefono = sheet.Cells[row, 12].Value.ToString()
                        };
                        profesores.Add(profesor);
                        // _context.Profesores.Add(profesor);
                    }

                    // Agregar universidad
                    var universidad = new Universidad
                    {
                        Nombre = sheet.Cells[row, 15].Value.ToString()!,
                        Decano = sheet.Cells[row, 13].Value.ToString()
                    };
                    universidades.Add(universidad);
                    // _context.Universidades.Add(universidad);

                    // Agregar carrera
                    var carrera = new Carrera
                    {
                        Nombre = sheet.Cells[row, 14].Value.ToString(),
                        UniversidadId = _context.Universidades.Last().Id,
                    };
                    carreras.Add(carrera);
                    // _context.Carreras.Add(carrera);

                    // Agregar materia
                    var materia = new Materia
                    {
                        Nombre = sheet.Cells[row, 6].Value.ToString(),
                        Semestre = sheet.Cells[row, 7].Value.ToString(),
                        Año = int.Parse(sheet.Cells[row, 8].Value.ToString()!),
                        CarreraId = _context.Carreras.Last().Id,
                        ProfesorId = _context.Profesores.First(p => p.Correo == correoProfesor).Id
                    };
                    materias.Add(materia);
                    // _context.Materias.Add(materia);

                    // Agregar inscripción
                    var inscripcion = new Inscripcion
                    {
                        Id = int.Parse(sheet.Cells[row, 1].Value.ToString()!),
                        Estado = sheet.Cells[row, 16].Value.ToString(),
                        EstudianteId = _context.Estudiantes.First(e => e.Correo == correoEstudiante).Id,
                        MateriaId = _context.Materias.Last().Id
                    };
                    inscripciones.Add(inscripcion);
                    //_context.Inscripciones.Add(inscripcion);

                    // Guardar cambios después de cada inserción
                    await _context.SaveChangesAsync();
                }
            }
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
        } */

        public async Task ProcessFileAsync(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var sheet = package.Workbook.Worksheets[0];
                int rowCount = sheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    int id = int.Parse(sheet.Cells[row, 1].Value.ToString()!);

                    // Verificar y agregar estudiante si el correo no existe
                    string correoEstudiante = sheet.Cells[row, 4].Value.ToString()!;
                    if (!await _context.Estudiantes.AnyAsync(e => e.Correo == correoEstudiante))
                    {
                        var estudiante = new Estudiante
                        {
                            Nombre = sheet.Cells[row, 2].Value.ToString(),
                            Apellido = sheet.Cells[row, 3].Value.ToString(),
                            Correo = correoEstudiante,
                            Telefono = sheet.Cells[row, 5].Value.ToString()
                        };
                        _context.Estudiantes.Add(estudiante);
                        await _context.SaveChangesAsync();  //Save here to get the ID for the next steps
                    }

                    // Verificar y agregar profesor si el correo no existe
                    string correoProfesor = sheet.Cells[row, 11].Value.ToString()!;
                    if (!await _context.Profesores.AnyAsync(p => p.Correo == correoProfesor))
                    {
                        var profesor = new Profesor
                        {
                            Nombre = sheet.Cells[row, 9].Value.ToString(),
                            Apellido = sheet.Cells[row, 10].Value.ToString(),
                            Correo = correoProfesor,
                            Telefono = sheet.Cells[row, 12].Value.ToString()
                        };
                        _context.Profesores.Add(profesor);
                        await _context.SaveChangesAsync();  //Save here to get the ID for the next steps
                    }

                    //Agregar universidad
                    var universidad = new Universidad
                    {
                        Nombre = sheet.Cells[row, 15].Value.ToString()!,
                        Decano = sheet.Cells[row, 13].Value.ToString()
                    };
                    _context.Universidades.Add(universidad);
                    await _context.SaveChangesAsync();  //Save here to get the ID for the next steps

                    //Agregar carrera
                    var carrera = new Carrera
                    {
                        Nombre = sheet.Cells[row, 14].Value.ToString(),
                        UniversidadId = await _context.Universidades
                                            .Where(u => u.Nombre == universidad.Nombre)
                                            .OrderByDescending(u => u.Id)
                                            .Select(u => u.Id)
                                            .FirstOrDefaultAsync()
                    };
                    _context.Carreras.Add(carrera);
                    await _context.SaveChangesAsync();  //Save here to get the ID for the next steps

                    //Agregar materia
                    var materia = new Materia
                    {
                        Nombre = sheet.Cells[row, 6].Value.ToString(),
                        Semestre = sheet.Cells[row, 7].Value.ToString(),
                        Año = int.Parse(sheet.Cells[row, 8].Value.ToString()!),
                        CarreraId = await _context.Carreras
                                            .Where(c => c.Nombre == carrera.Nombre)
                                            .OrderByDescending(c => c.Id)
                                            .Select(c => c.Id)
                                            .FirstOrDefaultAsync(),
                        ProfesorId = await _context.Profesores
                                            .Where(p => p.Correo == correoProfesor)
                                            .OrderByDescending(p => p.Id)
                                            .Select(p => p.Id)
                                            .FirstOrDefaultAsync()
                    };
                    _context.Materias.Add(materia);
                    await _context.SaveChangesAsync();  //Save here to get the ID for the next steps

                    //Agregar inscripción
                    var inscripcion = new Inscripcion
                    {
                        Id = id,
                        Estado = sheet.Cells[row, 16].Value.ToString(),
                        EstudianteId = await _context.Estudiantes
                                            .Where(e => e.Correo == correoEstudiante)
                                            .OrderByDescending(e => e.Id)
                                            .Select(e => e.Id)
                                            .FirstOrDefaultAsync(),
                        MateriaId = await _context.Materias
                                            .Where(m => m.Nombre == materia.Nombre)
                                            .OrderByDescending(m => m.Id)
                                            .Select(m => m.Id)
                                            .FirstOrDefaultAsync()
                    };
                    _context.Inscripciones.Add(inscripcion);
                    await _context.SaveChangesAsync();  //Save here to complete the process for this row
                }
            }
        }

    }
}
