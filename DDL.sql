-- Active: 1691412339871@@127.0.0.1@3306@GranET12


-- Crear la base de datos
CREATE DATABASE GranET12;

-- Tabla: Equipo
CREATE TABLE Equipo (
    id_equipo INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL UNIQUE
    -- Máximo 32 equipos ( Nota: Se tiene que controlar por el trigger [Si carmelo, se tiene ])
);

-- Tabla: futbolista
CREATE TABLE Futbolista (
    id_futbolista INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    apodo VARCHAR(45) NULL,
    fecha_nacimiento DATE NOT NULL,
    cotizacion DECIMAL(10,2) NOT NULL CHECK (cotizacion >= 0 AND cotizacion <= 99999999.99),
    tipo ENUM('Arquero','Defensor','Mediocampista','Delantero') NOT NULL,
    id_equipo INT NOT NULL
);

-- Tabla: Usuario

CREATE TABLE Usuario (
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    fecha_nacimiento DATE NOT NULL,
    contrasenia CHAR(64) NOT NULL
);

-- Tabla: Plantilla

CREATE TABLE PlantillaTitular(
    id_plantilla INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    presupuesto_max DECIMAL(10,2) NOT NULL,
    cant_max_futbolistas INT NOT NULL
);

CREATE TABLE Puntaje(
    id_plantilla INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    presupuesto_max DECIMAL(10,2) NOT NULL,
    cant_max_futbolistas INT NOT NULL
);


CREATE TABLE PlantillaSuplente(
    id_plantilla INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    presupuesto_max DECIMAL(10,2) NOT NULL,
    cant_max_futbolistas INT NOT NULL
);


CREATE TABLE PlantillaFutbolista (
    id_plantilla_futbolista INT AUTO_INCREMENT PRIMARY KEY,
    id_plantilla INT NOT NULL,
    id_futbolista INT NOT NULL,
    es_titular BOOLEAN NOT NULL,
    CONSTRAINT uq_plantilla_futbolista UNIQUE (id_plantilla, id_futbolista)
);

-- Tabla: Puntuacionfutbolista
CREATE TABLE PuntuacionFutbolista (
    id_puntuacion INT AUTO_INCREMENT PRIMARY KEY,
    id_futbolista INT NOT NULL,
    fecha INT NOT NULL CHECK (fecha >= 1 AND fecha <= 49),
    puntuacion DECIMAL(3,1) NOT NULL CHECK (puntuacion >= 1.0 AND puntuacion <= 10.0),
    CONSTRAINT uq_futbolista_fecha UNIQUE (id_futbolista, fecha)
);  




ALTER TABLE Futbolista
ADD CONSTRAINT fk_futbolista_equipo
FOREIGN KEY (id_equipo) REFERENCES Equipo(id_equipo);

ALTER TABLE Plantilla
ADD CONSTRAINT fk_plantilla_usuario
FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario);

ALTER TABLE PlantillaFutbolista
ADD CONSTRAINT fk_plantillafutbolista_plantilla
FOREIGN KEY (id_plantilla) REFERENCES Plantilla(id_plantilla);

ALTER TABLE PlantillaFutbolista
ADD CONSTRAINT fk_plantillafutbolista_futbolista
FOREIGN KEY (id_futbolista) REFERENCES Futbolista(id_futbolista);

ALTER TABLE PuntuacionFutbolista
ADD CONSTRAINT fk_puntuacion_futbolista
FOREIGN KEY (id_futbolista) REFERENCES Futbolista(id_futbolista);      