-- Active: 1700068523370@@127.0.0.1@3306@5to_GranET12
USE 5to_GranET12;

-- ==========================================================
-- 1. LIMPIAR DATOS DE PRUEBA (EN ORDEN DE DEPENDENCIA)
-- ==========================================================
-- Tablas de unión / "hijas" primero
DELETE FROM PlantillaTitular;
DELETE FROM PlantillaSuplente;
DELETE FROM PuntuacionFutbolista; -- (Faltaba esta limpieza)

-- Tablas principales
DELETE FROM Plantilla;
DELETE FROM Futbolista;
DELETE FROM Usuario;
DELETE FROM Administrador;
DELETE FROM Equipo;
DELETE FROM Tipo;

-- ==========================================================
-- 2. RESETEAR AUTO_INCREMENT (Para todas las tablas)
-- ==========================================================
ALTER TABLE Usuario AUTO_INCREMENT = 1;
ALTER TABLE Administrador AUTO_INCREMENT = 1;
ALTER TABLE Equipo AUTO_INCREMENT = 1;
ALTER TABLE Tipo AUTO_INCREMENT = 1;
ALTER TABLE Futbolista AUTO_INCREMENT = 1;
ALTER TABLE Plantilla AUTO_INCREMENT = 1;
ALTER TABLE PuntuacionFutbolista AUTO_INCREMENT = 1;

-- 3. INSERTAR DATOS LIMPIOS 
-- ==========================================================
INSERT INTO Equipo (id_equipo, nombre, presupuesto) 
VALUES 
(1, 'Equipo Test 1', 1000000.00),
(2, 'Equipo Test 2', 500000.00);

INSERT INTO Tipo (id_tipo, nombre) VALUES 
(1, 'Delantero'),
(2, 'Mediocampista'),
(3, 'Defensa'),
(4, 'Arquero');

-- INSERTAR USUARIOS Y ADMINS
INSERT INTO Usuario (id_usuario, nombre, apellido, email, contrasenia, fecha_nacimiento)
VALUES 
(1, 'Juan', 'Perez', 'juan@mail.com', SHA2('pass123', 256), '1990-01-01'),
(2, 'Maria', 'Lopez', 'maria@mail.com', SHA2('pass456', 256), '1985-05-15'),
(3, 'robensiño', 'torrensiño', 'robensiño@mail.com', SHA2('robem123', 256), '1999-12-31');

INSERT INTO Administrador (nombre, apellido, email, contrasenia, fecha_nacimiento)
VALUES ('Admin', 'Test', 'admin@fifa.com', SHA2('123456', 256), '1990-01-01');

-- INSERTAR FUTBOLISTAS 
INSERT INTO Futbolista (id_futbolista, nombre, apellido, apodo, num_camisa, fecha_nacimiento, cotizacion, id_tipo, id_equipo)
VALUES 
(1, 'Lionel', 'Messi', 'Leo', '10', '1987-06-24', 500000.00, 1, 1),
(2, 'Cristiano', 'Ronaldo', 'CR7', '7', '1985-02-05', 450000.00, 1, 1),
(3, 'Neymar', 'Junior', 'Ney', '11', '1992-02-05', 400000.00, 2, 1),
(4, 'Ruben', 'Nose', 'Catire', '69', '2005-04-06', 699999.00, 2, 2), 
(5, 'Carmelo', 'carmelon', 'pelota2', '007', '1987-11-06', 87777.00, 4, 1),
(6, 'Mica', 'Mica', 'pelota3 la secuela', '2', '2008-03-02', 13333.00, 2, 1),
(7, 'Micasa', 'En tu casa', 'Abrime', '23', '2005-03-02', 14444.00, 2, 1),

(8, 'Emanuel', 'manuel', 'de los manuel', '27', '2002-03-02', 15555.00, 3, 1),

(9, 'Mica', 'mika', 'pi', '45', '2003-03-02', 314159.00, 3, 1),

(10, 'Di', 'maria', 'santo', '77', '2009-03-02', 19999.00, 3, 1),

(11, 'John', 'Wick', 'Boogeyman', '12', '2006-03-02', 200000.00, 3, 1);





-- ==========================================================
-- 4. VERIFICAR DATOS INSERTADOS
-- ==========================================================
SELECT '=== USUARIOS DISPONIBLES ===' as info;
SELECT id_usuario, nombre, apellido, email FROM Usuario;

SELECT '=== ADMINISTRADORES DISPONIBLES ===' as info;
SELECT * FROM Administrador;

SELECT '=== EQUIPOS DISPONIBLES ===' as info;
SELECT * FROM Equipo;

SELECT '=== FUTBOLISTAS DISPONIBLES ===' as info;
SELECT * FROM Futbolista;

SELECT '=== PUNTUACIONES (Vacío) ===' as info;
SELECT * from PuntuacionFutbolista;