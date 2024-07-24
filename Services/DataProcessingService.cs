using OfficeOpenXml;
using dashboard.Models;
using dashboard.Interfaces;
using dashboard.Data;

namespace dashboard.Services
{
    public class DataProcessingService : IDataProcessingService
    {
        private readonly BaseContext _context;

        public DataProcessingService(BaseContext context)
        {
            _context = context;
        }

        public void ProcessFile(string filePath)
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
                    var estudiante = new Estudiante
                    {
                        Nombre = sheet.Cells[row, 2].Value.ToString(),
                        Apellido = sheet.Cells[row, 3].Value.ToString(),
                        Correo = sheet.Cells[row, 4].Value.ToString(),
                        Telefono = sheet.Cells[row, 5].Value.ToString()
                    };
                    estudiantes.Add(estudiante);

                    var profesor = new Profesor
                    {
                        Nombre = sheet.Cells[row, 9].Value.ToString(),
                        Apellido = sheet.Cells[row, 10].Value.ToString(),
                        Correo = sheet.Cells[row, 11].Value.ToString(),
                        Telefono = sheet.Cells[row, 12].Value.ToString()
                    };
                    profesores.Add(profesor);

                    var universidad = new Universidad
                    {
                        Nombre = sheet.Cells[row, 15].Value.ToString(),
                        Decano = sheet.Cells[row, 13].Value.ToString()
                    };
                    universidades.Add(universidad);

                    var carrera = new Carrera
                    {
                        Nombre = sheet.Cells[row, 14].Value.ToString(),
                        Id_universidad = int.Parse(sheet.Cells[row, 1].Value.ToString()!)
                    };
                    carreras.Add(carrera);

                    var materia = new Materia
                    {
                        Nombre = sheet.Cells[row, 6].Value.ToString(),
                        Semestre = sheet.Cells[row, 7].Value.ToString(),
                        Año = int.Parse(sheet.Cells[row, 8].Value.ToString()!),
                        Id_carrera = int.Parse(sheet.Cells[row, 1].Value.ToString()!),
                        Id_profesor = int.Parse(sheet.Cells[row, 1].Value.ToString()!)
                    };
                    materias.Add(materia);

                    var inscripcion = new Inscripcion
                    {
                        Estado = sheet.Cells[row, 16].Value.ToString(),
                        Id_materia = int.Parse(sheet.Cells[row, 1].Value.ToString()!),
                        Id_estudiante = int.Parse(sheet.Cells[row, 1].Value.ToString()!)
                    };
                    inscripciones.Add(inscripcion);
                }
            }

            InsertData(estudiantes, profesores, universidades, carreras, materias, inscripciones);
        }

        public void InsertData(List<Estudiante> estudiantes, List<Profesor> profesores, List<Universidad> universidades, List<Carrera> carreras, List<Materia> materias, List<Inscripcion> inscripciones)
        {
            _context.Estudiantes.AddRange(estudiantes);
            _context.Profesores.AddRange(profesores);
            _context.Universidades.AddRange(universidades);
            _context.Carreras.AddRange(carreras);
            _context.Materias.AddRange(materias);
            _context.Inscripciones.AddRange(inscripciones);

            _context.SaveChanges();
        }
    }
}
