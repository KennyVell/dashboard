CREATE TABLE Estudiantes (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Correo VARCHAR(100) UNIQUE NOT NULL,
    Telefono VARCHAR(50) NOT NULL
);

CREATE TABLE Profesores (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Correo VARCHAR(100) UNIQUE NOT NULL,
    Telefono VARCHAR(50) NOT NULL
);

CREATE TABLE Universidades (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Decano VARCHAR(100) NOT NULL
);

CREATE TABLE Carreras (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    UniversidadId INT NOT NULL,
    FOREIGN KEY (UniversidadId) REFERENCES Universidades(Id)
);

CREATE TABLE Materias (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Semestre VARCHAR(10) NOT NULL,
    Año INT NOT NULL,
    CarreraId INT NOT NULL,
    ProfesorId INT NOT NULL,
    FOREIGN KEY (carreraId) REFERENCES Carreras(Id),
    FOREIGN KEY (profesorId) REFERENCES Profesores(Id)
);

CREATE TABLE Inscripciones (
    Id INT PRIMARY KEY,
    EstudianteId INT NOT NULL,
    MateriaId INT NOT NULL,
    Estado VARCHAR(20) NOT NULL,
    FOREIGN KEY (EstudianteId) REFERENCES Estudiantes(Id),
    FOREIGN KEY (MateriaId) REFERENCES Materias(Id)
);
