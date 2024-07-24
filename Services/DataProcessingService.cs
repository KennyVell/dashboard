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
                int colCount = sheet.Dimension.Rows;

                for (int col = 2; col <= colCount; col++)
                {
                    var estudiante = new Estudiante
                    {
                        Nombre = sheet.Cells[col, 2].Value.ToString(),
                        Apellido = sheet.Cells[col, 3].Value.ToString(),
                        Correo = sheet.Cells[col, 4].Value.ToString(),
                        Telefono = sheet.Cells[col, 5].Value.ToString()
                    };
                    estudiantes.Add(estudiante);

                    var profesor = new Profesor
                    {
                        Nombre = sheet.Cells[col, 9].Value.ToString(),
                        Apellido = sheet.Cells[col, 10].Value.ToString(),
                        Correo = sheet.Cells[col, 11].Value.ToString(),
                        Telefono = sheet.Cells[col, 12].Value.ToString()
                    };
                    profesores.Add(profesor);

                    var universidad = new Universidad
                    {
                        Nombre = sheet.Cells[col, 15].Value.ToString(),
                        Decano = sheet.Cells[col, 13].Value.ToString()
                    };
                    universidades.Add(universidad);

                    var carrera = new Carrera
                    {
                        Nombre = sheet.Cells[col, 14].Value.ToString(),
                        Id_universidad = (int)sheet.Cells[col, 1].Value
                    };
                    carreras.Add(carrera);

                    var materia = new Materia
                    {
                        Nombre = sheet.Cells[col, 6].Value.ToString(),
                        Semestre = sheet.Cells[col, 7].Value.ToString(),
                        Año = (int)sheet.Cells[col, 8].Value,
                        Id_carrera = (int)sheet.Cells[col, 1].Value,
                        Id_profesor = (int)sheet.Cells[col, 1].Value
                    };
                    materias.Add(materia);

                    var inscripcion = new Inscripcion
                    {
                        Estado = sheet.Cells[col, 16].Value.ToString(),
                        Id_materia = (int)sheet.Cells[col, 1].Value,
                        Id_estudiante = (int)sheet.Cells[col, 1].Value
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
