-- 00_DDL.sql
DROP DATABASE IF EXISTS GranET12;
CREATE DATABASE GranET12;
USE GranET12;

-- =============================
-- TABLA EQUIPO
-- =============================
CREATE TABLE Equipo (
    id_equipo INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL UNIQUE
);

-- =============================
-- TABLA TIPO
-- =============================
CREATE TABLE Tipo (
    id_tipo INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL UNIQUE
);

-- =============================
-- TABLA FUTBOLISTA
-- =============================
CREATE TABLE Futbolista (
    id_futbolista INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    num_camisa VARCHAR(45),
    apellido VARCHAR(100) NOT NULL,
    apodo VARCHAR(45),
    fecha_nacimiento DATE NOT NULL,
    cotizacion DECIMAL(10,2) NOT NULL CHECK (cotizacion >= 0 AND cotizacion <= 99999999.99),
    id_equipo INT NOT NULL,
    id_tipo INT NOT NULL,
    CONSTRAINT fk_futbolista_equipo FOREIGN KEY (id_equipo) REFERENCES Equipo(id_equipo),
    CONSTRAINT fk_futbolista_tipo FOREIGN KEY (id_tipo) REFERENCES Tipo(id_tipo)
);

-- =============================
-- TABLA USUARIO
-- =============================
CREATE TABLE Usuario (
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    fecha_nacimiento DATE NOT NULL,
    contrasenia CHAR(64) NOT NULL
);


-- =============================
-- TABLA Administrador
-- =============================
CREATE TABLE Administrador (
    id_administrador INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    fecha_nacimiento DATE NOT NULL,
    contrasenia CHAR(64) NOT NULL
);

-- =============================
-- TABLA PLANTILLA (encabezado)
-- =============================
CREATE TABLE Plantilla (
    id_plantilla INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    id_administrador INT NOT NULL,
    presupuesto_max DECIMAL(10,2) NOT NULL,
    cant_max_futbolistas INT NOT NULL,
    CONSTRAINT fk_plantilla_usuario FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario)

);

-- =============================
-- PLANTILLA TITULAR y SUPLENTE (subconjuntos)
-- =============================
CREATE TABLE PlantillaTitular (
    id_plantilla_titular INT AUTO_INCREMENT PRIMARY KEY,
    id_plantilla INT NOT NULL,
    id_futbolista INT NOT NULL,
    CONSTRAINT uq_titular UNIQUE (id_plantilla, id_futbolista),
    CONSTRAINT fk_titular_plantilla FOREIGN KEY (id_plantilla) REFERENCES Plantilla(id_plantilla) ON DELETE CASCADE,
    CONSTRAINT fk_titular_futbolista FOREIGN KEY (id_futbolista) REFERENCES Futbolista(id_futbolista) ON DELETE CASCADE
);

CREATE TABLE PlantillaSuplente (
    id_plantilla_suplente INT AUTO_INCREMENT PRIMARY KEY,
    id_plantilla INT NOT NULL,
    id_futbolista INT NOT NULL,
    CONSTRAINT uq_suplente UNIQUE (id_plantilla, id_futbolista),
    CONSTRAINT fk_suplente_plantilla FOREIGN KEY (id_plantilla) REFERENCES Plantilla(id_plantilla) ON DELETE CASCADE,
    CONSTRAINT fk_suplente_futbolista FOREIGN KEY (id_futbolista) REFERENCES Futbolista(id_futbolista) ON DELETE CASCADE
);

-- =============================
-- TABLA PLANTILLA-FUTBOLISTA GENERAL (se mantiene para compatibilidad)
-- Se sincroniza con Titular/Suplente por triggers.
-- =============================
CREATE TABLE PlantillaFutbolista (
    id_plantilla_futbolista INT AUTO_INCREMENT PRIMARY KEY,
    id_plantilla INT NOT NULL,
    id_futbolista INT NOT NULL,
    es_titular BOOLEAN NOT NULL,
    CONSTRAINT uq_plantilla_futbolista UNIQUE (id_plantilla, id_futbolista),
    CONSTRAINT fk_plantillafutbolista_plantilla FOREIGN KEY (id_plantilla) REFERENCES Plantilla(id_plantilla) ON DELETE CASCADE,
    CONSTRAINT fk_plantillafutbolista_futbolista FOREIGN KEY (id_futbolista) REFERENCES Futbolista(id_futbolista) ON DELETE CASCADE
);

-- =============================
-- TABLA PUNTUACION
-- =============================
CREATE TABLE PuntuacionFutbolista (
    id_puntuacion INT AUTO_INCREMENT PRIMARY KEY,
    id_futbolista INT NOT NULL,
    fecha INT NOT NULL CHECK (fecha >= 1 AND fecha <= 49),
    puntuacion DECIMAL(3,1) NOT NULL CHECK (puntuacion >= 1.0 AND puntuacion <= 10.0),
    CONSTRAINT uq_futbolista_fecha UNIQUE (id_futbolista, fecha),
    CONSTRAINT fk_puntuacion_futbolista FOREIGN KEY (id_futbolista)
        REFERENCES Futbolista(id_futbolista)
        ON DELETE CASCADE
);





