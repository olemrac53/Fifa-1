-- Active: 1759169468515@@127.0.0.1@3306@5to_GranET12
USE 5to_GranET12;

-- 1. LIMPIAR DATOS DE PRUEBA
DELETE FROM PlantillaTitular WHERE id_plantilla IN (SELECT id_plantilla FROM Plantilla WHERE id_usuario IN (1, 2));
DELETE FROM PlantillaSuplente WHERE id_plantilla IN (SELECT id_plantilla FROM Plantilla WHERE id_usuario IN (1, 2));
DELETE FROM Plantilla WHERE id_usuario IN (1, 2);
DELETE FROM Usuario WHERE id_usuario IN (1, 2);
DELETE FROM Administrador;

-- 2. RESETEAR AUTO_INCREMENT
ALTER TABLE Usuario AUTO_INCREMENT = 1;
ALTER TABLE Administrador AUTO_INCREMENT = 1;

-- 3. INSERTAR USUARIOS DE PRUEBA
INSERT INTO Usuario (id_usuario, nombre, apellido, email, contrasenia, fecha_nacimiento)
VALUES 
(1, 'Juan', 'Perez', 'juan@mail.com', SHA2('pass123', 256), '1990-01-01'),
(2, 'Maria', 'Lopez', 'maria@mail.com', SHA2('pass456', 256), '1985-05-15');

-- 4. INSERTAR ADMINISTRADOR DE PRUEBA
INSERT INTO Administrador (nombre, apellido, email, contrasenia, fecha_nacimiento)
VALUES ('Admin', 'Test', 'admin@fifa.com', SHA2('123456', 256), '1990-01-01');

-- 5. VERIFICAR DATOS INSERTADOS
SELECT '=== USUARIOS DISPONIBLES ===' as info;
SELECT 
    id_usuario, 
    nombre, 
    apellido, 
    email,
    fecha_nacimiento
FROM Usuario
WHERE id_usuario IN (1, 2)
ORDER BY id_usuario;

SELECT '=== ADMINISTRADORES DISPONIBLES ===' as info;
SELECT * FROM Administrador;

-- Verificar Equipo
SELECT * FROM Equipo WHERE id_equipo = 1;

-- Verificar Tipos
SELECT * FROM Tipo WHERE id_tipo IN (1, 2, 3, 4);

-- Si no existen, ejecuta esto (sin UPDATE):
INSERT IGNORE INTO Equipo (id_equipo, nombre, presupuesto) 
VALUES (1, 'Equipo Test 1', 1000000.00);

INSERT IGNORE INTO Tipo (id_tipo, nombre) VALUES 
(1, 'Delantero'),
(2, 'Mediocampista'),
(3, 'Defensa'),
(4, 'Arquero');




-- Insertar futbolistas de prueba
INSERT IGNORE INTO Futbolista (id_futbolista, nombre, apellido, apodo, num_camisa, fecha_nacimiento, cotizacion, id_tipo, id_equipo)
VALUES 
(1, 'Lionel', 'Messi', 'Leo', '10', '1987-06-24', 500000.00, 1, 1),
(2, 'Cristiano', 'Ronaldo', 'CR7', '7', '1985-02-05', 450000.00, 1, 1),
(3, 'Neymar', 'Junior', 'Ney', '11', '1992-02-05', 400000.00, 1, 1);

SELECT '=== FUTBOLISTAS DISPONIBLES ===' as info;
SELECT * FROM Futbolista WHERE id_futbolista IN (1, 2, 3);